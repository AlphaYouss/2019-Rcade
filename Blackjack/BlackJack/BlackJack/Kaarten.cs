using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Kaarten
    {
        public Random randomNummer { get; private set; } = new Random();
        enum kleuren  { C, D, H, S };
        enum plaatjes { J, Q, K, A };



        public Kaart NieuweKaart()
        { 
            int IntKaart = randomNummer.Next(2, 15);
            int x = 0;
            bool gevonden = false;
            string StrKaart = "";

            for (int i = 11; i < 15; i++)
            {
                if (gevonden == false)
                {
                    if (IntKaart == i)
                    {
                        StrKaart = Enum.GetName(typeof(plaatjes), x) + Enum.GetName(typeof(kleuren), randomNummer.Next(0, 4)) + ".jpg";
                        gevonden = true;

                        if (IntKaart != 14)
                        {
                           IntKaart = 10;          
                        }
                    }
                    else
                    {
                        StrKaart = Convert.ToString(IntKaart) + Enum.GetName(typeof(kleuren), randomNummer.Next(0, 4)) + ".jpg";
                    }
                }
                x++;
            }

            Kaart DeKaart = new Kaart(IntKaart,StrKaart);
            return DeKaart;
        }
    }
}

