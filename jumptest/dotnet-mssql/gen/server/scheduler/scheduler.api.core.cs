
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest;
using jumptest.core;

namespace jumptest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        // =====================================
        // Job Management Operations
        // =====================================

        // POST api/<SchedulerController>/execute
        [HttpPost("execute")]
        public ActionResult<long> Execute([FromBody] long workflowId)
        {
            //Console.WriteLine($"Processing Scheduler EXECUTE: {workflowId}");
            
            SchedulerLogic.Create().execute(workflowId);
            return Ok(workflowId);
        }

        // POST api/<SchedulerController>/cancel/{id}
        [HttpPost("cancel/{id}")]
        public ActionResult Cancel(long workflowId)
        {
            //Console.WriteLine($"Processing Scheduler CANCEL: ID={workflowId}");
            
            SchedulerLogic.Create().cancel(workflowId);
            return Ok();
        }

        // POST api/<SchedulerController>/abort/{id}
        [HttpPost("abort/{id}")]
        public ActionResult Abort(long workflowId)
        {
            //Console.WriteLine($"Processing Scheduler ABORT: ID={workflowId}");
            
            SchedulerLogic.Create().abort(workflowId);
            return Ok();
        }

        // =====================================
        // Job Status & Monitoring Operations
        // =====================================

        // GET api/<SchedulerController>/status/{workflowId}
        [HttpGet("status/{workflowId}")]
        public ActionResult<object> Status(long workflowId)
        {
            //Console.WriteLine($"Processing Scheduler STATUS: ID={workflowId}");
            
            var status = SchedulerLogic.Create().status(workflowId);
            return Ok(status);
        }

        // =====================================
        // System Management Operations
        // =====================================

        // POST api/<SchedulerController>/register
        [HttpPost("register")]
        public ActionResult<long> Register([FromBody] ServerNode scheduler)
        {
            //Console.WriteLine($"Processing Scheduler REGISTER: {scheduler}");
            
            long schedulerId = SchedulerLogic.Create().register(scheduler);
            return Ok(schedulerId);
        }

        // POST api/<SchedulerController>/unregister/{workflowId}
        [HttpPost("unregister/{workflowId}")]
        public ActionResult Unregister(long workflowId)
        {
            //Console.WriteLine($"Processing Scheduler UNREGISTER: ID={workflowId}");
            
            SchedulerLogic.Create().unregister(workflowId);
            return Ok();
        }
    }
}
