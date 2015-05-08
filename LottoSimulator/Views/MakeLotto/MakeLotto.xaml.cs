using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using LottoSimulator.Model;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LottoSimulator.Views.MakeLotto
{
    public partial class MakeLotto : PhoneApplicationPage
    {
        Popup p;
        private List<int> selectedNumbers;
        public MakeLotto()
        {
            InitializeComponent();
            p = new Popup();
            selectedNumbers = new List<int>();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button pressedButton = (Button)sender;
            string chosenNumber = pressedButton.Content.ToString();
            selectedNumbers.Add(int.Parse(chosenNumber));
            if (selectedNumbers.Count == 7)
            {
                foreach (var element in ContentPanel.Children)
                {
                    if (element is Button)
                    {
                        Button tempButton = (Button)element;
                        tempButton.IsEnabled = false;
                    }
                }
                ChooseSelected.IsEnabled = true;
            }
            pressedButton.IsEnabled = false;
        }

        private void ChooseSelected_Click(object sender, RoutedEventArgs e)
        {
            LottoModel playerLotto = new LottoModel();
            selectedNumbers.Sort();
            playerLotto.OneBall = selectedNumbers[0];
            playerLotto.TwoBall = selectedNumbers[1];
            playerLotto.ThreeBall = selectedNumbers[2];
            playerLotto.FourBall = selectedNumbers[3];
            playerLotto.FiveBall = selectedNumbers[4];
            playerLotto.SixBall = selectedNumbers[5];
            playerLotto.SevenBall = selectedNumbers[6];
            NavigationService.Navigate(new Uri("/MainPage.xaml?numbers="+playerLotto.ToString(),UriKind.Relative));
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
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
            TextBlock textblock1 = new TextBlock();
            textblock1.Text = "Valitse 7 numeroa ja paina OK näppäintä.";
            textblock1.Foreground = new SolidColorBrush(Colors.White);
            textblock1.FontSize = 16;
            textblock1.Margin = new Thickness(5.0);
            panel1.Children.Add(textblock1);
            panel1.Children.Add(button1);
            border.Child = panel1;

            // Set the Child property of Popup to the border 
            // which contains a stackpanel, textblock and button.
            p.Child = border;

            // Set where the popup will show up on the screen.
            p.VerticalOffset = 150;
            p.HorizontalOffset = 75;

            // Open the popup.
            p.IsOpen = true;
        }
        void button1_Click(object sender, RoutedEventArgs e)
        {
            // Close the popup.
            p.IsOpen = false;

        }
    }
}