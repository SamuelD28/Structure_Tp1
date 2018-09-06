using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using SDD.Interface;
using SDD.Class;

namespace SDD
{

    [TestClass]
    public class TesterÉtat
    {
        public IÉtatCalc NewÉtat(string accumulateur, params int[] pile)
            => new ÉtatCalc(int.Parse(accumulateur), pile);

        public IÉtatCalc NewÉtat(params int[] pile)
            => new ÉtatCalc(null, pile);

        public IÉtatCalc NewÉtat()
            => new ÉtatCalc();

        public void Vérifier(IÉtatCalc état, int? accumulateur = null, params int[] pile)
        {
            AreEqual(accumulateur, état.Accumulateur);
            AreEqual(pile.Length, état.Pile.Count());
            for(int i = 0; i < pile.Length; ++i)
            {
                AreEqual(pile[i], état.Pile.ElementAt(i), $"Pour l'élément {i} de la pile");
            }
        }

        public void Vérifier(IÉtatCalc état, string toString)
        {
            AreEqual(toString, état.ToString());
        }

        [TestMethod]
        public void _01_ÉtatVide()
        {
            // Constructeur IEnumerable, int?
            Vérifier(new ÉtatCalc(new int[] { }));
            Vérifier(new ÉtatCalc(new int[] { }, null));

            // Constructeur IPileCalc, IAccumulateur
            Vérifier(new ÉtatCalc((IPileCalc)null));
            Vérifier(new ÉtatCalc((IPileCalc)null, null));

            // Constructeur int? params
            Vérifier(new ÉtatCalc());
            Vérifier(new ÉtatCalc((int?)null));
            Vérifier(new ÉtatCalc((int?)null, null));
        }

        [TestMethod]
        public void _02_ÉtatAvecPile()
        {
            // Constructeur IEnumerable, int?
            Vérifier(new ÉtatCalc(new int[] {10, 20, 30 }), null, 10, 20, 30);

            // Constructeur IPileCalc, IAccumulateur
            Vérifier(new ÉtatCalc(new PileCalcListe(new int[] { 10, 20, 30})), null, 10, 20, 30);

            // Constructeur int? params
            Vérifier(new ÉtatCalc((int?)null, 10, 20, 30), null, 10, 20, 30);
        }

        [TestMethod]
        public void _03a_ÉtatAvecAccumulateur()
        {
            // Constructeur IEnumerable, int?
            Vérifier(new ÉtatCalc(null, (int?)40), 40);

            // Constructeur IPileCalc, IAccumulateur
            Vérifier(new ÉtatCalc(null, new Accumuleur(40)), 40);

            // Constructeur int? params
            Vérifier(new ÉtatCalc(40), 40);
        }

        [TestMethod]
        public void _03b_ÉtatAvecAccumulateurErroné()
        {
            ThrowsException<ArgumentException>(()=> new ÉtatCalc(null, (int?)-40));
            ThrowsException<ArgumentException>(()=> new ÉtatCalc(null, new Accumuleur(-40)));
            ThrowsException<ArgumentException>(()=> new ÉtatCalc(-40));
        }

        [TestMethod]
        public void _04_ÉtatAvecPileEtAccumulateur()
        {
            // Constructeur IEnumerable, int?
            Vérifier(new ÉtatCalc(new int[] { 10, 20, 30 }, (int?)40), 40, 10, 20, 30);

            // Constructeur IPileCalc, IAccumulateur
            Vérifier(new ÉtatCalc(new PileCalcListe(new[] { 10, 20, 30 }), new Accumuleur(40)), 40, 10, 20, 30);

            // Constructeur int? params
            Vérifier(new ÉtatCalc((int?)40, 10, 20, 30), 40, 10, 20, 30);
        }

        [TestMethod]
        public void _05_EstVide()
        {
            IsTrue(NewÉtat().EstVide);
            IsFalse(NewÉtat("10").EstVide);
            IsFalse(NewÉtat(20).EstVide);
            IsFalse(NewÉtat("10", 20).EstVide);
        }

        [TestMethod]
        public void _06a_EnTexte()
        {
            AreEqual("", NewÉtat().EnTexte());
            AreEqual("40?", NewÉtat("40").EnTexte());
            AreEqual("10  20  30", NewÉtat(10, 20, 30).EnTexte());
            AreEqual("10  20  30  40?", NewÉtat("40", 10, 20, 30).EnTexte());
        }

