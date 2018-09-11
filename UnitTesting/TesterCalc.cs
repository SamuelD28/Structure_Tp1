// #define ROBUSTESSE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using SDD.Interface;
using SDD.Class;

namespace SDD
{

    public static class CalcExtension
    {
        public static void Exécuter(this ICalculatrice calc, char commande)
        {
            calc.Exécuter((CalcCommande)commande);
        }
    }

    [TestClass]
    public class TesterCalc : TesterCalcBase
    {
        public override object MaxValue => int.MaxValue;

        public override ICalculatrice NewCalc(string accumulateur, params int[] pile)
        {
            return new Calculatrice(new PileCalcListe(pile), new Accumuleur(accumulateur));
        }

        public override ICalculatrice NewCalc(string étatString)
        {
            return new Calculatrice(étatString);
        }
    }


    public abstract class TesterCalcAluBase : TesterCalcBase
    {
        public override ICalculatrice NewCalc(string accumulateur, params int[] pile)
        {
            accumulateur = string.IsNullOrWhiteSpace(accumulateur) ? "" : accumulateur + "?";
            return NewCalc(string.Join("", pile.Select(elem => elem + "  ")) + accumulateur + "?");
        }
    }


    public abstract class TesterCalcBase
    {
        public abstract object MaxValue { get; }
        public abstract ICalculatrice NewCalc(string accumulateur, params int[] pile);
        public abstract ICalculatrice NewCalc(string étatString);

        public ICalculatrice NewCalc(params int[] pile)
        {
            return NewCalc("", pile);
        }

        public void Vérifier(ICalculatrice calc, string commandes, string toString)
        {
            calc.Exécuter(commandes);
            AreEqual(toString, calc.ToString());
        }

        public void Vérifier(ICalculatrice calc, string toString)
        {
            AreEqual(toString, calc.ToString());
        }

        public void Vérifier(string commandes, string toString)
        {
            var calc = NewCalc();
            calc.Exécuter(commandes);
            AreEqual(toString, calc.ToString());
        }

        [Conditional("ROBUSTESSE")]
        public void Robuste(ICalculatrice calc, params int[] éléments)
        {
            AreEqual(String.Join("  ", éléments), calc.ToString());
        }

        [Conditional("ROBUSTESSE")]
        public void Robuste(ICalculatrice calc, string toString, string message = null)
        {
            AreEqual(toString, calc.ToString(), message);
        }

        public static readonly IEnumerable<char> ToutesCommandes =
            ((IEnumerable<CalcCommande>)Enum.GetValues(typeof(CalcCommande))).Select(comm => (char)comm);

        public void PeutExécuter(string état, IEnumerable<char> commandes)
        {
            var calc = NewCalc(état);
            foreach (var commande in commandes.Select(com => (CalcCommande)com))
            {
                IsTrue(calc.PeutExécuter(commande), $"La calculatrice doit accepter la commande '{commande}' dans l'état '{état}'");
                Robuste(calc, état, $"PeutExécuter({commande}) ne doit pas modifier l'état");
            }
        }

        public void PeutPasExécuter(string état, IEnumerable<char> commandes)
        {
            var calc = NewCalc(état);
            foreach (var commande in commandes.Select(com => (CalcCommande)com))
            {
                IsFalse(calc.PeutExécuter(commande), $"La calculatrice ne doit pas accepter la commande '{commande}' dans l'état '{état}'");
                Robuste(calc, état, $"PeutExécuter({commande}) ne doit pas modifier l'état");
            }
        }

        public void PeutSeulementExécuter(string état, string commandes)
        {
            PeutExécuter(état, commandes);
            PeutPasExécuter(état, ToutesCommandes.Except(commandes));
        }

        public void PeutSeulementPasExécuter(string état, string commandes)
        {
            PeutExécuter(état, ToutesCommandes.Except(commandes));
            PeutPasExécuter(état, commandes);
        }

