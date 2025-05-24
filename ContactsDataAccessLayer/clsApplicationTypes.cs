using PeopleDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDataAccessLayer
{
    public class clsApplicationTypesData
    {
        public static DataTable GetAllApplicationTypes()
        {
         
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllApplicationTypes", connection))
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

                    }
                    return dt;
                }
            }
        }

        public static bool FindApplicationType(int ApplicationTypeID, ref string ApplicationTypeTitle, ref double ApplicationTypeFees)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetApplicationType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    bool isFound = false;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                isFound = true;


                                ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                                ApplicationTypeFees = Convert.ToDouble(reader["ApplicationFees"]);


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

        public static bool UpdateApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, double ApplicationTypeFees)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_UpdateApplicationType", connection))
                {
                    command.CommandType= CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
                    command.Parameters.AddWithValue("@ApplicationFees", Convert.ToSingle(ApplicationTypeFees));

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

        public static int AddNewApplicationType(string Title, double Fees)
        {
            int ApplicationTypeID = -1;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString);

            string query = @"Insert Into ApplicationTypes (ApplicationTypeTitle,ApplicationFees)
                            Values (@Title,@Fees)
                            
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationTypeID = insertedID;
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


            return ApplicationTypeID;

        }

    }
}
