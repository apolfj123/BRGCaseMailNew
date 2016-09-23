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

namespace BRGCaseMail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Court> _courts;
        
        public MainWindow()
        {
            InitializeComponent();

            _courts = CourtService.GetAll();

            cboCourt.ItemsSource = _courts;

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
                    GridViewImportTransactions.ItemsSource = PacerImportTransactionService.GetForCourt(((Court)cboCourt.SelectedValue).ID);
                }

            }
        }

        private void cboCourt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridViewImportTransactions.ItemsSource = PacerImportTransactionService.GetForCourt(((Court)cboCourt.SelectedValue).ID);
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
    }
}
