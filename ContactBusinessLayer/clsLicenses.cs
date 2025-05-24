using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBusinessLayer
{
    public class clsLicenses
    {
        enum enMode { AddNew , Update}
        enMode Mode = enMode.AddNew ;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public clsDrivers DriverInfo;
        public int LicenseID {  get; set; }
           public int ApplicationID { get; set; }
           public int DriverID { get; set; }
           public int LicenseClass {  get; set; }
           public LicenseClasses LicenseClassInfo;
           public DateTime IssueDate { get; set; }
           public DateTime ExpirationDate { get; set; }
           public string Notes { get; set; }
           public double PaidFees { get; set; }
           public bool IsActive { get; set; }
           public enIssueReason  IssueReason { get; set; }

           public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }

        public clsDetainedLicenses DetainInfo { get; set; }

        public bool IsDetained
        {
            get { return clsDetainedLicenses.IsLicenseDetained(this.LicenseID); }
        }
     
        public int CreatedByUserID   { get; set; }

  
        public   clsLicenses()
        {
            LicenseID = -1;
            ApplicationID  = -1;
            DriverID  = -1;
            LicenseClass  = -1;
            IssueDate  = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes  = "";
            PaidFees  = -1;
            IsActive  = true;
            IssueReason  = enIssueReason.FirstTime;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }
    
        public clsLicenses ( int licenseID,int applicationID, int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate, string notes, double paidFees, bool isActive, enIssueReason issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClass = licenseClass;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;

            this.DriverInfo =clsDrivers.FindDriver(this.DriverID);
            this.LicenseClassInfo =LicenseClasses.FoundClass(this.LicenseClass);
            this.DetainInfo=clsDetainedLicenses.FindDetainedLicenseByLicenseID(this.LicenseID);

            Mode = enMode.Update;
        }

        static public clsLicenses FindLicense(int LicenseID)
        {
          
           int ApplicationID = -1;
           int DriverID = -1;
           int LicenseClass = -1;
          DateTime  IssueDate = DateTime.Now;
          DateTime ExpirationDate = DateTime.Now;
          string Notes = "";
          double PaidFees = -1;
          bool IsActive = false;
          byte  IssueReason = 0;
          int CreatedByUserID = -1;

            bool isFound = clsLicensesData.FindLicense(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID);
            if (isFound)
            {
                return new clsLicenses(LicenseID , ApplicationID , DriverID , LicenseClass,IssueDate , ExpirationDate , Notes , PaidFees , IsActive , (enIssueReason)IssueReason , CreatedByUserID );
            }
            else {
                return null; 
            }

        }

        public static DataTable GetAllLicenses()
        {
            return clsLicensesData.GetAllLicenses();

        }

        private bool _AddNewLicense( )
        {
            this.LicenseID = clsLicensesData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(byte) this.IssueReason, this.CreatedByUserID);
            if (LicenseID > 0 )
            {
                return true;
            }
            else 
                return  false;

        }
     
        private bool _UpdateLicense()
        {
            return clsLicensesData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(byte) this.IssueReason, this.CreatedByUserID);
        }
       
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else 
                        return false;

                case enMode.Update:
                    return _UpdateLicense();




            }
            return false;
        }
      
        public static bool IsLicenseExistByPersonID(int PersonID,int LicenseClassID )
        {

            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);

        }

        public static int GetActiveLicenseIDByPersonID(int PersonID , int LicenseClassID)
        {
            return clsLicensesData.GetActiveLicenseIDByPersonID (PersonID, LicenseClassID);
        }
       
        public static DataTable GetDriverLicense(int DriverID)
        {
            return clsLicensesData.GetDriversLicense(DriverID);
        }

        public Boolean IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public bool DeactivateCurrentLicense()
        {
            return (clsLicensesData.DeactivateLicense(this.LicenseID));
        }

        public static string GetIssueReasonText(enIssueReason IssueReason)
        {
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "FirstTime";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement For Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement For Lost";
                default:
                    return "First Time";

            }


        }

        public int Detain(float FineFees,int CreatedByUserID)
        {
            clsDetainedLicenses detainedLicenses = new clsDetainedLicenses();
            detainedLicenses.LicenseID = this.LicenseID;
            detainedLicenses.DetainDate = DateTime.Now;
            detainedLicenses.FineFees = Convert.ToDouble(FineFees); 
            detainedLicenses.CreatedByUserID = CreatedByUserID;

            if (!detainedLicenses.Save())
            {
                return -1;
            }
            return detainedLicenses.DetainID ;
        }

        public bool ReleaseDetainLicense(int ReleaseByUserID, ref int ApplicationID)
        {
            clsApplications App = new clsApplications();

            App.ApplicantPersonID = this.DriverInfo.PersonID;
            App.ApplicationDate = DateTime.Now;
            App.ApplicationTypeID = (int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicense;
            App.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            App.LastStatusDate = DateTime.Now;
            App.PaidFees = clsApplicationTypes.FoundApplicationType((int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicense).ApplicationTypeFee;
            App.CreatedByUserID = ReleaseByUserID;


            if (!App.Save())
            {
                ApplicationID = -1;
                return false;
            }
            ApplicationID = App.ApplicationID;

            return this.DetainInfo.ReleaseDetainedLicense(ReleaseByUserID, App.ApplicationID);
        }

        public clsLicenses  RenewLicense(string  Notes,  int CreatedByUserID)
        {
            clsApplications App = new clsApplications();

            App.ApplicantPersonID = this.DriverInfo.PersonID;
            App.ApplicationDate = DateTime.Now;
            App.ApplicationTypeID = (int)clsApplications.enApplicationType.RenewDrivingLicense;
            App.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            App.LastStatusDate = DateTime.Now;
            App.PaidFees = clsApplicationTypes.FoundApplicationType((int)clsApplications.enApplicationType.RenewDrivingLicense).ApplicationTypeFee;
            App.CreatedByUserID = CreatedByUserID ;


            if (!App.Save())
            {
                
                return null ;
            }
          
            clsLicenses NewLicense = new clsLicenses();

            NewLicense.ApplicationID = App.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LicenseClassInfo.DefaultValidityLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicenses.enIssueReason.Renew;
            NewLicense.CreatedByUserID =CreatedByUserID ;

            if (!NewLicense.Save())
            {
                return null ;   
            }

            DeactivateCurrentLicense();

            return NewLicense ;
        }


        public clsLicenses Replace(enIssueReason  IssueReason, int CreatedByUserID)
        {
            clsApplications App = new clsApplications();

            App.ApplicantPersonID = this.DriverInfo.PersonID;
            App.ApplicationDate = DateTime.Now;
            App.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplications.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplications.enApplicationType.ReplaceLostDrivingLicense;


            App.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            App.LastStatusDate = DateTime.Now;
            App.PaidFees = clsApplicationTypes.FoundApplicationType((int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicense).ApplicationTypeFee;
            App.CreatedByUserID = CreatedByUserID;


            if (!App.Save())
            {

                return null;
            }

            clsLicenses NewLicense = new clsLicenses();

            NewLicense.ApplicationID = App.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate =this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees =0;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLicense();

            return NewLicense;
        }


    }
}
