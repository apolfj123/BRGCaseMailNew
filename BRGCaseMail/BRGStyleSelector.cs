using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xaml;
using Telerik.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using BRGCaseMailServer;

namespace BRGCaseMail
{
    public class BRGStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is BRGCaseMailServer.PacerImportData)
            {
                PacerImportData _pacerImportData = item as PacerImportData;
                if (_pacerImportData.Imported == true)
                {
                    return HighlightedStyle;
                }
                else
                {
                    return RegularStyle;
                }
            }
            return null;
        }
        public Style HighlightedStyle { get; set; }
        public Style RegularStyle { get; set; }
    }

    public class DealerMailingListStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is BRGCaseMailServer.DealerMailingList)
            {
                DealerMailingList _dealerMailingList = item as DealerMailingList;
                if (_dealerMailingList.FiledCasesOnly == true)
                {
                    return HighlightedStyle;
                }
                else
                {
                    return RegularStyle;
                }
            }
            return null;
        }
        public Style HighlightedStyle { get; set; }
        public Style RegularStyle { get; set; }
    }

}
