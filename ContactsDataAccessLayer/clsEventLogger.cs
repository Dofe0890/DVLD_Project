using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace ContactsDataAccessLayer
{
    public class clsEventLogger
    {
        static public void  WriteEventLogger(Exception Ex)
        {
        string SourceName = "DVLD";
        try
        {
           if(!EventLog.SourceExists(SourceName))
           {
                EventLog.CreateEventSource(SourceName , "Application");
           }
           if(Ex != null)
           {
                EventLog.WriteEntry(SourceName, Ex.ToString(), EventLogEntryType.Error);
           }


        }
        catch(Exception ex )
            {
                try { 
                MessageBox.Show(
                      "Administrator privileges are required to create the event log source.\n\n" +
                      "Please run the application as Administrator one time to complete setup.",
                      "Permission Required",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Warning);
            }
            catch (Exception x)
            {
                MessageBox.Show(
                    "An error occurred while writing to the event log:\n\n" + x.Message,
                    "Logging Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        }
        


    }
}
