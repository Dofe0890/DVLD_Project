using ContactsDataAccessLayer;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PeopleDataAccessLayer
{
    public class clsCountryData
    {
        public static bool GetCountryInfoByID(int ID, ref string CountryName)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetCountryInfoByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@CountryID", ID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {


                                isFound = true;

                                CountryName = (string)reader["CountryName"];



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

        public static bool GetCountryInfoByName(string CountryName, ref int ID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetCountryInfoByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CountryName", CountryName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                isFound = true;

                                ID = (int)reader["CountryID"];



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

        public static DataTable GetAllCountries()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllCountries", connection))
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

 

    }
}
