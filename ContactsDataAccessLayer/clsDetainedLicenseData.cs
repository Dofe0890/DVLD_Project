using PeopleDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDataAccessLayer
{
    public class clsDetainedLicenseData
    {

        static public DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetAllDetainedLicenses", connection))
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

                    return dt;
                }
            }
        }

        static public bool FindDetain(int DetainID, ref int LicenseID, ref DateTime DetainDate, ref double FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetDetain", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DetainID", DetainID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                LicenseID = (int)reader["LicenseID"];
                                DetainDate = (DateTime)reader["DetainDate"];
                                FineFees = Convert.ToDouble(reader["FineFees"]);
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                IsReleased = (bool)reader["IsReleased"];

                                if (reader["ReleaseDate"] != DBNull.Value)
                                {
                                    ReleaseDate = (DateTime)reader["ReleaseDate"];
                                }
                                else
                                {
                                    ReleaseDate = DateTime.Now;
                                }

                                if (reader["ReleasedByUserID"] != DBNull.Value)
                                {
                                    ReleasedByUserID = (int)reader["ReleasedByUserID"];
                                }
                                else
                                {
                                    ReleasedByUserID = -1;
                                }
                                if (reader["ReleaseApplicationID"] != DBNull.Value)
                                {
                                    ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                                }
                                else
                                {
                                    ReleaseApplicationID = -1;
                                }

                            }
                        }

                    }

                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                        return false;
                    }


                    return IsFound;
                }
            }
        }

        static public bool FindDetainByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate, ref double FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetDetainByLicenseID", connection))
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
                                IsFound = true;
                                DetainID = (int)reader["DetainID"];
                                DetainDate = (DateTime)reader["DetainDate"];
                                FineFees = Convert.ToDouble(reader["FineFees"]);
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                IsReleased = (bool)reader["IsReleased"];

                                if (reader["ReleaseDate"] != DBNull.Value)
                                {
                                    ReleaseDate = (DateTime)reader["ReleaseDate"];
                                }
                                else
                                {
                                    ReleaseDate = DateTime.Now;
                                }

                                if (reader["ReleasedByUserID"] != DBNull.Value)
                                {
                                    ReleasedByUserID = (int)reader["ReleasedByUserID"];
                                }
                                else
                                {
                                    ReleasedByUserID = -1;
                                }
                                if (reader["ReleaseApplicationID"] != DBNull.Value)
                                {
                                    ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                                }
                                else
                                {
                                    ReleaseApplicationID = -1;
                                }
                            }

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

                    return IsFound;
                }
            }
        }

        static public int AddNewDetain(int LicenseID, DateTime DetainDate, double FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int DetainID = -1;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_InsertNewDetain", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);
                    command.Parameters.AddWithValue("@DetainDate", DetainDate);
                    command.Parameters.AddWithValue("@FineFees", FineFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@IsReleased", IsReleased);
                    if (ReleaseDate != null)
                    {
                        command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ReleaseDate", System.DBNull.Value);
                    }

                    if (ReleasedByUserID != null && ReleasedByUserID != -1)
                    {
                        command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ReleasedByUserID", System.DBNull.Value);
                    }

                    if (ReleaseApplicationID != null && ReleaseApplicationID != -1)
                    {
                        command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ReleaseApplicationID", System.DBNull.Value);
                    }
                    SqlParameter output = new SqlParameter("@DetainID", SqlDbType.Int)
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
                            DetainID = Convert.ToInt32(output.Value);
                        }

                    }
                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }

                    return DetainID;



                }
            }
        }

        static public bool UpdateDetain(int DetainID, int LicenseID, DateTime DetainDate, double FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = -1;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateDetain", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DetainID", DetainID);
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);
                    command.Parameters.AddWithValue("@DetainDate", DetainDate);
                    command.Parameters.AddWithValue("@FineFees", FineFees);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@IsReleased", IsReleased);
                    if (ReleaseDate != null)
                    {
                        command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ReleaseDate", System.DBNull.Value);
                    }

                    if (ReleasedByUserID != null && ReleasedByUserID != -1)
                    {
                        command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ReleasedByUserID", System.DBNull.Value);
                    }

                    if (ReleaseApplicationID != null && ReleaseApplicationID != -1)
                    {
                        command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ReleaseApplicationID", System.DBNull.Value);
                    }
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

                    return (rowsAffected > 0);
                }
            }
        }

        static public bool ReleaseDetainedLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_ReleaseDetainedLicense", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DetainID", DetainID);
                    command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                    command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

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


                    return (rowsAffected > 0);
                }
            }
        }

        static public bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {



                using (SqlCommand command = new SqlCommand("SP_IsLicenseDetained", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LicenseID", LicenseID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            IsDetained = Convert.ToBoolean(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }

                    return IsDetained;
                }
            }
        }
    }
}
