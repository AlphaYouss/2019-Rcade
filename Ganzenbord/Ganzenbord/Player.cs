using Windows.UI.Xaml.Media.Imaging;

namespace Ganzenbord
{
    class Player
    {
        public Board bord { get; private set; }
        public Dice dice { get; private set; }
        public SpecialFields specialFields { get; private set; }
        public BitmapImage playerImage { get; private set; }
        public int location { get; private set; }
        public bool skipTurn { get; private set; }
        public bool stuckInWell_Prison { get; private set; }
        public string Field { get; private set; }
        public bool winGame { get; private set; }

        public Player(Board board, Dice dice, SpecialFields specialFields, BitmapImage playerImage)
        {
            this.playerImage = playerImage;
            this.specialFields = specialFields;
            this.dice = dice;
            bord = board;
            location = 0;
            skipTurn = false;
            stuckInWell_Prison = false;

        }

        public string PlayerMove()
        {
            dice.XD6(2);
            if (location == 0 && dice.ThrowCount == 9)
            {
                location = 26;
                Field = "NineOnFirstTurn";
                return "NineOnFirstTurn";
                
            }
            else
            {
                location = location + dice.ThrowCount;
                if (location > 63)
                {
                    int number = 0;
                    number = location - 63;
                    location = 63;
                    location = location - number;
                    dice.ChangeThrowCount(-number);
                }
            }
            return "";
        }

        public void RevertLocation()
        {
            if (Field == "NineOnFirstTurn")
            {
                location = location - 26;
            }
            else
            {
                location = location - dice.ThrowCount;
            }
        }

        public void EventStart(string Event)
        {
            switch (Event)
            {
                default:
                    break;
                case "brug":
                  location = specialFields.BridgeEvent(location);
                    break;
                case "herberg":
                   skipTurn = specialFields.InnEvent();
                    break;
                case "put":
                   stuckInWell_Prison = specialFields.WellEvent();
                    break;
                case "doolhof":
                   location = specialFields.MazeEvent(location);
                    break;
                case "gevangenis":
                    stuckInWell_Prison = specialFields.PrisonEvent();
                    break;
                case "dood":
                   location = specialFields.DeathEvent(location);
                    break;
                case "einde":
                   winGame = specialFields.EndEvent();
                    break;
                case "dubbeleworp":
                   location = specialFields.DoubleThrow(location);
                    break;
            }
        }

        public void Restart()
        {
            location = 0;
        }

        public void ChangeStuckInWell_Prison()
        {
            stuckInWell_Prison = false;
        }

        public void ChangeSkipTurn()
        {
            skipTurn = false;
        }

        public void ChangeLocation(int number)
        {
            location = location + number;
        }
    }
}