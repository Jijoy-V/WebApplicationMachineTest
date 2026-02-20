using Microsoft.Data.SqlClient;
using System.Data;
using WebApplicationMachineTest.Models;
using WebApplicationMachineTest.VieModels;

namespace WebApplicationMachineTest.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
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


        #region Get Student Record
        public StudentRecordViewModel GetStudentRecordByUserId(int userId)
        {
            StudentRecordViewModel record = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetStudentRecordByUserId", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
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
                }
            }

            return record;
        }

        #endregion
    }
}
