using WebApplicationMachineTest.Models;
using WebApplicationMachineTest.VieModels;

namespace WebApplicationMachineTest.Repository
{
    public interface IStudentRepository
    {
        User Login(string username, string password);

        StudentRecordViewModel GetStudentRecordByUserId(int userId);

        
    }

}