        [TestMethod]
        public void _01_ConstructeursVides()
        {
            Vérifier(new Calculatrice(), "");
            Vérifier(new Calculatrice((IEnumerable<int>)null), "");
            Vérifier(new Calculatrice((IEnumerable<int>)null, null), "");
            Vérifier(new Calculatrice(new int[] { }, null), "");
            Vérifier(new Calculatrice((IPileCalc)null), "");
            Vérifier(new Calculatrice((IPileCalc)null), "");
            Vérifier(new Calculatrice((int?)null), "");
            Vérifier(new Calculatrice(""), "");
        }

        [TestMethod]
        public void _02_ConstructeursNonVides()
        {
            Vérifier(new Calculatrice(new int[] {10, 20 }, 30), "10  20  30?");
            Vérifier(new Calculatrice(new PileCalcListe(new int[] { 10, 20 }), new Accumuleur(30)), "10  20  30?");
            Vérifier(new Calculatrice(30, 10, 20), "10  20  30?");
            Vérifier(new Calculatrice("10  20  30?"), "10  20  30?");
        }

        [TestMethod]
        public void _05_Accumulation()
        {
            AreEqual("", NewCalc().Accumulation);
            AreEqual("100", NewCalc("100?").Accumulation);
        }

        [TestMethod]
        public void _06_Éléments()
        {
            var calc = NewCalc(10, 20, 30);
            AreEqual(3, calc.Éléments.Count());
            AreEqual(10, Convert.ToInt32(calc.Éléments.ElementAt(0)));
            AreEqual(20, Convert.ToInt32(calc.Éléments.ElementAt(1)));
            AreEqual(30, Convert.ToInt32(calc.Éléments.ElementAt(2)));
            AreEqual(MaxValue, NewCalc(MaxValue + "").Éléments.ElementAt(0));
        }

        [TestMethod]
        public void _07_Résultats()
        {
            AreEqual(30, Convert.ToInt32(NewCalc(10, 20, 30).Résultat));
            AreEqual(40, Convert.ToInt32(NewCalc("40", 10, 20, 30).Résultat));
            ThrowsException<PileVideException>(()=>NewCalc().Résultat);
            AreEqual(MaxValue, NewCalc(MaxValue + "?").Résultat);
        }

        [TestMethod]
        public void _08_EnTexte()
        {
            // Non paramétré
            AreEqual("", NewCalc().EnTexte());
            AreEqual("40?", NewCalc("40?").EnTexte());
            AreEqual("10  20  30", NewCalc(10, 20, 30).EnTexte());
            AreEqual("10  20  30  40?", NewCalc("40", 10, 20, 30).EnTexte());
            
            // paramétré
            AreEqual("10, 20, 30", NewCalc(10, 20, 30).EnTexte(", "));
            AreEqual("10 20 30 40?", NewCalc("40", 10, 20, 30).EnTexte(" "));
        }

        [TestMethod]
        public void _12_CommandeInconnu()
        {
            var calc = NewCalc();
            foreach (var commande in "[](){}")
            {
                ThrowsException<ArgumentException>(
                    ()=>calc.Exécuter(commande));
            }
            Vérifier(calc, "");  // robustesse
        }

        [TestMethod]
        public void _13_CommandesSimplesEtAccumulation()
        {
            var calc = NewCalc();
            Vérifier(calc, "1", "1?");
            Vérifier(calc, "2", "12?");
            Vérifier(calc, "3", "123?");
        }

        [TestMethod]
        public void _14_CommandesMultiplesEtAccumulation()
        {
            var calc = NewCalc("1?");
            Vérifier(calc, "1?");
            Vérifier(calc, "23", "123?");
            Vérifier(calc, "45", "12345?");
        }

        [TestMethod]
        public void _15_AccumulationDuZéro()
        {
            Vérifier("000", "0?");
            Vérifier("00100", "100?");
        }

