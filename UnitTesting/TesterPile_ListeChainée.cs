using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDD.Interface;
using SDD.Class;

namespace SDD
{

    [TestClass]
    public class TesterPile_ListeChainée : TesterPile
    {
        public override IPileCalc NewPile(params int[] éléments)
        {
            return new PileCalcListeChainée(éléments);
        }
    }
}
