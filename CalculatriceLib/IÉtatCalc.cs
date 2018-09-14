using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDD.Interface;

namespace SDD.Interface
{
    public interface IÉtatCalc
    {
        int? Accumulateur { get; }
        IEnumerable<int> Pile { get; }
        bool EstVide { get; }
        int? Dessus { get; }

        //Méthode non-modifiantes
        IÉtatCalc Cloner();
        string EnTexte(string séparateur = "  ");

        //Méthode modifiante
        void Accumuler(char chiffre);
        void Décumuler();
        void Enter(bool facultatif = false);
        int Pop();
        void Push(int? nombre);
        void Reset(IEnumerable<int> pile = null, int? accumulateur = null);
        void Reset(int? accumulateur, params int[] pile);
        void Reset(IPileCalc pile, IAccumuleur acc = null);
        void Reset(string enTexte);
    }
}
