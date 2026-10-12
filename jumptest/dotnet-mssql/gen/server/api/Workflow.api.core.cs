
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;

namespace jumptest.Controllers
{

    public partial class WorkflowController
    {

        // GET api/workflow/run/{id}
        [HttpGet("run/{id}")]
        public async Task<IActionResult> Run(long id)
        {
            Logger.Debug($"WorkflowController.Run: id={id}");
            var workflowCoreLogic = WorkflowCoreLogic.Create();

            try
            {
                await workflowCoreLogic.run(id);
            }
            catch(Exception x)
            {
                return StatusCode(503, x.Message);
            }

            
            return Ok(id);
        }


    }
}
