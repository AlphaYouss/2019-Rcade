namespace Rcade
{
    class GB_SpecialFields
    {
        public GB ganzenbord { get; private set; }
        public GB_Board board { get; private set; }
        public GB_Dice dice { get; private set; }

        public GB_SpecialFields(GB_Dice dice)
        {
            this.dice = dice;
        }

        public int DoubleThrow(int location)
        {
            location = location + dice.throwCount;
            dice.ChangeThrowCount(dice.throwCount);

            if (location > 63)
            {
                int number = 0;

                number = location - 63;

                location = 63;
                location = location - number;

                dice.ChangeThrowCount(-number);
            }
            return location;
        }

        public int BridgeEvent(int location)
        {
            dice.ChangeThrowCount(6);
            return location = 12;
        }

        public bool InnEvent()
        {
            return true;
        }

        public bool WellEvent()
        {
            return true;
        }

        public int MazeEvent(int location)
        {
            dice.ChangeThrowCount(-5);
            return location = 37;
        }

        public bool PrisonEvent()
        {
            return true;
        }

        public int DeathEvent(int location)
        {
            return location = 0;
        }

        public bool EndEvent()
        {
            return true;
        }
    }
}
