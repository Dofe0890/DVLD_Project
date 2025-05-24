using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersDataAccessLayer;
using static System.Net.Mime.MediaTypeNames;

namespace ContactBusinessLayer
{
    public class clsTest
    {
        enum enMode { AddNew,Update};
        enMode Mode = enMode.AddNew;
        public int TestID {  get; set; }

        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo { set; get; }
        public bool TestResult { get; set; }

        public string Notes { get; set; }

        public int CreatedUserID { get; set; }

 
        public  clsTest()
        {
            TestID = -1 ;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = "";
            CreatedUserID = -1;

            Mode = enMode.AddNew;
        }
        
        public clsTest(int testID, int testAppointmentID, bool testResult, string notes, int createdUserID)
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.FindAppointment(TestAppointmentID);
            TestResult = testResult;
            Notes = notes;
            CreatedUserID = createdUserID;
            Mode = enMode.Update;
        }


        public static clsTest FindTest(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestData.GetTest(TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTest(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }
      

        public static clsTest FindLastTestPerPersonIDAndLicenseClass(int PersonID,int LicenseClassID,clsTestTypes.enTestTypes TestTypeID)
        {
            int TestID = -1, CreatedUserID = -1 , TestAppointmentID = -1;
            string Notes = "";
            bool TestResult = false;
            bool isFound = clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID ,LicenseClassID ,(int)TestTypeID , ref TestID,ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedUserID);

            if (isFound)
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedUserID);
            }
            else
            {
                return null;
            }


        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(this.TestAppointmentID,this.TestResult ,this.Notes ,this.CreatedUserID);

         
            if (TestID > 0)
                return true;
            else
                return false;
        }

        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID , this.TestAppointmentID ,this.TestResult ,this.Notes ,this.CreatedUserID);
           
        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    _UpdateTest();
                    return true;

            }




            return false;
        }

        public static DataTable GetAllTests()
        {
            return clsTestData.GetAllTests();

        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }
    }
}
