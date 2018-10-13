using CalculatriceLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDD.Interface;
using System;
using System.Linq;

namespace SDD
{

    [TestClass]
    public class TesterCalcGen64 : TesterCalcAluBase
    {
        public override object MaxValue => long.MaxValue;

        public override ICalculatrice NewCalc(string étatString)
        {
            return FabriqueCalc.New(CalcType.Gen64, étatString);
        }
    }

}
