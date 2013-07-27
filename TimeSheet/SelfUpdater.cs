using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace TimeSheetManger
{
    public static class SelfUpdater
    {
        public static EventHandler OnDownloadUpdatesBegin = null, OnDownloadUpdatesEnd = null;        
        public static bool NeedUpdate
        {
            get
            {
                if (!Directory.Exists(Helper.ServerUpdatePath))
                    return false;
                //var serverVersion = Helper.GetFileVersion(UpdatePath);
                //var selfVersion = Helper.GetFileVersion(Application.ExecutablePath);
                //if (serverVersion != selfVersion)
                    //return true;
                return false;
            }
        }        
        public static void Update()
        {
            if (Path.GetFileName(Application.ExecutablePath).Substring(0, 8) == "Updated_")
            if (OnDownloadUpdatesBegin != null)
                OnDownloadUpdatesBegin(null, EventArgs.Empty);
            var tempname = Path.GetDirectoryName(Application.ExecutablePath) + "\\Updated_" + Path.GetFileName(Application.ExecutablePath);            
            //File.Copy(UpdatePath, tempname, true);            
            if (OnDownloadUpdatesEnd != null)
                OnDownloadUpdatesEnd(null, EventArgs.Empty);
            ProcessStartInfo si = new ProcessStartInfo("cmd", "/C ping 127.0.0.1 -n 2 > nul && "+tempname);
            si.CreateNoWindow = false;
            si.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(si);
            Environment.Exit(0);
        }
    }
}
