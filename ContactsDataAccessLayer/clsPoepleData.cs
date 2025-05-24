using ContactsDataAccessLayer;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PeopleDataAccessLayer
{
    public class clsPeopleDataAccess
    {

        public static bool GetPersonInfoByID(int PersonID, ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName, ref string NationalNo, ref DateTime DateOfBirth,
         ref short Gendor, ref string Address, ref string Phone, ref string Email,
         ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetPersonByID", connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                isFound = true;

                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];

                                if (reader["ThirdName"] != DBNull.Value)
                                {
                                    ThirdName = (string)reader["ThirdName"];
                                }
                                else
                                {
                                    ThirdName = "";
                                }

                                LastName = (string)reader["LastName"];
                                NationalNo = (string)reader["NationalNo"];
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Gendor = (byte)reader["Gendor"];
                                Address = (string)reader["Address"];
                                Phone = (string)reader["Phone"];


                                if (reader["Email"] != DBNull.Value)
                                {
                                    Email = (string)reader["Email"];
                                }
                                else
                                {
                                    Email = "";
                                }

                                NationalityCountryID = (int)reader["NationalityCountryID"];

                                if (reader["ImagePath"] != DBNull.Value)
                                {
                                    ImagePath = (string)reader["ImagePath"];
                                }
                                else
                                {
                                    ImagePath = "";
                                }

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
        public static bool GetPersonInfoByNational(string NationalNo, ref int ID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
       ref byte Gendor, ref string Email, ref string Phone, ref string Address,
        ref DateTime DateOfBirth, ref int NationalCountryID, ref string ImagePath)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetPersonByNationalNo", connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                isFound = true;
                                ID = (int)reader["PersonID"];
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];

                                if (reader["ThirdName"] != DBNull.Value)
                                {
                                    ThirdName = (string)reader["ThirdName"];
                                }
                                else
                                {
                                    ThirdName = "";
                                }


                                LastName = (string)reader["LastName"];
                                Gendor = (byte)reader["Gendor"];
                                Phone = (string)reader["Phone"];
                                Address = (string)reader["Address"];
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                NationalCountryID = (int)reader["NationalityCountryID"];


                                if (reader["Email"] != DBNull.Value)
                                {
                                    Email = (string)reader["Email"];
                                }
                                else
                                {
                                    Email = "";

                                }


                                if (reader["ImagePath"] != DBNull.Value)
                                {
                                    ImagePath = (string)reader["ImagePath"];
                                }
                                else
                                {
                                    ImagePath = "";
                                }

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

        public static int AddNewPerson(string FirstName, string SecondName,
            string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
            short Gendor, string Address, string Phone, string Email,
             int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_InsertNewPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);

                    if (ThirdName != "" && ThirdName != null)
                        command.Parameters.AddWithValue("@ThirdName", ThirdName);
                    else
                        command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor", Gendor);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);

                    if (Email != "" && Email != null)
                        command.Parameters.AddWithValue("@Email", Email);
                    else
                        command.Parameters.AddWithValue("@Email", System.DBNull.Value);

                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);



                    if (ImagePath != "" && ImagePath != null)
                        command.Parameters.AddWithValue("@ImagePath", ImagePath);
                    else
                        command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
                    SqlParameter OutPut = new SqlParameter("@NewPersonID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(OutPut);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        if (OutPut.Value != DBNull.Value)
                        {
                            PersonID = Convert.ToInt32(OutPut.Value);
                        }

                    }

                    catch (Exception ex)
                    {
                        clsEventLogger.WriteEventLogger(ex);
                    }

                    finally
                    {

                    }

                    return PersonID;
                }
            }
        }

        public static bool UpdatePerson(int ID, string FirstName, string SecondName,
            string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
            short Gendor, string Address, string Phone, string Email,
             int NationalityCountryID, string ImagePath)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_UpdatePerson", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", ID);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    if (ThirdName != "" && ThirdName != null)
                        command.Parameters.AddWithValue("@ThirdName", ThirdName);
                    else
                        command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@Gendor", Gendor);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

                    if (Email != "" && Email != null)
                        command.Parameters.AddWithValue("@Email", Email);
                    else
                        command.Parameters.AddWithValue("@Email", System.DBNull.Value);


                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);


                    if (ImagePath != "" && ImagePath != null)
                        command.Parameters.AddWithValue("@ImagePath", ImagePath);
                    else
                        command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


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

        public static DataTable GetAllPeople()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

              

                using (SqlCommand command = new SqlCommand("SP_GetAllPeople", connection))
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

        public static bool DeletePerson(int PersonID)
        {

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {



                using (SqlCommand command = new SqlCommand("SP_DeletePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_IsPersonExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsPersonExistByNationalNo", connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
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
    }
}
