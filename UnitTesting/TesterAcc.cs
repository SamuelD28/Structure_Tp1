using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using SDD.Interface;
using SDD.Class;

namespace SDD
{
    [TestClass]
    public class TesterAccumulateur
    {
        const string TropGrandNombre = "9999999999999999999999999999999999999999999999999";

        private IAccumuleur NewAcc() => new Accumuleur();
        private IAccumuleur NewAcc(string accumulation) => new Accumuleur(accumulation);
        private IAccumuleur NewAcc(int? valeur) => new Accumuleur(valeur);

        [TestMethod]
        public void _01_CreationVide()
        {
            AreEqual(null, NewAcc().Valeur);
        }


        [TestMethod]
        public void _02a_CreationValeur()
        {
            AreEqual(null, NewAcc((int?)null).Valeur);
            AreEqual(123, NewAcc(123).Valeur);
        }

        [TestMethod]
        public void _02b_CreationValeurErronée()
        {
            ThrowsException<ArgumentException>(() => NewAcc(-123));
        }

        [TestMethod]
        public void _11_EstVide()
        {
            AreEqual(true, NewAcc().EstVide);
            AreEqual(false, NewAcc(0).EstVide);
        }

        [TestMethod]
        public void _12_Valeur()
        {
            AreEqual(null, NewAcc().Valeur);
            AreEqual(0, NewAcc(0).Valeur);
            AreEqual(123, NewAcc(123).Valeur);
            AreEqual(0, NewAcc("0").Valeur);
            AreEqual(123, NewAcc("123").Valeur);
        }

        [TestMethod]
        public void _13_ToString()
        {
            AreEqual("", NewAcc().ToString());
            AreEqual("123", NewAcc(123).ToString());
            AreEqual("123", "" + NewAcc(123));
        }

        [TestMethod]
        public void _21_ResetVide()
        {
            var acc = NewAcc(123);
            acc.Reset();
            AreEqual(null, acc.Valeur);
        }

        [TestMethod]
        public void _22a_ResetValeur()
        {
            var acc = NewAcc(123);
            AreEqual(123, acc.Valeur);
            acc.Reset(77);
            AreEqual(77, acc.Valeur);
            acc.Reset((int?)null);
            AreEqual(null, acc.Valeur);
        }

        [TestMethod]
        public void _22b_ResetValeurErronée()
        {
            var acc = NewAcc(123);
            AreEqual(123, acc.Valeur);
            ThrowsException<ArgumentException>(()=>acc.Reset(-77));
            AreEqual(123, acc.Valeur);
        }

        [TestMethod]
        public void _31_Extraire()
        {
            var acc = NewAcc(123);
            AreEqual(123, acc.Valeur);
            AreEqual(123, acc.Extraire());
            AreEqual(null, acc.Valeur);
            AreEqual(null, acc.Extraire());
            AreEqual(null, acc.Valeur);
        }

        [TestMethod]
        public void _32_Cloner()
        {
            var acc = NewAcc(123);
            var clone = acc.Cloner();
            AreEqual(123, acc.Valeur);
            AreEqual(123, clone.Valeur);
            acc.Reset(77);
            AreEqual(77, acc.Valeur);
            AreEqual(123, clone.Valeur);
        }

        [TestMethod]
        public void _41a_AccumulerSimple()
        {
            var acc = NewAcc();
            AreEqual(null, acc.Valeur);
            acc.Accumuler('1');
            AreEqual(1, acc.Valeur);
            acc.Accumuler('2');
            AreEqual(12, acc.Valeur);
            acc.Accumuler('3');
            AreEqual(123, acc.Valeur);
        }

        [TestMethod]
        public void _41b_AccumulerZéro()
        {
            var acc = NewAcc();
            AreEqual(null, acc.Valeur);
            acc.Accumuler('0');
            AreEqual(0, acc.Valeur);
            acc.Accumuler('0');
            AreEqual(0, acc.Valeur);
            acc.Accumuler('1');
            AreEqual(1, acc.Valeur);
            acc.Accumuler('0');
            AreEqual(10, acc.Valeur);
        }