        [TestMethod]
        public void _21_PeutExécuter()
        {
            var calc = NewCalc();
            foreach (var commande in "[](){}")
            {
                IsFalse(calc.PeutExécuter((CalcCommande)commande), "La calculatrice ne doit pas accepter la commande: " + commande);
            }
            PeutSeulementExécuter("", " 0123456789r");
            PeutSeulementExécuter("10", " 0123456789dnpr²");
            PeutSeulementPasExécuter("10?", "+-*\\%s");
            PeutSeulementPasExécuter("10  20", "be");
            PeutSeulementPasExécuter("10  20?", "");
            PeutSeulementPasExécuter("10  0?", "\\%");
            PeutPasExécuter($"10  {MaxValue}?", "0123456789*²+");
            PeutPasExécuter($"-10  {MaxValue}?", "-");
        }

        [TestMethod]
        public void _22a_ClonerVide()
        {
            var calc = NewCalc();
            var clone = calc.Cloner();
            Vérifier(calc, "");
            Vérifier(clone, "");
            calc.Exécuter("20 3");
            Vérifier(calc, "20  3?");
            Vérifier(clone, "");
        }

        [TestMethod]
        public void _22b_ClonerNonVide()
        {
            var calc = NewCalc(10, 20);
            var clone = calc.Cloner();
            Vérifier(calc, "10  20");
            Vérifier(clone, "10  20");
            calc.Exécuter("30 40");
            Vérifier(calc, "10  20  30  40?");
            Vérifier(clone, "10  20");
        }

        [TestMethod]
        public void _31a_Backspace()
        {
            var calc = NewCalc("1234?");
            Vérifier(calc, "b", "123?");
            Vérifier(calc, "b", "12?");
            Vérifier(calc, "bb", "");
        }

        [TestMethod]
        public void _31b_BackspaceVideErreur()
        {
            var calc = NewCalc();
            ThrowsException<AccumuleurVideException>(() => calc.Exécuter('b'));
            Vérifier(calc, "");
        }

        [TestMethod]
        public void _32_BackspaceEtAccumulationMultiples()
        {
            var calc = NewCalc();
            Vérifier(calc, "1234bb34b", "123?");
        }

        [TestMethod]
        public void _41_Reset()
        {
            var calc = NewCalc("40", 10, 20, 30);
            Vérifier(calc, "r", "");
            Vérifier(calc, "rr", "");
        }

        [TestMethod]
        public void _42a_PousserObligatoire()
        {
            var calc = NewCalc("40", 10, 20, 30);
            Vérifier(calc, "10  20  30  40?");
            Vérifier(calc, "e", "10  20  30  40");
        }

        [TestMethod]
        public void _42b_PousserObligatoireErreur()
        {
            var calc = NewCalc(10, 20, 30);
            ThrowsException<AccumuleurVideException>(() => calc.Exécuter('e'));
            Robuste(calc, 10, 20, 30);
        }

        [TestMethod]
        public void _42c_PousserFacultative()
        {
            var calc = NewCalc("40", 10, 20, 30);
            Vérifier(calc, "10  20  30  40?");
            Vérifier(calc, " ", "10  20  30  40");
            Vérifier(calc, "    ", "10  20  30  40");
        }

        [TestMethod]
        public void _43a_Pop()
        {
            var calc = NewCalc("30", 10, 20);
            Vérifier(calc, "10  20  30?");
            Vérifier(calc, "p", "10  20");
            Vérifier(calc, "p", "10");
            Vérifier(calc, "p", "");
        }

        [TestMethod]
        public void _43b_PopVideErreur()
        {
            var calc = NewCalc();
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter('p'));
            Robuste(calc);
        }

        [TestMethod]
        public void _44a_Dupliquer()
        {
            var calc = NewCalc("30", 10, 20);
            Vérifier(calc, "10  20  30?");
            Vérifier(calc, "d", "10  20  30  30");
            Vérifier(calc, "d", "10  20  30  30  30");
        }

