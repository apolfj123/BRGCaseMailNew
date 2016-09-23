using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
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
using System.Configuration;

namespace BRGCaseMail
{
    /// <summary>
    /// A user control for the dealer.  uses GMap for mapping.  http://greatmaps.codeplex.com/
    /// </summary>
    public partial class DealerDetailUC : UserControl
    {
        private Dealer _selectedDealer;
        private List<ZipGeoCode> _dealerZipCodes;
        private RadTabControl _rtc;
        RadTabItem _rt;
        bool blnLoadStates;
       
        // marker
        GMapMarker currentMarker;
        
        // zones list
        List<GMapMarker> Circles = new List<GMapMarker>();
        List<GMapMarker> _otherDealerMarkers = new List<GMapMarker>();
        List<GMapMarker> _zipCodeMarkers = new List<GMapMarker>();
        
        string _selectedPath;

        bool IsLoading;
        public Dealer SelectedDealer
        {
            get
            {
                return _selectedDealer;
            }
            set
            {
                _selectedDealer = value;
            }
        }
        public DealerDetailUC()
        {
            InitializeComponent();
        }
        public DealerDetailUC(Dealer _dealer, RadTabControl rtc, RadTabItem rt)
        {
            InitializeComponent();

            blnLoadStates = false;

            _rtc = rtc;
            _rt = rt;

            try
            {
                System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("www.bing.com");
            }
            catch
            {
                MainMap.Manager.Mode = AccessMode.CacheOnly;
                //MainMap.CacheLocation = "J:\\";
                MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", "GMap.NET - Demo.WindowsPresentation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            // setup zoom min/max
            sliderZoom.Maximum = MainMap.MaxZoom;
            sliderZoom.Minimum = MainMap.MinZoom;

            if (_dealer == null)
            {
                _selectedDealer = new Dealer();
                _selectedDealer.CurrentCustomer = true;
                _selectedDealer.MaxDistance = 100;
            }
            else
            {
                _selectedDealer = _dealer;
            }

            LoadDealer();

            LoadPreviousMailings();
            
            // set cache mode only if no internet avaible

            // map events
            //MainMap.OnCurrentPositionChanged += new CurrentPositionChanged(MainMap_OnCurrentPositionChanged);
            //MainMap.OnTileLoadComplete += new TileLoadComplete(MainMap_OnTileLoadComplete);
            //MainMap.OnTileLoadStart += new TileLoadStart(MainMap_OnTileLoadStart);
            //MainMap.OnMapTypeChanged += new MapTypeChanged(MainMap_OnMapTypeChanged);
            //MainMap.MouseMove += new System.Windows.Input.MouseEventHandler(MainMap_MouseMove);
            //MainMap.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(MainMap_MouseLeftButtonDown);
            //MainMap.Loaded += new RoutedEventHandler(MainMap_Loaded);
            //MainMap.MouseEnter += new MouseEventHandler(MainMap_MouseEnter);

            //if (_dealer.MaxDistance != null && _dealer.MaxDistance > 0.0 && _dealer.Latitude != null)
            //    AddCircleZone((double)_dealer.MaxDistance, new PointLatLng(){Lat=(double)_dealer.Latitude, Lng=(double)_dealer.Longitude});

        }
        public void LoadDealer()
        {
            GridMain.DataContext = _selectedDealer;

            txtMaxDistance2.Text = _selectedDealer.MaxDistance.ToString();

            List<PacerFileFormat> _formats = PacerFileFormatService.GetAll();

            if (_selectedDealer.ID > 0)
            {
                _dealerZipCodes = ZipGeoCodeService.GetForDealer(_selectedDealer.ID);

                BindTreeToZipCodes(null);

                try
                {
                    trvZipCodes.BringPathIntoView(_selectedPath);
                }
                catch 
                { 
                }

                LoadTotals();
            }

            // config map
            if (_selectedDealer.Latitude != null && _selectedDealer.Latitude > 0)
            {
                MainMap.Position = new PointLatLng((double)_selectedDealer.Latitude, (double)_selectedDealer.Longitude);
                //set current marker
                currentMarker = new GMapMarker(MainMap.Position);
                {
                    currentMarker.Shape = new CustomMarkerRed(currentMarker, _selectedDealer);
                    currentMarker.Offset = new System.Windows.Point(-5, -5);
                    currentMarker.ZIndex = int.MaxValue;
                    MainMap.Markers.Add(currentMarker);
                }
            }
        }

        // add zone circle
        public void AddCircleZone(double areaRadius, PointLatLng center)
        {
            //remove if exists
            try
            {
                RemoveCircleZone(center);
            }
            catch 
            { 
            }

            if (chkShowRadius.IsChecked == true)
            {
                GMapMarker it = new GMapMarker(center);
                it.ZIndex = -1;

                Circle c = new Circle();
                c.Center = center;
                c.Bound = center.createPointLatLngFromCenterMiles(areaRadius);
                c.Tag = it;
                c.IsHitTestVisible = false;

                UpdateCircle(c);
                Circles.Add(it);

                it.Shape = c;
                MainMap.Markers.Add(it);
            }
        }

        public void RemoveCircleZone(PointLatLng center)
        {
            foreach(GMapMarker _marker in Circles)
            {
                if (_marker.Position.Lat == center.Lat && _marker.Position.Lng == center.Lng)
                {
                    MainMap.Markers.Remove(_marker);
                    Circles.Remove(_marker);
                    return;
                }
            }
        }

        //private void btnGeocode_Click(object sender, RoutedEventArgs e)
        //{
        //    SaveScreenToDealer();
        //    YahooGeoCoder.GeocodeDealer(_selectedDealer);
        //    txtLatitude.Text = _selectedDealer.Latitude.ToString();
        //    txtLongitude.Text = _selectedDealer.Longitude.ToString();
        //}

        // center markers on load

        //void MainMap_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    MainMap.Focus();
        //}

        //void MainMap_Loaded(object sender, RoutedEventArgs e)
        //{
        //    MainMap.ZoomAndCenterMarkers(null);
        //}

        //void MainMap_OnMapTypeChanged(MapType type)
        //{
        //    sliderZoom.Minimum = MainMap.MinZoom;
        //    sliderZoom.Maximum = MainMap.MaxZoom;
        //}

        //void MainMap_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    System.Windows.Point p = e.GetPosition(MainMap);
        //    currentMarker.Position = MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
        //}

        //// move current marker with left holding
        //void MainMap_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
        //    {
        //        System.Windows.Point p = e.GetPosition(MainMap);
        //        currentMarker.Position = MainMap.FromLocalToLatLng((int)p.X, (int)p.Y);
        //    }
        //}

        //// zoo max & center markers
        //private void button13_Click(object sender, RoutedEventArgs e)
        //{
        //    MainMap.ZoomAndCenterMarkers(null);
        //}

        //// tile louading starts
        //void MainMap_OnTileLoadStart()
        //{
        //    System.Windows.Forms.MethodInvoker m = delegate()
        //    {
        //        //progressBar1.Visibility = Visibility.Visible;
        //    };

        //    try
        //    {
        //        this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, m);
        //    }
        //    catch
        //    {
        //    }
        //}

        //// tile loading stops
        //void MainMap_OnTileLoadComplete(long ElapsedMilliseconds)
        //{
        //    MainMap.ElapsedMilliseconds = ElapsedMilliseconds;

        //    System.Windows.Forms.MethodInvoker m = delegate()
        //    {
        //        //progressBar1.Visibility = Visibility.Hidden;
        //        //groupBox3.Header = "loading, last in " + MainMap.ElapsedMilliseconds + "ms";
        //    };

        //    try
        //    {
        //        this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, m);
        //    }
        //    catch
        //    {
        //    }
        //}

        //// current location changed
        //void MainMap_OnCurrentPositionChanged(PointLatLng point)
        //{
        //    //mapgroup.Header = "gmap: " + point;
        //}

        private void SaveDealer()
        {
            if (txtLatitude.Text.Length == 0 || txtLatitude.Text == "0")
            {
                //MapQuestGeoCoder.GeocodeDealer(_selectedDealer);
                GoogleGeocoder.GeocodeDealer(_selectedDealer);
            }
            //TODO put in transation

            DealerService.Save(_selectedDealer);

            //delete all
            DealerService.DeleteDealerZipCodes(_selectedDealer.ID);
            
            //then add back
            if (_dealerZipCodes != null)
            {
                foreach (ZipGeoCode _zipGeoCode in _dealerZipCodes)
                {
                    DealerService.AddDealerZipCode(new DealerZipCode() { DealerID = _selectedDealer.ID, ZipGeoCodeID = _zipGeoCode.ID });
                }                
            }

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            SaveDealer();

            LoadDealer();

            
            MessageBox.Show("Dealer Saved!");

        }
        private void btnSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            SaveDealer();
            _rtc.Items.Remove(_rt);
            _rtc.SelectedItem = _rtc.Items[0];
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            _rtc.Items.Remove(_rt);
            _rtc.SelectedItem = _rtc.Items[0];
        }
        // zoom up
        private void czuZoomUp_Click(object sender, RoutedEventArgs e)
        {
            MainMap.Zoom = ((int)MainMap.Zoom) + 1;
        }
        // zoom down
        private void czuZoomDown_Click(object sender, RoutedEventArgs e)
        {
            MainMap.Zoom = ((int)(MainMap.Zoom + 0.99)) - 1;
        }
        // zoom changed
        private void sliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // updates circles on map
            foreach (var c in Circles)
            {
                UpdateCircle(c.Shape as Circle);
            }
        }

