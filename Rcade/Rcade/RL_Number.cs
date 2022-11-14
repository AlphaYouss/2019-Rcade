namespace Rcade
{
    class RL_Number
    {
        public int number { get; private set; }
        public colors color;
        public enum colors { Red, Black, Green };

        public RL_Number(int number, colors color)
        {
            this.number = number;
            this.color = color;
        }
    }
}