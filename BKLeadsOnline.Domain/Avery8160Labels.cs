namespace BKLeadsOnline.Domain
{
	using System.ComponentModel;
	using System.Drawing;
	using Telerik.Reporting;
	using Telerik.Reporting.Drawing;
	using System.Data;
	using System;
    using BKLeadsOnline.Domain;

	/// <summary>
	/// Summary description for Avery8160Labels.
	/// </summary>
    public partial class Avery8160Labels : Report
    {
        public Avery8160Labels(int DealerMailingListID)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            //don't include removes and marked as sold
            this.DataSource = DealerMailingListCaseService.GetForMailingList(DealerMailingListID, false);
        }
        public Avery8160Labels()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

        }
    }
}