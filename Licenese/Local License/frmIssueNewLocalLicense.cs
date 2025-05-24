using ContactBusinessLayer;
using CurrentUser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD_Project.Licenese.New_Local_License
{
    public partial class frmIssueNewLocalLicense : Form
    {
       
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingApplication _LocalDrivingLicenseApplication;

        public frmIssueNewLocalLicense(int LocalDrivingLicenseAppID)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseAppID;
            InitializeComponent();
           
        }

        private void frmIssueNewLocalLicense_Load(object sender, EventArgs e)
        {
            rcbNote.Focus();
           _LocalDrivingLicenseApplication = clsLocalDrivingApplication.FindLocalDrivingLicenseApplication(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            if (!_LocalDrivingLicenseApplication.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            ctrlDrivingLicenseApplicationInfo1.LoadInfoDataByLocalDrivingLicenseID(_LocalDrivingLicenseApplicationID);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int LicenseID = _LocalDrivingLicenseApplication.IssueLicenseForFirstTime(rcbNote.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
