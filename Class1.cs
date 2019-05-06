using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.IO.Compression;
using System.Configuration;
using System.Collections.Specialized;

namespace TCoS_Reborn_Launcher
{
    class Launcher
    {
        
        public static void PlayGame()
        {
            //Function to Launch sb_client.exe
            //Get info from Config file
            string version = ConfigurationManager.AppSettings.Get("gameVersion");
            string location = ConfigurationManager.AppSettings.Get("installPath");
            //Use Config file info to create the launch path and start the process.
            Process.Start(location + "/TheChroniclesofTCoS" + version + "/bin/client/sb_client.exe");
            //Close Launcher after game launch
            Environment.Exit(0);
        }
        public static void LaunchWebsite(string url)
        {
            //automaticlly loads default browser and targets Url
            Process.Start(url);
        }
        public static void Exit()
        {
            //Close the launcher
            Environment.Exit(01);
        }
        public static void InstallGame(string installPath)
        {
            //Push Intall location to Config file
            updatePath(installPath);
            //Unzip download to Install location
            ZipFile.ExtractToDirectory("C:/Games/tcosSetup", installPath);
        }
        public static void updatePath(string installPath)
        {
            //Open Config File
            Configuration config =
            ConfigurationManager.OpenExeConfiguration
                        (ConfigurationUserLevel.None);
            //Write new value for installPath key
            config.AppSettings.Settings["installPath"].Value = installPath;
            //Save Config File
            config.Save(ConfigurationSaveMode.Modified);
            //Refresh Config File
            ConfigurationManager.RefreshSection("appSettings");
        }
        public static void updateVersion(string version)
        {
            //Open Config File
            Configuration config =
            ConfigurationManager.OpenExeConfiguration
                        (ConfigurationUserLevel.None);
            //Write new value for gameVersion key
            config.AppSettings.Settings["gameVersion"].Value = version;
            //Save Config File
            config.Save(ConfigurationSaveMode.Modified);
            //Refresh Config File
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
