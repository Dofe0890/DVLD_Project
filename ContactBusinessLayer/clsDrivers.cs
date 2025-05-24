using ContactsBusinessLayer;
using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBusinessLayer
{
    public class clsDrivers
    {
        enum enMode { AddNew , Update}
        enMode Mode = enMode.AddNew;

        public clsPerson PersonInfo;
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public  clsDrivers(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            DriverID = driverID;
            PersonID = personID;
            PersonInfo=clsPerson.Find(personID);
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;
            
        }
      
        public clsDrivers()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }

        static public clsDrivers FindDriver(int DriverID)
        {
          
           int PersonID = -1;
           int CreatedByUserID = -1;
           DateTime CreatedDate = DateTime.Now;
            bool isFound = clsDriversData.FindDriver(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate);


            if (isFound == true)
            {
       
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }
     
        static public clsDrivers FindDriverByPersonID(int PersonID)
        {

            int DriverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            bool isFound = clsDriversData.FindDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate);


            if (isFound == true)
            {

                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        static public bool IsDriverExists(int PersonID)
        {
            if (FindDriverByPersonID(PersonID ) != null)
            {
                return true;
            }
            else 
                return false;
        }
    
        private bool _AddNewDriver()
        {
            this.DriverID = clsDriversData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            
            if (DriverID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
      
        private bool _UpdateDriver()
        {
            return clsDriversData.UpdateDriver(this.DriverID , this.PersonID,this.CreatedByUserID, this.CreatedDate);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
 if (_AddNewDriver())
            {
                Mode = enMode.Update;
                return true;
            }
  else
            {
                return false;
            }
                case enMode.Update:
                   return _UpdateDriver();

            }
            return false;
           
          
        }

        static public DataTable GetAllDrivers()
        {
            return clsDriversData.GetAllDrivers();
        }

        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicenses.GetDriverLicense(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicense.GetDriverInternationalLicense(DriverID);
        }

    }
} 
