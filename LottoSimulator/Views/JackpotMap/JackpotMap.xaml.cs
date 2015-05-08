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

namespace LottoSimulator.Views.JackpotMap
{
    public partial class JackpotMap : PhoneApplicationPage
    {
        private MobileServiceCollection<Lotto, Lotto> highscores;
        private IMobileServiceTable<Lotto> highscoreTable = App.MobileService.GetTable<Lotto>();
        public JackpotMap()
        {
            InitializeComponent();
            ShowLottoLocation();
        }
        private async void ShowLottoLocation()
        {       
            highscores = await highscoreTable.ToCollectionAsync();
            MapOfLuckList.ItemsSource = highscores;
            //foreach()
            GeoCoordinate myloc = new GeoCoordinate { };
        }
    }
}