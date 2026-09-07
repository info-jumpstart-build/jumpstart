
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
    public partial class TestResultController : ControllerBase
    {
        /// <summary>
        /// Retrieves all domain objects as a list of view objects.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="TestResultView"/> representing all domain objects.</returns>
        [HttpGet]
        public IEnumerable<TestResultView> Get()
        {
            //Console.WriteLine("Processing GET List");

            List<TestResultView> testresults = TestResultLogic.Create().select<TestResultView>();

            return testresults;
        }

        /// <summary>
        /// Retrieves a single domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve.</param>
        /// <returns>The <see cref="TestResult"/> instance with the specified id, or null if not found.</returns>
        [HttpGet("{id}")]
        public TestResult Get(long id)
        {
            //Console.WriteLine($"Processing TestResult GET ID={id}");

            TestResult testresult = TestResultLogic.Create().get(id);

            return testresult;
        }

        /// <summary>
        /// Retrieves a view representation of a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve the view for.</param>
        /// <returns>A <see cref="TestResultView"/> instance, or null if not found.</returns>
        [HttpGet("view/{id}")]
        public TestResultView View(long id)
        {
            //Console.WriteLine($"Processing TestResult View ID={id}");

            TestResultView testresultView = TestResultLogic.Create().view(id);

            return testresultView;
        }

     

        /// <summary>
        /// Retrieves all domain objects as enum helper objects containing id and display string.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="EnumHelper"/> objects for enumeration purposes.</returns>
        [HttpGet("enum")]
        public IEnumerable<EnumHelper> GetEnum()
        {
            //Console.WriteLine("Processing GET Enum");

            List<TestResult> testresults = TestResultLogic.Create().select<TestResult>();

            return testresults.Select(testresult => new EnumHelper(testresult.id, testresult.getRwkString()));
        }
       

      
        /// <summary>
        /// Creates a new domain object from the provided view object.
        /// </summary>
        /// <param name="testresultView">The view object containing the data for the new domain object.</param>
        /// <returns>The created <see cref="TestResultView"/> with the assigned id.</returns>
        [HttpPost]
        public TestResultView Post([FromBody] TestResultView testresultView)
        {
            //Console.WriteLine($"Processing TestResult POST: {testresult}");
            
            JsonHelper.ProcessJsonElements(testresultView);
            
            // Process any JsonElement values to ensure proper type conversion
            TestResult testresult = new TestResult(testresultView);

            
            
            TestResultLogic.Create().put(testresult); 

            testresultView.id = testresult.id;

            return testresultView;
        }

        /// <summary>
        /// Updates an existing domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to update.</param>
        /// <param name="testresultView">The view object containing the updated data.</param>
        /// <returns>The updated <see cref="TestResultView"/>.</returns>
        [HttpPut("{id}")]
        public TestResultView Put(long id, [FromBody] TestResultView testresultView)
        {
            //Console.WriteLine($"Processing TestResult PUT: ID = {id}\n{testresult}");
            
            // Process any JsonElement values to ensure proper type conversion
            JsonHelper.ProcessJsonElements(testresultView);
            
            TestResult testresult = new TestResult(testresultView);

            TestResultLogic.Create().update(id, testresult);

            testresultView.id = testresult.id;

            return testresultView;
        }

        /// <summary>
        /// Deletes a domain object by id (soft delete).
        /// </summary>
        /// <param name="id">The id of the domain object to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            TestResultLogic.Create().delete(id);
        }
         
        /// <summary>
        /// Retrieves the history records for a domain object by id.
        /// </summary>
        /// <param name="id">The id of the domain object to retrieve history for.</param>
        /// <returns>An enumerable collection of <see cref="TestResultHistory"/> entries.</returns>
        [HttpGet("{id}/history")]
        public IEnumerable<TestResultHistory> GetHistory(long id)
        {
            //Console.WriteLine($"Processing GET History for ID={id}");

            List<TestResultHistory> historyList = TestResultLogic.Create().history(id);

            return historyList;
        }
            

    }
}
