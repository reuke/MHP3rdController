using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHP3rdController
{
    static class Setting
    {
        public static string PpssppExePath;
        public static string PpssppDirPath;
        public static string PpssppLogPath;
        public static string MH3rdpIsoPath;
        public static string StatePath;

        public static double ScopeXSens = 0.0833333333333333;
        public static double ScopeYSens = 2;
        public static double CameraXSens = 0.0555555555555556;

        public static void LoadIni()
        {
            string IniPath = System.IO.Directory.GetCurrentDirectory() + "\\MHP3rd.cfg";

            if (!File.Exists(IniPath))
                Environment.Exit(1);
            
            IniFile Ini = new IniFile(IniPath);
            PpssppExePath = Ini.IniReadValue("pathes", "PPSSPP");
            MH3rdpIsoPath = Ini.IniReadValue("pathes", "MH3rdP");
            StatePath = Ini.IniReadValue("pathes", "State");
            PpssppDirPath = PpssppExePath.Substring(0, PpssppExePath.LastIndexOf("\\"));
            PpssppLogPath = PpssppDirPath + "\\ppsspplog.txt";
        }
    }
}
