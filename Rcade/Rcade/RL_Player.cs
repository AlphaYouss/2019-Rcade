using System;
using System.Collections.Generic;

namespace Rcade
{
    class RL_Player
    {
        public List<KeyValuePair<string, int>> bet { get; private set; } = new List<KeyValuePair<string, int>>();
        public string name { get; private set; }
        public int balance { get; private set; }
        public string placedbet { get; private set; }
        public DateTime lastPlayed { get; private set; }

        public RL_Player(int balance, string name)
        {
            this.balance = balance;
            this.name = name;
            lastPlayed = DateTime.Now;
        }

        public void PlaceBet(string name, int amount)
        {
            bet.Add(new KeyValuePair<string, int>(name, amount));

            placedbet = name;
            balance = balance - amount;
        }

        public void CancelBet(int amount)
        {
            bet.Clear();
            balance = balance + amount;
        }

        public void ClearBet()
        {
            bet.Clear();
        }

        public void SetBalance(int amount)
        {
            balance = balance + amount;
        }
    }
}