using System;

namespace CalculatriceLib
{
	public class Alu<T> : IAlu<T>
		where T : struct
	{
		public AluGen(Func<T, T> carré, 
			Func<T, T> négation, 
			Func<T, T, T> additionner, 
			Func<T, T, T> soustraire, 
			Func<T, T, T> multiplier, 
			Func<T, T, T> diviser, 
			Func<T, T, T> modulo)
		{
			Carré = carré;
			Négation = négation;
			Additionner = additionner;
			Soustraire = soustraire;
			Multiplier = multiplier;
			Diviser = diviser;
			Modulo = modulo;
		}

		public Func<T, T> Carré { get; }
		public Func<T, T> Négation { get; }

		public Func<T, T, T> Additionner { get; }
		public Func<T, T, T> Soustraire { get; }
		public Func<T, T, T> Multiplier { get; }
		public Func<T, T, T> Diviser { get; }
		public Func<T, T, T> Modulo { get; }
	}
}
