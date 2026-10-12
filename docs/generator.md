---
layout: default
title: Generator
nav_order: 4
---

# Generator

The Jumpstart generator reads CSV metadata, builds an in-memory model, and renders Razor templates to produce the application output.

## Pipeline

```
~/.jumpstart.json
       |
       v
 +-----+------+     +----------+     +----------+
 | CSVLoader   | --> | CoreLoader| --> |GlobalCSV |
 | (model.csv) |     |(core.csv)|     |(global.csv)|
 +-----+------+     +-----+----+     +-----+----+
       |                   |                |
       +-------------------+----------------+
                           |
                    +------v------+
                    |  MetaModel  |
                    |  .Process() |
                    +------+------+
                           |
                    +------v------+
                    | Generator   |
                    | (RazorLight)|
                    +------+------+
                           |
              +------------+------------+
              |            |            |
         model types  object types  build types
         (1 file)    (1 per table)  (1 file)
```

### Loading Sequence

0. The application **namespace** is inferred from the model filename (`myapp.csv` → `myapp`), and any `auth0` settings in `~/.jumpstart.json` are wired into the MetaModel for templates to reference
1. **CSVLoader** reads your application's metadata CSV into `MetaModel`
2. **CoreLoader** reads `templates/core/core.csv`, adding built-in system tables
3. **GlobalCSVLoader** reads `templates/core/global.csv`, adding audit columns to every table
4. **MetaModel.Process()** resolves relationships:
   - `ProcessChildren()` -- finds parent FK references and registers child collections
   - `ProcessEnumObjects()` -- finds enum FK references and synthesizes display name columns
   - `ProcessView()` -- for `*_view` tables, builds JOIN structures and synthesizes RWK columns
5. **SortMetaObjectsByReference()** -- topological sort using Tarjan's SCC algorithm to ensure parent tables generate before children

### Generation Sequence

```
GenerateApp()       -->  model-type templates (one output file each)
GenerateSchemas()   -->  schema-type templates (one per database schema)
GenerateObjects()   -->  object-type templates (one per domain object)
GenerateBuild()     -->  build-type templates (build artifacts)
```

## Meta Model

The MetaModel is the in-memory representation of your application.

### Class Hierarchy

```
MetaBaseElement          (base: Name, FileName)
  |
  +-- MetaAttribute      (column definition)
  +-- MetaObject          (table/entity definition)
  +-- MetaSchema          (database schema grouping)
  +-- MetaModel           (root: all schemas, objects, relationships)
  +-- MetaBuild           (build output tracking)
```

### MetaModel

The root container for the entire application model.

