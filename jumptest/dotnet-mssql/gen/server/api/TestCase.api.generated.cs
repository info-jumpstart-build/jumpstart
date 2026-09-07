
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
    public partial class TestCaseController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="TestCaseView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<TestCaseView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<TestCaseView> testcases = TestCaseLogic.Create().select<TestCaseView>();

            return testcases;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="TestCase"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public TestCase Get(long id)
        {
            //Console.WriteLine($"Processing TestCase GET ID={id}");

            TestCase testcase = TestCaseLogic.Create().get(id);

            return testcase;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="TestCaseView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public TestCaseView View(long id)
        {
            //Console.WriteLine($"Processing TestCase View ID={id}");

            TestCaseView testcaseView = TestCaseLogic.Create().view(id);

            return testcaseView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<TestCase> testcases = TestCaseLogic.Create().select<TestCase>();

            return testcases.Select(testcase => new EnumHelper(testcase.id, testcase.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="testcaseView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="TestCaseView"/> with the assigned id.</returns>
        [HttpPost]
        public TestCaseView Post([FromBody] TestCaseView testcaseView)
        {
            //Console.WriteLine($"Processing TestCase POST: {testcase}");
            
            JsonHelper.ProcessJsonElements(testcaseView);
            
            // Process any JsonElement values to ensure proper type conversion
            TestCase testcase = new TestCase(testcaseView);

            
            
            TestCaseLogic.Create().put(testcase); 

            testcaseView.id = testcase.id;

            return testcaseView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="testcaseView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="TestCaseView"/>.</returns>
        [HttpPut("{id}")]
        public TestCaseView Put(long id, [FromBody] TestCaseView testcaseView)
        {
            //Console.WriteLine($"Processing TestCase PUT: ID = {id}\n{testcase}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(testcaseView);
            
            TestCase testcase = new TestCase(testcaseView);

            TestCaseLogic.Create().update(id, testcase);

            testcaseView.id = testcase.id;

            return testcaseView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            TestCaseLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="TestCaseHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<TestCaseHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<TestCaseHistory> historyList = TestCaseLogic.Create().history(id);

            return historyList;
        }
            


       // Retrieves child records for a given domain object id and child role.
        [HttpGet("{id}/testresult_test_case")]
        public IEnumerable<TestResultView> Gettestresult_test_case(long id)
        {
            //Console.WriteLine($"Processing GET Test Case for TestCase ID={id}");

            List<TestResultView> testresults = TestCaseLogic.Create().children<TestResultView>(id, "testresult-test_case");

            return testresults;
        }
            }
}
