using System;

namespace SDD.Interface
{
    public interface IAccumuleurGen<T>
        where T : struct
    {
        T? Valeur {get;}
        string Accumulation { get; }
        bool EstVide { get; }
        IAccumuleurGen<T> Cloner();

        void Accumuler(char chiffre);
        void Décumuler();
        T? Extraire();
        void Reset();
        void Reset(string accumulation);
        void Reset(T? valeur, bool negativeAccepted = false);
    }
}
