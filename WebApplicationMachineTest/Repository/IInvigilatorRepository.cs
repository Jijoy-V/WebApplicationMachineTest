using WebApplicationMachineTest.Models;
using WebApplicationMachineTest.VieModels;

namespace WebApplicationMachineTest.Repository
{
    public interface IInvigilatorRepository
    {
        User Login(string username, string password);

        (string RollNumber, string Username, string Password) AddStudent(string fullName);

        void SoftDeleteStudent(string rollNumber);

        void AddStudentRecord(string rollNumber, byte maths, byte physics,
                              byte chemistry, byte english, byte programming);

        void UpdateStudentRecord(string rollNumber, byte maths, byte physics,
                                 byte chemistry, byte english, byte programming);

        StudentRecordViewModel GetStudentRecord(string rollNumber);

        List<StudentRecordViewModel> GetAllStudents();

    }
}
