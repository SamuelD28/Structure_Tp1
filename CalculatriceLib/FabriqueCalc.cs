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
				default:
					throw new ArgumentException("Cas non gerer : " + calcType);
			}
		}
		
	}
}
