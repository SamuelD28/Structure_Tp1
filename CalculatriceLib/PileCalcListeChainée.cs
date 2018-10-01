using SDD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDD.Class
{
    public class PileCalcListeChainée : IPileCalc
    {
        /// <summary>
        /// Cosntructor that can take an initial value of ListÉlément property
        /// </summary>
        /// <param name="listeÉléments"></param>
        public PileCalcListeChainée(IEnumerable<int> listeÉléments = null)
        {
			ListeÉléments = new LinkedList();

			if (listeÉléments != null)
				ListeÉléments = new LinkedList(listeÉléments);
        }

        /// <summary>
        /// Property that stores a list of int value passed to this class
        /// </summary>
        internal LinkedList ListeÉléments
        {
            get;
            set;
        }

        /// <summary>
        /// Property that returns the values stored inside the ListeÉlément property
        /// </summary>
        public IEnumerable<int> Éléments
        {
            get
            {
				return ListeÉléments.ReturnAllNode().Reverse();
            }
        }

        /// <summary>
        /// Property that reutrns the values stored inside ListÉlément but in the reverse order
        /// </summary>
        public IEnumerable<int> ÉlémentsInversés
        {
            get
            {
				return ListeÉléments.ReturnAllNode();
            }
        }

        /// <summary>
        /// Property that returns the value stored last inside the property ListeÉlément
        /// </summary>
        public int Dessus
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
        public IPileCalc Cloner()
        {
			PileCalcListeChainée tempPile = new PileCalcListeChainée();

			if(ListeÉléments != null)
				tempPile.ListeÉléments = new LinkedList(ListeÉléments.Premier);

            return tempPile;
        }

        /// <summary>
        /// Method that remove the last number inserted in the pile. Display an error if the pile is currently empty
        /// </summary>
        public int Pop()
        {
            int lastElement = Dessus;
            ListeÉléments.RemoveFirst();
            return lastElement;
        }

        /// <summary>
        /// Method that push a new number inside an IEnumerable élément.
        /// </summary>
        /// <param name="nombre"></param>
        public void Push(int nombre)
        {
            ListeÉléments.AddFirst(nombre);
        }

        /// <summary>
        /// Method that resets the numbers contain within the Élément IEnumerable
        /// </summary>
        /// <param name="éléments"></param>
        public void Reset(IEnumerable<int> éléments = null)
        {
            ListeÉléments.Clear();

			if (éléments != null)
				ListeÉléments = new LinkedList(éléments);
        }
    }

	public class Node
	{
		public Node Suivant;
		public int Valeur;
	}

	public class LinkedList
	{
		public LinkedList()
		{

		}

		public LinkedList(Node premier)
		{
			Premier = premier;
		}

		public LinkedList(IEnumerable<int> enumerable)
		{
			for (int i = 0; i < enumerable.Count(); i++)
			{
				int courant = enumerable.ElementAt(i);
				AddFirst(courant);
			}
		}

		public Node Premier;
		public Node Dernier;

		public IEnumerable<int> ReturnAllNode()
		{
			Node courante = Premier;
			while (courante != null)
			{
				yield return courante.Valeur;
				courante = courante.Suivant;
			}
		}

		public void Clear()
		{
			Premier = null;
		}

		public void RemoveFirst()
		{
			Premier = Premier.Suivant;
		}

		public void AddFirst(int valeur)
		{
			Node noeud = new Node();
			noeud.Valeur = valeur;
			noeud.Suivant = Premier;
			Premier = noeud;

			if (noeud.Suivant is null)
				Dernier = noeud;
		}

		public void AddLast(int valeur)
		{
			if (Premier == null)
			{
				Premier = new Node();

				Premier.Valeur = valeur;
				Premier.Suivant = null;
			}
			else
			{
				Node noeud = new Node();
				noeud.Valeur = valeur;

				Node courante = Premier;
				while (courante.Suivant != null)
				{
					courante = courante.Suivant;
				}

				courante.Suivant = noeud;
			}
		}
	}
}
