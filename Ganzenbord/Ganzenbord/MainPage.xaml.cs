using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Ganzenbord
{
    sealed partial class MainPage : Page
    {
        List<Image> vakimages = new List<Image> { };
        public Ganzenbord ganzenbord { get; private set; }
        public string Spelernaam1 { get; private set; }
        public string Spelernaam2 { get; private set; }
        public string Spelernaam3 { get; private set; }
        public string Spelernaam4 { get; private set; }
        public string Spelernaam5 { get; private set; }
        public int Well_PrisonTurn { get; private set; }
        public int getal { get; private set; }

        public MainPage(int aantalSpelers, string Spelernaam2, string Spelernaam3, string Spelernaam4, string Spelernaam5)
        {

            this.InitializeComponent();

            ganzenbord = new Ganzenbord(aantalSpelers, vakimages);

            this.Spelernaam2 = Spelernaam2;
            this.Spelernaam3 = Spelernaam3;
            this.Spelernaam4 = Spelernaam4;
            this.Spelernaam5 = Spelernaam5;

            Speler2.Text = Spelernaam2;
            Speler3.Text = Spelernaam3;
            Speler4.Text = Spelernaam4;
            Speler5.Text = Spelernaam5;

            VulImages();
        }

        private void VulImages()
        {
            vakimages.Add(vak0);
            vakimages.Add(vak1);
            vakimages.Add(vak2);
            vakimages.Add(vak3);
            vakimages.Add(vak4);
            vakimages.Add(vak5);
            vakimages.Add(vak6);
            vakimages.Add(vak7);
            vakimages.Add(vak8);
            vakimages.Add(vak9);
            vakimages.Add(vak10);

            vakimages.Add(vak11);
            vakimages.Add(vak12);
            vakimages.Add(vak13);
            vakimages.Add(vak14);
            vakimages.Add(vak15);
            vakimages.Add(vak16);
            vakimages.Add(vak17);
            vakimages.Add(vak18);
            vakimages.Add(vak19);
            vakimages.Add(vak20);

            vakimages.Add(vak21);
            vakimages.Add(vak22);
            vakimages.Add(vak23);
            vakimages.Add(vak24);
            vakimages.Add(vak25);
            vakimages.Add(vak26);
            vakimages.Add(vak27);
            vakimages.Add(vak28);
            vakimages.Add(vak29);
            vakimages.Add(vak30);

            vakimages.Add(vak31);
            vakimages.Add(vak32);
            vakimages.Add(vak33);
            vakimages.Add(vak34);
            vakimages.Add(vak35);
            vakimages.Add(vak36);
            vakimages.Add(vak37);
            vakimages.Add(vak38);
            vakimages.Add(vak39);
            vakimages.Add(vak40);

            vakimages.Add(vak41);
            vakimages.Add(vak42);
            vakimages.Add(vak43);
            vakimages.Add(vak44);
            vakimages.Add(vak45);
            vakimages.Add(vak46);
            vakimages.Add(vak47);
            vakimages.Add(vak48);
            vakimages.Add(vak49);
            vakimages.Add(vak50);

            vakimages.Add(vak51);
            vakimages.Add(vak52);
            vakimages.Add(vak53);
            vakimages.Add(vak54);
            vakimages.Add(vak55);
            vakimages.Add(vak56);
            vakimages.Add(vak57);
            vakimages.Add(vak58);
            vakimages.Add(vak59);
            vakimages.Add(vak60);

            vakimages.Add(vak61);
            vakimages.Add(vak62);
            vakimages.Add(vak63);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Eventvak.Text = "Dobbellen...";
            //Task.Delay(2000).Wait();

            ganzenbord.PlayerMove();

                switch (ganzenbord.Field)
                {
                default:
                    Eventvak.Text = "";
                    break;
                case "brug":
                    Eventvak.Text = "Je bent op de brug gekomen! Je bent naar veld 12 verplaatst";
                    break;
                case "herberg":
                    Eventvak.Text = "Je bent in de herberg beland. Je moet een beurt overslaan";
                     break;
                case "put":
                    Eventvak.Text = "Je bent in de put beland. Je zit hier vast todat iemand je eruit haalt of je 3 beurten hebt overgeslagen";
                    break;
                case "doolhof":
                    Eventvak.Text = "Je bent vastgelopen in het doolhof. Je gaat terug naar veld 37.";
                    break;
                case "gevangenis":
                    Eventvak.Text = "Je bent in de gevangenis beland. Je zit hier vast todat iemand je eruit haalt. of je 3 beurten hebt overgeslagen";
                    break;
                case "dood":
                    Eventvak.Text = "Je bent in een val beland. Je moet helaas opnieuw beginnen.";
                    break;
                case "einde":
                    Eventvak.Text = "Je hebt gewonnen! Ga terug of restart het spel!";
                    btnDobbel.IsEnabled = false;
                    btnRestart.Visibility = Visibility.Visible;
                    break;
                case "dubbeleworp":
                    Eventvak.Text = "Je bent op een speciaal veld beland. Je worp wordt verdubbeld.";
                    break;
                case "TweeOpÉénVak":
                    Eventvak.Text = "Je bent op een veld beland waar al iemand op stond. Je bent terug verplaatst.";
                    break;
                case "NineOnFirstTurn":
                    Eventvak.Text = "Je bent op veld 9 gekomen in de eerste beurt. Je bent naar veld 26 verplaatst";
                    break;
            }

            dobbel.Text = "aantal ogen gegooid:" + " " + Convert.ToString(ganzenbord.dice.PipCount) + "\n Je staat nu op vak:" + " " + ganzenbord.players[ganzenbord.playerTurn].location;

            if (ganzenbord.winGame)
            {
                Eventvak.Text = SelecteerSpeler(ganzenbord.playerTurn) + " " + "heeft gewonnen! Ga terug of restart het spel!";
                btnDobbel.IsEnabled = false;
                btnRestart.Visibility = Visibility.Visible;
            }

            ganzenbord.NextPlayer();


            if (ganzenbord.CheckSkippingTurn())
            {
                ganzenbord.ChangeSkipTurn();
                ganzenbord.NextPlayer();
            } 
            else if (ganzenbord.CheckStuck())
            {
                if (ganzenbord.getal == 2)
                {
                    ganzenbord.ChangeStuck();
                    ganzenbord.getal = 0;
                }
                else
                {
                    ganzenbord.getal++;
                    ganzenbord.NextPlayer();
                }
            }

            SelecteerSpeler(ganzenbord.playerTurn);

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            ganzenbord.Restart();
            btnDobbel.IsEnabled = true;

            AantalSpelers aantalSpelers = new AantalSpelers();
            Content = aantalSpelers;
        }

        private string SelecteerSpeler(int player)
        {
            switch (player)
            {
                default:
                    return "";
                case 0:
                    speler.Text = "Frankie speelt" + " " + "(paars)";
                    return "Frankie";
                case 1:
                    speler.Text = Spelernaam2 + " " + "speelt" + " " + "(blauw)";
                    return Spelernaam2;
                case 2:
                    speler.Text = Spelernaam3 + " " + "speelt" + " " + "(groen)";
                    return Spelernaam3;
                case 3:
                    speler.Text = Spelernaam4 + " " + "speelt" + " " + "(rood)";
                    return Spelernaam4;
                case 4:
                    speler.Text = Spelernaam5 + " " + "speelt" + " " + "(zwart)";
                    return Spelernaam5;
            }
        }
    }
}
