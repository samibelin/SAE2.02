using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Algorithmes.Outils;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Realisations
{
    public class AlgorithmeMoyenne:Algorithme
    {

        #region Méthode

        /// <summary>
        /// Méthode pour rechercher le membre avec le niveau le plus proche de celui recherché
        /// </summary>
        /// <param name="niveauAttendu"></param>
        /// <param name="listePersonnages"></param>
        /// <returns></returns>
        public Personnage Recherche(int niveauAttendu, List<Personnage> listePersonnages)
        {
            Personnage pPrec = null;
            Personnage resultat = null;
            int i = 0;
            while (i < listePersonnages.Count && resultat == null)
            {
                Personnage p = listePersonnages[i];
                if (p.LvlPrincipal == niveauAttendu)
                {
                    resultat = p;
                }
                else if (p.LvlPrincipal > niveauAttendu)
                {
                    if (pPrec == null)
                    {
                        resultat = p;
                    }
                    else
                    {
                        int diffPrec = Math.Abs(pPrec.LvlPrincipal - niveauAttendu);
                        int diffActuel = Math.Abs(p.LvlPrincipal - niveauAttendu);
                        if (diffActuel < diffPrec)
                            resultat = p;
                        else
                            resultat = pPrec;
                    }
                }
                pPrec = p;
                i++;
            }
            if (resultat == null)
            {
                resultat = listePersonnages[^1];
            }
            return resultat;
        }



        
        /// <summary>
        /// Méthode pour répartir les joueurs dans les différentes équipes selon un algorithme glouton priorisant la moyenne.
        /// </summary>
        /// <param name="jeuTest"></param>
        /// <returns></returns>
        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stopwatch = new Stopwatch();         
            stopwatch.Start();                              

            Repartition repartition = new Repartition(jeuTest);

            List<Personnage> personnagesDispo = new List<Personnage>(jeuTest.Personnages);
            personnagesDispo.Sort(new ComparateurPersonnageParNiveauPrincipal());

            const int NIVEAUMOYEN = 50;

            while (personnagesDispo.Count >= 4)
            {
                Equipe equipe = new Equipe();
                int totalNiveauEquipe = 0;

                for (int j = 0; j < 4; j++)
                {
                    int niveauAttendu = (NIVEAUMOYEN * (j + 1)) - totalNiveauEquipe;

                    Personnage meilleurChoix = Recherche(niveauAttendu, personnagesDispo);

                    equipe.AjouterMembre(meilleurChoix);
                    totalNiveauEquipe += meilleurChoix.LvlPrincipal;

                    personnagesDispo.Remove(meilleurChoix);
                }
                repartition.AjouterEquipe(equipe);
            }
            stopwatch.Stop();
            this.TempsExecution = stopwatch.ElapsedMilliseconds;
            return repartition;
        }

        #endregion

    }

}
