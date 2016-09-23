using System.Windows.Controls;
using GMap.NET;
using BRGCaseMailServer;

namespace BRGCaseMail.Controls
{
   /// <summary>
   /// Interaction logic for BRGTooltip.xaml
   /// </summary>
   public partial class BRGTooltip : UserControl
   {
      public BRGTooltip()
      {
         InitializeComponent();
      }

      public void SetValues(Dealer _dealer)
      {
          lbl.Content = _dealer.CompanyName;
      }

      public void SetValues(ZipGeoCode _zipGeoCode)
      {
          lbl.Content =  _zipGeoCode.State + "  " + _zipGeoCode.ZipCodeString + ": " + _zipGeoCode.CasesAvailable + " Cases Available!";
      }
   }
}