        [TestMethod]
        public void _06b_EnTexteParamétré()
        {
            AreEqual("10, 20, 30", NewÉtat(10, 20, 30).EnTexte(", "));
            AreEqual("10 20 30 40?", NewÉtat("40", 10, 20, 30).EnTexte(" "));
        }

        [TestMethod]
        public void _06c_ToString()
        {
            AreEqual("", NewÉtat().ToString());
            AreEqual("40?", "" + NewÉtat("40"));
            AreEqual("10  20  30", NewÉtat(10, 20, 30).ToString());
            AreEqual("10  20  30  40?", "" + NewÉtat("40", 10, 20, 30));
        }

        [TestMethod]
        public void _07_Dessus()
        {
            ThrowsException<PileVideException>(()=>NewÉtat().Dessus);
            AreEqual(40, NewÉtat("40").Dessus);
            AreEqual(30, NewÉtat(10, 20, 30).Dessus);
            AreEqual(40, NewÉtat("40", 10, 20, 30).Dessus);
        }

        [TestMethod]
        public void _11a_Accumuler()
        {
            var état = NewÉtat();
            Vérifier(état, "");
            état.Accumuler('1');
            Vérifier(état, "1?");
            état.Accumuler('2');
            Vérifier(état, "12?");
            état.Accumuler('3');
            Vérifier(état, "123?");
        }

        [TestMethod]
        public void _11b_AccumulerOverflow()
        {
            var état = NewÉtat(int.MaxValue.ToString());
            Vérifier(état, int.MaxValue + "?");
            ThrowsException<OverflowException>(()=>état.Accumuler('0'));
            Vérifier(état, int.MaxValue + "?");
        }

        [TestMethod]
        public void _11c_AccumulerErroné()
        {
            var état = NewÉtat("10");
            Vérifier(état, "10?");
            ThrowsException<ArgumentException>(() => état.Accumuler('a'));
            Vérifier(état, "10?");
        }

        [TestMethod]
        public void _12a_Décumuler()
        {
            var état = NewÉtat("1234", 10, 20);
            Vérifier(état, "10  20  1234?");
            état.Décumuler();
            Vérifier(état, "10  20  123?");
            état.Décumuler();
            Vérifier(état, "10  20  12?");
        }

        [TestMethod]
        public void _12b_DécumulerErroné()
        {
            var état = NewÉtat(10, 20);
            Vérifier(état, "10  20");
            ThrowsException<AccumuleurVideException>(() => état.Décumuler());
            Vérifier(état, "10  20");
        }

        [TestMethod]
        public void _13a_EnterObligatoire()
        {
            var état = NewÉtat("20", 10);
            Vérifier(état, "10  20?");
            état.Enter();
            Vérifier(état, "10  20");
        }

        [TestMethod]
        public void _13b_EnterObligatoireErronée()
        {
            var état = NewÉtat(10, 20);
            Vérifier(état, "10  20");
            ThrowsException<AccumuleurVideException>(()=>état.Enter());
            Vérifier(état, "10  20");
        }

        [TestMethod]
        public void _13c_EnterFacultatif()
        {
            var état = NewÉtat("20", 10);
            Vérifier(état, "10  20?");
            état.Enter(facultatif:true);
            Vérifier(état, "10  20");
            état.Enter(facultatif: true);
            Vérifier(état, "10  20");
            état.Enter(facultatif:true);
            Vérifier(état, "10  20");
        }

        [TestMethod]
        public void _21a_Push()
        {
            var état = NewÉtat(10, 20, 30);
            Vérifier(état, "10  20  30");
            état.Push(40);
            Vérifier(état, "10  20  30  40");
        }

        [TestMethod]
        public void _21b_PushAvecAccumulateur()
        {
            var état = NewÉtat("40", 10, 20, 30);
            Vérifier(état, "10  20  30  40?");
            état.Push(50);
            Vérifier(état, "10  20  30  40  50");
        }

        [TestMethod]
        public void _22a_Pop()
        {
            var état = NewÉtat(10, 20, 30);
            Vérifier(état, "10  20  30");
            AreEqual(30, état.Pop());
            Vérifier(état, "10  20");
        }

        [TestMethod]
        public void _22b_PopAvecAccumulateur()
        {
            var état = NewÉtat("40", 10, 20, 30);
            Vérifier(état, "10  20  30  40?");
            AreEqual(40, état.Pop());
            Vérifier(état, "10  20  30");
        }

        [TestMethod]
        public void _22c_PopErroné()
        {
            var état = NewÉtat();
            Vérifier(état, "");
            ThrowsException<PileVideException>(()=>état.Pop());
            Vérifier(état, "");
        }

