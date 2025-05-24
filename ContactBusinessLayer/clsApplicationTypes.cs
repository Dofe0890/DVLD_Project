using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UsersDataAccessLayer;

namespace ContactBusinessLayer
{
   public class clsApplicationTypes
    {
        public enum enMode { AddNew  , Update }
       public enMode Mode = enMode.Update;
        public int ApplicationTypeID {  get; set; }

        public string ApplicationTypeTitle { get; set; }

        public double ApplicationTypeFee { get; set; }


        public clsApplicationTypes() 
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle= "";
            this.ApplicationTypeFee = -1;
            Mode = enMode.AddNew;
        
        
        }

        public clsApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, double ApplicationTypeFee)
        {
            this.ApplicationTypeID =ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationTypeFee = ApplicationTypeFee;
            Mode = enMode.Update;
        }


        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesData.GetAllApplicationTypes();
        }

        public static clsApplicationTypes FoundApplicationType(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            double ApplicationTypeFee = -1;

            bool isFound = clsApplicationTypesData.FindApplicationType(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationTypeFee);

            if (isFound)
            {
                return new clsApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationTypeFee);
            }
            else 
                return null;

        }

        private bool _UpdateApplicationType()
        {


            return clsApplicationTypesData.UpdateApplicationTypes(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationTypeFee);
        }

        private bool _AddNewApplicationType()
        {
            //call DataAccess Layer 

            this.ApplicationTypeID = clsApplicationTypesData.AddNewApplicationType(this.ApplicationTypeTitle , this.ApplicationTypeFee);


            return (this.ApplicationTypeID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplicationType();

            }

            return false;
        }


    }



}
