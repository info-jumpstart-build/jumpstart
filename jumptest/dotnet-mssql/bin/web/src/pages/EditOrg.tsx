import { useEffect, useMemo, useState, type FormEventHandler, type ReactNode } from "react";
import { useNavigate, useParams, useLocation } from "react-router-dom";
import { useApiClient } from "../api/apiClient";
import DataTable, { type DataTableColumn } from "../components/DataTable";
import TabControl, { type TabItem } from "../components/TabControl";
import "../styles/editForm.css";
import type { Dispatch, SetStateAction } from "react";

// Raw shape sent/received by GET|POST|PUT /api/org[/{id}] -- matches
// the Org domain class generated for the dotnet/rust servers (every
// attribute, unlike the *View interface the list page works with).
export interface Org {
  id: number;
  name: string;
  is_active: number;
  created_by: string;
  last_updated: string;
  last_updated_by: string;
  txn_id: number;
}

// History rows are the same shape as the live record (see
// shared/dotnet/domain/template.generated.cs.cshtml's "OrgHistory : Org").
export type OrgHistory = Org;


// Matches EnumHelper (shared/dotnet/common/EnumHelper.cs.cshtml): the option
// list behind every FK/enum dropdown.
interface EnumOption {
  id: number;
  rwkString: string;
}


// Matches MapOption (shared/dotnet/common/MapOption.cs.cshtml): one row of a
// many-to-many relationship checklist tab -- GET /api/org/{id}/map/{other}_{role}.
interface MapOption {
  id: number;
  label: string;
  mapped: boolean;
}

// Flips one option's checked state within a Set<id> -- shared by every
// map-relationship checklist tab below (each backed by its own
// useState<Set<number>>). Entity-escaped nested generics here --
// bare nested generic brackets inside this markup excursion get parsed as
// HTML tags by RazorLight's markup tokenizer, which never finds a matching
// close and fails. Avoid writing angle brackets literally in comments in
// this region -- use words instead, or entity-escape them.
function toggleMapChecked(setChecked: Dispatch<SetStateAction<Set<number>>>, optionId: number, checked: boolean) {
  setChecked((current) => {
    const next = new Set(current);
    if (checked) next.add(optionId);
    else next.delete(optionId);
    return next;
  });
}

interface FormField {
  key: string;
  label: string;
  kind: "string" | "number" | "boolean" | "date" | "enum";
  fkVar: string;
  isGlobal: boolean;
  isId: boolean;
}

const FORM_FIELDS: FormField[] = [
  { key: "id", label: "Organization ID", kind: "number", fkVar: "", isGlobal: false, isId: true },
  { key: "name", label: "Name", kind: "string", fkVar: "", isGlobal: false, isId: false },
  { key: "is_active", label: "Active", kind: "number", fkVar: "", isGlobal: true, isId: false },
  { key: "created_by", label: "Created By", kind: "string", fkVar: "", isGlobal: true, isId: false },
  { key: "last_updated", label: "Last Updated", kind: "date", fkVar: "", isGlobal: true, isId: false },
  { key: "last_updated_by", label: "Last Updated By", kind: "string", fkVar: "", isGlobal: true, isId: false },
  { key: "txn_id", label: "Txn Id", kind: "number", fkVar: "", isGlobal: true, isId: false },
];

const OWN_COLUMNS: DataTableColumn[] = [
  { key: "id", label: "Organization ID" },
  { key: "name", label: "Name" },
  { key: "is_active", label: "Active" },
  { key: "created_by", label: "Created By" },
  { key: "last_updated", label: "Last Updated" },
  { key: "last_updated_by", label: "Last Updated By" },
];


// Read-only display text for global/audit fields (field.isGlobal below) --
// same null/blank handling as DataTable.tsx.cshtml's formatValue, plus a
// friendlier Yes/No for booleans like is_active.
function formatReadOnlyValue(value: unknown): string {
  if (value === null || value === undefined || value === "") return "";
  if (typeof value === "boolean") return value ? "Yes" : "No";
  return String(value);
}

function defaultValueForKind(kind: FormField["kind"]): string | number | boolean {
  switch (kind) {
    case "number":
    case "enum":
      return 0;
    case "boolean":
      return false;
    default:
      return "";
  }
}

function createEmptyOrg(): Org {
  // Plain index-signature object type here (rather than a generic
  // Record-of-string-to-unknown type) -- RazorLight's markup parser can
  // mistake a bare generic type argument for the start of an HTML/JSX tag
  // when it appears in markup mode, so every such generic in this file is
  // written to avoid a bare angle bracket following an identifier.
  const obj: { [key: string]: unknown } = {};
  for (const field of FORM_FIELDS) {
    obj[field.key] = defaultValueForKind(field.kind);
  }
  return obj as unknown as Org;
}

// Not written as a generic function (only ever called with FORM_FIELDS
// below) -- a bare type-parameter angle bracket here trips the same
// RazorLight tag-detection issue described above.
function chunkFormFields(items: FormField[], size: number): FormField[][] {
  const rows: FormField[][] = [];
  for (let i = 0; i < items.length; i += size) {
    rows.push(items.slice(i, i + size));
  }
  return rows;
}

