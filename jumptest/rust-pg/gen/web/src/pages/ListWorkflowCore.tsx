import { useCallback, useEffect, useRef, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { useApiClient, openNotificationStream, type ApiClient } from "../api/apiClient";
import DataTableContextMenu, { type DataTableContextMenuColumn, type ContextMenuItem } from "../components/DataTableContextMenu";

// Row shape returned by GET /api/workflow and /api/workflow/view/{id} --
// matches the WorkflowView class generated for the dotnet/rust servers.
// exec_status_image is included here (even though it's not one of the
// visible `columns` below) since DataTableContextMenu reads it off the row
// at render time to build the "exec_status_name" column's <img src>.
export interface WorkflowView {
  id: number;
  workflow_type_id: number;
  parent_id: number;
  name: string;
  seq: number;
  server_node_id: number;
  process_id: number;
  exec_status_id: number;
  last_start_time: string;
  last_end_time: string;
  schedule_id: number;
  on_failure_action_id: number;
  is_active: number;
  created_by: string;
  last_updated: string;
  last_updated_by: string;
  txn_id: number;
  workflow_type_name: string;
  parent_parent_id: number;
  parent_name: string;
  server_node_hostname: string;
  server_node_port: number;
  process_name: string;
  exec_status_name: string;
  exec_status_image: string;
  schedule_name: string;
  on_failure_action_action: string;
}

const DOMAIN_OBJECT_NAME = "Workflow";

const columns: DataTableContextMenuColumn[] = [
  { key: "id", label: "Workflow ID" },
  { key: "name", label: "Name" },
  { key: "seq", label: "Sequence" },
  { key: "last_start_time", label: "Last Start Time" },
  { key: "last_end_time", label: "LastEnd Time" },
  { key: "is_active", label: "Active" },
  { key: "created_by", label: "Created By" },
  { key: "last_updated", label: "Last Updated" },
  { key: "last_updated_by", label: "Last Updated By" },
  { key: "workflow_type_name", label: "Type" },
  { key: "parent_parent_id", label: "Parent Parent" },
  { key: "parent_name", label: "Parent" },
  { key: "server_node_hostname", label: "Agent Hostname" },
  { key: "server_node_port", label: "Agent Port" },
  { key: "process_name", label: "Process" },
  { key: "exec_status_name", label: "Status", imageKey: "exec_status_image" },
  { key: "schedule_name", label: "Schedule" },
  { key: "on_failure_action_action", label: "On Failure Action" },
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

// No explicit `: Promise<WorkflowView[]>` return type here -- a bare
// `Identifier<...>` outside a `<text>`-wrapped code region can make
// RazorLight's markup parser mistake it for an HTML/JSX tag opening, same
// as everywhere else in this file. TypeScript infers the return type fine
// from the `as WorkflowView[]` cast below.
async function fetchWorkflowList(api: ApiClient) {
  return (await api.get("/api/workflow")) as WorkflowView[];
}

export default function ListWorkflowCore() {
  const api = useApiClient();
  const navigate = useNavigate();
  const location = useLocation();

  const [list, setList] = useState(null as WorkflowView[] | null);
  // Typed via the initial value's "as" cast (rather than useRef<EventSource
  // | null>(...)) -- a bare `Identifier<...>` outside a `<text>`-wrapped code
  // region can make RazorLight's markup parser mistake it for an HTML/JSX
  // tag opening.
  const sseRef = useRef(null as EventSource | null);
  const listRef = useRef(null as WorkflowView[] | null);
  useEffect(() => {
    listRef.current = list;
  }, [list]);

  const reloadList = useCallback(async () => {
    try {
      setList(await fetchWorkflowList(api));
    } catch (err) {
      console.error("ListWorkflowCore: error reloading list", err);
    }
  }, [api]);

  useEffect(() => {
    let cancelled = false;

    fetchWorkflowList(api)
      .then((data) => {
        if (!cancelled) setList(data);
      })
      .catch((err) => console.error("ListWorkflowCore: error loading list", err));

    return () => {
      cancelled = true;
    };
  }, [api]);

  // Real-time updates, same SSE stream every other list page uses. Opening
  // the stream is async (it first mints a stream ticket over a normal,
  // authenticated POST -- see openNotificationStream), so this guards against
  // the effect's cleanup running before that mint call resolves (e.g. fast
  // unmount right after mount).
  useEffect(() => {
    let cancelled = false;

    openNotificationStream(api)
      .then((source) => {
        if (cancelled) {
          source.close();
          return;
        }
        sseRef.current = source;

        source.addEventListener("PropertyUpdated", async (event: MessageEvent) => {
          try {
            const message: PropertyUpdateMessage = JSON.parse(event.data);
            if (message.DomainObjectName !== DOMAIN_OBJECT_NAME) return;

            const index = (listRef.current ?? []).findIndex((item) => item.id === message.InstanceId);
            if (index >= 0) {
              const updated = (await api.get(`/api/workflow/view/${message.InstanceId}`)) as WorkflowView;
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
            console.error("ListWorkflowCore: error handling notification", err);
          }
        });
      })
      .catch((err) => console.error("ListWorkflowCore: error opening notification stream", err));

    return () => {
      cancelled = true;
      sseRef.current?.close();
      sseRef.current = null;
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [api]);

  // Fire-and-forget, same as Blazor's RunWorkflow (no result to show --
  // progress/status shows up via the SSE-driven exec_status update above).
  function runWorkflow(id: number) {
    api.get(`/api/workflow/run/${id}`).catch((err) => {
      console.error("ListWorkflowCore: error running workflow", err);
    });
  }

  const contextMenuItems: ContextMenuItem[] = [{ label: "Run", action: runWorkflow }];

  function onAdd() {
    const returnUrl = encodeURIComponent(location.pathname);
    navigate(`/edit-workflow?returnUrl=${returnUrl}`);
  }

  function onEdit(item: WorkflowView) {
    const returnUrl = encodeURIComponent(location.pathname);
    navigate(`/edit-workflow/${item.id}?returnUrl=${returnUrl}`);
  }

  return (
    <>
      <h1>Workflow</h1>
      <p>This page demonstrates fetching Workflow data from the server.</p>

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
