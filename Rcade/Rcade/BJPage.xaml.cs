using System;
using System.Data;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Rcade
{
    public sealed partial class BJPage : Page
    {
        private Databasehandler_bj dbh_BJ { get; set; } = new Databasehandler_bj(false);
        private User user { get; set; }
        private BJ gameHost { get; set; } = new BJ();
        private Image[] playerImages { get; set; }
        private Image[] dealerImages { get; set; }
        public double stake { get; private set; }

        public BJPage()
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

        private void btnResetStake_Click(object sender, RoutedEventArgs e)
        {
            gameHost.SetStakeReset();
            ClearText();
            UpdateText();

            btnResetStake.IsEnabled = false;
        }

        private void btnFifty_Click(object sender, RoutedEventArgs e)
        {
            if (gameHost.CheckStake(Convert.ToDouble(BJ.fiches.Fifty)))
            {
                stake = gameHost.actualStake;
                playerStake.Text = Convert.ToString(stake);

                btnResetStake.IsEnabled = true;
            }
            UpdateText();
        }

        private void btnHundred_Click(object sender, RoutedEventArgs e)
        {
            if (gameHost.CheckStake(Convert.ToDouble(BJ.fiches.Hundered)))
            {
                stake = gameHost.actualStake;
                playerStake.Text = Convert.ToString(stake);

                btnResetStake.IsEnabled = true;
            }
            UpdateText();
        }

        private void btnTwohundred_Click(object sender, RoutedEventArgs e)
        {
            if (gameHost.CheckStake(Convert.ToDouble(BJ.fiches.Twohundered)))
            {
                stake = gameHost.actualStake;
                playerStake.Text = Convert.ToString(stake);

                btnResetStake.IsEnabled = true;
            }
            UpdateText();
        }

        private void btnFivehundred_Click(object sender, RoutedEventArgs e)
        {
            if (gameHost.CheckStake(Convert.ToDouble(BJ.fiches.Fivehunderd)))
            {
                stake = gameHost.actualStake;
                playerStake.Text = Convert.ToString(stake);

                btnResetStake.IsEnabled = true;
            }
            UpdateText();
        }

        private void btnOne_Click(object sender, RoutedEventArgs e)
        {
            gameHost.player.playerCards[gameHost.player.aceLocation].SetAceValue(1);
            Ace();

            if (!gameHost.playedTurn)
            {
                gameHost.SecondTurn(gameHost.actualStake);
                CheckAce(gameHost.player.CheckForAce());
                gameHost.SetPlayedTurn(true);

            }
            gameHost.CheckBlackjack(stake);
            CheckGame();
            UpdateText();
        }

        private void btnEleven_Click(object sender, RoutedEventArgs e)
        {
            gameHost.player.playerCards[gameHost.player.aceLocation].SetAceValue(11);
            Ace();

            if (!gameHost.playedTurn)
            {
                gameHost.SecondTurn(gameHost.actualStake);
                CheckAce(gameHost.player.CheckForAce());
                gameHost.SetPlayedTurn(true);
            }
            gameHost.CheckBlackjack(stake);
            CheckGame();
            UpdateText();
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            gameHost.player.SetCard(gameHost.stackOfCards);

            CheckAce(gameHost.player.CheckForAce());

            if (gameHost.player.CheckForAce())
            {
                CheckAce(true);
            }
            else
            {
                gameHost.CheckForPlayerBust();
            }

            gameHost.CheckBlackjack(stake);

            CheckGame();
            UpdateText();


            gameHost.SetPlayedTurn(true);

            btnInsurance.Visibility = Visibility.Collapsed;
            btnDoubleDown.Visibility = Visibility.Collapsed;
        }

        private void btnStand_Click(object sender, RoutedEventArgs e)
        {
            Stand();
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            gameHost.Clear();
            ClearText();
            UpdateText();
            EnableStake();

            btnNewGame.Visibility = Visibility.Collapsed;

            btnOne.IsEnabled = false;
            btnEleven.IsEnabled = false;
        }

        private void btnDoubleDown_Click(object sender, RoutedEventArgs e)
        {
            gameHost.actualStake *= 2;
          //  stake *= 2;
            playerStake.Text = Convert.ToString(gameHost.actualStake);
            gameHost.player.DecreaseBalance(stake);




          // HIT
            gameHost.player.SetCard(gameHost.stackOfCards);

            CheckAce(gameHost.player.CheckForAce());

            if (gameHost.player.CheckForAce())
            {
                CheckAce(true);
            }
            else
            {
                gameHost.CheckForPlayerBust();
            }

            gameHost.CheckBlackjack(gameHost.actualStake);

            CheckGame();
            UpdateText();

            btnInsurance.Visibility = Visibility.Collapsed;
            btnDoubleDown.Visibility = Visibility.Collapsed;


            btnDoubleDown.IsEnabled = false;
        }

        private void btnInsurance_Click(object sender, RoutedEventArgs e)
        {
            gameHost.SetInsurance(true);
            gameHost.player.DecreaseBalance(gameHost.actualStake / 2);
            btnInsurance.Visibility = Visibility.Collapsed;

            if (!gameHost.playedTurn)
            {
                gameHost.SecondTurn(gameHost.actualStake);
                CheckAce(gameHost.player.CheckForAce());
                gameHost.SetPlayedTurn(true);
            }

            CheckGame();

            UpdateText();
        }

        private void btnConfirmStake_Click(object sender, RoutedEventArgs e)
        {
            if (gameHost.CheckStake(0))
            {
                stake = Convert.ToDouble(playerStake.Text);
                gameHost.player.DecreaseBalance(stake);

                StartNewGame();

                btnConfirmStake.IsEnabled = false;
                btnConfirmStake.Visibility = Visibility.Collapsed;

                btnBack.IsEnabled = false;
                btnBack.Visibility = Visibility.Collapsed;

                btnResetStake.IsEnabled = false;

                btnHit.IsEnabled = true;
                btnStand.IsEnabled = true;

            //    btnHit.Visibility = Visibility.Visible;
            //    btnStand.Visibility = Visibility.Visible;

                result.Text = "";
                gameHost.SetResult("");

                btnEleven.IsEnabled = true;
                btnOne.IsEnabled = true;

                btnFifty.IsEnabled = false;
                btnHundred.IsEnabled = false;
                btnTwohundred.IsEnabled = false;
                btnFivehundred.IsEnabled = false;
            }
            else
            {
                gameHost.SetStakeReset();
            }
            UpdateText();
        }

        internal void SetUp()
        {
            playerImages = new Image[] { playerCard1, playerCard2, playerCard3, playerCard4, playerCard5, playerCard6, playerCard7, playerCard8 };
            dealerImages = new Image[] { dealerCard1, dealerCard2, dealerCard3, dealerCard4, dealerCard5, dealerCard6, dealerCard7, dealerCard8 };

            gameHost.player.SetPlayerName(user.userName);

            playerName.Text = gameHost.player.namePlayer;

            ClearText();
            EnableStake();
            UpdateText();

            if (gameHost.player.balance <= 0)
            {
                balance.Margin = new Thickness(1, 0, 0, 4);
            }
        }

        private void ClearText()
        {
            balance.Text = "";
            result.Text = "";

            gameHost.SetResult("");

            btnHit.Visibility = Visibility.Collapsed;
            btnStand.Visibility = Visibility.Collapsed;

            btnNewGame.Visibility = Visibility.Collapsed;

            btnEleven.Visibility = Visibility.Collapsed;
            btnOne.Visibility = Visibility.Collapsed;

            btnInsurance.Visibility = Visibility.Collapsed;
            btnDoubleDown.Visibility = Visibility.Collapsed;

            gameHost.SetInsurance(false);

            SetImageReset(playerImages);
            SetImageReset(dealerImages);
        }

        private void UpdateText()
        {
            balance.Text = Convert.ToString(gameHost.player.balance);

            if (gameHost.player.balance <= 0)
            {
                balance.Margin = new Thickness(1, 0, 0, 4);
            }


            bool Ace = false;
            for (int i = 0; i < gameHost.player.playerCards.Count; i++)
            {
               if (gameHost.player.playerCards[i].value == 14)
                {
                    Ace = true;
                }
            }


            if (gameHost.player.GetTotalPoints() != 0 && !Ace)
            {
                TbPoints.Text = Convert.ToString(gameHost.player.GetTotalPoints());
            }
            else
            {
                TbPoints.Text = "";
            }
            
            
            
            result.Text = gameHost.result;
            playerStake.Text = Convert.ToString(gameHost.actualStake);
            sessionBalance.Text = Convert.ToString(gameHost.player.sessionBalance);

            SetPlayerImage(gameHost.player, playerImages);

            if (gameHost.gameOver)
            {
                SetPlayerImage(gameHost.dealer, dealerImages);
            }
        }

        private void EnableStake()
        {
            btnHit.IsEnabled = false;
            btnStand.IsEnabled = false;

            playerStake.Text = Convert.ToString(gameHost.actualStake);

            btnConfirmStake.Visibility = Visibility.Visible;
            btnConfirmStake.IsEnabled = true;

            btnDoubleDown.IsEnabled = true;
            btnInsurance.IsEnabled = true;
           
            btnResetStake.IsEnabled = false;
            btnFifty.IsEnabled = true;
            btnHundred.IsEnabled = true;
            btnTwohundred.IsEnabled = true;
            btnFivehundred.IsEnabled = true;
        }

        private void StartNewGame()
        {
            ClearText();

            gameHost.FirstTurn(stake);
          

            CheckAce(gameHost.player.hasAce);

            if (gameHost.hasInsurance)
            {
                btnInsurance.Visibility = Visibility.Visible;
            }

            if (gameHost.player.playerCards.Count == 2)
            {
                DoubleDown();
            }

            UpdateText();
            EnableStake();

            SetPlayerImage(gameHost.player, playerImages);
            SetDealerImage(gameHost.dealer, dealerImages);
        }

        public void CheckGame()
        {
            if (gameHost.gameOver)
            {
                SetUserStats(Convert.ToInt32(gameHost.player.balance));

                btnHit.Visibility = Visibility.Collapsed;
                btnStand.Visibility = Visibility.Collapsed;

                btnNewGame.Visibility = Visibility.Visible;

                btnBack.Visibility = Visibility.Visible;
                btnBack.IsEnabled = true;

                btnEleven.Visibility = Visibility.Collapsed;
                btnOne.Visibility = Visibility.Collapsed;

                btnDoubleDown.Visibility = Visibility.Collapsed;
                btnInsurance.Visibility = Visibility.Collapsed;

                SetPlayerImage(gameHost.dealer, dealerImages);
            }
        }

        private void Stand()
        {
            gameHost.Stand(gameHost.actualStake, gameHost.stackOfCards);

            CheckGame();
            UpdateText();
            SetPlayerImage(gameHost.dealer, dealerImages);
        }

        private void CheckAce(bool ace)
        {
            if (ace)
            {
                if (gameHost.player.GetTotalPoints() - 14 > 21)
                {
                    Stand();
                }
                else
                {
                    btnEleven.Visibility = Visibility.Visible;
                    btnOne.Visibility = Visibility.Visible;

                    btnEleven.IsEnabled = true;
                    btnOne.IsEnabled = true;

                    btnHit.Visibility = Visibility.Collapsed;
                    btnStand.Visibility = Visibility.Collapsed;
                }
                UpdateText();
                gameHost.player.SetHasAce(false);
            }
            else
            {
                btnHit.Visibility = Visibility.Visible;
                btnStand.Visibility = Visibility.Visible;
            }
        }

        private void Ace()
        {
            btnEleven.Visibility = Visibility.Collapsed;
            btnOne.Visibility = Visibility.Collapsed;
            btnHit.Visibility = Visibility.Visible;
            btnStand.Visibility = Visibility.Visible;
            btnInsurance.Visibility = Visibility.Collapsed;
            btnDoubleDown.Visibility = Visibility.Collapsed;

            gameHost.CheckForPlayerBust();
            CheckGame();
            UpdateText();
        }

        private void DoubleDown()
        {
            if (gameHost.player.CheckDoubleDown() && stake * 2 < gameHost.player.balance)
            {
                btnDoubleDown.Visibility = Visibility.Visible;
                gameHost.SetResult("");
            }
        }

        public int GetUserStats()
        {
            if (dbh_BJ.TestConnection() == false)
            {
                MainPage main = new MainPage();
                Content = main;

                return 0;
            }
            else
            {
                dbh_BJ.GetUser(user.id);

                int bjSaldo = 0;

                foreach (DataRow row in dbh_BJ.table.Rows)
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
            if (dbh_BJ.TestConnection() == false)
            {
                MainPage main = new MainPage();
                Content = main;
            }
            else
            {
                gameHost.player.SetStats(GetUserStats());
                balance.Margin = new Thickness(10, -5, 7, 0);

                UpdateText();
            }
        }

        private void SetUserStats(int balance)
        {
            if (dbh_BJ.TestConnection() == false)
            {
                MainPage main = new MainPage();
                Content = main;
            }
            else
            {
                dbh_BJ.SetUser(user.id, balance, gameHost.player.lastPlayed);
            }
        }

        private void SetPlayerImage(BJ_Player speler, Image[] ImagesArray)
        {
            for (int i = 0; i < speler.playerCards.Count; i++)
            {
                string locatie = "ms-appx:///Assets/Images/bj/cards/" + speler.GetCard(i);

                BitmapImage ImageSource = new BitmapImage(new Uri(locatie));
                ImagesArray[i].Source = ImageSource;
            }
        }

        private void SetDealerImage(BJ_Player dealer, Image[] ImagesArray)
        {
            string locatie = "ms-appx:///Assets/Images/bj/cards/" + dealer.GetCard(0);

            BitmapImage ImageSource1 = new BitmapImage(new Uri(locatie));
            ImagesArray[0].Source = ImageSource1;

            BitmapImage ImageSource2 = new BitmapImage(new Uri("ms-appx:///Assets/Images/bj/cards/back.png"));
            ImagesArray[1].Source = ImageSource2;
        }

        private void SetImageReset(Image[] ImagesArray)
        {
            for (int i = 0; i < ImagesArray.Length; i++)
            {
                ImagesArray[i].Source = null;
            }
        }
    }
}