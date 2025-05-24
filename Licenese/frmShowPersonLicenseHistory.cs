using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD_Project.Licenese
{
    public partial class frmShowPersonLicenseHistory : Form
    {

        private int _SelectedPersonID = -1;
        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }
      public  frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _SelectedPersonID = PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
            if(_SelectedPersonID == -1)
            {
                ctrlDriverLicenses1.Clear();
            }
            else 
                ctrlDriverLicenses1.LoadInfoByPersonID(_SelectedPersonID);
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if(_SelectedPersonID != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_SelectedPersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                ctrlDriverLicenses1.LoadInfoByPersonID(_SelectedPersonID) ;
            }
            else
            {
                ctrlPersonCardWithFilter1.Enabled = true;
                ctrlPersonCardWithFilter1.FilterFocus();    
            }
        }





    }
}
