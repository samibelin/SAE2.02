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
    internal class AlgorithmeSmartRandomBetterEndStage2 : Algorithme
    {
        #region Méthode

        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Repartition repartition = new Repartition(jeuTest);
            Random rng = new Random();

            List<Personnage> dpsDispo = new List<Personnage>();
            List<Personnage> tankDispo = new List<Personnage>();
            List<Personnage> supportDispo = new List<Personnage>();

            foreach (var perso in jeuTest.Personnages)
            {
                switch (perso.Classe.RolePrincipal())
                {
                    case Role.DPS: dpsDispo.Add(perso); break;
                    case Role.TANK: tankDispo.Add(perso); break;
                    case Role.SUPPORT: supportDispo.Add(perso); break;
                }
            }

            List<Personnage> dpsEchec = new List<Personnage>();
            List<Personnage> tankEchec = new List<Personnage>();
            List<Personnage> supportEchec = new List<Personnage>();

            double margeBasse = 50;
            double margeHaute = 50;
            const double ChangementMarge = 0.05;

            while (dpsDispo.Count >= 2 && tankDispo.Count >= 1 && supportDispo.Count >= 1 && (margeHaute - margeBasse <= 50))
            {
                // Sélection aléatoire des membres de l’équipe
                Personnage tank = tankDispo[rng.Next(tankDispo.Count)];
                Personnage support = supportDispo[rng.Next(supportDispo.Count)];
                List<Personnage> dpsSelectionnes = new List<Personnage>();

                while (dpsSelectionnes.Count < 2)
                {
                    Personnage dps = dpsDispo[rng.Next(dpsDispo.Count)];
                    if (!dpsSelectionnes.Contains(dps))
                        dpsSelectionnes.Add(dps);
                }

                List<Personnage> equipeTemp = new List<Personnage> { tank, support };
                equipeTemp.AddRange(dpsSelectionnes);

                double moyenneNiveau = equipeTemp.Average(p => p.LvlPrincipal);

                if (moyenneNiveau >= margeBasse && moyenneNiveau <= margeHaute)
                {
                    Equipe equipe = new Equipe();
                    foreach (var membre in equipeTemp)
                        equipe.AjouterMembre(membre);

                    repartition.AjouterEquipe(equipe);

                    dpsDispo.RemoveAll(p => dpsSelectionnes.Contains(p));
                    tankDispo.Remove(tank);
                    supportDispo.Remove(support);
                }
                else
                {
                    dpsEchec.AddRange(dpsSelectionnes);
                    tankEchec.Add(tank);
                    supportEchec.Add(support);

                    dpsDispo.RemoveAll(p => dpsSelectionnes.Contains(p));
                    tankDispo.Remove(tank);
                    supportDispo.Remove(support);
                }

                // Si pas assez de membres, relancer avec les échecs + élargir la marge
                if (dpsDispo.Count < 2 || tankDispo.Count < 1 || supportDispo.Count < 1)
                {
                    if (dpsEchec.Count >= 2 && tankEchec.Count >= 1 && supportEchec.Count >= 1)
                    {
                        dpsDispo = new List<Personnage>(dpsEchec);
                        tankDispo = new List<Personnage>(tankEchec);
                        supportDispo = new List<Personnage>(supportEchec);

                        dpsEchec.Clear();
                        tankEchec.Clear();
                        supportEchec.Clear();

                        margeBasse -= ChangementMarge;
                        margeHaute += ChangementMarge;
                    }
                }
            }

            // Phase de fin

            while (dpsDispo.Count >= 2 && tankDispo.Count >= 1 && supportDispo.Count >= 1)
            {
                Equipe meilleureEquipe = null;
                int meilleureDistance = int.MaxValue;

                foreach (var support in supportDispo)
                {
                    foreach (var tank in tankDispo)
                    {
                        for (int i = 0; i < dpsDispo.Count - 1; i++)
                        {
                            for (int j = i + 1; j < dpsDispo.Count; j++)
                            {
                                int somme = support.LvlPrincipal + tank.LvlPrincipal + dpsDispo[i].LvlPrincipal + dpsDispo[j].LvlPrincipal;
                                int distance = Math.Abs(somme - 200);

                                if (distance < meilleureDistance)
                                {
                                    meilleureDistance = distance;
                                    meilleureEquipe = new Equipe();
                                    meilleureEquipe.AjouterMembre(support);
                                    meilleureEquipe.AjouterMembre(tank);
                                    meilleureEquipe.AjouterMembre(dpsDispo[i]);
                                    meilleureEquipe.AjouterMembre(dpsDispo[j]);
                                }
                            }
                        }
                    }
                }

                if (meilleureEquipe != null)
                {
                    foreach (var p in meilleureEquipe.Membres)
                    {
                        supportDispo.Remove(p);
                        tankDispo.Remove(p);
                        dpsDispo.Remove(p);
                    }

                    repartition.AjouterEquipe(meilleureEquipe);
                }
                else
                {
                    break; // Plus de combinaison possible
                }
            }




            stopwatch.Stop();
            this.TempsExecution = stopwatch.ElapsedMilliseconds;
            return repartition;
        }

        #endregion
    }
}
