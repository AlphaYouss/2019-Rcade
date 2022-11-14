using System;

namespace Rcade
{
    class Hm_Player
    {
        public int totalScore { get; private set; }
        public int totalMistakes { get; private set; }
        public int score { get; private set; } = 0;
        public int gameScore { get; private set; } = 0;
        public int turn { get; private set; } = 0;
        public DateTime lastPlayed { get; private set; }
        enum scoreOptions { Ten = 10, Seven = 7, Five = 5, Three = 3, One = 1, Zero = 0 }

        public void SetPlayerTotalStats(int totalScore, int totalMistakes)
        {
            this.totalScore = totalScore;
            this.totalMistakes = totalMistakes;

            lastPlayed = DateTime.Now;
        }

        public void SetPlayerStats(int gameScore, int turn)
        {
            totalScore = totalScore + gameScore;
            totalMistakes = totalMistakes + turn;

            lastPlayed = DateTime.Now;
        }

        public void SetScore()
        {
            if (turn == 1 || turn == 0)
            {
                score += Convert.ToInt32(scoreOptions.Ten);
                gameScore += Convert.ToInt32(scoreOptions.Ten);
            }
            else if (turn >= 2 && turn < 4)
            {
                score += Convert.ToInt32(scoreOptions.Seven);
                gameScore += Convert.ToInt32(scoreOptions.Seven);
            }
            else if (turn >= 4 && turn < 6)
            {
                score += Convert.ToInt32(scoreOptions.Five);
                gameScore += Convert.ToInt32(scoreOptions.Five);
            }
            else if (turn >= 6 && turn < 9)
            {
                score += Convert.ToInt32(scoreOptions.Three);
                gameScore += Convert.ToInt32(scoreOptions.Three);
            }
            else if (turn >= 9 && turn < 11)
            {
                score += Convert.ToInt32(scoreOptions.One);
                gameScore += Convert.ToInt32(scoreOptions.One);
            }
            else if (turn >= 11)
            {
                score += Convert.ToInt32(scoreOptions.Zero);
                gameScore += Convert.ToInt32(scoreOptions.Zero);
            }
        }

        public void AddTurn()
        {
            turn++;
        }

        public void ClearTurn()
        {
            turn = 0;
        }

        public void ClearGameScore()
        {
            gameScore = 0;
        }
    }
}