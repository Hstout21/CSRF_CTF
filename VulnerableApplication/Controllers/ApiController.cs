using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using VulnerableApplication.Backend;
using VulnerableApplication.Controllers;

[ApiController]
[Route("api/admin")]
public class ApiController(IBackend _backend) : Controller
{
    private IBackend backend { get; set; } = _backend;

    [HttpPost("privilege")]
    public async Task<IActionResult> AdminPrivilege()
    {
        try
        {
            if (Request.Method != HttpMethods.Post) { return BadRequest(new { message = "Not a POST request" }); }

            if (Request.Headers.TryGetValue("Content-Type", out var contentType))
            {
                var allowedPrefixes = new[] { "application/x-www-form-urlencoded", "multipart/form-data", "text/plain" };
                if (!allowedPrefixes.Any(prefix => contentType.ToString().StartsWith(prefix, StringComparison.OrdinalIgnoreCase)))
                    return BadRequest(new { message = $"Content-Type '{contentType}' is not allowed in a simple request." });
            }
            else { return BadRequest(new { message = "Content Type Invalid." }); }

            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string requestBody = await reader.ReadToEndAsync();
                backend.ToggleUserAdmin(backend.GetUserId(requestBody), backend.isUserAdmin(requestBody));
                return Ok(new { message = "Request body read successfully", body = requestBody });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while reading the request body.", error = ex.Message });
        }
    }
}
