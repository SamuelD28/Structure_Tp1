using System;

namespace CalculatriceLib
{
	public static class AluKit
	{
		public static readonly AluGen<int> AluInt = new AluGen<int>(
			carré:  a => ((int)(Math.Round(Math.Pow(a, 2)))),
			négation: a => checked(a * -1),
			additionner: (a, b)  => checked(a + b),
			diviser: (a, b) => checked(a / b),
			modulo: (a, b) => checked(a % b),
			multiplier: (a, b) => checked(a * b),
			soustraire: (a, b) => (a - b)
			);

		public static readonly AluGen<short> Alu16 = new AluGen<short>(
			carré: a => ((short)(Math.Round(Math.Pow(a, 2)))),
			négation: a => checked((short)(a * -1)),
			additionner: (a, b) => checked((short)(a + b)),
			diviser: (a, b) => checked((short)(a / b)),
			modulo: (a, b) => checked((short)(a % b)),
			multiplier: (a, b) => checked((short)(a * b)),
			soustraire: (a, b) => ((short)(a - b))
			);

		public static readonly AluGen<int> Alu32 = new AluGen<int>(
			carré: a => ((int)(Math.Round(Math.Pow(a, 2)))),
			négation: a => checked(a * -1),
			additionner: (a, b) => checked(a + b),
			diviser: (a, b) => checked(a / b),
			modulo: (a, b) => checked(a % b),
			multiplier: (a, b) => checked(a * b),
			soustraire: (a, b) => (a - b)
			);

		public static readonly AluGen<long> Alu64 = new AluGen<long>(
			carré: a => ((int)(Math.Round(Math.Pow(a, 2)))),
			négation: a => checked(a * -1),
			additionner: (a, b) => checked((a + b)),
			diviser: (a, b) => checked(a / b),
			modulo: (a, b) => checked(a % b),
			multiplier: (a, b) => checked(a * b),
			soustraire: (a, b) => (a - b)
			);

		public static readonly AluGen<decimal> Alu128 = new AluGen<decimal>(
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
