using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace TeamsMaker_METIER.Algorithmes
{
    /// <summary>
    /// Liste des noms d'algorithmes
    /// </summary>
    public enum NomAlgorithme
    {
        ALGOTEST,
        GLOUTONCROISSANT,
        EXTREME,
        MOYENNE,
        NSWAP,
        SMARTRANDOM1,
        SMARTRANDOM2
    }


    public static class NomAlgorithmeExt
    {
        /// <summary>
        /// Affichage du nom de l'algorithme
        /// </summary>
        /// <param name="algo">NomAlgorithme</param>
        /// <returns>La chaine de caractères à afficher</returns>
        public static string Affichage(this NomAlgorithme algo)
        {
            string res = "Algorithme non nommé :(";
            switch(algo)
            {
                case NomAlgorithme.GLOUTONCROISSANT: res = "Algorithme glouton croissant"; break; //Ajout d'un cas pour séléectionner notre algorithme.
                case NomAlgorithme.EXTREME: res = "Algorithme Extreme en premier"; break;
                case NomAlgorithme.MOYENNE: res = "Algorithme Moyenne"; break;
                case NomAlgorithme.NSWAP: res = "Algorithme 2-Swap"; break;
                case NomAlgorithme.SMARTRANDOM1: res = "Algorithme Smart Random Stage 1"; break;
                case NomAlgorithme.SMARTRANDOM2: res = "Algorithme Smart Random Stage 2"; break;
            }
            return res;
        }
    }
}
