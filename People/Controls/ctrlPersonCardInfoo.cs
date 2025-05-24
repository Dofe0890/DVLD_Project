using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer;
using DVlD_Project.Properties;
using static DVlD_Project.frmAddOrEditPerson;
using System.IO;
namespace DVlD_Project
{
    public partial class ctrlPersonCardInfoo : UserControl
    {

        private clsPerson _Person;

        private int _PersonID = -1;
        public clsPerson SelectedPerson { get { return _Person; } }

        public int PersonID { get { return _PersonID; } }


        public ctrlPersonCardInfoo()
        {
            InitializeComponent();
        }
       
        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            _FillPersonInfo();
        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with NationalNo = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }

        private void _LoadPersonImage ()
        {
            if (_Person.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
           if(ImagePath != "") 
                if (File.Exists(ImagePath))
                {
                    pbPersonImage.ImageLocation = ImagePath;
                }
                else
                {
                    MessageBox.Show($"Image not found: {ImagePath}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    pbPersonImage.Image = Resources.person_man; // Default image
                }
            
        }
        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.ID ;
            lblPersonID.Text = _Person.ID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblFullName.Text = _Person.FullName;
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text = clsCountry.Find(_Person.NationalCountryID).CountryName;
            lblAddress.Text = _Person.Address;
            _LoadPersonImage();
            



        }
        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.Man_32;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;

        }
     

  
      

        private void ucPersonInfoo_Load(object sender, EventArgs e)
        {
            
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddOrEditPerson frm = new frmAddOrEditPerson(_PersonID);
            frm.ShowDialog();

            LoadPersonInfo(PersonID);
        }
    }
}
