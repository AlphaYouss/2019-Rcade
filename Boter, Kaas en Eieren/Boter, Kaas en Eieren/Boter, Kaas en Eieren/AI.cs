using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace Boter__Kaas_en_Eieren
{
    class AI
    {
        public Veld veld { get; private set; } 
        public int scoreAI { get; private set; } = 0;
        public int stapAI { get; private set; }
        public string naamAI { get; private set; } = "Stalin";
        public BitmapImage plaatjeAI { get; private set; } 

        public AI(Veld veld)
        {
            this.veld = veld;
            plaatjeAI = new BitmapImage(new Uri("ms-appx:///Assets/Fotos/Dikke O.png"));
        }

        public void ZetStapAI()
        {
            List<int> overgeblevenVakken = new List<int>();

            for (int i = 1; i < veld.Velden.Count; i++)
            {
                if (veld.Velden[i] != "X" && veld.Velden[i] != "O")
                {
                    overgeblevenVakken.Add(i);               
                }
            }

            int vak = GenereerNummer(overgeblevenVakken.Count, overgeblevenVakken);
        
            veld.Velden[vak] = "O";
            stapAI = vak;     
        }

        private int GenereerNummer(int overgeblevenGetallen, List<int> overgeblevenVakken)
        {
            Random rnd = new Random();
            int vak = rnd.Next(1, overgeblevenGetallen);

            return overgeblevenVakken[vak];
        }
   
        public void SetScoreAIZero()
        {
            if (scoreAI < 0)
            {
                scoreAI = 0;
            }
        }

        public void SetScoreAI(int score)
        {
            scoreAI += score;
        }
    }
}
