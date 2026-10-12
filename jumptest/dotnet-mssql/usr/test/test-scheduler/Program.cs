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
                await SchedulerTest.testDeleteExistingWorkflows();
                await SchedulerTest.testSingleJobWorkflow();
                await SchedulerTest.testTwoJobWorkflow();
                await SchedulerTest.testFourJobWorkflowWithConcurrency();
                await SchedulerTest.testNestedWorkflow();
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
		}
    }
}

