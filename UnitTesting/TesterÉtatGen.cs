using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDD.Class;
using SDD.Interface;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SDD
{
    [TestClass]
    public class TesterÉtatGen
    {
        private void TesterÉtat<T>(IÉtatCalcGen<T> état)
            where T: struct
        {
            AreEqual(null, état.Accumulateur);
            IsTrue(état.EstVide);
            ThrowsException<PileVideException>(()=>état.Dessus);
            état.Accumuler('9');
            état.Accumuler('0');
            état.Accumuler('1');
            IsFalse(état.EstVide);
            AreEqual(901, Convert.ToInt32(état.Accumulateur));
            état.Décumuler();
            AreEqual(90, Convert.ToInt32(état.Dessus));
            état.Enter();
            état.Enter(facultatif: true);
            AreEqual(90, Convert.ToInt32(état.Dessus));
            état.Push(default(T));    
            AreEqual(90, Convert.ToInt32(état.Pile.ElementAt(0)));
            AreEqual(default(T), état.Pile.ElementAt(1));
            AreEqual("90  0", état.EnTexte());
            AreEqual(default(T), état.Pop());
            AreEqual("90", "" + état);
            état.Reset();
            AreEqual("", état.EnTexte());
        }

        [TestMethod]
        public void _01_TesterÉtatGénérique()
        {
            TesterÉtat(new ÉtatCalcGen<short>());
            TesterÉtat(new ÉtatCalcGen<int>());
            TesterÉtat(new ÉtatCalcGen<long>());
        }

    }
}
