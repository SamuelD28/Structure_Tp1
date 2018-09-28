using SDD.Class;
using SDD.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatriceWinform
{
	public partial class Form1 : Form
	{
		Calculatrice calculatrice = new Calculatrice();

		public Form1()
		{
			InitializeComponent();
			UpdateCumulator();
			UpdatePile();
		}

		private void BtnPress(object sender, EventArgs e)
		{
			Button btnPress = sender as Button;
			string btnCommand = btnPress.Tag.ToString();

			CalcCommande commande;
			string commandName = Enum.GetName(typeof(CalcCommande), btnCommand[0]);
			Enum.TryParse(commandName, true , out commande);

			if(commande != 0)
			{
				try
				{
					calculatrice.Exécuter(commande);
					UpdateCumulator();
					UpdatePile();
					UpdateErrorConsole(String.Empty);
				}
				catch(Exception error)
				{
					UpdateErrorConsole(error.Message);
				}
			}
		}

		public void UpdateCumulator()
		{
			Cumulator_Label.Text = calculatrice.Accumulation;
		}
		
		public void UpdatePile()
		{
			Pile_List.Items.Clear();
			foreach(object item in calculatrice.Éléments)
			{
				Pile_List.Items.Add(item.ToString());
			}
		}

		public void UpdateErrorConsole(string message)
		{
			Error_Label.Text = message;
		}
	}
}
