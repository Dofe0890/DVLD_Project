using ContactBusinessLayer;
using DVlD_Project.Peoplee;
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

namespace DVlD_Project.Tests
{
    public partial class frmListTestAppointment : Form
    {
        private  int _LocalDrivingLicenseApplicationID = -1;
        private clsTestTypes.enTestTypes _TestType = clsTestTypes.enTestTypes.VisionTest;
        private DataTable _dtLicenseTestAppointments;
        public frmListTestAppointment(int LocalDrivingLicenseApplicationID,clsTestTypes.enTestTypes TestTypeID)
        { 
            InitializeComponent();
            _TestType = TestTypeID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
           
         
         
            
        }

       
        
        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestTypes.enTestTypes.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                          pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestTypes.enTestTypes.WritingTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                           pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestTypes.enTestTypes.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                           pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
          
        }
       
        private void frmVisionTest_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();


            ctrlDrivingLicenseApplicationInfo1.LoadInfoDataByLocalDrivingLicenseID(_LocalDrivingLicenseApplicationID);
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestType);

            dgvAppointment.DataSource = _dtLicenseTestAppointments;
            lblRecordsCount.Text = dgvAppointment.Rows.Count.ToString();

            if (dgvAppointment.Rows.Count > 0)
            {
                dgvAppointment.Columns[0].HeaderText = "Appointment ID";
                dgvAppointment.Columns[0].Width = 150;

                dgvAppointment.Columns[1].HeaderText = "Appointment Date";
                dgvAppointment.Columns[1].Width = 200;

                dgvAppointment.Columns[2].HeaderText = "Paid Fees";
                dgvAppointment.Columns[2].Width = 150;

                dgvAppointment.Columns[3].HeaderText = "Is Locked";
                dgvAppointment.Columns[3].Width = 100;
            }

        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingApplication localDrivingApplication = clsLocalDrivingApplication.FindLocalDrivingLicenseApplication(_LocalDrivingLicenseApplicationID);

            if (localDrivingApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = localDrivingApplication.GetLastTestPerTestType(_TestType);

            if (LastTest == null)
            {
                Form frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
                frm1.ShowDialog();
                frmVisionTest_Load(null, null);
                return;
            }

            if(LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form frm = new frmScheduleTest(LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID ,_TestType);
            frm.ShowDialog();
             frmVisionTest_Load (null,null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
       int  AppointmentID = (int) dgvAppointment.CurrentRow.Cells[0].Value;
            Form frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID,_TestType,AppointmentID);
            frm.ShowDialog();
            frmVisionTest_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int AppointmentID = (int)dgvAppointment.CurrentRow.Cells[0].Value;
            Form frm = new frmTakeTest(AppointmentID, _TestType);
                frm.ShowDialog();
            frmVisionTest_Load(null, null);
        }
    }
}
