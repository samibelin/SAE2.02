using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TeamsMaker_METIER.Algorithmes.Outils;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;
using TeamsMaker_METIER.Personnages.Classes;

namespace TeamsMaker_METIER.Algorithmes.Realisations
{
    internal class AlgorithmeExtremeN2 : Algorithme
    {
        #region Méthode
        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Repartition repartition = new Repartition(jeuTest);
            Personnage[] arrJeuTest = jeuTest.Personnages;
            Array.Sort(arrJeuTest, new ComparateurPersonnageParNiveauPrincipal());
            List<Personnage> copie = arrJeuTest.ToList();

            bool peutFormerEquipe = true;

            while (copie.Count >= 4 && peutFormerEquipe)
            {
                List<Role> rolesRecherches = new List<Role> { Role.DPS, Role.TANK, Role.SUPPORT, Role.DPS };
                Equipe equipe = new Equipe();
                List<Personnage> membresAjoutes = new List<Personnage>();

                // Sélection des 2 personnages les plus faibles
                int i = 0;
                while (i < copie.Count && equipe.Membres.Count() < 2)
                {
                    Personnage p = copie[i];
                    if (rolesRecherches.Contains(p.RolePrincipal))
                    {
                        equipe.AjouterMembre(p);
                        membresAjoutes.Add(p);
                        rolesRecherches.Remove(p.RolePrincipal);
                        copie.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }

                // Sélection des 2 personnages les plus forts
                int j = copie.Count - 1;
                while (j >= 0 && equipe.Membres.Count() < 4)
                {
                    Personnage p = copie[j];
                    if (rolesRecherches.Contains(p.RolePrincipal))
                    {
                        equipe.AjouterMembre(p);
                        membresAjoutes.Add(p);
                        rolesRecherches.Remove(p.RolePrincipal);
                        copie.RemoveAt(j);
                    }
                    j--;
                }

                // Vérification de la validité de l'équipe
                if (equipe.Membres.Count() == 4)
                {
                    repartition.AjouterEquipe(equipe);
                }
                else
                {
                    // Équipe incomplète : rollback des personnages ajoutés
                    foreach (var perso in membresAjoutes)
                    {
                        copie.Add(perso);
                    }

                    // Tri à nouveau pour respecter l'ordre original (faible à fort)
                    copie.Sort(new ComparateurPersonnageParNiveauPrincipal());

                    // On ne peut plus former d'équipe valide
                    peutFormerEquipe = false;
                }
            }

            stopwatch.Stop();
            this.TempsExecution = stopwatch.ElapsedMilliseconds;
            return repartition;
        }
        #endregion
    }
}
