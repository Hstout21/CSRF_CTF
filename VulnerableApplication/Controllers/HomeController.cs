using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using VulnerableApplication.Backend;
using VulnerableApplication.Models;

namespace VulnerableApplication.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> logger { get; set; }
        private IBackend backend { get; set; }

        public HomeController(ILogger<HomeController> _logger, IBackend _backend)
        {
            logger = _logger;
            backend = _backend;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (backend.isUser(username, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, backend.RemoveDomainFromEmail(username)),
                    new Claim(ClaimTypes.Role, "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToAction("Index", "Home"); // Redirect to home or desired page
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
