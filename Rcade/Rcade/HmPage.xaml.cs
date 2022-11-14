using System;
using System.Collections.Generic;
using System.Data;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Rcade
{
    public sealed partial class HmPage : Page
    {
        private Databasehandler_hm dbh_Hm { get; set; } = new Databasehandler_hm(false);
        private User user { get; set; }
        private Hm host { get; set; } = new Hm();
        private List<string> numbers { get; set; } = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        private List<Button> buttons { get; set; } = new List<Button> { };
        private int previousTurn { get; set; } = 0;

        public HmPage()
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

        private void alphabet_Click(object sender, RoutedEventArgs e)
        {
            btnBack.Visibility = Visibility.Collapsed;

            char letter = Convert.ToChar(((Control)sender).Name);

            Button button = sender as Button;
            button.IsEnabled = false;

            host.CheckLetter(letter);

            UpdateDisplay();

            if (host.WinCheck() || host.EndGameCheck())
            {
                EndGame();
            }
        }

        private void btnNewgame_Click(object sender, RoutedEventArgs e)
        {
            btnBack.Visibility = Visibility.Collapsed;
            btnNewgame.Visibility = Visibility.Collapsed;

            buttons.ForEach(x => x.IsEnabled = true);
            Start();
        }

        public void Setup()
        {
            AddButtons();
            Start();
        }

        private void AddButtons()
        {
            buttons.Add(A);
            buttons.Add(B);
            buttons.Add(C);
            buttons.Add(D);
            buttons.Add(E);
            buttons.Add(F);
            buttons.Add(G);
            buttons.Add(H);
            buttons.Add(I);
            buttons.Add(J);
            buttons.Add(K);
            buttons.Add(L);
            buttons.Add(M);
            buttons.Add(N);
            buttons.Add(O);
            buttons.Add(P);
            buttons.Add(Q);
            buttons.Add(R);
            buttons.Add(S);
            buttons.Add(T);
            buttons.Add(U);
            buttons.Add(V);
            buttons.Add(W);
            buttons.Add(X);
            buttons.Add(Y);
            buttons.Add(Z);
        }

        private void Start()
        {
            if (dbh_Hm.TestConnection() == false)
            {
                MainPage main = new MainPage();
                Content = main;
            }
            else
            {
                dbh_Hm.GetUser(user.id);

                int totalScore = 0;
                int totalMistakes = 0;

                foreach (DataRow row in dbh_Hm.table.Rows)
                {
                    totalScore = Convert.ToInt32(row["totaal_punten"]);
                    totalMistakes = Convert.ToInt32(row["totaal_fouten"]);
                }

                host.player.SetPlayerTotalStats(totalScore, totalMistakes);

                previousTurn = 0;
                btnNewgame.Visibility = Visibility.Collapsed;

                host.Start();
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            TbLetterDisplay.Text = new String(host.displayedLetters);
            TbGuessedLetters.Text = new String(host.guessedLetters.ToArray());
            TbResult.Text = host.result;

            UpdateImage(HangmanImage);

            TbScore.Text = Convert.ToString(host.player.score);
        }

        private void UpdateImage(Image image)
        {
            if (previousTurn != host.player.turn || host.player.turn == 0)
            {
                string Location = "ms-appx:///Assets/Images/hm/" + numbers[host.player.turn] + ".jpg";

                BitmapImage ImageSource = new BitmapImage(new Uri(Location));
                image.Source = ImageSource;

                previousTurn = host.player.turn;
            }
        }


        private void EndGame()
        {
            btnNewgame.Visibility = Visibility.Visible;
            btnBack.Visibility = Visibility.Visible;

            TbResult.Text = host.result;
            TbLetterDisplay.Text = new String(host.correctLetters);

            host.player.SetScore();
            SetUserStats();
            TbScore.Text = Convert.ToString(host.player.score);

            host.player.ClearGameScore();
            host.player.ClearTurn();
            host.Clear();

            buttons.ForEach(x => x.IsEnabled = false);
        }

        internal void SetUser(User user)
        {
            this.user = user;
        }

        private void SetUserStats()
        {
            if (dbh_Hm.TestConnection() == false)
            {
                MainPage main = new MainPage();
                Content = main;
            }
            else
            {
                host.player.SetPlayerStats(host.player.gameScore, host.player.turn);
                dbh_Hm.SetUser(user.id, host.player.totalScore, host.player.totalMistakes, host.player.lastPlayed);
            }
        }
    }
}