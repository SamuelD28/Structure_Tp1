using Calculatrice.Interface;
using SDD.Interface;
using System;
using System.Collections.Generic;

namespace Calculatrice.Class
{
	class ÉtatCalc : IÉtatCalc
    {
        //Constructors//
        public ÉtatCalc(IPileCalc pile = null, IAccumuleur acc = null)
        {
			mAccumuleur = acc;
			mPile = pile;
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

		//Private Properties of the class
		private IAccumuleur mAccumuleur { get; set; }

		private IPileCalc mPile { get; set; }

		//Public Properties of the class
		public int? Accumulateur => mAccumuleur.Valeur;

        public IEnumerable<int> Pile => mPile.ÉlémentsInversés;

		public bool EstVide => throw new NotImplementedException();

		//A reverifier. Possibilite de changer Dessus pour un type int nullable
		public int Dessus =>
			(mAccumuleur.Valeur != null)? (int)mAccumuleur.Valeur :
			(mPile.Dessus != 0)? mPile.Dessus:
			throw new PileVideException();

		//Methods inherited from the accumulator and pile driver
		public void Accumuler(char chiffre) => mAccumuleur.Accumuler(chiffre);

        public IÉtatCalc Cloner()
        {
			return new ÉtatCalc(mPile, mAccumuleur);
		}

		public void Décumuler() => mAccumuleur.Décumuler();

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
			if (mAccumuleur.Valeur != null)
				return (int)mAccumuleur.Extraire();
			else
				return mPile.Pop();
        }

        public void Push(int nombre)
        {
			if (mAccumuleur.Valeur != null)
				mPile.Push((int)mAccumuleur.Extraire());

			mPile.Push(nombre);
        }

        public void Reset(IEnumerable<int> pile = null, int? accumulateur = null)
        {

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
