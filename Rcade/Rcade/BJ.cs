namespace Rcade
{
    class BJ
    {
        public BJ_Player player { get; private set; } = new BJ_Player();
        public BJ_Player dealer { get; private set; } = new BJ_Player();
        public BJ_Cards stackOfCards { get; private set; } = new BJ_Cards();
        public enum fiches { Fifty = 50, Hundered = 100, Twohundered = 200, Fivehunderd = 500 };
        public string result { get; private set; } = "";
        public bool gameOver { get; private set; } = false;
        public bool insurance { get; private set; } = false;
        public bool hasInsurance { get; private set; }
        public bool playedTurn { get; private set; } = false;
        public double actualStake { get;  set; } = 50;
        private double stake { get; set; } = 50;

        public void FirstTurn(double stake)
        {
            bool visible = false;

            player.SetCard(stackOfCards);
            if (player.CheckForAce() == true)
            {
                visible = true;
            }

            dealer.SetCard(stackOfCards);
            dealer.SetDealerAces();
            if (dealer.CheckInsurance())
            {
                hasInsurance = true;
                visible = true;
            }

            if (!visible)
            {
                SecondTurn(stake);
            }

        }

        public void SecondTurn(double stake)
        {
            player.SetCard(stackOfCards);
            player.CheckForAce();

            dealer.SetCard(stackOfCards);
            dealer.SetDealerAces();

            if (insurance)
            {
                if (dealer.playerCards[1].value == 10)
                {
                    double insuranceMoney = (stake / 2) + stake;

                    player.AddBalance(insuranceMoney);

                    Stand(stake, stackOfCards);

                    gameOver = true;
                }
            }
            CheckBlackjack(stake);
            playedTurn = true;
        }

        public void CheckForPlayerBust()
        {
            if (player.GetTotalPoints() > 21)
            {
                result = "Player bust!";
                gameOver = true;
            }
        }

        public void CheckBlackjack(double stake)
        {
            if (player.GetTotalPoints() == 21)
            {
                gameOver = true;
                CheckWinner(stake);
            }
        }

        public void Stand(double stake, BJ_Cards cards)
        {
            dealer.CheckHit(cards);
            CheckWinner(stake);
        }

        public string CheckWinner(double stake)
        {
            int playerPoints = player.GetTotalPoints();
            int dealerPoints = dealer.GetTotalPoints();

            if (insurance && dealerPoints == 21)
            {
                result = "Insurance paid out!";
            }

            else if (playerPoints == 21)
            {
                result = "Player blackjack!";
                player.AddBalance(stake * 2);
            }

            else if (dealerPoints > 21)
            {
                result = "Dealer Bust!";
                player.AddBalance(stake * 2);
            }

            else if (playerPoints > dealerPoints && playerPoints <= 21)
            {
                result = "Player wins!";
                player.AddBalance(stake * 2);
            }

            else if (dealerPoints > playerPoints)
            {
                result = "Dealer wins!";
            }

            else if (playerPoints == dealerPoints)
            {
                result = "No winner, Push!";
                player.AddBalance(stake);
            }

            gameOver = true;
            return result;
        }

        public void Clear()
        {
            player.Clear();
            dealer.Clear();

            result = "";

            gameOver = false;
            hasInsurance = false;
            playedTurn = false;

            SetStakeReset();
        }

        public bool CheckStake(double stake)
        {
            this.stake += stake;

            if (this.stake > player.balance)
            {
                result = "Balance too low.";
                SetStakeReset();

                return false;
            }
            else if (this.stake > 1000)
            {
                result = "Bet max 1000.";
                this.stake = actualStake;

                return false;
            }
            else if (player.balance <= 0)
            {
                result = "Balance negative.";
                return false;
            }
            else
            {
                result = "";
                actualStake = this.stake;

                return true;
            }
        }

        public void SetResult(string result)
        {
            this.result = result;
        }

        public void SetInsurance(bool value)
        {
            insurance = value;
        }

        public void SetPlayedTurn(bool value)
        {
            playedTurn = value;
        }

        public void SetStakeReset()
        {
            stake = 50;
            actualStake = 50;
        }
    }
}