export default function EditOrg() {
  const api = useApiClient();
  const navigate = useNavigate();
  const location = useLocation();
  const params = useParams();
  const id = params.id ? Number(params.id) : null;

  const returnUrl = useMemo(() => new URLSearchParams(location.search).get("returnUrl"), [location.search]);

  const [formData, setFormData] = useState<Org>(() => createEmptyOrg());
  const [historyList, setHistoryList] = useState<OrgHistory[] | null>(null);
  const [enumOptions, setEnumOptions] = useState({} as { [key: string]: EnumOption[] });
  const [activeTab, setActiveTab] = useState(0);
  const [activeChildTab, setActiveChildTab] = useState(0);
  const [principal_principal_Options, set_principal_principal_Options] = useState<MapOption[] | null>(null);
  const [principal_principal_Checked, set_principal_principal_Checked] = useState<Set<number>>(() => new Set());

  // Loads the record (plus history/children) when editing an existing row;
  // resets to a blank record when navigating to the "create" route.
  useEffect(() => {
    let cancelled = false;

    async function load() {
      if (id == null) {
        setFormData(createEmptyOrg());
        setHistoryList(null);
        return;
      }

      try {
        const record = await api.get<Org>(`/api/org/${id}`);
        if (!cancelled) setFormData(record);
      } catch (err) {
        console.error("EditOrg: error loading record", err);
      }

      try {
        const history = await api.get<OrgHistory[]>(`/api/org/${id}/history`);
        if (!cancelled) setHistoryList(history);
      } catch (err) {
        console.error("EditOrg: error loading history", err);
        if (!cancelled) setHistoryList([]);
      }

      try {
        const principal_principal_options = await api.get<MapOption[]>(
          `/api/org/${id}/map/principal_principal`,
        );
        if (!cancelled) {
          set_principal_principal_Options(principal_principal_options);
          set_principal_principal_Checked(
            new Set(principal_principal_options.filter((o) => o.mapped).map((o) => o.id)),
          );
        }
      } catch (err) {
        console.error("EditOrg: error loading principal_principal map", err);
        if (!cancelled) set_principal_principal_Options([]);
      }
    }

    load();
    return () => {
      cancelled = true;
    };
  }, [id, api]);

  // Enum/parent dropdown options -- needed for both create and edit, same as
  // Blazor's LoadEnumData() (called unconditionally in OnParametersSetAsync).
  useEffect(() => {
    let cancelled = false;

    Promise.all(
      FORM_FIELDS.filter((f) => f.kind === "enum").map(async (field) => {
        const options = (await api.get(`/api/${field.fkVar}/enum`)) as EnumOption[];
        return [field.key, options] as const;
      }),
    )
      .then((entries) => {
        if (!cancelled) setEnumOptions(Object.fromEntries(entries));
      })
      .catch((err) => console.error("EditOrg: error loading enum options", err));

    return () => {
      cancelled = true;
    };
  }, [api]);

  function setField(key: string, value: string | number | boolean) {
    setFormData((current) => ({ ...current, [key]: value }));
  }

  // Typed via the FormEventHandler variable annotation (using its default
  // type parameter) rather than an inline generic FormEvent parameter
  // annotation -- same bare-generic issue as elsewhere in this file.
  const handleSubmit: FormEventHandler = async (e) => {
    e.preventDefault();
    try {
      if (id == null) {
        await api.post<Org>("/api/org", formData);
      } else {
        await api.put<Org>(`/api/org/${id}`, formData);

        // Apply every map-relationship checklist alongside the object save --
        // only meaningful for an existing record, since a brand-new record
        // has no checklist to have edited yet (mirrors the Delete button
        // above, and the child-relationship tabs, which are likewise only
        // loaded once id is known).
        try {
          await api.put<void>(
            `/api/org/${id}/map/principal_principal`,
            Array.from(principal_principal_Checked),
          );
        } catch (err) {
          console.error("EditOrg: error saving principal_principal map", err);
        }
      }
    } catch (err) {
      console.error("EditOrg: error saving org", err);
    }
    navigate(returnUrl ?? "/org");
  };

  function handleCancel() {
    navigate(returnUrl ?? "/org");
  }

  // Soft delete -- DELETE /api/org/{id} sets is_active=0 server-side
  // (see OrgLogic.delete() / server/dotnet/api/template.api.generated.cs.cshtml's
  // Delete action), it doesn't remove the row. Only meaningful for an
  // existing record, so the button below is hidden while creating a new one.
  async function handleDelete() {
    if (id == null) return;
    // window.confirm here (rather than a custom modal) matches the plain
    // browser confirm() the Blazor Edit page uses via JS interop below --
    // keeps both clients' delete confirmation behavior identical.
    if (!window.confirm(`Delete this Organization? This cannot be undone.`)) return;
    try {
      await api.del(`/api/org/${id}`);
    } catch (err) {
      console.error("EditOrg: error deleting org", err);
      return;
    }
    navigate(returnUrl ?? "/org");
  }

  const rwkString = [formData.name]
    .filter((v) => v !== null && v !== undefined && v !== "")
    .join(" ");

  // The "id" field is dropped from the form entirely while creating a new
  // record (there's no id yet) and rendered read-only below while editing an
  // existing one -- see the isId comment on formFields above.
  const visibleFields = id == null ? FORM_FIELDS.filter((f) => !f.isId) : FORM_FIELDS;
  const fieldRows = chunkFormFields(visibleFields, 3);
  const values = formData as unknown as { [key: string]: unknown };

  const editTabContent: ReactNode = (
    <form onSubmit={handleSubmit}>
      <div className="form-group">
        <table className="table table-borderless">
          <tbody>
            {fieldRows.map((row, rowIndex) => (
              <tr key={rowIndex}>
                {row.map((field) => (
                  <td key={field.key} style={{ width: "33%", padding: "0.5rem" }}>
                    <div className="mb-2">
                      <label htmlFor={field.key} className="form-label">
                        {field.label}
                      </label>
                      {field.isGlobal || field.isId ? (
                        <div id={field.key} className="form-control-plaintext">
                          {formatReadOnlyValue(values[field.key])}
                        </div>
                      ) : field.kind === "enum" ? (
                        <select
                          id={field.key}
                          className="form-control"
                          value={String(values[field.key] ?? "")}
                          onChange={(e) => setField(field.key, Number(e.target.value) || 0)}
                        >
                          <option value="">-- Select --</option>
                          {(enumOptions[field.key] ?? []).map((opt) => (
                            <option key={opt.id} value={opt.id}>
                              {opt.rwkString}
                            </option>
                          ))}
                        </select>
                      ) : field.kind === "boolean" ? (
                        <input
                          id={field.key}
                          type="checkbox"
                          className="form-check-input"
                          checked={Boolean(values[field.key])}
                          onChange={(e) => setField(field.key, e.target.checked)}
                        />
                      ) : field.kind === "number" ? (
                        <input
                          id={field.key}
                          type="number"
                          className="form-control"
                          value={String(values[field.key] ?? 0)}
                          onChange={(e) => setField(field.key, Number(e.target.value) || 0)}
                        />
                      ) : field.kind === "date" ? (
                        <input
                          id={field.key}
                          type="date"
                          className="form-control"
                          value={String(values[field.key] ?? "").slice(0, 10)}
                          onChange={(e) => setField(field.key, e.target.value)}
                        />
                      ) : (
                        <input
                          id={field.key}
                          type="text"
                          className="form-control"
                          value={String(values[field.key] ?? "")}
                          onChange={(e) => setField(field.key, e.target.value)}
                        />
                      )}
                    </div>
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className="mb-3 d-flex justify-content-between align-items-center">
        <div>
          <button type="submit" className="btn btn-primary">
            Save
          </button>
          <button type="button" className="btn btn-secondary ms-2" onClick={handleCancel}>
            Cancel
          </button>
        </div>
        {id != null && (
          <button
            type="button"
            className="btn"
            style={{ backgroundColor: "#8b0000", color: "#fff" }}
            onClick={handleDelete}
          >
            Delete
          </button>
        )}
      </div>
    </form>
  );

  const historyTabContent: ReactNode = (
    <div className="mt-3">
      <div className="alert alert-info">History records: {historyList?.length ?? 0}</div>
      <DataTable data={historyList} columns={OWN_COLUMNS} showActions={false} showAddButton={false} />
    </div>
  );

  const editTabs: TabItem[] = [
    { title: "Edit", content: editTabContent },
    { title: "History", content: historyTabContent },
  ];


  const childTabs: TabItem[] = [

    {
      title: "Principal",
      content: (
        <div className="mt-3">
          <div className="alert alert-info">
            Principal mapped: {principal_principal_Checked.size} of{" "}
            {principal_principal_Options?.length ?? 0}
          </div>
          {(principal_principal_Options ?? []).map((option) => (
            <div className="form-check" key={option.id}>
              <input
                type="checkbox"
                className="form-check-input"
                id={`principal_principal_${option.id}`}
                checked={principal_principal_Checked.has(option.id)}
                onChange={(e) =>
                  toggleMapChecked(set_principal_principal_Checked, option.id, e.target.checked)
                }
              />
              <label className="form-check-label" htmlFor={`principal_principal_${option.id}`}>
                {option.label}
              </label>
            </div>
          ))}
        </div>
      ),
    },

  ];

  return (
    <>
      {id == null ? <h3>Create Organization</h3> : <h3>Edit Organization {rwkString}</h3>}

      <TabControl id="editTabs" tabs={editTabs} activeTab={activeTab} onTabChanged={setActiveTab} />

      <div className="mt-4">
        <h4>Related Records</h4>
        <TabControl id="childTabs" tabs={childTabs} activeTab={activeChildTab} onTabChanged={setActiveChildTab} />
      </div>
    </>
  );
}
