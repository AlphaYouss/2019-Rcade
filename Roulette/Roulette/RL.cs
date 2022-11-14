using System;

namespace Roulette
{
    class RL
    {
        public RL_Wheel wheel { get; private set; }
        public RL_Player player { get; private set; }
        private int multiplier { get; set; }
        public int totalMoneyWon { get; private set; } = 0;
        public bool isPlaying { get; private set; } = false;
        public enum chips { Fifty = 50, OneHundred = 100, TwoHundred = 200, FiveHundred = 500 }
        public enum numbers { btnZero = 0, btnOne = 1, btnTwo = 2, btnThree = 3, btnFour = 4, btnFive = 5, btnSix = 6, btnSeven = 7, btnEight = 8, btnNine = 9, btnTen = 10, btnEleven = 11, btnTwelve = 12, btnThirteen = 13, btnFourteen = 14, btnFifteen = 15, btnSixteen = 16, btnSeventeen = 17, btnEighteen = 18, btnNineteen = 19, btnTwenty = 20, btnTwentyone = 21, btnTwentytwo = 22, btnTwentythree = 23, btnTwentyfour = 24, btnTwentyfive = 25, btnTwentysix = 26, btnTwentyseven = 27, btnTwentyeight = 28, btnTwentynine = 29, btnThirty = 30, btnThirtyone = 31, btnThirtytwo = 32, btnThirtythree = 33, btnThirtyfour = 34, btnThirtyfive = 35, btnThirtysix = 36 }
        public enum colors { btnRed = 0, btnBlack = 1 };
        public enum bets { btnEven = 0, btnOdd = 1 };
        public enum halves { btnFirstEighteen = 0, btnLastEighteen = 1 };
        public enum columns { btnFirstTwelve = 0, btnSecondTwelve = 1, btnThirdTwelve = 2 };
        public enum rows { btnFirstRow = 0, btnSecondRow = 1, btnThirdRow = 2 };

        public RL()
        {
            wheel = new RL_Wheel();
            player = new RL_Player();
        }

        public void SpinWheel()
        {
            wheel.Spin();
        }

        public void PlaceBet(string buttonName, int type, int amount)
        {
            string value = "";
            int number = 0;

            switch (type)
            {
                case 0:
                    int selectedNumber = Convert.ToInt32((numbers)Enum.Parse(typeof(numbers), buttonName));
                    value = selectedNumber.ToString();
                    break;
                case 1:
                    number = Convert.ToInt32((columns)Enum.Parse(typeof(columns), buttonName));

                    value = GetColumn(number);
                    break;
                case 2:
                    number = Convert.ToInt32((rows)Enum.Parse(typeof(rows), buttonName));

                    value = GetRow(number);
                    break;
                case 3:
                    number = Convert.ToInt32((halves)Enum.Parse(typeof(halves), buttonName));

                    value = GetHalves(number);
                    break;
                case 4:
                    number = Convert.ToInt32((bets)Enum.Parse(typeof(bets), buttonName));

                    value = GetBet(number);
                    break;
                case 5:
                    number = Convert.ToInt32((colors)Enum.Parse(typeof(colors), buttonName));

                    value = GetColor(number);
                    break;
            }
            player.PlaceBet(value, amount);
        }

        public void Payout()
        {
            foreach (var bet in player.bet)
            {
                switch (bet.Key)
                {
                    default:
                        Number(bet.Value, bet.Key);
                        break;
                    case "Red":
                    case "Black":
                        RedBlack(bet.Value, bet.Key);
                        break;
                    case "Even":
                    case "Odd":
                        EvenOdd(bet.Value, bet.Key);
                        break;
                    case "1-18":
                    case "19-36":
                        Halves(bet.Value);
                        break;
                    case "1st 12":
                    case "2nd 12":
                    case "3rd 12":
                        Columns(bet.Value, bet.Key);
                        break;
                    case "1st row":
                    case "2nd row":
                    case "3rd row":
                        Rows(bet.Value, bet.Key);
                        break;
                }
            }
        }

        public void Number(int value, string number)
        {
            multiplier = 35;

            if (wheel.winningNumber.number == Convert.ToInt32(number))
            {
                int moneyWon = value * multiplier;
                totalMoneyWon = totalMoneyWon + moneyWon;
                player.SetBalance(moneyWon);
            }
        }

        public void RedBlack(int value, string color)
        {
            multiplier = 1;

            if (wheel.winningNumber.color == color)
            {
                int moneyWon = value * multiplier;
                totalMoneyWon = totalMoneyWon + moneyWon;
                player.SetBalance(moneyWon);
            }
        }

        public void EvenOdd(int value, string type)
        {
            multiplier = 1;

            if (Convert.ToInt32(wheel.winningNumber.number) != 0)
            {
                switch (type)
                {
                    case "Even":
                        if (Convert.ToInt32(wheel.winningNumber.number) % 2 == 0)
                        {
                            int moneyWon = value * multiplier;
                            totalMoneyWon = totalMoneyWon + moneyWon;
                            player.SetBalance(moneyWon);
                        }
                        break;
                    case "Odd":
                        if (Convert.ToInt32(wheel.winningNumber.number) % 2 == 1)
                        {
                            int moneyWon = value * multiplier;
                            totalMoneyWon = totalMoneyWon + moneyWon;
                            player.SetBalance(moneyWon);
                        }
                        break;
                }
            }
        }

