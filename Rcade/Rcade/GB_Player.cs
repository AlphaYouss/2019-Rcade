using Windows.UI.Xaml.Media.Imaging;

namespace Rcade
{
    class GB_Player
    {
        public GB_Board bord { get; private set; }
        public GB_Dice dice { get; private set; }
        public GB_SpecialFields specialFields { get; private set; }
        public BitmapImage playerImage { get; private set; }
        public int location { get; private set; }
        public bool skipTurn { get; private set; }
        public bool stuckInWellPrison { get; private set; }
        public string field { get; private set; }
        public bool winGame { get; private set; }

        public GB_Player(GB_Board board, GB_Dice dice, GB_SpecialFields specialFields, BitmapImage playerImage)
        {
            this.playerImage = playerImage;
            this.specialFields = specialFields;
            this.dice = dice;

            bord = board;
            location = 0;

            skipTurn = false;
            stuckInWellPrison = false;
        }

        public string PlayerMove()
        {
            dice.XD6(2);
            if (location == 0 && dice.throwCount == 9)
            {
                location = 26;

                field = "nineOnFirstTurn";
                return "nineOnFirstTurn";
            }
            else
            {
                location = location + dice.throwCount;

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
            if (field == "nineOnFirstTurn")
            {
                location = location - 26;
            }
            else
            {
                location = location - dice.throwCount;
            }
        }

        public void EventStart(string eventString)
        {
            switch (eventString)
            {
                default:
                    break;
                case "bridge":
                    location = specialFields.BridgeEvent(location);
                    break;
                case "inn":
                    skipTurn = specialFields.InnEvent();
                    break;
                case "well":
                    stuckInWellPrison = specialFields.WellEvent();
                    break;
                case "maze":
                    location = specialFields.MazeEvent(location);
                    break;
                case "jail":
                    stuckInWellPrison = specialFields.PrisonEvent();
                    break;
                case "dead":
                    location = specialFields.DeathEvent(location);
                    break;
                case "end":
                    winGame = specialFields.EndEvent();
                    break;
                case "dubbleThrow":
                    location = specialFields.DoubleThrow(location);
                    break;
            }
        }

        public void Restart()
        {
            location = 0;
        }

        public void ChangeStuckInWellPrison()
        {
            stuckInWellPrison = false;
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