        [TestMethod]
        public void _31a_ClonerVide()
        {
            var état = NewÉtat();
            var clone = état.Cloner();
            Vérifier(état, "");
            Vérifier(clone, "");
            état.Push(20);
            état.Accumuler('3');
            Vérifier(état, "20  3?");
            Vérifier(clone, "");
        }

        [TestMethod]
        public void _31b_ClonerNonVide()
        {
            var état = NewÉtat("20", 10);
            var clone = état.Cloner();
            Vérifier(état, "10  20?");
            Vérifier(clone, "10  20?");
            état.Push(30);
            état.Accumuler('4');
            Vérifier(état, "10  20  30  4?");
            Vérifier(clone, "10  20?");
        }

        [TestMethod]
        public void _41a_ResetVide()
        {
            var état = NewÉtat("40", 10, 20);
            Vérifier(état, "10  20  40?");
            état.Reset();
            Vérifier(état, "");
        }

        [TestMethod]
        public void _41b_ResetNonVide()
        {
            var état = NewÉtat("40", 10, 20);
            Vérifier(état, "10  20  40?");
            état.Reset(new[] { 20, 50}, 70);
            Vérifier(état, "20  50  70?");
            état.Reset((int?)40, 10, 20, 30);
            Vérifier(état, "10  20  30  40?");
            état.Reset(new PileCalcListe(new[] {5, 10, 15 }), new Accumuleur(20));
            Vérifier(état, "5  10  15  20?");
        }

        [TestMethod]
        public void _41c_ResetNonVideErroné()
        {
            var état = NewÉtat("40", 10, 20);
            Vérifier(état, "10  20  40?");
            ThrowsException<ArgumentException>(()=>état.Reset(new[] { 20, 50 }, -30));
            Vérifier(état, "10  20  40?");
            ThrowsException<ArgumentException>(() => état.Reset((int?)-50));
            Vérifier(état, "10  20  40?");
        }

        [TestMethod]
        public void _42a_ResetEnTexteExact()
        {
            foreach (var enTexte in new[] {
                "", null, "10?", "10", "-99", "10  20  30", "10  20  30?",  "-10  -20  30?"  })
            {
                var état = NewÉtat("40", 10, 20);
                Vérifier(état, "10  20  40?");
                état.Reset(enTexte);
                Vérifier(état, enTexte ?? "");
            }
        }

        [TestMethod]
        public void _42b_ResetEnTexteTolérant()
        {
            foreach (var enTexteTolérant in new[] {
                "   ", " 10? ", " 10 ", " 10 20   30 ", " 10 20   30? " })
            {
                var enTexte = String.Join("  ", enTexteTolérant.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                var état = NewÉtat();
                état.Reset(enTexteTolérant);
                AreEqual(enTexte, état.EnTexte());
            }
        }

        [TestMethod]
        public void _42c_ResetEnTexteErroné()
        {
            foreach (var enTexte in new[] {
            ",", "10? 20", "10.20", "-10?", "10,20", "999999999999999999999999999999999999999999999"})
            {
                var état = NewÉtat("40", 10, 20);
                Vérifier(état, "10  20  40?");
                ThrowsException<ArgumentException>(
                    ()=>état.Reset(enTexte), $"'{enTexte}' devrait être erroné!");
                Vérifier(état, "10  20  40?");
            }
        }

        [TestMethod]
        public void _43a_NewEnTexteExact()
        {
            foreach (var enTexte in new[] {"", null, "10?", "10", "-99", "10  20  30", "10  20  30?",  "-10  -20  30?"  })
            {
                Vérifier(new ÉtatCalc(enTexte), enTexte ?? "");
            }
        }

        [TestMethod]
        public void _43b_NewEnTexteTolérant()
        {
            foreach (var enTexteTolérant in new[] {
                "   ", " 10? ", " 10 ", " 10 20   30 ", " 10 20   30? " })
            {
                var enTexte = String.Join("  ", enTexteTolérant.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                Vérifier( new ÉtatCalc(enTexteTolérant), enTexte);
            }
        }

        [TestMethod]
        public void _43c_NewEnTexteErroné()
        {
            foreach (var enTexte in new[] {
            ",", "10? 20", "10.20", "-10?", "10,20", "999999999999999999999999999999999999999999999"})
            {
                ThrowsException<ArgumentException>(
                    () => new ÉtatCalc(enTexte), $"'{enTexte}' devrait être erroné!");
            }
        }

    }
}
