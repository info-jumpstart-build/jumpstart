// Custom list page for TestPlan, mirroring the ListWorkflowCore convention
// (gen/web/src/pages/ListWorkflowCore.tsx): a right-click context menu adds a
// custom action alongside the standard Edit/Add affordances every DataTable
// page gets. Here the action is "Generate" -- it calls the custom API route
// usr/server/api/user_api.rs wires up (POST /api/testplan/generate/{id}),
// which in turn calls TestPlanLogic::generate (usr/shared/logic/TestPlanLogic.user.rs):
// creates a new TestRun for the plan and one Unexecuted TestResult per active
// TestCase.
//
// Hand-written (usr/web), not generated: this file replaces the generated
// gen/web/src/pages/ListTestPlan.tsx (same name -- no "Core" suffix needed;
// that convention exists only for pages, like ListWorkflowCore, that must
// coexist alongside the generated per-object page of the same domain object
// under a different name). This one fully supersedes the generated version:
// it's copied over the generated project into bin/web at build time (see
// gen/web/makefile's `cp -r -f ../../usr/web/. $(TARGETDIR)` step), so both
// the default "/testplan" route and the custom "/usr/testplan" route (added
// in usr/web/src/App.tsx, matching test_plan's URI column) end up serving
// this component.
import { useCallback, useEffect, useRef, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { useApiClient, type ApiClient } from "../api/apiClient";
import DataTableContextMenu, { type DataTableContextMenuColumn, type ContextMenuItem } from "../components/DataTableContextMenu";

// Row shape returned by GET /api/testplan and /api/testplan/view/{id} --
// matches the TestPlanView class generated for the dotnet/rust servers.
export interface TestPlanView {
  id: number;
  name: string;
  description: string;
  is_active: number;
  created_by: string;
  last_updated: string;
  last_updated_by: string;
  txn_id: number;
}

const DOMAIN_OBJECT_NAME = "TestPlan";

const columns: DataTableContextMenuColumn[] = [
  { key: "id", label: "Test Plan ID" },
  { key: "name", label: "Name" },
  { key: "description", label: "Description" },
  { key: "is_active", label: "Active" },
  { key: "created_by", label: "Created By" },
  { key: "last_updated", label: "Last Updated" },
  { key: "last_updated_by", label: "Last Updated By" },
];

// Matches the server's PropertyUpdateMessage (see
// server/dotnet/api/EventAggregator.core.cs.cshtml and
// shared/rust/logic/notification.core.rs.cshtml).
interface PropertyUpdateMessage {
  DomainObjectName: string;
  InstanceId: number;
  JsonData: string;
  Timestamp: string;
}

async function fetchTestPlanList(api: ApiClient): Promise<TestPlanView[]> {
  return api.get<TestPlanView[]>("/api/testplan");
}

export default function ListTestPlan() {
  const api = useApiClient();
  const navigate = useNavigate();
  const location = useLocation();

  const [list, setList] = useState(null as TestPlanView[] | null);
  const sseRef = useRef(null as EventSource | null);
  const listRef = useRef(null as TestPlanView[] | null);
  useEffect(() => {
    listRef.current = list;
  }, [list]);

  const reloadList = useCallback(async () => {
    try {
      setList(await fetchTestPlanList(api));
    } catch (err) {
      console.error("ListTestPlan: error reloading list", err);
    }
  }, [api]);

  useEffect(() => {
    let cancelled = false;

    fetchTestPlanList(api)
      .then((data) => {
        if (!cancelled) setList(data);
      })
      .catch((err) => console.error("ListTestPlan: error loading list", err));

    return () => {
      cancelled = true;
    };
  }, [api]);

  // Real-time updates, same SSE stream every other list page uses.
  useEffect(() => {
    const source = new EventSource(`${api.baseUrl}/api/Notification/stream`);
    sseRef.current = source;

    source.addEventListener("PropertyUpdated", async (event: MessageEvent) => {
      try {
        const message: PropertyUpdateMessage = JSON.parse(event.data);
        if (message.DomainObjectName !== DOMAIN_OBJECT_NAME) return;

        const index = (listRef.current ?? []).findIndex((item) => item.id === message.InstanceId);
        if (index >= 0) {
          const updated = await api.get<TestPlanView>(`/api/testplan/view/${message.InstanceId}`);
          setList((current) => {
            if (!current) return current;
            const next = [...current];
            const i = next.findIndex((item) => item.id === message.InstanceId);
            if (i >= 0) next[i] = updated;
            return next;
          });
        } else {
          await reloadList();
        }
      } catch (err) {
        console.error("ListTestPlan: error handling notification", err);
      }
    });

    return () => {
      source.close();
      sseRef.current = null;
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [api]);

  // POST /api/testplan/generate/{id} -- usr/server/api/user_api.rs routes this
  // to TestPlanLogic::generate, which creates a new TestRun (and one
  // Unexecuted TestResult per active TestCase in the plan). Report the new
  // test_run id, then offer to jump to the Test Run list to record results.
  function generate(id: number) {
    api
      .post<{ test_plan_id: number; test_run_id: number }>(`/api/testplan/generate/${id}`, {})
      .then(({ test_run_id }) => {
        if (window.confirm(`Test run ${test_run_id} created. Go to Test Runs now?`)) {
          navigate("/testrun");
        }
      })
      .catch((err) => {
        console.error("ListTestPlan: error generating test run", err);
        window.alert(`Generate failed: ${err instanceof Error ? err.message : String(err)}`);
      });
  }

  const contextMenuItems: ContextMenuItem[] = [{ label: "Generate", action: generate }];

  function onAdd() {
    const returnUrl = encodeURIComponent(location.pathname);
    navigate(`/edit-testplan?returnUrl=${returnUrl}`);
  }

  function onEdit(item: TestPlanView) {
    const returnUrl = encodeURIComponent(location.pathname);
    navigate(`/edit-testplan/${item.id}?returnUrl=${returnUrl}`);
  }

  return (
    <>
      <h1>Test Plan</h1>
      <p>Right-click a plan and choose "Generate" to create a new test run with an Unexecuted result for every active test case.</p>

      <DataTableContextMenu
        data={list}
        columns={columns}
        showActions
        showAddButton
        onEdit={onEdit}
        onAdd={onAdd}
        contextMenuItems={contextMenuItems}
      />
    </>
  );
}
