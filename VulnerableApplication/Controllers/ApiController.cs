using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

[ApiController]
[Route("api/admin")]
public class ApiController : Controller
{
    [HttpPost("privilege")]
    public IActionResult DetectSimpleRequest()
    {
        // Check if the method is POST
        if (Request.Method != HttpMethods.Post)
        {
            return BadRequest(new { message = "Not a POST request" });
        }

        // Check for allowed headers
        var allowedHeaders = new[] { "Accept", "Accept-Language", "Content-Language", "Content-Type" };
        foreach (var header in Request.Headers)
        {
            if (!allowedHeaders.Contains(header.Key, StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = $"Header '{header.Key}' is not allowed in a simple request." });
            }
        }

        // Check if Content-Type is allowed for a simple request
        if (Request.Headers.TryGetValue("Content-Type", out StringValues contentType))
        {
            var allowedContentTypes = new[]
            {
                "application/x-www-form-urlencoded",
                "multipart/form-data",
                "text/plain"
            };
            if (!allowedContentTypes.Contains(contentType.ToString(), StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "Content-Type is not allowed in a simple request." });
            }
        }

        // If all checks pass
        return Ok(new { message = "This is a simple request." });
    }
}
