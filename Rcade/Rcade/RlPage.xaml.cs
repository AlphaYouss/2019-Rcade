using System;
using System.Data;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Rcade
{
    public sealed partial class RLPage : Page
    {
        private Databasehandler_rl dbh_RL { get; set; } = new Databasehandler_rl(false);
        private RL roulette { get; set; }
        private User user { get; set; }
        private int stake { get; set; } = Convert.ToInt32(RL.chips.Fifty);
        private int totalBetValue { get; set; } = 0;
        private int maxBet { get; set; } = 1000;

        public RLPage()
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

        private void btnBet_Click(object sender, RoutedEventArgs e)
        {
            if (roulette.isPlaying == false)
            {
                if (CanBet() == true)
                {
                    if (AllowedToBet() == true)
                    {
                        string buttonName = ((Control)sender).Name;
                        PlaceBet(buttonName);

                        btnBack.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        UpdateResult("Max bet is 1000!");
                    }
                }
                else
                {
                    UpdateResult("Your balance is too low!");
                }
            }
        }

        private void btnSpin_Click(object sender, RoutedEventArgs e)
        {
            if (roulette.player.bet.Count != 0)
            {
                SetIsPlaying(true);
                Spin();
            }
            else
            {
                UpdateResult("Place a bet!");
            }
        }

        private void btnChip_Click(object sender, RoutedEventArgs e)
        {
            string buttonName = ((Control)sender).Name;
            RL.chips currentChip = RL.chips.Fifty;

            switch (buttonName)
            {
                case "btnFifty":
                    stake = Convert.ToInt32(RL.chips.Fifty);
                    currentChip = RL.chips.Fifty;
                    break;
                case "btnOnehundred":
                    stake = Convert.ToInt32(RL.chips.OneHundred);
                    currentChip = RL.chips.OneHundred;
                    break;
                case "btnTwohundred":
                    stake = Convert.ToInt32(RL.chips.TwoHundred);
                    currentChip = RL.chips.TwoHundred;
                    break;
                case "btnFivehundred":
                    stake = Convert.ToInt32(RL.chips.FiveHundred);
                    currentChip = RL.chips.FiveHundred;
                    break;
            }

            UpdateCurrentChip(currentChip);
        }

        private void btnResetStake_Click(object sender, RoutedEventArgs e)
        {
            if (roulette.player.bet.Count == 0)
            {
                UpdateResult("Nothing to reset!");
            }
            else
            {
                ResetBets();
            }
        }

        private void PlaceBet(string buttonName)
        {
            int type = 0;

            switch (buttonName)
            {
                case "btnFirstTwelve":
                case "btnSecondTwelve":
                case "btnThirdTwelve":
                    type = 1;
                    break;
                case "btnFirstRow":
                case "btnSecondRow":
                case "btnThirdRow":
                    type = 2;
                    break;
                case "btnFirstEighteen":
                case "btnLastEighteen":
                    type = 3;
                    break;
                case "btnEven":
                case "btnOdd":
                    type = 4;
                    break;
                case "btnRed":
                case "btnBlack":
                    type = 5;
                    break;
            }

            roulette.PlaceBet(buttonName, type, stake);

            UpdateResult("Bet placed!");

            balance.Text = roulette.player.balance.ToString();
            totalBet.Text = totalBetValue.ToString();

            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = stake + " bet on " + roulette.player.placedbet;
            listBoxItem.IsSelected = true;

            betList.Items.Add(listBoxItem);
        }

        private void Spin()
        {
            winningNumber.Text = "-";

            btnResetStake.IsEnabled = false;
            btnSpin.IsEnabled = false;

            SpinUpdate();

            roulette.SpinWheel();
            UpdateWinningNumber();
        }

        private void ResetBets()
        {
            int betMoney = 0;

            foreach (var bet in roulette.player.bet)
            {
                betMoney = betMoney + bet.Value;
            }

            roulette.player.CancelBet(betMoney);

            UpdateResult("Bets resetted!");

            balance.Text = roulette.player.balance.ToString();
            totalBet.Text = "0";

            betList.Items.Clear();
            totalBetValue = 0;

            btnBack.Visibility = Visibility.Visible;
        }

        private bool CanBet()
        {
            if (roulette.player.balance - stake > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool AllowedToBet()
        {
            if (totalBetValue + stake > maxBet)
            {
                return false;
            }
            else
            {
                totalBetValue = totalBetValue + stake;
                return true;
            }
        }

        private void UpdateCurrentChip(RL.chips chip)
        {
            string location = "";

            switch (chip)
            {
                case RL.chips.Fifty:
                    location = "ms-appx:///Assets/Images/chips/50.png";
                    break;
                case RL.chips.OneHundred:
                    location = "ms-appx:///Assets/Images/chips/100.png";
                    break;
                case RL.chips.TwoHundred:
                    location = "ms-appx:///Assets/Images/chips/200.png";
                    break;
                case RL.chips.FiveHundred:
                    location = "ms-appx:///Assets/Images/chips/500.png";
                    break;
            }
            BitmapImage chipSource = new BitmapImage(new Uri(location));
            currentChip.Source = chipSource;
        }

        private async void UpdateResult(string message)
        {
            result.Text = message;
            await Task.Delay(2000);
            result.Text = "";
        }

        private async void UpdateWinningNumber()
        {
            await Task.Delay(5000);

            int winningNumberValue = roulette.wheel.winningNumber.number;

            winningNumber.Text = winningNumberValue.ToString();

            roulette.Payout();
            SetUserStats(roulette.player.balance);

            WinningMessage();
            ResetTable();
        }

        private async void WinningMessage()
        {
            if (roulette.totalMoneyWon != 0)
            {
                result.Text = "You won " + roulette.totalMoneyWon + "!";
                await Task.Delay(5000);
            }
            else
            {
                result.Text = "You lost " + totalBetValue + "!";
                await Task.Delay(5000);
            }
            result.Text = "";
        }

        private async void ResetTable()
        {
            await Task.Delay(5000);

            balance.Text = roulette.player.balance.ToString();
            totalBet.Text = "0";

            betList.Items.Clear();

            roulette.SetIsPlaying(false);
            roulette.player.ClearBet();

            totalBetValue = 0;
            roulette.SetTotalMoneyWon(0);

            btnSpin.IsEnabled = true;
            btnResetStake.IsEnabled = true;
            btnBack.Visibility = Visibility.Visible;
        }

        private async void SpinUpdate()
        {
            SpinImage();
            result.Text = "Spinning.";
            await Task.Delay(1000);

            SpinImage();
            result.Text = "Spinning..";
            await Task.Delay(1000);

            SpinImage();
            result.Text = "Spinning...";
            await Task.Delay(1000);

            SpinImage();
            result.Text = "Spinning....";
            await Task.Delay(1000);

            SpinImage();
            result.Text = "Spinning.....";
            await Task.Delay(1000);
        }

        private async void SpinImage()
        {
            RotateTransform m_transform = new RotateTransform();
            wheel.RenderTransform = m_transform;

            while (m_transform.Angle != 360)
            {
                m_transform.Angle = m_transform.Angle + 36;
                await Task.Delay(100);
            }
        }

        private int GetUserStats()
        {
            if (dbh_RL.TestConnection() == false)
            {
                MainPage main = new MainPage();
                Content = main;

                return 0;
            }
            else
            {
                dbh_RL.GetUser(user.id);

                int bjSaldo = 0;

                foreach (DataRow row in dbh_RL.table.Rows)
                {
                    bjSaldo = Convert.ToInt32(row["saldo"]);
                }

                return bjSaldo;
            }
        }

        internal void SetUser(User user)
        {
            this.user = user;
        }

        internal void SetUserStats()
        {
            if (dbh_RL.TestConnection() == false)
            {
                MainPage main = new MainPage();
                Content = main;
            }
            else
            {
                roulette = new RL(GetUserStats(), user.userName);

                balance.Text = roulette.player.balance.ToString();
                playerName.Text = user.userName;
            }
        }

        private void SetUserStats(int balance)
        {
            if (dbh_RL.TestConnection() == false)
            {
                MainPage main = new MainPage();
                Content = main;
            }
            else
            {
                dbh_RL.SetUser(user.id, balance, roulette.player.lastPlayed);
            }
        }

        private void SetIsPlaying(bool value)
        {
            roulette.SetIsPlaying(value);
        }
    }
}