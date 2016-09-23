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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BRGCaseMailServer;
using Telerik.Windows.Controls;
using System.IO;

namespace BRGCaseMail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RawDataWindow : Window
    {

        private PacerImportTransaction _pITransaction;

        public PacerImportTransaction PITransaction
        {
            get
            {
                return _pITransaction;
            }
            set
            {
                _pITransaction = value;
            }
        }


        public RawDataWindow()
        {
            InitializeComponent();
        }

        public RawDataWindow(PacerImportTransaction _transaction)
        {
            InitializeComponent();

            if (_transaction != null)
            {
                this._pITransaction = _transaction;
                txtCourtName.Text = _pITransaction.CourtName;
                txtBillablePages.Text = _pITransaction.BillablePages.ToString();
                txtCost.Text = _pITransaction.Cost.ToString("c");
                txtTimestamp.Text = _pITransaction.DownloadTimeStamp.ToLongTimeString();
                txtStartDate.Text = _pITransaction.StartDate.ToShortDateString();
                txtEndDate.Text = _pITransaction.EndDate.ToShortDateString();
                txtLines.Text = _pITransaction.LineCount.ToString();
                txtUniqueCases.Text = _pITransaction.TotalCases.ToString();
                txtImportStatus.Text = _pITransaction.ImportStatus;

                try
                {
                    // Read the file as one string.
                    System.IO.StreamReader myFile = new System.IO.StreamReader(_pITransaction.FilePath);
                    txtRawData.Text = myFile.ReadToEnd();
                    myFile.Close();

                }
                catch (IOException e)
                {
                    // Let the user know what went wrong.
                    txtRawData.Text =  "The original raw data file was not found and may have been moved/removed from the directory!";
                }

                GridViewLineItems.ItemsSource = PacerImportDataService.GetForImportTransaction(_pITransaction.ID);

                GridViewBankruptcyCases.ItemsSource = BankruptcyCaseService.GetFiltered(_pITransaction.ID.ToString(), string.Empty, string.Empty, string.Empty, 0, string.Empty, false);

                Mouse.OverrideCursor = Cursors.Arrow;
            }

        }

        private void btnDownLoad_Click(object sender, RoutedEventArgs e)
        {

            //if (cboCourt.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Please Select a Court!");
            //    return;
            //}
            //else
            //{
            //    DownloadDataWindow _downloadDataWindow = new DownloadDataWindow((Court)cboCourt.SelectedValue);
            //    _downloadDataWindow.ShowDialog();
            //    if (_downloadDataWindow.DialogResult == true)
            //    {
            //        //reload the datagrid
            //        GridViewImportTransactions.ItemsSource = PacerImportTransactionService.GetForCourt(((Court)cboCourt.SelectedValue).ID);
            //    }

            //}
        }

        private void cboCourt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //GridViewImportTransactions.ItemsSource = PacerImportTransactionService.GetForCourt(((Court)cboCourt.SelectedValue).ID);
        }

        private void GridViewImportTransactions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement originalSender = e.OriginalSource as FrameworkElement;
            if (originalSender != null)
            {
                var row = originalSender.ParentOfType<Telerik.Windows.Controls.GridView.GridViewRow>();
                if (row != null)
                {
                    MessageBox.Show("The double-clicked row is " + ((PacerImportTransaction)row.DataContext).FilePath);
                }
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = File.Create("D:\\Projects\\BRG\\rawdata.xls"))
            {
                Byte[] info = Encoding.Default.GetBytes(GridViewLineItems.ToHtml(true));
                fs.Write(info, 0, info.Length);
            } 
        }

    }
}
