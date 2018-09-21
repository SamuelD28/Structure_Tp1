using SDD.Interface;
using System;
using static System.Console;
using static SDD.Utility.CharExtensions;
using static SDD.Utility.StringExtensions;
using static SDD.Utility.Utils;

namespace SDD.Class
{
    public class Accumuleur : IAccumuleur
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Accumuleur()
        {
            Accumulation = String.Empty;
        }

        /// <summary>
        /// Constructor that takes an initial accumulation value
        /// </summary>
        /// <param name="accumulation"></param>
        public Accumuleur(string accumulation)
        {
            if (accumulation.IsNumberSyntaxOkay(true)) Accumulation = accumulation;
            else throw new ArgumentException();
        }

        /// <summary>
        /// Constructor that takes an initial value for valeur
        /// </summary>
        /// <param name="valeur"></param>
        public Accumuleur(int? valeur)
        {
            if (valeur < 0) throw new ArgumentException();
            else Valeur = valeur;
        }

        /// <summary>
        /// Property that check is the current valeur is empty.
        /// </summary>
        public bool EstVide { get { return Valeur == null; } }

        /// <summary>
        /// Property that stores the value of the cumulator
        /// </summary>
        public int? Valeur {
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
                if (value == String.Empty || value == null) Valeur = null;
                else Valeur = Convert.ToInt32(value); 
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
        public IAccumuleur Cloner()
        {
            return new Accumuleur(Valeur);
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
        public int? Extraire()
        {
            int? accumulatorValue = Valeur;
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
            if (accumulation.IsNumberSyntaxOkay(true))
                Accumulation = accumulation;
            else
                throw new ArgumentException();
        }

        /// <summary>
        /// Method that resets the value of valeur based on valeur parameter
        /// </summary>
        /// <param name="valeur"></param>
        public void Reset(int? valeur, bool negativeAccepted = false)
        {
            if (valeur < 0 && !negativeAccepted)
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
