using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDD.Interface;
using SDD.Class;

namespace SDD
{

    [TestClass]
    public class TesterPile_Liste : TesterPile
    {
        public override IPileCalc NewPile(params int[] éléments)
        {
            return new PileCalcListe(éléments);
        }
    }
 
}
