using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PeopleDataAccessLayer;
using System.ComponentModel.Design;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Configuration;
using ContactsDataAccessLayer;

namespace UsersDataAccessLayer
{
    public class clsUsersData
    {

        public static bool FindUser(string Username, string Password, ref int UserID, ref int PersonID, ref bool isActive)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetUserByUsername", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", Username);
                    command.Parameters.AddWithValue("@Password", Password);
                    bool isFound = false;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;


                                UserID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                isActive = (bool)reader["IsActive"];

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

        public static bool FindUser(int UserID, ref string Username, ref string Password, ref int PersonID, ref bool isActive)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", UserID);
                    bool isFound = false;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;


                                Username = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                PersonID = (int)reader["PersonID"];
                                isActive = (bool)reader["IsActive"];

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

        public static bool FindUsernameAndPassword(string UserName, string Password, ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
              

                using (SqlCommand command = new SqlCommand("SP_GetUserByUsername", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", UserName);
                    command.Parameters.AddWithValue("@Password", Password);


                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                UserID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];


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

        public static bool FindUserByPersonID(int PersonID, ref int UserID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_FindPersonByID", connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                isFound = true;

                                UserID = (int)reader["UserID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];


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

        public static DataTable GetAllUsers()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {
        
                using (SqlCommand command = new SqlCommand("SP_GetAllUsers", connection))
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

        public static int AddNewUser(string UserName, string Password, int PersonID, bool isActive)
        {
            int UserID = -1;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_InsertNewUser", connection))
                {
                    command .CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@IsActive", isActive);


                    SqlParameter outputParam = new SqlParameter("@UserID", SqlDbType.Int)
                    {

                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();

                        if(outputParam .Value != DBNull.Value )
                        {
                            UserID = Convert.ToInt32(outputParam.Value);
                        }
                    }

                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }

                    finally
                    {
                     
                    }

                    return UserID;
                }
            }
        }

        public static bool UpdateUser(int ID, string UserName, string Password, int PersonID, bool isActive)
        {

            int rowsAffcted = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", ID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@IsActive", isActive);

                    try
                    {
                        connection.Open();
                        rowsAffcted = command.ExecuteNonQuery();


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

        public static bool DeleteUser(int UserID)

        {

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsUserExistByID", connection))
                {
                    command.CommandType= CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("UserID", UserID);

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

        public static bool IsUserExist(string UserName)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsUserExistByUsername", connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.CommandType = CommandType.StoredProcedure;
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

        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsPersonExist", connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command .CommandType = CommandType.StoredProcedure;
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
      
    }
}
