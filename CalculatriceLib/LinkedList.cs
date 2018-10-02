using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDD.Class
{
    public class Node<T>
    {
        public Node<T> Suivant;
        public T Valeur;
    }

    public class LinkedList<T>
    {
        public LinkedList()
        {

        }

        public LinkedList(Node<T> premier)
        {
            Premier = premier;
        }

        public LinkedList(IEnumerable<T> enumerable)
        {
            for (int i = 0; i < enumerable.Count(); i++)
            {
                T courant = enumerable.ElementAt(i);
                AddFirst(courant);
            }
        }

        public Node<T> Premier;
        public Node<T> Dernier;

        public IEnumerable<T> ReturnAllNode()
        {
            Node<T> courante = Premier;
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

        public void AddFirst(T valeur)
        {
            Node<T> noeud = new Node<T>();
            noeud.Valeur = valeur;
            noeud.Suivant = Premier;
            Premier = noeud;

            if (noeud.Suivant is null)
                Dernier = noeud;
        }

        public void AddLast(T valeur)
        {
            if (Premier == null)
            {
                Premier = new Node<T>();

                Premier.Valeur = valeur;
                Premier.Suivant = null;
            }
            else
            {
                Node<T> noeud = new Node<T>();
                noeud.Valeur = valeur;

                Node<T> courante = Premier;
                while (courante.Suivant != null)
                {
                    courante = courante.Suivant;
                }

                courante.Suivant = noeud;
            }
        }
    }
}
