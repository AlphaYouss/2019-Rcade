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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlackJack
{
  
    public sealed partial class MainPage : Page
    {

        BlackJack GameHost = new BlackJack();
        public double inzet { get; private set; }
        
        Image[] SpelerImages;
        Image[] DealerImages;


        public MainPage()
        { 
            InitializeComponent();

            SpelerImages = new Image[] { SpelerKaart1, SpelerKaart2, SpelerKaart3, SpelerKaart4, SpelerKaart5, SpelerKaart6, SpelerKaart7, SpelerKaart8 };
            DealerImages = new Image[] { DealerKaart1, DealerKaart2, DealerKaart3, DealerKaart4, DealerKaart5, DealerKaart6, DealerKaart7, DealerKaart8 };

            ClearText();
            InzetAan();
            UpdateText();
        }



        // Text aanpassen
        private void ClearText()
        {
            Saldo.Text = "";
            Uitkomst.Text = "";
            KaartenSpeler.Text = "";
            KaartenDealer.Text = "";
            Uitkomst.Text = "";

            GameHost.ChangeUitkomst("");

            Hit.Visibility = Visibility.Visible;
            Stand.Visibility = Visibility.Visible;

            NewGame.Visibility = Visibility.Collapsed;

            Elf.Visibility = Visibility.Collapsed;
            Een.Visibility = Visibility.Collapsed;

            Insurance.Visibility = Visibility.Collapsed;
            DoubleDownKnop.Visibility = Visibility.Collapsed;

            GameHost.ChangeInsurance(false);
            ImagesClear(SpelerImages);
            ImagesClear(DealerImages);
        }


        private void UpdateText()
        {
            Saldo.Text = Convert.ToString(GameHost.deSpeler.saldo);
            KaartenDealer.Text = GameHost.deDealer.GetKaarten();
            KaartenSpeler.Text = GameHost.deSpeler.GetKaarten();
            Uitkomst.Text = GameHost.uitkomst;
            TbInzet.Text =Convert.ToString(GameHost.EchteInzet);
            tbWinstVerlies.Text = Convert.ToString(GameHost.deSpeler.WinstVerlies);

            UpdateImage(GameHost.deSpeler, SpelerImages);

            if (!GameHost.deSpeler.Aas())
            {
                TotaalPuntenLatenZien();
            }

            if (GameHost.gameOver)
            {
                UpdateImage(GameHost.deDealer, DealerImages);
            }
        }



        //Inzet Bepalen
        private void InzetAan()
        {
            Hit.IsEnabled = false;
            Stand.IsEnabled = false;

            TbInzet.Text = Convert.ToString(GameHost.EchteInzet);
            ConfirmInzet.IsEnabled = true;
            ResetInzet.IsEnabled = false;
            VijftigKnop.IsEnabled = true;
            HonderdKnop.IsEnabled = true;
            TweehonderdKnop.IsEnabled = true;
            VijfhonderdKnop.IsEnabled = true;
        }

        private void ConfirmInzet_Click(object sender, RoutedEventArgs e)
        {
            if (GameHost.InzetCheck(0))
            {
                inzet = Convert.ToDouble(TbInzet.Text);
                GameHost.deSpeler.SaldoAfschrijven(inzet);

                StartNewGame();

                ConfirmInzet.IsEnabled = false;
                ResetInzet.IsEnabled = false;

                Hit.IsEnabled = true;
                Stand.IsEnabled = true;

                Uitkomst.Text = "";
                GameHost.ChangeUitkomst("");

                Elf.IsEnabled = true;
                Een.IsEnabled = true;

                VijftigKnop.IsEnabled = false;
                HonderdKnop.IsEnabled = false;
                TweehonderdKnop.IsEnabled = false;
                VijfhonderdKnop.IsEnabled = false;
            }
            else
            {
                GameHost.ResetInzet();
            }
            UpdateText();
        }

        private void ResetInzet_Click(object sender, RoutedEventArgs e)
        {
            GameHost.ResetInzet();
            UpdateText();
            ResetInzet.IsEnabled = false;
        }



        //Start
        private void StartNewGame()
        {
            ClearText();

            GameHost.Beurt1(inzet);
            AasControle(GameHost.deSpeler.HeeftAas);
           
            if (GameHost.HeeftInsurance)
            {
                Insurance.Visibility = Visibility.Visible;
            }
                        
            if (GameHost.deSpeler.spelerKaarten.Count == 2)
            {
                DoubleDown();
            }
           
            UpdateText();
            InzetAan();
            
            UpdateImage(GameHost.deSpeler, SpelerImages);
            DealerImage(GameHost.deDealer, DealerImages);
        }



        //Hit
        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            GameHost.deSpeler.PakKaart(GameHost.bank);

            AasControle(GameHost.deSpeler.Aas());

            if (GameHost.deSpeler.Aas())
            {
                AasControle(true);
            }
            else
            {
                GameHost.SpelerBustControle(inzet);
            }

            GameCheck();
            UpdateText();
            Insurance.Visibility = Visibility.Collapsed;
            DoubleDownKnop.Visibility = Visibility.Collapsed;
        }


        public void GameCheck()
        {
            if (GameHost.gameOver)
            {
                Hit.Visibility = Visibility.Collapsed;
                Stand.Visibility = Visibility.Collapsed;

                NewGame.Visibility = Visibility.Visible;
                Elf.Visibility = Visibility.Collapsed;
                Een.Visibility = Visibility.Collapsed;

                UpdateImage(GameHost.deDealer, DealerImages);
            }
        }


        private void TotaalPuntenLatenZien()
        {
            if (GameHost.deSpeler.TotaalPunten() != 0)
            {
                TotaalPuntenSpeler.Text = Convert.ToString(GameHost.deSpeler.TotaalPunten());
            }
            else
            {
                TotaalPuntenSpeler.Text = "";
            }
        }



        // Stand
        private  void Stand_Click(object sender, RoutedEventArgs e)
        {
            StandMethod();
        }

        private void StandMethod()
        {
            GameHost.Stand(inzet, GameHost.bank);

            GameCheck();

            UpdateText();

            UpdateImage(GameHost.deDealer, DealerImages);
        }



        // Einde
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            GameHost.Clear();
            ClearText();
            UpdateText();
            InzetAan();
            NewGame.Visibility = Visibility.Collapsed;
            Een.IsEnabled = false;
            Elf.IsEnabled = false;
        }


 
        //Aas
        private void AasControle(bool Aas)
        {
            if (Aas)
            {
                if (GameHost.deSpeler.TotaalPunten() - 14 > 21)
                {
                    StandMethod(); 
                }
                else
                {
                    Elf.Visibility = Visibility.Visible;
                    Een.Visibility = Visibility.Visible;
                    Elf.IsEnabled = true;
                    Een.IsEnabled = true;

                    Hit.Visibility = Visibility.Collapsed;
                    Stand.Visibility = Visibility.Collapsed;
                }
                UpdateText();
                GameHost.deSpeler.VeranderHeeftAas(false);
            }
        }

        private void AasClick()
        {
            Elf.Visibility = Visibility.Collapsed;
            Een.Visibility = Visibility.Collapsed;
            Hit.Visibility = Visibility.Visible;
            Stand.Visibility = Visibility.Visible;

            TotaalPuntenLatenZien();

            GameHost.SpelerBustControle(inzet);
            GameCheck();
            UpdateText();
        }

        private void Elf_Click(object sender, RoutedEventArgs e)
        {
            GameHost.deSpeler.spelerKaarten[GameHost.deSpeler.AasPlek].ChangeToEleven();
            AasClick();
            
            if (!GameHost.Beurtgedaan)
            {
                GameHost.Beurt2(inzet);
                AasControle(GameHost.deSpeler.Aas());
                GameHost.ChangeBeurtGedaan(true);
            }
            UpdateText();
        }

        private void Een_Click(object sender, RoutedEventArgs e)
        {
            GameHost.deSpeler.spelerKaarten[GameHost.deSpeler.AasPlek].ChangeToOne();
            AasClick();
           
            if (!GameHost.Beurtgedaan)
            {
                GameHost.Beurt2(inzet);
                AasControle(GameHost.deSpeler.Aas());
                GameHost.ChangeBeurtGedaan(true);
            }
            UpdateText();
        }



        // Double Down
        private void DoubleDown()
        {
            if (GameHost.deSpeler.DoubleDownControle() && inzet*2 < GameHost.deSpeler.saldo )
            {
                DoubleDownKnop.Visibility = Visibility.Visible;
                GameHost.ChangeUitkomst("");
            }
        }

        private void DoubleDown_Click(object sender, RoutedEventArgs e)
        {
            inzet *= 2;
            TbInzet.Text = Convert.ToString(inzet);
            DoubleDownKnop.Visibility = Visibility.Collapsed;
            GameHost.deSpeler.SaldoAfschrijven(inzet);
        }



        //Insurance 
        private void Insurance_Click(object sender, RoutedEventArgs e)
        {
            GameHost.ChangeInsurance(true);
            GameHost.deSpeler.SaldoAfschrijven(inzet / 2);
            Insurance.Visibility = Visibility.Collapsed;

            if (!GameHost.Beurtgedaan)
            {
                GameHost.Beurt2(inzet);
                AasControle(GameHost.deSpeler.Aas());
                GameHost.ChangeBeurtGedaan(true);
            }
            UpdateText();
        }



        //  Fiches
        private void VijftigKnop_Click(object sender, RoutedEventArgs e)
        {
            if (GameHost.InzetCheck(Convert.ToDouble(BlackJack.fiches.Fifty)))
            {
                inzet = GameHost.EchteInzet;
                TbInzet.Text = Convert.ToString(inzet);
                ResetInzet.IsEnabled = true;
            }
            UpdateText();
        }

        private void HonderdKnop_Click(object sender, RoutedEventArgs e)
        {
            if (GameHost.InzetCheck(Convert.ToDouble(BlackJack.fiches.Hundered)))
            {
                inzet = GameHost.EchteInzet;
                TbInzet.Text = Convert.ToString(inzet);
                ResetInzet.IsEnabled = true;
            }
            UpdateText();
        }

        private void TweehonderdKnop_Click(object sender, RoutedEventArgs e)
        {
            if (GameHost.InzetCheck(Convert.ToDouble(BlackJack.fiches.Twohundered)))
            {
                inzet = GameHost.EchteInzet;
                TbInzet.Text = Convert.ToString(inzet);
                ResetInzet.IsEnabled = true;
            }
            UpdateText();
        }

        private void VijfhonderdKnop_Click(object sender, RoutedEventArgs e)
        {
            if (GameHost.InzetCheck(Convert.ToDouble(BlackJack.fiches.Fivehunderd)))
            {
                inzet = GameHost.EchteInzet;
                TbInzet.Text = Convert.ToString(inzet);
                ResetInzet.IsEnabled = true;
            }
            UpdateText();
        }



        //Images
        private void UpdateImage(Speler speler, Image[] ImagesArray)
        {
            for (int i = 0; i < speler.spelerKaarten.Count; i++)
            {
                string locatie = "ms-appx:///Assets/Speelkaarten/" + speler.GetKaart(i); 
                
                BitmapImage ImageSource = new BitmapImage(new Uri(locatie));
                ImagesArray[i].Source = ImageSource;
            }
        }

        private void DealerImage(Speler dealer, Image[] ImagesArray)
        {
                
                string locatie = "ms-appx:///Assets/Speelkaarten/" + dealer.GetKaart(0);
                BitmapImage ImageSource1 = new BitmapImage(new Uri(locatie));
                ImagesArray[0].Source = ImageSource1;

                BitmapImage ImageSource2 = new BitmapImage(new Uri("ms-appx:///Assets/Speelkaarten/achterkant kaart.jpg"));
                ImagesArray[1].Source = ImageSource2;
            
        }

        private void ImagesClear(Image[] ImagesArray)
        {
            for (int i = 0; i < ImagesArray.Length; i++)
            {
                ImagesArray[i].Source = null;
            }
        }

        
    }
}
