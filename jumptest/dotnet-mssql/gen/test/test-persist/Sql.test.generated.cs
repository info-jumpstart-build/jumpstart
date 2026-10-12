using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class SqlTest : BaseTest
    {
        public static void testInsert()
        {
            var sql = new Sql();


                    sql.name = Convert.ToString(BaseTest.getTestData(sql, "VARCHAR", TestDataType.random));
                    
                    sql.data_source_id = (long) BaseTest.getLastId("datasource");
                    
                    sql.sql_text = Convert.ToString(BaseTest.getTestData(sql, "VARCHAR", TestDataType.random));
                    
                    sql.description = Convert.ToString(BaseTest.getTestData(sql, "VARCHAR", TestDataType.random));
                    
                Logger.Info("Testing SqlLogic insert: " + sql.ToString());
                SqlLogic.Create().insert(sql);
                BaseTest.addLastId("sql", sql.id);
                    }

        public static void testUpdate()
        {
            long lastId = BaseTest.getLastId("sql");
            var sqlView  = SqlLogic.Create().get(lastId);

            Sql sql = new Sql(sqlView);

                        sql.name = (string) BaseTest.getTestData(sql, "VARCHAR", TestDataType.random);
                    
                            sql.data_source_id = (long) BaseTest.getLastId("datasource");
                        
                        sql.sql_text = (string) BaseTest.getTestData(sql, "VARCHAR", TestDataType.random);
                    
                        sql.description = (string) BaseTest.getTestData(sql, "VARCHAR", TestDataType.random);
                    
                Logger.Info("Testing SqlLogic update: " + sql.ToString());
                SqlLogic.Create().update(lastId, sql);
                    }
    }
}
