using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Kaart
    {
        public int Punt { get; private set;}
        public string Plaatje { get; private set;}


        //Constructor
        public Kaart(int GegevenPunt, string GegevenKaart)
        {
            Punt = GegevenPunt;
            Plaatje = GegevenKaart;
        }


        // Voor Aas
        public void ChangeToOne()
        {
           Punt = 1;
        }

        public void ChangeToEleven()
        {
            Punt = 11;
        }
    }
}
