using WebApplicationMachineTest.Models;
using WebApplicationMachineTest.VieModels;

namespace WebApplicationMachineTest.Service
{
    public interface IInvigilatorService 
    {
        User Authenticate(string username, string password);

        (string RollNumber, string Username, string Password) CreateStudent(string fullName);

        bool DeleteStudent(string rollNumber);

        bool CreateStudentRecord(string rollNumber, byte maths, byte physics,
                                 byte chemistry, byte english, byte programming);

        bool UpdateStudentRecord(string rollNumber, byte maths, byte physics,
                                 byte chemistry, byte english, byte programming);

        StudentRecordViewModel GetStudentRecord(string rollNumber);

        List<StudentRecordViewModel> GetAllStudents();
    }
}