        public void Halves(int value)
        {
            multiplier = 1;
            if (wheel.winningNumber.number < 19)
            {
                int moneyWon = value * multiplier;
                totalMoneyWon = totalMoneyWon + moneyWon;
                player.SetBalance(moneyWon);
            }
            else
            {
                int moneyWon = value * multiplier;
                totalMoneyWon = totalMoneyWon + moneyWon;
                player.SetBalance(moneyWon);
            }
        }

        public void Columns(int value, string type)
        {
            multiplier = 2;

            switch (type)
            {
                case "1st 12":
                    if (wheel.winningNumber.number < 13)
                    {
                        int moneyWon = value * multiplier;
                        totalMoneyWon = totalMoneyWon + moneyWon;
                        player.SetBalance(moneyWon);
                    }
                    break;
                case "2nd 12":
                    if (wheel.winningNumber.number < 26)
                    {
                        int moneyWon = value * multiplier;
                        totalMoneyWon = totalMoneyWon + moneyWon;
                        player.SetBalance(moneyWon);
                    }
                    break;
                case "3rd 12":
                    if (wheel.winningNumber.number >= 26)
                    {
                        int moneyWon = value * multiplier;
                        totalMoneyWon = totalMoneyWon + moneyWon;
                        player.SetBalance(moneyWon);
                    }
                    break;
            }
        }

        public void Rows(int value, string type)
        {
            multiplier = 2;

            switch (type)
            {
                case "1st row":
                    if (wheel.winningNumber.number == 1 || wheel.winningNumber.number == 4 || wheel.winningNumber.number == 7 || wheel.winningNumber.number == 10 || wheel.winningNumber.number == 13 || wheel.winningNumber.number == 16 || wheel.winningNumber.number == 19 || wheel.winningNumber.number == 22 || wheel.winningNumber.number == 25 || wheel.winningNumber.number == 28 || wheel.winningNumber.number == 31 || wheel.winningNumber.number == 34)
                    {
                        int moneyWon = value * multiplier;
                        totalMoneyWon = totalMoneyWon + moneyWon;
                        player.SetBalance(moneyWon);
                    }
                    break;
                case "2nd row":
                    if (wheel.winningNumber.number == 2 || wheel.winningNumber.number == 5 || wheel.winningNumber.number == 8 || wheel.winningNumber.number == 11 || wheel.winningNumber.number == 14 || wheel.winningNumber.number == 17 || wheel.winningNumber.number == 20 || wheel.winningNumber.number == 23 || wheel.winningNumber.number == 26 || wheel.winningNumber.number == 29 || wheel.winningNumber.number == 32 || wheel.winningNumber.number == 35)
                    {
                        int moneyWon = value * multiplier;
                        totalMoneyWon = totalMoneyWon + moneyWon;
                        player.SetBalance(moneyWon);
                    }
                    break;
                case "3rd row":
                    if (wheel.winningNumber.number == 3 || wheel.winningNumber.number == 6 || wheel.winningNumber.number == 9 || wheel.winningNumber.number == 12 || wheel.winningNumber.number == 15 || wheel.winningNumber.number == 18 || wheel.winningNumber.number == 19 || wheel.winningNumber.number == 24 || wheel.winningNumber.number == 27 || wheel.winningNumber.number == 30 || wheel.winningNumber.number == 33 || wheel.winningNumber.number == 36)
                    {
                        int moneyWon = value * multiplier;
                        totalMoneyWon = totalMoneyWon + moneyWon;
                        player.SetBalance(moneyWon);
                    }
                    break;
            }
        }

        private string GetColumn(int number)
        {
            string name = "";
            switch (number)
            {
                case 0:
                    name = "1st 12";
                    break;
                case 1:
                    name = "2nd 12";
                    break;
                case 2:
                    name = "3rd 12";
                    break;
            }
            return name;
        }

        private string GetRow(int number)
        {
            string name = "";
            switch (number)
            {
                case 0:
                    name = "1st row";
                    break;
                case 1:
                    name = "2nd row";
                    break;
                case 2:
                    name = "3rd row";
                    break;
            }
            return name;
        }

        private string GetHalves(int number)
        {
            string name = "";
            switch (number)
            {
                case 0:
                    name = "1-18";
                    break;
                case 1:
                    name = "19-36";
                    break;
            }
            return name;
        }

        private string GetBet(int number)
        {
            string name = "";
            switch (number)
            {
                case 0:
                    name = "Even";
                    break;
                case 1:
                    name = "Odd";
                    break;
            }
            return name;
        }

        private string GetColor(int number)
        {
            string name = "";
            switch (number)
            {
                case 0:
                    name = "Red";
                    break;
                case 1:
                    name = "Black";
                    break;
            }
            return name;
        }
        public void SetIsPlaying(bool value)
        {
            isPlaying = value;
        }

        public void SetTotalMoneyWon(int value)
        {
            totalMoneyWon = value;
        }
    }
}