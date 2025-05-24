using ContactBusinessLayer;
using ContactsBusinessLayer;
using CurrentUser;
using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class frmScheduleTest : Form
    {

        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestTypes.enTestTypes _TestTypeID = clsTestTypes.enTestTypes.VisionTest;
        private int _AppointmentID = -1;

        public frmScheduleTest(int LocalDrivingLicenseApplication,clsTestTypes.enTestTypes TestTypeID,int TestAppointmentID=-1)
        {

           
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplication;
            _TestTypeID = TestTypeID;
            _AppointmentID = TestAppointmentID;   
        }

        private void frmAddorEditVisionTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypesID = _TestTypeID;
           ctrlScheduleTest1.LoadData(_LocalDrivingLicenseApplicationID,_AppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
