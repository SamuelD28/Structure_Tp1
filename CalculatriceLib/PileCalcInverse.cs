using SDD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDD.Class
{
    public class PileCalcListeInverse : IPileCalc
    {
        /// <summary>
        /// Cosntructor that can take an initial value of ListÉlément property
        /// </summary>
        /// <param name="mPile.ListeÉléments"></param>
        public PileCalcListeInverse(IEnumerable<int> listeÉléments = null)
        {
            ListeÉléments = new List<int>();

            if (listeÉléments != null)
                ListeÉléments.AddRange(listeÉléments);
        }


        /// <summary>
        /// Property that stores a list of int value passed to this class
        /// </summary>
        public List<int> ListeÉléments {
            get;
            set;
        }

        /// <summary>
        /// Property that returns the values stored inside the ListeÉlément property
        /// </summary>
        public IEnumerable<int> Éléments {
            get
            {
                return ListeÉléments;
            }
        }

        /// <summary>
        /// Property that reutrns the values stored inside ListÉlément but in the reverse order
        /// </summary>
        public IEnumerable<int> ÉlémentsInversés
        {
            get
            {
                return ListeÉléments.AsEnumerable().Reverse();
            }
        }

        /// <summary>
        /// Property that returns the value stored last inside the property ListeÉlément
        /// </summary>
        public int Dessus
        {
            get
            {
                return (ListeÉléments.Count > 0) ? ListeÉléments[ListeÉléments.Count - 1] : throw new PileVideException();
            }
        }

        public int Dessous
        {
            get
            {
                return (ListeÉléments.Count > 0) ? ListeÉléments[0] : throw new PileVideException();
            }
        }

        /// <summary>
        /// Property that check if the property ListeÉlément is empty
        /// </summary>
        public bool EstVide { get { return ListeÉléments.Count == 0; } }

        /// <summary>
        /// Method that returns a clone of the current values stored inside of this class
        /// </summary>
        /// <returns></returns>
        public IPileCalc Cloner()
        {
            return new PileCalcListeInverse(Éléments);
        }

        /// <summary>
        /// Method that remove the last number inserted in the pile. Display an error if the pile is currently empty
        /// </summary>
        public int Pop()
        {
            int lastElement = Dessus;
            List<int> tempList = ÉlémentsInversés.ToList();
            tempList.RemoveAt(0);
            ListeÉléments = tempList.Reverse<int>().ToList();
            return lastElement;
        }

        /// <summary>
        /// Method that push a new number inside an IEnumerable élément.
        /// </summary>
        /// <param name="nombre"></param>
        public void Push(int nombre)
        {
            List<int> tempList = ÉlémentsInversés.ToList();
            tempList.Insert(0, nombre);
            ListeÉléments = tempList.Reverse<int>().ToList();
        }

        /// <summary>
        /// Method that resets the numbers contain within the Élément IEnumerable
        /// </summary>
        /// <param name="éléments"></param>
        public void Reset(IEnumerable<int> éléments = null)
        {
            ListeÉléments.Clear();

            if (éléments != null)
                ListeÉléments.AddRange(éléments);
        }
    }
}
