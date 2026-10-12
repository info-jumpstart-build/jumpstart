
using System;


namespace jumptest
{
   
    public interface ISqlUserLogic
    {
        // User-defined methods
    }

    public class SqlUserLogic : SqlLogic, ISqlUserLogic
    {
        protected SqlUserLogic()
        {
           
        }
        
    }
}

