using ContactBusinessLayer;
using CurrentUser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD_Project.Tests
{
    public partial class frmTakeTest : Form
    {
        private int  _AppointmentID = -1 ;
        private clsTestTypes.enTestTypes _TestTypeID;

        private int _TestID = -1;
        private clsTest _Test;

        
        public frmTakeTest(int AppointmentID , clsTestTypes.enTestTypes TestTypeID )
        {

            InitializeComponent();
            _AppointmentID = AppointmentID ;
            _TestTypeID = TestTypeID ;
        
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this. Close();  
        }

        private void btnSave_Click(object sender, EventArgs e)
        {



            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No
           )
            {
                return;
            }

            _Test.TestAppointmentID = _AppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedUserID = clsGlobal.CurrentUser.UserID;

            if (_Test.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlTakeTest1.TestTypesID = _TestTypeID;

            ctrlTakeTest1.LoadData(_AppointmentID);

            if(ctrlTakeTest1 .TestAppointmentID == -1)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }
            
            int _TestID = ctrlTakeTest1 .TestID ;
            if (_TestID != -1)
            {
                _Test = clsTest.FindTest(_TestID);
                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbPass.Checked = false;

                txtNotes.Text = _Test.Notes;
                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
            }
            else
            {
                _Test = new clsTest();  
            }


        }
    }
}
