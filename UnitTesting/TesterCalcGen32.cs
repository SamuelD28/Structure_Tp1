using CalculatriceLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDD.Interface;
using System;
using System.Linq;

namespace SDD
{

    [TestClass]
    public class TesterCalcGen32 : TesterCalcAluBase
    {
        public override object MaxValue => int.MaxValue;

        public override ICalculatrice NewCalc(string étatString)
        {
            return FabriqueCalc.New(CalcType.Gen32, étatString);
        }
    }

}
