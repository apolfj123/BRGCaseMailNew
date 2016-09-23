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
using System.ComponentModel;
using System.Threading;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace BRGCaseMail
{
    /// <summary>
    /// Interaction logic for PacerDataMgmt.xaml
    /// </summary>
    public partial class PacerDataMgmt : UserControl
    {
        private List<Court> _courts;
        BackgroundWorker _asyncWorker;
        private List<PacerImportTransaction> _multipleTransactions;

        BackgroundWorker _asyncZipStatusWorker;

        public PacerDataMgmt()
        {
            InitializeComponent();
            _courts = CourtService.GetAll();
            cboCourt.ItemsSource = _courts;
            grdCourts.ItemsSource = _courts;
            List<PacerFileFormat> _formats = PacerFileFormatService.GetAll();
            cboECFVersion.ItemsSource = _formats;

            rdpEndDate.SelectedDate = DateTime.Now.AddDays(-1);
            rdpStartDate.SelectedDate = DateTime.Now.AddMonths(-1);

            _asyncWorker = new BackgroundWorker();
            _asyncWorker.WorkerReportsProgress = true;
            _asyncWorker.WorkerSupportsCancellation = true;
            _asyncWorker.ProgressChanged += new ProgressChangedEventHandler(bwAsync_ProgressChanged);
            _asyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwAsync_RunWorkerCompleted);
            _asyncWorker.DoWork += new DoWorkEventHandler(bwAsync_DoWork);

            _asyncZipStatusWorker = new BackgroundWorker();
            _asyncZipStatusWorker.WorkerReportsProgress = true;
            _asyncZipStatusWorker.WorkerSupportsCancellation = true;
            _asyncZipStatusWorker.ProgressChanged += new ProgressChangedEventHandler(bwAsyncZip_ProgressChanged);
            _asyncZipStatusWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwAsyncZip_RunWorkerCompleted);
            _asyncZipStatusWorker.DoWork += new DoWorkEventHandler(bwAsyncZip_DoWork);
        }
        private void btnDownLoad_Click(object sender, RoutedEventArgs e)
        {
            if (cboCourt.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select a Court!");
                return;
            }
            else
            {
                DownloadDataWindow _downloadDataWindow = new DownloadDataWindow((Court)cboCourt.SelectedValue);
                _downloadDataWindow.ShowDialog();
                if (_downloadDataWindow.DialogResult == true)
                {
                    //reload the datagrid
                    LoadTransactions();
                }
            }
        }
        private void btnImportNewZipCodeFiles_Click(object sender, RoutedEventArgs e)
        {
            ImportZipCodeWindow _ImportZipCodeWindow = new ImportZipCodeWindow();
            _ImportZipCodeWindow.ShowDialog();
        }
        private void LoadTransactions()
        {
            GridViewImportTransactions.ItemsSource = PacerImportTransactionService.GetForCourt(((Court)cboCourt.SelectedValue).ID);
        }
        private void cboCourt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadTransactions();
        }
        private void GridViewImportTransactions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement originalSender = e.OriginalSource as FrameworkElement;
            if (originalSender != null)
            {
                var row = originalSender.ParentOfType<Telerik.Windows.Controls.GridView.GridViewRow>();
                if (row != null)
                {
                    //MessageBox.Show("The double-clicked row is " + ((PacerImportTransaction)row.DataContext).ID);
                    Mouse.OverrideCursor = Cursors.Wait;
                    RawDataWindow _rawDataWindow = new RawDataWindow(((PacerImportTransaction)row.DataContext));
                    _rawDataWindow.ShowDialog();
                    LoadTransactions();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            RadButton _button = (RadButton)sender;

            PacerImportTransaction _transaction =  ((PacerImportTransaction)_button.DataContext);
                        
            //Dealer _selectedDealer = DealerService.GetByID(ID);

            if (MessageBox.Show("Delete the transaction from " + _transaction.DownloadTimeStamp.ToString() + " and all record of imported cases from the database?", "Delete Transaction?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                PacerImportTransactionService.Delete(_transaction);
                LoadTransactions();
            }

        }

        private void btnReprocess_Click(object sender, RoutedEventArgs e)
        {
            RadButton _button = (RadButton)sender;

            PacerImportTransaction _transaction = ((PacerImportTransaction)_button.DataContext);

            //Dealer _selectedDealer = DealerService.GetByID(ID);

            Mouse.OverrideCursor = Cursors.Wait;

            if (cboECFVersion.SelectedIndex > -1)
            {
                _transaction.PacerFileFormatID = ((PacerFileFormat)cboECFVersion.SelectedItem).ID;
            }

            if (MessageBox.Show("Geocode the address locations?", "Geocode Locations?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                _transaction.Reprocess(false);
            }
            else
            {
                _transaction.Reprocess(true);
            }

            Mouse.OverrideCursor = Cursors.Arrow;
            LoadTransactions();
            MessageBox.Show(_transaction.ImportMessage);
        }

        #region multiple process

        private void LoadMultipleTransactions()
        {
            GridViewMultipleImportTransactions.ItemsSource = _multipleTransactions;
            GridViewMultipleImportTransactions.Rebind();
        }
        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            grdCourts.SelectAll();
        }
        private void btnSelectNone_Click(object sender, RoutedEventArgs e)
        {
            grdCourts.SelectedItems.Clear();
        }
        
        private void btnProcessMultipleCourts_Click(object sender, RoutedEventArgs e)
        {
            // If the background thread is running then clicking this
            // button causes a cancel, otherwise clicking this button
            // launches the background thread.
            _multipleTransactions = new List<PacerImportTransaction>();
            
            if (_asyncWorker.IsBusy)
            {
                btnProcessMultipleCourts.IsEnabled = false;
                lblProcessingStatus.Content = "Canceling...";

                // Notify the worker thread that a cancel has been requested.
                // The cancel will not actually happen until the thread in the 
                // DoWork checks the bwAsync.CancellationPending flag, for this
                // reason we set the label to "Canceling...", because we haven't 
                // actually cancelled yet.
                
                _asyncWorker.CancelAsync();
            }
            else
            {
                if (grdCourts.SelectedItems == null || grdCourts.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please Select a Court!");
                    return;
                }
                else
                {
                    btnProcessMultipleCourts.Content = "Cancel";
                    lblProcessingStatus.Content = "Processing...";

                    MutipleProcessSpec _spec = new MutipleProcessSpec();
                    _spec.Courts = grdCourts.SelectedItems.ToList();
                    radProgressBarDownload.Maximum = grdCourts.SelectedItems.Count;
        
                    if (rdoUseSpecifiedDate.IsChecked == true)
                    {
                        _spec.StartDate = rdpStartDate.SelectedDate;
                        _spec.EndDate = rdpEndDate.SelectedDate;
                    }

                    _spec.FiledOnly = (bool)rdoFiled.IsChecked;
                    _spec.GeocodeAddresses = (bool)chkGeocodeAddresses.IsChecked;

                    // Kickoff the worker thread to begin it's DoWork function.
                    _asyncWorker.RunWorkerAsync(_spec);
                
                }
            }
        }

        private void bwAsync_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // The Sender is the BackgroundWorker object we need it to
                // report progress and check for cancellation.
                BackgroundWorker bwAsync = sender as BackgroundWorker;

                MutipleProcessSpec _spec = (MutipleProcessSpec)e.Argument;

                int i = 0;

                string _state = "Checking ECF Versions...";
                bwAsync.ReportProgress(0, _state);

                _state = CourtService.CheckECFVersions();
                bwAsync.ReportProgress(0, _state);

                Thread.Sleep(1000);

                foreach (object _court in _spec.Courts)
                {
                    i++;

                    //refresh court from ID to get version that may have been updated
                    CourtService.Refresh((Court)_court);

                    // Periodically report progress to the main thread so that it can
                    // update the UI.  
                    _state = "Downloading cases for  " + ((Court)_court).CourtName + "...";
                    bwAsync.ReportProgress(Convert.ToInt32(i * (100.0 / _courts.Count)), _state);
                    PacerImportTransaction _transaction = new PacerImportTransaction();

                    _transaction.CourtID = ((Court)_court).ID;
                    _transaction.CourtName = ((Court)_court).CourtName;
                    _transaction.DischargedCases = !_spec.FiledOnly;

                    //TODO: test this..
                    if (_spec.StartDate != null)
                    {
                        _transaction.StartDate = (DateTime)_spec.StartDate;
                    }
                    //filed only cases use the LastPacerLoadFileDate
                    else if (_spec.FiledOnly == true)
                    {
                        if (((DateTime)((Court)_court).LastPacerLoadFileDate) > DateTime.MinValue)
                        {
                            _transaction.StartDate = ((DateTime)((Court)_court).LastPacerLoadFileDate).AddDays(1);
                        }
                        else
                        {
                            _transaction.StartDate = DateTime.Now.AddMonths(-1);
                        }
                    }
                    //discharged cases use the LastPacerLoadFileDate
                    else if (_spec.FiledOnly != false)
                    {
                        if (((DateTime)((Court)_court).LastPacerLoadDischargeDate) > DateTime.MinValue)
                        {
                            _transaction.StartDate = ((DateTime)((Court)_court).LastPacerLoadDischargeDate).AddDays(1);
                        }
                        else
                        {
                            _transaction.StartDate = DateTime.Now.AddMonths(-1);
                        }
                    }

                    if (_spec.EndDate != null)
                    {
                        _transaction.EndDate = (DateTime)_spec.EndDate;
                    }
                    else
                    {
                        _transaction.EndDate = DateTime.Parse(DateTime.Now.AddDays(-1).ToShortDateString());
                    }

                    _transaction.PacerFileFormatID = ((Court)_court).PacerFileFormatID;

                    //check if the transaction overlaps prior periods to avoid extra chanrges, if so throw an error
                    _transaction.CheckForPriorOverlappingDownloads();                    
                    
                    _transaction.DownloadNewCases(_spec.GeocodeAddresses);
                    _multipleTransactions.Add(_transaction);

                    // Periodically check if a Cancellation request is pending.  If the user
                    // clicks cancel the line _asyncWorker.CancelAsync(); 
                    if (bwAsync.CancellationPending)
                    {
                        // Pause for bit to demonstrate that there is time between 
                        // "Cancelling..." and "Canceled".
                        // Thread.Sleep(1200);

                        // Set the e.Cancel flag so that the WorkerCompleted event
                        // knows that the process was canceled.
                        e.Cancel = true;
                        return;
                    }
                }

                bwAsync.ReportProgress(100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bwAsync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. We need to inspect 
            // our response to see if an error occured, a cancel was 
            // requested or if we completed succesfully.

            btnProcessMultipleCourts.Content = "Download Data for Selected Courts!";
            btnProcessMultipleCourts.IsEnabled = true;
            radProgressBarDownload.Value = 0;

            // Check to see if an error occured in the 
            // background process.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            // Check to see if the background process was cancelled.
            if (e.Cancelled)
            {
                lblProcessingStatus.Content = "Cancelled...";
            }
            else
            {
                // Everything completed normally.
                // process the response using e.Result

                lblProcessingStatus.Content = "Completed...";
            }
            LoadMultipleTransactions();
            LoadCourts();
        }
        private void bwAsync_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // This function fires on the UI thread so it's safe to edit
            // the UI control directly, no funny business with Control.Invoke.
            // Update the progressBar with the integer supplide to us from the 
            // ReportProgress() function.  Note, e.UserState is a "tag" property
            // that can be used to send other information from the BackgroundThread
            // to the UI thread.

            radProgressBarDownload.Value = e.ProgressPercentage;
            lblProcessingStatus.Content = (string)e.UserState;
            LoadMultipleTransactions();
        }
        private void GridViewMultipleImportTransactions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement originalSender = e.OriginalSource as FrameworkElement;
            if (originalSender != null)
            {
                var row = originalSender.ParentOfType<Telerik.Windows.Controls.GridView.GridViewRow>();
                if (row != null)
                {
                    //MessageBox.Show("The double-clicked row is " + ((PacerImportTransaction)row.DataContext).ID);
                    Mouse.OverrideCursor = Cursors.Wait;
                    RawDataWindow _rawDataWindow = new RawDataWindow(((PacerImportTransaction)row.DataContext));
                    _rawDataWindow.ShowDialog();
                }
            }
        }

        #endregion

        #region Zip Statistics

        private void btnUpdateStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (_asyncZipStatusWorker.IsBusy)
            {
                btnUpdateStatistics.IsEnabled = false;
                lblProcessingStatus.Content = "Canceling...";
                _asyncWorker.CancelAsync();
            }
            else
            {
                btnUpdateStatistics.Content = "Cancel";
                lblProcessingStatus.Content = "Processing...";

                List<State> _states = StateService.GetAll();

                radProgressBarDownload.Maximum = _states.Count;

                // Kickoff the worker thread to begin it's DoWork function.
                _asyncZipStatusWorker.RunWorkerAsync();
            }
        }

        private void bwAsyncZip_DoWork(object sender, DoWorkEventArgs e)
        {
            // The Sender is the BackgroundWorker object we need it to
            // report progress and check for cancellation.
            BackgroundWorker _asyncZipStatusWorker = sender as BackgroundWorker;

            int i = 0;

            string _status;

            Thread.Sleep(100);

            List<State> _states = StateService.GetActive();
            
            foreach (State _state in _states)
            {
                i++;

                // Periodically report progress to the main thread so that it can
                // update the UI.  
                _status = "Updating Statistics for State: " + _state.StateCode + "...";

                _asyncZipStatusWorker.ReportProgress(Convert.ToInt32(i * (100.0 / _states.Count)), _status);

                ZipGeoCodeService.UpdateStatsForState(_state);
                
                // Periodically check if a Cancellation request is pending.  If the user
                // clicks cancel the line _asyncWorker.CancelAsync(); 
                if (_asyncZipStatusWorker.CancellationPending)
                {
                    // Pause for bit to demonstrate that there is time between 
                    // "Cancelling..." and "Canceled".
                    // Thread.Sleep(1200);

                    // Set the e.Cancel flag so that the WorkerCompleted event
                    // knows that the process was canceled.
                    e.Cancel = true;
                    return;
                }
            }
            _asyncZipStatusWorker.ReportProgress(100);
        }
        private void bwAsyncZip_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. We need to inspect 
            // our response to see if an error occured, a cancel was 
            // requested or if we completed succesfully.

            btnUpdateStatistics.Content = "Update Zip Statistics";
            btnUpdateStatistics.IsEnabled = true;
            radProgressBarDownload.Value = 0;

            // Check to see if an error occured in the 
            // background process.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                return;
            }

            // Check to see if the background process was cancelled.
            if (e.Cancelled)
            {
                lblProcessingStatus.Content = "Cancelled...";
            }
            else
            {
                // Everything completed normally.
                // process the response using e.Result

                lblProcessingStatus.Content = "Completed...";
            }
        }
        private void bwAsyncZip_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // This function fires on the UI thread so it's safe to edit
            // the UI control directly, no funny business with Control.Invoke.
            // Update the progressBar with the integer supplide to us from the 
            // ReportProgress() function.  Note, e.UserState is a "tag" property
            // that can be used to send other information from the BackgroundThread
            // to the UI thread.

            radProgressBarDownload.Value = e.ProgressPercentage;
            lblProcessingStatus.Content = (string)e.UserState;
        }

        #endregion
        private void LoadCourts()
        {
            _courts = CourtService.GetAll();
            cboCourt.ItemsSource = null;
            cboCourt.Items.Clear();
            cboCourt.ItemsSource = _courts;

            grdCourts.ItemsSource = null; 
            grdCourts.Items.Clear();
            grdCourts.ItemsSource = _courts;
        }
        private void btnCheckCourtVersion_Click(object sender, RoutedEventArgs e)
        {
            //ECFVersionByCourt _ecf = new ECFVersionByCourt();
            //List<ECFVersion> _versions = _ecf.GetBankruptcyCourtVersions();

            if (cboCourt.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select a Court!");
                return;
            }
            else
            {
                try
                {
                    Mouse.OverrideCursor = Cursors.Wait; 
                    Court _court = (Court)cboCourt.SelectedValue;
                    ECFVersionByCourt _version = new ECFVersionByCourt();
                    ECFVersion _ecfVersion = _version.GetBankruptcyCourtVersion(_court.CourtName);

                    if (_court.PacerFileVersion == _ecfVersion.versionString)
                    {
                        MessageBox.Show("Versions match!: " + _ecfVersion.versionString);
                    }
                    else
                    {
                        //get the corresponding ID;
                        PacerFileFormat _format = PacerFileFormatService.GetByVersionString(_ecfVersion.versionString);
                        if (_format == null)
                        {
                            MessageBox.Show("Unknown Version: " + _ecfVersion.versionString);
                        }
                        else
                        {
                            _court.PacerFileFormatID = _format.ID;
                            _court.PacerFileVersion = _format.PacerFileVersion;
                            CourtService.Save(_court);
                            cboECFVersion.SelectedIndex = 0; 
                            MessageBox.Show("Court Version updated to: " + _ecfVersion.versionString);
                            
                            
                            //LoadCourts();


                        }
                    }
                    Mouse.OverrideCursor = Cursors.Arrow;

                    //MessageBox.Show(_ecfVersion.versionString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                    Mouse.OverrideCursor = Cursors.Arrow;
                }
            }
        }
        //private void btnUpdateVersion_Click(object sender, RoutedEventArgs e)
        //{
        //    //ECFVersionByCourt _ecf = new ECFVersionByCourt();
        //    //List<ECFVersion> _versions = _ecf.GetBankruptcyCourtVersions();

        //    if (cboCourt.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Please Select a Court!");
        //        return;
        //    }
        //    else if (cboECFVersion.SelectedIndex == -1) 
        //    {
        //        MessageBox.Show("Please Select an ECF Version!");
        //        return;
        //    }
                
        //    else
        //    {
        //        try
        //        {
        //            Court _court = (Court)cboCourt.SelectedValue;
        //            //get the corresponding ID;
        //            _court.PacerFileFormatID = ((PacerFileFormat)cboECFVersion.SelectedValue).ID;

        //            CourtService.Save(_court);

        //            MessageBox.Show("Version updated to " + ((PacerFileFormat)cboECFVersion.SelectedValue).PacerFileVersion + " for court: " + _court.CourtName);
        //            lblECFVersion.Content = ((PacerFileFormat)cboECFVersion.SelectedValue).PacerFileVersion;
        //            cboECFVersion.SelectedIndex = -1;

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.ToString());
        //        }
        //    }
        //}
        private void btnCheckCourtVersions_Click(object sender, RoutedEventArgs e)
        {
            //ECFVersionByCourt _ecf = new ECFVersionByCourt();
            //List<ECFVersion> _versions = _ecf.GetBankruptcyCourtVersions();
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                MessageBox.Show(CourtService.CheckECFVersions(), "PACER ECF Version Update Results!");
                Mouse.OverrideCursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadCourts();
        }
    }

    public class MutipleProcessSpec
    {
        public List<object> Courts{get;set;}
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool FiledOnly { get; set; }
        public bool GeocodeAddresses { get; set; }
    }

}
