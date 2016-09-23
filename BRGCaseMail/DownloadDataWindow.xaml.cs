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
    /// Interaction logic for DownloadDataWindow.xaml
    /// </summary>
    
    public partial class DownloadDataWindow : Window
    {
        Court _court;

        public DownloadDataWindow(Court court)
        {
            InitializeComponent();
            _court = court;

            gbCourt.Header += " " + _court.CourtName;
            lblFilePrefix.Content = _court.FilePrefix;
            lblWebsite.Content =  _court.URLFull;

            rdpStartDate.SelectedDate = DateTime.Now;
            rdpEndDate.SelectedDate = DateTime.Now;
            rdpStartDate.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnDownLoad_Click(object sender, RoutedEventArgs e)
        {

            PacerImportTransaction _transaction = new PacerImportTransaction() { DischargedCases = (bool)rdoDischarged.IsChecked, StartDate = (DateTime)rdpStartDate.SelectedDate, EndDate = (DateTime)rdpEndDate.SelectedDate, CourtID = _court.ID, PacerFileFormatID= _court.PacerFileFormatID };

            Mouse.OverrideCursor = Cursors.Wait;

            if (_transaction.QueryNewCases() == true)
            {
                if (MessageBox.Show("Do you wish to download and pay for " + _transaction.BillablePages.ToString() + " billable pages for " + _transaction.Cost.ToString("C"), "Continue?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (_transaction.DownloadNewCases(true) == true)
                    {
                        Mouse.OverrideCursor = Cursors.Arrow;
                        MessageBox.Show(_transaction.ImportMessage);
                        this.DialogResult = true;
                        this.Close();
                    }
                    else 
                    {
                        Mouse.OverrideCursor = Cursors.Arrow;
                        MessageBox.Show(_transaction.ImportMessage);                    
                    }
                }
                Mouse.OverrideCursor = Cursors.Arrow;
            }
            else
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                MessageBox.Show(_transaction.ImportMessage);
            }

        }

    }
}
