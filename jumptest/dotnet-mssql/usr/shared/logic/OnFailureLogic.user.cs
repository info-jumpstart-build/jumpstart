
using System;


namespace jumptest
{
   
    public interface IOnFailureUserLogic
    {
        // User-defined methods
    }

    public class OnFailureUserLogic : OnFailureLogic, IOnFailureUserLogic
    {
        protected OnFailureUserLogic()
        {
           
        }
        
    }
}

