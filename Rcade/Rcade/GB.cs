using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Rcade
{
    class GB
    {
        public GB_Dice dice { get; private set; }
        public GB_Board board { get; private set; }

        public List<GB_Player> players { get; private set; }
        public GB_Player player1 { get; private set; }
        public GB_Player player2 { get; private set; }
        public GB_Player player3 { get; private set; }
        public GB_Player player4 { get; private set; }
        public GB_Player player5 { get; private set; }

        public List<Image> fieldImages { get; private set; }
        public BitmapImage purplePawn { get; private set; }
        public BitmapImage bluePawn { get; private set; }
        public BitmapImage greenPawn { get; private set; }
        public BitmapImage redPawn { get; private set; }
        public BitmapImage blackPawn { get; private set; }
        public BitmapImage noPicture { get; private set; }

        public int numberOfPlayers { get; private set; }
        public int playerTurn { get; private set; }
        public string field { get; private set; }
        public int number { get; set; }
        public bool winGame { get; set; }

        public GB(int numberOfPlayers, List<Image> fieldImages)
        {
            dice = new GB_Dice();
            board = new GB_Board(dice);

            this.fieldImages = fieldImages;

            noPicture = new BitmapImage(new Uri("ms-appx:///"));
            purplePawn = new BitmapImage(new Uri("ms-appx:///Assets/Images/gb/PionBlauw.png"));
            bluePawn = new BitmapImage(new Uri("ms-appx:///Assets/Images/gb/PionGeel.png"));
            greenPawn = new BitmapImage(new Uri("ms-appx:///Assets/Images/gb/PionGroen.png"));
            redPawn = new BitmapImage(new Uri("ms-appx:///Assets/Images/gb/PionRood.png"));
            blackPawn = new BitmapImage(new Uri("ms-appx:///Assets/Images/gb/PionWit.png"));

            players = new List<GB_Player>();

            player1 = new GB_Player(board, dice, board.specialfields, purplePawn);
            player2 = new GB_Player(board, dice, board.specialfields, bluePawn);
            player3 = new GB_Player(board, dice, board.specialfields, greenPawn);
            player4 = new GB_Player(board, dice, board.specialfields, redPawn);
            player5 = new GB_Player(board, dice, board.specialfields, blackPawn);

            playerTurn = 0;

            this.numberOfPlayers = numberOfPlayers;

            FillPlayerList();
            board.GenerateBoard();
        }

        public void PlayerMove()
        {
            fieldImages[players[playerTurn].location].Source = noPicture;
            field = players[playerTurn].PlayerMove();

            CheckField(players[playerTurn].location);

            if (CheckPlayersOnField(players[playerTurn].location))
            {
                players[playerTurn].RevertLocation();
                field = "twoOnOneBox";
            }
            winGame = players[playerTurn].winGame;
            ChangeImage();
        }

        private void ChangeImage()
        {
            fieldImages[players[playerTurn].location].Source = players[playerTurn].playerImage;
            Task.Delay(200).Wait();
        }

        private void FillPlayerList()
        {
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                switch (i)
                {
                    default:
                        break;
                    case 1:
                        players.Add(player1);
                        break;
                    case 2:
                        players.Add(player2);
                        break;
                    case 3:
                        players.Add(player3);
                        break;
                    case 4:
                        players.Add(player4);
                        break;
                    case 5:
                        players.Add(player5);
                        break;
                }
            }
        }

        private bool CheckPlayersOnField(int location)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].location == location && players[i] != players[playerTurn] && location != 0)
                {
                    players[playerTurn].ChangeSkipTurn();
                    return true;
                }
            }
            return false;
        }

        private void CheckField(int location)
        {
            if (field != "nineOnFirstTurn")
            {
                field = board.fields[location];

                if (field != null)
                {
                    string fieldAfterThrow;

                    players[playerTurn].EventStart(field);

                    location = players[playerTurn].location;
                    fieldAfterThrow = board.fields[location];

                    players[playerTurn].EventStart(fieldAfterThrow);
                    CheckWellPrison();
                }
            }
            else if (CheckPlayersOnField(players[playerTurn].location))
            {
                players[playerTurn].RevertLocation();
                field = "twoOnOneBox";
            }
        }

        public void NextPlayer()
        {
            playerTurn++;

            if (playerTurn == numberOfPlayers)
            {
                playerTurn = 0;
            }
            dice.ResetThrowCount();
        }

        public bool CheckSkippingTurn()
        {
            return (players[playerTurn].skipTurn);
        }

        public bool CheckStuck()
        {
            return players[playerTurn].stuckInWellPrison;
        }

        private bool CheckWellPrison()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[playerTurn].stuckInWellPrison == true && players[i].stuckInWellPrison == true && players[playerTurn] != players[i])
                {
                    players[i].ChangeStuckInWellPrison();
                    players[i].ChangeLocation(1);

                    fieldImages[players[i].location].Source = players[i].playerImage;
                    fieldImages[players[i].location - 1].Source = noPicture;

                    number = 0;

                    return true;
                }
            }
            return false;
        }

        public void Restart()
        {
            for (int i = 0; i < players.Count; i++)
            {
                fieldImages[players[i].location].Source = noPicture;
                players[i].Restart();
            }
        }

        public void ChangeSkipTurn()
        {
            players[playerTurn].ChangeSkipTurn();
        }

        public void ChangeStuck()
        {
            players[playerTurn].ChangeStuckInWellPrison();
        }
    }
}