using System.Collections.Generic;

namespace Roulette
{
    class RL_Numbers
    {
        public List<RL_Number> numbers = new List<RL_Number>();
        private enum colors { Red, Black, Green };

        public RL_Numbers()
        {
            CreateNumbers();
        }

        public void CreateNumbers()
        {
            numbers.Add(new RL_Number(0, colors.Green.ToString()));

            numbers.Add(new RL_Number(1, colors.Red.ToString()));
            numbers.Add(new RL_Number(2, colors.Black.ToString()));
            numbers.Add(new RL_Number(3, colors.Red.ToString()));
            numbers.Add(new RL_Number(4, colors.Black.ToString()));
            numbers.Add(new RL_Number(5, colors.Red.ToString()));
            numbers.Add(new RL_Number(6, colors.Black.ToString()));
            numbers.Add(new RL_Number(7, colors.Red.ToString()));
            numbers.Add(new RL_Number(8, colors.Black.ToString()));
            numbers.Add(new RL_Number(9, colors.Red.ToString()));
            numbers.Add(new RL_Number(10, colors.Black.ToString()));
            numbers.Add(new RL_Number(11, colors.Black.ToString()));
            numbers.Add(new RL_Number(12, colors.Red.ToString()));

            numbers.Add(new RL_Number(13, colors.Black.ToString()));
            numbers.Add(new RL_Number(14, colors.Red.ToString()));
            numbers.Add(new RL_Number(15, colors.Black.ToString()));
            numbers.Add(new RL_Number(16, colors.Red.ToString()));
            numbers.Add(new RL_Number(17, colors.Black.ToString()));
            numbers.Add(new RL_Number(18, colors.Red.ToString()));
            numbers.Add(new RL_Number(19, colors.Black.ToString()));
            numbers.Add(new RL_Number(20, colors.Black.ToString()));
            numbers.Add(new RL_Number(21, colors.Red.ToString()));
            numbers.Add(new RL_Number(22, colors.Black.ToString()));
            numbers.Add(new RL_Number(23, colors.Red.ToString()));
            numbers.Add(new RL_Number(24, colors.Black.ToString()));

            numbers.Add(new RL_Number(25, colors.Red.ToString()));
            numbers.Add(new RL_Number(26, colors.Black.ToString()));
            numbers.Add(new RL_Number(27, colors.Red.ToString()));
            numbers.Add(new RL_Number(28, colors.Red.ToString()));
            numbers.Add(new RL_Number(29, colors.Black.ToString()));
            numbers.Add(new RL_Number(30, colors.Red.ToString()));
            numbers.Add(new RL_Number(31, colors.Black.ToString()));
            numbers.Add(new RL_Number(32, colors.Red.ToString()));
            numbers.Add(new RL_Number(33, colors.Black.ToString()));
            numbers.Add(new RL_Number(34, colors.Red.ToString()));
            numbers.Add(new RL_Number(35, colors.Black.ToString()));
            numbers.Add(new RL_Number(36, colors.Red.ToString()));
        }
    }
}