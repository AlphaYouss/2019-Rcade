using System;
using Windows.UI.Xaml.Media.Imaging;

namespace Rcade
{
    class TTT_Player
    {
        public TTT_Field field { get; private set; }
        public BitmapImage imagePlayer { get; private set; } = new BitmapImage(new Uri("ms-appx:///Assets/Images/ttt/x.png"));
        public string namePlayer { get; private set; } = "";
        public int scorePlayer { get; private set; } = 0;
        public int won { get; private set; } = 0;
        public int lost { get; private set; } = 0;
        public int draw { get; private set; } = 0;
        public DateTime lastPlayed { get; private set; }

        public TTT_Player(TTT_Field field)
        {
            this.field = field;
        }

        public void TakeTurn(int box)
        {
            field.box[box] = "X";
        }

        public void SetPlayerName(string playerName)
        {
            namePlayer = playerName;
        }

        public void SetStats(string[] tttValues)
        {
            won = Convert.ToInt32(tttValues[0]);
            lost = Convert.ToInt32(tttValues[1]);
            draw = Convert.ToInt32(tttValues[2]);
            lastPlayed = DateTime.Now;
        }

        public void SetStats(int status)
        {
            switch (status)
            {
                case 0:
                    won = won + 1;
                    break;
                case 1:
                    lost = lost + 1;
                    break;
                case 2:
                    draw = draw + 1;
                    break;
            }
        }

        public void SetScore(int score)
        {
            scorePlayer += score;
        }

        public void SetScore()
        {
            if (scorePlayer < 0)
            {
                scorePlayer = 0;
            }
        }
    }
}