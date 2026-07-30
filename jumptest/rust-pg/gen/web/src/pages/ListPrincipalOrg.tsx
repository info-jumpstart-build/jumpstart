import { useCallback, useEffect, useRef, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { useApiClient, type ApiClient } from "../api/apiClient";
import DataTable, { type DataTableColumn } from "../components/DataTable";

// Row shape returned by GET /api/principalorg and /api/principalorg/view/{id}
// -- matches the PrincipalOrgView class generated for the dotnet/rust servers.
export interface PrincipalOrgView {
  id: number;
  org_id: number;
  principal_id: number;
  is_active: number;
  created_by: string;
  last_updated: string;
  last_updated_by: string;
  txn_id: number;
  org_name: string;
  principal_email: string;
  principal_principal_status_id: number;
}

const DOMAIN_OBJECT_NAME = "PrincipalOrg";

const columns: DataTableColumn[] = [
  { key: "id", label: "Principal Org ID" },
  { key: "is_active", label: "Active" },
  { key: "created_by", label: "Created By" },
  { key: "last_updated", label: "Last Updated" },
  { key: "last_updated_by", label: "Last Updated By" },
  { key: "org_name", label: "Organization ID" },
  { key: "principal_email", label: "Principal ID Email" },
  { key: "principal_principal_status_id", label: "Principal ID Status" },
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

async function fetchList(api: ApiClient): Promise<PrincipalOrgView[]> {
  return api.get<PrincipalOrgView[]>("/api/principalorg");
}

export default function ListPrincipalOrg() {
  const api = useApiClient();
  const navigate = useNavigate();
  const location = useLocation();

  const [list, setList] = useState<PrincipalOrgView[] | null>(null);
  // Typed via the initial value's "as" cast (rather than useRef<EventSource
  // | null>(...)) -- a bare `Identifier<...>` outside a `<text>`-wrapped code
  // region can make RazorLight's markup parser mistake it for an HTML/JSX
  // tag opening.
  const sseRef = useRef(null as EventSource | null);

  // Mirrors the current `list` state so the SSE handler below (registered
  // once per `api` change, not per render) always sees the latest data
  // instead of the value captured when the effect first ran.
  const listRef = useRef<PrincipalOrgView[] | null>(null);
  useEffect(() => {
    listRef.current = list;
  }, [list]);

  const reloadList = useCallback(async () => {
    try {
      setList(await fetchList(api));
    } catch (err) {
      console.error("ListPrincipalOrg: error reloading list", err);
    }
  }, [api]);

  useEffect(() => {
    let cancelled = false;

    fetchList(api)
      .then((data) => {
        if (!cancelled) setList(data);
      })
      .catch((err) => console.error("ListPrincipalOrg: error loading list", err));

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
          const updated = await api.get<PrincipalOrgView>(`/api/principalorg/view/${message.InstanceId}`);
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
        console.error("ListPrincipalOrg: error handling notification", err);
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
    navigate(`/edit-principalorg?returnUrl=${returnUrl}`);
  }

  function onEdit(item: PrincipalOrgView) {
    const returnUrl = encodeURIComponent(location.pathname);
    navigate(`/edit-principalorg/${item.id}?returnUrl=${returnUrl}`);
  }

  return (
    <>
      <h1>Principals</h1>
      <p>This page demonstrates fetching Principals data from the server.</p>

      <DataTable data={list} columns={columns} showActions showAddButton onEdit={onEdit} onAdd={onAdd} />
    </>
  );
}
