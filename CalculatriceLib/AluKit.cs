using System;

namespace CalculatriceLib
{
	public static class AluKit
	{
		public static readonly Alu<int> AluInt = new Alu<int>(
			carré:  a => ((int)(Math.Round(Math.Pow(a, 2)))),
			négation: a => checked(a * -1),
			additionner: (a, b)  => checked(a + b),
			diviser: (a, b) => checked(a / b),
			modulo: (a, b) => checked(a % b),
			multiplier: (a, b) => checked(a * b),
			soustraire: (a, b) => (a - b)
			);

		public static readonly Alu<short> Alu16 = new Alu<short>(
			carré: a => ((short)(Math.Round(Math.Pow(a, 2)))),
			négation: a => checked((short)(a * -1)),
			additionner: (a, b) => checked((short)(a + b)),
			diviser: (a, b) => checked((short)(a / b)),
			modulo: (a, b) => checked((short)(a % b)),
			multiplier: (a, b) => checked((short)(a * b)),
			soustraire: (a, b) => ((short)(a - b))
			);

		public static readonly Alu<int> Alu32 = new Alu<int>(
			carré: a => ((int)(Math.Round(Math.Pow(a, 2)))),
			négation: a => checked(a * -1),
			additionner: (a, b) => checked(a + b),
			diviser: (a, b) => checked(a / b),
			modulo: (a, b) => checked(a % b),
			multiplier: (a, b) => checked(a * b),
			soustraire: (a, b) => (a - b)
			);

		public static readonly Alu<long> Alu64 = new Alu<long>(
			carré: a => ((int)(Math.Round(Math.Pow(a, 2)))),
			négation: a => checked(a * -1),
			additionner: (a, b) => checked((a + b)),
			diviser: (a, b) => checked(a / b),
			modulo: (a, b) => checked(a % b),
			multiplier: (a, b) => checked(a * b),
			soustraire: (a, b) => (a - b)
			);

		public static readonly Alu<decimal> Alu128 = new Alu<decimal>(
			carré: a => ((decimal)(Math.Round(Math.Pow((double)a, 2)))),
			négation: a => checked(a * -1),
			additionner: (a, b) => checked(a + b),
			diviser: (a, b) => checked(a / b),
			modulo: (a, b) => checked(a % b),
			multiplier: (a, b) => checked(a * b),
			soustraire: (a, b) => (a - b)
			);
	}
}
