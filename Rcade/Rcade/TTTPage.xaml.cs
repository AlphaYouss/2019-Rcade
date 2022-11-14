using System;
using System.Data;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Rcade
{
    public sealed partial class TTTPage : Page
    {
        private Databasehandler_ttt dbh_TTT { get; set; } = new Databasehandler_ttt(false);
        private User user { get; set; }
        private TTT ttt { get; set; }
        private TTT_Field field { get; set; }
        private TTT_Player player { get; set; }
        private TTT_NewAI ai { get; set; }
        private BitmapImage defaultImage { get; set; } = new BitmapImage(new Uri("ms-appx:///Assets/Images/ttt/transparent.png"));
        private string[] tttStats { get; set; } = new string[3];

        public TTTPage()
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

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            btnBack.Visibility = Visibility.Collapsed;
            btnRestart.Visibility = Visibility.Collapsed;

            for (int i = 1; i < 10; i++)
            {
                SetField(i, defaultImage, true, true);
            }

            field.ClearField();
            ai.firstMoveDone = false;

            vak1.Click += vak_Click;
            vak2.Click += vak_Click;
            vak3.Click += vak_Click;
            vak4.Click += vak_Click;
            vak5.Click += vak_Click;
            vak6.Click += vak_Click;
            vak7.Click += vak_Click;
            vak8.Click += vak_Click;
            vak9.Click += vak_Click;

            txtNameAI.Foreground = new SolidColorBrush(Colors.White);
            txtNamePlayer.Foreground = new SolidColorBrush(Colors.White);
        }

        private void vak_Click(object sender, RoutedEventArgs e)
        {
            string ctrlName = ((Control)sender).Name;
            string[] button = ctrlName.Split("vak");

            int clickedBox = Convert.ToInt32(button[1]);

            btnBack.Visibility = Visibility.Collapsed;
            btnRestart.Visibility = Visibility.Collapsed;

            player.TakeTurn(clickedBox);
            SetField(clickedBox, player.imagePlayer, false, true);

            int remainingBoxes = ttt.CheckField();

            if (ttt.CheckWin())
            {
                // Player wins.

                player.SetScore(3);
                ai.SetScore(-1);

                SetUserStats(0);

                txtNamePlayer.Foreground = new SolidColorBrush(Colors.Green);
                txtNameAI.Foreground = new SolidColorBrush(Colors.Red);

                NextGame();
            }
            else if (remainingBoxes != 0)
            {
                ai.NewMove();
                SetField(ai.moveAI, ai.imageAi, false, true);

                if (ttt.CheckWin())
                {
                    // AI wins.   

                    player.SetScore(-1);
                    ai.SetScore(3);
                    SetUserStats(1);

                    txtNamePlayer.Foreground = new SolidColorBrush(Colors.Red);
                    txtNameAI.Foreground = new SolidColorBrush(Colors.Green);

                    NextGame();
                }
            }
            else
            {
                // Draw.

                SetUserStats(2);

                txtNamePlayer.Foreground = new SolidColorBrush(Colors.Orange);
                txtNameAI.Foreground = new SolidColorBrush(Colors.Orange);

                NextGame();
            }
        }

        internal void SetUp()
        {
            field = new TTT_Field();
            field.GenerateField();

            player = new TTT_Player(field);
            ai = new TTT_NewAI(field);
            ttt = new TTT(field);

            player.SetPlayerName(user.userName);

            txtNameAI.Text = ai.nameAi;
            txtNamePlayer.Text = player.namePlayer;
        }

        private void NextGame()
        {
            btnBack.Visibility = Visibility.Visible;
            btnRestart.Visibility = Visibility.Visible;

            player.SetScore();
            ai.SetScore();

            txtScorePlayer.Text = player.scorePlayer.ToString();
            txtScoreAI.Text = ai.scoreAi.ToString();

            for (int i = 1; i < 10; i++)
            {
                SetField(i, defaultImage, false, false);
            }
        }

        private void SetField(int box, BitmapImage imageSource, bool enabled, bool reset)
        {
            switch (box)
            {
                default:
                    break;
                case 1:
                    if (reset == true)
                    {
                        image.Source = imageSource;
                    }

                    vak1.Click -= vak_Click;
                    break;
                case 2:
                    if (reset == true)
                    {
                        image2.Source = imageSource;
                    }

                    vak2.Click -= vak_Click;
                    break;
                case 3:
                    if (reset == true)
                    {
                        image3.Source = imageSource;
                    }

                    vak3.Click -= vak_Click;
                    break;
                case 4:
                    if (reset == true)
                    {
                        image4.Source = imageSource;
                    }

                    vak4.Click -= vak_Click;
                    break;
                case 5:
                    if (reset == true)
                    {
                        image5.Source = imageSource;
                    }

                    vak5.Click -= vak_Click;
                    break;
                case 6:
                    if (reset == true)
                    {
                        image6.Source = imageSource;
                    }

                    vak6.Click -= vak_Click;
                    break;
                case 7:
                    if (reset == true)
                    {
                        image7.Source = imageSource;
                    }

                    vak7.Click -= vak_Click;
                    break;
                case 8:
                    if (reset == true)
                    {
                        image8.Source = imageSource;
                    }

                    vak8.Click -= vak_Click;
                    break;
                case 9:
                    if (reset == true)
                    {
                        image9.Source = imageSource;
                    }

                    vak9.Click -= vak_Click;
                    break;
            }
        }

        internal void SetUser(User user)
        {
            this.user = user;
        }

        public void GetUserStats()
        {
            dbh_TTT.GetUser(user.id);

            foreach (DataRow row in dbh_TTT.table.Rows)
            {
                tttStats[0] = Convert.ToString(row["gewonnen_potjes"]);
                tttStats[1] = Convert.ToString(row["verloren_potjes"]);
                tttStats[2] = Convert.ToString(row["gelijkspel_potjes"]);
            }
        }

        internal void SetUserStats()
        {
            if (dbh_TTT.TestConnection() == false)
            {
                MainPage main = new MainPage();
                Content = main;
            }
            else
            {
                GetUserStats();
                player.SetStats(tttStats);
            }
        }

        private void SetUserStats(int status)
        {
            if (dbh_TTT.TestConnection() == false)
            {
                MainPage main = new MainPage();
                Content = main;
            }
            else
            {
                player.SetStats(status);
                dbh_TTT.SetUser(user.id, player.won, player.lost, player.draw, player.lastPlayed);
            }
        }
    }
}