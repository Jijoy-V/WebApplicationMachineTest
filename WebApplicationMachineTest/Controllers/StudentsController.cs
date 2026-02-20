using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMachineTest.Service;
using WebApplicationMachineTest.VieModels;

namespace WebApplicationMachineTest.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // ================= LOGIN =================
        public IActionResult Login()
        {
            return View();
        }

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

            if (user.RoleId != 2) // 2 = Student
            {
                ViewBag.Error = "Access Denied";
                return View(model);
            }

            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("RoleId", user.RoleId.ToString());

            return RedirectToAction("MyRecord");
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("RoleId") != "2")
                return RedirectToAction("Login", "Login");

            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var record = _studentService.GetMyRecord(userId);

            return View(record);
        }

        // ================= VIEW OWN RECORD =================
        public IActionResult MyRecord()
        {
            string userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login");

            int userId = Convert.ToInt32(userIdString);

            var record = _studentService.GetMyRecord(userId);

            if (record == null)
                return View("NotFound");

            return View(record);
        }

        // ================= LOGOUT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
