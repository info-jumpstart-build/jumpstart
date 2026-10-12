
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jumptest.core;

namespace jumptest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ScriptAgentController : ControllerBase
    {
        // =====================================
        // Process Management Operations
        // =====================================


        // POST api/scriptagent/stop/{executionId}
        [HttpPost("stop/{executionId}")]
        public ActionResult Stop(long executionId)
        {
            //Console.WriteLine($"Processing Agent STOP: ID={executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().stop(executionId);
            return Ok();
        }

        // POST api/scriptagent/kill/{executionId}
        [HttpPost("kill/{executionId}")]
        public ActionResult Kill(long executionId)
        {
            //Console.WriteLine($"Processing Agent KILL: ID={executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().kill(executionId);
            return Ok();
        }

        // POST api/scriptagent/restart/{executionId}
        [HttpPost("restart/{executionId}")]
        public ActionResult Restart(long executionId)
        {
            //Console.WriteLine($"Processing Agent RESTART: ID={executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().restart(executionId);
            return Ok();
        }

        // POST api/scriptagent/pause/{executionId}
        [HttpPost("pause/{executionId}")]
        public ActionResult Pause(long executionId)
        {
            //Console.WriteLine($"Processing Agent PAUSE: ID={executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().pause(executionId);
            return Ok();
        }

        // POST api/scriptagent/resume/{executionId}
        [HttpPost("resume/{executionId}")]
        public ActionResult Resume(long executionId)
        {
            //Console.WriteLine($"Processing Agent RESUME: ID={executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().resume(executionId);
            return Ok();
        }

        // =====================================
        // Status & Reporting Operations
        // =====================================

        // GET api/scriptagent/status/{executionId}
        [HttpGet("status/{executionId}")]
        public ActionResult<object> Status(long executionId)
        {
            //Console.WriteLine($"Processing Agent STATUS: ID={executionId}");
            
            var status = jumptest.core.ScriptAgentLogic.Create().status(executionId);
            return Ok(status);
        }

        // POST api/scriptagent/heartbeat
        [HttpPost("heartbeat")]
        public ActionResult Heartbeat([FromBody] object heartbeatData)
        {
            //Console.WriteLine($"Processing Agent HEARTBEAT");
            
            jumptest.core.ScriptAgentLogic.Create().heartbeat(heartbeatData);
            return Ok();
        }

        // POST api/scriptagent/report
        [HttpPost("report")]
        public ActionResult Report([FromBody] object reportData)
        {
            //Console.WriteLine($"Processing Agent REPORT");
            
            jumptest.core.ScriptAgentLogic.Create().report(reportData);
            return Ok();
        }

        // POST api/scriptagent/log
        [HttpPost("log")]
        public ActionResult Log([FromBody] object logData)
        {
            //Console.WriteLine($"Processing Agent LOG");
            
            jumptest.core.ScriptAgentLogic.Create().log(logData);
            return Ok();
        }

        // POST api/scriptagent/metrics
        [HttpPost("metrics")]
        public ActionResult Metrics([FromBody] object metricsData)
        {
            //Console.WriteLine($"Processing Agent METRICS");
            
            jumptest.core.ScriptAgentLogic.Create().metrics(metricsData);
            return Ok();
        }

        // =====================================
        // Resource Management Operations
        // =====================================

       
        // GET api/scriptagent/capabilities
        [HttpGet("capabilities")]
        public ActionResult<object> Capabilities()
        {
            //Console.WriteLine($"Processing Agent CAPABILITIES");
            
            var capabilities = jumptest.core.ScriptAgentLogic.Create().capabilities();
            return Ok(capabilities);
        }

        // POST api/scriptagent/allocate
        [HttpPost("allocate")]
        public ActionResult<long> Allocate([FromBody] long executionId)
        {
            //Console.WriteLine($"Processing Agent ALLOCATE: {executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().allocate(executionId);
            return Ok(executionId);
        }

        // POST api/scriptagent/release/{executionId}
        [HttpPost("release/{executionId}")]
        public ActionResult Release(long executionId)
        {
            //Console.WriteLine($"Processing Agent RELEASE: ID={executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().release(executionId);
            return Ok();
        }

        // =====================================
        // Job Execution Operations
        // =====================================

        // POST api/scriptagent/execute
        [HttpPost("execute")]
        public ActionResult<long> Execute([FromBody] long executionId)
        {
            //Console.WriteLine($"Processing Agent EXECUTE: {executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().execute(executionId);
            return Ok(executionId);
        }

        // POST api/scriptagent/validate
        [HttpPost("validate")]
        public ActionResult Validate([FromBody] long executionId)
        {
            //Console.WriteLine($"Processing Agent VALIDATE: {executionId}");
            
            var validationResult = jumptest.core.ScriptAgentLogic.Create().validate(executionId);
            return Ok(validationResult);
        }

        // POST api/scriptagent/prepare
        [HttpPost("prepare")]
        public ActionResult<long> Prepare([FromBody] long executionId)
        {
            //Console.WriteLine($"Processing Agent PREPARE: {executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().prepare(executionId);
            return Ok(executionId);
        }

        // POST api/scriptagent/cleanup/{executionId}
        [HttpPost("cleanup/{executionId}")]
        public ActionResult Cleanup(long executionId)
        {
            //Console.WriteLine($"Processing Agent CLEANUP: ID={executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().cleanup(executionId);
            return Ok();
        }

        // POST api/scriptagent/retry/{executionId}
        [HttpPost("retry/{executionId}")]
        public ActionResult Retry(long executionId)
        {
            //Console.WriteLine($"Processing Agent RETRY: ID={executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().retry(executionId);
            return Ok();
        }

        // =====================================
        // Communication Operations
        // =====================================

        // GET api/scriptagent/ping
        [HttpGet("ping")]
        public ActionResult Ping()
        {
            //Console.WriteLine($"Processing Agent PING");
            
            var pingResponse = jumptest.core.ScriptAgentLogic.Create().ping();
            return Ok(pingResponse);
        }

        // POST api/scriptagent/acknowledge
        [HttpPost("acknowledge")]
        public ActionResult Acknowledge([FromBody] long executionId)
        {
            //Console.WriteLine($"Processing Agent ACKNOWLEDGE: {executionId}");
            
            jumptest.core.ScriptAgentLogic.Create().acknowledge(executionId);
            return Ok();
        }

        // POST api/scriptagent/notify
        [HttpPost("notify")]
        public ActionResult Notify([FromBody] object notificationData)
        {
            //Console.WriteLine($"Processing Agent NOTIFY");
            
            jumptest.core.ScriptAgentLogic.Create().notify(notificationData);
            return Ok();
        }

        // POST api/scriptagent/request
        [HttpPost("request")]
        public new ActionResult<object> Request([FromBody] object requestData)
        {
            //Console.WriteLine($"Processing Agent REQUEST");
            
            var response = jumptest.core.ScriptAgentLogic.Create().request(requestData);
            return Ok(response);
        }

        // =====================================
        // System Operations
        // =====================================

        // POST api/scriptagent/shutdown
        [HttpPost("shutdown")]
        public ActionResult Shutdown()
        {
            //Console.WriteLine($"Processing Agent SHUTDOWN");
            
            jumptest.core.ScriptAgentLogic.Create().shutdown();
            return Ok();
        }

        // POST api/scriptagent/reload
        [HttpPost("reload")]
        public ActionResult Reload()
        {
            //Console.WriteLine($"Processing Agent RELOAD");
            
            jumptest.core.ScriptAgentLogic.Create().reload();
            return Ok();
        }

        // POST api/scriptagent/update
        [HttpPost("update")]
        public ActionResult Update([FromBody] object updateData)
        {
            //Console.WriteLine($"Processing Agent UPDATE");
            
            jumptest.core.ScriptAgentLogic.Create().update(updateData);
            return Ok();
        }

        // GET api/scriptagent/diagnose
        [HttpGet("diagnose")]
        public ActionResult Diagnose()
        {
            //Console.WriteLine($"Processing Agent DIAGNOSE");
            
            var diagnostics = jumptest.core.ScriptAgentLogic.Create().diagnose();
            return Ok(diagnostics);
        }

        // GET api/scriptagent/health
        [HttpGet("health")]
        public ActionResult Health()
        {
            //Console.WriteLine($"Processing Agent HEALTH");
            
            var healthStatus = jumptest.core.ScriptAgentLogic.Create().health();
            return Ok(healthStatus);
        }

        // =====================================
        // Security Operations
        // =====================================

        // POST api/scriptagent/authenticate
        [HttpPost("authenticate")]
        public ActionResult Authenticate([FromBody] object credentials)
        {
            //Console.WriteLine($"Processing Agent AUTHENTICATE");
            
            var authResult = jumptest.core.ScriptAgentLogic.Create().authenticate(credentials);
            return Ok(authResult);
        }

        // POST api/scriptagent/authorize
        [HttpPost("authorize")]
        public ActionResult Authorize([FromBody] object authorizationData)
        {
            //Console.WriteLine($"Processing Agent AUTHORIZE");
            
            var authResult = jumptest.core.ScriptAgentLogic.Create().authorize(authorizationData);
            return Ok(authResult);
        }

        // =====================================
        // Standard Operations
        // =====================================

        // GET: api/agent/executions
        [HttpGet("executions")]
        public ActionResult<IEnumerable<long>> GetExecutions()
        {
            //Console.WriteLine("Processing GET Execution List");
            List<long> executionIds = jumptest.core.ScriptAgentLogic.Create().select();
            return Ok(executionIds);
        }

        // GET api/scriptagent/execution/{executionId}
        [HttpGet("execution/{executionId}")]
        public ActionResult<object> GetExecution(long executionId)
        {
            //Console.WriteLine($"Processing Agent GET Execution ID={executionId}");
            var execution = jumptest.core.ScriptAgentLogic.Create().get(executionId);
            return Ok(execution);
        }

        // GET api/scriptagent/info
        [HttpGet("info")]
        public ActionResult<object> GetAgent()
        {
            //Console.WriteLine($"Processing Agent GET Info");
            var agentInfo = jumptest.core.ScriptAgentLogic.Create().getAgentInfo();
            return Ok(agentInfo);
        }

        // POST api/scriptagent/execution
        [HttpPost("execution")]
        public ActionResult<long> CreateExecution([FromBody] object executionDefinition)
        {
            //Console.WriteLine($"Processing Agent POST: Create Execution");
            long executionId = jumptest.core.ScriptAgentLogic.Create().insert(executionDefinition);
            return CreatedAtAction(nameof(GetExecution), new { executionId = executionId }, executionId);
        }

        // PUT api/scriptagent/execution/{executionId}
        [HttpPut("execution/{executionId}")]
        public ActionResult UpdateExecution(long executionId, [FromBody] object executionData)
        {
            //Console.WriteLine($"Processing Agent PUT: Update Execution ID={executionId}");
            jumptest.core.ScriptAgentLogic.Create().update(executionId, executionData);
            return Ok();
        }

        // DELETE api/scriptagent/execution/{executionId}
        [HttpDelete("execution/{executionId}")]
        public ActionResult DeleteExecution(long executionId)
        {
            //Console.WriteLine($"Processing Agent DELETE: Execution ID={executionId}");
            jumptest.core.ScriptAgentLogic.Create().delete(executionId);
            return Ok();
        }
    }
}
