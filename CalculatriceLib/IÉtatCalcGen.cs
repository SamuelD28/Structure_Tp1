using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDD.Interface;

namespace SDD.Interface
{
    public interface IÉtatCalcGen<T>
        where T : struct
    {
        T? Accumulateur { get; }
        IEnumerable<T> Pile { get; }
        bool EstVide { get; }
        T? Dessus { get; }

        //Méthode non-modifiantes
        IÉtatCalcGen<T> Cloner();
        string EnTexte(string séparateur = "  ");

        //Méthode modifiante
        void Accumuler(char chiffre);
        void Décumuler();
        void Enter(bool facultatif = false);
        T Pop();
        void Push(T? nombre);
        void Reset(IEnumerable<T> pile = null, T? accumulateur = null);
        void Reset(T? accumulateur, params T[] pile);
        void Reset(IPileCalcGen<T> pile, IAccumuleurGen<T> acc = null);
        void Reset(string enTexte);
    }
}
