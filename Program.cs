//SID:1720289
//Trimester 1 2020
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Timers;

namespace Guess_The_Word
{
    class Program
    {       
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!!!");
            Menu();
   
        }

        static void Menu()
        {
            //Menu. It will prompt the user with a few options.
            string menu;
            string yesOrNo;
            Console.WriteLine("Choose one of the following options:");
            Console.WriteLine("Rules: press 1");
            Console.WriteLine("Play Game: press 2");
            Console.WriteLine("Exit game:press 3");
            menu = Console.ReadLine();
            if (menu == "1")
            {
               //Rules of the game, explained.
                Console.Clear();
                Console.WriteLine("You must guess which word the computer is thinking");
                Console.WriteLine("You will have the chance to choose the number of attempts");
                Console.WriteLine("You will get teasing messages, but be aware. You can lose the game.");
                Console.WriteLine("Press any key do continue");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Remember, Machines cannnot be beaten...AHAHAH");
                Console.WriteLine("Do you want to play: Y/N");
                yesOrNo = Console.ReadLine();
                if (yesOrNo == "y")
                {
                    Play.Initialise();
                }
                else
                {
                    Menu();
                }
                Console.ReadLine();

            }
            else if (menu == "2")
            {
               //Initiates Game
                 Play.Initialise();
            }
            else if (menu == "3")
            {
               //Exit Game
                Environment.Exit(0);
            }
        }

    }
}
