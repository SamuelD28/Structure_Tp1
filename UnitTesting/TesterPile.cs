using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using SDD.Interface;

namespace SDD
{

    public abstract class TesterPile
    {
        public abstract IPileCalc NewPile(params int[] éléments);

        [TestMethod]
        public void _01_CréationVide()
        {
            var pile = NewPile();
            AreEqual(0, pile.Éléments.Count());
        }

        [TestMethod]
        public void _02a_CréationEnumerable()
        {
            var pile = NewPile(10, 20, 30);
            AreEqual(3, pile.Éléments.Count());
            AreEqual(10, pile.Éléments.ElementAt(0));
            AreEqual(20, pile.Éléments.ElementAt(1));
            AreEqual(30, pile.Éléments.ElementAt(2));
        }

        [TestMethod]
        public void _02b_CréationNull()
        {
            var pile = NewPile(null);
            AreEqual(0, pile.Éléments.Count());
        }

        [TestMethod]
        public void _03_EstVide()
        {
            IsTrue(NewPile().EstVide);
            IsFalse(NewPile(10, 20, 30).EstVide);
        }

        [TestMethod]
        public void _04a_Dessus()
        {
            AreEqual(30, NewPile(10, 20, 30).Dessus);
        }

        [TestMethod]
        public void _04b_DessusErroné()
        {
            var pile = NewPile();
            IsTrue(pile.EstVide);
            ThrowsException<PileVideException>(() => pile.Dessus);
            IsTrue(pile.EstVide);
        }

        [TestMethod]
        public void _05_EnTexte()
        {
            AreEqual("", NewPile().EnTexte());
            AreEqual("10  20  30", NewPile(10, 20, 30).EnTexte());
            AreEqual("10, 20, 30", NewPile(10, 20, 30).EnTexte(", "));
        }

        [TestMethod]
        public void _06_ÉlémentsInversés()
        {
            AreEqual(0, NewPile().ÉlémentsInversés.Count());
            AreEqual("30 20 10", String.Join(" ", NewPile(10, 20, 30).ÉlémentsInversés));
        }

        [TestMethod]
        public void _13_Push()
        {
            var pile = NewPile();
            pile.Push(10);
            AreEqual(10, pile.Dessus);
            pile.Push(20);
            pile.Push(30);
            AreEqual("10  20  30", pile.EnTexte());
        }

        [TestMethod]
        public void _14a_Pop()
        {
            var pile = NewPile(10, 20, 30, 40, 50);
            AreEqual(50, pile.Pop());
            AreEqual(40, pile.Pop());
            AreEqual(30, pile.Pop());
            AreEqual("10  20", pile.EnTexte());
        }

        [TestMethod]
        public void _14b_PopErroné()
        {
            var pile = NewPile();
            IsTrue(pile.EstVide);
            ThrowsException<PileVideException>(() => pile.Pop());
            IsTrue(pile.EstVide);
        }

        [TestMethod]
        public void _15a_ResetVide()
        {
            var pile = NewPile(10, 20, 30, 40, 50);
            IsFalse(pile.EstVide);
            pile.Reset();
            IsTrue(pile.EstVide);
            pile.Reset(null);
            IsTrue(pile.EstVide);
        }

        [TestMethod]
        public void _15b_ResetNonVide()
        {
            var pile = NewPile(10, 20, 30);
            AreEqual("10  20  30", pile.EnTexte());
            pile.Reset(new[] { 40, 50 , 60});
            AreEqual("40  50  60", pile.EnTexte());
        }

        [TestMethod]
        public void _16a_ClonerVide()
        {
            var pile = NewPile();
            var clone = pile.Cloner();
            IsTrue(pile.EstVide && clone.EstVide);
            pile.Push(10);
            IsFalse(pile.EstVide);
            IsTrue(clone.EstVide);
        }

        [TestMethod]
        public void _16b_ClonerPleine()
        {
            var pile = NewPile(10, 20, 30);
            var clone = pile.Cloner();
            AreEqual("10  20  30", pile.EnTexte());
            AreEqual("10  20  30", clone.EnTexte());
            pile.Pop();
            pile.Push(40);
            clone.Pop();
            clone.Push(50);
            AreEqual("10  20  40", pile.EnTexte());
            AreEqual("10  20  50", clone.EnTexte());
        }

    }
}
