using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace Rcade
{
    class TTT_Ai
    {
        public TTT_Field field { get; private set; }
        public BitmapImage imageAi { get; private set; } = new BitmapImage(new Uri("ms-appx:///Assets/Images/ttt/o.png"));
        public string nameAi { get; private set; } = "Izaak";
        public int scoreAi { get; private set; } = 0;
        public int moveAi { get; private set; }

        public TTT_Ai(TTT_Field field)
        {
            this.field = field;
        }

        public void TakeTurn()
        {
            List<int> remainingBoxes = new List<int>();

            for (int i = 1; i < field.box.Count; i++)
            {
                if (field.box[i] != "X" && field.box[i] != "O")
                {
                    remainingBoxes.Add(i);
                }
            }

            int box = GenerateNumber(remainingBoxes.Count, remainingBoxes);

            field.box[box] = "O";
            moveAi = box;
        }

        private int GenerateNumber(int remainingNumbers, List<int> remainingBoxes)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, remainingNumbers);

            return remainingBoxes[number];
        }

        public void SetScore(int score)
        {
            scoreAi += score;
        }

        public void SetScore()
        {
            if (scoreAi < 0)
            {
                scoreAi = 0;
            }
        }
    }
}