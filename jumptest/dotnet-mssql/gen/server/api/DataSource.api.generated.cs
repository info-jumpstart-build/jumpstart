
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class DataSourceController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="DataSourceView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<DataSourceView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<DataSourceView> datasources = DataSourceLogic.Create().select<DataSourceView>();

            return datasources;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="DataSource"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public DataSource Get(long id)
        {
            //Console.WriteLine($"Processing DataSource GET ID={id}");

            DataSource datasource = DataSourceLogic.Create().get(id);

            return datasource;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="DataSourceView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public DataSourceView View(long id)
        {
            //Console.WriteLine($"Processing DataSource View ID={id}");

            DataSourceView datasourceView = DataSourceLogic.Create().view(id);

            return datasourceView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<DataSource> datasources = DataSourceLogic.Create().select<DataSource>();

            return datasources.Select(datasource => new EnumHelper(datasource.id, datasource.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="datasourceView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="DataSourceView"/> with the assigned id.</returns>
        [HttpPost]
        public DataSourceView Post([FromBody] DataSourceView datasourceView)
        {
            //Console.WriteLine($"Processing DataSource POST: {datasource}");
            
            JsonHelper.ProcessJsonElements(datasourceView);
            
            // Process any JsonElement values to ensure proper type conversion
            DataSource datasource = new DataSource(datasourceView);

            
            
            DataSourceLogic.Create().put(datasource); 

            datasourceView.id = datasource.id;

            return datasourceView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="datasourceView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="DataSourceView"/>.</returns>
        [HttpPut("{id}")]
        public DataSourceView Put(long id, [FromBody] DataSourceView datasourceView)
        {
            //Console.WriteLine($"Processing DataSource PUT: ID = {id}\n{datasource}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(datasourceView);
            
            DataSource datasource = new DataSource(datasourceView);

            DataSourceLogic.Create().update(id, datasource);

            datasourceView.id = datasource.id;

            return datasourceView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            DataSourceLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="DataSourceHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<DataSourceHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<DataSourceHistory> historyList = DataSourceLogic.Create().history(id);

            return historyList;
        }
            

    }
}
