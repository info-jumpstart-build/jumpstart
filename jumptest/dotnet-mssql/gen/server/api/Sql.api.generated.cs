
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
    public partial class SqlController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="SqlView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<SqlView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<SqlView> sqls = SqlLogic.Create().select<SqlView>();

            return sqls;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="Sql"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public Sql Get(long id)
        {
            //Console.WriteLine($"Processing Sql GET ID={id}");

            Sql sql = SqlLogic.Create().get(id);

            return sql;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="SqlView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public SqlView View(long id)
        {
            //Console.WriteLine($"Processing Sql View ID={id}");

            SqlView sqlView = SqlLogic.Create().view(id);

            return sqlView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<Sql> sqls = SqlLogic.Create().select<Sql>();

            return sqls.Select(sql => new EnumHelper(sql.id, sql.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="sqlView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="SqlView"/> with the assigned id.</returns>
        [HttpPost]
        public SqlView Post([FromBody] SqlView sqlView)
        {
            //Console.WriteLine($"Processing Sql POST: {sql}");
            
            JsonHelper.ProcessJsonElements(sqlView);
            
            // Process any JsonElement values to ensure proper type conversion
            Sql sql = new Sql(sqlView);

            
            
            SqlLogic.Create().put(sql); 

            sqlView.id = sql.id;

            return sqlView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="sqlView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="SqlView"/>.</returns>
        [HttpPut("{id}")]
        public SqlView Put(long id, [FromBody] SqlView sqlView)
        {
            //Console.WriteLine($"Processing Sql PUT: ID = {id}\n{sql}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(sqlView);
            
            Sql sql = new Sql(sqlView);

            SqlLogic.Create().update(id, sql);

            sqlView.id = sql.id;

            return sqlView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            SqlLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="SqlHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<SqlHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<SqlHistory> historyList = SqlLogic.Create().history(id);

            return historyList;
        }
            

    }
}
