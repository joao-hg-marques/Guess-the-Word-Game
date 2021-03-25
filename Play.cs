//SID:1720289
//Trimester 1 2020
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;//searching out this function
using System.Security.Cryptography.X509Certificates;



namespace Guess_The_Word
{
    class Play
    {
        Random random = new Random();
        //Inserting the txt file to be called again
        //The "System.IO.File.ReadAllLine" opens a text file, reads all lines of the file into a string array, and then closes the file.
        //Variables
        private static string[] words = System.IO.File.ReadAllLines(@"C:\Users\joaoh\OneDrive\Anglia Ruskin Uni\Third Year\AI\sid1720289\Guess_The_Word_Assigment\Guess The Word\dictionary.txt");        
        private List<char> letters= new List<char>();
        private List<char> lettersGuessed= new List<char>();
        private List<char> wrongLetters= new List<char>();
        private List<char> word = new List<char>();
        private List<char> wrongGuesses = new List<char>();
        private bool gamePlaying = false;
        string user;
        int numberOfGuesses = 0;
        bool numberOfAttempts;
        int incorrectGuessesCount;
        private static bool GuessingVerification(string guess)
        {
            //using System.Text.RegularExpressions;
            //Provides regular expression functionality that may be used from any platform or language that runs within .NET. 
            //In addition to the types contained in this namespace, the RegexStringValidator class enables you to determine whether a particular string conforms to a regular expression pattern.
            //referenced from https://github.com/MattJonesDev
            // Must be alphabetical, and a single character.
            //Validates whether or not the guess is valid.
            //The following example uses a regular expression to check for repeated occurrences of words in a string. The regular expression \b(?<word>\w+)\s+(\k<word>)\b can be interpreted as shown in the following table.
            //https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex?view=netcore-3.1
            //<returns>True if the guess is valid, otherwise false.</returns>
            //using the RegexStringValidator class
            return (guess.Length == 1) && Regex.IsMatch(guess, @"^[a-zA-Z]+$");
        }
        public static void Initialise()
        {
            // It will select random words from the dictionary.
            //It starts the game from the beguining
            // It will connect with the Play method and therefore, the game can be played.
            var random = new Random();
            var game = new Play(words[random.Next(words.Length)]);
        }
        public Play(string words)
        {
          
            // Setup the game with the new word.
            letters.AddRange(words);                
            for (var i = 0; i < letters.Count; i++)
            {
                lettersGuessed.Add('_');
            }
            //If gamePlaying is true, it calls the Game Method.
            //Visual Studio suggested to add the exclamation point because it was not happening anything.
            if (!gamePlaying)
            {
                Game();
            }               
        }
        
