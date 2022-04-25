using EliteATMWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EliteATMWebApp.Data;
using System.Net.Mail;
using System.Net;

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

        [ActionName("contact-sent")]
        public IActionResult Contact([Bind("FirstName,LastName,Email,Subject,Comments")] Contact c)
        {
            if (ModelState.IsValid)
            {
                var senderEmail = new MailAddress("etileatmservice@gmail.com", "Elite ATM Email Service");
                var receiverEmail = new MailAddress("elliotmorrison58@gmail.com", "Receiver");
                var password = "\\9?^TDe{\\2";
                var sub = $"Feedback: {c.Subject}";
                var body = $"<p><b>Name:</b> {c.getFullName()}</p>" +
                    $"<p><b>Email:</b> {c.Email}</p>" +
                    $"<p><b>Subject:</b> {c.Subject}</p>" +
                    $"<p><br><b>Body:</b> {c.Comments}</p>";
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = senderEmail;
                    mail.To.Add(receiverEmail);
                    mail.Subject = sub;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    //mail.Attachments.Add(new Attachment("wwwroot\\Images\\Business.jpg"));
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(senderEmail.Address, password);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                //ModelState.Clear();
                ViewBag.FeedBackSent = true;
                return View("Contact");
            }
            else
            {
                return Error();
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}