
using System;


namespace jumptest
{
   
    public interface ITestResultUserLogic
    {
        // User-defined methods
    }

    public class TestResultUserLogic : TestResultLogic, ITestResultUserLogic
    {
        protected TestResultUserLogic()
        {
           
        }
        
    }
}

