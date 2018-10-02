using SDD.Interface;
using System;
using static System.Console;
using static SDD.Utility.CharExtensions;
using static SDD.Utility.StringExtensions;
using static SDD.Utility.Utils;

namespace SDD.Class
{
    public class AccumuleurGen<T> : IAccumuleurGen<T>
        where T : struct
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public AccumuleurGen()
        {
            Accumulation = String.Empty;
        }

        /// <summary>
        /// Constructor that takes an initial accumulation value
        /// </summary>
        /// <param name="accumulation"></param>
        public AccumuleurGen(string accumulation)
        {
            if (accumulation.IsNumberSyntaxOkay(true)) Accumulation = accumulation;
            else throw new ArgumentException();
        }

        /// <summary>
        /// Constructor that takes an initial value for valeur
        /// </summary>
        /// <param name="valeur"></param>
        public AccumuleurGen(T? valeur)
        {
            if (valeur.ToString().Contains("-")) throw new ArgumentException();
            else Valeur = valeur;
        }

        /// <summary>
        /// Property that check is the current valeur is empty.
        /// </summary>
        public bool EstVide { get { return Valeur == null; } }

        /// <summary>
        /// Property that stores the value of the cumulator
        /// </summary>
        public T? Valeur {
            get;
            private set;
        }

        /// <summary>
        /// Property that return the string representation of the property valeur
        /// </summary>
        public string Accumulation
        {
            get {
                if (Valeur is null) return String.Empty;
                else return Convert.ToString(Valeur);
            }
            protected set {
                if (value == String.Empty || value is null) Valeur = null;
                else Valeur = (T)Convert.ChangeType(value, typeof(T)); 
            }
        }

        /// <summary>
        /// Method that stacks new number inside the property valeur
        /// </summary>
        /// <param name="chiffre"></param>
        public void Accumuler(char chiffre)
        {
            if (chiffre.IsNumber())
            {
                string tempString = Accumulation + chiffre.ToString();
                Accumulation = tempString;
            }
            else{
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Method that creates a clone of the current content inside this class 
        /// </summary>
        /// <returns></returns>
        public IAccumuleurGen<T> Cloner()
        {
            return new AccumuleurGen<T>(Valeur);
        }

        /// <summary>
        /// Method that remove the last number of the property of valeur
        /// </summary>
        public void Décumuler()
        {
            if (Accumulation != String.Empty)
            {
                string tempString = Accumulation.Substring(0, Accumulation.Length - 1);
                Accumulation = tempString;
            }
            else{
                throw new AccumuleurVideException();
            }
        }

        /// <summary>
        /// Method that extracts the current values store inside the property valeur and reset it to null
        /// </summary>
        /// <returns></returns>
        public T? Extraire()
        {
            T? accumulatorValue = Valeur;
            Valeur = null;
            return accumulatorValue;
        }

        /// <summary>
        /// Method that resets the value of valeur to be an empty string
        /// </summary>
        public void Reset()
        {
            Accumulation = String.Empty;
        }

        /// <summary>
        /// Method that resets the value of valeur based on the parameter accumulation
        /// </summary>
        /// <param name="accumulation"></param>
        public void Reset(string accumulation)
        {
            Reset((T)Convert.ChangeType(accumulation, typeof(T)), false);
        }

        /// <summary>
        /// Method that resets the value of valeur based on valeur parameter
        /// </summary>
        /// <param name="valeur"></param>
        public void Reset(T? valeur, bool negativeAccepted = false)
        {
            if (valeur.ToString().Contains("-") && !negativeAccepted)
                throw new ArgumentException();

            Valeur = valeur;
        }

        /// <summary>
        /// Override method that return the content of the property valeur
        /// </summary>
        /// <returns></returns>
        override public string ToString()
        {
            if (Valeur != null)
                return Valeur.ToString();
            else
                return String.Empty;
        }

    }
}
