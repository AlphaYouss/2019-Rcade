using System;
using System.Collections.Generic;

namespace Rcade
{
    class BJ_Cards
    {
        private Random randomNumber { get; set; } = new Random();
        private enum colors { C, D, H, S };
        private enum images { J, Q, K, A };


        public BJ_Card NewCard()
        {         
            int intCard = randomNumber.Next(2, 15);

            int x = 0;

            bool foundCard = false;
            string card = "";

            for (int i = 11; i < 15; i++)
            {
                if (foundCard == false)
                {
                    if (intCard == i)
                    {
                        card = Enum.GetName(typeof(images), x) + Enum.GetName(typeof(colors), randomNumber.Next(0, 4)) + ".jpg";
                        foundCard = true;

                        if (intCard != 14)
                        {
                            intCard = 10;
                        }
                    }
                    else
                    {
                        card = Convert.ToString(intCard) + Enum.GetName(typeof(colors), randomNumber.Next(0, 4)) + ".jpg";
                    }
                }
                x++;
            }
            BJ_Card aCard = new BJ_Card(intCard, card);

            return aCard;
        }
    }
}