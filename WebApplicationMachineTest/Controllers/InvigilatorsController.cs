using Microsoft.AspNetCore.Mvc;
using WebApplicationMachineTest.Service;
using WebApplicationMachineTest.VieModels;

namespace WebApplicationMachineTest.Controllers
{
    public class InvigilatorsController : Controller
    {
        private readonly IInvigilatorService _invigilatorService;

        public InvigilatorsController(IInvigilatorService invigilatorService)
        {
            _invigilatorService = invigilatorService;
        }

        

        // ================= DASHBOARD =================
        public IActionResult Index()
        {
            

            var students = _invigilatorService.GetAllStudents();
            return View(students);
        }

        // ================= ADD STUDENT =================
        public IActionResult AddStudent()
        {
            

            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(AddStudentViewModel model)
        {
            

            if (!ModelState.IsValid)
                return View(model);

            var result = _invigilatorService.CreateStudent(model.FullName);

            ViewBag.RollNumber = result.RollNumber;
            ViewBag.Username = result.Username;
            ViewBag.Password = result.Password;

            return View("StudentCreated");
        }

        // ================= ADD RECORD =================
        [HttpPost]
        public IActionResult AddRecord(string rollNumber,
                                       byte maths, byte physics,
                                       byte chemistry, byte english,
                                       byte programming)
        {

            _invigilatorService.CreateStudentRecord(
                rollNumber, maths, physics,
                chemistry, english, programming);

            return RedirectToAction("Index");
        }

        // ================= EDIT RECORD =================
        [HttpPost]
        public IActionResult EditRecord(string rollNumber,
                                        byte maths, byte physics,
                                        byte chemistry, byte english,
                                        byte programming)
        {
            

            _invigilatorService.UpdateStudentRecord(
                rollNumber, maths, physics,
                chemistry, english, programming);

            return RedirectToAction("Index");
        }

        // ================= DELETE STUDENT =================
        [HttpPost]
        public IActionResult Delete(string rollNumber)
        {
            

            _invigilatorService.DeleteStudent(rollNumber);

            return RedirectToAction("Index");
        }

        // ================= VIEW SINGLE STUDENT =================
        public IActionResult Details(string rollNumber)
        {
            

            var record = _invigilatorService.GetStudentRecord(rollNumber);
            return View(record);
        }

        // ================= LOGOUT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
