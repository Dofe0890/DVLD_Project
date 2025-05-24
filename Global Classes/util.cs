using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD_Project.Global_Classes
{
    public class clsUtil
    {


        public static string GenrateGUID()
        {
            Guid NewGuid = Guid.NewGuid();

            return NewGuid.ToString();
        }

        public static bool CreateFolderIfDoseNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
              
            } 
            return true;
        }

        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            string fileName = sourceFile;
            FileInfo fi = new FileInfo(fileName);
            string extn = fi.Extension;
            return GenrateGUID() + extn;
        }

        public static bool CopyImageToProjectImageFolder(ref string sourceFile)
        {
            string DestinationFolder = @"C:\DVLD-People-Images\";

            if (!CreateFolderIfDoseNotExist(DestinationFolder))
            {
                return false;
            }

            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);


            try
            {

                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            sourceFile = destinationFile;
            return true;
        }
        
    }
}
