// Mirrors web/blazor/Components/DataTable.razor.cshtml. Blazor derives its
// columns at runtime via reflection over [ColumnInfo] attributes; TS/JS has
// no attribute reflection, so the per-object list page (Listtemplate.tsx)
// computes the equivalent `columns` array at *generation* time (from the
// same MetaObject.Attributes + FK-view data the C# ColumnInfo attributes
// come from) and passes it in as a prop instead.
import "./DataTable.css";

export interface DataTableColumn {
  key: string;
  label: string;
}

// Deliberately unconstrained: the generated row interfaces (e.g. CustomerView)
// have no index signature, so `T extends Record<string, unknown>` would
// reject them at every call site. Column values are read via a cast to
// Record<string, unknown> inside the component instead.
interface DataTableProps<T> {
  data: T[] | null;
  columns: DataTableColumn[];
  showActions?: boolean;
  showAddButton?: boolean;
  onEdit?: (item: T) => void;
  onDelete?: (item: T) => void;
  onAdd?: () => void;
}

function formatValue(value: unknown): string {
  if (value === null || value === undefined) return "";
  if (typeof value === "string" && value === "{}") return "";
  return String(value);
}

export default function DataTable<T>({
  data,
  columns,
  showActions = true,
  showAddButton = true,
  onEdit,
  onDelete,
  onAdd,
}: DataTableProps<T>) {
  const colSpan = columns.length + (showActions ? 1 : 0);

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
                return (
                  <tr key={String(row.id ?? index)}>
                    {columns.map((column) =>
                      column.key === "id" && onEdit ? (
                        <td key={column.key}>
                          <a
                            href="#"
                            onClick={(e) => {
                              e.preventDefault();
                              onEdit(item);
                            }}
                          >
                            {formatValue(row[column.key])}
                          </a>
                        </td>
                      ) : (
                        <td key={column.key}>{formatValue(row[column.key])}</td>
                      ),
                    )}
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
    </>
  );
}
