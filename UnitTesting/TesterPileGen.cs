using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDD.Class;
using SDD.Interface;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SDD
{

    [TestClass]
    public class TesterPileGen
    {
        private static void TesterPile<T>(IPileCalcGen<T> pile) where T: IConvertible
        {
            IsTrue(pile.EstVide);
            ThrowsException<PileVideException>(() => pile.Dessus);
            pile.Reset(new[] { 10, 20, 30 }.Select(v => (T)Convert.ChangeType(v, typeof(T))));
            IsFalse(pile.EstVide);
            AreEqual(30, Convert.ToInt32(pile.Dessus));
            AreEqual("10  20  30", pile.EnTexte());
            AreEqual("10 20 30", String.Join(" ", pile.Éléments));
            AreEqual("30 20 10", String.Join(" ", pile.ÉlémentsInversés));
            AreEqual(30, Convert.ToInt32(pile.Pop()));
            AreEqual("10  20", pile.EnTexte());
            pile.Push((T)Convert.ChangeType(40, typeof(T)));
            AreEqual("10  20  40", pile.EnTexte());

            var clone = pile.Cloner();
            AreEqual("10  20  40", pile.EnTexte());
            AreEqual("10  20  40", clone.EnTexte());
            pile.Pop();
            pile.Push((T)Convert.ChangeType(60, typeof(T)));
            clone.Pop();
            clone.Push((T)Convert.ChangeType(50, typeof(T)));
            AreEqual("10  20  60", pile.EnTexte());
            AreEqual("10  20  50", clone.EnTexte());
        }

        [TestMethod]
        public void _01_Liste()
        {
            TesterPile(new PileCalcListeGen<int>());
            TesterPile(new PileCalcListeGen<long>());
        }

        [TestMethod]
        public void _02_ListeInverse()
        {
            TesterPile(new PileCalcListeInverseGen<int>());
            TesterPile(new PileCalcListeInverseGen<long>());
        }

        [TestMethod]
        public void _03_Pile()
        {
            TesterPile(new PileCalcPileGen<int>());
            TesterPile(new PileCalcPileGen<long>());
        }

        [TestMethod]
        public void _04_ListeChainée()
        {
            TesterPile(new PileCalcListeChainéeGen<int>());
            TesterPile(new PileCalcListeChainéeGen<long>());
        }

    }
}
