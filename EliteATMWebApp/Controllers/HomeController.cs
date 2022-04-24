using EliteATMWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EliteATMWebApp.Data;

namespace EliteATMWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EliteATMDBContext _EliteATMDBContext;
        
        public HomeController(ILogger<HomeController> logger, EliteATMDBContext context)
        {
            _logger = logger;
            _EliteATMDBContext = context;
        }
        
        public IActionResult Index([Bind("Email,Password")] User? user)
        {
            if(user?.Email == null || user.Password == null)
            {
                return View();
            }
            else
            {
                try
                {
                    UserTracker.User = _EliteATMDBContext.Users.First(m => m.Email == user.Email && m.Password == user.Password);
                    return View();
                }
                catch (InvalidOperationException) { return RedirectToAction("Login", "Home", new {FailedLogin = true} ); }
            }
            
            
        }
        [ActionName("index-contact")]
        public IActionResult Index([Bind("Email,Subject,Comments")] Contact c)
        {
            ViewBag.Contact = c;
            return View("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login(bool? FailedLogin)
        {
            ViewBag.FL = FailedLogin;
            return View();
        }
        public IActionResult LogOut()
        {
            UserTracker.User = null;
            return RedirectToAction(nameof(HomeController.Index));
        }
        public IActionResult Contact()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}