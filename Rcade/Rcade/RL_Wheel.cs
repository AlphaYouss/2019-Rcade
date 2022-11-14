using System;
using System.Threading.Tasks;

namespace Rcade
{
    class RL_Wheel
    {
        public RL_Numbers allNumbers { get; private set; }
        public RL_Number winningNumber { get; private set; }
        private Random randomNumber { get; set; } = new Random();
        public int spinDuration { get; private set; }
        private int landedNumber { get; set; }

        public RL_Wheel()
        {
            allNumbers = new RL_Numbers();
        }

        public async void Spin()
        {
            SetSpinDuration();

            while (spinDuration != 0)
            {
                spinDuration = spinDuration - 1;
                await Task.Delay(200);
            }

            landedNumber = randomNumber.Next(0, 37);
            GetWinningNumber();
        }

        private void GetWinningNumber()
        {
            winningNumber = allNumbers.numbers.Find(i => i.number == landedNumber);
        }

        private void SetSpinDuration()
        {
            spinDuration = randomNumber.Next(10, 21);
        }
    }
}