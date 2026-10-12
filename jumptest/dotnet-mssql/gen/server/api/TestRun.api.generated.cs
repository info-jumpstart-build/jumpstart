
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
    public partial class TestRunController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="TestRunView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<TestRunView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<TestRunView> testruns = TestRunLogic.Create().select<TestRunView>();

            return testruns;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="TestRun"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public TestRun Get(long id)
        {
            //Console.WriteLine($"Processing TestRun GET ID={id}");

            TestRun testrun = TestRunLogic.Create().get(id);

            return testrun;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="TestRunView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public TestRunView View(long id)
        {
            //Console.WriteLine($"Processing TestRun View ID={id}");

            TestRunView testrunView = TestRunLogic.Create().view(id);

            return testrunView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<TestRun> testruns = TestRunLogic.Create().select<TestRun>();

            return testruns.Select(testrun => new EnumHelper(testrun.id, testrun.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="testrunView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="TestRunView"/> with the assigned id.</returns>
        [HttpPost]
        public TestRunView Post([FromBody] TestRunView testrunView)
        {
            //Console.WriteLine($"Processing TestRun POST: {testrun}");
            
            JsonHelper.ProcessJsonElements(testrunView);
            
            // Process any JsonElement values to ensure proper type conversion
            TestRun testrun = new TestRun(testrunView);

            
            
            TestRunLogic.Create().put(testrun); 

            testrunView.id = testrun.id;

            return testrunView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="testrunView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="TestRunView"/>.</returns>
        [HttpPut("{id}")]
        public TestRunView Put(long id, [FromBody] TestRunView testrunView)
        {
            //Console.WriteLine($"Processing TestRun PUT: ID = {id}\n{testrun}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(testrunView);
            
            TestRun testrun = new TestRun(testrunView);

            TestRunLogic.Create().update(id, testrun);

            testrunView.id = testrun.id;

            return testrunView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            TestRunLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="TestRunHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<TestRunHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<TestRunHistory> historyList = TestRunLogic.Create().history(id);

            return historyList;
        }
            


       // Retrieves child records for a given domain object id and child role.
        [HttpGet("{id}/testresult_test_run")]
        public IEnumerable<TestResultView> Gettestresult_test_run(long id)
        {
            //Console.WriteLine($"Processing GET Test Run for TestRun ID={id}");

            List<TestResultView> testresults = TestRunLogic.Create().children<TestResultView>(id, "testresult-test_run");

            return testresults;
        }
            }
}
