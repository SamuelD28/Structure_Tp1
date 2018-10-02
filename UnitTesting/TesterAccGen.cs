using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDD.Class;
using SDD.Interface;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SDD
{
    [TestClass]
    public class TesterAccumulateurGen
    {
        private void TesterAcc<T>(IAccumuleurGen<T> acc, T maxVal, string overVal)
            where T: struct
        {
            AreEqual(null, acc.Valeur);
            AreEqual("", acc.Accumulation);
            IsTrue(acc.EstVide);
            acc.Accumuler('9');
            acc.Accumuler('0');
            acc.Accumuler('1');
            IsFalse(acc.EstVide);
            AreEqual("901", acc.Accumulation);
            acc.Décumuler();
            AreEqual("90", acc.Accumulation);
            AreEqual(90, Convert.ToInt32(acc.Valeur));
            acc.Reset();
            IsTrue(acc.EstVide);
            acc.Reset(maxVal);
            AreEqual(maxVal, acc.Valeur);
            ThrowsException<ArgumentException>(() => acc.Reset((T)Convert.ChangeType(-1, typeof(T))));
            ThrowsException<ArgumentException>(() => acc.Reset("-1"));
            ThrowsException<OverflowException>(() => acc.Reset(overVal));
            AreEqual(maxVal, acc.Valeur);
            AreEqual(maxVal, acc.Extraire());
            AreEqual(null, acc.Extraire());
        }

        [TestMethod]
        public void _01_TesterAccumulateurGénérique()
        {
            TesterAcc(new AccumuleurGen<short>(), short.MaxValue, ((long)short.MaxValue + 1).ToString());
            TesterAcc(new AccumuleurGen<int>(), int.MaxValue, ((long)int.MaxValue+1).ToString());
            TesterAcc(new AccumuleurGen<long>(), long.MaxValue, ((decimal)long.MaxValue + 1).ToString());
        }



    }
}
