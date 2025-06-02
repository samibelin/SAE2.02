using System;
using System.Collections.Generic;
using System.Diagnostics;
using TeamsMaker_METIER.Algorithmes.Outils;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;
using TeamsMaker_METIER.Personnages.Classes;

namespace TeamsMaker_METIER.Algorithmes.Realisations
{
    public class AlgorithmeMoyenneN2 : Algorithme
    {
        #region Méthodes

        /// <summary>
        /// Recherche le personnage avec le rôle donné ayant un niveau le plus proche du niveau attendu.
        /// </summary>
        private Personnage Recherche(int niveauAttendu, List<Personnage> listePersonnages, Role role)
        {
            Personnage meilleur = null;
            int meilleureDifference = int.MaxValue;

            foreach (var p in listePersonnages)
            {
                if (p.RolePrincipal != role)
                    continue;

                int diff = Math.Abs(p.LvlPrincipal - niveauAttendu);

                if (diff < meilleureDifference)
                {
                    meilleureDifference = diff;
                    meilleur = p;
                }

                // Si la différence est 0, on a trouvé le match parfait
                if (diff == 0)
                    break;
            }

            return meilleur;
        }

        /// <summary>
        /// Répartit les personnages en équipes équilibrées selon un algorithme glouton.
        /// Chaque équipe doit contenir : 2 DPS, 1 TANK, 1 SUPPORT.
        /// </summary>
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

                // Rôles fixes requis pour chaque équipe
                Role[] roles = new Role[] { Role.DPS, Role.DPS, Role.TANK, Role.SUPPORT };
                bool equipeValide = true;

                foreach (Role role in roles)
                {
                    int niveauAttendu = (NIVEAUMOYEN * (equipe.Membres.Count() + 1)) - totalNiveauEquipe;

                    Personnage choix = Recherche(niveauAttendu, personnagesDispo, role);

                    if (choix == null)
                    {
                        equipeValide = false;
                        break;
                    }

                    equipe.AjouterMembre(choix);
                    totalNiveauEquipe += choix.LvlPrincipal;
                    personnagesDispo.Remove(choix);
                }

                if (equipeValide)
                    repartition.AjouterEquipe(equipe);
                else
                    break; // Impossible de former une autre équipe valide
            }

            stopwatch.Stop();
            this.TempsExecution = stopwatch.ElapsedMilliseconds;
            return repartition;
        }

        #endregion
    }
}
