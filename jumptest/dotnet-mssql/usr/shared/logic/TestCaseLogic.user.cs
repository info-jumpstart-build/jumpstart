
using System;


namespace jumptest
{
   
    public interface ITestCaseUserLogic
    {
        // User-defined methods
    }

    public class TestCaseUserLogic : TestCaseLogic, ITestCaseUserLogic
    {
        protected TestCaseUserLogic()
        {
           
        }
        
    }
}

