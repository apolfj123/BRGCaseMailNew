using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using GMap.NET.WindowsPresentation;
using Telerik.Windows.Controls;
using BRGCaseMail.Controls;
using BRGCaseMailServer;

namespace BRGCaseMail.CustomMarkers
{
   /// <summary>
   /// Interaction logic for CustomMarkerDemo.xaml
   /// </summary>
   public partial class CustomMarkerBlue
   {
      Popup Popup;
      BRGTooltip _tooltip;
      GMapMarker Marker;

      public CustomMarkerBlue(GMapMarker marker, ZipGeoCode _zipGeoCode)
      {
         this.InitializeComponent();

         this.Marker = marker;

         Popup = new Popup();
         _tooltip = new BRGTooltip();
         _tooltip.SetValues(_zipGeoCode);

         this.Loaded += new RoutedEventHandler(CustomMarkerDemo_Loaded);
         this.SizeChanged += new SizeChangedEventHandler(CustomMarkerDemo_SizeChanged);
         this.MouseEnter += new MouseEventHandler(MarkerControl_MouseEnter);
         this.MouseLeave += new MouseEventHandler(MarkerControl_MouseLeave);
         this.MouseLeftButtonUp += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonUp);
         this.MouseLeftButtonDown += new MouseButtonEventHandler(CustomMarkerDemo_MouseLeftButtonDown);

         Popup.Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse;
         {
            //Label.Background = Brushes.Blue;
            //Label.Foreground = Brushes.White;
            //Label.BorderBrush = Brushes.WhiteSmoke;
            //Label.BorderThickness = new Thickness(2);
            //Label.Padding = new Thickness(5);
            //Label.FontSize = 22;
            //Label.Content = title;
         }
         Popup.Child = _tooltip;
      }

      void CustomMarkerDemo_Loaded(object sender, RoutedEventArgs e)
      {
         if(icon.Source.CanFreeze)
         {
            icon.Source.Freeze();
         }
      }

      void CustomMarkerDemo_SizeChanged(object sender, SizeChangedEventArgs e)
      {
         Marker.Offset = new Point(-e.NewSize.Width/2, -e.NewSize.Height);
      }

      void CustomMarkerDemo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         if(!IsMouseCaptured)
         {
            Mouse.Capture(this);
         }
      }

      void CustomMarkerDemo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {
         if(IsMouseCaptured)
         {
            Mouse.Capture(null);
         }
      }

      void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
      {
         Marker.ZIndex -= 10000;
         Popup.IsOpen = false;
      }

      void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
      {
         Marker.ZIndex += 10000;
         Popup.IsOpen = true;
      }
   }
}