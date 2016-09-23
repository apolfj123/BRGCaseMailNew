namespace BKLeadsOnline.Domain
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for DealerSalesReport.
    /// </summary>
    public partial class DealerSalesReport : Telerik.Reporting.Report
    {
        public int DealerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DealerSalesReport()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public DealerSalesReport(int dealerID, DateTime startDate, DateTime endDate)
        {
            DealerID = dealerID;
            StartDate = startDate;
            EndDate = EndDate;

            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            this.DataSource = DealerMailingListCaseService.GetByDealerSalesReport(dealerID, startDate, endDate);

            txtStartDate.Value = startDate.ToShortDateString();
            txtEndDate.Value = endDate.ToShortDateString();

            Dealer _dealer = DealerService.GetByID(DealerID);
            txtDealerName.Value = _dealer.DealerName;

        }
    }
}