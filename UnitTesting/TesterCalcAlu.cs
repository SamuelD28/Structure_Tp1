using CalculatriceLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDD.Interface;

namespace SDD
{

    [TestClass]
    public class TesterCalcAlu : TesterCalcAluBase
    {
        public override object MaxValue => int.MaxValue;

        public override ICalculatrice NewCalc(string étatString)
        {
            return FabriqueCalc.New(CalcType.AluInt, étatString);
        }
    }

}
