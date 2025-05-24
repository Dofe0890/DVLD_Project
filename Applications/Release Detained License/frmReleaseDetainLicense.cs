using ContactBusinessLayer;
using CurrentUser;
using DVlD_Project.Global_Classes;
using DVlD_Project.Licenese;
using DVlD_Project.Licenese.New_Local_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD_Project.Applicationiens.Datain_Licenses
{
    public partial class frmReleaseDetainLicense : Form
    {

        private int _SelectedLicenseID = -1;

     
        public frmReleaseDetainLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainLicense(int  licenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = licenseID;

            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(licenseID);
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }

      

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReleaseDetainLicense_Load(object sender, EventArgs e)
        {
           
        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;


            bool IsReleased = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.ReleaseDetainLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID); 

            lblApplicationID.Text = ApplicationID.ToString();

            if (!IsReleased)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            button1.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            llShowLicensesHestory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)

            {
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationFees.Text = clsApplicationTypes.FoundApplicationType((int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicense).ApplicationTypeFee.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;

            lblDetainID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainInfo.DetainID.ToString();
            lblLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID.ToString();

            lblCreatedByUser.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainInfo.CreatedByUserInfo.Username;
            lblDetainDate.Text = clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainInfo.DetainDate);
            lblFineFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToDouble(lblFineFees.Text)).ToString();

            button1.Enabled = true;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLocalLicense(_SelectedLicenseID);
            frm.ShowDialog();

        }

        private void llShowLicensesHestory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void frmReleaseDetainLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }
    }
}
