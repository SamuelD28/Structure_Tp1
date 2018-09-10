using SDD.Interface;
using System;
using System.Collections.Generic;
using static SDD.Utility.StringExtensions;

namespace SDD.Class
{
	public class ÉtatCalc : IÉtatCalc
    {
        //Constructors//
        public ÉtatCalc(IPileCalc pile = null, IAccumuleur acc = null)
        {
            if (acc != null)
                mAccumuleur = (Accumuleur)acc;
            else
                mAccumuleur = new Accumuleur();

            if (pile != null)
                mPile = (PileCalcListe)pile;
            else
                mPile = new PileCalcListe();
        }

        public ÉtatCalc(int? accumulateur, params int[] pile)
        {
            if (pile != null)
                mPile = new PileCalcListe(pile);
            else
                mPile = new PileCalcListe();

            if (accumulateur != null)
                mAccumuleur = new Accumuleur(accumulateur);
            else
                mAccumuleur = new Accumuleur();
        }

        public ÉtatCalc(IEnumerable<int> pile, int? accumulateur = null)
        {
            mPile = new PileCalcListe(pile);

            if (accumulateur != null)
                mAccumuleur = new Accumuleur(accumulateur);
            else
                mAccumuleur = new Accumuleur();
        }

        public ÉtatCalc(string enTexte)
        {
			if (!enTexte.IsStateStringOkay())
				throw new ArgumentException();

            mPile = new PileCalcListe(enTexte.ParsePile());
            mAccumuleur = new Accumuleur(enTexte.ParseCumulator());
        }

		//Private Properties of the class//
		private Accumuleur mAccumuleur { get; set; }

		private PileCalcListe mPile { get; set; }

		public int? Accumulateur => mAccumuleur.Valeur;

        public IEnumerable<int> Pile => mPile.Éléments;

        public bool EstVide => (mAccumuleur.EstVide && mPile.EstVide) ? true : false;

		//A reverifier. Possibilite de changer Dessus pour un type int nullable
		public int Dessus =>
			(mAccumuleur.Valeur != null)? (int)mAccumuleur.Valeur :
			(mPile.Dessus != 0)? mPile.Dessus:
			throw new PileVideException();

		//Methods inherited from the accumulator and pile driver
		public void Accumuler(char chiffre) => mAccumuleur.Accumuler(chiffre);

        public IÉtatCalc Cloner()
        {
			return new ÉtatCalc(EnTexte());
		}

		public void Décumuler() => mAccumuleur.Décumuler();

        public void Enter(bool facultatif = false)
        {
                if (Accumulateur is null && !facultatif)
                    throw new AccumuleurVideException();

				if(Accumulateur  != null)
					mPile.Push((int)mAccumuleur.Extraire());
        }

        public string EnTexte(string séparateur = "  ")
        {
            string pileContent = mPile.EnTexte(séparateur);
            string stateContent = (mAccumuleur.Accumulation != String.Empty) ?mAccumuleur.Accumulation + "?" : String.Empty;
            return (stateContent != String.Empty) ? String.Join(séparateur, pileContent, stateContent).Trim() : pileContent;
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
			mAccumuleur.Reset(accumulateur);

			if(accumulateur > 0 || accumulateur is null)
				mPile.Reset(pile);
        }

        public void Reset(int? accumulateur, params int[] pile)
        {
			mAccumuleur.Reset(accumulateur);
			mPile.Reset(pile);
        }

        public void Reset(IPileCalc pile, IAccumuleur acc = null)
        {
			mPile.Reset(pile.Éléments);
			mAccumuleur.Reset(acc.Valeur);
        }

        public void Reset(string enTexte)
        {
			if (!enTexte.IsStateStringOkay())
				throw new ArgumentException();

			Reset(enTexte.ParsePile(), enTexte.ParseCumulator());
        }

        public override string ToString() => EnTexte();
    }

	public static class ÉtatCalcExtensions
	{
		public static bool  IsStateStringOkay(this string str)
		{

			if (str != null)
			{
				string[] pileStr = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				for (int i = 0; i < pileStr.Length; i++)
				{
					string element = pileStr.GetValue(i).ToString();

					if (element.Length > 10)
						return false;
					else if (element.Contains("?") && element.Contains("-"))
						return false;
					else if (element.Contains("?") && i == 0 && pileStr.Length > 1)
						return false;
					else if (!element.IsNumberSyntaxOkay(true, new char[] { ',', '.' }))
						return false;
				}
			}
			return true;
		}

		public static IEnumerable<int> ParsePile(this string str)
		{
			List<int> pileInt = new List<int>();

			if (str is null)
				return pileInt;

			string[] pileStr = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < pileStr.Length; i++)
			{
				string element = (string)pileStr.GetValue(i);

				if (element.Contains("-"))
					pileInt.Add(Convert.ToInt32(element));
				else if (element.IsNumberSyntaxOkay(false))
					pileInt.Add(Convert.ToInt32(element));
			}

			return pileInt;
		}

		public static int? ParseCumulator(this string str)
		{
			int? cumulatorValue = null;

			if (str is null)
				return cumulatorValue;

			string[] pile = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < pile.Length; i++)
			{
				string element = (string)pile.GetValue(i);

				if (element.Contains("?"))
					cumulatorValue = Convert.ToInt32(element.Trim('?'));
			}

			return cumulatorValue; 
		}

	}

}
