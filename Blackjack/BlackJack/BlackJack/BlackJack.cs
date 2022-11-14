using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class BlackJack
    {
        public Speler deSpeler { get; private set; } = new Speler();
        public Speler deDealer { get; private set; } = new Speler();
        public Kaarten bank { get; private set; } = new Kaarten();
        
        public string uitkomst { get; private set; } = "";
        public bool gameOver { get; private set; } = false;
        
        public bool Insurance { get; private set; } = false;
        public bool HeeftInsurance { get; private set; }
        public bool Beurtgedaan { get; private set; } = false;

        private double Inzet = 50;
        public double EchteInzet { get; private set; } = 50;
        
        public enum fiches { Fifty = 50, Hundered = 100, Twohundered = 200, Fivehunderd = 500 };
        


        //Start 
        public void Beurt1(double inzet)
        {
            bool Zichtbaar = false;

            deSpeler.PakKaart(bank);
            if (deSpeler.Aas() == true)
            {
                Zichtbaar = true;
            }

            deDealer.PakKaart(bank);
            deDealer.DealerAzen();
            if (deDealer.InsuranceControle())
            {
                HeeftInsurance = true;
                Zichtbaar = true;
            }

            if (!Zichtbaar)
            {
                Beurt2(inzet);
            }
        }

        public void Beurt2(double inzet)
        {
            deSpeler.PakKaart(bank);
            deSpeler.Aas();
            

            deDealer.PakKaart(bank);
            deDealer.DealerAzen();
            if (Insurance)
            {
                if (deDealer.spelerKaarten[1].Punt == 10)
                {
                    double InsuranceGeld = (inzet / 2) + inzet;

                    deSpeler.SaldoBijschrijven(InsuranceGeld);

                    Stand(inzet,bank);
                }
            }
            Beurtgedaan = true;
        }



        //Hit
        public void Hit(double inzet)
        {
            deSpeler.PakKaart(bank);
            SpelerBustControle(inzet);
        }

        public void SpelerBustControle(double Inzet)
        {
          if (deSpeler.TotaalPunten() > 21)
            {
                uitkomst = "Speler bust!";
                gameOver = true;
            } 
        }



        //Stand
        public void Stand(double inzet, Kaarten bank)
        {
            deDealer.HitControle(bank);

            WinnaarControle(inzet);
        }
      
        public string WinnaarControle(double inzet)
        {
            
            int Speler = deSpeler.TotaalPunten();
            int Dealer = deDealer.TotaalPunten();

            if(Insurance && Dealer == 21)
            {
                uitkomst = "Insurance wordt uitbetaald!"; 

            }

            else if (Speler == 21)
            {
                uitkomst = "De speler heeft blackjack!";
                deSpeler.SaldoBijschrijven(inzet *2);
            }

            else if (Dealer > 21)
            {
                uitkomst = "Dealer Bust!";
                deSpeler.SaldoBijschrijven(inzet * 2);
            }


            else if (Speler > Dealer && Speler <= 21)
            {
                uitkomst = "De speler heeft gewonnen!";
                deSpeler.SaldoBijschrijven(inzet * 2);
            }
           
            
            else if ( Dealer > Speler )
            {
                uitkomst = "De dealer heeft gewonnen!";
                
            }

            else if (Speler == Dealer)
            {
                uitkomst = "Push. Gelijkspel!";
                deSpeler.SaldoBijschrijven(inzet);
               
            }
            gameOver = true;
            return uitkomst;
        }



        //Einde
        public void Clear()
        {
            deSpeler.HandLegen();
            deDealer.HandLegen();
            uitkomst = "";
            gameOver = false;
            HeeftInsurance = false;
            Beurtgedaan = false;
            ResetInzet();
        }



        //Inzet
        public bool InzetCheck(double inzet)
        {
            Inzet += inzet;

            if (Inzet > deSpeler.saldo)
            {
                uitkomst = "Je inzet is groter dan je saldo!";
                ResetInzet();
                return false;
            }
            else if(Inzet > 1000)
            {
                uitkomst = "Je inzet is te hoog (max 1000)";
                Inzet = EchteInzet;
                return false;
            }
            else if(deSpeler.saldo <= 0)
            {
                uitkomst = "Je saldo is negatief..";
                // Hier komt een link naar de back knop
                return false;
            }
            else
            {
                uitkomst = "";
                EchteInzet = Inzet;
                return true;
            }
        }



        // Variabele met een private set aanpassen
        public void ChangeUitkomst(string NieuweUitkomst)
        {
            uitkomst = NieuweUitkomst;
        }

        public void ChangeInsurance(bool Waarde)
        {
                Insurance = Waarde;
        }

        public void ChangeBeurtGedaan(bool Waarde)
        {
            Beurtgedaan = Waarde;
        }

        public void ResetInzet()
        {
            Inzet = 50;
            EchteInzet = 50;
        }
    }
}
