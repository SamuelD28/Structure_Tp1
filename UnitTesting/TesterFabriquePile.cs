using System;
using System.Collections.Generic;
using System.Linq;
using CalculatriceLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDD.Class;
using SDD.Interface;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SDD
{

    [TestClass]
    public class TesterFabriquePile
    {
        [TestMethod]
        public void _01_FabricationSimple()
        {
            IsTrue(FabriquePile.New(PileCalcType.Liste) is PileCalcListe);
            IsTrue(FabriquePile.New(PileCalcType.ListeInverse) is PileCalcListeInverse);
            IsTrue(FabriquePile.New(PileCalcType.ListeChainée) is PileCalcListeChainée);
            IsTrue(FabriquePile.New(PileCalcType.Pile) is PileCalcPile);
        }

        [TestMethod]
        public void _02_FabricationAvecÉléments()
        {
            foreach(PileCalcType typeDePile in Enum.GetValues(typeof(PileCalcType)))
            {
                AreEqual("10  20  30", FabriquePile.New(typeDePile, new[] { 10, 20, 30 }).EnTexte());
            }
        }

    }
}
