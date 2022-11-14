namespace Rcade
{
    class BJ_Card
    {
        public int value { get; private set; }
        public string image { get; private set; }

        public BJ_Card(int givenValue, string givenCard)
        {
            value = givenValue;
            image = givenCard;
        }

        public void SetAceValue(int value)
        {
            this.value = value;
        }
    }
}