        // calculates circle radius
        void UpdateCircle(Circle c)
        {
            var pxCenter = MainMap.FromLatLngToLocal(c.Center);
            var pxBounds = MainMap.FromLatLngToLocal(c.Bound);

            double a = (double)(pxBounds.X - pxCenter.X);
            double b = (double)(pxBounds.Y - pxCenter.Y);
            var pxCircleRadius = Math.Sqrt(a * a + b * b);

            c.Width = 55 + pxCircleRadius * 2;
            c.Height = 55 + pxCircleRadius * 2;
            (c.Tag as GMapMarker).Offset = new System.Windows.Point(-c.Width / 2, -c.Height / 2);
        }
        private void chkShowRadius_Checked(object sender, RoutedEventArgs e)
        {
            if(_selectedDealer != null && _selectedDealer.Latitude != null)
                AddCircleZone((double)_selectedDealer.MaxDistance, new PointLatLng() { Lat = (double)_selectedDealer.Latitude, Lng = (double)_selectedDealer.Longitude });
        }
        private void chkShowRadius_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_selectedDealer.Latitude != null)
                RemoveCircleZone(new PointLatLng() { Lat = (double)_selectedDealer.Latitude, Lng = (double)_selectedDealer.Longitude });

        }
        private void chkShowOtherDealers_Checked(object sender, RoutedEventArgs e)
        {
                ShowOtherDealers(true);
        }
        private void chkShowOtherDealers_Unchecked(object sender, RoutedEventArgs e)
        {
            ShowOtherDealers(false);
        }
        private void ShowOtherDealers(bool show)
        {
            if (_otherDealerMarkers.Count > 0)
            {
                //we already have other dealers shown on the map - remove them first
                foreach (GMapMarker _marker in _otherDealerMarkers)
                {
                    MainMap.Markers.Remove(_marker);
                    //RemoveCircleZone(_marker.Position);
                }
                _otherDealerMarkers = new List<GMapMarker>();
            }

            if (show == true)
            {
                List<Dealer> _dealers = DealerService.GetInRadius((double)_selectedDealer.Latitude, (double)_selectedDealer.Longitude, (double)_selectedDealer.MaxDistance);

                foreach (Dealer _dealer in _dealers)
                {
                    if (_dealer.Latitude != null && _dealer.ID != _selectedDealer.ID)
                    {
                        GMapMarker currentMarker = new GMapMarker(new PointLatLng() { Lat = (double)_dealer.Latitude, Lng = (double)_dealer.Longitude });
                        {
                            currentMarker.Shape = new CustomMarkerOrange(currentMarker, _dealer);
                            currentMarker.Offset = new System.Windows.Point(-2, -2);
                            currentMarker.ZIndex = int.MaxValue;
                            MainMap.Markers.Add(currentMarker);
                            //AddCircleZone((double)_dealer.MaxDistance, new PointLatLng() { Lat = (double)_dealer.Latitude, Lng = (double)_dealer.Longitude });
                            _otherDealerMarkers.Add(currentMarker);
                        }
                    }
                }
            }
        }
        private void chkShowZipCenters_Checked(object sender, RoutedEventArgs e)
        {
            ShowZipCodeCenters(true);
        }
        private void chkShowZipCenters_Unchecked(object sender, RoutedEventArgs e)
        {
            ShowZipCodeCenters(false);
        }
        private void ShowZipCodeCenters(bool show)
        {
            if (_zipCodeMarkers.Count > 0)
            {
                //we already have other dealers shown on the map - remove them first
                foreach (GMapMarker _marker in _zipCodeMarkers)
                {
                    MainMap.Markers.Remove(_marker);
                    //RemoveCircleZone(_marker.Position);
                }
                _zipCodeMarkers = new List<GMapMarker>();
            }

            if (show == true)
            {

                foreach (ZipGeoCode _zipGeoCode in _dealerZipCodes)
                {
                    if (_zipGeoCode.Latitude != null)
                    {
                        GMapMarker currentMarker = new GMapMarker(new PointLatLng() { Lat = _zipGeoCode.Latitude, Lng = _zipGeoCode.Longitude });
                        {
                            currentMarker.Shape = new CustomMarkerBlue(currentMarker, _zipGeoCode);
                            //currentMarker.Offset = new System.Windows.Point(-2, -2);
                            currentMarker.ZIndex = int.MaxValue;
                            MainMap.Markers.Add(currentMarker);
                            //AddCircleZone((double)_dealer.MaxDistance, new PointLatLng() { Lat = (double)_dealer.Latitude, Lng = (double)_dealer.Longitude });
                            _zipCodeMarkers.Add(currentMarker);
                        }
                    }
                }
            }
        }

        private void btnRefresh1_Click(object sender, RoutedEventArgs e)
        {
            BindTreeToZipCodes(null);

            try
            {
                trvZipCodes.BringPathIntoView(_selectedPath);
                LoadTotals();
            }
            catch
            {
            }
        }
        private void BindTreeToZipCodes(RadTreeViewItem parentNode)
        {

            if (parentNode == null)
            {
                trvZipCodes.Items.Clear();

                List<State> _states;
                
                try
                {
                     _states = StateService.GetWithinDistance((float)_selectedDealer.Latitude, (float)_selectedDealer.Longitude, (float)_selectedDealer.MaxDistance);
                }
                catch
                {
                     _states = StateService.GetActive();
                }

                foreach (State _state in _states)
                {

                    //ZipGeoCodeService.UpdateStatsForState(_state);
                    RadTreeViewItem node = new RadTreeViewItem() { Header = _state.StateDescription };
                    node.DefaultImageSrc = "/Resources/folder.png";

                    node.Tag = "State";
                    
                    if (_state.StateCode == _selectedDealer.StateCode)
                    {
                        node.IsExpanded = true;
                        _selectedPath = _state.StateDescription;
                    }
                    else
                    {
                        node.IsExpanded = false;
                    }

                    trvZipCodes.Items.Add(node);

                    //call recursively to add zip parts
                    BindTreeToZipCodes(node);
                
                }


                //save while the form is open
                blnLoadStates = true;
                
            }
            else //parent node is not null - what level is it
            {
                if (((string)parentNode.Tag) == "State")
                {
                    List<ZipCodePart> _zipParts = ZipCodePartService.GetForState(parentNode.Header.ToString().Substring(0, 2));
                    foreach (ZipCodePart _zipPart in _zipParts)
                    {
                        RadTreeViewItem node = new RadTreeViewItem() { Header = _zipPart.ZipPart };
                        node.Tag = "ZipPart";
                        parentNode.Items.Add(node);
                    }
                    if (parentNode.IsExpanded == true)
                    {
                        ((RadTreeViewItem)parentNode.Items[0]).IsSelected = true;
                        _selectedPath += " | " + _zipParts[_zipParts.Count - 1].ZipPart;
                    }
                }
            }

        }
        private void trvZipCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get a reference to the treeview
            Telerik.Windows.Controls.RadTreeView treeView = sender as Telerik.Windows.Controls.RadTreeView;
            // Get the currently selected items
            ObservableCollection<Object> selectedItems = treeView.SelectedItems;
            if (treeView.SelectedItems != null && treeView.SelectedItems.Count > 0)
            {
                RadTreeViewItem item = selectedItems[0] as RadTreeViewItem;

                if (((string)item.Tag) == "State")
                {
                    GridSelectedZipCodes.ItemsSource = ZipGeoCodeService.GetForState(((string)item.Header).Substring(0, 2), (float)_selectedDealer.Latitude, (float)_selectedDealer.Longitude, (float)_selectedDealer.MaxDistance);
                    SelectZipCodes();
                }
                else
                {
                    GridSelectedZipCodes.ItemsSource = ZipGeoCodeService.GetForZipPart((string)item.Header, (float)_selectedDealer.Latitude, (float)_selectedDealer.Longitude, (float)_selectedDealer.MaxDistance);
                    SelectZipCodes();
                }

            }
        }
        private void SelectZipCodes()
        {
            IsLoading = true;
            foreach (ZipGeoCode _item in GridSelectedZipCodes.Items)
            {
                if (_dealerZipCodes.Find(delegate(ZipGeoCode zgc) {return zgc.ID == _item.ID; }) != null)
                {
                    GridSelectedZipCodes.SelectedItems.Add(_item);
                }
            }
            LoadTotals();
            IsLoading = false;
        }
        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            GridSelectedZipCodes.SelectAll();
        }

        private void btnSelectAllInRadius_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                _dealerZipCodes = ZipGeoCodeService.GetInsideRadius(_selectedDealer);

                SaveDealer();

                LoadDealer();

                BindTreeToZipCodes(null);

                ShowZipCodeCenters((bool)chkShowZipCenters.IsChecked);

                AddCircleZone((double)_selectedDealer.MaxDistance, new PointLatLng() { Lat = (double)_selectedDealer.Latitude, Lng = (double)_selectedDealer.Longitude });

                MessageBox.Show("Dealer Updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void btnSelectNone_Click(object sender, RoutedEventArgs e)
        {
            GridSelectedZipCodes.SelectedItems.Clear();
        }
        private void GridSelectedZipCodes_DataLoaded(object sender, EventArgs e)
        {
            SelectZipCodes();
        }
        
        private void rdoSimple_Checked(object sender, RoutedEventArgs e)
        {
            if (rdoSimple.IsChecked == true && GridSelectedZipCodes != null)
            {
                GridSelectedZipCodes.SelectionMode = SelectionMode.Multiple;
            }
            else if (GridSelectedZipCodes != null)
            {
                GridSelectedZipCodes.SelectionMode = SelectionMode.Extended;
            }
        }
        private void GridSelectedZipCodes_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            if (!(IsLoading))
            {
                //remove all items

                foreach (ZipGeoCode _item in GridSelectedZipCodes.Items)
                {
                    ZipGeoCode _zipGeoCode = _dealerZipCodes.Find(delegate(ZipGeoCode zgc) { return zgc.ID == _item.ID; });

                    if (_zipGeoCode != null)
                    {
                        _dealerZipCodes.Remove(_zipGeoCode);
                    }
                }

                foreach (ZipGeoCode _item in GridSelectedZipCodes.SelectedItems)
                {
                    _dealerZipCodes.Add(_item);
                }

                LoadTotals();
            }
        }
        
        private void LoadTotals()
        {
            int _totalSelectedZipCodes =0;
            int _totalSelectedCases =0;
            int _totalSelectedAvailableCases =0;
            
            foreach (ZipGeoCode _zipGeoCode in GridSelectedZipCodes.SelectedItems)
            {
                _totalSelectedZipCodes++;
                _totalSelectedCases += _zipGeoCode.CasesAvailable + _zipGeoCode.CasesMarkedSold;
                _totalSelectedAvailableCases += _zipGeoCode.CasesAvailable;
            }

            int _totalDealerZipCodes = 0;
            int _totalDealerCases = 0;
            int _totalDealerAvailableCases = 0;

            foreach (ZipGeoCode _zipGeoCode in _dealerZipCodes)
            {
                _totalDealerZipCodes++;
                _totalDealerCases += _zipGeoCode.CasesAvailable + _zipGeoCode.CasesMarkedSold;
                _totalDealerAvailableCases += _zipGeoCode.CasesAvailable;
            }

            txtDealerAvailableCases.Text = _totalDealerAvailableCases.ToString();
            txtDealerCases.Text = _totalDealerCases.ToString();
            txtDealerZipCodes.Text = _totalDealerZipCodes.ToString();

            txtTotalCases.Text = _totalSelectedCases.ToString();
            txtTotalZipCodes.Text = _totalSelectedZipCodes.ToString();
            txtAvailableCases.Text = _totalSelectedAvailableCases.ToString();
        
        }

        private void txtMaxDistance_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindTreeToZipCodes(null);

            try
            {
                trvZipCodes.BringPathIntoView(_selectedPath);
            }
            catch
            {
            }

        }
        private void btnRefreshCircle_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedDealer != null && _selectedDealer.Latitude != null)
                AddCircleZone((double)_selectedDealer.MaxDistance, new PointLatLng() { Lat = (double)_selectedDealer.Latitude, Lng = (double)_selectedDealer.Longitude });
        }
        private void btnDeleteOutsideRadius_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("This will save any changes for this dealer into the database first.  Continue", "Save Changes?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                SaveDealer();

                LoadDealer();

                _dealerZipCodes = ZipGeoCodeService.GetForDealerInsideRadius(_selectedDealer);

                SelectZipCodes();
                ShowZipCodeCenters((bool)chkShowZipCenters.IsChecked);
                AddCircleZone((double)_selectedDealer.MaxDistance, new PointLatLng() { Lat = (double)_selectedDealer.Latitude, Lng = (double)_selectedDealer.Longitude });
            }

        }

        #region Mailings

        private void btnRemerge_Click(object sender, RoutedEventArgs e)
        {
            RadButton _button = (RadButton)sender;
            int ID = ((DealerMailingList)_button.DataContext).ID;
            DealerMailingList _selectedDealerMailingList = DealerMailingListService.GetByID(ID);

            try
            {
                if (txtReprintTemplateFilePath.Text != _selectedDealerMailingList.TemplateFilePath && txtReprintTemplateFilePath.Text.Length != 0)
                {
                    if (MessageBox.Show("You are selecting a different template than was originally used for this Mailing.  A New Mailing record and Mail Merge will be created using the original CSV file.", "Create New Mail Merge Record?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        _selectedDealerMailingList.TemplateFilePath = txtReprintTemplateFilePath.Text;
                        _selectedDealerMailingList.CreationDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                        _selectedDealerMailingList.ID = 0;
                        //get the New ID as it is used in the mail merge
                        DealerMailingListService.Save(_selectedDealerMailingList);
                        MailMergeHelper _helper = new MailMergeHelper(_selectedDealer, _selectedDealerMailingList);
                        //process but don't merge
                        _helper.ProcessWordMailMerge(true);
                        LoadPreviousMailings();
                        MessageBox.Show("Mail Merge Created!");
                    }
                }
                else
                {
                    MailMergeHelper _helper = new MailMergeHelper(_selectedDealer, _selectedDealerMailingList);
                    //process but don't merge
                    _helper.ProcessWordMailMerge(true);
                    LoadPreviousMailings();
                    MessageBox.Show("Mail Merge Created!");
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
                Utils.OpenMicrosoftWord(_selectedDealerMailingList.MailMergeFilePath);

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

            if (MessageBox.Show("Delete the mailing?", "Delete Mailing?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                if (File.Exists(_selectedDealerMailingList.CsvFilePath))
                {
                    File.Delete(_selectedDealerMailingList.CsvFilePath);
                }
                if (File.Exists(_selectedDealerMailingList.MailMergeFilePath))
                {
                    File.Delete(_selectedDealerMailingList.MailMergeFilePath);
                }

                DealerMailingListService.Delete(_selectedDealerMailingList);

                LoadPreviousMailings();
            }


            Mouse.OverrideCursor = Cursors.Arrow;

        }

        public void LoadPreviousMailings()
        {
            GridViewMailings.ItemsSource = null;
            GridViewMailings.ItemsSource = DealerMailingListService.GetForDealer(_selectedDealer.ID);
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
        private void btnmarkTrackback_Click(object sender, RoutedEventArgs e)
        {
            RadButton _button = (RadButton)sender;
            int ID = ((DealerMailingList)_button.DataContext).ID;
            DealerMailingList _selectedDealerMailingList = DealerMailingListService.GetByID(ID);
            _selectedDealerMailingList.TrackingLetterRecieved = true;
            DealerMailingListService.Save(_selectedDealerMailingList);
            MessageBox.Show("Dealer Mailing List Updated!");
            LoadPreviousMailings();
        }
        #endregion

        private void btnUpdateCaseAvailCount_Click(object sender, RoutedEventArgs e)
        {
            if (trvZipCodes.SelectedItems != null && trvZipCodes.SelectedItems.Count > 0)
            {
                RadTreeViewItem item = trvZipCodes.SelectedItems[0] as RadTreeViewItem;

                if (((string)item.Tag) == "State")
                {
                    ZipGeoCodeService.UpdateStatsForState(((string)item.Header).Substring(0, 2));
                    GridSelectedZipCodes.ItemsSource = ZipGeoCodeService.GetForState(((string)item.Header).Substring(0, 2), (float)_selectedDealer.Latitude, (float)_selectedDealer.Longitude, (float)_selectedDealer.MaxDistance);
                    GridSelectedZipCodes.Rebind();
                    SelectZipCodes();
                }
                else
                {
                    ZipGeoCodeService.UpdateStatsForZipPart(((string)item.Header).Substring(0, 3));
                    GridSelectedZipCodes.ItemsSource = ZipGeoCodeService.GetForZipPart((string)item.Header, (float)_selectedDealer.Latitude, (float)_selectedDealer.Longitude, (float)_selectedDealer.MaxDistance);
                    SelectZipCodes();
                }

            }
        }

        private void btnSynchZIpCodeTotals_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            ZipGeoCodeService.UpdateStatsForDealer(_selectedDealer.ID);
            LoadDealer();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

    }

        //private void cboCourt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    GridViewImportTransactions.ItemsSource = PacerImportTransactionService.GetForCourt(((Court)cboCourt.SelectedValue).ID);
        //}

        //private void GridViewImportTransactions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    FrameworkElement originalSender = e.OriginalSource as FrameworkElement;
        //    if (originalSender != null)
        //    {
        //        var row = originalSender.ParentOfType<Telerik.Windows.Controls.GridView.GridViewRow>();
        //        if (row != null)
        //        {
        //            //MessageBox.Show("The double-clicked row is " + ((PacerImportTransaction)row.DataContext).ID);
        //            RawDataWindow _rawDataWindow = new RawDataWindow(((PacerImportTransaction)row.DataContext));
        //            _rawDataWindow.ShowDialog();
        //        }
        //    }

        //}
}
