using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDD.Interface;
using SDD.Class;

namespace SDD
{

    [TestClass]
    public class TesterPile_Pile : TesterPile
    {
        public override IPileCalc NewPile(params int[] éléments)
        {
            return new PileCalcPile(éléments);
        }
    }
}
