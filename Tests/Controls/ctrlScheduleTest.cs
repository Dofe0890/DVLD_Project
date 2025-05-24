using ContactBusinessLayer;
using ContactsBusinessLayer;
using CurrentUser;
using DVlD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD_Project
{
    public partial class ctrlScheduleTest : UserControl
    {

        
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }


        enum enMode { AddNew, Update }
        enMode _Mode = enMode.AddNew;

        enum enCreationMode { FirstTimeSchedule=0,RetakeTestSchedule=1 }
        enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private clsTestTypes.enTestTypes _TestTypesID = clsTestTypes.enTestTypes.VisionTest;
        private clsTestAppointment _TestAppointment;
        private clsLocalDrivingApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _TestAppointmentID = -1;
   
        public clsTestTypes.enTestTypes TestTypesID
        {
            get
            {
                return _TestTypesID;
            }
            set
            {
                _TestTypesID = value;
                switch (_TestTypesID)
                {
                    case clsTestTypes.enTestTypes.VisionTest:
                        {
                      
                            lblTitle.Text = " Vision Test ";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }
                    case clsTestTypes.enTestTypes.WritingTest:
                        {
                            lblTitle.Text = " Writing Test ";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestTypes.enTestTypes.StreetTest:
                        {
                            lblTitle.Text = " Street Test ";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;
                        }

                }
            }

        }


        public void LoadData(int LocalDrivingLicenseApplicationID ,int AppointmentID =-1 )
        {

            if (AppointmentID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;
            _LocalDrivingLicenseApplication = clsLocalDrivingApplication.FindLocalDrivingLicenseApplication(_LocalDrivingLicenseApplicationID);


            if (_LocalDrivingLicenseApplication==null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            if (_LocalDrivingLicenseApplication.DoseAttendTestType(_TestTypesID))
            {
                _CreationMode = enCreationMode.RetakeTestSchedule;
            }
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;

            if(_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeAppFees.Text = clsApplicationTypes.FoundApplicationType((int)clsApplications.enApplicationType.RetakeTest).ApplicationTypeFee.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }

            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";


            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            lblTrial .Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypesID).ToString();


            if(_Mode ==enMode .AddNew)
            {
                lblFees.Text = clsTestTypes.FoundTestType(_TestTypesID ).TestTypeFees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";

                _TestAppointment = new clsTestAppointment();
            }

            else
            {

                if (!_LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = (Convert.ToDouble(lblFees.Text) + Convert.ToDouble(lblRetakeAppFees.Text)).ToString();


            if (!_HandleActiveTestAppointmentConstraint())
                return;
            if (!_HandleAppointmentLockedConstraint())
                return;
            if (!_HandlePreviousTestConstraint())
                return;



        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if(_Mode == enMode.AddNew && clsLocalDrivingApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID ,_TestTypesID))
            {
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;

            }
            return true;
        }
      
        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.FindAppointment(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;

            dtpTestDate.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

            }
            return true;
        }

        private bool _HandleAppointmentLockedConstraint()
        {
            if(_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;

            }
            else
                lblUserMessage.Visible = false;

            return true;
        }
       
        private bool _HandlePreviousTestConstraint()
        {
            switch (TestTypesID)
            {

                case clsTestTypes.enTestTypes.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;
                case clsTestTypes.enTestTypes.WritingTest:
                    if (!_LocalDrivingLicenseApplication.DosePassTestType(clsTestTypes.enTestTypes.VisionTest))
                        {
                        lblUserMessage.Text = "Cannot Schule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;
                case clsTestTypes.enTestTypes.StreetTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestTypes.WritingTest))
                    {
                        lblUserMessage.Text = "Cannot Schule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }


                    return true;
            }
            return true;
        }

        private bool _HandleRetakeApplication()
        {
            if(_Mode == enMode.AddNew && _CreationMode==enCreationMode.RetakeTestSchedule)
            {
                clsApplications Application = new clsApplications();

                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplications.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationTypes.FoundApplicationType((int)clsApplications.enApplicationType.RetakeTest).ApplicationTypeFee;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;

            }
            return true;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
           if(!_HandleRetakeApplication())
                return;



            _TestAppointment.TestTypeID = _TestTypesID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingApplicationID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToDouble(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

        private void ctrlTestTypes_Load_1(object sender, EventArgs e)
        {
           
        }
    }
}
