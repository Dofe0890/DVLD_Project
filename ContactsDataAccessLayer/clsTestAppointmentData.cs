using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PeopleDataAccessLayer;
using System.Data;
using System.Configuration;
namespace ContactsDataAccessLayer
{
    public  class clsTestAppointmentData
    { 
        
        public static DataTable GetAllAppointments( )
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllTestAppointments", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return dt;
        }

        public static DataTable GetApplicationTestAppointmentPerTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetApplicationTestAppointmentPerTestType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return dt;
        }

        public static bool FindAppointment(int TestAppointmentID,
          ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
          ref DateTime AppointmentDate, ref double  PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAppointmentByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            TestTypeID = (int)reader["TestTypeID"];
                            LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                            AppointmentDate = (DateTime)reader["AppointmentDate"];
                            PaidFees = Convert.ToDouble(reader["PaidFees"]);
                            CreatedByUserID = (int)reader["CreatedByUserID"];
                            IsLocked = (bool)reader["IsLocked"];
                            RetakeTestApplicationID = reader["RetakeTestApplicationID"] != DBNull.Value ? (int)reader["RetakeTestApplicationID"] : -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return isFound;
        }

        public static bool FindLastTestAppointment(int LocalDrivingLicenseApplicationID,  int TestTypeID,ref int TestAppointmentID ,ref DateTime AppointmentDate,ref double PaidFees,ref int CreatedUserID,ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetLastTestAppointment", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            TestAppointmentID = (int)reader["TestAppointmentID"];
                            AppointmentDate = (DateTime)reader["AppointmentDate"];
                            PaidFees = Convert.ToDouble(reader["PaidFees"]);
                            CreatedUserID = (int)reader["CreatedByUserID"];
                            IsLocked = (bool)reader["IsLocked"];
                            RetakeTestApplicationID = reader["RetakeTestApplicationID"] != DBNull.Value ? (int)reader["RetakeTestApplicationID"] : -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return isFound;
        }

        public static int AddNewAppointment( int TestTypeID, int LocalDrivingLicenseApplicationID,DateTime AppointmentDate, double PaidFees,int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_InsertNewAppointment", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@IsLocked", IsLocked);
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID == -1 ? (object)DBNull.Value : RetakeTestApplicationID);
                SqlParameter output = new SqlParameter("@AppointmentID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                   
                   command.Parameters.Add(output);
                
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                   if(output .Value != DBNull .Value )
                    {
                        TestAppointmentID =Convert.ToInt32 (output.Value);
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return TestAppointmentID;
        }

        public static bool UpdateAppointment(int TestAppointmentID,int TestTypeID,int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, double PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateAppointment", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@IsLocked", IsLocked);
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID == -1 ? (object)DBNull.Value : RetakeTestApplicationID);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return rowsAffected > 0;
        }

        public static int GetTestID (int TestAppointmentID)
        {
            int TestID = -1;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetTestIDByAppointment", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        TestID = id;
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return TestID;
        }

    }
}
