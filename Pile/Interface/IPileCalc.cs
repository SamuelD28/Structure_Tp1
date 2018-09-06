using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDD.Interface
{
    /// <summary>
    /// Interface used for creating a new pile object
    /// </summary>
    public interface IPileCalc
    {
        IEnumerable<int> Éléments { get; }
        IEnumerable<int> ÉlémentsInversés { get; }
        int Dessus { get; }
        bool EstVide { get; }

        //Méthode non-modifiante
        IPileCalc Cloner();

        int Pop();
        void Push(int nombre);
        void Reset(IEnumerable<int> éléments = null);
    }

    /// <summary>
    /// Exception trowed when the pile is empty
    /// </summary>
    public class PileVideException : ApplicationException {
        public PileVideException() : base("Oops! The pile is empty".PadRight(35)) { }
    }

    /// <summary>
    /// Extensions methods that can be used on object that inherits the IPileCalc Interface
    /// </summary>
    public static class IPileCalcExtensions
    {
        /// <summary>
        /// Method that return a string format of all the numbers in the pile
        /// </summary>
        /// <param name="pile"></param>
        /// <param name="séparateur"></param>
        /// <returns>String representation of all the numbers in a pile</returns>
        public static string EnTexte(this IPileCalc pile, string séparateur = "  ")
        {
            if (pile.Éléments == null)
                return "Aucun élément dans la pile";
            else
                return String.Join(séparateur, pile.Éléments);
        }
    }
}
