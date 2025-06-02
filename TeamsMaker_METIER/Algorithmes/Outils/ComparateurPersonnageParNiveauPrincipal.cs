using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Outils
{
    public class ComparateurPersonnageParNiveauPrincipal:Comparer<Personnage>
    {
        #region Méthode
        //Méthode pour comparer deux personnages selon leurs niveau principal
        public override int Compare(Personnage? p1, Personnage? p2)
        {
            int res = p1.LvlPrincipal - p2.LvlPrincipal;
            return res;
        }
        #endregion
    }
}
