using SDD.Interface;
using System;
using System.Collections.Generic;

namespace SDD.Class
{
    public class Calculatrice : ÉtatCalc, ICalculatrice
    {
        public Calculatrice(IPileCalc pile = null, IAccumuleur acc = null)
        {

        }

        public Calculatrice(int? accumulateur, params int[] pile)
        {

        }

        public Calculatrice(IEnumerable<int> pile, int? accumulateur = null)
        {

        }

        public Calculatrice(string enTexte)
        {

        }

        public string Accumulation => throw new NotImplementedException();

        public IEnumerable<object> Éléments => throw new NotImplementedException();

        public object Résultat => throw new NotImplementedException();

        public new ICalculatrice Cloner()
        {
            throw new NotImplementedException();
        }

        public new string EnTexte(string séparateur = "  ")
        {
            throw new NotImplementedException();
        }

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

        public bool PeutExécuter(CalcCommande commande)
        {
            throw new NotImplementedException();
        }

        public new void Reset(string enTexte = "")
        {
            throw new NotImplementedException();
        }
    }
}