        private void Game()
        {
           //Prompitng the user with the number of attempts
            PromptingUser();
            //Rules
            //Handles and runs the Game.
            // Bool condition "gameplaying" is true, it is now running the functionality of the game
            gamePlaying = true;
        
            while (lettersGuessed.Contains('_'))
            {
                //It Cleans the screen above the word to be guessed and displays "feedback" to the player. 
                Console.Clear();
               //Displaying feedback to the player by giving which letters have been guessed by the player
                UpdateInfo(lettersGuessed, wrongLetters);
                //Giving feedback by giving teasing messages and the final "last chance" message.                           
                NumberOfAttempts(wrongLetters, incorrectGuessesCount);
                //Asks the user's guesses.
                //"var" representes a text character.
                var guessingLetter = Console.ReadLine().ToLower();
                //Possible fuzzy system/Bayeasian system.
                //It has two different results while playinf the game. One for guessed letters and another for number of attempts
                if (GuessingVerification(guessingLetter)) 
                {
                    var rigth = Convert.ToChar(guessingLetter);                                      
                    //The "!" was added because it was not resulting any result. Then, negating the function, it allowed to match any letter beloging to the word.                   
                    if (!lettersGuessed.Contains(rigth) && !wrongLetters.Contains(rigth))
                    {  
                        if (letters.Contains(rigth))
                        {
                            //If there is a correct guess.
                            for (var i = 0; i < letters.Count; i++)
                            {
                                if (letters[i] == rigth)
                                {
                                    lettersGuessed[i] = rigth;
                                }
                                else
                                {
                                    //It will not count as a lost guess when a letter is guessed.
                                    incorrectGuessesCount  = rigth;
                                }                                                              
                            }                          
                        }
                        else
                        {
                            // If there is an incorrect guess.
                            wrongLetters.Add(rigth);
                            //wrongGuesses was added in order to count the number of attempts. If is "rigth" it will count the number of attempts.
                            wrongGuesses.Add(rigth);
                            //When the number of attempts is greater than the number of attempts inserted by player,gives a message for losers.
                            if (wrongLetters.Count >= numberOfGuesses && wrongGuesses.Count >= numberOfGuesses) 
                            {                                
                                MessageForLosers(letters);
                            }
                        }
                    }
                }
            }
            // When the player wins
            MessageForWinners(letters, word);
        }
        public void UpdateInfo(List<char> word, List<char> wrongGuesses)
        { 
           Console.WriteLine("Word: ");
            foreach (var letter in word)
            {
                Console.Write(letter + " ");            
            }       
            Console.WriteLine();
            Console.WriteLine("Used letters: ");
            foreach (var letter in wrongGuesses)
            {                
                Console.Write(letter + ",");
            }
           Console.WriteLine();              
        }
        public void NumberOfAttempts(List<char> wrongGuesses, int incorrectGuessesCount)
        {
            Console.WriteLine("Number of Attempts:{0}",numberOfGuesses);
            //The foreach counts the number of wrong guesses with the function "Count()".
            foreach (var letter in wrongGuesses)
            {
                wrongGuesses.Count();
            }
            //If the number of guesses are 0, no messages are sent.
            if (wrongGuesses.Count == 0)
            {
                Console.WriteLine(); 
            }
            else
            {
                //It will count and display the number of attempts 
                Console.WriteLine("You've wasted {0} of {1} attempts!!You are ruining attempts and I am winning!!!Keep going!!", wrongGuesses.Count, numberOfGuesses);
            }
            Console.WriteLine();
            //warning the player that he has one attempt left.
            if (incorrectGuessesCount >= numberOfGuesses-1)
            {
                Console.WriteLine("AHAHAH!! Be careful, it migth be your last guess!!");
            }
            Console.WriteLine();
            
            if (wrongGuesses.Count >= random.Next(5,9))
            {
                Console.WriteLine("I am bored!! I decided to load a new word. :)");
                Play.Initialise();
            }
            Console.WriteLine();
        }
        public void MessageForLosers(List<char>word)
        {
           //Message for loosers 
            Console.Clear();
            Console.WriteLine(" You Lose!!!!!!!This computer is better than you!!!Sending Love!!!");
            Console.WriteLine("Press any key do continue");
            Console.ReadLine();
            Console.WriteLine("The word was:"); 
            //Displays the word
            foreach (var letter in word)
            {
                Console.Write(letter);
            }
            Console.WriteLine();
            Console.WriteLine("Do you want to continue playing?Y/N");
            user = Console.ReadLine();
            if (user == "y")
            {
                //it cleans the screen and calls the Initialise Method and the game starts over again.
                Console.Clear();
                Initialise();
            }
            else
            {
                Environment.Exit(0);
            }          
        }
        public void MessageForWinners(List<char> word,List<char> letters)
        {
            //When the player wins the game
            Console.Clear();
            Console.WriteLine("WOW!!!You have beated me!!!");
            Console.WriteLine("The word was: ");
            //It gives the guessed word. It gathers all guessed letters.
            foreach (var letter in word)
            {
                Console.Write(letter);
            }
            Console.ReadLine();
            Console.WriteLine("Do you want to play another game?Y/N");
            user= Console.ReadLine();
            if (user == "y")
            {
                //it cleans the screen and calls the Initialise method.
                Console.Clear();
                Initialise();
            }
            else
            {
                //exit
                Environment.Exit(0);
            }
           
        }
        public void PromptingUser()
        {
           //It prompts the user the number of attempts he wants. 
            Console.WriteLine("How many attempts do you want?");
            user = Console.ReadLine();
            numberOfAttempts = int.TryParse(user, out numberOfGuesses);
            Console.WriteLine("You have {0} attempts.Press any key to continue playing.", numberOfGuesses);
            Console.ReadKey();
           
        }

    }
}
               


