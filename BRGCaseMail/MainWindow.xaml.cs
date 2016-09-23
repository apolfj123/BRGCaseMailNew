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
using BRGCaseMailServer.SalesForceWebReference;
using System.Configuration;
using System.Web.Services.Protocols;
using System.Diagnostics;
using MailBee.Pop3Mail;
using MailBee.ImapMail;
using Telerik.Windows.Controls;
using MailBee.Mime;


namespace BRGCaseMail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private List<RemoveMail> _removeMailMessages;
        
        public MainWindow()
        {
            List<State> _states;
            List<Court> _courts;

            InitializeComponent();

            if (ConfigurationManager.AppSettings.Get("Debug") == "true")
            {
                MessageBox.Show("I'm here");
            }

            //set the Pacer Management Window
            rtPacerDataMgmt.Content = new PacerDataMgmt();

            _courts = CourtService.GetAll();
            cboCourt.ItemsSource = _courts;

            
            // export filter criteria
            //_courts.Insert(0, new Court(){CourtName="[Select...]"});
            cboCourtExportCriteria.ItemsSource = _courts;

            _states = StateService.GetActive();
            //_states.Insert(0, new State() { StateCode = "[Select...]" });
            cboState.ItemsSource = _states;
            
            //LoadAndProcessRemoveEmails();

            //if (DateTime.Now > DateTime.Parse("02/01/12"))
            //{
            //    MessageBox.Show("Your system has outstanding invoices due. Please contact Metamorpho-Sys for scheduling payment at (503) 901-5395");
            //    if (DateTime.Now > DateTime.Parse("04/01/12"))
            //    {
            //        MessageBox.Show("Your system has outstanding invoices due. Please contact Metamorpho-Sys for scheduling payment at (503) 901-5395. Applciation will now close");
            //        this.Close();
            //    }
            //}

            List<Dealer> _dealers = DealerService.GetFiltered(string.Empty, true);
            GridViewDealers.ItemsSource = _dealers;

            //_dealers.Insert(0, new Dealer() { CompanyName = "[Select...]" });
            cboDealerExportCriteria.ItemsSource = _dealers;        
        }

        private void LoadAndProcessRemoveEmails()
        {
            try
            {
                MailMessageCollection msgs;
                grdViewRemoveEmails.ItemsSource = null;
                
                Imap imp = new Imap();
                imp.Connect(ConfigurationManager.AppSettings["RemovePOP3Server"]);
                imp.Login(ConfigurationManager.AppSettings["RemovePOP3User"], ConfigurationManager.AppSettings["RemovePOP3Password"]);
                imp.SelectFolder("Inbox");

                int totalCount = imp.MessageCount;
                msgs = imp.DownloadEntireMessages("1:" + totalCount.ToString(), false);
                
                if (totalCount > 0)
                {
                    _removeMailMessages = new List<RemoveMail>();
                    RemoveMail _removeMailMessage = new RemoveMail();
                    
                    int i = 1;

                    foreach (MailMessage _msg in msgs)
                    {
                        _removeMailMessage = new RemoveMail() { IndexOnMailServer = i, MsgDateTime=_msg.DateReceived, ToAddress = _msg.To.ToString(), FromAddress = _msg.From.ToString(), Body = _msg.BodyPlainText, Subject = _msg.Subject };

                        if (((RemoveMail)_removeMailMessage).Subject.ToUpper().Contains("REMOVE"))
                        {
                            try
                            {
                                string[] _body = ((RemoveMail)_removeMailMessage).Body.Trim().Split(',');
                                string[] _name = _body[0].Split(' ');
                                BankruptcyCase _case = BankruptcyCaseService.GetByNameAndZip(_name[0], "", _name[1], Int32.Parse(_body[1].Trim()));

                                if (_case != null)
                                {
                                    _case.DontSend = true;
                                    BankruptcyCaseService.Save(_case,false);

                                    ((RemoveMail)_removeMailMessage).Processed = true;
                                    ((RemoveMail)_removeMailMessage).FoundAndRemoved = true;
                                }
                                else
                                {
                                    ((RemoveMail)_removeMailMessage).Processed = true;
                                    ((RemoveMail)_removeMailMessage).FoundAndRemoved = false;
                                }
                            }
                            catch { }
                        }                        
                        
                        _removeMailMessages.Add(_removeMailMessage);
                        
                        //save to the DB
                        RemoveMailService.Save(_removeMailMessage);
                    }

                    grdViewRemoveEmails.ItemsSource = _removeMailMessages;
                }

                //delete the messages
                if ( ConfigurationManager.AppSettings["RemovePOP3Messages"] == "true" )
                {
                    imp.DeleteMessages(Imap.AllMessages, false);                  
                    //_removeMailMessages.Clear();
                }
                imp.Close();
                imp.Disconnect();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void closeTabItem(RadTabItem item) 
        {         
            if (item != null)        
            {             
                // find the parent tab control
                RadTabControl tabControl = item.Parent as RadTabControl;
                if (tabControl != null)                 
                    tabControl.Items.Remove(item);
                this.tabCtrlMain.SelectedItem = tabControl.Items[1];
                // remove tabItem         
            }     
        }
        private void addTabItemMenu(RadTabItem item)
        {
            ContextMenu contextMenu1; 
            contextMenu1 = new ContextMenu(); 
            MenuItem menuItem1;
            menuItem1 = new MenuItem();

            //add menu item in context menu         
            contextMenu1.Items.Add(menuItem1);
            menuItem1.Header = "Close";
            // define name of context menu         
            //Create Tab Items         
            
            // define clicking event of menuitem         
            menuItem1.Click += delegate { closeTabItem(item); };
            // Incorporate context menu with tab items         
            item.ContextMenu = contextMenu1;
        }
        private void mnuCloseTabs_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (this.tabCtrlMain.Items.Count > 3)
            {
                while (this.tabCtrlMain.Items.Count > 3)
                {
                    this.tabCtrlMain.Items.Remove(this.tabCtrlMain.Items[this.tabCtrlMain.Items.Count - 1]);
                }
                this.tabCtrlMain.SelectedItem = this.tabCtrlMain.Items[1];
            }
        }
        private void mnuExit_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            this.Close();
        }
        
        #region Dealers
        
        private void btnSearchDealers_Click(object sender, RoutedEventArgs e)
        {
            LoadDealers();
        }
        private void LoadDealers()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            List<Dealer> _dealers = DealerService.GetFiltered(txtDealerName.Text, (bool)rdoActiveOnly.IsChecked);

            GridViewDealers.ItemsSource = _dealers;

            foreach (Dealer _dealer in _dealers)
            {
                if (_dealer.Latitude == null)
                {
                    BingGeoCoder.GeocodeDealer(_dealer);
                    DealerService.Save(_dealer);
                }
            }

            Mouse.OverrideCursor = Cursors.Arrow;        
        }
        
        private void btnAddNewDealer_Click(object sender, RoutedEventArgs e)
        {
            RadTabItem tabToAdd = new RadTabItem();
            tabToAdd.Header = "Add New Dealer..";
            tabToAdd.Margin = new System.Windows.Thickness(0, 0, 5, 0);
            tabToAdd.Content = new DealerDetailUC(null, tabCtrlMain, tabToAdd);
            tabToAdd.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            //should only be one
            addTabItemMenu(tabToAdd);
            this.tabCtrlMain.Items.Add(tabToAdd);
            this.tabCtrlMain.SelectedItem = tabToAdd;
        }
        private void btnGeoCodeDealers_Click(object sender, RoutedEventArgs e)
        {
             List<Dealer> _dealers =  DealerService.GetAll();
             foreach (Dealer _dealer in _dealers)
             {
                 BingGeoCoder.GeocodeDealer(_dealer);
                 DealerService.Save(_dealer);
             }
        }
        private void GridViewDealers_KeyDown(object sender, KeyEventArgs e)
        {
            var grid = (RadGridView)sender;
            if (Key.Enter == e.Key)
            {
                foreach (var row in grid.SelectedItems)
                {
                    RadTabItem tabToAdd = new RadTabItem();

                    tabToAdd.Header = "Dealer Detail:" + ((Dealer)row).CompanyName;
                    tabToAdd.Margin = new System.Windows.Thickness(0, 0, 5, 0);
                    //should only be one
                    this.tabCtrlMain.Items.Add(tabToAdd);
                    this.tabCtrlMain.SelectedItem = tabToAdd;
                }
            }
        }
        private void GridViewDealers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement originalSender = e.OriginalSource as FrameworkElement;
            if (originalSender != null)
            {
                var row = originalSender.ParentOfType<Telerik.Windows.Controls.GridView.GridViewRow>();
                if (row != null)
                {
                    //MessageBox.Show("The double-clicked row is " + ((PacerImportTransaction)row.DataContext).ID);
                    RadTabItem tabToAdd = new RadTabItem();

                    tabToAdd.Header = "Dealer: " + (((Dealer)row.DataContext)).CompanyName;
                    tabToAdd.Margin = new System.Windows.Thickness(0, 0, 5, 0);
                    tabToAdd.Content = new DealerDetailUC((Dealer)row.DataContext, tabCtrlMain, tabToAdd);
                    tabToAdd.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
                    //should only be one
                    addTabItemMenu(tabToAdd);
                    this.tabCtrlMain.Items.Add(tabToAdd);
                    this.tabCtrlMain.SelectedItem = tabToAdd;
                }
            }
        }
        private void btnOpenDealer_Click(object sender, RoutedEventArgs e)
        {
        
            RadButton _button = (RadButton)sender;
            int ID = ((Dealer)_button.DataContext).ID;

            Dealer _selectedDealer = DealerService.GetByID(ID);

            //MessageBox.Show("The double-clicked row is " + ((PacerImportTransaction)row.DataContext).ID);
            RadTabItem tabToAdd = new RadTabItem();

            tabToAdd.Header = "Dealer: " + _selectedDealer.CompanyName;
            tabToAdd.Margin = new System.Windows.Thickness(0, 0, 5, 0);
            tabToAdd.Content = new DealerDetailUC(_selectedDealer, tabCtrlMain, tabToAdd);
            tabToAdd.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            //should only be one
            addTabItemMenu(tabToAdd);
            this.tabCtrlMain.Items.Add(tabToAdd);
            this.tabCtrlMain.SelectedItem = tabToAdd;
        
        }

        private void btnCloseDealers_Click(object sender, RoutedEventArgs e)
        {
            if (this.tabCtrlMain.Items.Count > 2)
            {
                while (this.tabCtrlMain.Items.Count > 2)
                {
                    this.tabCtrlMain.Items.Remove(this.tabCtrlMain.Items[this.tabCtrlMain.Items.Count - 1]);
                }
                this.tabCtrlMain.SelectedItem = this.tabCtrlMain.Items[1];
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            RadButton _button = (RadButton)sender;
            int ID = ((Dealer)_button.DataContext).ID;
            Dealer _selectedDealer = DealerService.GetByID(ID);

            if (MessageBox.Show("Delete the dealer: " + _selectedDealer.CompanyName, "Confirm delete dealer", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DealerService.Delete(_selectedDealer);
            }
            
            LoadDealers();
        }

        #endregion

        #region BankruptcyCases
        private void btnSearchBankruptcyCases_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                List<BankruptcyCase> _bankruptcyCases;
                if (cboCourt.SelectedIndex > -1)
                {
                    _bankruptcyCases = BankruptcyCaseService.GetFiltered(txtPacerImportTransactionID.Text, txtCaseNumber.Text, txtDebtorFirstName.Text, txtDebtorLastName.Text, ((Court)cboCourt.SelectedValue).ID, txtPostalCode.Text, (bool)rdoUnsoldOnly.IsChecked);
                }
                else
                {
                    _bankruptcyCases = BankruptcyCaseService.GetFiltered(txtPacerImportTransactionID.Text, txtCaseNumber.Text, txtDebtorFirstName.Text, txtDebtorLastName.Text, 0, txtPostalCode.Text, (bool)rdoUnsoldOnly.IsChecked);
                }
                
                GridViewBankruptcyCases.ItemsSource = _bankruptcyCases;
            }
            catch (Exception ex) 
            {
                
            }

            //foreach (BankruptcyCase _BankruptcyCase in _BankruptcyCases)
            //{
            //    if (_BankruptcyCase.Latitude == null)
            //    {
            //        BingGeoCoder.GeocodeCase(_BankruptcyCase);
            //        BankruptcyCaseService.Save(_BankruptcyCase);
            //    }
            //}

            Mouse.OverrideCursor = Cursors.Arrow;

        }
        private void GridViewBankruptcyCases_KeyDown(object sender, KeyEventArgs e)
        {
            var grid = (RadGridView)sender;
            if (Key.Enter == e.Key)
            {
                foreach (var row in grid.SelectedItems)
                {
                    RadTabItem tabToAdd = new RadTabItem();

                    tabToAdd.Header = "Case: " + ((BankruptcyCase)row).FullName;
                    tabToAdd.Margin = new System.Windows.Thickness(0, 0, 5, 0);
                    //should only be one
                    this.tabCtrlMain.Items.Add(tabToAdd);
                    this.tabCtrlMain.SelectedItem = tabToAdd;
                }
            }
        }

        private void GridViewBankruptcyCases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement originalSender = e.OriginalSource as FrameworkElement;
            if (originalSender != null)
            {
                var row = originalSender.ParentOfType<Telerik.Windows.Controls.GridView.GridViewRow>();
                if (row != null)
                {
                    //MessageBox.Show("The double-clicked row is " + ((PacerImportTransaction)row.DataContext).ID);
                    RadTabItem tabToAdd = new RadTabItem();

                    tabToAdd.Header = "Case: " + (((BankruptcyCase)row.DataContext)).FullName;
                    tabToAdd.Margin = new System.Windows.Thickness(0, 0, 5, 0);
                    tabToAdd.Content = new BankruptcyCaseDetailUC((BankruptcyCase)row.DataContext, tabCtrlMain, tabToAdd);
                    tabToAdd.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
                    //should only be one
                    addTabItemMenu(tabToAdd);
                    this.tabCtrlMain.Items.Add(tabToAdd);
                    this.tabCtrlMain.SelectedItem = tabToAdd;
                }
            }
        }

        private void btnCloseBankruptcyCases_Click(object sender, RoutedEventArgs e)
        {
            if (this.tabCtrlMain.Items.Count > 2)
            {
                while (this.tabCtrlMain.Items.Count > 2)
                {
                    this.tabCtrlMain.Items.Remove(this.tabCtrlMain.Items[this.tabCtrlMain.Items.Count - 1]);
                }
                this.tabCtrlMain.SelectedItem = this.tabCtrlMain.Items[1];
            }
        }

        private void btnOpenCase_Click(object sender, RoutedEventArgs e)
        {
            RadButton _button = (RadButton)sender;
            int ID = ((BankruptcyCase)_button.DataContext).ID;

            BankruptcyCase _selectedBankruptcyCase = BankruptcyCaseService.GetByID(ID);

            //MessageBox.Show("The double-clicked row is " + ((PacerImportTransaction)row.DataContext).ID);
            RadTabItem tabToAdd = new RadTabItem();

            tabToAdd.Header = "Case: " + _selectedBankruptcyCase.FullName;
            tabToAdd.Margin = new System.Windows.Thickness(0, 0, 5, 0);
            tabToAdd.Content = new BankruptcyCaseDetailUC(_selectedBankruptcyCase, tabCtrlMain, tabToAdd);
            tabToAdd.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            //should only be one
            addTabItemMenu(tabToAdd);
            this.tabCtrlMain.Items.Add(tabToAdd);
            this.tabCtrlMain.SelectedItem = tabToAdd;
        }

        #endregion

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            RadButton _button = (RadButton)sender;

            int ID = ((Dealer)_button.DataContext).ID;

            Dealer _selectedDealer = DealerService.GetByID(ID);
            //MessageBox.Show("The double-clicked row is " + ((PacerImportTransaction)row.DataContext).ID);
            DealerMailingWindow _dealerMailingWindow = new DealerMailingWindow(_selectedDealer);
            _dealerMailingWindow.ShowInTaskbar = false;
            _dealerMailingWindow.Owner = Window.GetWindow(this);
            _dealerMailingWindow.ShowDialog();

        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Comma Separated Files (.csv)|*.csv;*";
            dlg.InitialDirectory = ConfigurationManager.AppSettings.Get("RemoveCSVFilePath");

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

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if (txtFilePath.Text != string.Empty)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                string _returnMessage = string.Empty;
                
                try
                {
                    _returnMessage = BankruptcyCaseService.ProcessRemoveFile(txtFilePath.Text);
                }
                catch (Exception ex) 
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                    MessageBox.Show("Error: Zip Import was unsuccessful, please contact your administrator!" + ": " + ex.ToString());                
                }
                
                Mouse.OverrideCursor = Cursors.Arrow;
                txtResults.Text = _returnMessage;
            }
            else
            {
                MessageBox.Show("Please select as filepath!");
            }
        }

        private void btnProcessSelected_Click(object sender, RoutedEventArgs e)
        {
            foreach (var _removeMail in grdViewRemoveEmails.SelectedItems.ToList())
            {
                if (((RemoveMail)_removeMail).Subject.ToUpper() == "REMOVE")
                {
                    string[] _body = ((RemoveMail)_removeMail).Body.Trim().Split(',');
                    string[] _name = _body[0].Split(' ');
                    BankruptcyCase _case = BankruptcyCaseService.GetByNameAndZip(_name[0], "", _name[1], Int32.Parse(_body[1].Trim()));

                    if (_case != null)
                    {
                        _case.DontSend = true;
                        BankruptcyCaseService.Save(_case, false);

                        ((RemoveMail)_removeMail).Processed = true;
                        ((RemoveMail)_removeMail).FoundAndRemoved = true;
                    }
                    else 
                    {
                        ((RemoveMail)_removeMail).Processed = true;
                        ((RemoveMail)_removeMail).FoundAndRemoved = false;
                    }

                    Imap.QuickDownloadMessages(ConfigurationManager.AppSettings["RemovePOP3Server"], ConfigurationManager.AppSettings["RemovePOP3User"], ConfigurationManager.AppSettings["RemovePOP3Password"], "Inbox");
                }
            }
        }        

        private void btnDownloadAndProcess_Click(object sender, RoutedEventArgs e)
        {
            LoadAndProcessRemoveEmails();
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            if (rdoAll.IsChecked == true)
            {
                grdViewRemoveEmails.ItemsSource = RemoveMailService.GetAll();
            }
            else if (rdoMTD.IsChecked == true)
            {
                grdViewRemoveEmails.ItemsSource = RemoveMailService.GetSince(DateTime.Parse(DateTime.Now.Month.ToString() + "/01/" + DateTime.Now.Year.ToString()));
            }
            else if (rdoYTD.IsChecked == true)
            {
                grdViewRemoveEmails.ItemsSource = RemoveMailService.GetSince(DateTime.Parse("01/01/" + DateTime.Now.Year.ToString()));
            }

        }

        #region NCOA

        private void btnSelectFileExportPath_Click(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            string initPath = ConfigurationManager.AppSettings["NCOAExportFilePath"];
            dlg.InitialDirectory = System.IO.Path.GetFullPath(initPath);
            dlg.RestoreDirectory = true;

            DateTime? _startDate = rdpStartDateExportCriteria.SelectedDate;
            DateTime? _endDate = rdpEndDateExportCriteria.SelectedDate;

            if ( _startDate != null && _endDate != null)
            {
                dlg.FileName = "NCOA_Export_" + DateTime.Parse(_startDate.ToString()).ToString("MM-dd-yyyy") + "_" + DateTime.Parse(_endDate.ToString()).ToString("MM-dd-yyyy") + ".txt";
            }
            else
                dlg.FileName = "NCOA_Export.txt"; // Default file name

            dlg.DefaultExt = ".text"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension 

			// Show save file dialog box
			Nullable<bool> result = dlg.ShowDialog();

			// Process save file dialog box results 
			if (result == true)
			{
				string filename = dlg.FileName;
				txtFilePathExport.Text = filename;
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            
            State state = (State)cboState.SelectedItem;
            string stateCode = (state != null) ? state.StateCode : string.Empty;
            Court court = (Court)cboCourtExportCriteria.SelectedItem;
            string courtName = (court != null) ? court.CourtName : string.Empty;
            Dealer dealer = (Dealer)cboDealerExportCriteria.SelectedItem;
            int dealerId = (dealer != null) ? dealer.ID : 0;
            DateTime? startDate = rdpStartDateExportCriteria.SelectedDate;
            DateTime? endDate = rdpEndDateExportCriteria.SelectedDate;
            string saveToPath = txtFilePathExport.Text;

            List<BankruptcyCase> list = BankruptcyCaseService.GetAddressVerificationList(courtName, dealerId, stateCode, startDate, endDate);

            // Create CSV File
            bool created = BankruptcyCaseService.CreateAddressVerificationCSVFile(list, saveToPath);
        }

        private void btnSelectImportFile_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            string initPath = ConfigurationManager.AppSettings["NCOAImportFilePath"];
            dlg.InitialDirectory = System.IO.Path.GetFullPath(initPath);
            dlg.RestoreDirectory = true;
            
            dlg.DefaultExt = ".text"; // Default file extension
            dlg.Filter = "NOCA Text documents (.txt)|*.txt"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process open file dialog box results 
            if (result == true)
            {
                string filename = dlg.FileName;
                txtFilePathImport.Text = filename;
            }
        
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            string filePath = txtFilePathImport.Text;
            BankruptcyCaseService.ProcessAddressVerificationFile(txtFilePathImport.Text);
        }
        #endregion

        private void btnClearSelections_Click(object sender, RoutedEventArgs e)
        {
            cboState.SelectedIndex = -1;
            cboDealerExportCriteria.SelectedIndex = -1;
            cboCourtExportCriteria.SelectedIndex = -1;
            rdpStartDateExportCriteria.SelectedDate = null;
            rdpEndDateExportCriteria.SelectedDate = null;
        }

        //private void btnExport_Click(object sender, RoutedEventArgs e)
        //{
        //    string content = "";

        //    content = GridViewDealers.ToHtml(true);

        //    using (FileStream fs = File.Create("D:\\Dealers.xls"))
        //    {
        //        Byte[] info = Encoding.Default.GetBytes(content);
        //        fs.Write(info, 0, info.Length);
        //    }
        //}

    }
}
