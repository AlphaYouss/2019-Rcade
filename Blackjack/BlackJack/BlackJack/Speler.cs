using System.Collections.Generic;

namespace BlackJack
{
    class Speler
    {
        public List<Kaart> spelerKaarten { get; private set; } = new List<Kaart>();
        public double saldo { get; private set; } = 25000;
        public int AasPlek { get; private set; }
        public bool HeeftAas { get; private set; }
        public double WinstVerlies { get; private set; } = 0;



        //Start
        public void PakKaart(Kaarten bank)
        {
            spelerKaarten.Add(bank.NieuweKaart());
        }

        public string GetKaart(int i)
        {
            return spelerKaarten[i].Plaatje;
        }

        public string GetKaarten()
        {
            string Kaart = "";
            for (int i = 0; i < spelerKaarten.Count; i++)
            {
                Kaart = Kaart + "    " + spelerKaarten[i].Plaatje;
            }
            return Kaart;
        }



        //Hit
        public int TotaalPunten()
        {
            int totaal = 0;

            for (int i = 0; i < spelerKaarten.Count; i++)
            {
                totaal += spelerKaarten[i].Punt;
            }
            return totaal;
        }

        public void DealerAzen()
        {
            for (int i = 0; i < spelerKaarten.Count; i++)
            {
                if (spelerKaarten[i].Punt == 14)
                {
                    spelerKaarten[i].ChangeToEleven();
                }
            }
        }



        //Stand
        public void HitControle(Kaarten bank)
        {
            while (TotaalPunten() < 17)
            {
                PakKaart(bank);
                DealerAzen();
            }
        }

        public void SaldoBijschrijven(double Inzet)
        {
            saldo += Inzet;
            WinstVerlies += Inzet;
        }

        public void SaldoAfschrijven(double Inzet)
        {
            saldo -= Inzet;
            WinstVerlies -= Inzet;
        }



        //Einde
        public void VeranderHeeftAas(bool Choice)
        {
            HeeftAas = Choice;
        }

        public void HandLegen()
        {
            spelerKaarten.Clear();
            HeeftAas = false;
        }



        // Speciale Methodes
        public bool Aas()
        {
            for (int i = 0; i < spelerKaarten.Count; i++)
            {
                if (spelerKaarten[i].Punt == 14)
                {
                    AasPlek = i;
                    HeeftAas = true;
                    return true;
                }
            }
            return false;
        }

        public bool InsuranceControle()
        {
            if (spelerKaarten[0].Plaatje.Contains("A"))
            {
                return true;
            }
            return false;
        }

        public bool DoubleDownControle()
        {
            if (TotaalPunten() == 9 || TotaalPunten() == 10 || TotaalPunten() == 11)
            {
                return true;
            }
            return false;
        }    
    }
}
