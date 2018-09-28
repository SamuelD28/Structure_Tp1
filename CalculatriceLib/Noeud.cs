using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDD.Class
{
    public class Noeud
    {
        public Noeud(int valeur, Noeud suivant = null)
        {
            Valeur = valeur;
            Suivant = suivant;
        }
        public int Valeur { get;}
        public Noeud Suivant { get;}
    }
}
