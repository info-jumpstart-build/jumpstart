// The Node/React analogue of Blazor's App.razor.cshtml: top-level router.
// Blazor discovers pages by scanning the compiled assembly for @page
// directives at runtime; React Router has no equivalent discovery
// mechanism, so this model-type template (it runs once with the full
// MetaModel, same as MainLayout/NavMenu) loops over every domain object
// and wires up its generated list/edit pages' imports + <Route>s explicitly.
import { Routes, Route } from "react-router-dom";
import MainLayout from "./layout/MainLayout";
import ProtectedRoute from "./auth/ProtectedRoute";
import AuthCallback from "./auth/AuthCallback";
import Home from "./pages/Home";
// Custom list page for the "workflow" core table -- linked from the nav
// menu via its own MetaObject.Uri ("core/workflow") instead of the default
// "/{domainVar}" every other object gets (see
// src/pages/ListWorkflowCore.tsx.cshtml). Registered as a static route
// below rather than through the per-object loop, since it isn't one.
import ListWorkflowCore from "./pages/ListWorkflowCore";
import ListTestResultStatus from "./pages/ListTestResultStatus";
import EditTestResultStatus from "./pages/EditTestResultStatus";
import ListTestPlan from "./pages/ListTestPlan";
import EditTestPlan from "./pages/EditTestPlan";
import ListOrg from "./pages/ListOrg";
import EditOrg from "./pages/EditOrg";
import ListOperation from "./pages/ListOperation";
import EditOperation from "./pages/EditOperation";
import ListOpRole from "./pages/ListOpRole";
import EditOpRole from "./pages/EditOpRole";
import ListCronEvery from "./pages/ListCronEvery";
import EditCronEvery from "./pages/EditCronEvery";
import ListCronMinute from "./pages/ListCronMinute";
import EditCronMinute from "./pages/EditCronMinute";
import ListCronHour from "./pages/ListCronHour";
import EditCronHour from "./pages/EditCronHour";
import ListCronDom from "./pages/ListCronDom";
import EditCronDom from "./pages/EditCronDom";
import ListCronMonth from "./pages/ListCronMonth";
import EditCronMonth from "./pages/EditCronMonth";
import ListCronDow from "./pages/ListCronDow";
import EditCronDow from "./pages/EditCronDow";
import ListNavMenu from "./pages/ListNavMenu";
import EditNavMenu from "./pages/EditNavMenu";
import ListDataSource from "./pages/ListDataSource";
import EditDataSource from "./pages/EditDataSource";
import ListAgentStatus from "./pages/ListAgentStatus";
import EditAgentStatus from "./pages/EditAgentStatus";
import ListOnFailure from "./pages/ListOnFailure";
import EditOnFailure from "./pages/EditOnFailure";
import ListExecStatus from "./pages/ListExecStatus";
import EditExecStatus from "./pages/EditExecStatus";
import ListServerNodeStatus from "./pages/ListServerNodeStatus";
import EditServerNodeStatus from "./pages/EditServerNodeStatus";
import ListScriptType from "./pages/ListScriptType";
import EditScriptType from "./pages/EditScriptType";
import ListServerNodeType from "./pages/ListServerNodeType";
import EditServerNodeType from "./pages/EditServerNodeType";
import ListWorkflowType from "./pages/ListWorkflowType";
import EditWorkflowType from "./pages/EditWorkflowType";
import ListPrincipalStatus from "./pages/ListPrincipalStatus";
import EditPrincipalStatus from "./pages/EditPrincipalStatus";
import ListTestCase from "./pages/ListTestCase";
import EditTestCase from "./pages/EditTestCase";
import ListTestRun from "./pages/ListTestRun";
import EditTestRun from "./pages/EditTestRun";
import ListOpRoleMap from "./pages/ListOpRoleMap";
import EditOpRoleMap from "./pages/EditOpRoleMap";
import ListSchedule from "./pages/ListSchedule";
import EditSchedule from "./pages/EditSchedule";
import ListSql from "./pages/ListSql";
import EditSql from "./pages/EditSql";
import ListScript from "./pages/ListScript";
import EditScript from "./pages/EditScript";
import ListServerNode from "./pages/ListServerNode";
import EditServerNode from "./pages/EditServerNode";
import ListPrincipal from "./pages/ListPrincipal";
import EditPrincipal from "./pages/EditPrincipal";
import ListEventService from "./pages/ListEventService";
import EditEventService from "./pages/EditEventService";
import ListProcess from "./pages/ListProcess";
import EditProcess from "./pages/EditProcess";
import ListTestResult from "./pages/ListTestResult";
import EditTestResult from "./pages/EditTestResult";
import ListPrincipalOrg from "./pages/ListPrincipalOrg";
import EditPrincipalOrg from "./pages/EditPrincipalOrg";
import ListOpRoleMember from "./pages/ListOpRoleMember";
import EditOpRoleMember from "./pages/EditOpRoleMember";
import ListWorkflow from "./pages/ListWorkflow";
import EditWorkflow from "./pages/EditWorkflow";
import ListExecLog from "./pages/ListExecLog";
import EditExecLog from "./pages/EditExecLog";

