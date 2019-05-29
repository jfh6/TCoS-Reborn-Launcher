
namespace TCoS_Reborn_Launcher
{
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
    using System.Configuration;
    using System.Collections.Specialized;
    using Microsoft.Win32;

        public partial class MainWindow : Window
        {
            public MainWindow()
            {           
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Spellborn Fan Hub\\Patcher", true))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue("Version");
                        if (o != null)
                        {
                            Version version = new Version(o as String);  //"as" because it's REG_SZ...otherwise ToString() might be safe(r)

                            string Sversion = Launcher.GetVersion();

                            if (version.ToString() != Sversion)
                            {
                                InitializeComponent();
                                MessageBox.Show("Version Mismatch ~ Client Version: " + version + " Server Version: " + Sversion);
                            }
                            else
                            {
                                InitializeComponent();
                            }
                            

                        }
                    }
                    else
                    {
                        Launcher.updateVersion("1.1.0.4");

                    }
                }
            }
            catch (Exception ex)
            {
                //react appropriately
                MessageBox.Show(ex.ToString());
            }          

                               
             
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                Launcher.LaunchWebsite("http://TCoS.org");
            }

            private void Play_Now_Button_Click_1(object sender, RoutedEventArgs e)
            {
                Launcher.PlayGame();
            }

            private void Exit_Click(object sender, RoutedEventArgs e)
            {
                Launcher.Exit();
            }

// --Start Copied Code from Kevin Petit's Version--
            WebClient webClient;

            public void DownloadFile(string urlAddress, string location)
            {
                using (webClient = new WebClient())
                {
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);


                    Uri URL = urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("http://" + urlAddress);

                    try
                    {
                        webClient.DownloadFileAsync(URL, location);
                    }
                    catch (Exception ex)
                    {
                    MessageBox.Show(ex.Message + "  " + URL);

                    }
                }

                void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
                {


                    downloadProgress.Value = e.ProgressPercentage;

                    LdownloadProgress.Text = e.ProgressPercentage.ToString() + "%";
                }

                void Completed(object sender, AsyncCompletedEventArgs e)
                {

                    if (e.Error != null)
                    {
                        string error = e.Error.ToString();

                        MessageBox.Show(error);
                        return;
                    }
                    if (e.Cancelled == true)
                    {
                        MessageBox.Show("Download has been canceled.");
                    }
                    else
                    {
                     // Alert on download complete
                        MessageBox.Show("Client Downloaded Please wait for Install Confirmation!");
                     // Run Intall function with textbox input to update config file and extract game files
                        Launcher.InstallGame(installPath.Text.ToString());               
                     // Alert on Completion
                        MessageBox.Show("TCoS Installed Successfully!");
                    }
                }
            }
// --End Copied Code--
            private void Button_Click_1(object sender, RoutedEventArgs e)
            {
             // Download the file and save it to the static Dir.
                DownloadFile("http://files.Spellborn.org/latest.zip", "C:/Windows/TEMP/latest.zip");
            }

            private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

            private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            {

            }

            private void Button_Click_2(object sender, RoutedEventArgs e)
            {
             // Run update function with textbox input to update config file with install location
                Launcher.updatePath(installPath.Text);
            MessageBox.Show("Game Location registered to: " + Launcher.GetInstallPath());
            }

            private void Button_Click_3(object sender, RoutedEventArgs e)
            {
                Launcher.LaunchWebsite("http://Spellborn.nl");
            }
        }
    }
