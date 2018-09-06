using System;
using static SDD.Utility.Utils;
using SDD.Class;
using static System.Console;
using static SDD.Utility.CharExtensions;

namespace SDD
{
    class Program
    {
        /// <summary>
        /// Program that stacks a set of number inside a cumulator
        /// </summary>
        static void Main()
        {
            new Program().Principal();
        }

        /// <summary>
        /// Method that runs a for ever loop for letting the user input various instruction cumulator
        /// </summary>
        void Principal()
        {
            Accumuleur accumuleur = new Accumuleur((string)null);
            for( ; ; )
            {
                DisplayCumulatorContent(accumuleur);
                DisplayInstructions("[Chiffre] e/extraire d/décumuler r/reset x/exit");

                char choix = DisplayKeyPress();

                if (choix.IsNumber())
                {
                    accumuleur.Accumuler(choix);
                    Clear();
                }
                else if (choix == 'e')
                {
                    accumuleur.Extraire();
                    Clear();
                }
                else if (choix == 'd')
                {
                    accumuleur.Décumuler();
                    Clear();
                }
                else if (choix == 'r')
                {
                    accumuleur.Reset();
                    Clear();
                }
                else if (choix == 'x')
                {
                    Environment.Exit(0);
                }
                else
                    DisplayInvalidInstruction("The instruction entered is unhandled at the moment");

            }
        }

        /// <summary>
        /// Method that display the current cummulator content on the screen
        /// </summary>
        /// <param name="accumuleur">Cumulator content to display</param>
        private static void DisplayCumulatorContent(Accumuleur accumuleur)
        {
            ChangeConsoleColor(ConsoleColor.Cyan, ConsoleColor.Black);
            WriteLine(accumuleur.Accumulation.PadRight(50));
        }
    }
}
