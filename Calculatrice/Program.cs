using System;
using static SDD.Utility.Utils;
using SDD.Class;
using static System.Console;
using static SDD.Utility.CharExtensions;
using SDD.Interface;

namespace SDD
{
	class Program
	{
		static void Main()
		{
			new Program().Principal();
		}

		void Principal()
		{
			Calculatrice calculatrice = new Calculatrice();
			for (; ; )
			{
				DisplayCalculatorContent(calculatrice);
				DisplayInstructions("% * + - 0 1 2 3 4 5 6 7 8 9 \\ B D E N P R S ²");

				string commande = DisplayUserInput();

				if (commande == "exit")
				{
					Environment.Exit(0);
				}
				else if(!String.IsNullOrEmpty(commande))
				{
					Calculatrice tempCalc = (Calculatrice)calculatrice.Cloner();
					try
					{
						calculatrice.Exécuter(commande);
						Clear();
					}
					catch(Exception e)
					{
						DisplayInvalidInstruction(e.Message);
						calculatrice = tempCalc;
					}
				}
				else
				{
					DisplayInvalidInstruction("The instruction entered is unhandled at the moment");
				}
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="accumuleur">Cumulator content to display</param>
		private static void DisplayCalculatorContent(Calculatrice calculatrice)
		{
			ChangeConsoleColor(ConsoleColor.Cyan, ConsoleColor.Black);
			WriteLine(calculatrice.EnTexte().PadRight(50));
		}
	}
}
