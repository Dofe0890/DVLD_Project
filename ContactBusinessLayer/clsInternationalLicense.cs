using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBusinessLayer
{
    public class clsInternationalLicense:clsApplications
    {

        enum enMode { AddNew , Update}
        enMode Mode = enMode.AddNew;

    
        public clsDrivers DriverInfo;
public int InternationalLicenseID { get; set; }
public int DriverID { get; set; }
public int IssuedUsingLocalLicenseID { get; set; }
public DateTime IssueDate { get; set; }
public DateTime ExpirationDate { get; set; }
public bool IsActive { get; set; }

  
        public clsInternationalLicense()
        {
            this.ApplicationTypeID = (int)clsApplications.enApplicationType.NewInternationalLicense;
            
            InternationalLicenseID = -1;       
            DriverID = -1;
            IssuedUsingLocalLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = true;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;

        }
     
        public clsInternationalLicense(int ApplicationID ,int ApplicantPersonID ,DateTime ApplicationDate,enApplicationStatus ApplicationStatus,DateTime LastStatusDate
             , double PaidFees,int CreatedByUserID ,int internationalLicenseID ,int driverID,int issuedUsingLocalLicenseID,DateTime issueDate,DateTime expirationDate , bool isActive )
        {

            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationStatus = ApplicationStatus;
            base.ApplicationTypeID = (int)clsApplications .enApplicationType.NewInternationalLicense;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;


            InternationalLicenseID = internationalLicenseID;
            this.ApplicationID = ApplicationID;
            DriverID = driverID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            this.CreatedByUserID = CreatedByUserID;
            this.DriverInfo = clsDrivers.FindDriver(this.DriverID);

            Mode = enMode.Update;
            
        }
      
        public static clsInternationalLicense FindInternationalLicense(int InternationalLicense)
        {

         int ApplicationID = -1;
         int   DriverID = -1;
         int   IssuedUsingLocalLicenseID = -1;
         DateTime   IssueDate = DateTime.Now;
         DateTime   ExpirationDate = DateTime.Now;
         bool   IsActive = false;
         int  CreatedByUserID = -1;

           bool isFound = clsInternationalLicenseData.FindInternationalLicense(InternationalLicense,ref ApplicationID,ref DriverID,ref IssuedUsingLocalLicenseID,ref IssueDate,ref ExpirationDate, ref IsActive,ref CreatedByUserID);
            if (isFound)
            {
                clsApplications Application = clsApplications.FindApplication(ApplicationID);



                return new clsInternationalLicense(Application.ApplicationID ,Application.ApplicantPersonID ,Application .ApplicationDate,(enApplicationStatus)Application.ApplicationStatus ,Application.LastStatusDate ,Application.PaidFees ,Application.CreatedByUserID ,
                    InternationalLicense,DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive);
            }
            else 
            return null;

        }

        public static DataTable GetAllInternationalApplications()
        {
            return clsInternationalLicenseData.GetAllInternationalApplications();
        }
      
        public static bool IsInternationalLicenseExistByLocalLicense(int License)
        {
            return FindInternationalLicense(License) != null;
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            if (InternationalLicenseID > 0 )
            {
                return true;
            }
            else 
                return false;
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }
      
        public bool Save()
        {

            base.Mode = (clsApplications.enMode)Mode;
            if(!base.Save())
                return false;


            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else 
                        return false;
                    case enMode.Update:
                    return _UpdateInternationalLicense();


            }
            return false;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID((int)DriverID);
        }

        public static DataTable GetDriverInternationalLicense(int DriverID)
        {

            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
    }
}
