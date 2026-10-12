using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jumptest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                if (args.Any(a => a == "--init"))
                {
                    await WorkflowTest.testInsertWorkflows();
                }
                else
                {
                    await WorkflowTest.selectTestWorkflows();
                }
                
                await WorkflowTest.testExecWorkflows();
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
		}
    }
}
