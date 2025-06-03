using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Realisations
{
    public class Algorithme2Opt : Algorithme
    {
        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Repartition repartition = new AlgorithmeGloutonCroissant().Repartir(jeuTest);
            repartition.LancerEvaluation(Problemes.Probleme.SIMPLE);

            bool amelioration = true;

            while (amelioration)
            {
                amelioration = false;

                for (int i = 0; i < repartition.Equipes.Count(); i++)
                {
                    for (int j = i + 1; j < repartition.Equipes.Count(); j++)
                    {
                        Equipe equipe1 = repartition.Equipes[i];
                        Equipe equipe2 = repartition.Equipes[j];

                        var tousPersos = equipe1.Membres.Concat(equipe2.Membres).ToList();

                        var repartitionsPossibles = GenererToutesRepartitions(tousPersos);

                        foreach (var (groupe1, groupe2) in repartitionsPossibles)
                        {
                            var copie = repartition.Clone();

                            Equipe copie1 = copie.Equipes[i];
                            Equipe copie2 = copie.Equipes[j];

                            // Retirer tous les membres actuels
                            foreach (var p in equipe1.Membres)
                                copie1.RetirerMembre(copie1.Membres.First(mp => mp == p));

                            foreach (var p in equipe2.Membres)
                                copie2.RetirerMembre(copie2.Membres.First(mp => mp == p));

                            // Ajouter les nouveaux membres
                            foreach (var p in groupe1)
                                copie1.AjouterMembre(p);

                            foreach (var p in groupe2)
                                copie2.AjouterMembre(p);

                            copie.LancerEvaluation(Problemes.Probleme.SIMPLE);

                            if (copie.Score < repartition.Score)
                            {
                                repartition = copie;
                                amelioration = true;
                                goto RedemarrerDepuisDebut;
                            }
                        }
                    }
                }

            RedemarrerDepuisDebut:;
            }

            stopwatch.Stop();
            this.TempsExecution = stopwatch.ElapsedMilliseconds;
            return repartition;
        }

        private List<(List<Personnage>, List<Personnage>)> GenererToutesRepartitions(List<Personnage> persos)
        {
            var resultats = new List<(List<Personnage>, List<Personnage>)>();

            void Combiner(List<Personnage> courant, int index)
            {
                if (courant.Count == 4)
                {
                    var complement = persos.Except(courant).ToList();
                    resultats.Add((new List<Personnage>(courant), complement));
                    return;
                }

                for (int i = index; i < persos.Count; i++)
                {
                    courant.Add(persos[i]);
                    Combiner(courant, i + 1);
                    courant.RemoveAt(courant.Count - 1);
                }
            }

            Combiner(new List<Personnage>(), 0);
            return resultats;
        }
    }
}
