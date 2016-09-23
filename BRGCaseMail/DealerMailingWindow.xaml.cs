using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
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
using System.Configuration;
using System.Diagnostics;

namespace BRGCaseMail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DealerMailingWindow : System.Windows.Window
    {
        private Dealer _dealer;
        DealerMailingList _dealerMailingList = null;

        bool IsLoading;

        public Dealer Dealer
        {
            get
            {
                return _dealer;
            }
            set
            {
                _dealer = value;
            }
        }

        public DealerMailingWindow()
        {
            InitializeComponent();
        }

        public DealerMailingWindow(Dealer _dealer)
        {
            IsLoading = true; 
            
            InitializeComponent();

            if (_dealer != null)
            {
                this._dealer = _dealer;
                gbGenerateMailing.Header = "Generate New Mailing for " + _dealer.CompanyName;
                txtNumberMailings.Text = _dealer.DefaultNumberMailings.ToString();
            }

            rdpStartSelectDate.SelectedDate = DateTime.Now.AddYears(-2);
            rdpEndSelectDate.SelectedDate = DateTime.Now;

            LoadMailingTotals();

            LoadPreviousMailings();

            IsLoading = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadMailingTotals()
        {
            if (_dealer.ID > 0)
            {
                txtTotalMailingCases.Text = BankruptcyCaseService.getTotalAvailableCount(_dealer.ID, (DateTime)rdpStartSelectDate.SelectedDate, (DateTime)rdpEndSelectDate.SelectedDate, (bool)rdoFiledOnly.IsChecked).ToString();
                txtTotalUnsoldCases.Text = BankruptcyCaseService.getTotalUnsoldCount(_dealer.ID, (DateTime)rdpStartSelectDate.SelectedDate, (DateTime)rdpEndSelectDate.SelectedDate, (bool)rdoFiledOnly.IsChecked).ToString();
            }
        }

        private void btnSelectTemplate_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".dot";
            dlg.Filter = "Word Template documents (.dot, .doc, .dotx)|*.dot;*.doc;*.dotx";
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

        private void btnSelectReprintTemplate_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".dot";
            dlg.Filter = "Word Template documents (.dot, .doc, .dotx)|*.dot;*.doc;*.dotx";
            dlg.InitialDirectory = ConfigurationManager.AppSettings.Get("TemplateFilePath");

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
            
                // Open document 
                string filename = dlg.FileName;
                txtReprintTemplateFilePath.Text = filename;
            
            }
        }
        
        private void btn1StepProcess_Click(object sender, RoutedEventArgs e)
        {
            //todo: validation - put it in UI
            if (txtNumberMailings.Text.Length > 0)
            {
                try
                {
                    _dealer.DefaultNumberMailings = Int32.Parse(txtNumberMailings.Text);
                }
                catch
                {
                    MessageBox.Show("A numeric number or mailings is required!");
                    return;
                }
            }
            else
            {
                return;
            }

            if (txtFilePath.Text.Length == 0)
            {
                 MessageBox.Show("Please select a template!");
                 return;
            }

            LoadMailingTotals();            

            //save the dealer...
            DealerService.Save(_dealer);

            if (Int32.Parse(txtTotalUnsoldCases.Text) > Int32.Parse(txtNumberMailings.Text))
            {
                _dealerMailingList = new DealerMailingList() { DealerID = _dealer.ID, FiledCasesOnly = (bool)rdoFiledOnly.IsChecked, CreationDate = DateTime.Now, MarkedAsSold = (bool)chkMarkAsSold.IsChecked, StartFilterDate = (DateTime)rdpStartSelectDate.SelectedDate, EndFilterDate = (DateTime)rdpEndSelectDate.SelectedDate, TemplateFilePath = txtFilePath.Text, NumberCases = Int32.Parse(txtNumberMailings.Text) };
            }
            else
            {
                MessageBox.Show("The number of available unsold cases is less that the Selected Number of Mailings");
                return;
            }

            Mouse.OverrideCursor = Cursors.Wait;

            MailMergeHelper _helper = new MailMergeHelper(_dealer, _dealerMailingList);
            
            try
            {
                _helper.ProcessMailingList(true);
                MessageBox.Show("Mail Merge Created!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Processing Mail merge: " + ex.ToString(), "Error");
            }

            Mouse.OverrideCursor = Cursors.Arrow;
            LoadPreviousMailings();
            LoadMailingTotals();
        }
        
        private void btn2StepProcessCreateCSV_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtNumberMailings.Text.Length > 0)
                {
                    try
                    {
                        _dealer.DefaultNumberMailings = Int32.Parse(txtNumberMailings.Text);
                    }
                    catch
                    {
                        MessageBox.Show("A numeric number or mailings is required!");
                        return;
                    }
                }
                else
                {
                    return;
                }

                //save the dealer...
                DealerService.Save(_dealer);

                if (Int32.Parse(txtTotalUnsoldCases.Text) >= Int32.Parse(txtNumberMailings.Text))
                {
                    _dealerMailingList = new DealerMailingList() { DealerID = _dealer.ID, FiledCasesOnly = (bool)rdoFiledOnly.IsChecked, CreationDate = DateTime.Now, MarkedAsSold = (bool)chkMarkAsSold.IsChecked, StartFilterDate = (DateTime)rdpStartSelectDate.SelectedDate, EndFilterDate = (DateTime)rdpEndSelectDate.SelectedDate, TemplateFilePath = txtFilePath.Text, NumberCases = Int32.Parse(txtNumberMailings.Text) };
                }
                else
                {
                    MessageBox.Show("The number of available unsold cases is less that the Selected Number of Mailings");
                    return;
                }

                Mouse.OverrideCursor = Cursors.Wait;

                MailMergeHelper _helper = new MailMergeHelper(_dealer, _dealerMailingList);
                //process but don't merge
                try
                {
                    _helper.ProcessMailingList(false);
                    MessageBox.Show("Mail Merge Created!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Processing Mail merge: " + ex.ToString(), "Error");
                }

                Mouse.OverrideCursor = Cursors.Arrow;
                LoadPreviousMailings();
                LoadMailingTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Mouse.OverrideCursor = Cursors.Arrow;
            }        
        }

        private void btn2StepProcessOpenMailMerge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailMergeHelper _helper = new MailMergeHelper(_dealer, _dealerMailingList);
                //process but don't merge
                try
                {
                    _helper.ProcessWordMailMerge(true);
                    MessageBox.Show("Mail Merge Created!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Processing Mail merge: " + ex.ToString(), "Error");
                }

                LoadPreviousMailings();
                LoadMailingTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The Mail Merge failed: " + ex.ToString());
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (!IsLoading)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                LoadMailingTotals();
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }
        
        private void btnRemerge_Click(object sender, RoutedEventArgs e)
        {
            RadButton _button = (RadButton)sender;
            int ID = ((DealerMailingList)_button.DataContext).ID;
            DealerMailingList _selectedDealerMailingList = DealerMailingListService.GetByID(ID);
            
            try
            {
                if (txtReprintTemplateFilePath.Text != _selectedDealerMailingList.TemplateFilePath && txtReprintTemplateFilePath.Text.Length != 0)
                {
                    if (MessageBox.Show("You are selecting a different template than was originally used for this Mailing.  This will replace the mailinglist record but will leave the old word document in the docs folder." + ConfigurationManager.AppSettings["MailMergeDocFilePath"], "Create New Mail Merge Record?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        _selectedDealerMailingList.TemplateFilePath = txtReprintTemplateFilePath.Text;
                        //don't allow cloning of a mailing list on the same day
                        _selectedDealerMailingList.CreationDate = DateTime.Now;
                        //_selectedDealerMailingList.ID = 0;
                        
                        //get the New ID as it is used in the mail merge
                        //DealerMailingListService.Save(_selectedDealerMailingList);
                        MailMergeHelper _helper = new MailMergeHelper(_dealer, _selectedDealerMailingList);
                        //process and merge
                        try
                        {
                            _helper.ProcessMailingList(true);
                            MessageBox.Show("Mail Merge Created!");
                        }
                        catch (IOException ioex)
                        {
                            if (ioex.ToString().Contains("The process cannot access the file"))
                            {
                                MessageBox.Show("The mail merge failed becuase one of the required files (.csv or doc) is already open in Word or Excel.  Please clsoe Word and Excel completely and try again.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error Processing Mail merge: " + ex.ToString(), "Error");
                        }

                        LoadPreviousMailings();

                    }
                }
                else if (_selectedDealerMailingList.TemplateFileName == null)
                {
                    MessageBox.Show("Please Select a template!");
                }
                else
                {
                    MailMergeHelper _helper = new MailMergeHelper(_dealer, _selectedDealerMailingList);
                    //process and merge
                    try
                    {
                        _helper.ProcessMailingList(true);
                        MessageBox.Show("Mail Merge Created!");
                    }
                    catch (IOException ioex)
                    {
                        if (ioex.ToString().Contains("The process cannot access the file"))
                        {
                            MessageBox.Show("The mail merge failed becuase one of the required files (.csv or doc) is already open in Word or Excel.  Please clsoe Word and Excel completely and try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Processing Mail merge: " + ex.ToString(), "Error");
                    }

                    LoadPreviousMailings();
                }
            }
            catch (IOException ioex)
            {
                if (ioex.ToString().Contains("The process cannot access the file" ))
                {
                    MessageBox.Show("The mail merge failed becuase one of the required files (.csv or doc) is already open in Word or Excel.  Please clsoe Word and Excel completely and try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The mail merge failed: " + ex.ToString());
            }

        }
        private void btnOpenWordDoc_Click(object sender, RoutedEventArgs e)
        {
            RadButton _button = (RadButton)sender;

            Mouse.OverrideCursor = Cursors.Wait;

            int ID = ((DealerMailingList)_button.DataContext).ID;

            DealerMailingList _selectedDealerMailingList = DealerMailingListService.GetByID(ID);

            //CREATING OBJECTS OF WORD AND DOCUMENT
            //Microsoft.Office.Interop.Word.Application oWordApp = new Microsoft.Office.Interop.Word.Application();

            ////close any open documents
            //foreach (Microsoft.Office.Interop.Word.Document _doc in oWordApp.Documents)
            //{
            //    _doc.Close(oFalse, oMissing, oMissing);
            //}
            try
            {
                 Utils.OpenMicrosoftWord( _selectedDealerMailingList.MailMergeFilePath);
                
                //oWordApp.Visible = true;
                //Microsoft.Office.Interop.Word.Document oWordDoc = oWordApp.Documents.Open(_selectedDealerMailingList.MailMergeFilePath);
                //oWordDoc.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Mouse.OverrideCursor = Cursors.Arrow;

        }

        private void btnOpenCsvFile_Click(object sender, RoutedEventArgs e)
        {
            RadButton _button = (RadButton)sender;

            Mouse.OverrideCursor = Cursors.Wait;

            int ID = ((DealerMailingList)_button.DataContext).ID;

            DealerMailingList _selectedDealerMailingList = DealerMailingListService.GetByID(ID);

            //CREATING OBJECTS OF WORD AND DOCUMENT
            //Microsoft.Office.Interop.Word.Application oWordApp = new Microsoft.Office.Interop.Word.Application();

            ////close any open documents
            //foreach (Microsoft.Office.Interop.Word.Document _doc in oWordApp.Documents)
            //{
            //    _doc.Close(oFalse, oMissing, oMissing);
            //}
            try
            {
                Utils.OpenExcel(_selectedDealerMailingList.CsvFilePath);

                //oWordApp.Visible = true;
                //Microsoft.Office.Interop.Word.Document oWordDoc = oWordApp.Documents.Open(_selectedDealerMailingList.MailMergeFilePath);
                //oWordDoc.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Mouse.OverrideCursor = Cursors.Arrow;

        }

        private void btnDeleteMailing_Click(object sender, RoutedEventArgs e)
        {
            RadButton _button = (RadButton)sender;
            Mouse.OverrideCursor = Cursors.Wait;
            int ID = ((DealerMailingList)_button.DataContext).ID;

            DealerMailingList _selectedDealerMailingList = DealerMailingListService.GetByID(ID);

            //there is the possibility that more than one mailing list has been created from this CSV file.

            try
            {
                List<DealerMailingList> _mailingLists = DealerMailingListService.GetByCsvFilePath(_selectedDealerMailingList.CsvFilePath);

                if (_mailingLists.Count > 1)
                {
                    MessageBox.Show("There are multiple mailing lists generated from this set of cases! The CSV file will not be deleted and the cases will show as SOLD until the other mailing lists generated against this data set are deleted.");
                }

                if (MessageBox.Show("Delete the mailing?", "Delete Mailing?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    if (File.Exists(_selectedDealerMailingList.CsvFilePath) && _mailingLists.Count == 1)
                    {
                        File.Delete(_selectedDealerMailingList.CsvFilePath);
                    }

                    if (File.Exists(_selectedDealerMailingList.MailMergeFilePath))
                    {
                        File.Delete(_selectedDealerMailingList.MailMergeFilePath);
                    }

                    DealerMailingListService.Delete(_selectedDealerMailingList);

                    LoadPreviousMailings();
                    LoadMailingTotals();
                }
            }
            catch (IOException ioex)
            {
                if (ioex.ToString().Contains("The process cannot access the file"))
                {
                    MessageBox.Show("One of the required files (.csv or doc) is open in Word or Excel.  Please clsoe Word and Excel completely and try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The delete failed: " + ex.ToString());
            }
                
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        public void LoadPreviousMailings()
        {
            GridViewMailings.ItemsSource = DealerMailingListService.GetForDealer(_dealer.ID);
        }

        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rdoFiledOnly_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoading == false)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                LoadMailingTotals();
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        private void rdoDischarged_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoading == false)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                LoadMailingTotals();
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

    }

    public class Utils
    {
        /// <summary>
        /// Open specified word document.
        /// </summary>
        static public  void OpenMicrosoftWord(string f)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "WINWORD.EXE";
            startInfo.Arguments = f;
            Process.Start(startInfo);
        }
        static public void OpenExcel(string f)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "EXCEL.EXE";
            startInfo.Arguments = f;
            Process.Start(startInfo);
        }
    }

}
