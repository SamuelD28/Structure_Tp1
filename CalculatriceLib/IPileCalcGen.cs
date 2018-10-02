using System;
using System.Collections.Generic;

namespace SDD.Interface
{
	/// <summary>
	/// Interface used for creating a new pile object
	/// </summary>
	public interface IPileCalcGen<T>
    {
        IEnumerable<T> Éléments { get;}
        IEnumerable<T> ÉlémentsInversés { get;}
        T Dessus { get; }
        bool EstVide { get; }

        //Méthode non-modifiante
        IPileCalcGen<T> Cloner();

        T Pop();
        void Push(T nombre);
        void Reset(IEnumerable<T> éléments = null);
    }

    /// <summary>
    /// Extensions methods that can be used on object that inherits the IPileCalc Interface
    /// </summary>
    public static class IPileCalcGenExtensions
    {
        /// <summary>
        /// Method that return a string format of all the numbers in the pile
        /// </summary>
        /// <param name="pile"></param>
        /// <param name="séparateur"></param>
        /// <returns>String representation of all the numbers in a pile</returns>
        public static string EnTexte<T>(this IPileCalcGen<T> pile, string séparateur = "  ")
        {
            if (pile.Éléments == null)
                return "Aucun élément dans la pile";
            else
                return String.Join(séparateur, pile.Éléments);
        }
    }
}
