using Microsoft.Data.SqlClient;
using System.Data;
using WebApplicationMachineTest.Models;
using WebApplicationMachineTest.VieModels;

namespace WebApplicationMachineTest.Repository
{
    public class InvigilatorRepository : IInvigilatorRepository
    {
        private readonly string _connectionString;

        public InvigilatorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnStrMVC");
        }


        #region Login
        public User Login(string username, string password)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Users WHERE Username=@Username AND PasswordHash=@Password AND IsActive=1",
                    conn);

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Username = reader["Username"].ToString(),
                        RoleId = Convert.ToInt32(reader["RoleId"])
                    };
                }
            }

            return user;
        }

        #endregion


        #region Add Student

        public (string RollNumber, string Username, string Password) AddStudent(string fullName)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_AddStudent", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FullName", fullName);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            string roll = "", user = "", pass = "";

            if (reader.Read())
            {
                roll = reader["RollNumber"].ToString();
                user = reader["Username"].ToString();
                pass = reader["Password"].ToString();
            }

            return (roll, user, pass);
        }

        #endregion


        #region Soft Delete Student

        public void SoftDeleteStudent(string rollNumber)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_SoftDeleteStudent", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RollNumber", rollNumber);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        #endregion


        #region Add Student Record

        public void AddStudentRecord(string rollNumber, byte maths, byte physics,
                             byte chemistry, byte english, byte programming)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_AddStudentRecord", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RollNumber", rollNumber);
            cmd.Parameters.AddWithValue("@Maths", maths);
            cmd.Parameters.AddWithValue("@Physics", physics);
            cmd.Parameters.AddWithValue("@Chemistry", chemistry);
            cmd.Parameters.AddWithValue("@English", english);
            cmd.Parameters.AddWithValue("@Programming", programming);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        #endregion


        #region Update Student Record

        public void UpdateStudentRecord(string rollNumber, byte maths, byte physics,
                                byte chemistry, byte english, byte programming)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_UpdateStudentRecord", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RollNumber", rollNumber);
            cmd.Parameters.AddWithValue("@Maths", maths);
            cmd.Parameters.AddWithValue("@Physics", physics);
            cmd.Parameters.AddWithValue("@Chemistry", chemistry);
            cmd.Parameters.AddWithValue("@English", english);
            cmd.Parameters.AddWithValue("@Programming", programming);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        #endregion


        #region View Single Student Record

        public StudentRecordViewModel GetStudentRecord(string rollNumber)
        {
            StudentRecordViewModel record = null;

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ViewStudentRecord", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RollNumber", rollNumber);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                record = new StudentRecordViewModel
                {
                    FullName = reader["FullName"].ToString(),
                    RollNumber = reader["RollNumber"].ToString(),
                    Maths = Convert.ToByte(reader["Maths"]),
                    Physics = Convert.ToByte(reader["Physics"]),
                    Chemistry = Convert.ToByte(reader["Chemistry"]),
                    English = Convert.ToByte(reader["English"]),
                    Programming = Convert.ToByte(reader["Programming"]),
                    LastUpdated = Convert.ToDateTime(reader["LastUpdated"])
                };
            }

            return record;
        }

        #endregion


        #region View All Student Records

        public List<StudentRecordViewModel> GetAllStudents()
        {
            List<StudentRecordViewModel> list = new();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ViewAllStudents", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new StudentRecordViewModel
                {
                    FullName = reader["FullName"].ToString(),
                    RollNumber = reader["RollNumber"].ToString(),
                    Maths = Convert.ToByte(reader["Maths"]),
                    Physics = Convert.ToByte(reader["Physics"]),
                    Chemistry = Convert.ToByte(reader["Chemistry"]),
                    English = Convert.ToByte(reader["English"]),
                    Programming = Convert.ToByte(reader["Programming"])
                });
            }

            return list;
        }

        #endregion

    }
}
