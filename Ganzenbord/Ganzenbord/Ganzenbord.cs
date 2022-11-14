using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Ganzenbord
{
    class Ganzenbord
    {
        public Dice dice { get; private set; }
        public Board board { get; private set; }

        public List<Player> players { get; private set; }
        public Player player1 { get; private set; }
        public Player player2 { get; private set; }
        public Player player3 { get; private set; }
        public Player player4 { get; private set; }
        public Player player5 { get; private set; }

        public List<Image> fieldImages { get; private set; }
        public BitmapImage purplePawn { get; private set; }
        public BitmapImage bluePawn { get; private set; }
        public BitmapImage greenPawn { get; private set; }
        public BitmapImage redPawn { get; private set; }
        public BitmapImage blackPawn { get; private set; }
        public BitmapImage noPicture { get; private set; } 

        public int numberOfPlayers { get; private set; }
        public int playerTurn { get; private set; }
        public string Field { get; private set; }
        public int getal { get; set; }
        public bool winGame { get; set; }

        public Ganzenbord(int numberOfPlayers, List<Image> fieldImages)
        {
            dice = new Dice();
            board = new Board(dice);

            this.fieldImages = fieldImages;

            noPicture = new BitmapImage(new Uri("ms-appx:///"));
            purplePawn = new BitmapImage(new Uri("ms-appx:///Assets/pion paars_wit.png"));
            bluePawn = new BitmapImage(new Uri("ms-appx:///Assets/pion blauw_wit.png"));
            greenPawn = new BitmapImage(new Uri("ms-appx:///Assets/pion groen_wit.png"));
            redPawn = new BitmapImage(new Uri("ms-appx:///Assets/pion rood_wit.png"));
            blackPawn = new BitmapImage(new Uri("ms-appx:///Assets/pion zwart_wit.png"));

            players = new List<Player>();

            player1 = new Player(board, dice, board.specialfields, purplePawn);
            player2 = new Player(board, dice, board.specialfields, bluePawn);
            player3 = new Player(board, dice, board.specialfields, greenPawn);
            player4 = new Player(board, dice, board.specialfields, redPawn);
            player5 = new Player(board, dice, board.specialfields, blackPawn);

            playerTurn = 0;


            this.numberOfPlayers = numberOfPlayers;

            FillPlayerList();
            board.GenerateBoard();
        }
        
        public void PlayerMove()
        {
            fieldImages[players[playerTurn].location].Source = noPicture;
            Field = players[playerTurn].PlayerMove();
            CheckField(players[playerTurn].location);
            if (CheckPlayersOnField(players[playerTurn].location))
            {
                players[playerTurn].RevertLocation();
                Field = "TweeOpÉénVak";
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

                //for (int i = 0; i < numberOfPlayers; i++)
                //{
                //   Player player = new Player;
                //    players.Add(player);
                //}
            }
        }

        private bool CheckPlayersOnField(int location)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].location == location && players[i] != players[playerTurn] && location != 0 )
                {
                    players[playerTurn].ChangeSkipTurn();
                    return true;
                }
            }
            return false;
        }

        private void CheckField(int locatie)
        {
            if (Field != "NineOnFirstTurn")
            {
                Field = board.fields[locatie];

                if (Field != null)
                {
                    string field2;
                    players[playerTurn].EventStart(Field);
                    locatie = players[playerTurn].location;
                    field2 = board.fields[locatie];
                    players[playerTurn].EventStart(field2);
                    if (CheckWell_Prison())
                    {

                    }
                }
            }
            else if (CheckPlayersOnField(players[playerTurn].location))
            {
                players[playerTurn].RevertLocation();
                Field = "TweeOpÉénVak";
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
            return players[playerTurn].stuckInWell_Prison;
        }


        private bool CheckWell_Prison()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[playerTurn].stuckInWell_Prison == true && players[i].stuckInWell_Prison == true && players[playerTurn] != players[i])
                {
                    players[i].ChangeStuckInWell_Prison();
                    players[i].ChangeLocation(1);
                    fieldImages[players[i].location].Source = players[i].playerImage;
                    fieldImages[players[i].location - 1].Source = noPicture;
                    getal = 0;

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
            players[playerTurn].ChangeStuckInWell_Prison();
        }
    }
}
