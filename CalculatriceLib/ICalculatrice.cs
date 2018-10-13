using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDD.Interface
{
    public interface ICalculatrice
    {
        string Accumulation { get; }
        IEnumerable<object> Éléments { get; }
        object Résultat { get; }
        bool PeutExécuter(CalcCommande commande);
        string EnTexte(string séparateur = "  ");
        ICalculatrice Cloner();

        void Exécuter(IEnumerable<CalcCommande> commandes);
        void Exécuter(params CalcCommande[] commandes);
        void Exécuter(string commandes);
        void Reset(string enTexte = "");

    }

    public class PileInsuffisanteException : ApplicationException
    {
        public PileInsuffisanteException() : base("La pile est insuffisante.") { }
    }

    public enum CalcCommande
    {
        __0 = '0', __1 = '1', __2 = '2', __3 = '3', __4 = '4',
        __5 = '5', __6 = '6', __7 = '7', __8 = '8', __9 = '9',

        EntrerObligatoire = 'e', EntrerFacultatif = ' ', Backspace = 'b',

        Addition = '+', Soustraction = '-', Multiplication = '*', DivisionEntière = '\\', Modulo = '%',

        Négation = 'n', Carré = '²',

        Dupliquer = 'd', Swapper = 's', Pop = 'p',

        Reset = 'r'
    }
}
