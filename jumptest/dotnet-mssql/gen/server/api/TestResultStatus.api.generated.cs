
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
    public partial class TestResultStatusController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="TestResultStatusView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<TestResultStatusView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<TestResultStatusView> testresultstatuss = TestResultStatusLogic.Create().select<TestResultStatusView>();

            return testresultstatuss;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="TestResultStatus"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public TestResultStatus Get(long id)
        {
            //Console.WriteLine($"Processing TestResultStatus GET ID={id}");

            TestResultStatus testresultstatus = TestResultStatusLogic.Create().get(id);

            return testresultstatus;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="TestResultStatusView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public TestResultStatusView View(long id)
        {
            //Console.WriteLine($"Processing TestResultStatus View ID={id}");

            TestResultStatusView testresultstatusView = TestResultStatusLogic.Create().view(id);

            return testresultstatusView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<TestResultStatus> testresultstatuss = TestResultStatusLogic.Create().select<TestResultStatus>();

            return testresultstatuss.Select(testresultstatus => new EnumHelper(testresultstatus.id, testresultstatus.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="testresultstatusView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="TestResultStatusView"/> with the assigned id.</returns>
        [HttpPost]
        public TestResultStatusView Post([FromBody] TestResultStatusView testresultstatusView)
        {
            //Console.WriteLine($"Processing TestResultStatus POST: {testresultstatus}");
            
            JsonHelper.ProcessJsonElements(testresultstatusView);
            
            // Process any JsonElement values to ensure proper type conversion
            TestResultStatus testresultstatus = new TestResultStatus(testresultstatusView);

            
            
            TestResultStatusLogic.Create().put(testresultstatus); 

            testresultstatusView.id = testresultstatus.id;

            return testresultstatusView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="testresultstatusView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="TestResultStatusView"/>.</returns>
        [HttpPut("{id}")]
        public TestResultStatusView Put(long id, [FromBody] TestResultStatusView testresultstatusView)
        {
            //Console.WriteLine($"Processing TestResultStatus PUT: ID = {id}\n{testresultstatus}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(testresultstatusView);
            
            TestResultStatus testresultstatus = new TestResultStatus(testresultstatusView);

            TestResultStatusLogic.Create().update(id, testresultstatus);

            testresultstatusView.id = testresultstatus.id;

            return testresultstatusView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            TestResultStatusLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="TestResultStatusHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<TestResultStatusHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<TestResultStatusHistory> historyList = TestResultStatusLogic.Create().history(id);

            return historyList;
        }
            

    }
}
