using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

[ApiController]
[Route("api/admin")]
public class ApiController : Controller
{
    [HttpPost("privilege")]
    public async Task<IActionResult> AdminPrivilege()
    {
        // Check if the method is POST
        if (Request.Method != HttpMethods.Post)
        {
            return BadRequest(new { message = "Not a POST request" });
        }

        if (Request.Headers.TryGetValue("Content-Type", out var contentType))
        {
            var allowedPrefixes = new[] { "application/x-www-form-urlencoded", "multipart/form-data", "text/plain" };

            // Check if the Content-Type starts with any allowed prefix
            if (!allowedPrefixes.Any(prefix => contentType.ToString().StartsWith(prefix, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest(new { message = $"Content-Type '{contentType}' is not allowed in a simple request." });
            }
        }
        else { return BadRequest(new { message = "Content Type Invalid." }); }

        try
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                // Read the body content as a string
                string requestBody = await reader.ReadToEndAsync();

                // Return the body content in the response
                return Ok(new { message = "Request body read successfully", body = requestBody });
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that might occur
            return StatusCode(500, new { message = "An error occurred while reading the request body.", error = ex.Message });
        }
    }
}
