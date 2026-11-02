// Mirrors web/blazor/Components/DataTableContextMenu.razor.cshtml: a
// variant of DataTable that adds right-click context-menu actions and
// optional per-column image rendering. Kept as its own component rather
// than bolted onto DataTable.tsx, same as Blazor keeps DataTableContextMenu
// separate from DataTable -- these two features only exist for
// src/pages/ListWorkflowCore.tsx.cshtml (see there) and shouldn't add
// complexity to the shared component every ordinary list page uses.
import { useState } from "react";
import type { MouseEvent } from "react";
import "./DataTableContextMenu.css";

export interface DataTableContextMenuColumn {
  key: string;
  label: string;
  // If set, render this column as an <img> using the row's [imageKey]
  // field as the src (falling back to the column's own text value when
  // that field is empty) instead of plain text -- see ListWorkflowCore's
  // "exec_status_name" column, which shows exec_status's "image" RWK
  // column instead of its raw text.
  imageKey?: string;
}

export interface ContextMenuItem {
  label: string;
  action: (rowId: number) => void;
}

interface DataTableContextMenuProps<T> {
  data: T[] | null;
  columns: DataTableContextMenuColumn[];
  showActions?: boolean;
  showAddButton?: boolean;
  onEdit?: (item: T) => void;
  onDelete?: (item: T) => void;
  onAdd?: () => void;
  contextMenuItems?: ContextMenuItem[];
}

function formatValue(value: unknown): string {
  if (value === null || value === undefined) return "";
  if (typeof value === "string" && value === "{}") return "";
  return String(value);
}

function rowId(row: Record<string, unknown>): number {
  const value = row.id;
  return typeof value === "number" ? value : Number(value ?? 0);
}

// Normalize an image-column value to a root-relative asset URL. Values like
// "images/exec-status-1.png" (the exec_status.image RWK) come from the shared
// core data and are relative -- Blazor resolves them against its <base href="/">,
// but this SPA has no <base>, so a bare relative src would resolve against the
// current route (e.g. /core/images/...) and 404. Anchor it at the site root,
// matching how the app references its other public assets (/css, /lib, ...).
// Absolute URLs and data: URIs are passed through untouched.
function assetSrc(value: unknown): string {
  const src = String(value ?? "");
  if (src === "" || /^(https?:\/\/|\/|data:)/i.test(src)) return src;
  return "/" + src;
}

export default function DataTableContextMenu<T>({
  data,
  columns,
  showActions = true,
  showAddButton = true,
  onEdit,
  onDelete,
  onAdd,
  contextMenuItems,
}: DataTableContextMenuProps<T>) {
  const [menu, setMenu] = useState(null as { x: number; y: number; rowId: number } | null);

  const colSpan = columns.length + (showActions ? 1 : 0);

  function onRowContextMenu(e: MouseEvent, id: number) {
    if (!contextMenuItems || contextMenuItems.length === 0) return;
    e.preventDefault();
    setMenu({ x: e.clientX, y: e.clientY, rowId: id });
  }

  function invokeContextAction(item: ContextMenuItem) {
    const id = menu?.rowId ?? 0;
    setMenu(null);
    item.action(id);
  }

  return (
    <>
      <div className="table-responsive">
        <table className="table table-striped table-bordered table-hover">
          <thead className="table-dark">
            <tr>
              {columns.map((column) => (
                <th key={column.key}>{column.label}</th>
              ))}
              {showActions && <th>Actions</th>}
            </tr>
          </thead>
          <tbody>
            {data == null ? (
              <tr>
                <td colSpan={colSpan} className="text-center text-muted">
                  Loading...
                </td>
              </tr>
            ) : data.length === 0 ? (
              <tr>
                <td colSpan={colSpan} className="text-center text-muted">
                  No data available
                </td>
              </tr>
            ) : (
              data.map((item, index) => {
                const row = item as Record<string, unknown>;
                const id = rowId(row);
                return (
                  <tr key={String(row.id ?? index)} className="ctx-row" onContextMenu={(e) => onRowContextMenu(e, id)}>
                    {columns.map((column) => (
                      <td key={column.key}>
                        {column.imageKey && row[column.imageKey] ? (
                          <img
                            src={assetSrc(row[column.imageKey])}
                            alt={formatValue(row[column.key])}
                            title={formatValue(row[column.key])}
                            style={{ height: "24px" }}
                          />
                        ) : column.key === "id" && onEdit ? (
                          <a
                            href="#"
                            onClick={(e) => {
                              e.preventDefault();
                              onEdit(item);
                            }}
                          >
                            {formatValue(row[column.key])}
                          </a>
                        ) : (
                          formatValue(row[column.key])
                        )}
                      </td>
                    ))}
                    {showActions && (
                      <td>
                        {onDelete && (
                          <button className="btn btn-danger btn-sm" onClick={() => onDelete(item)}>
                            Delete
                          </button>
                        )}
                      </td>
                    )}
                  </tr>
                );
              })
            )}
          </tbody>
        </table>
      </div>

      {showAddButton && onAdd && (
        <div className="mt-3">
          <button className="btn btn-primary" onClick={onAdd}>
            Add
          </button>
        </div>
      )}

      {menu && contextMenuItems && contextMenuItems.length > 0 && (
        <>
          <div className="ctx-overlay" onClick={() => setMenu(null)} />
          <div className="ctx-menu" style={{ left: menu.x, top: menu.y }}>
            {contextMenuItems.map((item) => (
              <div key={item.label} className="ctx-menu-item" onClick={() => invokeContextAction(item)}>
                {item.label}
              </div>
            ))}
          </div>
        </>
      )}
    </>
  );
}
