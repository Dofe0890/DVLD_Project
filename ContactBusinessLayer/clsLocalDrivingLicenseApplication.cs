using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsBusinessLayer;
using ContactsDataAccessLayer;
using static System.Net.Mime.MediaTypeNames;
namespace ContactBusinessLayer
{
    public class clsLocalDrivingApplication:clsApplications
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int LocalDrivingApplicationID { get; set; }

        public int LicenseID { get; set; }
        public LicenseClasses LicenseClassInfo;
        public string PersonFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }

        }

        public clsLocalDrivingApplication()
        {
            LocalDrivingApplicationID = 0;
            LicenseID = 0;
            Mode = enMode.AddNew;
        }

        public clsLocalDrivingApplication(int localDrivingApplicationID,int applicationID,int licenseID )
        {
         LocalDrivingApplicationID = localDrivingApplicationID;
            ApplicationID=applicationID;
            LicenseID=licenseID;
        }

        private clsLocalDrivingApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
           DateTime ApplicationDate, int ApplicationTypeID,
            enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
            double PaidFees, int CreatedByUserID, int LicenseClassID)

        {
            this.LocalDrivingApplicationID = LocalDrivingLicenseApplicationID; ;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseID = LicenseClassID;
            this.LicenseClassInfo = LicenseClasses .FoundClass(LicenseClassID);
            Mode = enMode.Update;
        }

     
        public bool DosePassTestType(clsTestTypes.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicnseApplicationData.DoesPassTestType(this.LocalDrivingApplicationID, (int)TestTypeID);
        }
      
        public static clsLocalDrivingApplication FindLocalDrivingLicenseApplication(int LocalDrivingApplicationID)
        {
            int ApplicationID = -1, LicenseID = -1;


            bool isFound = clsLocalDrivingLicnseApplicationData.FindLocalDrivingLicenseApplication(LocalDrivingApplicationID, ref ApplicationID, ref LicenseID);
            if (isFound)
            {
                clsApplications Application = clsApplications.FindApplication(ApplicationID);

                return new clsLocalDrivingApplication(LocalDrivingApplicationID , Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseID) ;
            }
            else
                return null;

        }


        private bool _AddNewLocalDrivingLicenseApplication()
        {


            this.LocalDrivingApplicationID  = clsLocalDrivingLicnseApplicationData.AddLocalDrivingLicenseApplication (this.ApplicationID, this.LicenseID);

            return (LocalDrivingApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
    

            return clsLocalDrivingLicnseApplicationData.UpdateLocalDrivingLicenseApplication
                (
                this.LocalDrivingApplicationID , this.ApplicationID, this.LicenseID);

        }


        public static clsLocalDrivingApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseID = -1;


            bool isFound = clsLocalDrivingLicnseApplicationData.FindLocalDrivingLicenseApplicationByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseID);
            if (isFound)
            {
                clsApplications Application = clsApplications.FindApplication(ApplicationID);

                return new clsLocalDrivingApplication(LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseID);
            }
            else
                return null;

        }


        public bool Save()
        {

         
            base.Mode = (clsApplications.enMode)Mode;
            if (!base.Save())
                return false;


            
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplication();

            }

            return false;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicnseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public bool Delete()
        {

            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;

            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicnseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingApplicationID);


            if (!IsLocalDrivingApplicationDeleted)
            {
                return false;

            }

            IsBaseApplicationDeleted = base.DeleteApplication();
            return IsBaseApplicationDeleted;

        }

        public bool DoesPassTestType(clsTestTypes.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicnseApplicationData.DoesPassTestType(this.LocalDrivingApplicationID, (int)TestTypeID);
        }

        public bool DosePassPerviousTest(clsTestTypes.enTestTypes CurrentTestType)
        {
            switch (CurrentTestType)
            {
                case clsTestTypes.enTestTypes.VisionTest:

                    return true;
                case clsTestTypes.enTestTypes.WritingTest:
                    return this.DosePassTestType(clsTestTypes.enTestTypes.VisionTest);
                case clsTestTypes.enTestTypes.StreetTest:
                    return this.DoesPassTestType(clsTestTypes.enTestTypes.WritingTest);

                default:
                    return false;

            }





        }


        public static bool DosePassTest(int LocalDrivingLicenseApplicationID,clsTestTypes.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicnseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoseAttendTestType(clsTestTypes.enTestTypes TestTypeID) {


            return clsLocalDrivingLicnseApplicationData.DoesAttendTestType(this.LocalDrivingApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestTypes.enTestTypes TestTypeID)
        {
            return clsLocalDrivingLicnseApplicationData.TotalTrialsPerTest(this.LocalDrivingApplicationID , (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestTypeID)

        {
            return clsLocalDrivingLicnseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestTypeID)

        {
            return clsLocalDrivingLicnseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public bool AttendedTest(clsTestTypes.enTestTypes TestTypeID)

        {
            return clsLocalDrivingLicnseApplicationData.TotalTrialsPerTest(this.LocalDrivingApplicationID, (int)TestTypeID) > 0;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestTypeID)

        {

            return clsLocalDrivingLicnseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestTypes.enTestTypes TestTypeID)

        {

            return clsLocalDrivingLicnseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingApplicationID, (int)TestTypeID);
        }

        public clsTest GetLastTestPerTestType(clsTestTypes.enTestTypes TestTypeID)
        {
            return clsTest.FindLastTestPerPersonIDAndLicenseClass(this.ApplicantPersonID, this.LicenseID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
         
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }


        public int IssueLicenseForFirstTime(string Notes , int CreatedByUserID)
        {
            int DriverID = -1;

            clsDrivers Driver = clsDrivers.FindDriverByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                Driver = new clsDrivers();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }


            clsLicenses License = new clsLicenses();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = this.LicenseID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicenses.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                this.SetComplete();
                return License.LicenseID;
            }
            else 
                return -1;


        }


        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {
            return clsLicenses.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseID);
        }



    }
}