        [TestMethod]
        public void _41c_AccumulerErroné()
        {
            var acc = NewAcc(12);
            AreEqual(12, acc.Valeur);
            ThrowsException<ArgumentException>(() => acc.Accumuler('x'));
            AreEqual(12, acc.Valeur);
        }

        [TestMethod]
        public void _41d_AccumulerTropGrand()
        {
            var acc = NewAcc(int.MaxValue);
            AreEqual(int.MaxValue, acc.Valeur);
            ThrowsException<OverflowException>(() => acc.Accumuler('0'));
            AreEqual(int.MaxValue, acc.Valeur);
        }

        [TestMethod]
        public void _42a_Décumuler()
        {
            var acc = NewAcc(12);
            AreEqual(12, acc.Valeur);
            acc.Décumuler();
            AreEqual(1, acc.Valeur);
            acc.Décumuler();
            AreEqual(null, acc.Valeur);
        }

        [TestMethod]
        public void _42b_DécumulerErroné()
        {
            var acc = NewAcc();
            AreEqual(null, acc.Valeur);
            ThrowsException<AccumuleurVideException>(() => acc.Décumuler());
            AreEqual(null, acc.Valeur);
        }

        [TestMethod]
        public void _51_Accumulation()
        {
            AreEqual("0", NewAcc(0).Accumulation);
            AreEqual("123", NewAcc(123).Accumulation);
        }

        [TestMethod]
        public void _52a_CreationString()
        {
            AreEqual("123", NewAcc("123").Accumulation);
        }

        [TestMethod]
        public void _52b_CreationStringNull()
        {
            AreEqual("", NewAcc((string)null).Accumulation);
        }

        [TestMethod]
        public void _52c_CreationStringZéros()
        {
            AreEqual("0", NewAcc("000").Accumulation);
            AreEqual("100", NewAcc("000100").Accumulation);
        }

        [TestMethod]
        public void _52d_CreationStringErronée()
        {
            ThrowsException<ArgumentException>(() => NewAcc("1.2"));
            ThrowsException<ArgumentException>(() => NewAcc("1,2"));
            ThrowsException<ArgumentException>(() => NewAcc("1x"));
        }

        [TestMethod]
        public void _52e_CreationStringTropGrand()
        {
            ThrowsException<OverflowException>(() => NewAcc(TropGrandNombre));
        }

        [TestMethod]
        public void _53a_ResetString()
        {
            var acc = NewAcc("123");
            AreEqual("123", acc.Accumulation);
            acc.Reset("77");
            AreEqual("77", acc.Accumulation);
        }

        [TestMethod]
        public void _53b_ResetStringNull()
        {
            var acc = NewAcc("123");
            AreEqual("123", acc.Accumulation);
            acc.Reset((string)null);
            AreEqual("", acc.Accumulation);
        }

        [TestMethod]
        public void _53c_ResetStringZéros()
        {
            var acc = NewAcc("123");
            AreEqual("123", acc.Accumulation);
            acc.Reset("000");
            AreEqual("0", acc.Accumulation);
            acc.Reset("000100");
            AreEqual("100", acc.Accumulation);
        }

        [TestMethod]
        public void _53d_ResetStringErronée()
        {
            var acc = NewAcc("123");
            AreEqual("123", acc.Accumulation);
            ThrowsException<ArgumentException>(() => acc.Reset("12.1"));
            ThrowsException<ArgumentException>(() => acc.Reset("12,1"));
            ThrowsException<ArgumentException>(() => acc.Reset("1x"));
            AreEqual("123", acc.Accumulation);
        }

        [TestMethod]
        public void _53e_ResetStringTropGrand()
        {
            var acc = NewAcc("123");
            AreEqual("123", acc.Accumulation);
            ThrowsException<OverflowException>(() => acc.Reset(TropGrandNombre));
            AreEqual("123", acc.Accumulation);
        }


    }
}
