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

namespace DVlD_Project.Licenese.Local_License
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        private int _LocalDrivingLicenseAppID = -1;

        clsLocalDrivingApplication _LocalDrivingLicenseApp;


        public frmLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseAppID  = LocalDrivingLicenseApplicationID;
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApp = clsLocalDrivingApplication.FindLocalDrivingLicenseApplication(_LocalDrivingLicenseAppID);
            ctrlDrivingLicenseApplicationInfo1.LoadInfoDataByLocalDrivingLicenseID( _LocalDrivingLicenseAppID);
        }
    }
}
