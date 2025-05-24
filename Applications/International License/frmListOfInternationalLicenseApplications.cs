using ContactBusinessLayer;
using DVlD_Project.Peoplee;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD_Project.Licenese.New_International_License
{
    public partial class frmListOfInternationalLicenseApplications : Form
    {

        private DataTable _dtInternationalLicenseApplications;


        public frmListOfInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void frmInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalApplications();
            cbFilterBy.SelectedIndex = 0;

            dgvGetAllInternationalLicenseApplications.DataSource = _dtInternationalLicenseApplications;
            lblInternationalLicensesRecords.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();

            if (dgvGetAllInternationalLicenseApplications.Rows.Count > 0)
            {
                dgvGetAllInternationalLicenseApplications.Columns[0].HeaderText = "Int.License ID";
                dgvGetAllInternationalLicenseApplications.Columns[0].Width = 160;

                dgvGetAllInternationalLicenseApplications.Columns[1].HeaderText = "Application ID";
                dgvGetAllInternationalLicenseApplications.Columns[1].Width = 150;

                dgvGetAllInternationalLicenseApplications.Columns[2].HeaderText = "Driver ID";
                dgvGetAllInternationalLicenseApplications.Columns[2].Width = 130;

                dgvGetAllInternationalLicenseApplications.Columns[3].HeaderText = "L.License ID";
                dgvGetAllInternationalLicenseApplications.Columns[3].Width = 130;

                dgvGetAllInternationalLicenseApplications.Columns[4].HeaderText = "Issue Date";
                dgvGetAllInternationalLicenseApplications.Columns[4].Width = 180;

                dgvGetAllInternationalLicenseApplications.Columns[5].HeaderText = "Expiration Date";
                dgvGetAllInternationalLicenseApplications.Columns[5].Width = 180;

                dgvGetAllInternationalLicenseApplications.Columns[6].HeaderText = "Is Active";
                dgvGetAllInternationalLicenseApplications.Columns[6].Width = 120;

            }

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvGetAllInternationalLicenseApplications.CurrentRow.Cells[2].Value;
            int PersonID = clsDrivers.FindDriver(DriverID).PersonID;
            Form frm = new frmShowDatiles(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvGetAllInternationalLicenseApplications.CurrentRow.Cells[0].Value;
            Form frm = new frmShowInternationalLicense(ID);
            frm.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvGetAllInternationalLicenseApplications.CurrentRow.Cells[2].Value;
            int PersonID = clsDrivers.FindDriver(ID).PersonID;
            Form frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true ;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0 ;
            }

            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                   

                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();








            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "Is Active";
            string FilterValue = cbIsReleased.Text;

            switch(FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;

            }

            if(FilterValue == "All")
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
            else
                _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterColumn,FilterValue);

            lblInternationalLicensesRecords.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
          
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    {
                        FilterColumn = "ApplicationID";
                        break;
                    };

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblInternationalLicensesRecords.Text = dgvGetAllInternationalLicenseApplications .Rows.Count.ToString();
                return;
            }



            _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            lblInternationalLicensesRecords.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNewInternationalLicense_Click_1(object sender, EventArgs e)
        {
            Form frm = new frmNewnternationalLicenseApplication();
            frm.ShowDialog();

            frmInternationalLicenseApplications_Load(null, null);

        }
    }
}
