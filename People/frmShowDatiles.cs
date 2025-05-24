using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD_Project.Peoplee
{
    public partial class frmShowDatiles : Form
    {
        public frmShowDatiles(int PersonID)
        {
            InitializeComponent();
            ctrlPersonCardInfoo1.LoadPersonInfo(PersonID);
        }
        public frmShowDatiles(string NationalNo)
        {
            InitializeComponent();
            ctrlPersonCardInfoo1.LoadPersonInfo(NationalNo);
        }
    

        private void frmShowDatiles_Load(object sender, EventArgs e)
        {

        }


        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
