using ContactBusinessLayer;
using DVlD_Project.Applicationiens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Applicationiens
{
    public partial class frmApplicationsTypes : Form
    {

        private DataTable _dtAllApplicationTypes;
        public frmApplicationsTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmApplicationsTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationTypes.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtAllApplicationTypes;

            dgvApplicationTypes.Columns[0].HeaderText = "ID";
            dgvApplicationTypes.Columns[0].Width = 110;

            dgvApplicationTypes.Columns[1].HeaderText = "Title";
            dgvApplicationTypes.Columns[1].Width = 400;

            dgvApplicationTypes.Columns[2].HeaderText = "Fees";
            dgvApplicationTypes.Columns[2].Width = 100;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
 
            Form frm = new frmUpdateApplicationTypes((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmApplicationsTypes_Load(null, null);
        }

        private void dgvApplicationTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
