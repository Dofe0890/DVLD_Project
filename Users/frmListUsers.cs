using ContactBusinessLayer;
using ContactsBusinessLayer;
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
namespace DVlD_Project.Users
{
    public partial class frmListUsers : Form
    {

        private static DataTable _dtAllUsers;

        public frmListUsers()
        {
            InitializeComponent();

        }
      

        private void frmAllUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUsers.GetAllUsers();
            dgvAllUsers.DataSource = _dtAllUsers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvAllUsers .Rows.Count .ToString();

            dgvAllUsers.Columns[0].HeaderText = "UserID";
            dgvAllUsers.Columns[0].Width = 110;

            dgvAllUsers.Columns[1].HeaderText = "Person ID";
            dgvAllUsers.Columns[1].Width = 120;

            dgvAllUsers.Columns[2].HeaderText = "Full Name";
            dgvAllUsers.Columns[2].Width = 350;

            dgvAllUsers.Columns[3].HeaderText = "UserName";
            dgvAllUsers.Columns[3].Width = 120;

            dgvAllUsers.Columns[4].HeaderText = "Is Active";
            dgvAllUsers.Columns[4].Width = 120;

        }

        private void showDateilsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowDatilesUser((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvAllUsers.CurrentRow.Cells[0].Value;
            if (clsUsers.DeleteUser(UserID))
            {
                MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmAllUsers_Load(null, null);
            }

            else
                MessageBox.Show("User is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddnewUser();
            frm.ShowDialog();
            frmAllUsers_Load(null, null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        
            Form frm = new frmAddnewUser((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmAllUsers_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void changePasswordStripMenuItem2_Click(object sender, EventArgs e)
        {
            int UserID =(int) dgvAllUsers.CurrentRow.Cells[0].Value;
            frmChangePassword frm = new frmChangePassword(UserID);
            frm.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true ;
                cbIsActive.Focus();
                cbIsActive .SelectedIndex = 0 ;
            }
            else
            {


                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;
                if(cbFilterBy .Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else
                {
                    txtFilterValue.Enabled = true;

                }
                txtFilterValue.Text = "";
                txtFilterValue.Focus ();
                
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
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


            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = _dtAllUsers.Rows.Count.ToString();

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch(cbFilterBy.Text)
            {

                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (txtFilterValue.Text.Trim() =="" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvAllUsers.Rows .Count .ToString ();
                return;
            }

            if(FilterColumn != "FullName"&&FilterColumn != "UserName")
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            lblRecordsCount.Text = _dtAllUsers.Rows.Count.ToString();

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddnewUser();
            frm.ShowDialog();
           frmAllUsers_Load(null,null);
        }

        private void dgvAllUsers_DoubleClick(object sender, EventArgs e)
        {
            frmShowDatilesUser frm = new frmShowDatilesUser((int)dgvAllUsers.CurrentRow .Cells[0].Value);
            frm.ShowDialog();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Person ID" || cbFilterBy .Text == "User ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar)&& !char.IsControl(e.KeyChar);
            }
        }
    }
}
