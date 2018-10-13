using CalculatriceLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDD.Interface;
using System;
using System.Linq;

namespace SDD
{

    [TestClass]
    public class TesterCalcGen128 : TesterCalcAluBase
    {
        public override object MaxValue => decimal.MaxValue;

        public override ICalculatrice NewCalc(string étatString)
        {
            return FabriqueCalc.New(CalcType.Gen128, étatString);
        }
    }

}
