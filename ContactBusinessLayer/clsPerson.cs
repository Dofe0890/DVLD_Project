using System;
using System.Data;
using PeopleDataAccessLayer;
using Microsoft.SqlServer.Server;


namespace ContactsBusinessLayer
{
    public class clsPerson
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string FullName {
            get {
                return FirstName + " "+ SecondName +" " + ThirdName + " " + LastName ; }
                }
        public short Gendor {  set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }

        public clsCountry CountryInfo;

        private string _ImagePath;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public int NationalCountryID { set; get; }
        public clsPerson()

        {
            this.ID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
          
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
      
            this.NationalCountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;

        }

        private clsPerson(int ID,string NationalNo, string FirstName,string SecondName,string ThirdName, string LastName,short Gendor,
            string Email, string Phone, string Address, DateTime DateOfBirth,int NationalCountryID, string ImagePath)

        {
            this.ID = ID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;   
            this.Gendor = Gendor;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryInfo = clsCountry.Find(NationalCountryID);
            this.NationalCountryID = NationalCountryID;
            this.ImagePath = ImagePath;

            Mode = enMode.Update;

        }
        
        private bool _AddNewContact()
        {
            //call DataAccess Layer 

            this.ID= clsPeopleDataAccess.AddNewPerson(
                this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.NationalNo,
                this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email,
                this.NationalCountryID, this.ImagePath);

            return (this.ID != -1);
        }

        private bool _UpdateContact()
        {
            //call DataAccess Layer 

           return clsPeopleDataAccess.UpdatePerson(this.ID,this.FirstName,this.SecondName ,this.ThirdName , this.LastName,this.NationalNo,this.DateOfBirth,this.Gendor , this.Address , this.Phone,
                this.Email,this.NationalCountryID,this.ImagePath);

        }

     
        public static clsPerson Find(int PersonID)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gendor = 0;

            bool IsFound = clsPeopleDataAccess.GetPersonInfoByID
                                (
                                    PersonID, ref FirstName, ref SecondName,
                                    ref ThirdName, ref LastName, ref NationalNo, ref DateOfBirth,
                                    ref Gendor, ref Address, ref Phone, ref Email,
                                    ref NationalityCountryID, ref ImagePath
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, Gendor,
              Email, Phone, Address, DateOfBirth, NationalityCountryID, ImagePath);
            else
                return null;
        }


        public static clsPerson Find(string NationalNo)
        {

            string  FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;
            int NationalCountryID = -1;
            int ID = -1;

            if (clsPeopleDataAccess.GetPersonInfoByNational(NationalNo , ref ID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref Gendor,
                        ref Email, ref Phone, ref Address, ref DateOfBirth, ref NationalCountryID, ref ImagePath))

                return new clsPerson(ID, NationalNo, FirstName, SecondName, ThirdName, LastName, Gendor,
                           Email, Phone, Address, DateOfBirth, NationalCountryID, ImagePath);
            else
                return null;

        }
        public bool Save()
        {
            

            switch  (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateContact();

            }




            return false;
        }
        
        public static DataTable GetAllPeople()
        {
            return clsPeopleDataAccess.GetAllPeople();

        }

        public static bool DeletePerson(int ID)
        {
           return  clsPeopleDataAccess.DeletePerson (ID);
        }


        public static bool isPersonExist(int ID)
        {
            return clsPeopleDataAccess.IsPersonExist(ID);
        }

        public static bool isPersonExist(string NationlNo)
        {
            return clsPeopleDataAccess.IsPersonExist(NationlNo);
        }

    }
}
