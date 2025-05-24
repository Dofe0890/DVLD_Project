using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD_Project.Licenese.New_Local_License
{
    public partial class frmShowLocalLicense : Form
    {
        private int _LicenseID = -1;
        public frmShowLocalLicense(int LicenseID)
        {
            _LicenseID = LicenseID;
            InitializeComponent();
        }

        private void frmShowLocalLicense_Load(object sender, EventArgs e)
        {
         ctrlDriverLicenseInfo1.LoadInfo (_LicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this .Close();
        }
    }
}
