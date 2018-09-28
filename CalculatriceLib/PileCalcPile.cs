using SDD.Interface;
using System.Collections.Generic;
using System.Linq;

namespace SDD.Class
{
    public class PileCalcPile : IPileCalc
    {
        /// <summary>
        /// Cosntructor that can take an initial value of ListÉlément property
        /// </summary>
        /// <param name="listeÉléments"></param>
        public PileCalcPile(IEnumerable<int> listeÉléments = null)
        {
            ListeÉléments = (listeÉléments is null) ? new Stack<int>() : new Stack<int>(listeÉléments);
        }

        /// <summary>
        /// Property that stores a list of int value passed to this class
        /// </summary>
        internal Stack<int> ListeÉléments {
            get;
            set;
        }

        /// <summary>
        /// Property that returns the values stored inside the ListeÉlément property
        /// </summary>
        public IEnumerable<int> Éléments {
            get
            {
                return ListeÉléments.Reverse();
            }
        }

        /// <summary>
        /// Property that reutrns the values stored inside ListÉlément but in the reverse order
        /// </summary>
        public IEnumerable<int> ÉlémentsInversés
        {
            get
            {
                return ListeÉléments;
            }
        }

        /// <summary>
        /// Property that returns the value stored last inside the property ListeÉlément
        /// </summary>
        public int Dessus
        {
            get
            {
                return (ListeÉléments.Count > 0) ? ListeÉléments.Peek() : throw new PileVideException();
            }
        }

        /// <summary>
        /// Property that check if the property ListeÉlément is empty
        /// </summary>
        public bool EstVide {
            get
            {
                return ListeÉléments.Count == 0;
            }
        }

        /// <summary>
        /// Method that returns a clone of the current values stored inside of this class
        /// </summary>
        /// <returns></returns>
        public IPileCalc Cloner()
        {
            return new PileCalcPile(Éléments);
        }

        /// <summary>
        /// Method that remove the last number inserted in the pile. Display an error if the pile is currently empty
        /// </summary>
        public int Pop() => (ListeÉléments.Count > 0) ? ListeÉléments.Pop(): throw new PileVideException();

        /// <summary>
        /// Method that push a new number inside an IEnumerable élément.
        /// </summary>
        /// <param name="nombre"></param>
        public void Push(int nombre) => ListeÉléments.Push(nombre);

        /// <summary>
        /// Method that resets the numbers contain within the Élément IEnumerable
        /// </summary>
        /// <param name="éléments"></param>
        public void Reset(IEnumerable<int> éléments = null)
        {
            ListeÉléments.Clear();
            if (éléments != null)
                ListeÉléments = new Stack<int>(éléments);
        }
    }
}
