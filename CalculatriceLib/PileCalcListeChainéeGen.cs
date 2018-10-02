using SDD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDD.Class
{
    public class PileCalcListeChainéeGen<T> : IPileCalcGen<T>
    {
        /// <summary>
        /// Cosntructor that can take an initial value of ListÉlément property
        /// </summary>
        /// <param name="listeÉléments"></param>
        public PileCalcListeChainéeGen(IEnumerable<T> listeÉléments = null)
        {
			ListeÉléments = new LinkedList<T>();

			if (listeÉléments != null)
				ListeÉléments = new LinkedList<T>(listeÉléments);
        }

        /// <summary>
        /// Property that stores a list of int value passed to this class
        /// </summary>
        internal LinkedList<T> ListeÉléments
        {
            get;
            set;
        }

        /// <summary>
        /// Property that returns the values stored inside the ListeÉlément property
        /// </summary>
        public IEnumerable<T> Éléments
        {
            get
            {
				return ListeÉléments.ReturnAllNode().Reverse();
            }
        }

        /// <summary>
        /// Property that reutrns the values stored inside ListÉlément but in the reverse order
        /// </summary>
        public IEnumerable<T> ÉlémentsInversés
        {
            get
            {
				return ListeÉléments.ReturnAllNode();
            }
        }

        /// <summary>
        /// Property that returns the value stored last inside the property ListeÉlément
        /// </summary>
        public T Dessus
        {
            get
            {
                return (ListeÉléments.Premier != null) ? ListeÉléments.Premier.Valeur : throw new PileVideException();
            }
        }

        /// <summary>
        /// Property that check if the property ListeÉlément is empty
        /// </summary>
        public bool EstVide
        {
            get
            {
                return ListeÉléments.Premier is null;
            }
        }

        /// <summary>
        /// Method that returns a clone of the current values stored inside of this class
        /// </summary>
        /// <returns></returns>
        public IPileCalcGen<T> Cloner()
        {
            PileCalcListeChainéeGen<T> tempPile = new PileCalcListeChainéeGen<T>();

			if(ListeÉléments != null)
				tempPile.ListeÉléments = new LinkedList<T>(ListeÉléments.Premier);

            return tempPile;
        }

        /// <summary>
        /// Method that remove the last number inserted in the pile. Display an error if the pile is currently empty
        /// </summary>
        public T Pop()
        {
            T lastElement = Dessus;
            ListeÉléments.RemoveFirst();
            return lastElement;
        }

        /// <summary>
        /// Method that push a new number inside an IEnumerable élément.
        /// </summary>
        /// <param name="nombre"></param>
        public void Push(T nombre)
        {
            ListeÉléments.AddFirst(nombre);
        }

        /// <summary>
        /// Method that resets the numbers contain within the Élément IEnumerable
        /// </summary>
        /// <param name="éléments"></param>
        public void Reset(IEnumerable<T> éléments = null)
        {
            ListeÉléments.Clear();

			if (éléments != null)
				ListeÉléments = new LinkedList<T>(éléments);
        }
    }
}
