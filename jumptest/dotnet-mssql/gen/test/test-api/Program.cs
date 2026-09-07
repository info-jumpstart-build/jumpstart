using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jumptest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {

                // Get admin role
                var opRoles = await BaseTest.GetListAsync<OpRole>("OpRole");
                var adminRole = opRoles.Find(role => role.name == "Administrator");
                BaseTest.addLastId("op_role", adminRole.id);

                // Get current principal
                var principals = await BaseTest.GetListAsync<Principal>("Principal");
                var currentPrincipal = principals.Find(principal => principal.username == Environment.UserName);

                if (currentPrincipal == null)
                {
                    await PrincipalTest.testInsert();
                    long principalId = BaseTest.getLastId("Principal");
                    await OpRoleMemberTest.testInsert();
                }
                else
                {
                    BaseTest.addLastId("principal", currentPrincipal.id);

                    var opRoleMembers = await BaseTest.GetListAsync<OpRoleMember>("OpRoleMember");
                    var adminRoleMembership = opRoleMembers.Find(membership => membership.principal_id == currentPrincipal.id && membership.op_role_id == adminRole.id);

                    if (adminRoleMembership == null)
                    {
                        await OpRoleMemberTest.testInsert();
                    }
                }


                Logger.Info("Testing TestResultStatus");
                await TestResultStatusTest.testInsert();            
                await TestResultStatusTest.testUpdate();    
                 await TestResultStatusTest.testInsert();            
                await TestResultStatusTest.testUpdate();     
                await TestResultStatusTest.testSelect(); 
                await TestResultStatusTest.testInsertRwkOnly();
                await TestResultStatusTest.testSelect();           
                
                Logger.Info("Testing TestPlan");
                await TestPlanTest.testInsert();            
                await TestPlanTest.testUpdate();    
                 await TestPlanTest.testInsert();            
                await TestPlanTest.testUpdate();     
                await TestPlanTest.testSelect(); 
                await TestPlanTest.testInsertRwkOnly();
                await TestPlanTest.testSelect();           
                
                Logger.Info("Testing TestCase");
                await TestCaseTest.testInsert();            
                await TestCaseTest.testUpdate();    
                 await TestCaseTest.testInsert();            
                await TestCaseTest.testUpdate();     
                await TestCaseTest.testSelect(); 
                await TestCaseTest.testInsertRwkOnly();
                await TestCaseTest.testSelect();           
                
                Logger.Info("Testing TestRun");
                await TestRunTest.testInsert();            
                await TestRunTest.testUpdate();    
                 await TestRunTest.testInsert();            
                await TestRunTest.testUpdate();     
                await TestRunTest.testSelect(); 
                await TestRunTest.testInsertRwkOnly();
                await TestRunTest.testSelect();           
                
                Logger.Info("Testing TestResult");
                await TestResultTest.testInsert();            
                await TestResultTest.testUpdate();    
                 await TestResultTest.testInsert();            
                await TestResultTest.testUpdate();     
                await TestResultTest.testSelect(); 
                await TestResultTest.testInsertRwkOnly();
                await TestResultTest.testSelect();           
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
