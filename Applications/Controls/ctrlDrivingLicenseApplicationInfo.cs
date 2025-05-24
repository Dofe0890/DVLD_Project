using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Internal;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactBusinessLayer;
using CurrentUser;
using DVlD_Project.Licenese;
using DVlD_Project.Licenese.New_Local_License;
using DVlD_Project.Peoplee;
namespace DVlD_Project
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {

        private int _LicenseID;

        private  int _LocalDrivingLicenseApplicationID = -1;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }


        private clsLocalDrivingApplication LDLA;
     
  
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void ucDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {


        }


        private void _ResetLDLA()
        {
            lblLDLAID.Text = "[????]";
            lblAppliedForLicens.Text = "[????]";
            lblPassedTests.Text = "[????]";
            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblFees.Text = "[????]";
            lblType.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text =  "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedUser.Text = "[????]";




        }

        private void _FillData()
        {

     _LicenseID = LDLA.GetActiveLicenseID();

            llShowLicenseInfo.Enabled = (_LicenseID != -1);
         


            lblLDLAID.Text = LDLA.LocalDrivingApplicationID.ToString();
            lblAppliedForLicens.Text = LDLA.LicenseClassInfo.ClassName;
            lblPassedTests.Text = LDLA.GetPassedTestCount().ToString()+"/3";



            lblApplicationID.Text = LDLA.ApplicationID.ToString();
            lblStatus.Text = LDLA.StatusText;
            lblFees.Text = LDLA.PaidFees.ToString();
            lblType.Text =LDLA.ApplicationTypeID.ToString();
            lblApplicant.Text = LDLA.ApplicantPersonID.ToString();
            lblDate.Text =LDLA.ApplicationDate.ToString();
            lblStatusDate.Text = LDLA.LastStatusDate.ToString();
            lblCreatedUser.Text = clsGlobal.CurrentUser.Username;


        }


        public void LoadInfoDataByLocalDrivingLicenseID(int LocalDrivingLicenseApplicationID)
        {

        LDLA = clsLocalDrivingApplication .FindLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
                if ( LDLA  == null )
            {
                _ResetLDLA();
                MessageBox.Show("No Application With ID =  " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                _FillData();

        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            
           
            Form frm = new frmShowDatiles(LDLA.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLocalLicense frm = new frmShowLocalLicense(LDLA.GetActiveLicenseID());
            frm.ShowDialog();
        }
    }
}
