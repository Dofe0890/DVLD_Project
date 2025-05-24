using ContactBusinessLayer;
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

namespace DVlD_Project.Users
{
    public partial class frmShowDatilesUser : Form
    {

        private int _UserID;

        public frmShowDatilesUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void frmShowDatilesUser_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void btnCloseN_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
