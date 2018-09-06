using SDD.Class;
using SDD.Utility;
using System;
using static SDD.Interface.IPileCalcExtensions;
using static SDD.Utility.Utils;
using static System.Console;

namespace SamuelDube_Tp1
{
    class Program
    {
        /// <summary>
        /// Program that stores multiple number value inside a pile
        /// </summary>
        static void Main()
        {
            new Program().Principal();
        }

        /// <summary>
        /// Main Method that runs an infinite loop that performs operation on a numbers pile
        /// </summary>
        void Principal()
        {
            PileCalcListe pile = new PileCalcListe();
            for (; ; )
            {
                DisplayPileContent(pile);
                DisplayInstructions("[nombre] p/pop r/reset e/exit");

                string userInput = DisplayUserInput();

                if (userInput.IsNumberSyntaxOkay(true))
                    pile.Push(userInput.ExtractInt32());
                else if (userInput == "p")
                    pile.Pop();
                else if (userInput == "r")
                    pile.Reset();
                else if (userInput == "e")
                    Environment.Exit(0);
                else
                    DisplayInvalidInstruction("The instruction entered is invalid");

            }
        }

        /// <summary>
        /// Method that display the content of the current on the screen
        /// </summary>
        /// <param name="pile"></param>
        private void DisplayPileContent(PileCalcListe pile)
        {
            ChangeConsoleColor(ConsoleColor.Green, ConsoleColor.Black);
            WriteLine(pile.EnTexte().PadRight(50));
        }

    }
}
