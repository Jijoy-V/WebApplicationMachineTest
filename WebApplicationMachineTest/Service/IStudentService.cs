using WebApplicationMachineTest.Models;
using WebApplicationMachineTest.Repository;
using WebApplicationMachineTest.VieModels;

namespace WebApplicationMachineTest.Service
{
    public interface IStudentService
    {

        
        User Authenticate(string username, string password);

        StudentRecordViewModel GetMyRecord(int userId);
        
    }
}
