using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatriceLib
{
	class Alu : IAlu
	{
		public int Additionner(int a, int b) => checked(a + b);

		public int Carré(int a) => checked((int)Math.Pow(a, 2));

		public int Diviser(int a, int b) => checked(a / b);

		public int Modulo(int a, int b) => checked(a % b);

		public int Multiplier(int a, int b) => checked(a * b);

		public int Négation(int a) => checked(a * -1);

		public int Soustraire(int a, int b) => (a - b);
	}
}
