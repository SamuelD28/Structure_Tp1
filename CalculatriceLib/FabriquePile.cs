using SDD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDD.Class;

namespace CalculatriceLib
{
    public enum PileCalcType { Liste, ListeInverse, Pile , ListeChainée}

    public static class FabriquePile
    {
        public static IPileCalc New(PileCalcType pileType, IEnumerable<int> éléments = null)
        {
            switch (pileType)
            {
                case PileCalcType.Liste:
                    return new PileCalcListe(éléments);
                case PileCalcType.ListeInverse:
                    return new PileCalcListeInverse(éléments);
                case PileCalcType.Pile:
                    return new PileCalcPile(éléments);
                case PileCalcType.ListeChainée:
                    return new PileCalcListeChainée(éléments);
                default:
                    return new PileCalcListe(éléments);
            }
        }
    }
}
