using SDD.Interface;
using System;
using System.Collections.Generic;
using static SDD.Utility.Utils;
using System.Linq;
using CalculatriceLib;

namespace SDD.Class
{
	public class Calculatrice<T> : ÉtatCalcGen<T>, ICalculatrice
		where T : struct
	{
		/// <summary>
		/// Constructor that takes a pile and an accumulator as arguments
		/// </summary>
		/// <param name="pile"></param>
		/// <param name="acc"></param>
		public Calculatrice(IAlu<T> alu, IPileCalcGen<T> pile = null, IAccumuleurGen<T> acc = null) : base(pile, acc) { this.alu = alu; }

		/// <summary>
		/// Constructor that takes an int cummulator and array of int for the pile content
		/// </summary>
		/// <param name="accumulateur"></param>
		/// <param name="pile"></param>
		public Calculatrice(IAlu<T> alu, T? accumulateur, params T[] pile) : base(accumulateur, pile) { this.alu = alu; }

		/// <summary>
		/// Constructor that takes an IEnumerable for the pile and a int for the cumulator value
		/// </summary>
		/// <param name="pile"></param>
		/// <param name="accumulateur"></param>
		public Calculatrice(IAlu<T> alu, IEnumerable<T> pile, T? accumulateur = null) : base(pile, accumulateur) { this.alu = alu; }

		/// <summary>
		/// Constructor that can initialise the pile and cumulator with a string parameter
		/// </summary>
		/// <param name="enTexte"></param>
		public Calculatrice(IAlu<T> alu,string enTexte) : base(enTexte) { this.alu = alu; }

		public IAlu<T> alu { get; }

		/// <summary>
		/// Return the cumulator value
		/// </summary>
		public string Accumulation => mAccumuleur.Accumulation;

		/// <summary>
		/// Return an IEnumerable object that contains the pile content
		/// </summary>
		public IEnumerable<object> Éléments => Pile.Cast<object>().ToList();

		/// <summary>
		/// Return the results of the operations
		/// </summary>
		public object Résultat => Dessus;

		/// <summary>
		/// Returns a new Calculatrice based of the current one
		/// </summary>
		/// <returns></returns>
		public new ICalculatrice Cloner() => new Calculatrice<T>(alu,EnTexte());

		/// <summary>
		/// Methods that execute the corresponding command passed by the users. Receives an IEnumerable of commands
		/// </summary>
		/// <param name="commandes"></param>
		public void Exécuter(IEnumerable<CalcCommande> commandes)
		{
			Calculatrice<T> initialCalc = (Calculatrice<T>)Cloner();
			List<CalcCommande> listeCommandes = commandes.Cast<CalcCommande>().ToList();

			foreach (CalcCommande commande in listeCommandes)
			{
				if (!Enum.IsDefined(typeof(CalcCommande), commande))
					throw new ArgumentException();

				switch (commande)
				{
					case CalcCommande.__0:
					case CalcCommande.__1:
					case CalcCommande.__2:
					case CalcCommande.__3:
					case CalcCommande.__4:
					case CalcCommande.__5:
					case CalcCommande.__6:
					case CalcCommande.__7:
					case CalcCommande.__8:
					case CalcCommande.__9:
						HandleCumulatorCommand((char)commande);
						break;
					case CalcCommande.EntrerObligatoire:
						HandleEnterCommand(initialCalc, false);
						break;
					case CalcCommande.EntrerFacultatif:
						HandleEnterCommand(initialCalc);
						break;
					case CalcCommande.Backspace:
						HandleBackspaceCommand(initialCalc);
						break;
					case CalcCommande.Addition:
					case CalcCommande.Soustraction:
					case CalcCommande.Multiplication:
					case CalcCommande.DivisionEntière:
					case CalcCommande.Modulo:
						HandleMultipleNumberOperationCommand(commande, initialCalc);
						break;
					case CalcCommande.Négation:
						HandleNegationCommand(initialCalc);
						break;
					case CalcCommande.Carré:
						HandleSquareCommand(initialCalc);
						break;
					case CalcCommande.Dupliquer:
						HandleDuplicateCommand(initialCalc);
						break;
					case CalcCommande.Swapper:
						HandleSwapCommand(initialCalc);
						break;
					case CalcCommande.Pop:
						HandlePopCommand(initialCalc);
						break;
					case CalcCommande.Reset:
						Reset();
						break;
					default:
						break;
				}

			}
		}

		/// <summary>
		/// Methods that receives an Array of command and convert it to a list to pass it to the original execute method
		/// </summary>
		/// <param name="commandes"></param>
		public void Exécuter(params CalcCommande[] commandes)
		{
			Exécuter(commandes.Cast<CalcCommande>().ToList());
		}

		/// <summary>
		/// Method that receive a string as a list of command and then parses it to List of CalcCommand
		/// </summary>
		/// <param name="commandes"></param>
		public void Exécuter(string commandes)
		{
			List<CalcCommande> listeCommandes = new List<CalcCommande>();

			for (int i = 0; i < commandes.Length; i++)
			{
				CalcCommande commande;  // Empty Command equal to 0
				string commandeName;    // Empty string equal to null

				commandeName = Enum.GetName(typeof(CalcCommande), commandes[i]); //Search for the Enum name based on the cahr value
				Enum.TryParse(commandeName, true, out commande);   //Parse the enum to an actual CalcCommande Enum Type

				if (commande != 0)
					listeCommandes.Add(commande); //If the parsing did work, Add the Command to the Command List
				else
					throw new ArgumentException();
			}

			Exécuter(listeCommandes);
		}

		/// <summary>
		/// Indicate wether the method can be executed
		/// </summary>
		/// <param name="commande"></param>
		/// <returns></returns>
		public bool PeutExécuter(CalcCommande commande)
		{
			Calculatrice<T> tempCalc = (Calculatrice<T>)Cloner();
			try
			{
				Exécuter(commande);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			finally
			{
				mAccumuleur = tempCalc.mAccumuleur;
				mPile = tempCalc.mPile;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="initalCalc"></param>
		private void RétablirCalculatrice(Calculatrice<T> initalCalc)
		{
			mAccumuleur = initalCalc.mAccumuleur;
			mPile = initalCalc.mPile;
		}

		private void HandleBackspaceCommand(Calculatrice<T> initialCalc)
		{
			if (mAccumuleur.EstVide)
			{
				RétablirCalculatrice(initialCalc);
				throw new AccumuleurVideException();
			}
			else
				Décumuler();
		}

		/// <summary>
		/// Method that handle operations that involves two adjacents numbers
		/// </summary>
		/// <param name="commande"></param>
		private void HandleMultipleNumberOperationCommand(CalcCommande commande, Calculatrice<T> initialCalc)
		{
			Push();

			List<T> tempList = Pile.ToList();

			if (EstVide || tempList.Count <= 1)
			{
				RétablirCalculatrice(initialCalc);
				throw new PileInsuffisanteException();
			}

			T result = (T) Convert.ChangeType(0, typeof(T));
			T firstNumber = (T) Convert.ChangeType(0, typeof(T));
			T secondNumber = (T) Convert.ChangeType(0, typeof(T));

			if (IsWithinRange(tempList[tempList.Count - 2]))
				firstNumber = tempList[tempList.Count - 2];
			else
			{
				RétablirCalculatrice(initialCalc);
				throw new OverflowException();
			}

			if (IsWithinRange(tempList[tempList.Count - 1]))
				secondNumber = tempList[tempList.Count - 1];
			else
			{
				RétablirCalculatrice(initialCalc);
				throw new OverflowException();
			}

			switch (commande)
			{
				case CalcCommande.Addition:
					result = alu.Additionner(firstNumber, secondNumber);
					break;
				case CalcCommande.Soustraction:
					result = alu.Soustraire(firstNumber, secondNumber);
					break;
				case CalcCommande.Multiplication:
					result = alu.Multiplier(firstNumber, secondNumber);
					break;
				case CalcCommande.DivisionEntière:
					result = alu.Diviser(firstNumber, secondNumber);
					break;
				case CalcCommande.Modulo:
					result = alu.Modulo(firstNumber, secondNumber);
					break;
			}

			if (!IsWithinRange(result)) throw new OverflowException();

			tempList.RemoveAt(tempList.Count - 1);
			tempList[tempList.Count - 1] = result;
			Reset(tempList);
		}

		/// <summary>
		/// Method that handle the square command
		/// </summary>
		private void HandleSquareCommand(Calculatrice<T> initialCalc)
		{
			if (EstVide)
			{
				RétablirCalculatrice(initialCalc);
				throw new PileInsuffisanteException();
			}

			T dessus = alu.Carré((T)Dessus);

			if (!IsWithinRange(dessus))
			{
				RétablirCalculatrice(initialCalc);
				throw new OverflowException();
			}

			if (!String.IsNullOrEmpty(Accumulation))
			{
				mAccumuleur.Reset(dessus, true);
			}
			else if (Éléments.Count() > 0)
			{
				Pop();
				Push(dessus);
			}
		}

		/// <summary>
		/// Method that handle the negation command
		/// </summary>

		private void HandleNegationCommand(Calculatrice<T> initialCalc)
		{
			if (EstVide)
			{
				RétablirCalculatrice(initialCalc);
				throw new ArgumentException();
			}
			T dessus;

			if (!String.IsNullOrEmpty(Accumulation))
			{
				dessus = alu.Négation((T)mAccumuleur.Extraire());
				mAccumuleur.Reset(dessus, true);
			}
			else if (Éléments.Count() > 0)
			{
				dessus = alu.Négation(mPile.Dessus);
				Pop();
				Push(dessus);
			}
		}

		/// <summary>
		/// Method that handle the change of the cumulator content
		/// </summary>
		/// <param name="commande"></param>
		private void HandleCumulatorCommand(char commande)
		{
			Accumuler(commande);
		}

		/// <summary>
		/// Method that handle the enter command
		/// </summary>
		/// <param name="nullAccepted"></param>
		private void HandleEnterCommand(Calculatrice<T> initialCalc, bool nullAccepted = true)
		{
			T? valeur = mAccumuleur.Extraire();

			if (valeur is null && !nullAccepted)
			{
				RétablirCalculatrice(initialCalc);
				throw new AccumuleurVideException();
			}

			if (valeur != null)
				Push((T)valeur);
		}

		/// <summary>
		/// Method that handle the pop command
		/// </summary>
		private void HandlePopCommand(Calculatrice<T> initialCalc)
		{
			if (EstVide)
			{
				RétablirCalculatrice(initialCalc);
				throw new PileInsuffisanteException();
			}
			Pop();
		}

		/// <summary>
		/// Method that handle the duplicate command
		/// </summary>
		private void HandleDuplicateCommand(Calculatrice<T> initalCalc)
		{
			if (EstVide)
			{
				RétablirCalculatrice(initalCalc);
				throw new PileInsuffisanteException();
			}

			T? dessus = Dessus;

			if (dessus != null)
				Push((T)dessus);
		}

		/// <summary>
		/// Method that handle the swap command
		/// </summary>
		private void HandleSwapCommand(Calculatrice<T> initialCalc)
		{
			if (EstVide)
			{
				RétablirCalculatrice(initialCalc);
				throw new PileInsuffisanteException();
			}

			Push();

			if (Éléments.Count() < 2)
			{
				RétablirCalculatrice(initialCalc);
				throw new PileInsuffisanteException();
			}
			else
			{
				List<T> tempList = Éléments.Cast<T>().ToList();
				tempList[tempList.Count - 1] = (T)Éléments.ElementAt(Éléments.Count() - 2);
				tempList[tempList.Count - 2] = (T)Dessus;
				Reset(tempList);
			}


		}

	}
}
