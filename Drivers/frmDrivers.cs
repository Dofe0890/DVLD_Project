using ContactBusinessLayer;
using DVlD_Project.Licenese;
using DVlD_Project.Peoplee;
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
using DVlD_Project.Licenese;
using DVlD_Project.Licenese.New_International_License;
namespace DVlD_Project.Drivers
{
    public partial class frmDrivers : Form
    {

        private DataTable _dtAllDrivers;
        public frmDrivers()
        {
            InitializeComponent();
        }

        private void frmDrivers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dtAllDrivers = clsDrivers.GetAllDrivers();
            dgvGetAllDrivers.DataSource = _dtAllDrivers;
            lblRecordsCount.Text = dgvGetAllDrivers.Rows.Count.ToString();
            if (dgvGetAllDrivers.Rows.Count > 0)
            {
                dgvGetAllDrivers.Columns[0].HeaderText = "Driver ID";
                dgvGetAllDrivers.Columns[0].Width = 120;
                
                dgvGetAllDrivers.Columns[1].HeaderText = "Person ID";
                dgvGetAllDrivers.Columns[1].Width = 120;
                
                dgvGetAllDrivers.Columns[2].HeaderText = "National No.";
                dgvGetAllDrivers.Columns[2].Width = 140;
                
                dgvGetAllDrivers.Columns[3].HeaderText = "Full Name";
                dgvGetAllDrivers.Columns[3].Width = 320;
                
                dgvGetAllDrivers.Columns[4].HeaderText = "Date";
                dgvGetAllDrivers.Columns[4].Width = 170;
                
                dgvGetAllDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvGetAllDrivers.Columns[5].Width = 150;
            }
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvGetAllDrivers.CurrentRow.Cells[1].Value;
            frmShowDatiles frm = new frmShowDatiles(PersonID);
            frm.ShowDialog();
            
            frmDrivers_Load(null, null);
        }

        private void showPersonLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvGetAllDrivers.CurrentRow.Cells[1].Value;


            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void txtFilterValue_TextChanged_1(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvGetAllDrivers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
                //in this case we deal with numbers not string.
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtAllDrivers.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");


            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Enabled = false;
            }
            else
                txtFilterValue.Enabled = true;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
