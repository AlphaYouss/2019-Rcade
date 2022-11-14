using System;
using Windows.UI.Xaml.Media.Imaging;

namespace Boter__Kaas_en_Eieren
{
    public class Speler
    {
        public Veld veld { get; private set; } 
        public int scoreSpeler { get; private set; } = 0;
        public string naamSpeler { get; private set; } = "Goebles";
        public BitmapImage plaatjeSpeler { get; private set; } = new BitmapImage(new Uri("ms-appx:///Assets/Fotos/Dikke x.png"));

        public Speler(Veld veld)
        {
            this.veld = veld;   
        }

        public void ZetStapSpeler(int vak)
        {
            veld.Velden[vak] = "X";    
        }

        public void SetScoreSpeler(int score)
        {
            scoreSpeler += score;
        }

        public void SetScoreSpelerZero()
        {
            if (scoreSpeler < 0)
            {
                scoreSpeler = 0;
            }
        }  
    }
}
