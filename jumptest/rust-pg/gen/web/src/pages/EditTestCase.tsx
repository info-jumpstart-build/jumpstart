import { useEffect, useMemo, useState, type FormEventHandler, type ReactNode } from "react";
import { useNavigate, useParams, useLocation } from "react-router-dom";
import { useApiClient } from "../api/apiClient";
import DataTable, { type DataTableColumn } from "../components/DataTable";
import TabControl, { type TabItem } from "../components/TabControl";
import "../styles/editForm.css";

// Raw shape sent/received by GET|POST|PUT /api/testcase[/{id}] -- matches
// the TestCase domain class generated for the dotnet/rust servers (every
// attribute, unlike the *View interface the list page works with).
export interface TestCase {
  id: number;
  test_plan_id: number;
  code: string;
  area: string;
  title: string;
  preconditions: string;
  steps: string;
  expected_result: string;
  priority: string;
  component: string;
  is_active: number;
  created_by: string;
  last_updated: string;
  last_updated_by: string;
  txn_id: number;
}

// History rows are the same shape as the live record (see
// shared/dotnet/domain/template.generated.cs.cshtml's "TestCaseHistory : TestCase").
export type TestCaseHistory = TestCase;


// Raw shape returned by GET /api/testcase/{id}/{child}_{role} for the
// "TestResult" child relationship(s) below.
interface TestResultRow {
  id: number;
  test_run_id: number;
  test_run_name: string;
  test_run_test_plan_id: number;
  test_case_id: number;
  test_result_status_id: number;
  test_result_status_name: string;
  executed_at: string;
  executed_by_id: number;
  executed_by_email: string;
  executed_by_principal_status_id: number;
  actual_result: string;
  notes: string;
  is_active: number;
  created_by: string;
  last_updated: string;
  last_updated_by: string;
  txn_id: number;
}

// Matches EnumHelper (shared/dotnet/common/EnumHelper.cs.cshtml): the option
// list behind every FK/enum dropdown.
interface EnumOption {
  id: number;
  rwkString: string;
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
  { key: "id", label: "Test Case ID", kind: "number", fkVar: "", isGlobal: false, isId: true },
  { key: "test_plan_id", label: "Test Plan", kind: "enum", fkVar: "testplan", isGlobal: false, isId: false },
  { key: "code", label: "Code", kind: "string", fkVar: "", isGlobal: false, isId: false },
  { key: "area", label: "Area", kind: "string", fkVar: "", isGlobal: false, isId: false },
  { key: "title", label: "Title", kind: "string", fkVar: "", isGlobal: false, isId: false },
  { key: "preconditions", label: "Preconditions", kind: "string", fkVar: "", isGlobal: false, isId: false },
  { key: "steps", label: "Steps", kind: "string", fkVar: "", isGlobal: false, isId: false },
  { key: "expected_result", label: "Expected Result", kind: "string", fkVar: "", isGlobal: false, isId: false },
  { key: "priority", label: "Priority", kind: "string", fkVar: "", isGlobal: false, isId: false },
  { key: "component", label: "Component", kind: "string", fkVar: "", isGlobal: false, isId: false },
  { key: "is_active", label: "Active", kind: "number", fkVar: "", isGlobal: true, isId: false },
  { key: "created_by", label: "Created By", kind: "string", fkVar: "", isGlobal: true, isId: false },
  { key: "last_updated", label: "Last Updated", kind: "date", fkVar: "", isGlobal: true, isId: false },
  { key: "last_updated_by", label: "Last Updated By", kind: "string", fkVar: "", isGlobal: true, isId: false },
  { key: "txn_id", label: "Txn Id", kind: "number", fkVar: "", isGlobal: true, isId: false },
];

const OWN_COLUMNS: DataTableColumn[] = [
  { key: "id", label: "Test Case ID" },
  { key: "code", label: "Code" },
  { key: "area", label: "Area" },
  { key: "title", label: "Title" },
  { key: "preconditions", label: "Preconditions" },
  { key: "steps", label: "Steps" },
  { key: "expected_result", label: "Expected Result" },
  { key: "priority", label: "Priority" },
  { key: "component", label: "Component" },
  { key: "is_active", label: "Active" },
  { key: "created_by", label: "Created By" },
  { key: "last_updated", label: "Last Updated" },
  { key: "last_updated_by", label: "Last Updated By" },
];


