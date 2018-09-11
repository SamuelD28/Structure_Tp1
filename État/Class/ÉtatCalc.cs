using SDD.Interface;
using System;
using System.Collections.Generic;
using static SDD.Utility.StringExtensions;
using static SDD.Class.ÉtatCalcUtils;

namespace SDD.Class
{
	public class ÉtatCalc : IÉtatCalc
	{
		//Constructors//
		public ÉtatCalc(IPileCalc pile = null, IAccumuleur acc = null)
		{
			mAccumuleur = (acc != null) ? (Accumuleur)acc : new Accumuleur();
			mPile = (pile != null) ? mPile = (PileCalcListe)pile : new PileCalcListe();
		}

		public ÉtatCalc(int? accumulateur, params int[] pile)
		{
			mPile = (pile != null) ? new PileCalcListe(pile) : new PileCalcListe();
			mAccumuleur = (accumulateur != null) ? new Accumuleur(accumulateur) : new Accumuleur();
		}

		public ÉtatCalc(IEnumerable<int> pile, int? accumulateur = null)
		{
			mPile = new PileCalcListe(pile);
			mAccumuleur = (accumulateur != null) ? new Accumuleur(accumulateur) : new Accumuleur();
		}

		public ÉtatCalc(string enTexte)
		{
			char[] separator = new char[] { ' ' };
			string[] strArray = enTexte.ParseStringToArray(separator);

			if (!IsStateStringOkay(strArray))
				throw new ArgumentException();

			mPile = new PileCalcListe(ParsePile(strArray));
			mAccumuleur = new Accumuleur(ParseCumulator(strArray));
		}

		//Private Properties of the class//
		private Accumuleur mAccumuleur { get; set; }

		private PileCalcListe mPile { get; set; }

		public int? Accumulateur => mAccumuleur.Valeur;

		public IEnumerable<int> Pile => mPile.Éléments;

		public bool EstVide => (mAccumuleur.EstVide && mPile.EstVide) ? true : false;

		public int Dessus =>
			(mAccumuleur.Valeur != null) ? (int)mAccumuleur.Valeur :
			(mPile.Dessus != 0) ? mPile.Dessus :
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

			if (Accumulateur != null)
				mPile.Push((int)mAccumuleur.Extraire());
		}

		public string EnTexte(string séparateur = "  ")
		{
			string pileContent = mPile.EnTexte(séparateur);
			string stateContent = (mAccumuleur.Accumulation != String.Empty) ? mAccumuleur.Accumulation + "?" : String.Empty;
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

			if (accumulateur > 0 || accumulateur is null)
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
			char[] separator = new char[] { ' ' };
			string[] strArray = enTexte.ParseStringToArray(separator);

			if (!IsStateStringOkay(strArray))
				throw new ArgumentException();

			Reset(ParsePile(strArray), ParseCumulator(strArray));
		}

		public override string ToString() => EnTexte();
	}

	public class ÉtatCalcUtils
	{
		public static bool IsStateStringOkay(string[] strArray)
		{
			if (strArray != null)
			{
				for (int i = 0; i < strArray.Length; i++)
				{
					string	element = strArray.GetValue(i).ToString();
					return	(element.Length > 10) ? false :
							(element.Contains("?") && element.Contains("-")) ? false :
							(element.Contains("?") && i == 0 && strArray.Length > 1) ? false :
							(!element.IsNumberSyntaxOkay(true, new char[] { ',', '.' })) ? false : true;
				}
			}
			return true;
		}

		public static IEnumerable<int> ParsePile(string[] strArray)
		{
			List<int> pileInt = new List<int>();

			if (strArray is null) return pileInt;

			for (int i = 0; i < strArray.Length; i++)
			{
				string element = strArray.GetValue(i).ToString();

				if (element.Contains("-"))
					pileInt.Add(Convert.ToInt32(element));
				else if (element.IsNumberSyntaxOkay(nullAccepted: false))
					pileInt.Add(Convert.ToInt32(element));
			}

			return pileInt;
		}

		public static int? ParseCumulator(string[] strArray)
		{
			int? cumulatorValue = null;

			if (strArray is null) return cumulatorValue;

			for (int i = 0; i < strArray.Length; i++)
			{
				string element = strArray.GetValue(i).ToString();

				if (element.Contains("?"))
					cumulatorValue = Convert.ToInt32(element.Trim('?'));
			}

			return cumulatorValue;
		}
	}
}
