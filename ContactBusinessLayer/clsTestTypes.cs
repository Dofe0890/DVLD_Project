using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersDataAccessLayer;

namespace ContactBusinessLayer
{
  public class clsTestTypes
    {
        public enum enMode { AddNew = 1, Update = 2 }
        public enMode Mode = enMode.Update;

   public enum enTestTypes { VisionTest = 1, WritingTest = 2, StreetTest = 3 }

        public enTestTypes TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription {  get; set; }    
        public double TestTypeFees { get; set; }



        public clsTestTypes()
        {
            this.TestTypeID = clsTestTypes.enTestTypes.VisionTest;
            this.TestTypeTitle = "";
            this.TestTypeFees = -1;
            this.TestTypeDescription = "";

            Mode = enMode.AddNew;
        }

        public clsTestTypes(clsTestTypes.enTestTypes TestTypeID, string TestTypeTitle, string TestTypeDescription, double TestTypeFees)
        {
     
          this.TestTypeID= TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeFees = TestTypeFees;
            this.TestTypeDescription = TestTypeDescription;

            Mode = enMode.Update;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }

        public static clsTestTypes FoundTestType(clsTestTypes.enTestTypes TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            double TestTypeFees = -1;

            bool IsFound = clsTestTypesData.FindTestType((int)TestTypeID,ref TestTypeTitle,ref TestTypeDescription,ref TestTypeFees);

            if (IsFound)
            {
                return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
                return null;


        }


        private bool _AddNewTestType()
        {
            //call DataAccess Layer 

            this.TestTypeID = (clsTestTypes.enTestTypes)clsTestTypesData.AddNewTestType(this.TestTypeTitle , this.TestTypeDescription , this.TestTypeFees);

            return (this.TestTypeTitle != "");
        }

        private bool _UpdateTestType()
        {

            return clsTestTypesData.UpdateTestTypes((int)this.TestTypeID ,this. TestTypeTitle,this. TestTypeDescription, this.TestTypeFees);
        
        }


        public bool Save()
        {
            if (Mode == enMode.Update)
            {
              _UpdateTestType();
                return true;
            }
            else { return false; }

        }
    }
}
