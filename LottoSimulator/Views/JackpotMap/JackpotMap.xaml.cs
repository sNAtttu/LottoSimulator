using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using LottoSimulator.Model;
using System.Device.Location;
using System.Windows.Media;
using System.Diagnostics;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media.Imaging;

namespace LottoSimulator.Views.JackpotMap
{
    public partial class JackpotMap : PhoneApplicationPage
    {
        private MobileServiceCollection<Lotto, Lotto> highscores;
        private IMobileServiceTable<Lotto> highscoreTable = App.MobileService.GetTable<Lotto>();

        Map mapOfLuck;
        MapOverlay pin;
        MapLayer myLayer;
        Image mapPin = new Image();
        public JackpotMap()
        {
            InitializeComponent();
            mapOfLuck = MapOfLuck;
            BitmapImage tn = new BitmapImage();
            tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/Map_pin1.png", UriKind.Relative)).Stream);
            mapPin.Source = tn;
            mapPin.Height = 50;
            mapPin.Width = 50;
            myLayer = new MapLayer();
            pin = new MapOverlay();
            pin.Content = mapPin;
            myLayer.Add(pin);
            mapOfLuck.Layers.Add(myLayer);
            ShowLottoLocation();
        }
        private async void ShowLottoLocation()
        {       
            highscores = await highscoreTable.ToCollectionAsync();
            MapOfLuckList.ItemsSource = highscores;
        }

        private void MapOfLuckList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(MapOfLuckList != null && MapOfLuckList.SelectedItem != null)
            {
                var selectedLotto = (Lotto)MapOfLuckList.SelectedItem;
                GeoCoordinate selectedLottoLocation = new GeoCoordinate { Longitude = selectedLotto.longtitude, Latitude = selectedLotto.latitude };
                pin.GeoCoordinate = selectedLottoLocation;
                mapOfLuck.Center = selectedLottoLocation;
                mapOfLuck.ZoomLevel = 7;
            }      
        }
    }
}