export default function App() {
  return (
    <Routes>
      <Route path="/authentication/callback" element={<AuthCallback />} />
      <Route
        path="/*"
        element={
          <ProtectedRoute>
            <MainLayout>
              <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/core/workflow" element={<ListWorkflowCore />} />
          
                <Route path="/testresultstatus" element={<ListTestResultStatus />} />
                <Route path="/edit-testresultstatus" element={<EditTestResultStatus />} />
                <Route path="/edit-testresultstatus/:id" element={<EditTestResultStatus />} />
          
                <Route path="/testplan" element={<ListTestPlan />} />
                <Route path="/edit-testplan" element={<EditTestPlan />} />
                <Route path="/edit-testplan/:id" element={<EditTestPlan />} />
          
                <Route path="/org" element={<ListOrg />} />
                <Route path="/edit-org" element={<EditOrg />} />
                <Route path="/edit-org/:id" element={<EditOrg />} />
          
                <Route path="/operation" element={<ListOperation />} />
                <Route path="/edit-operation" element={<EditOperation />} />
                <Route path="/edit-operation/:id" element={<EditOperation />} />
          
                <Route path="/oprole" element={<ListOpRole />} />
                <Route path="/edit-oprole" element={<EditOpRole />} />
                <Route path="/edit-oprole/:id" element={<EditOpRole />} />
          
                <Route path="/cronevery" element={<ListCronEvery />} />
                <Route path="/edit-cronevery" element={<EditCronEvery />} />
                <Route path="/edit-cronevery/:id" element={<EditCronEvery />} />
          
                <Route path="/cronminute" element={<ListCronMinute />} />
                <Route path="/edit-cronminute" element={<EditCronMinute />} />
                <Route path="/edit-cronminute/:id" element={<EditCronMinute />} />
          
                <Route path="/cronhour" element={<ListCronHour />} />
                <Route path="/edit-cronhour" element={<EditCronHour />} />
                <Route path="/edit-cronhour/:id" element={<EditCronHour />} />
          
                <Route path="/crondom" element={<ListCronDom />} />
                <Route path="/edit-crondom" element={<EditCronDom />} />
                <Route path="/edit-crondom/:id" element={<EditCronDom />} />
          
                <Route path="/cronmonth" element={<ListCronMonth />} />
                <Route path="/edit-cronmonth" element={<EditCronMonth />} />
                <Route path="/edit-cronmonth/:id" element={<EditCronMonth />} />
          
                <Route path="/crondow" element={<ListCronDow />} />
                <Route path="/edit-crondow" element={<EditCronDow />} />
                <Route path="/edit-crondow/:id" element={<EditCronDow />} />
          
                <Route path="/navmenu" element={<ListNavMenu />} />
                <Route path="/edit-navmenu" element={<EditNavMenu />} />
                <Route path="/edit-navmenu/:id" element={<EditNavMenu />} />
          
                <Route path="/datasource" element={<ListDataSource />} />
                <Route path="/edit-datasource" element={<EditDataSource />} />
                <Route path="/edit-datasource/:id" element={<EditDataSource />} />
          
                <Route path="/agentstatus" element={<ListAgentStatus />} />
                <Route path="/edit-agentstatus" element={<EditAgentStatus />} />
                <Route path="/edit-agentstatus/:id" element={<EditAgentStatus />} />
          
                <Route path="/onfailure" element={<ListOnFailure />} />
                <Route path="/edit-onfailure" element={<EditOnFailure />} />
                <Route path="/edit-onfailure/:id" element={<EditOnFailure />} />
          
                <Route path="/execstatus" element={<ListExecStatus />} />
                <Route path="/edit-execstatus" element={<EditExecStatus />} />
                <Route path="/edit-execstatus/:id" element={<EditExecStatus />} />
          
                <Route path="/servernodestatus" element={<ListServerNodeStatus />} />
                <Route path="/edit-servernodestatus" element={<EditServerNodeStatus />} />
                <Route path="/edit-servernodestatus/:id" element={<EditServerNodeStatus />} />
          
                <Route path="/scripttype" element={<ListScriptType />} />
                <Route path="/edit-scripttype" element={<EditScriptType />} />
                <Route path="/edit-scripttype/:id" element={<EditScriptType />} />
          
                <Route path="/servernodetype" element={<ListServerNodeType />} />
                <Route path="/edit-servernodetype" element={<EditServerNodeType />} />
                <Route path="/edit-servernodetype/:id" element={<EditServerNodeType />} />
          
                <Route path="/workflowtype" element={<ListWorkflowType />} />
                <Route path="/edit-workflowtype" element={<EditWorkflowType />} />
                <Route path="/edit-workflowtype/:id" element={<EditWorkflowType />} />
          
                <Route path="/principalstatus" element={<ListPrincipalStatus />} />
                <Route path="/edit-principalstatus" element={<EditPrincipalStatus />} />
                <Route path="/edit-principalstatus/:id" element={<EditPrincipalStatus />} />
          
                <Route path="/testcase" element={<ListTestCase />} />
                <Route path="/edit-testcase" element={<EditTestCase />} />
                <Route path="/edit-testcase/:id" element={<EditTestCase />} />
          
                <Route path="/testrun" element={<ListTestRun />} />
                <Route path="/edit-testrun" element={<EditTestRun />} />
                <Route path="/edit-testrun/:id" element={<EditTestRun />} />
          
                <Route path="/oprolemap" element={<ListOpRoleMap />} />
                <Route path="/edit-oprolemap" element={<EditOpRoleMap />} />
                <Route path="/edit-oprolemap/:id" element={<EditOpRoleMap />} />
          
                <Route path="/schedule" element={<ListSchedule />} />
                <Route path="/edit-schedule" element={<EditSchedule />} />
                <Route path="/edit-schedule/:id" element={<EditSchedule />} />
          
                <Route path="/sql" element={<ListSql />} />
                <Route path="/edit-sql" element={<EditSql />} />
                <Route path="/edit-sql/:id" element={<EditSql />} />
          
                <Route path="/script" element={<ListScript />} />
                <Route path="/edit-script" element={<EditScript />} />
                <Route path="/edit-script/:id" element={<EditScript />} />
          
                <Route path="/servernode" element={<ListServerNode />} />
                <Route path="/edit-servernode" element={<EditServerNode />} />
                <Route path="/edit-servernode/:id" element={<EditServerNode />} />
          
                <Route path="/principal" element={<ListPrincipal />} />
                <Route path="/edit-principal" element={<EditPrincipal />} />
                <Route path="/edit-principal/:id" element={<EditPrincipal />} />
          
                <Route path="/eventservice" element={<ListEventService />} />
                <Route path="/edit-eventservice" element={<EditEventService />} />
                <Route path="/edit-eventservice/:id" element={<EditEventService />} />
          
                <Route path="/process" element={<ListProcess />} />
                <Route path="/edit-process" element={<EditProcess />} />
                <Route path="/edit-process/:id" element={<EditProcess />} />
          
                <Route path="/testresult" element={<ListTestResult />} />
                <Route path="/edit-testresult" element={<EditTestResult />} />
                <Route path="/edit-testresult/:id" element={<EditTestResult />} />
          
                <Route path="/principalorg" element={<ListPrincipalOrg />} />
                <Route path="/edit-principalorg" element={<EditPrincipalOrg />} />
                <Route path="/edit-principalorg/:id" element={<EditPrincipalOrg />} />
          
                <Route path="/oprolemember" element={<ListOpRoleMember />} />
                <Route path="/edit-oprolemember" element={<EditOpRoleMember />} />
                <Route path="/edit-oprolemember/:id" element={<EditOpRoleMember />} />
          
                <Route path="/workflow" element={<ListWorkflow />} />
                <Route path="/edit-workflow" element={<EditWorkflow />} />
                <Route path="/edit-workflow/:id" element={<EditWorkflow />} />
          
                <Route path="/execlog" element={<ListExecLog />} />
                <Route path="/edit-execlog" element={<EditExecLog />} />
                <Route path="/edit-execlog/:id" element={<EditExecLog />} />
                <Route path="*" element={<p role="alert">Sorry, there's nothing at this address.</p>} />
              </Routes>
            </MainLayout>
          </ProtectedRoute>
        }
      />
    </Routes>
  );
}
