using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace Rcade
{
    class TTT_NewAI
    {
        public TTT_Field field { get; private set; } = new TTT_Field();
        public BitmapImage imageAi { get; private set; } = new BitmapImage(new Uri("ms-appx:///Assets/Images/ttt/o.png"));
        public bool firstMoveDone { get; set; }
        public string nameAi { get; private set; } = "Gibby";
        public int scoreAi { get; private set; } = 0;
        public int moveAI { get; private set; }
        private string[,] rows { get; set; }

        public TTT_NewAI(TTT_Field field)
        {
            this.field = field;
            UpdateRows();
        }

        private void UpdateRows()
        {
            rows = new string[,]
            {
                {field.box[1], field.box[2], field.box[3]},
                {field.box[4], field.box[5], field.box[6]},
                {field.box[7], field.box[8], field.box[9]},

                {field.box[1], field.box[4], field.box[7]},
                {field.box[2], field.box[5], field.box[8]},
                {field.box[3], field.box[6], field.box[9]},

                {field.box[1], field.box[5], field.box[9]},
                {field.box[3], field.box[5], field.box[7]},
            };
        }

        public void NewMove()
        {
            if (!firstMoveDone)
            {
                RandomTurn();
                firstMoveDone = true;
            }
            else
            {
                TryToWin();
            }
        }

        private void RandomTurn()
        {
            List<int> remainingBoxes = new List<int>();

            for (int i = 1; i < field.box.Count; i++)
            {
                if (field.box[i] != "X" && field.box[i] != "O")
                {
                    remainingBoxes.Add(i);
                }
            }
           int box = remainingBoxes[GenerateNumber(remainingBoxes.Count)];
           // int box = 1;
            field.box[box] = "O";
            moveAI = box;
        }

        public int GenerateNumber(int MaxValue)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, MaxValue);

            return number;
        }

        private void Clear()
        {
            Array.Clear(rows, 0, 24);
        }

        private void ForkBlock()
        {
            Clear();
            UpdateRows();

            int i = 0;
            int x = 0;

            bool done = false;
            int contains = 0;

            for (i = 0; i < rows.GetLength(0); i++)
            {
                for (x = 0; x < 3; x++)
                {
                    if (rows[i, x] == "X" && rows[i, x] != "O")
                    {
                        contains++;
                    }
                    else if (rows[i, x] != "X" && rows[i, x] != "O")
                    {
                        moveAI = Convert.ToInt32(rows[i, x]);
                    }
                    else
                    {
                        moveAI = 0;
                    }
                }

                if (contains == 2 && field.box[moveAI] != "O" && moveAI != 0)
                {
                    field.box[moveAI] = "O";
                    done = true;

                    break;
                }
                else
                {
                    contains = 0;
                }
            }
            if (!done)
            {
                RandomTurn();
            }
            done = false;
        }

        private void TryToWin()
        {
            Clear();
            UpdateRows();

         //   moveAI = 0;
            int i = 0;
            int x = 0;

            bool done = false;
            int contains = 0;

            for (i = 0; i < rows.GetLength(0); i++)
            {
                for (x = 0; x < 3; x++)
                {
                    if (rows[i, x] == "O" && rows[i, x] != "X")
                    {
                        contains++;
                    }
                    else if (rows[i, x] != "X" && rows[i, x] != "O")
                    {
                        moveAI = Convert.ToInt32(rows[i, x]);
                    }
                    else
                    {
                        moveAI = 0;
                    }
                }
                if (contains == 2 && field.box[moveAI] != "O" && moveAI != 0)
                {
                    field.box[moveAI] = "O";
                    done = true;

                    break;
                }
                else
                {
                    contains = 0;
                }
            }
            if (!done)
            {
                ForkBlock();
            }
            done = false;
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