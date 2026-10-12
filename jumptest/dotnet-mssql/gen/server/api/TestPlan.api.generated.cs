
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
    public partial class TestPlanController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="TestPlanView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<TestPlanView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<TestPlanView> testplans = TestPlanLogic.Create().select<TestPlanView>();

            return testplans;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="TestPlan"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public TestPlan Get(long id)
        {
            //Console.WriteLine($"Processing TestPlan GET ID={id}");

            TestPlan testplan = TestPlanLogic.Create().get(id);

            return testplan;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="TestPlanView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public TestPlanView View(long id)
        {
            //Console.WriteLine($"Processing TestPlan View ID={id}");

            TestPlanView testplanView = TestPlanLogic.Create().view(id);

            return testplanView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<TestPlan> testplans = TestPlanLogic.Create().select<TestPlan>();

            return testplans.Select(testplan => new EnumHelper(testplan.id, testplan.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="testplanView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="TestPlanView"/> with the assigned id.</returns>
        [HttpPost]
        public TestPlanView Post([FromBody] TestPlanView testplanView)
        {
            //Console.WriteLine($"Processing TestPlan POST: {testplan}");
            
            JsonHelper.ProcessJsonElements(testplanView);
            
            // Process any JsonElement values to ensure proper type conversion
            TestPlan testplan = new TestPlan(testplanView);

            
            
            TestPlanLogic.Create().put(testplan); 

            testplanView.id = testplan.id;

            return testplanView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="testplanView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="TestPlanView"/>.</returns>
        [HttpPut("{id}")]
        public TestPlanView Put(long id, [FromBody] TestPlanView testplanView)
        {
            //Console.WriteLine($"Processing TestPlan PUT: ID = {id}\n{testplan}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(testplanView);
            
            TestPlan testplan = new TestPlan(testplanView);

            TestPlanLogic.Create().update(id, testplan);

            testplanView.id = testplan.id;

            return testplanView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            TestPlanLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="TestPlanHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<TestPlanHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<TestPlanHistory> historyList = TestPlanLogic.Create().history(id);

            return historyList;
        }
            


       // Retrieves child records for a given domain object id and child role.
        [HttpGet("{id}/testcase_test_plan")]
        public IEnumerable<TestCaseView> Gettestcase_test_plan(long id)
        {
            //Console.WriteLine($"Processing GET Test Plan for TestPlan ID={id}");

            List<TestCaseView> testcases = TestPlanLogic.Create().children<TestCaseView>(id, "testcase-test_plan");

            return testcases;
        }
        
       // Retrieves child records for a given domain object id and child role.
        [HttpGet("{id}/testrun_test_plan")]
        public IEnumerable<TestRunView> Gettestrun_test_plan(long id)
        {
            //Console.WriteLine($"Processing GET Test Plan for TestPlan ID={id}");

            List<TestRunView> testruns = TestPlanLogic.Create().children<TestRunView>(id, "testrun-test_plan");

            return testruns;
        }
            }
}
