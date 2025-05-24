using ContactsBusinessLayer;
using PeopleDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UsersDataAccessLayer;
using static ContactsBusinessLayer.clsPerson;

namespace ContactBusinessLayer
{
    public class clsUsers
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { set; get; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo;
       
        public string Username { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }


      public  clsUsers()
        {
            PersonID = -1;
            UserID = -1;
            Password = "";
            Username = "";
            IsActive = false;
            Mode = enMode.AddNew;
        }

      public  clsUsers( int userID,int personID, string userName, string password, bool isActive)
        {
            this.PersonID = personID;
            this.PersonInfo = clsPerson.Find(personID);
            this.UserID = userID;
            this.Username = userName;
            this.Password = password;
            this.IsActive = isActive;
            Mode = enMode.Update;
        }

        public static clsUsers Find(string Username, string Password)
        {
            int UserID = -1, PersonID = -1;
            bool isActive = false;
            bool isFound = clsUsersData.FindUser(Username, Password, ref UserID, ref PersonID, ref isActive);

            if (isFound == true)
            {
                return new clsUsers(PersonID, UserID, Username, Password, isActive);
            }
            else
            {
                return null;
            }

        }

        public static clsUsers Find(int UserID)
        {
            int  PersonID = -1;
            string Username ="", Password = "";
            bool isActive = false;
            bool isFound = clsUsersData.FindUser(UserID,ref Username, ref Password, ref PersonID, ref isActive);

            if (isFound == true)
            {
                return new clsUsers(UserID,PersonID, Username, Password, isActive);
            }
            else
            {
                return null;
            }

        }

        private bool _AddNewUser()
        {
            if (clsPerson.Find(PersonID) != null)
            {
                
            this.UserID = clsUsersData.AddNewUser(this.Username, ComputeHash(this.Password), this.PersonID, this.IsActive);
            if (UserID > 0)
                return true;
            else
                return false; }
            return false;
        }
      
        private bool _UpdateUser()
          {
            
           return clsUsersData.UpdateUser(this.UserID, this.Username, ComputeHash(this.Password), this.PersonID, this.IsActive);
       }

        public static clsUsers FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            bool IsFound = clsUsersData.FindUserByPersonID
                                (PersonID, ref UserID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsUsers(UserID, UserID, UserName, Password, IsActive);
            else
                return null;
        }

        public static clsUsers FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;

            bool IsActive = false;

            bool IsFound = clsUsersData.FindUsernameAndPassword
                                (UserName, ComputeHash(Password), ref UserID, ref PersonID, ref IsActive);

            if (IsFound)
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }




            return false;
        }

        public static bool DeleteUser(int UserID)
        {
           return  (clsUsersData.DeleteUser(UserID));
        }
     
        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers();
        }

        public static bool isUserExist(int UserID)
        {
            return clsUsersData.IsUserExist(UserID);
        }

        public static bool isUserExist(string UserName)
        {
            return clsUsersData.IsUserExist(UserName);
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            return clsUsersData.IsUserExistForPersonID(PersonID);
        }

       public static string ComputeHash(string Input)
        {



            using (SHA256 sha256 = SHA256.Create())
            {

                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            }
        }

    }
}
