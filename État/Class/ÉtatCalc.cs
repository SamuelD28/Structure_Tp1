using Calculatrice.Interface;
using SDD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatrice.Class
{
    class ÉtatCalc : IÉtatCalc
    {
        //Constructors//
        public ÉtatCalc(IPileCalc pile = null, IAccumuleur acc = null)
        {

        }

        public ÉtatCalc(int? accumulateur, params int[] pile)
        {

        }

        public ÉtatCalc(IEnumerable<int> pile, int? accumulateur = null)
        {

        }

        public ÉtatCalc(string enTexte)
        {

        }


        public int? Accumulateur => throw new NotImplementedException();

        public IEnumerable<int> Pile => throw new NotImplementedException();

        public bool EstVide => throw new NotImplementedException();

        public int Dessus => throw new NotImplementedException();

        public void Accumuler(char chiffre)
        {
            throw new NotImplementedException();
        }

        public IÉtatCalc Cloner()
        {
            throw new NotImplementedException();
        }

        public void Décumuler()
        {
            throw new NotImplementedException();
        }

        public void Enter(bool facultatif = false)
        {
            throw new NotImplementedException();
        }

        public string EnTexte(string séparateur = "  ")
        {
            throw new NotImplementedException();
        }

        public int Pop()
        {
            throw new NotImplementedException();
        }

        public void Push(int nombre)
        {
            throw new NotImplementedException();
        }

        public void Reset(IEnumerable<int> pile = null, int? accumulateur = null)
        {
            throw new NotImplementedException();
        }

        public void Reset(int? accumulateur, params int[] pile)
        {
            throw new NotImplementedException();
        }

        public void Reset(IPileCalc pile, IAccumuleur acc = null)
        {
            throw new NotImplementedException();
        }

        public void Reset(string enTexte)
        {
            throw new NotImplementedException();
        }
    }
}
