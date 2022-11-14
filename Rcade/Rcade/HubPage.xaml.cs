using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Rcade
{
    public sealed partial class HubPage : Page
    {
        private User user { get; set; }
        public HubPage()
        {
            InitializeComponent();

            ApplicationView.GetForCurrentView().SetPreferredMinSize(
            new Size(
            1000,
            1000
            ));
        }

        internal void SetUser(User user)
        {
            this.user = user;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainPage main = new MainPage();
            Content = main;
        }

        private void btnBJ_Click(object sender, RoutedEventArgs e)
        {
            BJPage bj = new BJPage();
            bj.SetUser(user);
            bj.SetUp();

            if (user.dbh.TestConnection() == false)
            {
                btnLogout_Click(sender, e);
            }
            else
            {
                bj.SetUserStats();
                Content = bj;
            }
        }

        private void btnTTT_Click(object sender, RoutedEventArgs e)
        {
            TTTPage ttt = new TTTPage();
            ttt.SetUser(user);
            ttt.SetUp();

            if (user.dbh.TestConnection() == false)
            {
                btnLogout_Click(sender, e);
            }
            else
            {
                ttt.SetUserStats();
                Content = ttt;
            }
        }

        private void btnLeaderboard_Click(object sender, RoutedEventArgs e)
        {
            if (user.dbh.TestConnection() == false)
            {
                btnLogout_Click(sender, e);
            }
            else
            {
                LeaderboardPage lb = new LeaderboardPage();
                lb.SetUser(user);

                Content = lb;
            }
        }

        private void btnRoulette_Click(object sender, RoutedEventArgs e)
        {
            if (user.dbh.TestConnection() == false)
            {
                btnLogout_Click(sender, e);
            }
            else
            {
                RLPage rl = new RLPage();
                rl.SetUser(user);
                rl.SetUserStats();

                Content = rl;
            }
        }

        private void btnHangman_Click(object sender, RoutedEventArgs e)
        {
            if (user.dbh.TestConnection() == false)
            {
                btnLogout_Click(sender, e);
            }
            else
            {
                HmPage hm = new HmPage();
                hm.SetUser(user);
                hm.Setup();

                Content = hm;
            }
        }

        private void BtnGooseboard_Click(object sender, RoutedEventArgs e)
        {
            GBPlayersPage gbp = new GBPlayersPage();
            gbp.SetUser(user);

            Content = gbp;
        }
    }
}