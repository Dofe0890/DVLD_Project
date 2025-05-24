using ContactBusinessLayer;
using DVlD_Project.Global_Classes;
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
using static ContactBusinessLayer.clsApplications;

namespace DVlD_Project.Applicationiens
{
    
    public partial class frmUpdateApplicationTypes : Form
    {
      private clsApplicationTypes _ApplicationType;
        private int _ApplicationTypeID = -1;

        public frmUpdateApplicationTypes(int ApplicationTypeID )
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;

        }

        private void frmUpdateApplicationTypes_Load(object sender, EventArgs e)
        {
            lblID.Text = _ApplicationTypeID.ToString();

            _ApplicationType = clsApplicationTypes.FoundApplicationType(_ApplicationTypeID);

            if (_ApplicationType != null)
            {
                textTitle.Text = _ApplicationType.ApplicationTypeTitle;
                textFees.Text = _ApplicationType.ApplicationTypeFee.ToString();


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid! , put the mouse over the icons to see the error","Valdtion Erorr",MessageBoxButtons.OK , MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.ApplicationTypeTitle = textTitle.Text.Trim();
            _ApplicationType.ApplicationTypeFee = Convert.ToInt32(textFees.Text.Trim());    
            if(_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Erorr: Data Its Not Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

      

     

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(textTitle, null);
            }
        }

        private void textFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(textTitle, "Fees cannot be empty!");
                return;
            }
            else
            {

                errorProvider1.SetError(textTitle, null);
            }

            if(!clsValidation.IsNumber(textFees.Text))
            {

                e.Cancel = true;

                errorProvider1.SetError(textFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(textFees, null);
            };

        }

           




        
    }
}