const TESTRESULT_COLUMNS: DataTableColumn[] = [
  { key: "id", label: "Test Result ID" },
  { key: "test_run_name", label: "Test Run" },
  { key: "test_run_test_plan_id", label: "Test Run Test Plan" },
  { key: "test_result_status_name", label: "Status" },
  { key: "executed_at", label: "Executed At" },
  { key: "executed_by_email", label: "Executed By Email" },
  { key: "executed_by_principal_status_id", label: "Executed By Status" },
  { key: "actual_result", label: "Actual Result" },
  { key: "notes", label: "Notes" },
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

function createEmptyTestCase(): TestCase {
  // Plain index-signature object type here (rather than a generic
  // Record-of-string-to-unknown type) -- RazorLight's markup parser can
  // mistake a bare generic type argument for the start of an HTML/JSX tag
  // when it appears in markup mode, so every such generic in this file is
  // written to avoid a bare angle bracket following an identifier.
  const obj: { [key: string]: unknown } = {};
  for (const field of FORM_FIELDS) {
    obj[field.key] = defaultValueForKind(field.kind);
  }
  return obj as unknown as TestCase;
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

export default function EditTestCase() {
  const api = useApiClient();
  const navigate = useNavigate();
  const location = useLocation();
  const params = useParams();
  const id = params.id ? Number(params.id) : null;

  const returnUrl = useMemo(() => new URLSearchParams(location.search).get("returnUrl"), [location.search]);

  const [formData, setFormData] = useState<TestCase>(() => createEmptyTestCase());
  const [historyList, setHistoryList] = useState<TestCaseHistory[] | null>(null);
  const [enumOptions, setEnumOptions] = useState({} as { [key: string]: EnumOption[] });
  const [activeTab, setActiveTab] = useState(0);
  const [activeChildTab, setActiveChildTab] = useState(0);
  const [testresult_test_caseList, set_testresult_test_caseList] = useState<TestResultRow[] | null>(null);

  // Loads the record (plus history/children) when editing an existing row;
  // resets to a blank record when navigating to the "create" route.
  useEffect(() => {
    let cancelled = false;

    async function load() {
      if (id == null) {
        setFormData(createEmptyTestCase());
        setHistoryList(null);
        return;
      }

      try {
        const record = await api.get<TestCase>(`/api/testcase/${id}`);
        if (!cancelled) setFormData(record);
      } catch (err) {
        console.error("EditTestCase: error loading record", err);
      }

      try {
        const history = await api.get<TestCaseHistory[]>(`/api/testcase/${id}/history`);
        if (!cancelled) setHistoryList(history);
      } catch (err) {
        console.error("EditTestCase: error loading history", err);
        if (!cancelled) setHistoryList([]);
      }

      try {
        const testresult_test_case = await api.get<TestResultRow[]>(
          `/api/testcase/${id}/testresult_test_case`,
        );
        if (!cancelled) set_testresult_test_caseList(testresult_test_case);
      } catch (err) {
        console.error("EditTestCase: error loading testresult_test_case", err);
        if (!cancelled) set_testresult_test_caseList([]);
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
      .catch((err) => console.error("EditTestCase: error loading enum options", err));

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
        await api.post<TestCase>("/api/testcase", formData);
      } else {
        await api.put<TestCase>(`/api/testcase/${id}`, formData);

        // Apply every map-relationship checklist alongside the object save --
        // only meaningful for an existing record, since a brand-new record
        // has no checklist to have edited yet (mirrors the Delete button
        // above, and the child-relationship tabs, which are likewise only
        // loaded once id is known).
      }
    } catch (err) {
      console.error("EditTestCase: error saving testcase", err);
    }
    navigate(returnUrl ?? "/testcase");
  };

  function handleCancel() {
    navigate(returnUrl ?? "/testcase");
  }

  // Soft delete -- DELETE /api/testcase/{id} sets is_active=0 server-side
  // (see TestCaseLogic.delete() / server/dotnet/api/template.api.generated.cs.cshtml's
  // Delete action), it doesn't remove the row. Only meaningful for an
  // existing record, so the button below is hidden while creating a new one.
  async function handleDelete() {
    if (id == null) return;
    // window.confirm here (rather than a custom modal) matches the plain
    // browser confirm() the Blazor Edit page uses via JS interop below --
    // keeps both clients' delete confirmation behavior identical.
    if (!window.confirm(`Delete this Test Case? This cannot be undone.`)) return;
    try {
      await api.del(`/api/testcase/${id}`);
    } catch (err) {
      console.error("EditTestCase: error deleting testcase", err);
      return;
    }
    navigate(returnUrl ?? "/testcase");
  }

  const rwkString = [formData.code, formData.title]
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
      title: "Test Result",
      content: (
        <div className="mt-3">
          <div className="alert alert-info">
            Test Case records: {testresult_test_caseList?.length ?? 0}
          </div>
          <DataTable
            data={testresult_test_caseList}
            columns={TESTRESULT_COLUMNS}
            showActions={false}
            showAddButton={false}
            onEdit={(item) => {
              const childReturnUrl = encodeURIComponent(location.pathname);
              navigate(`/edit-testresult/${item.id}?returnUrl=${childReturnUrl}`);
            }}
          />
        </div>
      ),
    },

  ];

  return (
    <>
      {id == null ? <h3>Create Test Case</h3> : <h3>Edit Test Case {rwkString}</h3>}

      <TabControl id="editTabs" tabs={editTabs} activeTab={activeTab} onTabChanged={setActiveTab} />

      <div className="mt-4">
        <h4>Related Records</h4>
        <TabControl id="childTabs" tabs={childTabs} activeTab={activeChildTab} onTabChanged={setActiveChildTab} />
      </div>
    </>
  );
}
