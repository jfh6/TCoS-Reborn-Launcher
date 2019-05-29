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
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace TCoS_Reborn_Launcher
{
    class Launcher
    {

        public static void PlayGame()
        {
            //Function to Launch sb_client.exe
            //Get version from server
            string version = Launcher.GetVersion();
            //Get install path from registry
            string location = Launcher.GetInstallPath();
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
            string downloadPath = "C:/Windows/TEMP/latest.zip";
            //Push Intall location to registry
            updatePath(installPath);

            if(CheckChecksum(downloadPath) == true){

            //Unzip download to Install location
            ZipFile.ExtractToDirectory(downloadPath, installPath);

            }else{
                MessageBox.Show("Error: File Was corrupted durning download. Please try again!" + "Server Checksum: " + GetChecksum() + " Client Checksum: " + getDownloadChecksum(downloadPath));
            }                
        }
        public static void updatePath(string installPath)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Spellborn Fan Hub\\Patcher", true))
                {
                    if (key != null)
                    {
                        //update value for install location
                        key.SetValue("installPath", installPath);
                    }
                    else
                    {
                        //Create the subkey if it doesn't exist
                        using (RegistryKey key2 = Registry.LocalMachine.CreateSubKey("Software\\Spellborn Fan Hub\\Patcher", true))
                        {
                            key2.SetValue("installPath", installPath);
                            MessageBox.Show(key2.ToString());
                        }
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
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Spellborn Fan Hub\\Patcher", true))
                {
                    if (key != null)
                    {
                        //update value for game version
                        key.SetValue("version", version);
                        MessageBox.Show(key.ToString());
                    }
                    else
                    {
                        //Create the subkey if it doesn't exist
                        using (RegistryKey key2 = Registry.LocalMachine.CreateSubKey("Software\\Spellborn Fan Hub\\Patcher", true))
                        {
                            key2.SetValue("version", version);
                            MessageBox.Show(key2.ToString());
                        }
                        
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
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("http://files.spellborn.org/latest.json");
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();

            var items = JsonConvert.DeserializeObject<dynamic>(str);

            return items.version;
        }
        public static string GetFile()
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("http://files.spellborn.org/latest.json");
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();

            var items = JsonConvert.DeserializeObject<dynamic>(str);

            return items.file;
        }
        public static string GetChecksum()
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("http://files.spellborn.org/latest.json");
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();

            var items = JsonConvert.DeserializeObject<dynamic>(str);

            return items.checksum;
        }
        public static string getDownloadChecksum(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    byte[] localChecksum = md5.ComputeHash(stream);
                    string lChecksum = BitConverter.ToString(localChecksum).Replace("-", string.Empty).ToLower();
                    return lChecksum;
                }
            }
        }
        public static bool CheckChecksum(string filename)
        {
                using (var md5 = MD5.Create())
                {
                using (var stream = File.OpenRead(filename))
                {
                byte[] localChecksum = md5.ComputeHash(stream);
                string lChecksum = BitConverter.ToString(localChecksum).Replace("-", string.Empty).ToLower();
                if ( lChecksum == GetChecksum())
                {
                    return true;
                }else
                {
                    return false;
                }
               }
            }
        }
        public static string GetInstallPath()
        {
        using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Spellborn Fan Hub\\Patcher", true))
            {
            Object o = key.GetValue("installPath");
            return o.ToString();                                                          
            }

        }
        public static string RegGetVersion()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Spellborn Fan Hub\\Patcher", true))
            {
                Object o = key.GetValue("version");
                return o.ToString();
            }

        }
    }
}
