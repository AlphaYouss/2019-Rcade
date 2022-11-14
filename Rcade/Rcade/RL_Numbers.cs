using System.Collections.Generic;

namespace Rcade
{
    class RL_Numbers
    {
        public List<RL_Number> numbers = new List<RL_Number>();

        public RL_Numbers()
        {
            CreateNumbers();
        }

        public void CreateNumbers()
        {
            numbers.Add(new RL_Number(0, RL_Number.colors.Green));

            numbers.Add(new RL_Number(1, RL_Number.colors.Red));
            numbers.Add(new RL_Number(2, RL_Number.colors.Black));
            numbers.Add(new RL_Number(3, RL_Number.colors.Red));
            numbers.Add(new RL_Number(4, RL_Number.colors.Black));
            numbers.Add(new RL_Number(5, RL_Number.colors.Red));
            numbers.Add(new RL_Number(6, RL_Number.colors.Black));
            numbers.Add(new RL_Number(7, RL_Number.colors.Red));
            numbers.Add(new RL_Number(8, RL_Number.colors.Black));
            numbers.Add(new RL_Number(9, RL_Number.colors.Red));
            numbers.Add(new RL_Number(10, RL_Number.colors.Black));
            numbers.Add(new RL_Number(11, RL_Number.colors.Black));
            numbers.Add(new RL_Number(12, RL_Number.colors.Red));

            numbers.Add(new RL_Number(13, RL_Number.colors.Black));
            numbers.Add(new RL_Number(14, RL_Number.colors.Red));
            numbers.Add(new RL_Number(15, RL_Number.colors.Black));
            numbers.Add(new RL_Number(16, RL_Number.colors.Red));
            numbers.Add(new RL_Number(17, RL_Number.colors.Black));
            numbers.Add(new RL_Number(18, RL_Number.colors.Red));
            numbers.Add(new RL_Number(19, RL_Number.colors.Black));
            numbers.Add(new RL_Number(20, RL_Number.colors.Black));
            numbers.Add(new RL_Number(21, RL_Number.colors.Red));
            numbers.Add(new RL_Number(22, RL_Number.colors.Black));
            numbers.Add(new RL_Number(23, RL_Number.colors.Red));
            numbers.Add(new RL_Number(24, RL_Number.colors.Black));

            numbers.Add(new RL_Number(25, RL_Number.colors.Red));
            numbers.Add(new RL_Number(26, RL_Number.colors.Black));
            numbers.Add(new RL_Number(27, RL_Number.colors.Red));
            numbers.Add(new RL_Number(28, RL_Number.colors.Red));
            numbers.Add(new RL_Number(29, RL_Number.colors.Black));
            numbers.Add(new RL_Number(30, RL_Number.colors.Red));
            numbers.Add(new RL_Number(31, RL_Number.colors.Black));
            numbers.Add(new RL_Number(32, RL_Number.colors.Red));
            numbers.Add(new RL_Number(33, RL_Number.colors.Black));
            numbers.Add(new RL_Number(34, RL_Number.colors.Red));
            numbers.Add(new RL_Number(35, RL_Number.colors.Black));
            numbers.Add(new RL_Number(36, RL_Number.colors.Red));
        }
    }
}