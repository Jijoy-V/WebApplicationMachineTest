using NuGet.Protocol.Core.Types;
using WebApplicationMachineTest.Models;
using WebApplicationMachineTest.Repository;
using WebApplicationMachineTest.VieModels;

namespace WebApplicationMachineTest.Service
{
    public class StudentService : IStudentService
    {
        // Service layer that handles business logic and interacts with the repository
        private readonly IStudentRepository _studentRepository;

        // Dependency injection 
        public StudentService(IStudentRepository studentRepository)
        {
            studentRepository = _studentRepository;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            return _studentRepository.Login(username, password);
        }

        public StudentRecordViewModel GetMyRecord(int userId)
        {
            return _studentRepository.GetStudentRecordByUserId(userId);
        }

    }
}
