using System;
using System.Collections.Generic;
using static System.Console;

namespace SDD.Utility
{
	public class Utils
	{
        readonly static int paddingOnWriteLine = 50;

		#region Extraction Methods
        /// <summary>
        /// Method that extract an int from a string parameter. Could possibly removed since i dont use it anymore
        /// </summary>
        /// <param name="numberInput"></param>
        /// <param name="numberConverted"></param>
		static public void ExtractInt(string numberInput, out int numberConverted)
		{
			WriteLine("This is a int"); //Used for debogging
			numberConverted = Convert.ToInt32(numberInput);
		}

        /// <summary>
        /// Method that extract a double based on a string parameter. Could be removed since i dont use it anymore
        /// </summary>
        /// <param name="numberInput"></param>
        /// <param name="numberConverted"></param>
		static public void ExtractDouble(string numberInput, out double numberConverted)
		{
			WriteLine("This is a double"); //Used for debogging
			numberConverted = Convert.ToDouble(numberInput);
		}
		#endregion

		#region Display Methods
        /// <summary>
        /// Method that display a feedback letting the user know it can input data. Could be split in two methods since it doesnt respect the single use concept of methods
        /// </summary>
        /// <returns></returns>
		static public string DisplayUserInput()
		{
			ChangeConsoleColor(ConsoleColor.Black, ConsoleColor.White);
			return ReadLine().Trim().ToLower();
		}

        /// <summary>
        /// Method that display a feedback to the user when he press a key
        /// </summary>
        /// <returns>Returns the key pressed</returns>
        static public char DisplayKeyPress()
        {
            ChangeConsoleColor(ConsoleColor.Black, ConsoleColor.White);
            return ReadKey().KeyChar;
        }

        /// <summary>
        /// Methods that display an error message to the user letting him know that the instruction is not working
        /// </summary>
        /// <param name="errorMessage"></param>
		static public void DisplayInvalidInstruction(string errorMessage)
		{
			Clear();
			ChangeConsoleColor(ConsoleColor.Red, ConsoleColor.Black);
			WriteLine(errorMessage.PadRight(paddingOnWriteLine));
		}

        /// <summary>
        /// Method that display a list of instructions to the user
        /// </summary>
		static public void DisplayInstructions(string instructions)
		{
			ChangeConsoleColor(ConsoleColor.Magenta, ConsoleColor.White);
			WriteLine(instructions.PadRight(paddingOnWriteLine));
		}

        /// <summary>
        /// Method that change the background and foreground color of the console based on the parameter received
        /// </summary>
        /// <param name="backgroundColor"></param>
        /// <param name="textColor"></param>
		static public void ChangeConsoleColor(ConsoleColor backgroundColor, ConsoleColor textColor)
		{
			ForegroundColor = textColor;
			BackgroundColor = backgroundColor;
		}
		#endregion
	}

	/// <summary>
	/// Extension Method used on strings
	/// </summary>
	public static class StringExtensions
	{
        //List of all the prohibited char not allowed inside a number
		readonly static char[] mprohibitedChar = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 't', 'u', 'v', 'w', 'x', 'y', 'z',',','.' };
        //List of all the number
		readonly static char[] numberList = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        /// <summary>
        /// Method that verify if the input string contains number
        /// </summary>
        /// <param name="str"></param>
        /// <returns>True if it contains number, False if it does't</returns>
		public static bool ContainsNumber(this string str)
		{
			if (str.IndexOfAny(numberList) != -1)
				return true;
			else
				return false;
		}

        /// <summary>
        /// Method that verify that the number syntax is okay. 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nullAccepted">Pass true if null are an accepted format</param>
        /// <returns></returns>
        public static bool IsNumberSyntaxOkay(this string str, bool nullAccepted)
        {
            if ((str == null && nullAccepted) || str.ContainsProhibitedChar())
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method that verify that the string does not contains any prohibited char.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="prohibitedChar">List of prohibited char. If null, the default list is used</param>
        /// <returns></returns>
		public static bool ContainsProhibitedChar(this string str, char[] prohibitedChar = null)
        {
            //If no list of char passed, use the default list of prohibited char
            if (prohibitedChar is null)
            {
                if (str.IndexOfAny(mprohibitedChar) == -1) return true;
                else return false;
            }
            else
            {
                if (str.IndexOfAny(prohibitedChar) == -1) return true;
                else return false;
            }
        }

        /// <summary>
        /// Method that verify if the number input is a double by cheking if it contains a comma
        /// </summary>
        /// <param name="str"></param>
        /// <returns>False if its not, True if it is</returns>
		public static bool IsNumberDouble(this string str)
		{
			switch (str.IndexOf(','))
			{
				case -1: return false;
				default: return true;
			}
		}

        /// <summary>
        /// Method that extract an Integer 32 bits out of a string and recursively trim it until it can be fited isnide an int 32
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ExtractInt32(this string str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch(OverflowException)
            {
                return ExtractInt32(str.Substring(0, str.Length - 1));
            }
        }

	}

    /// <summary>
    /// Extensions Method used on Chars
    /// </summary>
    public static class CharExtensions
    {
        //List of numbers
        readonly static char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        //Could be optimised, used for IndexOf property found in List type
        static List<char> numberList = new List<char>(numbers);

        /// <summary>
        /// Method that verify that the passed character is a number contained within numberList
        /// </summary>
        /// <param name="caracter"></param>
        /// <returns></returns>
        public static bool IsNumber(this char caracter)
        {
            if (numberList.IndexOf(caracter) != -1)
                return true;
            else
                return false;
        }

    }

}