        [TestMethod]
        public void _44b_DupliquerVideErreur()
        {
            var calc = NewCalc();
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("d"));
            Robuste(calc);
        }

        [TestMethod]
        public void _45a_Swapper()
        {
            var calc = NewCalc("30", 10, 20);
            Vérifier(calc, "s", "10  30  20");
            Vérifier(calc, "s", "10  20  30");
            Vérifier(calc, "ss", "10  20  30");
        }

        [TestMethod]
        public void _45b_SwapperErreurAcc()
        {
            var calc = NewCalc("10?");
            Vérifier(calc, "10?");
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter('s'));
            Robuste(calc, "10?");
        }

        [TestMethod]
        public void _45c_SwapperErreurPile()
        {
            var calc = NewCalc(10);
            Vérifier(calc, "10");
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter('s'));
            Robuste(calc, 10);
        }

        [TestMethod]
        public void _45d_SwapperErreurVide()
        {
            var calc = NewCalc();
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter('s'));
            Robuste(calc);
        }

        [TestMethod]
        public void _46_PilePlus()
        {
            var calc = NewCalc("30", 10, 20);
            Vérifier(calc, "sddppss", "10  30  20");
        }

        [TestMethod]
        public void _47_PileAccPlus()
        {
            var calc = NewCalc("30?");
            Vérifier(calc, "08be 9s", "9  300");
        }

        [TestMethod]
        public void _51_Négation()
        {
            var calc = NewCalc("30");
            Vérifier(calc, "n", "-30");
            Vérifier(calc, "nnn", "30");

            calc = NewCalc(2, 3);
            Vérifier(calc, "n", "2  -3");
            Vérifier(calc, "nn", "2  -3");
        }

        [TestMethod]
        public void _52a_Carré()
        {
            var calc = NewCalc("2");
            Vérifier(calc, "²", "4");
            Vérifier(calc, "²²", "256");
        }

        [TestMethod]
        public void _52b_CarréOverflow()
        {
            var calc = NewCalc(MaxValue + "?");
            ThrowsException<OverflowException>(() => calc.Exécuter("²"));
            Robuste(calc, MaxValue + "?");
        }

        [TestMethod]
        public void _52c_CarréPileInsuffisante()
        {
            var calc = NewCalc();
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("²"));
            Robuste(calc);
        }

        [TestMethod]
        public void _61a_Addition()
        {
            var calc = NewCalc("3", 1, 2);
            Vérifier(calc, "+", "1  5");
            Vérifier(calc, "+", "6");
        }

        [TestMethod]
        public void _61b_AdditionOverflow()
        {
            var calc = NewCalc($"1  {MaxValue}");
            ThrowsException<OverflowException>(() => calc.Exécuter("+"));
            Robuste(calc, $"1  {MaxValue}");
        }

        [TestMethod]
        public void _61c_AdditionPileVide()
        {
            var calc = NewCalc();
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("+"));
            Robuste(calc);
        }

        [TestMethod]
        public void _61d_AdditionPileInsuffisante()
        {
            var calc = NewCalc(2);
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("+"));
            Robuste(calc, 2);
        }

        [TestMethod]
        public void _62a_Soustraction()
        {
            var calc = NewCalc("3", 1, 2);
            Vérifier(calc, "-", "1  -1");
            Vérifier(calc, "-", "2");
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("-"));
            Robuste(calc, 2);
            calc.Reset();
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("-"));
            Robuste(calc);
        }

        [TestMethod]
        public void _62b_SoustractionOverflow()
        {
            var calc = NewCalc($"-{MaxValue}  2");
            ThrowsException<OverflowException>(() => calc.Exécuter("-"));
            Robuste(calc, $"-{MaxValue}  2");
        }

        [TestMethod]
        public void _63a_Multiplication()
        {
            var calc = NewCalc("3", 5, 2);
            Vérifier(calc, "*", "5  6");
            Vérifier(calc, "*", "30");
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("*"));
            Robuste(calc, 30);
            calc.Reset();
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("*"));
            Robuste(calc);
        }

        [TestMethod]
        public void _63b_MultiplicationOverflow()
        {
            var calc = NewCalc($"{MaxValue}  2");
            ThrowsException<OverflowException>(() => calc.Exécuter("*"));
            Robuste(calc, $"{MaxValue}  2");
        }

        [TestMethod]
        public void _64a_DivisionEntière()
        {
            var calc = NewCalc("3", 1, -20, 20);
            Vérifier(calc, "\\", "1  -20  6");
            Vérifier(calc, "\\", "1  -3");
            Vérifier(calc, "\\", "0");

            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("\\"));
            Robuste(calc, 0);
            calc.Reset();
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("\\"));
            Robuste(calc);
        }

        [TestMethod]
        public void _64b_DivisionParZéro()
        {
            var calc = NewCalc(4, 0);
            ThrowsException<DivideByZeroException>(() => calc.Exécuter("\\"));
            Robuste(calc, 4, 0);
        }

        [TestMethod]
        public void _65a_Modulo()
        {
            var calc = NewCalc("7", 2, -20, 20);
            Vérifier(calc, "%", "2  -20  6");
            Vérifier(calc, "%", "2  -2");
            Vérifier(calc, "%", "0");
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("%"));
            Robuste(calc, 0);
            calc.Reset();
            ThrowsException<PileInsuffisanteException>(() => calc.Exécuter("%"));
            Robuste(calc);
        }

        [TestMethod]
        public void _65b_ModuloParZéro()
        {
            var calc = NewCalc(4, 0);
            ThrowsException<DivideByZeroException>(() => calc.Exécuter("%"));
            Robuste(calc, 4, 0);
        }

        [TestMethod]
        public void _71_Pythagore()
        {
            Vérifier("r 3d* 4d* +", "25");  
            Vérifier("r 3² 4² +", "25");    
            Vérifier("r3e4s²s²+", "25");
            Vérifier("r5n²4n²-", "9");
        }

        [TestMethod]
        public void _72_ResteEtModulo()
        {
            Vérifier(@"95d10\s10%", "9  5");  // 95 mod 10 = 9 reste 5
        }

        [TestMethod]
        [Conditional("ROBUSTESSE")]
        public void _81_RobustesseMultiple()
        {
            var calc = NewCalc("10", 10);
            foreach(var commandes in new[] {
                "++", "--", "**", "s++", "ppp", "bbb", "²²²²²²²²²²²²²²²²" 
            }){
                try
                {
                    calc.Exécuter(commandes);
                    Fail("Doit lever une exception: " + commandes);
                }
                catch 
                {
                    IsTrue("10  10?" == calc.ToString(), "Ne doit pas modifier l'état: " + commandes);
                }
            }
        }

        [TestMethod]
        [Conditional("ROBUSTESSE")]
        public void _82_RobustesseReset()
        {
            var calc = NewCalc("40", 10, 20);
            Vérifier(calc, "10  20  40?");
            try { calc.Exécuter("r+"); } catch { }
            Robuste(calc, "10  20  40?");
        }

        [TestMethod]
        [Conditional("ROBUSTESSE")]
        public void _83a_ErreurPrioritaire1()
        {
            var calc = NewCalc("40", 10, 20);
            Vérifier(calc, "10  20  40?");
            ThrowsException<PileInsuffisanteException>(()=>calc.Exécuter("+++"));
            Robuste(calc, "10  20  40?");
            ThrowsException<ArgumentException>(() => calc.Exécuter("+++m"));
            Robuste(calc, "10  20  40?");
        }

        [TestMethod]
        [Conditional("ROBUSTESSE")]
        public void _83b_ErreurPrioritaire2()
        {
            var calc = NewCalc(10);
            Vérifier(calc, "10");
            ThrowsException<OverflowException>(() => calc.Exécuter("²²²²²²²²²²²²"));
            Robuste(calc, "10");
            ThrowsException<ArgumentException>(() => calc.Exécuter("²²²²²²²²²²²²m"));
            Robuste(calc, "10");
        }

        [TestMethod]
        public void _88_RobustesseTestée()
        {
#if ROBUSTESSE
            IsTrue(true);
#else
            IsTrue(false);
#endif
        }

    }
}
