
using System;


namespace jumptest
{
   
    public interface IPrincipalUserLogic
    {
        // User-defined methods
    }

    public class PrincipalUserLogic : PrincipalLogic, IPrincipalUserLogic
    {
        protected PrincipalUserLogic()
        {
           
        }
        
    }
}

