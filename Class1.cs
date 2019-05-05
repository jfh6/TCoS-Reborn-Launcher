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
            string version = ConfigurationManager.AppSettings.Get("gameVersion");
            string location = ConfigurationManager.AppSettings.Get("installPath");
            Process.Start(location + "/TheChroniclesofTCoS" + version + "/bin/client/sb_client.exe");
            Environment.Exit(0);
        }
        public static void LaunchWebsite(string url)
        {
            Process.Start(url);
        }
        public static void Exit()
        {
            Environment.Exit(01);
        }
        public static void InstallGame(string installPath)
        {
            ZipFile.ExtractToDirectory("C:/Games/tcosSetup", installPath);

            Configuration config =
            ConfigurationManager.OpenExeConfiguration
                        (ConfigurationUserLevel.None);

            config.AppSettings.Settings["installPath"].Value = installPath;

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }
        public static void updatePath(string installPath)
        {
            Configuration config =
            ConfigurationManager.OpenExeConfiguration
                        (ConfigurationUserLevel.None);

            config.AppSettings.Settings["installPath"].Value = installPath;

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
