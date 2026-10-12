using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class SqlTest : BaseTest
    {
        public static async Task testInsert()
        {
            var sql = new Sql();


                    sql.name = Convert.ToString(BaseTest.getTestData(sql, "VARCHAR", TestDataType.random));
                    
                    sql.data_source_id = BaseTest.getLastId("DataSource");
                    
                    sql.sql_text = Convert.ToString(BaseTest.getTestData(sql, "VARCHAR", TestDataType.random));
                    
                    sql.description = Convert.ToString(BaseTest.getTestData(sql, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing Sql API insert: " + sql.ToString());
                var createdSql = await PostAsyncReturn("Sql", sql);
                BaseTest.addLastId("Sql", createdSql.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var sql = new Sql();


                        sql.name = Convert.ToString(BaseTest.getTestData(sql, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing Sql API insert (RWK only): " + sql.ToString());
                var createdSql = await PostAsyncReturn("Sql", sql);
                BaseTest.addLastId("Sql", createdSql.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("Sql");
            var sqlView = await GetByIdAsync<SqlView>("Sql", lastId);
            var sql = new Sql(sqlView);


                        sql.name = (string) BaseTest.getTestData(sql, "VARCHAR", TestDataType.random);
                    
                            sql.data_source_id = BaseTest.getLastId("DataSource");
                        
                        sql.sql_text = (string) BaseTest.getTestData(sql, "VARCHAR", TestDataType.random);
                    
                        sql.description = (string) BaseTest.getTestData(sql, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing Sql API update: " + sql.ToString());
                await PutAsync("Sql", lastId, sql);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("Sql");
            var sqlView = await GetByIdAsync<SqlView>("Sql", lastId);
            var sql = new Sql(sqlView);


                        sql.name = (string) BaseTest.getTestData(sql, "VARCHAR", TestDataType.random);
                    
                            sql.data_source_id = BaseTest.getLastId("DataSource");
                        
                        sql.sql_text = (string) BaseTest.getTestData(sql, "VARCHAR", TestDataType.random);
                    
                        sql.description = (string) BaseTest.getTestData(sql, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing Sql API update: " + sql.ToString());
                await PutAsync("Sql", lastId, sql);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing Sql API select (list):");
            
            try
            {
                var sqlList = await BaseTest.GetListAsync<Sql>("Sql");
                
                Console.WriteLine($"Retrieved {sqlList.Count} Sql records");
                
                if (sqlList.Count > 0)
                {
                    Console.WriteLine("First record: " + sqlList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed Sql records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < sqlList.Count; i++)
                    {
                        var sql = sqlList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {sql.id}");

                        Console.WriteLine($"  name: {sql.name}");
                                
                        Console.WriteLine($"  data_source_id: {sql.data_source_id}");
                                
                        Console.WriteLine($"  sql_text: {sql.sql_text}");
                                
                        Console.WriteLine($"  description: {sql.description}");
                                
                        Console.WriteLine($"  is_active: {sql.is_active}");
                                
                        Console.WriteLine($"  created_by: {sql.created_by}");
                                
                        Console.WriteLine($"  last_updated: {sql.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {sql.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {sql.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", sqlList[0].id);
                }
                else
                {
                    Console.WriteLine("No Sql records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing Sql select: {ex.Message}");
                throw;
            }
        }
    }
}
