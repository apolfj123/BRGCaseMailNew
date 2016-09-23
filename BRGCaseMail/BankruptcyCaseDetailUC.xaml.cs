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
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Threading;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using System.IO;
using Telerik.Windows.Controls;
using BRGCaseMail.CustomMarkers;


namespace BRGCaseMail
{
    /// <summary>
    /// Interaction logic for PacerDataMgmt.xaml
    /// </summary>
    public partial class BankruptcyCaseDetailUC : UserControl
    {
        private BankruptcyCase _selectedBankruptcyCase;

        public BankruptcyCase SelectedBankruptcyCase
        {
            get
            {
                return _selectedBankruptcyCase;
            }
            set
            {
                _selectedBankruptcyCase = value;
            }
        }

        private RadTabControl _rtc;
        RadTabItem _rt;
      
        public BankruptcyCaseDetailUC()
        {
            InitializeComponent();
        }

        public BankruptcyCaseDetailUC(BankruptcyCase _BankruptcyCase, RadTabControl rtc, RadTabItem rt)
        {
            _selectedBankruptcyCase = _BankruptcyCase;
            _rtc = rtc;
            _rt = rt;
    
            InitializeComponent();                    

            GridMain.DataContext = _selectedBankruptcyCase;
            
        }
    
        private void SaveScreenToBankruptcyCase()
        {
            if (txtLatitude.Text.Length == 0)
                BingGeoCoder.GeocodeCase(_selectedBankruptcyCase);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBankruptcyCase == null)
            {
                _selectedBankruptcyCase = new BankruptcyCase();
            }
            SaveScreenToBankruptcyCase();
            BankruptcyCaseService.Save(_selectedBankruptcyCase, false);
            _rtc.Items.Remove(_rt);
            _rtc.SelectedItem = _rtc.Items[2];
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            _rtc.Items.Remove(_rt);
            _rtc.SelectedItem = _rtc.Items[2];
        }

    }
}
