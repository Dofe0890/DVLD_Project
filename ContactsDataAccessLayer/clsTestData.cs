using PeopleDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ContactsDataAccessLayer
{
   public class clsTestData
    {


        public static bool GetTest(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetTest", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestID", TestID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        isFound = true;
                        TestAppointmentID = (int)reader["TestAppointmentID"];
                        TestResult = (bool)reader["TestResult"];
                        Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : "";
                        CreatedByUserID = (int)reader["CreatedByUserID"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return isFound;
        }

        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID,int LicenseClassID ,int TestTypeID, ref int TestID,ref int TestAppointment,ref bool TestResult, ref string Notes, ref int CreatedUserID)
        {
          bool isFound = false;

    string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        using (SqlCommand command = new SqlCommand("SP_GetLastTestByPersonAndTestTypeAndLicenseClass", connection))
        {
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        isFound = true;

                        TestID = (int)reader["TestID"];
                        TestAppointment = (int)reader["TestAppointmentID"];
                        TestResult = (bool)reader["TestResult"];
                        Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : "";
                        CreatedUserID = (int)reader["CreatedByUserID"];
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.WriteEventLogger(ex);
                isFound = false;
            }
        }
    }

    return isFound;
        }

        public static int AddNewTest( int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID )
        {
            int TestID = -1;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_InsertNewTest", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);
                command.Parameters.AddWithValue("@Notes", Notes);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                SqlParameter outputID = new SqlParameter("@TestID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputID);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    if (outputID.Value != DBNull.Value)
                    {
                        TestID = Convert.ToInt32(outputID.Value);
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return TestID;
        }

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateTest", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestID", TestID);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);
                command.Parameters.AddWithValue("@Notes", Notes);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                    return false;
                }
            }
            return rowsAffected > 0;
        }

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllTests", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return dt;
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetPassedTestCount", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                    {
                        PassedTestCount = ptCount;
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return PassedTestCount;
        }


    }
}
