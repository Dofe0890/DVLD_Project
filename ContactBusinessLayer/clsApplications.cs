using ContactsBusinessLayer;
using ContactsDataAccessLayer;
using PeopleDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UsersDataAccessLayer;

namespace ContactBusinessLayer
{
  public class clsApplications
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public enum enApplicationType { NewDrivingLicense =1 , RenewDrivingLicense=2,ReplaceLostDrivingLicense=3,
            ReplaceDamagedDrivingLicense=4, ReleaseDetainedDrivingLicense=5,NewInternationalLicense=6,RetakeTest=8}

        public enMode Mode = enMode.AddNew;

        public enum enApplicationStatus {New = 1,Cancelled=2,Completed=3}

        public int ApplicationID { get; set; }

        public int ApplicantPersonID { get; set; }

        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }

        public DateTime ApplicationDate { get; set; }

        public int ApplicationTypeID {  get; set; }


        public clsApplicationTypes ApplicationTypeInfo;

        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {

                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";



                }




            }
        }

        public DateTime LastStatusDate { get; set; }

        public double PaidFees { get; set; }

        public int CreatedByUserID {  get; set; }
        public clsUsers CreatedByUserInfo;
        public  clsApplications(int applicationID, int applicantPersonID, DateTime applicationDate, int applicationTypeID, enApplicationStatus applicationStatus, DateTime lastStatusDate, double paidFees, int createdByUserID)
        {
            this.ApplicationID = applicationID;
            this.ApplicantPersonID = applicantPersonID;
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeInfo = clsApplicationTypes.FoundApplicationType(applicationTypeID);
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.CreatedByUserInfo = clsUsers.Find(createdByUserID);
            Mode = enMode.Update;
        }

      public  clsApplications()
        {
           this.ApplicationID = -1;
           this.ApplicantPersonID = -1;
           this.ApplicationDate = DateTime.Now;
           this.ApplicationTypeID = 1;
           this.ApplicationStatus = enApplicationStatus.New;
           this.LastStatusDate = DateTime.Now;
            this.PaidFees = -1;
           this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }  
           
        public static clsApplications FindApplication(int ApplicationID)
        {
           int ApplicantPersonID = -1 , ApplicationTypeID = -1, CreatedByUserID = -1;

           DateTime ApplicationDate = DateTime.Now,LastStatusDate = DateTime.Now;
          
          byte  ApplicationStatus = 0;
           
           double PaidFees = -1;
           
            bool isFound = clsApplicationsData.FindApplication(ApplicationID,ref ApplicantPersonID ,ref ApplicationDate ,ref ApplicationTypeID ,ref ApplicationStatus ,ref LastStatusDate ,ref PaidFees ,ref CreatedByUserID);

            if (isFound)
            {
                return new clsApplications(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID,(enApplicationStatus ) ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
                return null;

        }

        public  bool DeleteApplication()
        {
            return (clsApplicationsData.DeleteApplication(this.ApplicationID));
        }

        private bool _AddNewApplication()
        {

this.ApplicationID = clsApplicationsData.AddNewApplication(this.ApplicantPersonID , this.ApplicationDate ,this.ApplicationTypeID ,(byte)this.ApplicationStatus ,this.LastStatusDate , this.PaidFees ,this.CreatedByUserID);
            return (this.ApplicationID != -1);

        }

        private bool _UpdateApplication()
        {
           
            return clsApplicationsData.UpdateApplication(this.ApplicationID,this.ApplicantPersonID ,this.ApplicationDate,this.ApplicationTypeID ,(byte)this.ApplicationStatus ,this.LastStatusDate ,this.PaidFees ,this.CreatedByUserID );
        }

        public bool Cancel()
        {
            return clsApplicationsData.UpdateStatus(ApplicationID, 2);
        }

        public bool SetComplete()

        {
            return clsApplicationsData.UpdateStatus(ApplicationID, 3);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    {
                       return  _UpdateApplication();
                    }

            } 
          
                return false;


        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationsData.IsApplicationExist(ApplicationID);
        }

        public static bool DosePersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationsData.DosePersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DosePersonHaveActiveApplication(this.ApplicationID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplications.enApplicationType ApplicationTypeID)
        {
            return clsApplicationsData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }
     
        public static int GetActiveApplicationIDForLicenseClass (int PersonID,clsApplications.enApplicationType ApplicationTypeID,int LicenseID )
        {
            return clsApplicationsData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseID);
        }

        public int GetActiveApplicationID(clsApplications.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }
       
        public static DataTable GetAllApplications()
        {
            return clsApplicationsData.GetAllApplications();
        }

    }
}
