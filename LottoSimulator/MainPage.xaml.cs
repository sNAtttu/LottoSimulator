using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LottoSimulator.Resources;
using LottoSimulator.Model;
using LottoSimulator.Engine;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Device.Location;
using Windows.Devices.Geolocation;

namespace LottoSimulator
{
    public partial class MainPage : PhoneApplicationPage
    {
        LottoModel playerLotto;
        LottoModel lotto;
        LottoModel highestLotto;
        LottoMachine machine;
        Popup highscorePopup;
        TextBox playerNameTextBox;

        private long euroCounter = 0;
        private int jackpotCounter = 0;
        private int largestHit = 0;

        private double tempLongtitude;

        public double TempLongtitude
        {
            get { return tempLongtitude; }
            set { tempLongtitude = value; }
        }
        private double tempLatitude;

        public double TempLatitude
        {
            get { return tempLatitude; }
            set { tempLatitude = value; }
        }

        private bool cancelJackpot = false;
        public int LargestHit
        {
            get { return largestHit; }
            set { largestHit = value; }
        }

        public int JackpotCounter
        {
            get { return jackpotCounter; }
            set { jackpotCounter = value; }
        }


        public long EuroCounter
        {
            get { return euroCounter; }
            set { euroCounter = value; }
        }
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            lotto = new LottoModel();
            machine = new LottoMachine();
            highestLotto = new LottoModel();
            playerLotto = new LottoModel();
            highscorePopup = new Popup();
            ResultNumbers.Text = lotto.ToString() + " + " + lotto.ExtraOne + " " + lotto.ExtraTwo;
            playerNumbers.Text = playerLotto.ToString();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            lotto = machine.GenerateNumbers(lotto);
            ResultNumbers.Text = lotto.ToString() + " + " + lotto.ExtraOne + " " + lotto.ExtraTwo;
            int[] results = new int[2];
            results = machine.checkWinnings(playerLotto, lotto);
            int tempHit = results[0];
            LotteryResults.Text = "Sinulla oli oikein " + results[0] + " numeroa ja lisänumeroita oli oikein " + results[1] + " kappaletta.";
            if (results[0] == 7)
            {
                JackpotCounter++;
            }
            else
            {
                if (tempHit > LargestHit)
                {
                    LargestHit = tempHit;
                    largestHitSoFar.Text = LargestHit.ToString() + " oikein.";
                    highestLotto.OneBall = lotto.OneBall;
                    highestLotto.TwoBall = lotto.TwoBall;
                    highestLotto.ThreeBall = lotto.ThreeBall;
                    highestLotto.FourBall = lotto.FourBall;
                    highestLotto.FiveBall = lotto.FiveBall;
                    highestLotto.SixBall = lotto.SixBall;
                    highestLotto.SevenBall = lotto.SevenBall;
                    highestLotto.ExtraOne = lotto.ExtraOne;
                    highestLotto.ExtraTwo = lotto.ExtraTwo;
                }
            }
            EuroCounter++;
            EuroCounterText.Text = EuroCounter.ToString() + " Euroa.";
            JackPotText.Text = JackpotCounter.ToString() + " Kappaletta.";
            HighestRowText.Text = highestLotto.ToString() + " + " + highestLotto.ExtraOne + " " + highestLotto.ExtraTwo;
        }

        private void MakeLotto_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/MakeLotto/MakeLotto.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            int[] tempNumbers = new int[7];
            string[] tempArray = new string[7];
            string lottoNumbers = "";

            if (NavigationContext.QueryString.TryGetValue("numbers", out lottoNumbers))
            {
                tempArray = lottoNumbers.Split(' ');

                for (int i = 0; i < 7; i++)
                {
                    tempNumbers[i] = int.Parse(tempArray[i]);
                }
            }

            playerLotto.OneBall = tempNumbers[0];
            playerLotto.TwoBall = tempNumbers[1];
            playerLotto.ThreeBall = tempNumbers[2];
            playerLotto.FourBall = tempNumbers[3];
            playerLotto.FiveBall = tempNumbers[4];
            playerLotto.SixBall = tempNumbers[5];
            playerLotto.SevenBall = tempNumbers[6];

