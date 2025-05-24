using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBusinessLayer
{
    public class clsDetainedLicenses
    {

        enum enMode { AddNew , Update}
        enMode Mode = enMode.AddNew ;
public int DetainID { get; set; }
public int LicenseID { get; set; }
public DateTime DetainDate { get; set; }
public double FineFees { get; set; }
public int CreatedByUserID  { get; set; }
public clsUsers CreatedByUserInfo { get; set; }
public bool IsReleased { get; set; }
public DateTime ReleaseDate { get; set; }
public int ReleasedByUserID { get; set; }
public clsUsers ReleasedByUserInfo { set; get; }
public int ReleaseApplicationID { get; set; }

      public  clsDetainedLicenses()
        {
             DetainID = -1;
             LicenseID = -1;
             DetainDate = DateTime.Now;
             FineFees = -1;
             CreatedByUserID = -1;
             IsReleased = false;
             ReleaseDate = DateTime.Now;
             ReleasedByUserID = -1;
             ReleaseApplicationID = -1;
            Mode = enMode.AddNew;

        }

      public  clsDetainedLicenses(int detainID, int licenseID, DateTime detainDate, double fineFees, int createdByUserID, bool isReleased, DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {

            this.DetainID = detainID;
            this.LicenseID = licenseID;
            this.DetainDate = detainDate;
            this.FineFees = fineFees;
            this.CreatedByUserID = createdByUserID;
            this.CreatedByUserInfo = clsUsers.Find(this.CreatedByUserID);
            this.IsReleased = isReleased;
            this.ReleaseDate = releaseDate;
            this.ReleasedByUserID = releasedByUserID;
            this.ReleasedByUserInfo = clsUsers.Find(this.ReleasedByUserID);
            this.ReleaseApplicationID = releaseApplicationID;
            Mode = enMode.Update;

        }

        static public DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }

        static public clsDetainedLicenses FindDetainedLicense(int DetainID)
        {
            
           int LicenseID = -1;
           DateTime DetainDate = DateTime.Now;
           double FineFees = -1;
           int  CreatedByUserID = -1;
           bool IsReleased = false;
           DateTime ReleaseDate = DateTime.Now;
           int ReleasedByUserID = -1;
           int ReleaseApplicationID = -1;
            bool IsFound = clsDetainedLicenseData.FindDetain(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID);
            if (IsFound)
            {
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
                return null;



        }

        static public clsDetainedLicenses FindDetainedLicenseByLicenseID(int LicenseID)
        {

            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            double FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.Now;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;
            if( clsDetainedLicenseData.FindDetainByLicenseID(LicenseID , ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new clsDetainedLicenses(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            
            else
                return null;



        }

        static public bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID,
                   ReleasedByUserID, ReleaseApplicationID);
        }
        
        private bool _AddNewDetainLicense()
        {
this.DetainID=clsDetainedLicenseData.AddNewDetain(this.LicenseID,this.DetainDate,this.FineFees ,this.CreatedByUserID ,this.IsReleased ,this.ReleaseDate,this.ReleasedByUserID ,this.ReleaseApplicationID);

            return (this.DetainID != -1);
        }

        private bool _UpdateDetainLicense()
        {
return clsDetainedLicenseData.UpdateDetain(this.DetainID,this.LicenseID,this.DetainDate,this.FineFees ,this.CreatedByUserID,this.IsReleased ,this.ReleaseDate , this.ReleasedByUserID,this.ReleaseApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                 return _UpdateDetainLicense();
           
            }
            return false;



        }
    }
}
