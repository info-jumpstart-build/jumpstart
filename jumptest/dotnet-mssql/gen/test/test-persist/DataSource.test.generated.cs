using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class DataSourceTest : BaseTest
    {
        public static void testInsert()
        {
            var datasource = new DataSource();


                    datasource.name = Convert.ToString(BaseTest.getTestData(datasource, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing DataSourceLogic insert: " + datasource.ToString());
                DataSourceLogic.Create().insert(datasource);
                BaseTest.addLastId("datasource", datasource.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("datasource");
            var datasourceView  = DataSourceLogic.Create().get(lastId);

            DataSource datasource = new DataSource(datasourceView);

                        datasource.name = (string) BaseTest.getTestData(datasource, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing DataSourceLogic update: " + datasource.ToString());
                DataSourceLogic.Create().update(lastId, datasource);
                    }
    }
}
