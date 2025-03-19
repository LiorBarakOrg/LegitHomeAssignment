using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics.Tracing;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

using LegitHomeTask.Models;
using LegitHomeTask.Services;

namespace LegitHomeTask.Controllers
{
    [ApiController]
    [Route("api/Legit_home_task")]
    public class WebhookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SuspiciousBehaviorDetector _suspiciousBehaviorDetector;

        public WebhookController(IConfiguration configuration, SuspiciousBehaviorDetector suspiciousBehaviorDetector)
        {
            _configuration = configuration;
            _suspiciousBehaviorDetector = suspiciousBehaviorDetector;
        }

        [HttpPost("github_event/receive")]
        public async Task<IActionResult> ReceiveGithubEventWebhook()
        {
            // Get the event's type from the header of the given github event
            var eventType = Request.Headers["X-GitHub-Event"].ToString() ?? "";

            // Read the body of the request
            var reader = new StreamReader(Request.Body);

            // Read the body of the request
            var body = await reader.ReadToEndAsync();

            if (body == null || body.Length == 0 || string.IsNullOrWhiteSpace(body))
            {
                Console.WriteLine("Received empty request body in the event");
                return BadRequest("Empty body received");
            }

            // TODO: add authentication, authorization and valisation of the webhook

            try
            {
                // Parse the JSON body
                var jsonDoc = JsonDocument.Parse(body);
                var root = jsonDoc.RootElement;

                _suspiciousBehaviorDetector.ProcessEvent(root, eventType);

                return Ok("Webhook processed successfully");
            }
            catch (Exception ex)
            {
                // Log the error and return a failed response
                Console.WriteLine($"Error processing webhook: {ex.Message}");
                return BadRequest("Invalid webhook event");
            }
        }
    }
}