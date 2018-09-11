using SDD.Interface;
using System;
using System.Collections.Generic;
using static SDD.Utility.StringExtensions;
using static SDD.Class.ÉtatCalcUtils;

namespace SDD.Class
{
    public class ÉtatCalc : IÉtatCalc
    {
        /// <summary>
        /// Constructor that takes a PileCalcList and a Accumulator as parameters
        /// </summary>
        /// <param name="pile">Set to null by default</param>
        /// <param name="acc">Set to null by default</param>
        public ÉtatCalc(IPileCalc pile = null, IAccumuleur acc = null)
        {
            mAccumuleur = (acc != null) ? (Accumuleur)acc : new Accumuleur();
            mPile = (pile != null) ? mPile = (PileCalcListe)pile : new PileCalcListe();
        }

        /// <summary>
        /// Constructor that takes an initial value for the cumulator and a initial stack of value for the pile
        /// </summary>
        /// <param name="accumulateur"></param>
        /// <param name="pile"></param>
        public ÉtatCalc(int? accumulateur, params int[] pile)
        {
            mPile = (pile != null) ? new PileCalcListe(pile) : new PileCalcListe();
            mAccumuleur = (accumulateur != null) ? new Accumuleur(accumulateur) : new Accumuleur();
        }

        /// <summary>
        /// Constructor that takes an initial value for the cumulator and an IEnnumerable for the pile stack
        /// </summary>
        /// <param name="pile"></param>
        /// <param name="accumulateur"></param>
        public ÉtatCalc(IEnumerable<int> pile, int? accumulateur = null)
        {
            mPile = new PileCalcListe(pile);
            mAccumuleur = (accumulateur != null) ? new Accumuleur(accumulateur) : new Accumuleur();
        }

        /// <summary>
        /// Constructor that can initialise the pile and cumulator by parsing a string passed as an argument
        /// </summary>
        /// <param name="enTexte"></param>
        public ÉtatCalc(string enTexte)
        {
            char[] separator = new char[] { ' ' };
            string[] strArray = enTexte.ParseStringToArray(separator);

            if (!IsStateStringOkay(strArray))
                throw new ArgumentException();

            mPile = new PileCalcListe(ParsePile(strArray));
            mAccumuleur = new Accumuleur(ParseCumulator(strArray));
        }

        /// <summary>
        /// Cumulator Driver
        /// </summary>
        protected Accumuleur mAccumuleur { get; set; }

        /// <summary>
        /// Pile Driver
        /// </summary>
        protected PileCalcListe mPile { get; set; }

        /// <summary>
        /// Contains the cumulator value
        /// </summary>
        public int? Accumulateur => mAccumuleur.Valeur;

        /// <summary>
        /// Contains the stack of value inside the pile driver
        /// </summary>
        public IEnumerable<int> Pile => mPile.Éléments;

        /// <summary>
        /// Indicate wheter the state is empty by checking inside the pile and cumulator driver
        /// </summary>
        public bool EstVide => (mAccumuleur.EstVide && mPile.EstVide);

        /// <summary>
        /// Returns the cumulato value or the first element inside the pile stack
        /// </summary>
        public int? Dessus => DessusÉtat();

        /// <summary>
        /// Methods that stack a new number inside the pile driver
        /// </summary>
        /// <param name="chiffre"></param>
        public void Accumuler(char chiffre) => mAccumuleur.Accumuler(chiffre);

        /// <summary>
        /// Method that duplicate the current state inside a new object
        /// </summary>
        /// <returns></returns>
        public IÉtatCalc Cloner() => new ÉtatCalc(EnTexte());

        /// <summary>
        /// Method that trim the last number inside the cumulator value
        /// </summary>
        public void Décumuler() => mAccumuleur.Décumuler();

        /// <summary>
        /// Method that return a the value of the cumulator or pile as a string instea of the class name
        /// </summary>
        /// <returns></returns>
        public override string ToString() => EnTexte();

        /// <summary>
        /// Mehods that return the correct value to the Dessus property
        /// </summary>
        /// <returns></returns>
        private int? DessusÉtat()
        {
            return (Accumulateur != null) ? Accumulateur : (Pile != null) ? mPile.Dessus : throw new PileVideException();
        }

        /// <summary>
        /// Method that add the cumulator value to the pile stack
        /// </summary>
        /// <param name="facultatif"></param>
        public void Enter(bool facultatif = false)
        {
            if (Accumulateur is null && !facultatif)
                throw new AccumuleurVideException();

            if (Accumulateur != null)
                mPile.Push((int)mAccumuleur.Extraire());
        }

