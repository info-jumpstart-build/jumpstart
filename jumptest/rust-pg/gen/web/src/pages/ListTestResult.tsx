import { useCallback, useEffect, useRef, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { useApiClient, type ApiClient } from "../api/apiClient";
import DataTable, { type DataTableColumn } from "../components/DataTable";

// Row shape returned by GET /api/testresult and /api/testresult/view/{id}
// -- matches the TestResultView class generated for the dotnet/rust servers.
export interface TestResultView {
  id: number;
  test_run_id: number;
  test_case_id: number;
  test_result_status_id: number;
  executed_at: string;
  executed_by_id: number;
  actual_result: string;
  notes: string;
  is_active: number;
  created_by: string;
  last_updated: string;
  last_updated_by: string;
  txn_id: number;
  test_run_name: string;
  test_run_test_plan_id: number;
  test_case_test_plan_id: number;
  test_case_code: string;
  test_case_title: string;
  test_result_status_name: string;
  executed_by_email: string;
  executed_by_principal_status_id: number;
}

const DOMAIN_OBJECT_NAME = "TestResult";

const columns: DataTableColumn[] = [
  { key: "id", label: "Test Result ID" },
  { key: "executed_at", label: "Executed At" },
  { key: "actual_result", label: "Actual Result" },
  { key: "notes", label: "Notes" },
  { key: "is_active", label: "Active" },
  { key: "created_by", label: "Created By" },
  { key: "last_updated", label: "Last Updated" },
  { key: "last_updated_by", label: "Last Updated By" },
  { key: "test_run_name", label: "Test Run" },
  { key: "test_run_test_plan_id", label: "Test Run Test Plan" },
  { key: "test_case_test_plan_id", label: "Test Case Test Plan" },
  { key: "test_case_code", label: "Test Case Code" },
  { key: "test_case_title", label: "Test Case Title" },
  { key: "test_result_status_name", label: "Status" },
  { key: "executed_by_email", label: "Executed By Email" },
  { key: "executed_by_principal_status_id", label: "Executed By Status" },
];

// Matches the server's PropertyUpdateMessage (see
// server/dotnet/api/EventAggregator.core.cs.cshtml and
// shared/rust/logic/notification.core.rs.cshtml) -- PascalCase field names
// and all, since it is serialized once and shared verbatim by every client,
// Blazor included.
interface PropertyUpdateMessage {
  DomainObjectName: string;
  InstanceId: number;
  JsonData: string;
  Timestamp: string;
}

async function fetchList(api: ApiClient): Promise<TestResultView[]> {
  return api.get<TestResultView[]>("/api/testresult");
}

export default function ListTestResult() {
  const api = useApiClient();
  const navigate = useNavigate();
  const location = useLocation();

  const [list, setList] = useState<TestResultView[] | null>(null);
  // Typed via the initial value's "as" cast (rather than useRef<EventSource
  // | null>(...)) -- a bare `Identifier<...>` outside a `<text>`-wrapped code
  // region can make RazorLight's markup parser mistake it for an HTML/JSX
  // tag opening.
  const sseRef = useRef(null as EventSource | null);

  // Mirrors the current `list` state so the SSE handler below (registered
  // once per `api` change, not per render) always sees the latest data
  // instead of the value captured when the effect first ran.
  const listRef = useRef<TestResultView[] | null>(null);
  useEffect(() => {
    listRef.current = list;
  }, [list]);

  const reloadList = useCallback(async () => {
    try {
      setList(await fetchList(api));
    } catch (err) {
      console.error("ListTestResult: error reloading list", err);
    }
  }, [api]);

  useEffect(() => {
    let cancelled = false;

    fetchList(api)
      .then((data) => {
        if (!cancelled) setList(data);
      })
      .catch((err) => console.error("ListTestResult: error loading list", err));

    return () => {
      cancelled = true;
    };
  }, [api]);

  // Real-time updates arrive over Server-Sent Events -- the same
  // GET /api/Notification/stream the Blazor client's notifications.js
  // interop connects to. The browser's native EventSource needs no interop
  // layer here and reconnects on its own; we just clean up on unmount.
  useEffect(() => {
    const source = new EventSource(`${api.baseUrl}/api/Notification/stream`);
    sseRef.current = source;

    // MessageEvent's generic parameter defaults to `any` when omitted, so
    // this stays a bare `MessageEvent` rather than `MessageEvent<string>`
    // (same reasoning as the sseRef declaration above).
    source.addEventListener("PropertyUpdated", async (event: MessageEvent) => {
      try {
        const message: PropertyUpdateMessage = JSON.parse(event.data);
        if (message.DomainObjectName !== DOMAIN_OBJECT_NAME) return;

        const index = (listRef.current ?? []).findIndex((item) => item.id === message.InstanceId);
        if (index >= 0) {
          const updated = await api.get<TestResultView>(`/api/testresult/view/${message.InstanceId}`);
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
        console.error("ListTestResult: error handling notification", err);
      }
    });

    return () => {
      source.close();
      sseRef.current = null;
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [api]);

  function onAdd() {
    const returnUrl = encodeURIComponent(location.pathname);
    navigate(`/edit-testresult?returnUrl=${returnUrl}`);
  }

  function onEdit(item: TestResultView) {
    const returnUrl = encodeURIComponent(location.pathname);
    navigate(`/edit-testresult/${item.id}?returnUrl=${returnUrl}`);
  }

  return (
    <>
      <h1>Test Result</h1>
      <p>This page demonstrates fetching Test Result data from the server.</p>

      <DataTable data={list} columns={columns} showActions showAddButton onEdit={onEdit} onAdd={onAdd} />
    </>
  );
}
