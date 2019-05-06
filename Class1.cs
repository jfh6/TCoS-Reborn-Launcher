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
using Microsoft.Win32;
using Spellborn_Reborn_Luancher;

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
            // Check if we can access the Game directory and if the version is correct.
            if (Directory.Exists(location + "/TheChroniclesofSpellborn" + version + "/"))
            {
                //Use Config file info to create the launch path and start the process.
                Process.Start(location + "/TheChroniclesofSpellborn" + version + "/bin/client/sb_client.exe");
                //Close Launcher after game launch
                Environment.Exit(0);
            }
            else
            {
                // display error if install path can't be found.
                MessageBox.Show("Error: Could not find Install location! Please Update your client to the latest Version." +
                    location + "/TheChroniclesofSpellborn" + version + "/bin/client/sb_client.exe");
                return;
            }
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
            ZipFile.ExtractToDirectory("C:/Windows/TEMP/tcosSetup", installPath);
        }
        public static void updatePath(string installPath)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Spellborn Fan Hub\\Patcher"))
                {
                    if (key != null)
                    {
                        //update value for install location
                        key.SetValue("installPath", installPath);
                    }
                    else
                    {
                        //Create the subkey if it doesn't exist
                        Registry.LocalMachine.CreateSubKey("Software\\Spellborn Fan Hub\\Patcher");
                        key.SetValue("installPath", installPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void updateVersion(string version)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Spellborn Fan Hub\\Patcher"))
                {
                    if (key != null)
                    {
                        //update value for game version
                        key.SetValue("version", version);
                    }
                    else
                    {
                        //Create the subkey if it doesn't exist
                        Registry.LocalMachine.CreateSubKey("Software\\Spellborn Fan Hub\\Patcher");
                        key.SetValue("version", version);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static string GetVersion()
        {
            GameInfo g;
            g = new GameInfo();
            g.LoadInfo();
            string version = g.version;
            return version;
        }
        public static string GetFile()
        {
            GameInfo g;
            g = new GameInfo();
            g.LoadInfo();
            string File = g.file;
            return File;
        }
        public static string GetChecksum()
        {
            GameInfo g;
            g = new GameInfo();
            g.LoadInfo();
            string Checksum = g.checksum;
            return Checksum;
        }
    }
}
