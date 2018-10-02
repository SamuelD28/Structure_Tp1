using SDD.Interface;
using System;
using System.Collections.Generic;
using static SDD.Utility.StringExtensions;
using static SDD.Class.ÉtatCalcGenUtils;
using static SDD.Class.ÉtatCalcUtils;

namespace SDD.Class
{
    public class ÉtatCalcGen<T> : IÉtatCalcGen<T>
        where T : struct
    {
        /// <summary>
        /// Constructor that takes a PileCalcList and a Accumulator as parameters
        /// </summary>
        /// <param name="pile">Set to null by default</param>
        /// <param name="acc">Set to null by default</param>
        public ÉtatCalcGen(IPileCalcGen<T> pile = null, IAccumuleurGen<T> acc = null)
        {
            mAccumuleur = (acc != null) ? (AccumuleurGen<T>)acc : new AccumuleurGen<T>();
            mPile = (pile != null) ? pile : new PileCalcListeGen<T>();
        }

        /// <summary>
        /// Constructor that takes an initial value for the cumulator and a initial stack of value for the pile
        /// </summary>
        /// <param name="accumulateur"></param>
        /// <param name="pile"></param>
        public ÉtatCalcGen(T? accumulateur, params T[] pile)
        {
            mPile = (pile != null) ? new PileCalcListeGen<T>(pile) : new PileCalcListeGen<T>();
            mAccumuleur = (accumulateur != null) ? new AccumuleurGen<T>(accumulateur) : new AccumuleurGen<T>();
        }

        /// <summary>
        /// Constructor that takes an initial value for the cumulator and an IEnnumerable for the pile stack
        /// </summary>
        /// <param name="pile"></param>
        /// <param name="accumulateur"></param>
        public ÉtatCalcGen(IEnumerable<T> pile, T? accumulateur = null)
        {
            mPile = new PileCalcListeGen<T>(pile);
            mAccumuleur = (accumulateur != null) ? new AccumuleurGen<T>(accumulateur) : new AccumuleurGen<T>();
        }

        /// <summary>
        /// Constructor that can initialise the pile and cumulator by parsing a string passed as an argument
        /// </summary>
        /// <param name="enTexte"></param>
        public ÉtatCalcGen(string enTexte)
        {
            char[] separator = new char[] { ' ' };
            string[] strArray = enTexte.ParseStringToArray(separator);

            if (!IsStateStringOkay(strArray))
                throw new ArgumentException();

            mPile = new PileCalcListeGen<T>(ParsePile<T>(strArray));
            mAccumuleur = new AccumuleurGen<T>(ParseCumulator<T>(strArray));
        }

        /// <summary>
        /// Cumulator Driver
        /// </summary>
        protected AccumuleurGen<T> mAccumuleur { get; set; }

        /// <summary>
        /// Pile Driver
        /// </summary>
        protected IPileCalcGen<T> mPile { get; set; }

        /// <summary>
        /// Contains the cumulator value
        /// </summary>
        public T? Accumulateur => mAccumuleur.Valeur;

        /// <summary>
        /// Contains the stack of value inside the pile driver
        /// </summary>
        public IEnumerable<T> Pile => mPile.Éléments;

        /// <summary>
        /// Indicate wheter the state is empty by checking inside the pile and cumulator driver
        /// </summary>
        public bool EstVide => (mAccumuleur.EstVide && mPile.EstVide);

        /// <summary>
        /// Returns the cumulator value or the first element inside the pile stack
        /// </summary>
        public T? Dessus => (Accumulateur != null) ? Accumulateur: (Pile != null) ? mPile.Dessus : throw new PileVideException();

        /// <summary>
        /// Methods that stack a new number inside thye cumulator value
        /// </summary>
        /// <param name="chiffre"></param>
        public void Accumuler(char chiffre) => mAccumuleur.Accumuler(chiffre);

        /// <summary>
        /// Method that duplicate the current state inside a new object
        /// </summary>
        /// <returns></returns>
        public IÉtatCalcGen<T> Cloner() => new ÉtatCalcGen<T>(EnTexte());

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
        /// Method that add the cumulator value to the pile stack
        /// </summary>
        /// <param name="facultatif"></param>
        public void Enter(bool facultatif = false)
        {
            if (Accumulateur is null && !facultatif)
                throw new 
                    AccumuleurVideException();

            if (Accumulateur != null)
                mPile.Push((T)mAccumuleur.Extraire());
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
        public T Pop()
        {
            return (Accumulateur != null) ? (T)mAccumuleur.Extraire() : mPile.Pop();
        }

        /// <summary>
        /// Method that adds a new element to the pile stack
        /// </summary>
        /// <param name="nombre"></param>
        public void Push(T? nombre = null)
        {
            if (Accumulateur != null)
                mPile.Push((T)mAccumuleur.Extraire());

			if(nombre != null)
				mPile.Push((T)nombre);
        }

        /// <summary>
        /// Method that reset the current state based on its parameters
        /// </summary>
        /// <param name="pile"></param>
        /// <param name="accumulateur"></param>
        public void Reset(IEnumerable<T> pile = null, T? accumulateur = null)
        {
            mAccumuleur.Reset(accumulateur);

            if (accumulateur.ToString().Contains("-") || accumulateur is null)
                mPile.Reset(pile);
        }

        /// <summary>
        /// Method that reset the current state based on its parameters
        /// </summary>
        /// <param name="accumulateur"></param>
        /// <param name="pile"></param>
        public void Reset(T? accumulateur, params T[] pile)
        {
            mAccumuleur.Reset(accumulateur);
            mPile.Reset(pile);
        }

        /// <summary>
        /// Method that resets its current state based on his parameters
        /// </summary>
        /// <param name="pile"></param>
        /// <param name="acc"></param>
        public void Reset(IPileCalcGen<T> pile, IAccumuleurGen<T> acc = null)
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

            Reset(ParsePile<T>(strArray), ParseCumulator<T>(strArray));
        }
    }

    public class ÉtatCalcGenUtils
    {
        /// <summary>
        /// Method that parses the pile stacks out of a string array. Returns an empty List<int> if the string array is null or empty
        /// </summary>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static IEnumerable<T> ParsePile<T>(string[] strArray)
        {
            List<T> pileInt = new List<T>();

            if (strArray is null) return pileInt;

            for (int i = 0; i < strArray.Length; i++)
            {
                string element = strArray.GetValue(i).ToString();

                if (element.Contains("-"))
                    pileInt.Add((T)Convert.ChangeType(element, typeof(T)));
                else if (element.IsNumberSyntaxOkay(nullAccepted: false))
                    pileInt.Add((T)Convert.ChangeType(element, typeof(T)));
            }

            return pileInt;
        }

        /// <summary>
        /// Method that parses the cumulator value out of a string array. Return a null value if the string array is empty or null.
        /// </summary>
        /// <param name="strArray"></param>
        /// <returns></returns>
        public static T ParseCumulator<T>(string[] strArray)
        {
            T cumulatorValue = default(T);

            if (strArray is null) return cumulatorValue;

            for (int i = 0; i < strArray.Length; i++)
            {
                string element = strArray.GetValue(i).ToString();

                if (element.Contains("?"))
                    cumulatorValue = (T)Convert.ChangeType(element.Trim('?'), typeof(T));
            }

            return cumulatorValue;
        }
    }
}
