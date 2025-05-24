using ContactBusinessLayer;
using CurrentUser;
using DVlD_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Applicationiens;
using System.Windows.Forms;
using DVlD_Project.Applicationiens.Test_Type;
using DVlD_Project.Liceses.New_Local_License;
using DVlD_Project.Licenese.New_Local_License;
using DVlD_Project.Drivers;
using DVlD_Project.Licenese.New_International_License;
using DVlD_Project.Applicationiens.Renew;
using DVlD_Project.Applicationiens.Datain_Licenses;
using DVlD_Project.Applications.Replesments;
namespace DVlD_Project
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        public frmMain(frmLogin frm)
        {
            _frmLogin = frm;
            InitializeComponent();
        }

        private void localLicenseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form frm = new frmAddNewLocalLicense();
            frm.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmPeople();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form frm = new frmNewnternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmRenewLicense();
            frm.ShowDialog();
        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReplaceLostOrDamage();
            frm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseDetainLicense();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           Form frm = new frmListLocalDrivingLicenseApplications();
           frm.ShowDialog();
        }

        private void manageLocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void ManageInternationaDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmListOfInternationalLicenseApplications();
            frm.ShowDialog();
        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseDetainLicense();
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmApplicationsTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmTestTypes();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDrivers();
            frm.ShowDialog();
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowDatilesUser(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }
    }
}
