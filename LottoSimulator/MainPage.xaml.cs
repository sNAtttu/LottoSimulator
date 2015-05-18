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
        Button locationButton;

        private bool enableButtons = false;

        private long euroCounter = 0;
        private int jackpotCounter = 0;
        private int largestHit = 0;
        private int largestHitExtras = 0;
        private int rowCounter = 0;

        public int RowCounter
        {
            get { return rowCounter; }
            set { rowCounter = value; }
        }

        public int LargestHitExtras
        {
            get { return largestHitExtras; }
            set { largestHitExtras = value; }
        }

        private double tempLongitude;

        public double TempLongitude
        {
            get { return tempLongitude; }
            set { tempLongitude = value; }
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
            RowCounter++;
            RowCountTextBlock.Text = RowCounter.ToString();
            lotto = machine.GenerateNumbers(lotto);
            ResultNumbers.Text = lotto.ToString() + " + " + lotto.ExtraOne + " " + lotto.ExtraTwo;
            int[] results = new int[2];
            results = machine.checkWinnings(playerLotto, lotto);
            int tempHit = results[0];
            int tempExtra = results[1];
            int winSum = machine.GetWinnings(tempHit, tempExtra);
            LotteryResults.Text = "You had " + results[0] + " numbers correct and " + results[1] + " extra number. And you won "+winSum+" euros!";
            if (results[0] == 7)
            {
                LargestHit = tempHit;
                LargestHitExtras = tempExtra;
                largestHitSoFar.Text = LargestHit.ToString();
                highestLotto.OneBall = lotto.OneBall;
                highestLotto.TwoBall = lotto.TwoBall;
                highestLotto.ThreeBall = lotto.ThreeBall;
                highestLotto.FourBall = lotto.FourBall;
                highestLotto.FiveBall = lotto.FiveBall;
                highestLotto.SixBall = lotto.SixBall;
                highestLotto.SevenBall = lotto.SevenBall;
                highestLotto.ExtraOne = lotto.ExtraOne;
                highestLotto.ExtraTwo = lotto.ExtraTwo;
                JackpotCounter++;
                JackpotStatusBox.Text = "Below you can see how many Euros it will take to get 7/7.";
                EuroCounterText.Text = EuroCounter.ToString() + " Euros.";
                JackPotText.Text = JackpotCounter.ToString() + " times.";
                string jplargestHitText = LargestHit.ToString() + " + " + LargestHitExtras.ToString();
                largestHitSoFar.Text = jplargestHitText;
                HighestRowText.Text = highestLotto.ToString() + " + " + highestLotto.ExtraOne + " " + highestLotto.ExtraTwo;
                uploadHighscore.IsEnabled = true;
            }
            else
            {
                if (tempHit > LargestHit)
                {
                    LargestHit = tempHit;
                    LargestHitExtras = tempExtra;
                    largestHitSoFar.Text = LargestHit.ToString();
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
                else if (tempHit == LargestHit && tempExtra > LargestHitExtras)
                {
                    LargestHitExtras = tempExtra;
                    largestHitSoFar.Text = LargestHit.ToString();
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
            EuroCounter = EuroCounter - winSum;
            EuroCounterText.Text = EuroCounter.ToString() + " Euros.";
            JackPotText.Text = JackpotCounter.ToString() + " times.";
            string largestHitText = LargestHit.ToString() + " + " + LargestHitExtras.ToString();
            largestHitSoFar.Text = largestHitText;
            HighestRowText.Text = highestLotto.ToString() + " + " + highestLotto.ExtraOne + " " + highestLotto.ExtraTwo;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            int[] tempNumbers = new int[7];
            string[] tempArray = new string[7];
            string lottoNumbers = "";

            if (enableButtons == false)
            {
                LotteryResults.Text = "Before you can start, you must make a lottery row! Press '+' or '?' on appbar.";
            }

            if (NavigationContext.QueryString.TryGetValue("numbers", out lottoNumbers))
            {
                if (enableButtons == false)
                {
                    enableButtons = true;
                    PlayButton.IsEnabled = true;
                    PlayUntilWin.IsEnabled = true;
                }

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

        private async void PlayUntilWin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LotteryResults.Text = "";
                JackpotStatusBox.Text = "Lottery for 7/7 is on!";
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
            EuroCounterText.Text = EuroCounter.ToString() + " Euros.";
            if (!cancelJackpot)
            {
                JackpotStatusBox.Text = "Below you can see how many Euros it will take to get 7/7.";
                uploadHighscore.IsEnabled = true;
                JackpotCounter++;
            }
            else
            {
                JackpotStatusBox.Text = "Interrupted!";
                cancelJackpot = false;
            }
            PlayUntilWin.IsEnabled = true;
            EuroCounterText.Text = EuroCounter.ToString() + " Euros.";
            string largestHitText = LargestHit.ToString() + " + " + LargestHitExtras.ToString();
            largestHitSoFar.Text = largestHitText;
            ResultNumbers.Text = lotto.ToString() + " + " + lotto.ExtraOne + " " + lotto.ExtraTwo;
            HighestRowText.Text = highestLotto.ToString() + " + " + highestLotto.ExtraOne + " " + highestLotto.ExtraTwo;
            JackPotText.Text = JackpotCounter.ToString() + " times.";
            RowCountTextBlock.Text = RowCounter.ToString();
        }

        async Task<List<string>> ProcessAsyncData()
        {
            bool jackpot = false;
            JackpotStatusBox.Text = "Lottery for 7/7 is on!";
            var t = await Task.Run(() =>
            {
                
                List<string> result = new List<string>();

                while (!jackpot)
                {
                    RowCounter++;
                    if (cancelJackpot)
                    {
                        break;
                    }
                    lotto = machine.GenerateNumbers(lotto);
                    int[] results = new int[2];
                    int tempHit = 0;
                    int tempExtra = 0;
                    results = machine.checkWinnings(playerLotto, lotto);
                    tempHit = results[0];
                    tempExtra = results[1];
                    EuroCounter++;
                    if (tempHit != 7)
                    {
                        int winSum = machine.GetWinnings(tempHit, tempExtra);
                        EuroCounter = EuroCounter - winSum;
                        if (tempHit > LargestHit)
                        {
                            LargestHit = tempHit;
                            LargestHitExtras = tempExtra;
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
                        else if (tempHit == LargestHit && tempExtra > LargestHitExtras)
                        {
                            LargestHitExtras = tempExtra;
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
                        int winSum = machine.GetWinnings(tempHit, tempExtra);
                        EuroCounter = EuroCounter - winSum;
                        highestLotto.OneBall = lotto.OneBall;
                        highestLotto.TwoBall = lotto.TwoBall;
                        highestLotto.ThreeBall = lotto.ThreeBall;
                        highestLotto.FourBall = lotto.FourBall;
                        highestLotto.FiveBall = lotto.FiveBall;
                        highestLotto.SixBall = lotto.SixBall;
                        highestLotto.SevenBall = lotto.SevenBall;
                        highestLotto.ExtraOne = lotto.ExtraOne;
                        highestLotto.ExtraTwo = lotto.ExtraTwo;
                        LargestHit = tempHit;
                        LargestHitExtras = tempExtra;
                        jackpot = true;
                        result.Add(EuroCounter.ToString());
                    }
                }
                return result;
            });
            return t;
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
                locationButton = new Button();
                locationButton.Content = "Locate";
                locationButton.Margin = new Thickness(5.0);
                locationButton.Click += new RoutedEventHandler(LocateMe_Click);
                Button cancelUpload = new Button();
                cancelUpload.Content = "Cancel";
                cancelUpload.Margin = new Thickness(5.0);
                cancelUpload.Click += new RoutedEventHandler(cancelUpload_Click);
                TextBlock textblock1 = new TextBlock();
                textblock1.Text = "Player:";
                textblock1.Foreground = new SolidColorBrush(Colors.White);
                textblock1.FontSize = 20;
                textblock1.Margin = new Thickness(5.0);
                playerNameTextBox = new TextBox();
                playerNameTextBox.Margin = new Thickness(5.0);
                playerNameTextBox.Width = 250;
                panel1.Children.Add(textblock1);
                panel1.Children.Add(playerNameTextBox);
                panel1.Children.Add(locationButton);
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
                    longitude = TempLongitude
                };
                highscorePopup.IsOpen = false;
                await App.MobileService.GetTable<Lotto>().InsertAsync(highScore);
            }
            catch (Exception ex)
            {
                JackpotStatusBox.Text = ex.Message;
            }
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
                locationButton.IsEnabled = false;
                Geolocator myGeolocator = new Geolocator();
                Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
                Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
                tempLatitude = myGeocoordinate.Point.Position.Latitude;
                tempLongitude = myGeocoordinate.Point.Position.Longitude;
                locationButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                JackPotText.Text = ex.Message;
            }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            PlayUntilWin.IsEnabled = true;
            cancelJackpot = true;
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            playerLotto = new LottoModel();
            lotto = new LottoModel();
            highestLotto = new LottoModel();
            RowCounter = 0;
            uploadHighscore.IsEnabled = false;
            EuroCounter = 0;
            playerNumbers.Text = playerLotto.ToString();
            ResultNumbers.Text = lotto.ToString();
            EuroCounterText.Text = EuroCounter.ToString() + " Euros.";
            JackpotCounter = 0;
            JackPotText.Text = JackpotCounter.ToString() + " times.";
            enableButtons = false;
            PlayUntilWin.IsEnabled = false;
            PlayButton.IsEnabled = false;
        }

        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            EuroCounterText.Text = EuroCounter.ToString() + " Euros.";
            RowCountTextBlock.Text = RowCounter.ToString();
            string largestHitText = LargestHit.ToString() + " + " + LargestHitExtras.ToString();
            largestHitSoFar.Text = largestHitText;
            ResultNumbers.Text = lotto.ToString() + " + " + lotto.ExtraOne + " " + lotto.ExtraTwo;
            HighestRowText.Text = highestLotto.ToString() + " + " + highestLotto.ExtraOne + " " + highestLotto.ExtraTwo;
        }

        private void ApplicationBarIconButton_Click_3(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/MakeLotto/MakeLotto.xaml", UriKind.Relative));
        }

    }
}