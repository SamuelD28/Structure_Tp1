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
            ListeÉléments = new List<Noeud>();

            if (listeÉléments != null)
                ListeÉléments.AddRange(listeÉléments.ParseListeNoeud());
        }

        /// <summary>
        /// Property that stores a list of int value passed to this class
        /// </summary>
        internal List<Noeud> ListeÉléments
        {
            get;
            set;
        }

        private Noeud racine
        {
            get
            {
                return (ListeÉléments.Count > 0) ? ListeÉléments.ElementAt(0) : null;
            }
            set
            {
                racine = value;
            }
        }

        /// <summary>
        /// Property that returns the values stored inside the ListeÉlément property
        /// </summary>
        public IEnumerable<int> Éléments
        {
            get
            {
                foreach (Noeud noeud in ListeÉléments)
                {
                    yield return noeud.Valeur;
                }
            }
        }

        /// <summary>
        /// Property that reutrns the values stored inside ListÉlément but in the reverse order
        /// </summary>
        public IEnumerable<int> ÉlémentsInversés
        {
            get
            {
                foreach (Noeud noeud in ListeÉléments.AsEnumerable().Reverse())
                {
                    yield return noeud.Valeur;
                }
            }
        }

        /// <summary>
        /// Property that returns the value stored last inside the property ListeÉlément
        /// </summary>
        public int Dessus
        {
            get
            {
                return (ListeÉléments.Count > 0) ? (int)ListeÉléments[ListeÉléments.Count - 1].Valeur : throw new PileVideException();
            }
        }

        /// <summary>
        /// Property that check if the property ListeÉlément is empty
        /// </summary>
        public bool EstVide
        {
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
            return new PileCalcListeChainée(Éléments);
        }

        /// <summary>
        /// Method that remove the last number inserted in the pile. Display an error if the pile is currently empty
        /// </summary>
        public int Pop()
        {
            int lastElement = Dessus;
            ListeÉléments.RemoveAt(ListeÉléments.Count - 1);
            return lastElement;
        }

        /// <summary>
        /// Method that push a new number inside an IEnumerable élément.
        /// </summary>
        /// <param name="nombre"></param>
        public void Push(int nombre)
        {
            Noeud noeudCourant = new Noeud(nombre);
            ListeÉléments.Add(noeudCourant);
        }

        /// <summary>
        /// Method that resets the numbers contain within the Élément IEnumerable
        /// </summary>
        /// <param name="éléments"></param>
        public void Reset(IEnumerable<int> éléments = null)
        {
            ListeÉléments.Clear();

            if (éléments != null)
                ListeÉléments.AddRange(éléments.ParseListeNoeud());
        }
    }

    public static class ListExtensions
    {
        public static List<Noeud> ParseListeNoeud(this IEnumerable<int> enumerable)
        {
            List<Noeud> listeNoeuds = new List<Noeud>();
            for (int i = 0; i < enumerable.Count(); i++)
            {
                Noeud noeudSuivant;

                if (i + 1 < enumerable.Count())
                    noeudSuivant = new Noeud(enumerable.ElementAt(i + 1));
                else
                    noeudSuivant = null;

                Noeud noeudCourant = new Noeud(enumerable.ElementAt(i), noeudSuivant);
                listeNoeuds.Add(noeudCourant);
            }
            return listeNoeuds;
        }
    }
}
