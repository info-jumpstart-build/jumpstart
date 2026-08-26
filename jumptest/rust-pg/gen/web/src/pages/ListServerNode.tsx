import { useCallback, useEffect, useRef, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { useApiClient, openNotificationStream, type ApiClient } from "../api/apiClient";
import DataTable, { type DataTableColumn } from "../components/DataTable";

// Row shape returned by GET /api/servernode and /api/servernode/view/{id}
// -- matches the ServerNodeView class generated for the dotnet/rust servers.
export interface ServerNodeView {
  id: number;
  server_node_type_id: number;
  hostname: string;
  ip_address: string;
  port: number;
  username: string;
  url: string;
  user_domain: string;
  os_name: string;
  os_version: string;
  architecture: string;
  registered_at: string;
  server_node_status_id: number;
  is_active: number;
  created_by: string;
  last_updated: string;
  last_updated_by: string;
  txn_id: number;
  server_node_type_name: string;
  server_node_status_name: string;
}

const DOMAIN_OBJECT_NAME = "ServerNode";

const columns: DataTableColumn[] = [
  { key: "id", label: "Server Node ID" },
  { key: "hostname", label: "Hostname" },
  { key: "ip_address", label: "Address" },
  { key: "port", label: "Port" },
  { key: "username", label: "Username" },
  { key: "url", label: "URL" },
  { key: "user_domain", label: "User Domain" },
  { key: "os_name", label: "OS Name" },
  { key: "os_version", label: "OS Version" },
  { key: "architecture", label: "Architecture" },
  { key: "registered_at", label: "Registered At" },
  { key: "is_active", label: "Active" },
  { key: "created_by", label: "Created By" },
  { key: "last_updated", label: "Last Updated" },
  { key: "last_updated_by", label: "Last Updated By" },
  { key: "server_node_type_name", label: "Server Node Type" },
  { key: "server_node_status_name", label: "Status" },
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

async function fetchList(api: ApiClient): Promise<ServerNodeView[]> {
  return api.get<ServerNodeView[]>("/api/servernode");
}

export default function ListServerNode() {
  const api = useApiClient();
  const navigate = useNavigate();
  const location = useLocation();

  const [list, setList] = useState<ServerNodeView[] | null>(null);
  // Typed via the initial value's "as" cast (rather than useRef<EventSource
  // | null>(...)) -- a bare `Identifier<...>` outside a `<text>`-wrapped code
  // region can make RazorLight's markup parser mistake it for an HTML/JSX
  // tag opening.
  const sseRef = useRef(null as EventSource | null);

  // Mirrors the current `list` state so the SSE handler below (registered
  // once per `api` change, not per render) always sees the latest data
  // instead of the value captured when the effect first ran.
  const listRef = useRef<ServerNodeView[] | null>(null);
  useEffect(() => {
    listRef.current = list;
  }, [list]);

  const reloadList = useCallback(async () => {
    try {
      setList(await fetchList(api));
    } catch (err) {
      console.error("ListServerNode: error reloading list", err);
    }
  }, [api]);

  useEffect(() => {
    let cancelled = false;

    fetchList(api)
      .then((data) => {
        if (!cancelled) setList(data);
      })
      .catch((err) => console.error("ListServerNode: error loading list", err));

    return () => {
      cancelled = true;
    };
  }, [api]);

  // Real-time updates arrive over Server-Sent Events -- the same
  // GET /api/Notification/stream the Blazor client's notifications.js
  // interop connects to. The browser's native EventSource needs no interop
  // layer here and reconnects on its own; we just clean up on unmount.
  // Opening it is async (openNotificationStream first mints a stream ticket
  // over a normal, authenticated POST, since EventSource can't send an
  // Authorization header itself), so this guards against the effect's
  // cleanup running before that mint call resolves.
  useEffect(() => {
    let cancelled = false;

    openNotificationStream(api)
      .then((source) => {
        if (cancelled) {
          source.close();
          return;
        }
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
              const updated = await api.get<ServerNodeView>(`/api/servernode/view/${message.InstanceId}`);
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
            console.error("ListServerNode: error handling notification", err);
          }
        });
      })
      .catch((err) => console.error("ListServerNode: error opening notification stream", err));

    return () => {
      cancelled = true;
      sseRef.current?.close();
      sseRef.current = null;
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [api]);

  function onAdd() {
    const returnUrl = encodeURIComponent(location.pathname);
    navigate(`/edit-servernode?returnUrl=${returnUrl}`);
  }

  function onEdit(item: ServerNodeView) {
    const returnUrl = encodeURIComponent(location.pathname);
    navigate(`/edit-servernode/${item.id}?returnUrl=${returnUrl}`);
  }

  return (
    <>
      <h1>Server Node</h1>
      <p>This page demonstrates fetching Server Node data from the server.</p>

      <DataTable data={list} columns={columns} showActions showAddButton onEdit={onEdit} onAdd={onAdd} />
    </>
  );
}
