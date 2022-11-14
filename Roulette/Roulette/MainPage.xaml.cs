using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Roulette
{
    public sealed partial class MainPage : Page
    {
        private RL roulette { get; set; } = new RL();
        private int stake { get; set; } = Convert.ToInt32(RL.chips.Fifty);
        private int totalBetValue { get; set; } = 0;
        private int maxBet { get; set; } = 1000;

        public MainPage()
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

        }

        private void btnBet_Click(object sender, RoutedEventArgs e)
        {
            if (GetIsPlaying() == false)
            {
                if (CanBet() == true)
                {
                    if (AllowedToBet() == true)
                    {
                        string buttonName = ((Control)sender).Name;
                        PlaceBet(buttonName);
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
            SetIsPlaying(true);

            if (roulette.player.bet.Count != 0)
            {
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
            if (GetBets() == 0)
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
            totalBet.Text = "Total bet: " + totalBetValue;

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

            SpinMessage();

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
            totalBet.Text = "Total bet: 0";

            betList.Items.Clear();
            totalBetValue = 0;
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
                    location = "ms-appx:///Assets/Fiche50.png";
                    break;
                case RL.chips.OneHundred:
                    location = "ms-appx:///Assets/Fiche100.png";
                    break;
                case RL.chips.TwoHundred:
                    location = "ms-appx:///Assets/Fiche200.png";
                    break;
                case RL.chips.FiveHundred:
                    location = "ms-appx:///Assets/Fiche500.png";
                    break;
            }
            BitmapImage chipSource = new BitmapImage(new Uri(location));
            currentChip.Source = chipSource;
        }

        public async void UpdateResult(string message)
        {
            result.Text = message;
            await Task.Delay(2000);
            result.Text = "";
        }

        private async void UpdateWinningNumber()
        {
            await Task.Delay(5000);

            int winningNumberValue = roulette.wheel.winningNumber.number;
            string winningColor = roulette.wheel.winningNumber.color;

            switch (winningColor)
            {
                case "Black":
                    winningBlock.Background = new SolidColorBrush(Colors.Black);
                    break;

                case "Red":
                    winningBlock.Background = new SolidColorBrush(Colors.Red);
                    break;

                case "Green":
                    winningBlock.Background = new SolidColorBrush(Colors.Green);
                    break;
            }

            winningNumber.Text = winningNumberValue.ToString();

            roulette.Payout();
            WinningMessage();
            ResetTable();
        }

        public async void WinningMessage()
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

        public async void ResetTable()
        {
            await Task.Delay(5000);

            balance.Text = roulette.player.balance.ToString();
            totalBet.Text = "Total bet: 0";

            betList.Items.Clear();

            roulette.SetIsPlaying(false);
            roulette.player.ClearBet();

            totalBetValue = 0;
            roulette.SetTotalMoneyWon(0);

            btnSpin.IsEnabled = true;
            btnResetStake.IsEnabled = true;
        }

        public async void SpinMessage()
        {
            result.Text = "Spinning.";
            await Task.Delay(1000);

            result.Text = "Spinning..";
            await Task.Delay(1000);

            result.Text = "Spinning...";
            await Task.Delay(1000);

            result.Text = "Spinning....";
            await Task.Delay(1000);

            result.Text = "Spinning.....";
            await Task.Delay(1000);
        }

        private bool GetIsPlaying()
        {
            if (roulette.isPlaying != true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int GetBets()
        {
            return roulette.player.bet.Count;
        }

        private void SetIsPlaying(bool value)
        {
            roulette.SetIsPlaying(value);
        }
    }
}