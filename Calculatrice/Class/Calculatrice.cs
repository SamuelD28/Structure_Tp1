using SDD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDD.Class
{
    public class Calculatrice : ÉtatCalc, ICalculatrice
    {
        public Calculatrice(IPileCalc pile = null, IAccumuleur acc = null) : base(pile, acc) { }

        public Calculatrice(int? accumulateur, params int[] pile) : base(accumulateur, pile) { }

        public Calculatrice(IEnumerable<int> pile, int? accumulateur = null) : base(pile, accumulateur) { }

        public Calculatrice(string enTexte) : base(enTexte) { }

        public string Accumulation => mAccumuleur.Accumulation;

        public IEnumerable<object> Éléments => Pile.Cast<object>().ToList();

        public object Résultat => Dessus;

        public new ICalculatrice Cloner() => throw new NotImplementedException();

        public new string EnTexte(string séparateur = "  ") => throw new NotImplementedException();

        public void Exécuter(IEnumerable<CalcCommande> commandes)
        {
            throw new NotImplementedException();
        }

        public void Exécuter(params CalcCommande[] commandes)
        {
            throw new NotImplementedException();
        }

        public void Exécuter(string commandes)
        {
            throw new NotImplementedException();
        }

        public bool PeutExécuter(CalcCommande commande) => true;

        public new void Reset(string enTexte = "") => Reset(enTexte);
    }
}
