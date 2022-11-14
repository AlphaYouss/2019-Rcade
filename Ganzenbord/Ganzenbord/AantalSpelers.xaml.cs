using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ganzenbord
{

    public sealed partial class AantalSpelers : Page
    {
        List<TextBox> emptyBoxes = new List<TextBox>();
        List<TextBox> playerNames = new List<TextBox>();
        public int playerCount { get; private set; }


        public AantalSpelers()
        {
            this.InitializeComponent();
            playerCount = 2;
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            playerCount++;
            if (playerCount > 5)
            {
                playerCount = 5;
            }
            txtPlayerCount.Text = Convert.ToString(playerCount);
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= playerCount; i++)
            {
                switch (i)
                {
                    default:
                        break;
                    case 1:
                        s1.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        Speler2.Visibility = Visibility.Visible;
                        playerNames.Add(Speler2);
                        break;
                    case 3:
                        Speler3.Visibility = Visibility.Visible;
                        playerNames.Add(Speler3);
                        break;
                    case 4:
                        Speler4.Visibility = Visibility.Visible;
                        playerNames.Add(Speler4);
                        break;
                    case 5:
                        Speler5.Visibility = Visibility.Visible;
                        playerNames.Add(Speler5);
                        break;
                }
            }
            btnPlus.IsEnabled = false;
            btnMin.IsEnabled = false;
            btnPlay.Visibility = Visibility.Visible;
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            playerCount--;
            if (playerCount < 2)
            {
                playerCount = 2;
            }
            txtPlayerCount.Text = Convert.ToString(playerCount);
        }
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            txtPlayerCount.Text = Convert.ToString(playerCount);

            for (int i = 0; i < playerNames.Count; i++)
            {
                if (string.IsNullOrEmpty(playerNames[i].Text))
                {
                     emptyBoxes.Add(playerNames[i]);
                }  
            }
        
            if (emptyBoxes.Count != 0)
            {
                txtPlayerCount.Text = "Voer eerst je namen in!";
                emptyBoxes.Clear();
            }
            else
            {
                playerCount = Convert.ToInt32(txtPlayerCount.Text);
                MainPage mainPage = new MainPage(playerCount, Speler2.Text, Speler3.Text, Speler4.Text, Speler5.Text);
                Content = mainPage;
            }
        }

        private void Speler_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (((Control)sender).Name)
            {
                default:
                    break;
                case "Speler2":
                    if (!ValidateUsername(Speler2.Text) && Speler2.Text.Length != 0)
                    {
                        Speler2.Text = Speler2.Text.Remove(Speler2.Text.Length - 1);

                        Speler2.SelectionStart = Speler2.Text.Length;
                        Speler2.SelectionLength = 0;
                    }
                    break;
                case "Speler3":
                    if (!ValidateUsername(Speler3.Text) && Speler3.Text.Length != 0)
                    {
                        Speler3.Text=Speler3.Text.Remove(Speler3.Text.Length - 1);

                        Speler3.SelectionStart = Speler3.Text.Length;
                        Speler3.SelectionLength = 0;

                    }
                    break;
                case "Speler4":
                    if (!ValidateUsername(Speler4.Text) && Speler4.Text.Length != 0)
                    {
                        Speler4.Text=Speler4.Text.Remove(Speler4.Text.Length - 1);

                        Speler4.SelectionStart = Speler4.Text.Length;
                        Speler4.SelectionLength = 0;
                    }
                    break;
                case "Speler5":
                    if (!ValidateUsername(Speler5.Text) && Speler5.Text.Length != 0)
                    { 
                        Speler5.Text=Speler5.Text.Remove(Speler5.Text.Length - 1);

                        Speler5.SelectionStart = Speler5.Text.Length;
                        Speler5.SelectionLength = 0;
                    }
                    break;
            }
        }

        public bool ValidateUsername(string username)
        {
            var regex = @"[a-zA-Z]$";
            var match = Regex.Match(username, regex, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