| Property | Type | Description |
|----------|------|-------------|
| `Namespace` | `string` | Application namespace (from `TABLE_CATALOG`) |
| `Schemas` | `Dictionary<string, MetaSchema>` | All schemas by name |
| `Objects` | `List<MetaObject>` | Flat list of all tables |
| `NavMenus` | `Dictionary<string, List<MetaObject>>` | Objects grouped by nav menu |
| `GlobalAttributes` | `List<MetaAttribute>` | Audit columns from global.csv |
| `build` | `MetaBuild` | Output file tracking |
| `NoAuth` | `bool` | Set from the `--noauth` CLI flag (or `"noauth": true` in `~/.jumpstart.json`). When true, templates suppress authentication code generation -- see [Getting Started](getting-started.md#turning-off-authentication---noauth). |

### MetaObject

Represents a single database table or view.

| Property | Type | Description |
|----------|------|-------------|
| `Namespace` | `string` | Parent namespace |
| `TableName` | `string` | Database table name (`customer`) |
| `DomainObj` | `string` | PascalCase class name (`Customer`) |
| `DomainVar` | `string` | Lowercase variable name (`customer`) |
| `DomainConst` | `string` | Uppercase constant (`CUSTOMER`) |
| `DomainObjView` | `string` | View class name (`CustomerView`) |
| `SchemaName` | `string` | Database schema (`app`, `core`, `sec`) |
| `Label` | `string` | Display label |
| `NavMenu` | `string` | Navigation menu group |
| `Uri` | `string` | Custom URL path (falls back to `DomainVar`) |
| `IsView` | `bool` | True if table name ends with `_view` |
| `Attributes` | `List<MetaAttribute>` | All columns |
| `UserAttributes` | `List<MetaAttribute>` | Non-global columns |
| `GlobalAttributes` | `List<MetaAttribute>` | Audit columns |
| `Children` | `List<ChildRelationship>` | Child table relationships |
| `EnumAttributes` | `List<EnumRelationship>` | Enum FK relationships |
| `ViewRelationships` | `List<ViewRelationship>` | JOIN structure for views |

### MetaAttribute

Represents a single column in a table.

| Property | Type | Description |
|----------|------|-------------|
| `Name` | `string` | Column name (`org_id`) |
| `PascalName` | `string` | PascalCase name (`OrgId`) |
| `Label` | `string` | Display label |
| `SqlDataType` | `string` | PostgreSQL type (`VARCHAR`, `BIGINT`) |
| `MSSQLDataType` | `string` | SQL Server equivalent |
| `DotNetType` | `string` | .NET type (`string`, `long`) |
| `InputType` | `string` | UI input type (`Text`, `Number`, `Date`) |
| `ConvertMethod` | `string` | Conversion method (`ToString`, `ToInt64`) |
| `Length` | `string` | Character length |
| `RWK` | `string` | Real World Key flag (`0` or `1`) |
| `IsNullable` | `bool` | Whether the column allows nulls |
| `FkType` | `string` | FK type: `enum`, `parent`, `map`, or empty |
| `FkTable` | `string` | Referenced table name |
| `FkObject` | `string` | Referenced PascalCase class name |
| `TestDataSet` | `string` | Test data seed name |

## Templates

### Template Types

| Type | Model Class | Execution | Description |
|------|------------|-----------|-------------|
| `model` | `MetaModel` | Once per application | Shared infrastructure, config files, project files |
| `schema` | `MetaSchema` | Once per schema | Schema-level DDL |
| `object` | `MetaObject` | Once per table/entity | Domain classes, controllers, logic, tests |
| `build` | `MetaBuild` | Once at end | Build scripts, solution files |

### Template Definition CSV

Template definitions are CSV files that declare which templates to run and where to write output:

```csv
COMMENT,TEMPLATE_TYPE,TEMPLATE_PATH,OUTPUT_DIR,FORCE
rust server templates-domain,object,shared/rust/domain/template.generated.rs.cshtml,./gen/shared/domain,TRUE
rust server templates-domain,object,shared/rust/domain/template.user.rs.cshtml,./usr/shared/domain,FALSE
```

| Column | Description |
|--------|-------------|
| `COMMENT` | Descriptive label (ignored by generator) |
| `TEMPLATE_TYPE` | `model`, `schema`, `object`, or `build` |
| `TEMPLATE_PATH` | Path to `.cshtml` template, relative to `templates/` |
| `OUTPUT_DIR` | Destination directory for generated files |
| `FORCE` | `TRUE` = always overwrite; `FALSE` = create only if missing |

### Filename Convention

Output filenames are derived from template filenames:

```
targetFile = templateFile.Replace("template", model.FileName).Replace(".cshtml", "")
```

Examples:

| Template File | Model | Output File |
|---------------|-------|-------------|
| `template.generated.cs.cshtml` | `Customer` | `Customer.generated.cs` |
| `templateLogic.generated.cs.cshtml` | `Order` | `OrderLogic.generated.cs` |
| `template.api.generated.cs.cshtml` | `Product` | `Product.api.generated.cs` |
| `Config.core.cs.cshtml` | (model) | `Config.core.cs` |
| `ListWorkflowCore.razor.cshtml` | (model) | `ListWorkflowCore.razor` |

Note: Files without "template" in their name keep their original name (minus `.cshtml`).

### FORCE Flag and the gen/ vs usr/ Split

The FORCE flag controls overwrite behavior, and by convention it pairs with the output directory:

- **FORCE=TRUE** -- File is always regenerated. These templates write under **`./gen/`** and must never be hand-edited (e.g., `*.generated.cs`, `*.generated.rs`, `*.core.*`).
- **FORCE=FALSE** -- File is only created if it doesn't exist. These templates write under **`./usr/`** (or `./bin` for launchers) and are yours to edit (e.g., `*.user.cs`, `*.user.rs`, `appsettings.json`, `user_api.rs`).

This enables the **generated + user** extension convention in both backends:
- `Customer.generated.cs` / `Customer.generated.rs` (FORCE=TRUE, in `gen/`) -- regenerated every time
- `Customer.user.cs` / `Customer.user.rs` (FORCE=FALSE, in `usr/`) -- created once, then hand-editable

In C# the two halves are `partial class`es; in Rust the crate root `include!`s both files into one module. Corollary: anything *generated* code calls must be defined in *generated* code, because the generator cannot retroactively update an existing `usr/` file.

### Template Syntax

Templates use RazorLight, a Razor-based templating engine. Key syntax:

| Syntax | Meaning |
|--------|---------|
| `@Model.PropertyName` | Access model property |
| `@(Model.DomainObj)` | Parenthesized expression (avoids ambiguity) |
| `@@` | Escaped `@` in output (literal `@` sign) |
| `@foreach (var x in collection) { }` | Loop |
| `@if (condition) { }` | Conditional |
| `@{ var x = value; }` | Code block |
| `<text>...</text>` | Literal text output inside code blocks |

**Post-processing escapes** (handled by the generator after rendering):

| Template Syntax | Output |
|----------------|--------|
| `&at;` | `@` (for Blazor `@` directives) |
| `&lt;` | `<` |
| `&gt;` | `>` |

### Template Registries

There are ten template registries. Server, web, test, and tools registries come in matched pairs -- one per language target -- that produce structurally equivalent output.

| Registry | Target | Contents |
|----------|--------|----------|
| `database-pgsql.csv` / `database-mssql.csv` | PostgreSQL / SQL Server | DDL (schema, table, sequence, rwkindex, views), seed data, nav menus, named queries, build/load scripts |
| `server-dotnet.csv` | .NET 9 | common, domain, persist, logic, API, scheduler, script agent |
| `server-rust.csv` | Rust | the same layers as Cargo crates, plus the `script` crate (Rhai) |
| `web-blazor.csv` | Blazor WASM | pages, components, layout, auth, assets |
| `web-nodejs.csv` | React + Vite | pages, components, layout, auth, API client, assets |
| `test-dotnet.csv` / `test-rust.csv` | tests | test-persist, test-script, test-scheduler, test-scriptagent (+ test-api on .NET) |
| `tools-dotnet.csv` / `tools-rust.csv` | tools | CSV import and export utilities (+ test-impexp and setup-auth0 on .NET) |
| `root.csv` | project root | The top-level `makefile` that drives `jumpstart` itself across whichever registries you selected |

#### server-dotnet.csv / server-rust.csv (Server Templates)

Each server registry defines the full backend across these layers:

| Layer | .NET Templates | Rust Templates | Type |
|-------|----------------|----------------|------|
| **Common** | Config, BaseObject, Logger, DB providers, ScriptHost | config, base_object, logger, db providers, auth0 | model |
| **Domain** | `template.generated.cs` / `template.user.cs` | `template.generated.rs` / `template.user.rs` | object |
| **Domain Core** | Workflow, Execution, Script, ServerNode enums | same, as Rust modules | model |
| **Persist** | DBPersist + audit/basic strategies | db_persist + audit/basic/import strategies | model |
| **Logic** | `templateLogic.generated.cs` / `.user.cs` | `templateLogic.generated.rs` / `.user.rs` | object |
| **Logic Core** | BaseLogic, Proxy (DispatchProxy AOP) | logic_exec, proxy, dispatch (see [Rust reference](rust/logic-dispatch.md)) | model |
| **Script** | CSharpCompiler, PowerShell/Python providers (in common) | `script` crate: RhaiProvider, ScriptHost, logic_bridge | model |
| **API** | per-object controllers + Program.cs | generic router `main.generated.rs` + `user_api.rs` hook | object/model |
| **Scheduler** | Program.cs, QuartzSchedulerThread | main.generated.rs (cron crate) | model |
| **Script agent** | Program.cs, scriptagent.api.core.cs | main.generated.rs | model |
| **Bin launchers** | `start.sh`/`.cmd`, `stop.sh`/`.cmd` (start/stop api+scheduler+scriptagent+web together) | same, identical across both backends | model |

#### web-blazor.csv / web-nodejs.csv (Web Templates)

| Layer | Blazor | React | Type |
|-------|--------|-------|------|
| **Pages** | `Listtemplate.razor`, `Edittemplate.razor` | `Listtemplate.tsx`, `Edittemplate.tsx` | object |
| **Pages Core** | ListWorkflowCore.razor | ListWorkflowCore.tsx | model |
| **Components** | DataTable, DataTableContextMenu, TabControl | same, as .tsx + .css | model |
| **Layout** | MainLayout, NavMenuLayout | MainLayout, NavMenu | model |
| **Auth** | Authentication.razor, LoginDisplay, RedirectToLogin | ProtectedRoute, LoginDisplay, AuthCallback (auth0-react) | model |
| **Root** | App.razor, _Imports.razor, Program.cs | main.tsx, App.tsx, apiClient, vite.config | model |
| **Assets** | index.html, CSS, Bootstrap | index.html, public/, config.json | model |

#### database-pgsql.csv / database-mssql.csv (Database Templates)

| Layer | Key Templates | Type |
|-------|--------------|------|
| **DDL** | database.create, schema, table, sequence, rwkindex, views | model/schema/object |
| **Data** | static data, nav_menu, current user, named queries, children queries | model/object |
| **Build** | build.sh / build.py / build.ps1, load.sh / load.py | build |

## Relationship Processing

### ProcessChildren

For each MetaObject, scans all attributes for `FK_TYPE="parent"`:

```
exec_log.workflow_id (FK_TYPE=parent, FK_TABLE=workflow)
  --> workflow.Children.Add(new ChildRelationship("workflow", "Execution Log", exec_log))
```

This enables the API's `/api/workflow/children/{id}?child=execlog` endpoint.

### ProcessEnumObjects

For each `FK_TYPE="enum"` attribute, finds the referenced table's RWK column and synthesizes a name column:

```
workflow.workflow_type_id (FK_TYPE=enum, FK_TABLE=workflow_type)
  --> workflow_type has RWK column "name"
  --> Synthesizes "workflow_type_name" on WorkflowView
```

### ProcessView

For `*_view` tables, recursively follows FK chains to build JOIN structures:

```
customer_view has FK to customer
  customer has FK org_id -> org
    org has RWK column "name"
  --> Synthesizes customer_view.org_name
  --> Generates LEFT OUTER JOIN org ON customer.org_id = org.id
```

## Topological Sort

The generator uses **Tarjan's strongly connected components** algorithm to sort MetaObjects by dependency order. This ensures parent tables are generated before their children, which matters for:

- Database DDL (CREATE TABLE order)
- Static data loading (parent records inserted before child records)
- Test data that respects referential integrity
