using PeopleDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDataAccessLayer
{
    public class clsDriversData
    {
      

        static public DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString);
            string query = @"Select * from Drivers_View;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.WriteEventLogger(ex);
            }

            finally
            {
                connection .Close();
            }

            return dt;

        }

        static public bool FindDriver(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetDriver", connection))
                {

                    bool isFound = false;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DriverID", DriverID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                PersonID = (int)reader["PersonID"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CreatedDate = (DateTime)reader["CreatedDate"];
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

                    return isFound;
                }
            }
        }

        static public bool FindDriverByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetDriverByPersonID", connection))
                {

                    bool isFound = false;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                DriverID = (int)reader["DriverID"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CreatedDate = (DateTime)reader["CreatedDate"];
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

                    return isFound;
                }
            }
        }

        static public int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int DriverID = -1;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_InsertNewDriver", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                    SqlParameter output = new SqlParameter("@DriverID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(output);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                       if(output.Value != DBNull.Value)
                        {
                            DriverID = Convert.ToInt32(output.Value);
                        }


                    }
                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }

                    return DriverID;
                }
            }
        }

        static public bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {

            int rowsAffected = -1;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateDriver", connection))
                {
                    command.Parameters.AddWithValue("DriverID", DriverID);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

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
    }
}
