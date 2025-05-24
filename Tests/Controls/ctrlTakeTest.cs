using ContactBusinessLayer;
using ContactsBusinessLayer;
using DVlD_Project.Global_Classes;
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
    public partial class ctrlTakeTest : UserControl
    {
        public ctrlTakeTest()
        {
            InitializeComponent();
        }


        private int _TestID = -1;
        private clsTestTypes.enTestTypes _TestTypesID ;

        private clsLocalDrivingApplication _LocalDrivingLicenseApplication;

        private int _LocalDrivingLicenseApplicationID = -1;

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

        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }

        public int TestID
        {
            get
            {
                return _TestID;
            }
        }


        private int _TestAppointmentID = -1;
  
        private clsTestAppointment _TestAppointment;




        public void LoadData(int AppointmentID)
        {

            _TestAppointmentID = AppointmentID;


            _TestAppointment = clsTestAppointment.FindAppointment(_TestAppointmentID);


            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointment.TestID;

            _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingApplication.FindLocalDrivingLicenseApplication(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;


            //this will show the trials for this test before 
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypesID).ToString();



            lblDate.Text = clsFormat.DateToShort(_TestAppointment.AppointmentDate);
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Not Taken Yet" : _TestAppointment.TestID.ToString();


        }

        private void ctrlTakeTest_Load(object sender, EventArgs e)
        {

         
        }

        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }
    }
}
