using PeopleDataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDataAccessLayer
{
    public class clsLicensesData
    {

        public static bool FindLicense(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref double PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetLicenseByID", connection))
                {


                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                isFound = true;


                                if (reader["Notes"] != DBNull.Value)
                                {
                                    Notes = (string)reader["Notes"];
                                }
                                else
                                {
                                    Notes = "";
                                }

                                ApplicationID = (int)reader["ApplicationID"];
                                DriverID = (int)reader["DriverID"];
                                LicenseClass = (int)reader["LicenseClass"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                ExpirationDate = (DateTime)reader["ExpirationDate"];
                                PaidFees = Convert.ToDouble(reader["PaidFees"]);
                                IsActive = (bool)reader["isActive"];
                                IssueReason = (byte)reader["IssueReason"];
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

        public static bool FindLicenseByApp(int ApplicationID, ref int LicenseID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref double PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString);


            SqlCommand command = new SqlCommand("SP_GetLicenseByApplication", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;


                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
                    }

                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    PaidFees = Convert.ToDouble(reader["PaidFees"]);
                    IsActive = (bool)reader["isActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];





                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                clsEventLogger.WriteEventLogger(ex);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, double PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_InsertNewLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@DriverID", DriverID);

                    command.Parameters.AddWithValue("@IssueReason", IssueReason);
                    command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@IsActive", IsActive);

                    if (Notes != "" && Notes != null)
                        command.Parameters.AddWithValue("@Notes", Notes);
                    else
                        command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

                    SqlParameter Output = new SqlParameter("@LicenseID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output,

                    };
                    command.Parameters.Add(Output);



                    try
                    {
                        connection.Open();


                        command.ExecuteNonQuery();

                        if (Output.Value != DBNull.Value)
                        {
                            LicenseID = Convert.ToInt32(Output.Value);
                        }


                    }

                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }

                    finally
                    {

                    }

                    return LicenseID;
                }
            }
        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, double PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateLicense", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@DriverID", DriverID);
                    if (Notes != "" && Notes != null)
                        command.Parameters.AddWithValue("@Notes", Notes);
                    else
                        command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

                    command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@IssueReason", IssueReason);
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

                    finally
                    {

                    }

                    return (rowsAffected > 0);
                }
            }
        }

        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetAllLicenses", connection))
                {
                    try
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
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
                    }
                    return dt;

                }
            }
        }

        public static DataTable GetDriversLicense(int DriverID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetLicensesByDriverID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DriverID", DriverID);

                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
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
                    }

                    return dt;

                }
            }
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetActiveLicenseIDByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();


                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            LicenseID = insertedID;
                        }

                    }

                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }

                    finally
                    {

                    }


                    return LicenseID;

                }
            }
        }

        public static bool DeactivateLicense(int LicenseID)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {



                using (SqlCommand command = new SqlCommand("SP_DeactivateLicense", connection))
                {

                    command.Parameters.AddWithValue("@LicenseID", LicenseID);
                    command.CommandType = CommandType.StoredProcedure;

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
                    }

                    return (rowsAffected > 0);



                }
            }
        }
    }
}
