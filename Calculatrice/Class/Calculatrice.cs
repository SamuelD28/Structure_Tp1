﻿using SDD.Interface;
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

        public new ICalculatrice Cloner() => new Calculatrice(EnTexte());

        public void Exécuter(IEnumerable<CalcCommande> commandes)
        {
            List<CalcCommande> listeCommandes = commandes.Cast<CalcCommande>().ToList();

            foreach (CalcCommande commande in listeCommandes)
            {
                /***/
                if (!Enum.IsDefined(typeof(CalcCommande), commande)) throw new ArgumentException();
                /***/

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
                        HandleNumberCommand((char)commande);
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
                        break;
                    case CalcCommande.Soustraction:
                        break;
                    case CalcCommande.Multiplication:
                        break;
                    case CalcCommande.DivisionEntière:
                        break;
                    case CalcCommande.Modulo:
                        break;
                    case CalcCommande.Négation:
                        break;
                    case CalcCommande.Carré:
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

        public void Exécuter(params CalcCommande[] commandes)
        {
            Exécuter(commandes.Cast<CalcCommande>().ToList());
        }

        public void Exécuter(string commandes)
        {
            List<CalcCommande> listeCommandes = new List<CalcCommande>();

            for (int i = 0; i < commandes.Length; i++)
            {
                CalcCommande commande;  // Empty Command equal to 0
                string commandeName;    // Empty string equal to null

                commandeName = Enum.GetName(typeof(CalcCommande), commandes[i]); //Search for the Enum name based on the cahr value
                Enum.TryParse(commandeName, true, out commande);   //Parse the enum to an actual CalcCommande Enum Type

                if (commande != 0) listeCommandes.Add(commande); //If the parsing did work, Add the Command to the Command List
            }

            Exécuter(listeCommandes);
        }

        public bool PeutExécuter(CalcCommande commande) => true;

        private void HandleNumberCommand(char commande)
        {
            Accumuler(commande);
        }

        private void HandleEnterCommand(bool nullAccepted = true)
        {
            int? valeur = mAccumuleur.Extraire();

            if (valeur is null && !nullAccepted) throw new AccumuleurVideException();

            if (valeur != null)
                Push((int)valeur);
        }

        private void HandlePopCommand()
        {
            if (EstVide) throw new PileInsuffisanteException();
            Pop();
        }

        private void HandleDuplicateCommand()
        {
            if (EstVide) throw new PileInsuffisanteException();

            int? dessus = Dessus;

            if (dessus != null)
                Push((int)dessus);
        }

        private void HandleSwapCommand()
        {
            if (EstVide) throw new PileInsuffisanteException();


            if (Accumulateur != null)
            {
                int acc = (int)mAccumuleur.Extraire();
                HandleDuplicateCommand();
                mPile.ListeÉléments[mPile.ListeÉléments.Count - 2] = acc;
            }
            else
            {
                int dessus = (int)Dessus;
                HandleDuplicateCommand();
            }

        }

    }
}
