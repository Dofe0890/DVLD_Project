using PeopleDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Configuration;

namespace ContactsDataAccessLayer
{
    public class clsTestTypesData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllTestTypes", connection))
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

        public static bool FindTestType(int TestTypeID, ref string TestTypeTitle,ref string TestTypeDescription ,ref double TestTypeFees)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetTestType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {if (reader.Read())
                            {
                                isFound = true;
                                TestTypeTitle = (string)reader["TestTypeTitle"];
                                TestTypeDescription = (string)reader["TestTypeDescription"];
                                TestTypeFees = Convert.ToDouble(reader["TestTypeFees"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }

                    return isFound;
                }
            }
        }

        public static bool UpdateTestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription, double TestTypeFees)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateTestTypes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
                    command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
                    command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }

                    return (rowsAffected > 0);
                }
            }
        }

        public static int AddNewTestType(string Title, string Description, double Fees)
        {
            int TestTypeID = -1;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString);

            string query = @"Insert Into TestTypes (TestTypeTitle,TestTypeTitle,TestTypeFees)
                            Values (@TestTypeTitle,@TestTypeDescription,@ApplicationFees)
                            where TestTypeID = @TestTypeID;
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", Title);
            command.Parameters.AddWithValue("@TestTypeDescription", Description);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestTypeID = insertedID;
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


            return TestTypeID;

        }

     


    }
}
