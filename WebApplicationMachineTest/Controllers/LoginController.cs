using Microsoft.AspNetCore.Mvc;
using WebApplicationMachineTest.Service;
using WebApplicationMachineTest.VieModels;

namespace WebApplicationMachineTest.Controllers
{
    public class LoginController : Controller
    {
        private readonly IStudentService _studentService;

        public LoginController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // ================= LOGIN PAGE =================
        public IActionResult Login()
        {
            return View();
        }

        // ================= LOGIN POST =================
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _studentService.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid Username or Password";
                return View(model);
            }

            // Store session values
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("RoleId", user.RoleId.ToString());
            HttpContext.Session.SetString("Username", user.Username);

            // 🔥 ROLE BASED REDIRECT
            if (user.RoleId == 1) // Invigilator
            {
                return RedirectToAction("Index", "Invigilators");
            }
            else if (user.RoleId == 2) // Student
            {
                return RedirectToAction("Index", "Students");
            }

            // fallback
            return RedirectToAction("Login");
        }

        // ================= LOGOUT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
