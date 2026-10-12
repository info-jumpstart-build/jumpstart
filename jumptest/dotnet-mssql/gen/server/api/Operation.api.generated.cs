
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
    public partial class OperationController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="OperationView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<OperationView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<OperationView> operations = OperationLogic.Create().select<OperationView>();

            return operations;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="Operation"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public Operation Get(long id)
        {
            //Console.WriteLine($"Processing Operation GET ID={id}");

            Operation operation = OperationLogic.Create().get(id);

            return operation;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="OperationView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public OperationView View(long id)
        {
            //Console.WriteLine($"Processing Operation View ID={id}");

            OperationView operationView = OperationLogic.Create().view(id);

            return operationView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<Operation> operations = OperationLogic.Create().select<Operation>();

            return operations.Select(operation => new EnumHelper(operation.id, operation.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="operationView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="OperationView"/> with the assigned id.</returns>
        [HttpPost]
        public OperationView Post([FromBody] OperationView operationView)
        {
            //Console.WriteLine($"Processing Operation POST: {operation}");
            
            JsonHelper.ProcessJsonElements(operationView);
            
            // Process any JsonElement values to ensure proper type conversion
            Operation operation = new Operation(operationView);

            
            
            OperationLogic.Create().put(operation); 

            operationView.id = operation.id;

            return operationView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="operationView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="OperationView"/>.</returns>
        [HttpPut("{id}")]
        public OperationView Put(long id, [FromBody] OperationView operationView)
        {
            //Console.WriteLine($"Processing Operation PUT: ID = {id}\n{operation}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(operationView);
            
            Operation operation = new Operation(operationView);

            OperationLogic.Create().update(id, operation);

            operationView.id = operation.id;

            return operationView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            OperationLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="OperationHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<OperationHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<OperationHistory> historyList = OperationLogic.Create().history(id);

            return historyList;
        }
            


       // Retrieves the "Operation Role" checklist (id/label/mapped rows) for a given Operation id.
        [HttpGet("{id}/map/oprole_op_role")]
        public IEnumerable<MapOption> Getoprole_op_role_Map(long id)
        {
            //Console.WriteLine($"Processing GET Operation Role map for Operation ID={id}");

            List<MapOption> options = OperationLogic.Create().maplist(id, "oprole-op_role");

            return options;
        }
<<<<<<< HEAD
=======

        // Applies the "Operation Role" checklist submitted from the Edit page: the full set of
        // Operation Role ids that should be mapped to this Operation after the call.
        [HttpPut("{id}/map/oprole_op_role")]
        public void Putoprole_op_role_Map(long id, [FromBody] List<long> checkedIds)
        {
            //Console.WriteLine($"Processing PUT Operation Role map for Operation ID={id}");

            OperationLogic.Create().mapsync(id, "oprole-op_role", checkedIds ?? new List<long>());
        }
>>>>>>> main
            }
}
