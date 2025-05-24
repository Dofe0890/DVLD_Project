using ContactBusinessLayer;
using DVlD_Project.Liceses.New_Local_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVlD_Project.Tests;
using System.Windows.Forms;
using DVlD_Project.Licenese.Local_License;

namespace DVlD_Project.Licenese.New_Local_License
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
   


        private DataTable _dtAllLocalDrivingLicenseApplications;
        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingApplication.GetAllLocalDrivingLicenseApplications();
                dgvGetAllLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;

            lblRecordsCount.Text = dgvGetAllLocalDrivingLicenseApplications.Rows.Count.ToString();
            if (dgvGetAllLocalDrivingLicenseApplications.Rows.Count > 0)
            {

                dgvGetAllLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvGetAllLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvGetAllLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvGetAllLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvGetAllLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvGetAllLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvGetAllLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvGetAllLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvGetAllLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvGetAllLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvGetAllLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvGetAllLocalDrivingLicenseApplications.Columns[5].Width = 150;
            }

            cmbFindBy.SelectedIndex = 0;


        }
      
        private void btnAddNewLocalLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddNewLocalLicense();
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null,null);
     
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

                int _LocalDrivingApplicationID = (int)dgvGetAllLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
                Form frm = new frmAddNewLocalLicense(_LocalDrivingApplicationID);
                frm.ShowDialog();
               frmLocalDrivingLicenseApplications_Load(null , null);    
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            int ID = (int)dgvGetAllLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            int LocalDrivingApplicationID = (int)dgvGetAllLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingApplication localDrivingApplication = clsLocalDrivingApplication.FindLocalDrivingLicenseApplication(LocalDrivingApplicationID);

            if (localDrivingApplication != null)
            {
                if (localDrivingApplication.Cancel())
                {
                    MessageBox.Show("Application cancelled Successfully.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmLocalDrivingLicenseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


            }
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingApplicationID = (int)dgvGetAllLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
           clsLocalDrivingApplication localDrivingApplication = clsLocalDrivingApplication.FindLocalDrivingLicenseApplication(LocalDrivingApplicationID );

           if ( localDrivingApplication != null )
            {
                if(localDrivingApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                    frmLocalDrivingLicenseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete application, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }

        private void _ScheduleTest(clsTestTypes.enTestTypes TestType)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvGetAllLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmListTestAppointment frm = new frmListTestAppointment(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);

        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestTypes.VisionTest);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestTypes.WritingTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestTypes.StreetTest);

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingApplicationID = (int)dgvGetAllLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            Form frm = new frmIssueNewLocalLicense(LocalDrivingApplicationID);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null,null);
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvGetAllLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingApplication LocalDrivingLicenseApplication = clsLocalDrivingApplication.FindLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
      
            int TotalPassedTests = (int)dgvGetAllLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;

            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();


            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests ==3) && !LicenseExists;
            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplications.enApplicationStatus.New);

            cancelApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus== clsApplications.enApplicationStatus.New) ;

            deleteApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsApplications.enApplicationStatus.New);


            bool PassedVisionTest = LocalDrivingLicenseApplication.DosePassTestType(clsTestTypes.enTestTypes.VisionTest);
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DosePassTestType(clsTestTypes.enTestTypes.WritingTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DosePassTestType(clsTestTypes.enTestTypes.StreetTest);

            scheduleTestMenueToolStripMenuItem.Enabled = !LicenseExists &&
    LocalDrivingLicenseApplication.ApplicationStatus != clsApplications.enApplicationStatus.Cancelled && (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest);

            if(scheduleTestMenueToolStripMenuItem.Enabled )
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest&&!PassedWrittenTest;
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest&&PassedWrittenTest&& !PassedStreetTest;

            }
            
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           int LocalDrivingApplicationID   = (int)dgvGetAllLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
           int LicenseID  = clsLocalDrivingApplication.FindLocalDrivingLicenseApplication(LocalDrivingApplicationID).GetActiveLicenseID();


            if (LicenseID != -1)
            {
                Form frm = new frmShowLocalLicense(LicenseID);
                frm.ShowDialog();
            }
            else
            {

                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSearch.Visible = (cmbFindBy.Text != "None");

            if (tbSearch .Visible)
            {
                tbSearch .Text = "";
                tbSearch .Focus();
            }

            _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvGetAllLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cmbFindBy.Text)
            {

                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

         
            if (tbSearch.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvGetAllLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "LocalDrivingLicenseApplicationID")
             
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, tbSearch .Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, tbSearch.Text.Trim());

            lblRecordsCount.Text = dgvGetAllLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFindBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvGetAllLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null,null);
            
        }

        private void showPersonLicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvGetAllLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingApplication  localDrivingLicenseApplication = clsLocalDrivingApplication.FindLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(localDrivingLicenseApplication.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
