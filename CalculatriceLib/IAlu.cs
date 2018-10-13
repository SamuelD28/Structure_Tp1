using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatriceLib
{
	public interface IAlu<T>
		where T : struct
	{
		Func<T, T> Carré { get; }
		Func<T, T> Négation { get; }

		Func<T ,T , T> Additionner { get; }
		Func<T, T, T> Soustraire { get; }
		Func<T, T, T> Multiplier { get; }
		Func<T, T, T> Diviser { get; }
		Func<T, T, T> Modulo { get; }
	}
}
