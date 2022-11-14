using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Galgje
{

    public sealed partial class MainPage : Page
    {
        Hangman host = new Hangman();
        
        
        List<string> numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" }; 
        List<Button> buttons = new List<Button> {};

        int previousTurn = 0;




        public MainPage()
        {
            this.InitializeComponent();
            AddButtons();
            Start();

        }






        // Start
        private void Start()
        {
            previousTurn = 0;
            NewGameBtn.Visibility = Visibility.Collapsed;
            host.Start();
            UpdateDisplay();
        }


        private void AddButtons()
        {
            buttons.Add(A);
            buttons.Add(B);
            buttons.Add(C);
            buttons.Add(D);
            buttons.Add(E);
            buttons.Add(F);
            buttons.Add(G);
            buttons.Add(H);
            buttons.Add(I);
            buttons.Add(J);
            buttons.Add(K);
            buttons.Add(L);
            buttons.Add(M);
            buttons.Add(N);
            buttons.Add(O);
            buttons.Add(P);
            buttons.Add(Q);
            buttons.Add(R);
            buttons.Add(S);
            buttons.Add(T);
            buttons.Add(U);
            buttons.Add(V);
            buttons.Add(W);
            buttons.Add(X);
            buttons.Add(Y);
            buttons.Add(Z);
        }






        // Update gegevens tijdens spel
        private void UpdateDisplay()
        {
            TbLetterDisplay.Text = new String(host.displayedLetters);
            TbGuessedLetters.Text = new String(host.guessedLetters.ToArray());
            TbResult.Text = host.result;
            
            UpdateImage(HangmanImage); 

            TbScore.Text = Convert.ToString(host.player.score);
        }






        //Het 'toetsenbord'
        private void Alphabet_Click(object sender, RoutedEventArgs e)
        {
            char letter = Convert.ToChar(((Control)sender).Name);

            Button button = sender as Button;
            button.IsEnabled = false;

            host.CheckLetter(letter);
            
            UpdateDisplay();

            if (host.WinCheck() || host.EndGameCheck())
            {
                EndGame();
            }
        }






        //Einde van het spel
        private void EndGame()
        {
            NewGameBtn.Visibility = Visibility.Visible;
            
            TbResult.Text = host.result;
            TbLetterDisplay.Text = new String(host.correctLetters);

            host.player.SetScore();
            TbScore.Text = Convert.ToString(host.player.score);
            
            host.player.ClearTurn();
            host.Clear();

            buttons.ForEach(x => x.IsEnabled = false);         
        }



        


        // Images
        private void UpdateImage(Image image)
        {
            if (previousTurn != host.player.turn || host.player.turn == 0)
            {
                string Location = "ms-appx:///Assets/HangmanPictures/" + numbers[host.player.turn] + ".jpg";
                BitmapImage ImageSource = new BitmapImage(new Uri(Location));
                image.Source = ImageSource;
                previousTurn = host.player.turn;
            }   
        }






        // New Game
        private void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            NewGameBtn.Visibility = Visibility.Collapsed;
            
            buttons.ForEach(x => x.IsEnabled = true);
            Start();
        }
    }
}
