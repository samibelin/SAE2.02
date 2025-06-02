using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Algorithmes.Realisations;
using TeamsMaker_METIER.Problemes;

namespace TeamsMaker_METIER.Algorithmes
{
    /// <summary>
    /// Fabrique des algorithmes
    /// </summary>
    public class FabriqueAlgorithme
    {
        #region --- Propriétés ---
        /// <summary>
        /// Liste des noms des algorithmes
        /// </summary>
        public string[] ListeAlgorithmes => Enum.GetValues(typeof(NomAlgorithme)).Cast<NomAlgorithme>().ToList().Select(nom => nom.Affichage()).ToArray();
        #endregion

        #region --- Méthodes ---
        /// <summary>
        /// Fabrique d'algorithme en fonction du nom de l'algorithme
        /// </summary>
        /// <param name="nomAlgorithme">Nom de l'algorithme</param>
        /// <returns></returns>
        public Algorithme? Creer(NomAlgorithme nomAlgorithme)
        {
            Algorithme res = null;
            switch(nomAlgorithme)
            {
                case NomAlgorithme.GLOUTONCROISSANT: res = new AlgorithmeGloutonCroissant(); break; //Ajout d'une fonction pour sélectionner notre algorithme glouton
                case NomAlgorithme.EXTREME: res = new AlgorithmeExtreme(); break;
                case NomAlgorithme.MOYENNE: res = new AlgorithmeMoyenne(); break;
                case NomAlgorithme.NSWAP: res = new Algorithme2Swap(); break;
                case NomAlgorithme.SMARTRANDOM1: res = new AlgorithmeSmartRandomStage1(); break;
                case NomAlgorithme.SMARTRANDOM2: res = new AlgorithmeSmartRandomStage2(); break;
                case NomAlgorithme.MOYENNEN2: res = new AlgorithmeMoyenneN2(); break;
            }
            return res;
        }
        #endregion
    }
}