            playerNumbers.Text = playerLotto.ToString();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            playerLotto = new LottoModel();
            lotto = new LottoModel();
            EuroCounter = 0;
            playerNumbers.Text = playerLotto.ToString();
            ResultNumbers.Text = lotto.ToString();
            EuroCounterText.Text = EuroCounter.ToString() + " Euroa.";
            JackpotCounter = 0;
            JackPotText.Text = JackpotCounter.ToString() + " Kappaletta.";
        }

        private async void PlayUntilWin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JackpotStatusBox.Text = "Tarvittavien rivien määrän laskeminen käynnistetty.";
                PlayUntilWin.IsEnabled = false;
                List<string> data = await ProcessAsyncData();
                Done();
            }
            catch(Exception ex)
            {
                JackpotStatusBox.Text = ex.Message;
            }
        }

        private void Done()
        {
            JackpotStatusBox.Text = "";
            EuroCounterText.Text = EuroCounter.ToString() + " Euroa.";
            JackPotText.Text = JackpotCounter.ToString() + " Kappaletta.";
            if (!cancelJackpot)
            {
                JackpotStatusBox.Text = "Alhaalta näet kuinka monta euroa joudut käyttämään saadaksesi päävoiton.";
                JackpotCounter++;
            }
            else
            {
                JackpotStatusBox.Text = "Keskeytetty!";
                cancelJackpot = false;
            }
            PlayUntilWin.IsEnabled = true;
            EuroCounterText.Text = EuroCounter.ToString() + " Euroa.";
            largestHitSoFar.Text = LargestHit.ToString() + " oikein.";
            ResultNumbers.Text = lotto.ToString() + " + " + lotto.ExtraOne + " " + lotto.ExtraTwo;
            HighestRowText.Text = highestLotto.ToString() + " + " + highestLotto.ExtraOne + " " + highestLotto.ExtraTwo;
        }

        async Task<List<string>> ProcessAsyncData()
        {
            bool jackpot = false;
            JackpotStatusBox.Text = "Tarvittavien rivien määrän laskeminen on käynnissä.";
            var t = await Task.Run(() =>
            {
                
                List<string> result = new List<string>();
                while (!jackpot)
                {
                    if (cancelJackpot)
                    {
                        break;
                    }
                    EuroCounter++;                  
                    lotto = machine.GenerateNumbers(lotto);
                    int[] results = new int[2];
                    int tempHit = 0;
                    results = machine.checkWinnings(playerLotto, lotto);
                    tempHit = results[0];
                    if (tempHit != 7)
                    {
                        if (tempHit > LargestHit)
                        {
                            LargestHit = tempHit;
                            highestLotto.OneBall = lotto.OneBall;
                            highestLotto.TwoBall = lotto.TwoBall;
                            highestLotto.ThreeBall = lotto.ThreeBall;
                            highestLotto.FourBall = lotto.FourBall;
                            highestLotto.FiveBall = lotto.FiveBall;
                            highestLotto.SixBall = lotto.SixBall;
                            highestLotto.SevenBall = lotto.SevenBall;
                            highestLotto.ExtraOne = lotto.ExtraOne;
                            highestLotto.ExtraTwo = lotto.ExtraTwo;
                        }
                    }
                    else
                    {
                        LargestHit = tempHit;
                        jackpot = true;
                        result.Add(EuroCounter.ToString());
                    }
                    
                }
                return result;
            });
            return t;
        }

        private void UpdateEuros_Click(object sender, RoutedEventArgs e)
        {
            EuroCounterText.Text = EuroCounter.ToString() + " Euroa.";
            largestHitSoFar.Text = LargestHit.ToString() + " oikein.";
            ResultNumbers.Text = lotto.ToString() + " + " + lotto.ExtraOne + " " + lotto.ExtraTwo;
            HighestRowText.Text = highestLotto.ToString() + " + " + highestLotto.ExtraOne + " " + highestLotto.ExtraTwo;
        }

        private void CancelTask_Click(object sender, RoutedEventArgs e)
        {
            PlayUntilWin.IsEnabled = true;
            cancelJackpot = true;
        }

        private void uploadHighscore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Border border = new Border();
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.BorderThickness = new Thickness(5.0);

                StackPanel panel1 = new StackPanel();
                panel1.Background = new SolidColorBrush(Colors.Black);

                Button button1 = new Button();
                button1.Content = "OK";
                button1.Margin = new Thickness(5.0);
                button1.Click += new RoutedEventHandler(button1_Click);
                Button cancelUpload = new Button();
                cancelUpload.Content = "Peruuta";
                cancelUpload.Margin = new Thickness(5.0);
                cancelUpload.Click += new RoutedEventHandler(cancelUpload_Click);
                TextBlock textblock1 = new TextBlock();
                textblock1.Text = "Anna pelaajan nimi:";
                textblock1.Foreground = new SolidColorBrush(Colors.White);
                textblock1.FontSize = 20;
                textblock1.Margin = new Thickness(5.0);
                playerNameTextBox = new TextBox();
                playerNameTextBox.Margin = new Thickness(5.0);
                playerNameTextBox.Width = 150;
                panel1.Children.Add(textblock1);
                panel1.Children.Add(playerNameTextBox);
                panel1.Children.Add(button1);
                panel1.Children.Add(cancelUpload);
                border.Child = panel1;

                // Set the Child property of Popup to the border 
                // which contains a stackpanel, textblock and button.
                highscorePopup.Child = border;

                // Set where the popup will show up on the screen.
                highscorePopup.VerticalOffset = 150;
                highscorePopup.HorizontalOffset = 75;

                // Open the popup.
                highscorePopup.IsOpen = true;
            }
            catch (Exception ex)
            {
                JackpotStatusBox.Text = ex.Message;
            }
        }

        private void cancelUpload_Click(object sender, RoutedEventArgs e)
        {
            highscorePopup.IsOpen = false;
        }
        async void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Lotto highScore = new Lotto { Id = Guid.NewGuid().ToString(),
                    WinningRow = highestLotto.ToString(), 
                    Cost = EuroCounter, 
                    playerName = playerNameTextBox.Text,
                    latitude = TempLatitude,
                    longtitude = TempLongtitude
                };
                await App.MobileService.GetTable<Lotto>().InsertAsync(highScore);
            }
            catch (Exception ex)
            {
                JackpotStatusBox.Text = ex.Message;
                Debug.WriteLine("ONGELMA: " + ex.Message + " " + ex.InnerException.ToString());
            }
            highscorePopup.IsOpen = false;
        }

        private void JackPotMenu_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/HighScores/HighScores.xaml", UriKind.Relative));
        }
        private void HelpMenu_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/HelpMenu/HelpMenu.xaml", UriKind.Relative));
        }

        private async void LocateMe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Geolocator myGeolocator = new Geolocator();
                Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
                Geocoordinate myGeocoordinate = myGeoposition.Coordinate;

                tempLatitude = myGeocoordinate.Point.Position.Latitude;
                tempLongtitude = myGeocoordinate.Point.Position.Longitude;

                JackpotStatusBox.Text = "paikannus onnistui!";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                JackPotText.Text = ex.Message;
            }
        }
    }
}