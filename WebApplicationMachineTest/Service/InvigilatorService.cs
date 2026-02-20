using NuGet.Protocol.Core.Types;
using WebApplicationMachineTest.Models;
using WebApplicationMachineTest.Repository;
using WebApplicationMachineTest.VieModels;

namespace WebApplicationMachineTest.Service
{
    public class InvigilatorService : IInvigilatorService
    {
        private readonly IInvigilatorRepository _repository;

        public InvigilatorService(IInvigilatorRepository repository)
        {
            _repository = repository;
        }

        #region Login
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            return _repository.Login(username, password);
        }

        #endregion


        #region Add Student
        public (string RollNumber, string Username, string Password) CreateStudent(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required");

            if (fullName.Length > 30)
                throw new ArgumentException("Full name cannot exceed 30 characters");

            return _repository.AddStudent(fullName);
        }

        #endregion


        #region Delete Student (soft delete)
        public bool DeleteStudent(string rollNumber)
        {
            if (string.IsNullOrWhiteSpace(rollNumber))
                return false;

            _repository.SoftDeleteStudent(rollNumber);
            return true;
        }

        #endregion


        #region Add Student Records
        public bool CreateStudentRecord(string rollNumber, byte maths, byte physics,
                                        byte chemistry, byte english, byte programming)
        {
            if (!ValidateMarks(maths, physics, chemistry, english, programming))
                return false;

            _repository.AddStudentRecord(rollNumber, maths, physics, chemistry, english, programming);
            return true;
        }

        private bool ValidateMarks(byte maths, byte physics,
                                   byte chemistry, byte english, byte programming)
        {
            return maths >= 1 && maths <= 100 &&
                   physics >= 1 && physics <= 100 &&
                   chemistry >= 1 && chemistry <= 100 &&
                   english >= 1 && english <= 100 &&
                   programming >= 1 && programming <= 100;
        }

        #endregion


        #region Update Student Records
        public bool UpdateStudentRecord(string rollNumber, byte maths, byte physics,
                                byte chemistry, byte english, byte programming)
        {
            if (!ValidateMarks(maths, physics, chemistry, english, programming))
                return false;

            _repository.UpdateStudentRecord(rollNumber, maths, physics, chemistry, english, programming);
            return true;
        }

        #endregion


        #region Get Student Records
        public StudentRecordViewModel GetStudentRecord(string rollNumber)
        {
            if (string.IsNullOrWhiteSpace(rollNumber))
                return null;

            return _repository.GetStudentRecord(rollNumber);
        }

        #endregion


        #region Get All Student Records
        public List<StudentRecordViewModel> GetAllStudents()
        {
            return _repository.GetAllStudents();
        }

        #endregion
    }
}
