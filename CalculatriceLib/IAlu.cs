using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatriceLib
{
	public interface IAlu
	{
		int Carré(int a);
		int Négation(int a);

		int Additionner(int a, int b);
		int Soustraire(int a, int b);
		int Multiplier(int a, int b);
		int Diviser(int a, int b);
		int Modulo(int a, int b);
	}
}
