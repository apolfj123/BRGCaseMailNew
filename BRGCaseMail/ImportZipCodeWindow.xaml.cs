using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BRGCaseMailServer;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using Microsoft.SqlServer.Dts.Runtime;
using Telerik.Windows.Controls;
using System.ComponentModel;

namespace BRGCaseMail
{
    /// <summary>
    /// Interaction logic for ImportZipCodeWindow.xaml
    /// </summary>
    
    public partial class ImportZipCodeWindow : Window
    {

        public ImportZipCodeWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (txtFilePath.Text != string.Empty)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                string _returnMessage = ZipCodeImportService.ProcessZipCodeFile(txtFilePath.Text);
                if (_returnMessage.Contains("Success!"))
                {    
                    Mouse.OverrideCursor = Cursors.Arrow;
                    MessageBox.Show("Zip Import was successful!");
                    this.Close();
                }
                else
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                    MessageBox.Show("Error: Zip Import was unsuccessful, please contact your administrator!" + ": " + _returnMessage);
                };   
            }
            else
            {
                MessageBox.Show("Please select as filepath!");
            }
        }

        private void btnSelectZipCodeFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Zip Code CSV files (.csv) | *.csv";
            dlg.InitialDirectory = ConfigurationManager.AppSettings.Get("TemplateFilePath");

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
            
                // Open document 
                string filename = dlg.FileName;
                txtFilePath.Text = filename;
            }
        }

    }
}
