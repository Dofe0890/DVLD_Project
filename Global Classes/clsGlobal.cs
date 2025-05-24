using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactBusinessLayer;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace CurrentUser
{
    public class clsGlobal 
    {
        public static clsUsers CurrentUser;


        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            string keyPath = @"SOFTWARE\DVLD";


            if (string.IsNullOrEmpty(Username))
            {
                try {
                    using (RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\DVLD", writable: true))
                    {
                        if (Key != null)
                        {
                            Key.DeleteValue(Username, throwOnMissingValue: false);
                            Key.DeleteValue(Password, throwOnMissingValue: false);
                        }
                    }
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            try
            {
                using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(keyPath, writable: true))
                {
                    if (Key != null)
                    {
                        Key.SetValue("Username", Username, RegistryValueKind.String);
                        Key.SetValue("Password", Password ,RegistryValueKind.String);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
               
            }
            catch (Exception ex)
            {
                return false;
            }
            }

        public static bool GetStoredCredntail(ref string Username, ref string Password)
        {

            string kyePath = @"SOFTWARE\DVLD";

            try {

                using (RegistryKey Key = Registry.CurrentUser.OpenSubKey(kyePath, writable: true))
                {
                    if (Key != null)
                    {

                        string UsernameVal = Key.GetValue("Username") as string;
                        string PasswordVal = Key.GetValue("Password") as string;

                        if (UsernameVal != null && PasswordVal != null)
                        {
                            Username = UsernameVal;
                            Password = PasswordVal;
                            return true;


                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
            




        }

     

    }
}
