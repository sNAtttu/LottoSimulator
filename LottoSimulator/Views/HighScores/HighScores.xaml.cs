using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LottoSimulator.Model;
using Microsoft.WindowsAzure.MobileServices;
using System.Diagnostics;

namespace LottoSimulator.Views.HighScores
{
    public partial class HighScores : PhoneApplicationPage
    {
        private MobileServiceCollection<Lotto, Lotto> highscores;
        private IMobileServiceTable<Lotto> highscoreTable = App.MobileService.GetTable<Lotto>();
        public HighScores()
        {
            InitializeComponent();
        }

        private async void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtboxLoading.Text = "Loading highscores.....";
                highscores = await highscoreTable.ToCollectionAsync();
                highscoreTemplate.ItemsSource = highscores;
                txtboxLoading.Text = string.Empty;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void MapOfLuckButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/JackpotMap/JackpotMap.xaml", UriKind.Relative));
        }
    }
}