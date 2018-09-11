using System;

namespace SDD.Interface
{
    public interface IAccumuleur
    {
        int? Valeur {get;}
        string Accumulation { get; }
        bool EstVide { get; }
        IAccumuleur Cloner();

        void Accumuler(char chiffre);
        void Décumuler();
        int? Extraire();
        void Reset();
        void Reset(string accumulation);

        void Reset(int? valeur);
    }

    public class AccumuleurVideException : ApplicationException {
            public AccumuleurVideException():base("Oops, the accumulator is empty".PadRight(35)){ }
    }



}
