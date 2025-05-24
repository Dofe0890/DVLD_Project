using ContactsBusinessLayer;
using PeopleDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsDataAccessLayer;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace ContactBusinessLayer
{
    public class LicenseClasses
    {

    
        public int LicenseClassID { set; get; }

        public string ClassName { set; get; }
        
        public string ClassDescription {  set; get; }

        public byte MinimumAllowedAge { set; get; }

        public byte DefaultValidityLength {  set; get; }

        public double ClassFees {  set; get; }

            public LicenseClasses()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = -1;
        }
     private LicenseClasses(int LicenseClassID , string ClassName ,string ClassDescription,byte MinimumAllowedAge,byte DefaultValidityLength,double ClassFees )
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge =MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

        }


        public static LicenseClasses FoundClass(int  LicenseClassID)
        {
         
           string  ClassName = "";
           string  ClassDescription = "";
           byte  MinimumAllowedAge = 0;
           byte  DefaultValidityLength = 0;
           double  ClassFees = -1;

            if (LicensesClassesData.GetClassByID(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new LicenseClasses(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
                return null;
        }


        public static LicenseClasses FoundClass(string ClassName)
        {

           int LicenseClassID = -1;
            
            string ClassDescription = "";
            byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;
            double ClassFees = -1;

            if (LicensesClassesData.GetClassByName(ClassName, ref LicenseClassID , ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new LicenseClasses(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
                return null;
        }



        public static DataTable GetAllClasses()
        {
            return LicensesClassesData.GetAllClasses();

        }

    }
}
