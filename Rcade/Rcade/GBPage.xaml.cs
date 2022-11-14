using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Rcade
{
    public sealed partial class GBPage : Page
    {
        private List<Image> boxImages { get; set; } = new List<Image> { };
        private GB gb { get; set; }
        private User user { get; set; }
        public string firstPlayerName { get; private set; }
        public string secondPlayerName { get; private set; }
        public string thirdPlayerName { get; private set; }
        public string fourthPlayerName { get; private set; }
        public string fivePlayerName { get; private set; }

        public GBPage(int amountOfPlayers, string firstPlayerName, string secondPlayerName, string thirdPlayerName, string fourthPlayerName, string fivePlayerName)
        {
            InitializeComponent();

            ApplicationView.GetForCurrentView().SetPreferredMinSize(
            new Size(
                1000,
                1000
                ));

            gb = new GB(amountOfPlayers, boxImages);

            this.firstPlayerName = firstPlayerName;
            this.secondPlayerName = secondPlayerName;
            this.thirdPlayerName = thirdPlayerName;
            this.fourthPlayerName = fourthPlayerName;
            this.fivePlayerName = fivePlayerName;

            Speler1.Text = firstPlayerName;
            Speler2.Text = secondPlayerName;
            Speler3.Text = thirdPlayerName;
            Speler4.Text = fourthPlayerName;
            Speler5.Text = fivePlayerName;

            speler.Text = firstPlayerName + " " + "is playing (blue)";

            AddImages();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HubPage hub = new HubPage();
            hub.SetUser(user);

            Content = hub;
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            gb.Restart();
            btnDice.IsEnabled = true;

            GBPlayersPage gbp = new GBPlayersPage();
            gbp.SetUser(user);

            Content = gbp;
        }

        private async void btnDice_Click(object sender, RoutedEventArgs e)
        {
            SpinImage();
            Eventvak.Text = "Throwing...";
            btnDice.IsEnabled = false;
            await Task.Delay(2000);
            btnDice.IsEnabled = true;

            gb.PlayerMove();

            switch (gb.field)
            {
                default:
                    Eventvak.Text = "";
                    break;
                case "bridge":
                    Eventvak.Text = SelectPlayer(gb.playerTurn) + ", landed on a bridge! You have been moved to field 12.";
                    break;
                case "inn":
                    Eventvak.Text = SelectPlayer(gb.playerTurn) + ", you're staying at an inn for tonight. You have to skip your next turn.";
                    break;
                case "well":
                    Eventvak.Text = SelectPlayer(gb.playerTurn) + ", you're stuck in a well. You have to stay here until someone gets you out or until you climb out in 3 turns.";
                    break;
                case "maze":
                    Eventvak.Text = SelectPlayer(gb.playerTurn) + ", you were lost in a maze. You are moved back to field 37.";
                    break;
                case "jail":
                    Eventvak.Text = SelectPlayer(gb.playerTurn) + ", you're in prison! You have to stay here until someone bails you out or until you escape in 3 turns.";
                    break;
                case "dead":
                    Eventvak.Text = SelectPlayer(gb.playerTurn) + ", you've been caught in a trap. You'll have to start over from the beginning.";
                    break;
                case "dubbleThrow":
                    Eventvak.Text = SelectPlayer(gb.playerTurn) + ", you've landed on a special field. Your dice throw is doubled.";
                    break;
                case "twoOnOneBox":
                    Eventvak.Text = SelectPlayer(gb.playerTurn) + ", you landed on a field someone was already on. You've been moved back to your old position.";
                    break;
                case "nineOnFirstTurn":
                    Eventvak.Text = SelectPlayer(gb.playerTurn) + ", lucky you! You landed on field 9 in your first turn! You've been moved to field 26.";
                    break;
            }

            dobbel.Text = "Number of pips thrown:" + " " + Convert.ToString(gb.dice.pipCount) + "\n" + SelectPlayer(gb.playerTurn) + ", You are now on field:" + " " + gb.players[gb.playerTurn].location;

            if (gb.winGame)
            {
                Eventvak.Text = SelectPlayer(gb.playerTurn) + " " + "won the game! You can either play another game or go back to the home menu.";
                btnDice.IsEnabled = false;
                btnRestart.Visibility = Visibility.Visible;
            }

            gb.NextPlayer();


            if (gb.CheckSkippingTurn())
            {
                gb.ChangeSkipTurn();
                gb.NextPlayer();
            }
            else if (gb.CheckStuck())
            {
                if (gb.number == 2)
                {
                    gb.ChangeStuck();
                    gb.number = 0;
                }
                else
                {
                    gb.number++;
                    gb.NextPlayer();
                }
            }

            SelectPlayer(gb.playerTurn);
        }
        private async void SpinImage()
        {
            RotateTransform m_transform = new RotateTransform();
            Dice.RenderTransform = m_transform;

            while (m_transform.Angle != 360)
            {
                m_transform.Angle = m_transform.Angle + 36;
                await Task.Delay(100);
            }
        }

        private void AddImages()
        {
            boxImages.Add(vak0);
            boxImages.Add(vak1);
            boxImages.Add(vak2);
            boxImages.Add(vak3);
            boxImages.Add(vak4);
            boxImages.Add(vak5);
            boxImages.Add(vak6);
            boxImages.Add(vak7);
            boxImages.Add(vak8);
            boxImages.Add(vak9);
            boxImages.Add(vak10);

            boxImages.Add(vak11);
            boxImages.Add(vak12);
            boxImages.Add(vak13);
            boxImages.Add(vak14);
            boxImages.Add(vak15);
            boxImages.Add(vak16);
            boxImages.Add(vak17);
            boxImages.Add(vak18);
            boxImages.Add(vak19);
            boxImages.Add(vak20);

            boxImages.Add(vak21);
            boxImages.Add(vak22);
            boxImages.Add(vak23);
            boxImages.Add(vak24);
            boxImages.Add(vak25);
            boxImages.Add(vak26);
            boxImages.Add(vak27);
            boxImages.Add(vak28);
            boxImages.Add(vak29);
            boxImages.Add(vak30);

            boxImages.Add(vak31);
            boxImages.Add(vak32);
            boxImages.Add(vak33);
            boxImages.Add(vak34);
            boxImages.Add(vak35);
            boxImages.Add(vak36);
            boxImages.Add(vak37);
            boxImages.Add(vak38);
            boxImages.Add(vak39);
            boxImages.Add(vak40);

            boxImages.Add(vak41);
            boxImages.Add(vak42);
            boxImages.Add(vak43);
            boxImages.Add(vak44);
            boxImages.Add(vak45);
            boxImages.Add(vak46);
            boxImages.Add(vak47);
            boxImages.Add(vak48);
            boxImages.Add(vak49);
            boxImages.Add(vak50);

            boxImages.Add(vak51);
            boxImages.Add(vak52);
            boxImages.Add(vak53);
            boxImages.Add(vak54);
            boxImages.Add(vak55);
            boxImages.Add(vak56);
            boxImages.Add(vak57);
            boxImages.Add(vak58);
            boxImages.Add(vak59);
            boxImages.Add(vak60);

            boxImages.Add(vak61);
            boxImages.Add(vak62);
            boxImages.Add(vak63);
        }

        private string SelectPlayer(int player)
        {
            switch (player)
            {
                default:
                    return "";
                case 0:
                    speler.Text = firstPlayerName + " " + "is playing (blue)";
                    return firstPlayerName;
                case 1:
                    speler.Text = secondPlayerName + " " + "is playing" + " " + "(yellow)";
                    return secondPlayerName;
                case 2:
                    speler.Text = thirdPlayerName + " " + "is playing" + " " + "(green)";
                    return thirdPlayerName;
                case 3:
                    speler.Text = fourthPlayerName + " " + "is playing" + " " + "(red)";
                    return fourthPlayerName;
                case 4:
                    speler.Text = fivePlayerName + " " + "is playing" + " " + "(white)";
                    return fivePlayerName;
            }
        }

        internal void SetUser(User user)
        {
            this.user = user;
        }
    }
}