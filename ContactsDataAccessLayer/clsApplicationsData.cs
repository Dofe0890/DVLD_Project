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
    public class clsApplicationsData
    {
        public static bool FindApplication(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref double PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_FindApplication", connection))
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
                                ApplicantPersonID = (int)reader["ApplicantPersonID"];
                                ApplicationDate = (DateTime)reader["ApplicationDate"];
                                ApplicationTypeID = (int)reader["ApplicationTypeID"];
                                ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
                                LastStatusDate = (DateTime)reader["LastStatusDate"];
                                PaidFees = Convert.ToDouble(reader["PaidFees"]);
                                CreatedByUserID = (int)reader["CreatedByUserID"];





                            }
                            else
                            {
                                isFound = false;
                            }

                        }
                    }

                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                        isFound = false;
                    }

                    finally
                    {

                    }

                    return isFound;


                }
            }
        }

        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, double PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_InsertNewApplication", connection))
                {


                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("ApplicantPersonID", ApplicantPersonID);
                    command.Parameters.AddWithValue("ApplicationDate", ApplicationDate);
                    command.Parameters.AddWithValue("ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("ApplicationStatus", ApplicationStatus);
                    command.Parameters.AddWithValue("LastStatusDate", LastStatusDate);
                    command.Parameters.AddWithValue("PaidFees", PaidFees);
                    command.Parameters.AddWithValue("CreatedByUserID", CreatedByUserID);
                    SqlParameter output = new SqlParameter("@ApplicationID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(output);

                    try
                    {

                        connection.Open();
                        command.ExecuteNonQuery();


                        if (output.Value != DBNull.Value)
                        {
                            ApplicationID = Convert.ToInt32(output.Value);
                        }
                    }

                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }

                    finally
                    {
                    }

                    return ApplicationID;



                }
            }
        }

        public static bool UpdateApplicationStatus(int LocalDrivingLicenseApplicationID, byte ApplicationStatus)
        {
            int rowsAffcted = 0;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString);



            SqlCommand command = new SqlCommand("SP_UpdateApplicationStatus", connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);


            try
            {
                {
                    connection.Open();
                    rowsAffcted = command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                clsEventLogger.WriteEventLogger(ex);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffcted > 0);
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, double PaidFees, int CreatedByUserID)
        {
            int rowsAffcted = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateApplication", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                    command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                    command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



                    try
                    {
                        {
                            connection.Open();
                            rowsAffcted = command.ExecuteNonQuery();

                        }
                    }
                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                        return false;
                    }

                    finally
                    {
                    }

                    return (rowsAffcted > 0);
                }
            }
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsApplicationExis", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            isFound = reader.HasRows;
                        }

                    }
                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                        isFound = false;
                    }

                    finally
                    {
                    }
                    return isFound;
                }
            }
        }

        public static bool DosePersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {

            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);

        }

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetActiveApplication", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int AppID))
                        {
                            ActiveApplicationID = AppID;
                        }
                    }
                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                        return ActiveApplicationID;
                    }
                    finally
                    {
                    }

                    return ActiveApplicationID;
                }
            }
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetActiveApplicationIDForLicenseClass", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int AppID))
                        {
                            ActiveApplicationID = AppID;
                        }


                    }
                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                        return ActiveApplicationID;
                    }
                    finally
                    {
                    }
                    return ActiveApplicationID;

                }
            }
        }

        public static bool DeleteApplication(int ApplicationID)
        {


            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_DeleteApplication", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                    try
                    {
                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }
                    finally
                    {
                    }

                    return (rowsAffected > 0);

                }
            }
        }

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetAllApplications", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);

                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }
                    finally
                    {
                        connection.Close();
                    }

                    return dt;
                }
            }
        }

        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@NewStatus", NewStatus);


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

                    finally
                    {
                        connection.Close();
                    }

                    return (rowsAffected > 0);
                }
            }
        }
    }
}
