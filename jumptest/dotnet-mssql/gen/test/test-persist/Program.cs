using System;
using System.Collections.Generic;



namespace jumptest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                List<OpRole> opRoles = OpRoleLogic.Create().select();
                var adminRole = opRoles.Find(role => role.name == "Administrator");

                BaseTest.addLastId("op_role", adminRole.id);

                List<Principal> principals = PrincipalLogic.Create().select();
var currentPrincipal = principals.Find(principal => principal.username == Environment.UserName);

                if (currentPrincipal == null)
                {
                    PrincipalTest.testInsert();  
                    long principalId = BaseTest.getLastId("Principal");
                    OpRoleMemberTest.testInsert();
                }
                else
                {
                    BaseTest.addLastId("principal", currentPrincipal.id);

                    List<OpRoleMember> opRoleMembers = OpRoleMemberLogic.Create().select();
                    var adminRoleMembership = opRoleMembers.Find(membership => membership.principal_id == currentPrincipal.id && membership.op_role_id == adminRole.id);

                    if (adminRoleMembership == null)
                    {
                        OpRoleMemberTest.testInsert();
                    }
                }
                


                Logger.Info("Testing TestResultStatus");
                TestResultStatusTest.testInsert();            
                TestResultStatusTest.testUpdate(); 
                TestResultStatusTest.testInsert();            
                TestResultStatusTest.testUpdate();            
                
                Logger.Info("Testing TestPlan");
                TestPlanTest.testInsert();            
                TestPlanTest.testUpdate(); 
                TestPlanTest.testInsert();            
                TestPlanTest.testUpdate();            
                
                Logger.Info("Testing Org");
                OrgTest.testInsert();            
                OrgTest.testUpdate(); 
                OrgTest.testInsert();            
                OrgTest.testUpdate();            
                
                Logger.Info("Testing TestCase");
                TestCaseTest.testInsert();            
                TestCaseTest.testUpdate(); 
                TestCaseTest.testInsert();            
                TestCaseTest.testUpdate();            
                
                Logger.Info("Testing TestRun");
                TestRunTest.testInsert();            
                TestRunTest.testUpdate(); 
                TestRunTest.testInsert();            
                TestRunTest.testUpdate();            
                
<<<<<<< HEAD
                Logger.Info("Testing PrincipalOrg");
                PrincipalOrgTest.testInsert();            
                PrincipalOrgTest.testUpdate(); 
                PrincipalOrgTest.testInsert();            
                PrincipalOrgTest.testUpdate();            
                
=======
>>>>>>> main
                Logger.Info("Testing TestResult");
                TestResultTest.testInsert();            
                TestResultTest.testUpdate(); 
                TestResultTest.testInsert();            
                TestResultTest.testUpdate();            
<<<<<<< HEAD
=======
                
                Logger.Info("Testing PrincipalOrg");
                PrincipalOrgTest.testInsert();            
                PrincipalOrgTest.testUpdate(); 
                PrincipalOrgTest.testInsert();            
                PrincipalOrgTest.testUpdate();            
>>>>>>> main
                            }
            catch( Exception x)
            {
                Logger.Error("Error executing test: ", x);
                Console.WriteLine(x.Message);
                Console.WriteLine(x.StackTrace);

                if (x.InnerException != null)
                {
                    x = x.InnerException;
                    Console.WriteLine(x.Message);
                    Console.WriteLine(x.StackTrace);
                }	
            }
		}
    }
}