        /// <summary>
        /// Method that return the content of the pile and cumulator as a string
        /// </summary>
        /// <param name="séparateur"></param>
        /// <returns></returns>
        public string EnTexte(string séparateur = "  ")
        {
            string pileContent = mPile.EnTexte(séparateur);
            string stateContent = (mAccumuleur.Accumulation != String.Empty) ? mAccumuleur.Accumulation + "?" : String.Empty;
            return (stateContent != String.Empty) ? String.Join(séparateur, pileContent, stateContent).Trim() : pileContent;
        }

        /// <summary>
        /// Method that remove either the cumulator value if its not empty of the last element added to the pile stack
        /// </summary>
        /// <returns></returns>
        public int Pop()
        {
            return (Accumulateur != null) ? (int)mAccumuleur.Extraire() : mPile.Pop();
        }

        /// <summary>
        /// Method that adds a new element to the pile stack
        /// </summary>
        /// <param name="nombre"></param>
        public void Push(int nombre)
        {
            if (Accumulateur != null)
                mPile.Push((int)mAccumuleur.Extraire());

            mPile.Push(nombre);
        }

        /// <summary>
        /// Method that reset the current state based on its parameters
        /// </summary>
        /// <param name="pile"></param>
        /// <param name="accumulateur"></param>
        public void Reset(IEnumerable<int> pile = null, int? accumulateur = null)
        {
            mAccumuleur.Reset(accumulateur);

            if (accumulateur > 0 || accumulateur is null)
                mPile.Reset(pile);
        }

        /// <summary>
        /// Method that reset the current state based on its parameters
        /// </summary>
        /// <param name="accumulateur"></param>
        /// <param name="pile"></param>
        public void Reset(int? accumulateur, params int[] pile)
        {
            mAccumuleur.Reset(accumulateur);
            mPile.Reset(pile);
        }

        /// <summary>
        /// Method that resets its current state based on his parameters
        /// </summary>
        /// <param name="pile"></param>
        /// <param name="acc"></param>
        public void Reset(IPileCalc pile, IAccumuleur acc = null)
        {
            mPile.Reset(pile.Éléments);
            mAccumuleur.Reset(acc.Valeur);
        }

        /// <summary>
        /// Method that resets the current state based on a string. The string is parse to extract all the data.
        /// </summary>
        /// <param name="enTexte"></param>
        public void Reset(string enTexte)
        {
            char[] separator = new char[] { ' ' };
            string[] strArray = enTexte.ParseStringToArray(separator);

            if (!IsStateStringOkay(strArray))
                throw new ArgumentException();

            Reset(ParsePile(strArray), ParseCumulator(strArray));
        }
    }

    public class ÉtatCalcUtils
    {
        /// <summary>
        /// Boolean method that verify that the string used to initiate/reset the state is okay. Return false if not
        /// </summary>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static bool IsStateStringOkay(string[] strArray)
        {
            if (strArray != null)
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    string element = strArray.GetValue(i).ToString();


                    if (element.Contains("?") && element.Contains("-"))
                        return false;
                    else if (element.Contains("?") && i == 0 && strArray.Length > 1)
                        return false;
                    else if (!element.IsNumberSyntaxOkay(true, new char[] { ',', '.' }))
                        return false;
                    else
                    {
                        int result;
                        Int32.TryParse(element.Trim(new char[]{',', '?'}), out result);
                        if (result == 0) return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Method that parses the pile stacks out of a string array. Returns an empty List<int> if the string array is null or empty
        /// </summary>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static IEnumerable<int> ParsePile(string[] strArray)
        {
            List<int> pileInt = new List<int>();

            if (strArray is null) return pileInt;

            for (int i = 0; i < strArray.Length; i++)
            {
                string element = strArray.GetValue(i).ToString();

                if (element.Contains("-"))
                    pileInt.Add(Convert.ToInt32(element));
                else if (element.IsNumberSyntaxOkay(nullAccepted: false))
                    pileInt.Add(Convert.ToInt32(element));
            }

            return pileInt;
        }

        /// <summary>
        /// Method that parses the cumulator value out of a string array. Return a null value if the string array is empty or null.
        /// </summary>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static int? ParseCumulator(string[] strArray)
        {
            int? cumulatorValue = null;

            if (strArray is null) return cumulatorValue;

            for (int i = 0; i < strArray.Length; i++)
            {
                string element = strArray.GetValue(i).ToString();

                if (element.Contains("?"))
                    cumulatorValue = Convert.ToInt32(element.Trim('?'));
            }

            return cumulatorValue;
        }
    }
}
