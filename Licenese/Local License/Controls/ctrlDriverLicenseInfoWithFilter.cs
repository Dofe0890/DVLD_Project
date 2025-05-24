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

namespace DVlD_Project.Licenese.Local_License.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {

        public event Action<int> OnLicenseSelected;

        protected virtual void PersonSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;

            if(handler != null)
            {
               handler(LicenseID);
            }




        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }

        public clsLicenses SelectedLicenseInfo
        {
                get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; }
        }


        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }



        public void LoadLicenseInfo(int LicenseID)
        {

            txtLicenseID.Text = LicenseID.ToString();

            ctrlDriverLicenseInfo1.LoadInfo (LicenseID);

            _LicenseID = LicenseID;

            if(OnLicenseSelected  != null && FilterEnabled)
                OnLicenseSelected (LicenseID);


        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }
      
        

        private void ctrlDriverLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {

        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);

            if(e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
        }

       

      

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;
            }

            _LicenseID = int.Parse(txtLicenseID.Text.Trim());

            LoadLicenseInfo(_LicenseID);
        }

        private void txtLicenseID_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required !");
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, null);
            }
        }
    }
}
