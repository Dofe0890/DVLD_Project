using ContactBusinessLayer;
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
    public partial class ctrlUserCard : UserControl
    {
        private clsUsers _User;
        private int _UserID = -1;

        public int UserID
        {
            get { return _UserID; }
        }


        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            _User = clsUsers.Find(UserID);
            if(_User == null )
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {
            ctrlPersonCardInfoo1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.Username .ToString();
            if (_User.IsActive)
            {
                lblIsActive.Text = "Yes";
            }
            else
                lblIsActive.Text = "No";
        }
        private void _ResetPersonInfo()
        {

            ctrlPersonCardInfoo1.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }

        private void ctrlUserCard_Load(object sender, EventArgs e)
        {

        }
    }
}
