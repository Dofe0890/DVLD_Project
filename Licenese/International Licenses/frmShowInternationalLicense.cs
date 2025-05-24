using Bunifu.Framework.LICENSE;
using ContactBusinessLayer;
using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD_Project.Licenese.New_International_License
{
    public partial class frmShowInternationalLicense : Form
    {
        private int _InternationalID = -1;
     
        public frmShowInternationalLicense(int internationalID)
        {
            InitializeComponent();
            _InternationalID = internationalID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    

        private void frmShowInternationalLicense_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalInfo1.LoadInfo(_InternationalID);
        }
    }
}
