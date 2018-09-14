using SDD.Interface;
using System;
using System.Collections.Generic;
using static SDD.Utility.Utils;
using System.Linq;

namespace SDD.Class
{
    public class Calculatrice : ÉtatCalc, ICalculatrice
    {
		/// <summary>
		/// Constructor that takes a pile and an accumulator as arguments
		/// </summary>
		/// <param name="pile"></param>
		/// <param name="acc"></param>
        public Calculatrice(IPileCalc pile = null, IAccumuleur acc = null) : base(pile, acc) { }

		/// <summary>
		/// Constructor that takes an int cummulator and array of int for the pile content
		/// </summary>
		/// <param name="accumulateur"></param>
		/// <param name="pile"></param>
        public Calculatrice(int? accumulateur, params int[] pile) : base(accumulateur, pile) { }

		/// <summary>
		/// Constructor that takes an IEnumerable for the pile and a int for the cumulator value
		/// </summary>
		/// <param name="pile"></param>
		/// <param name="accumulateur"></param>
        public Calculatrice(IEnumerable<int> pile, int? accumulateur = null) : base(pile, accumulateur) { }

		/// <summary>
		/// Constructor that can initialise the pile and cumulator with a string parameter
		/// </summary>
		/// <param name="enTexte"></param>
        public Calculatrice(string enTexte) : base(enTexte) { }

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
        public new ICalculatrice Cloner() => new Calculatrice(EnTexte());

		/// <summary>
		/// Methods that execute the corresponding command passed by the users. Receives an IEnumerable of commands
		/// </summary>
		/// <param name="commandes"></param>
        public void Exécuter(IEnumerable<CalcCommande> commandes)
        {
            List<CalcCommande> listeCommandes = commandes.Cast<CalcCommande>().ToList();

            foreach (CalcCommande commande in listeCommandes)
            {
                if (!Enum.IsDefined(typeof(CalcCommande), commande)) throw new ArgumentException();
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
                        HandleEnterCommand(false);
                        break;
                    case CalcCommande.EntrerFacultatif:
                        HandleEnterCommand();
                        break;
                    case CalcCommande.Backspace:
                        Décumuler();
                        break;
                    case CalcCommande.Addition:
                    case CalcCommande.Soustraction:
                    case CalcCommande.Multiplication:
                    case CalcCommande.DivisionEntière:
                    case CalcCommande.Modulo:
						HandleMultipleNumberOperationCommand(commande);
                        break;
                    case CalcCommande.Négation:
						HandleNegationCommand();
                        break;
                    case CalcCommande.Carré:
						HandleSquareCommand();
                        break;
                    case CalcCommande.Dupliquer:
                        HandleDuplicateCommand();
                        break;
                    case CalcCommande.Swapper:
                        HandleSwapCommand();
                        break;
                    case CalcCommande.Pop:
                        HandlePopCommand();
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
        public bool PeutExécuter(CalcCommande commande) => false;

		/// <summary>
		/// Method that handle operations that involves two adjacents numbers
		/// </summary>
		/// <param name="commande"></param>
		private void HandleMultipleNumberOperationCommand(CalcCommande commande)
		{
			Push();

			if (EstVide || ListeÉléments.Count <= 1)
				throw new PileInsuffisanteException();

			int result = 0;
			int firstNumber = (IsInt32(ListeÉléments[ListeÉléments.Count - 2])) ? ListeÉléments[ListeÉléments.Count - 2] : throw new OverflowException();
			int secondNumber = (IsInt32(ListeÉléments[ListeÉléments.Count - 1])) ? ListeÉléments[ListeÉléments.Count - 1] : throw new OverflowException();

			switch (commande)
			{
				case CalcCommande.Addition:
					Int32.TryParse((firstNumber + secondNumber).ToString(), out result);
					break;
				case CalcCommande.Soustraction:
					Int32.TryParse((firstNumber - secondNumber).ToString(), out result);
					break;
				case CalcCommande.Multiplication:
					Int32.TryParse((firstNumber * secondNumber).ToString(), out result);
					break;
				case CalcCommande.DivisionEntière:
					Int32.TryParse((firstNumber / secondNumber).ToString(), out result);
					break;
				case CalcCommande.Modulo:
					Int32.TryParse((firstNumber % secondNumber).ToString(), out result);
					break;
			}

			if (!IsInt32(result)) throw new OverflowException();

			Pop();
			ListeÉléments[ListeÉléments.Count - 1] = result;

		}

		/// <summary>
		/// Method that handle the square command
		/// </summary>
		private void HandleSquareCommand()
		{
			if (EstVide) throw new PileInsuffisanteException();

			int dessus =(int)(Math.Round(Math.Pow((int)Dessus, 2)));

			if (dessus >= Int32.MaxValue || dessus <= Int32.MinValue) throw new OverflowException();

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
		private void HandleNegationCommand()
		{
			int dessus;

			if (!String.IsNullOrEmpty(Accumulation))
			{
				dessus = (int)mAccumuleur.Extraire() * - 1;
				mAccumuleur.Reset(dessus, true);
			}
			else if (Éléments.Count() > 0)
			{
				dessus = mPile.Dessus * - 1;
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
        private void HandleEnterCommand(bool nullAccepted = true)
        {
            int? valeur = mAccumuleur.Extraire();

            if (valeur is null && !nullAccepted) throw new AccumuleurVideException();

            if (valeur != null)
                Push((int)valeur);
        }

		/// <summary>
		/// Method that handle the pop command
		/// </summary>
        private void HandlePopCommand()
        {
            if (EstVide) throw new PileInsuffisanteException();
            Pop();
        }

		/// <summary>
		/// Method that handle the duplicate command
		/// </summary>
        private void HandleDuplicateCommand()
        {
            if (EstVide) throw new PileInsuffisanteException();

            int? dessus = Dessus;

            if (dessus != null)
                Push((int)dessus);
        }

		/// <summary>
		/// Method that handle the swap command
		/// </summary>
        private void HandleSwapCommand()
        {
            if (EstVide) throw new PileInsuffisanteException();

			Push();

			if (Éléments.Count() < 2)
				throw new PileInsuffisanteException();
			else
			{
				List<int> tempList = Éléments.Cast<int>().ToList();
				tempList[tempList.Count - 1] = (int)Éléments.ElementAt(Éléments.Count() - 2);
				tempList[tempList.Count - 2] = (int)Dessus;
				ListeÉléments = tempList;
			}


        }

    }
}
