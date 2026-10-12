using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest
{
    public partial class DataSourceTest : BaseTest
    {
        public static async Task testInsert()
        {
            var datasource = new DataSource();


                    datasource.name = Convert.ToString(BaseTest.getTestData(datasource, "VARCHAR", TestDataType.random));
                    
                Console.WriteLine("Testing DataSource API insert: " + datasource.ToString());
                var createdDataSource = await PostAsyncReturn("DataSource", datasource);
                BaseTest.addLastId("DataSource", createdDataSource.id);
                    }

        public static async Task testInsertRwkOnly()
        {
            var datasource = new DataSource();


                        datasource.name = Convert.ToString(BaseTest.getTestData(datasource, "VARCHAR", TestDataType.random));
                        
                Console.WriteLine("Testing DataSource API insert (RWK only): " + datasource.ToString());
                var createdDataSource = await PostAsyncReturn("DataSource", datasource);
                BaseTest.addLastId("DataSource", createdDataSource.id);
                    }

        public static async Task testUpdate()
        {
            long lastId = BaseTest.getLastId("DataSource");
            var datasourceView = await GetByIdAsync<DataSourceView>("DataSource", lastId);
            var datasource = new DataSource(datasourceView);


                        datasource.name = (string) BaseTest.getTestData(datasource, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing DataSource API update: " + datasource.ToString());
                await PutAsync("DataSource", lastId, datasource);
                    }

        public static async Task testPut()
        {
            long lastId = BaseTest.getLastId("DataSource");
            var datasourceView = await GetByIdAsync<DataSourceView>("DataSource", lastId);
            var datasource = new DataSource(datasourceView);


                        datasource.name = (string) BaseTest.getTestData(datasource, "VARCHAR", TestDataType.random);
                    
                Console.WriteLine("Testing DataSource API update: " + datasource.ToString());
                await PutAsync("DataSource", lastId, datasource);
                    }

        public static async Task testSelect()
        {
            Console.WriteLine("Testing DataSource API select (list):");
            
            try
            {
                var datasourceList = await BaseTest.GetListAsync<DataSource>("DataSource");
                
                Console.WriteLine($"Retrieved {datasourceList.Count} DataSource records");
                
                if (datasourceList.Count > 0)
                {
                    Console.WriteLine("First record: " + datasourceList[0].ToString());
                    
                    // Output all fields for each object, one per row
                    Console.WriteLine("\nDetailed DataSource records:");
                    Console.WriteLine("=" + new string('=', 50));
                    
                    for (int i = 0; i < datasourceList.Count; i++)
                    {
                        var datasource = datasourceList[i];
                        Console.WriteLine($"Record {i + 1}:");
                        Console.WriteLine($"  ID: {datasource.id}");

                        Console.WriteLine($"  name: {datasource.name}");
                                
                        Console.WriteLine($"  is_active: {datasource.is_active}");
                                
                        Console.WriteLine($"  created_by: {datasource.created_by}");
                                
                        Console.WriteLine($"  last_updated: {datasource.last_updated}");
                                
                        Console.WriteLine($"  last_updated_by: {datasource.last_updated_by}");
                                
                        Console.WriteLine($"  txn_id: {datasource.txn_id}");
                                                        Console.WriteLine();
                    }
                    
                    // Store the first record for potential use in other tests
                    BaseTest.addLastId("@(Model.DomainObj)", datasourceList[0].id);
                }
                else
                {
                    Console.WriteLine("No DataSource records found in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing DataSource select: {ex.Message}");
                throw;
            }
        }
    }
}
