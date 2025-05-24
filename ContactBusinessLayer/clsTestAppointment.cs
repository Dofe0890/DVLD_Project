using ContactsBusinessLayer;
using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersDataAccessLayer;

namespace ContactBusinessLayer
{
 public   class clsTestAppointment
    {
        enum enMode { AddNew , Update};
        enMode _Mode = enMode.AddNew;

      
        public clsTestTypes.enTestTypes TestTypeID
            { get; set; }
public int TestAppointmentID {  get; set; }
public int LocalDrivingLicenseApplicationID {  get; set; }
public DateTime AppointmentDate {  get; set; }
public double PaidFees {  get; set; }
public int CreatedByUserID {  get; set; }
public bool IsLocked {  get; set; }
        public int RetakeTestApplicationID { set; get; }
        public clsApplications RetakeTestAppInfo { set; get; }

        public int TestID
        {
            get { return _GetTestID(); }

        }
        public clsTestAppointment()
        {
TestAppointmentID = -1;
TestTypeID = clsTestTypes.enTestTypes.VisionTest;
LocalDrivingLicenseApplicationID = -1;
AppointmentDate = DateTime.Now;
PaidFees = -1;
CreatedByUserID = -1;
            RetakeTestApplicationID = -1;
IsLocked = false;
            _Mode = enMode.AddNew;
 


        }


    public    clsTestAppointment(int testAppointmentID, clsTestTypes.enTestTypes testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate, double paidFees, int createdByUserID, bool isLocked,int retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;
            RetakeTestAppInfo = clsApplications.FindApplication(RetakeTestApplicationID);
            _Mode = enMode.Update;
        }

        public static clsTestAppointment FindAppointment( int TestAppointment)
        {
            int TestTypesID = -1 , RetakeTestApplicationID = -1;
            int LocalDrivingLicenseApplicationID = -1 ,CreatedUserID =-1;
            DateTime AppointmentDate = DateTime.Now;
            double PaidFees = -1;
            bool IsLocked = false;
            bool isFound = clsTestAppointmentData.FindAppointment(TestAppointment, ref TestTypesID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedUserID, ref IsLocked ,ref RetakeTestApplicationID );

            if (isFound == true)
            {
                return new clsTestAppointment(TestAppointment,(clsTestTypes.enTestTypes)TestTypesID,LocalDrivingLicenseApplicationID ,AppointmentDate ,PaidFees ,CreatedUserID ,IsLocked ,RetakeTestApplicationID);
            }
            else
            {
                return null;
            }
           
        }
        private bool _AddNewAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID);
            if (TestAppointmentID > 0)
                return true;
            else
                return false;
        }


        private bool _UpdateAppointment()
        {
            
            return clsTestAppointmentData.UpdateAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID );
        }

        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllAppointments();

        }

        public bool Save()
        {


            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAppointment())
                    {
                       
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    _UpdateAppointment();
                    return true ;

            }




            return false;
        }

        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplication,clsTestTypes .enTestTypes TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now; double PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointmentData.FindLastTestAppointment(LocalDrivingLicenseApplication, (int)TestTypeID,
                ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplication,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;
        }

        public DataTable GetApplicationTestAppointmentsPerTestType(clsTestTypes.enTestTypes TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentPerTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID );
        }

    }
}
