using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;


namespace Boter__Kaas_en_Eieren
{

    public sealed partial class MainPage : Page
    {
        BKE bke;
        Veld veld;
        Speler speler1;
        AI ai1;

        public BitmapImage plaatjes { get; private set; } = new BitmapImage(new Uri("ms-appx:///"));
        
        public MainPage()
        {
            InitializeComponent();
            SetUp();
        }
       
        private void SetUp()
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Windows.Foundation.Size(1920, 1080));

            veld = new Veld();
            veld.VeldGenereren();

            speler1 = new Speler(veld);
            ai1 = new AI(veld);

            bke = new BKE(veld);

            txtnaamAI.Text = ai1.naamAI;
            txtnaamSpeler.Text = speler1.naamSpeler;

            txtnaamSpeler.Foreground = new SolidColorBrush(Colors.White);
            txtnaamAI.Foreground = new SolidColorBrush(Colors.White);
            txtscoreSpeler.Foreground = new SolidColorBrush(Colors.White);
            txtscoreAI.Foreground = new SolidColorBrush(Colors.White);

            txtnaamSpeler.FontFamily = new FontFamily("/Assets/Fonts/SFAlienEncounters-Italic.ttf#SF Alien Encounters");
            txtnaamAI.FontFamily = new FontFamily("/Assets/Fonts/SFAlienEncounters-Italic.ttf#SF Alien Encounters");
            txtscoreAI.FontFamily = new FontFamily("/Assets/Fonts/SFAlienEncounters-Italic.ttf#SF Alien Encounters");
            txtscoreSpeler.FontFamily = new FontFamily("/Assets/Fonts/SFAlienEncounters-Italic.ttf#SF Alien Encounters");
        }

        private async void vak_click(object sender, RoutedEventArgs e)
        {
            // Krijg geklikte vaknaam.
            string ctrlName = ((Control)sender).Name;
            string[] words = ctrlName.Split("vak");

            // Converteer de vaknaam naar een getal.
            int vak = Convert.ToInt32(words[1]);

            speler1.ZetStapSpeler(vak);
            VeranderVeld(vak, speler1.plaatjeSpeler, false, true);

            int beschikbarevakken = bke.CheckVak();

            if (bke.WinCheck())
                {
                    // Speler wint.

                    speler1.SetScoreSpeler(3);
                    ai1.SetScoreAI(-1);

                    txtnaamSpeler.Foreground = new SolidColorBrush(Colors.Green);
                    txtnaamAI.Foreground = new SolidColorBrush(Colors.Red);

                    NextGameShow();
                }
                else if (beschikbarevakken != 0)
                {
                    //await DelayAsync();

                    ai1.ZetStapAI();
                    VeranderVeld(ai1.stapAI, ai1.plaatjeAI, false , true);

                    if (bke.WinCheck())
                    {
                        // AI wint.   

                        speler1.SetScoreSpeler(-1);
                        ai1.SetScoreAI(3);

                        txtnaamSpeler.Foreground = new SolidColorBrush(Colors.Red);
                        txtnaamAI.Foreground = new SolidColorBrush(Colors.Green);

                        NextGameShow();
                    }
                }
            
            else
            {
                // Gelijkspel.

                txtnaamSpeler.Foreground = new SolidColorBrush(Colors.Orange);
                txtnaamAI.Foreground = new SolidColorBrush(Colors.Orange);

                NextGameShow();
            }
        }

        //private async Task DelayAsync()
        //{
        //    Task.Delay(TimeSpan.FromSeconds(0.5)).Wait();
        //}

        private void NextGameShow()
        {
            btnRestart.Visibility = Visibility.Visible;

            speler1.SetScoreSpelerZero();
            ai1.SetScoreAIZero();

            txtscoreSpeler.Text = speler1.scoreSpeler.ToString();
            txtscoreAI.Text = ai1.scoreAI.ToString();

            for (int i = 1; i < 10; i++)
            {
                VeranderVeld(i, plaatjes, false, false);
            }
        }

        private void VeranderVeld (int vak, BitmapImage imageSource, bool enabled, bool reset)
        {
            switch (vak)
            {
                default:
                    break;
                case 1:
                    if (reset == true)
                    {
                        image.Source = imageSource;
                    }

                    vak1.IsEnabled = enabled;
                    break;
                case 2:
                    if (reset == true)
                    {
                        image2.Source = imageSource;
                    }

                    vak2.IsEnabled = enabled;
                    break;
                case 3:
                    if (reset == true)
                    {
                        image3.Source = imageSource;
                    }

                    vak3.IsEnabled = enabled;
                    break;
                case 4:
                    if (reset == true)
                    {
                        image4.Source = imageSource;
                    }

                    vak4.IsEnabled = enabled;
                    break;
                case 5:
                    if (reset == true)
                    {
                        image5.Source = imageSource;
                    }

                    vak5.IsEnabled = enabled;
                    break;
                case 6:
                    if (reset == true)
                    {
                        image6.Source = imageSource;
                    }

                    vak6.IsEnabled = enabled;
                    break;
                case 7:
                    if (reset == true)
                    {
                        image7.Source = imageSource;
                    }

                    vak7.IsEnabled = enabled;
                    break;
                case 8:
                    if (reset == true)
                    {
                        image8.Source = imageSource;
                    }

                    vak8.IsEnabled = enabled;
                    break;
                case 9:
                    if (reset == true)
                    {
                        image9.Source = imageSource;
                    }

                    vak9.IsEnabled = enabled;
                    break;
            }
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            btnRestart.Visibility = Visibility.Collapsed;

            for (int i = 1; i < 10; i++)
            {
                VeranderVeld(i, plaatjes, true , true);
            }

            veld.ClearVeld();

            txtnaamAI.Foreground = new SolidColorBrush(Colors.White);
            txtnaamSpeler.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
