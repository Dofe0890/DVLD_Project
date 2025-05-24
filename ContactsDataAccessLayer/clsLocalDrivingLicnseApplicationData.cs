using PeopleDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ContactsDataAccessLayer
{
    public class clsLocalDrivingLicnseApplicationData
    {
        public static int AddLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int newID = -1;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_InsertNewLocalDrivingLicenseApplication", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                SqlParameter outputIdParam = new SqlParameter("@NewID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    newID = (int)outputIdParam.Value;
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return newID;
        }
        
        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateLocalDrivingLicenseApplication", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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
     
        public static bool FindLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID ,ref int ApplicationID,ref int LicenseClassID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetLocalDrivingLicenseApplication", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            ApplicationID = (int)reader["ApplicationID"];
                            LicenseClassID = (int)reader["LicenseClassID"];
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
       
        public static  bool FindLocalDrivingLicenseApplicationByApplicationID(int ApplicationID, ref int LocalDrivingLicenseApplicationID,ref int LicenseClassID)                   
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetLocalDrivingLicenseApplicationByApplicationID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                            LicenseClassID = (int)reader["LicenseClassID"];
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
      
        public static bool DoesPassTestType(int LocalDrivingLicenseApplication , int TestTypeID)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DoesPassTestType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplication);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    connection.Open();
                    object returnValue = command.ExecuteScalar();
                    if (returnValue != null && bool.TryParse(returnValue.ToString(), out bool parsedResult))
                    {
                        result = parsedResult;
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return result;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllLocalDrivingLicenseApplications", connection))
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

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeleteLocalDrivingLicenseApplication", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DoesAttendTestType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    connection.Open();
                    object returnValue = command.ExecuteScalar();
                    if (returnValue != null)
                    {
                        isFound = true;
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return isFound;

        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {
            byte totalTrials = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_TotalTrialsPerTest", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    connection.Open();
                    object returnValue = command.ExecuteScalar();
                    if (returnValue != null && byte.TryParse(returnValue.ToString(), out byte parsedResult))
                    {
                        totalTrials = parsedResult;
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return totalTrials;

        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_IsThereAnActiveScheduledTest", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    connection.Open();
                    object returnValue = command.ExecuteScalar();
                    if (returnValue != null)
                    {
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    clsEventLogger.WriteEventLogger(ex);
                }
            }
            return result;


        }



         


    }
}
