using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Rcade
{
    public sealed partial class GBPlayersPage : Page
    {
        private List<TextBox> emptyBoxes { get; set; } = new List<TextBox>();
        private List<TextBox> playerNames { get; set; } = new List<TextBox>();
        private User user { get; set; }
        public int playerCount { get; private set; }

        public GBPlayersPage()
        {
            InitializeComponent();

            ApplicationView.GetForCurrentView().SetPreferredMinSize(
            new Size(
                1000,
                1000
                ));

            playerCount = 2;
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            playerCount++;

            if (playerCount > 5)
            {
                playerCount = 5;
            }
            txtPlayerCount.Text = "Players:" + " " + Convert.ToString(playerCount);
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
                        Player2.Visibility = Visibility.Visible;
                        playerNames.Add(Player2);
                        break;
                    case 3:
                        Player3.Visibility = Visibility.Visible;
                        playerNames.Add(Player3);
                        break;
                    case 4:
                        Player4.Visibility = Visibility.Visible;
                        playerNames.Add(Player4);
                        break;
                    case 5:
                        Player5.Visibility = Visibility.Visible;
                        playerNames.Add(Player5);
                        break;
                }
            }
            btnPlus.IsEnabled = false;
            btnMin.IsEnabled = false;
            btnConfirm.Visibility = Visibility.Collapsed;

            btnPlay.Visibility = Visibility.Visible;
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            playerCount--;

            if (playerCount < 2)
            {
                playerCount = 2;
            }
            txtPlayerCount.Text = "Players:" + " " + Convert.ToString(playerCount);
        }

        internal void SetUser(User user)
        {
            this.user = user;
            Player1.Text = user.userName;
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
                txtPlayerCount.Text = "Fill in your names first!";
                emptyBoxes.Clear();
            }
            else
            {
                playerCount = Convert.ToInt32(txtPlayerCount.Text);

                GBPage gb = new GBPage(playerCount, Player1.Text, Player2.Text, Player3.Text, Player4.Text, Player5.Text);
                gb.SetUser(user);

                Content = gb;
            }
        }

        private void Speler_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (((Control)sender).Name)
            {
                default:
                    break;
                case "Player2":
                    if (!ValidateUsername(Player2.Text) && Player2.Text.Length != 0)
                    {
                        Player2.Text = Player2.Text.Remove(Player2.Text.Length - 1);

                        Player2.SelectionStart = Player2.Text.Length;
                        Player2.SelectionLength = 0;
                    }
                    break;
                case "Player3":
                    if (!ValidateUsername(Player3.Text) && Player3.Text.Length != 0)
                    {
                        Player3.Text = Player3.Text.Remove(Player3.Text.Length - 1);

                        Player3.SelectionStart = Player3.Text.Length;
                        Player3.SelectionLength = 0;

                    }
                    break;
                case "Player4":
                    if (!ValidateUsername(Player4.Text) && Player4.Text.Length != 0)
                    {
                        Player4.Text = Player4.Text.Remove(Player4.Text.Length - 1);

                        Player4.SelectionStart = Player4.Text.Length;
                        Player4.SelectionLength = 0;
                    }
                    break;
                case "Player5":
                    if (!ValidateUsername(Player5.Text) && Player5.Text.Length != 0)
                    {
                        Player5.Text = Player5.Text.Remove(Player5.Text.Length - 1);

                        Player5.SelectionStart = Player5.Text.Length;
                        Player5.SelectionLength = 0;
                    }
                    break;
            }
        }

        public bool ValidateUsername(string username)
        {
            var regex = @"[a-zA-Z]$";
            var match = Regex.Match(username, regex, RegexOptions.IgnoreCase);

            return match.Success;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HubPage hub = new HubPage();
            hub.SetUser(user);

            Content = hub;
        }
    }
}