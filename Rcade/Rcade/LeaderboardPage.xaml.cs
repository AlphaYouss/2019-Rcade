using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Rcade
{
    public sealed partial class LeaderboardPage : Page
    {
        private User user { get; set; }
        public int number { get; private set; } = 0;

        public LeaderboardPage()
        {
            InitializeComponent();

            ApplicationView.GetForCurrentView().SetPreferredMinSize(
            new Size(
            1000,
            1000
            ));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HubPage hub = new HubPage();
            hub.SetUser(user);

            Content = hub;
        }

        private void btnGame_Click(object sender, RoutedEventArgs e)
        {
            string buttonName = Convert.ToString(((Control)sender).Name);

            switch (buttonName)
            {
                case "btnBlackjack":
                    number = 1;
                    break;
                case "btnRoulette":
                    number = 2;
                    break;
                case "btnHangman":
                    number = 3;
                    break;
                case "btnBKE":
                    number = 4;
                    break;
            }

            LeaderbordGamePage lbg = new LeaderbordGamePage(number);
            lbg.SetUser(user);

            Content = lbg;
        }

        internal void SetUser(User user)
        {
            this.user = user;
        }
    }
}