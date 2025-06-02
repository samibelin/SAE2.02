using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TeamsMaker_METIER.Algorithmes.Outils;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Realisations
{
    internal class AlgorithmeSmartRandom : Algorithme
    {
        #region Méthode

        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Repartition repartition = new Repartition(jeuTest);

            List<Personnage> personnagesDispo = new List<Personnage>(jeuTest.Personnages);
            List<Personnage> tempEchecs = new List<Personnage>();
            Random rng = new Random();

            double margeBasse = 50;
            double margeHaute = 50;
            const double ChangementMarge = 0.1;
            const int nbMembreEquipe = 4;

            while (personnagesDispo.Count >= nbMembreEquipe)
            {
                List<Personnage> equipeTemp = new List<Personnage>();

                List<int> indices = new List<int>();
                while (equipeTemp.Count < nbMembreEquipe)
                {
                    int index = rng.Next(personnagesDispo.Count);
                    if (!indices.Contains(index))
                    {
                        indices.Add(index);
                        equipeTemp.Add(personnagesDispo[index]);
                    }
                }

                int totalNiveau = equipeTemp.Sum(p => p.LvlPrincipal);
                totalNiveau /= nbMembreEquipe;

                if (totalNiveau >= margeBasse && totalNiveau <= margeHaute)
                {
                    Equipe equipe = new Equipe();
                    foreach (var membre in equipeTemp)
                    {
                        equipe.AjouterMembre(membre);
                        personnagesDispo.Remove(membre);
                    }
                    repartition.AjouterEquipe(equipe);
                }
                else
                {
                    foreach (var membre in equipeTemp)
                    {
                        personnagesDispo.Remove(membre);
                        tempEchecs.Add(membre);
                    }
                }

                if (personnagesDispo.Count < nbMembreEquipe && tempEchecs.Count >= nbMembreEquipe)
                {
                    personnagesDispo = new List<Personnage>(tempEchecs);
                    tempEchecs.Clear();

                    margeBasse -= ChangementMarge;
                    margeHaute += ChangementMarge;
                }
            }

            stopwatch.Stop();
            this.TempsExecution = stopwatch.ElapsedMilliseconds;
            return repartition;
        }

        #endregion
    }
}
