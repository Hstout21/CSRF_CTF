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
            return View(backend.GetForumPosts());
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return User.Identity.IsAuthenticated && User.IsInRole("Admin") ? View(backend.GetUsers()) : RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            backend.DeletePost(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(int id, string message)
        {
            backend.UpdatePost(id, message);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(string message)
        {
            backend.CreatePost(message, User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(int id)
        {
            backend.DeleteUser(id);
            return RedirectToAction("Admin", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUser(int id, string password)
        {
            backend.UpdateUser(id, password);
            return RedirectToAction("Admin", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(string email, string password)
        {
            backend.CreateUser(email, password);
            return RedirectToAction("Admin", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleUserAdmin(int id, bool isCurrentAdmin)
        {
            backend.ToggleUserAdmin(id, isCurrentAdmin);
            return RedirectToAction("Admin", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (backend.isUser(username, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, backend.isUserAdmin(username) ? "Admin" : "User")
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
