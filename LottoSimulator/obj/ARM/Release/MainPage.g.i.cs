﻿#pragma checksum "C:\Users\Santeri\Source\Repos\LottoSimulator2\LottoSimulator\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FC6BFA12F8038A3EAA369D5942ACA0EB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace LottoSimulator {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock HeaderBlock;
        
        internal System.Windows.Controls.TextBlock ResultNumbers;
        
        internal System.Windows.Controls.Button PlayButton;
        
        internal System.Windows.Controls.TextBlock playerNumbers;
        
        internal System.Windows.Controls.TextBlock LotteryResults;
        
        internal System.Windows.Controls.TextBlock EuroCounterText;
        
        internal System.Windows.Controls.TextBlock JackPotText;
        
        internal System.Windows.Controls.Button PlayUntilWin;
        
        internal System.Windows.Controls.TextBlock JackpotStatusBox;
        
        internal System.Windows.Controls.TextBlock largestHitSoFar;
        
        internal System.Windows.Controls.TextBlock winrow;
        
        internal System.Windows.Controls.TextBlock HighestRowText;
        
        internal System.Windows.Controls.Button uploadHighscore;
        
        internal System.Windows.Controls.TextBlock RowCountTextBlock;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/LottoSimulator;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.HeaderBlock = ((System.Windows.Controls.TextBlock)(this.FindName("HeaderBlock")));
            this.ResultNumbers = ((System.Windows.Controls.TextBlock)(this.FindName("ResultNumbers")));
            this.PlayButton = ((System.Windows.Controls.Button)(this.FindName("PlayButton")));
            this.playerNumbers = ((System.Windows.Controls.TextBlock)(this.FindName("playerNumbers")));
            this.LotteryResults = ((System.Windows.Controls.TextBlock)(this.FindName("LotteryResults")));
            this.EuroCounterText = ((System.Windows.Controls.TextBlock)(this.FindName("EuroCounterText")));
            this.JackPotText = ((System.Windows.Controls.TextBlock)(this.FindName("JackPotText")));
            this.PlayUntilWin = ((System.Windows.Controls.Button)(this.FindName("PlayUntilWin")));
            this.JackpotStatusBox = ((System.Windows.Controls.TextBlock)(this.FindName("JackpotStatusBox")));
            this.largestHitSoFar = ((System.Windows.Controls.TextBlock)(this.FindName("largestHitSoFar")));
            this.winrow = ((System.Windows.Controls.TextBlock)(this.FindName("winrow")));
            this.HighestRowText = ((System.Windows.Controls.TextBlock)(this.FindName("HighestRowText")));
            this.uploadHighscore = ((System.Windows.Controls.Button)(this.FindName("uploadHighscore")));
            this.RowCountTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("RowCountTextBlock")));
        }
    }
}

