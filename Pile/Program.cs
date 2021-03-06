﻿using SDD.Class;
using SDD.Interface;
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
            PileCalcListeGen<long> pile = new PileCalcListeGen<long>();
            for (; ; )
            {
                DisplayPileContent(pile);
                DisplayInstructions("[nombre] p/pop r/reset e/exit");

                string userInput = DisplayUserInput();

                try
                {
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
                catch (Exception e)
                {
                    DisplayInvalidInstruction(e.Message);
                }

            }
        }

        /// <summary>
        /// Method that display the content of the current on the screen
        /// </summary>
        /// <param name="pile"></param>
        private void DisplayPileContent<T>(PileCalcListeGen<T> pile)
        {
            ChangeConsoleColor(ConsoleColor.Green, ConsoleColor.Black);
            WriteLine(pile.EnTexte().PadRight(50));
        }

    }
}
