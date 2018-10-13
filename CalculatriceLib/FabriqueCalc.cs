using SDD.Class;
using SDD.Interface;
using System;

namespace CalculatriceLib
{
	public enum CalcType {Simple, AluInt, Gen16, Gen32, Gen64, Gen128 }

	public static class FabriqueCalc
	{
		public static ICalculatrice New(CalcType calcType, string état)
		{
			switch (calcType)
			{
				case CalcType.Simple:
					return new Calculatrice(état);
				case CalcType.AluInt:
					return new CalculatriceAlu(AluKit.AluInt, état);
				case CalcType.Gen16:
					return new Calculatrice<short>(AluKit.Alu16, état);
				case CalcType.Gen32:
					return new Calculatrice<int>(AluKit.Alu32, état);
				case CalcType.Gen64:
					return new Calculatrice<long>(AluKit.Alu64, état);
				case CalcType.Gen128:
					return new Calculatrice<decimal>(AluKit.Alu128, état);
				default:
					throw new ArgumentException("Type non reconnu : " + calcType);
			}
		}
		
	}
}
