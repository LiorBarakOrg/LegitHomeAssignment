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

        [HttpPost("event/send")]
        // public IActionResult ReceiveWebhook([FromBody] MyModel model)
        public async Task<IActionResult> ReceiveWebhook()
        {
            // Get the event's type from the header of the given github event
            var eventType = Request.Headers["X-GitHub-Event"].ToString() ?? "";
            Console.WriteLine($"eventType: {eventType}");

            // Read the body of the request
            var reader = new StreamReader(Request.Body);
            // Console.WriteLine($"reader: {reader}");

            // Read the body of the request
            var body = await reader.ReadToEndAsync();
            // Console.WriteLine($"body: {body}");

            if (body == null || body.Length == 0 || string.IsNullOrWhiteSpace(body))
            {
                Console.WriteLine("Received empty request body in the event");
                return BadRequest("Empty body received");
            }

            // Get the signature from the header
            // var signature = Request.Headers["X-Hub-Signature"].ToString() ?? "";
            // Console.WriteLine($"signature: {signature}");
            // if (!VerifySignature(body, signature))
            // {
            //     Console.WriteLine("☠️ Invalid webhook received!");
            //     return Unauthorized("Invalid signature");
            // }

            try
            {
                // Parse the JSON body
                var jsonDoc = JsonDocument.Parse(body);
                var root = jsonDoc.RootElement;
                Console.WriteLine($"root: {root}");

                // // Deserialize the event into a specific model and process it
                // var model = JsonConvert.DeserializeObject<MyModel>(body);

                _suspiciousBehaviorDetector.ProcessEvent(root, eventType);
                // Call the specific handler for the event
                // githubEvent.HandleEvent();

                return Ok("Webhook processed successfully");
            }
            catch (Exception ex)
            {
                // Log the error and return a failed response
                Console.WriteLine($"Error processing webhook: {ex.Message}");
                return BadRequest("Invalid webhook event");
            }
        }

        public bool VerifySignature(string payload, string receivedSignature)
        {
            var secret = _configuration["Webhook:Secret"];
            if (string.IsNullOrEmpty(secret))
            {
                return false;
            }

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                var computedSignature = "sha1=" + BitConverter.ToString(hash).Replace("-", "").ToLower();

                return computedSignature == receivedSignature;
            }
        }
    }
}