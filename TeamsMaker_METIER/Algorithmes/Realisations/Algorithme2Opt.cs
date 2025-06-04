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
                bool sortieAnticipee = false;

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

                            RetirerMembres(copie1, equipe1.Membres);
                            RetirerMembres(copie2, equipe2.Membres);

                            AjouterMembres(copie1, groupe1);
                            AjouterMembres(copie2, groupe2);

                            copie.LancerEvaluation(Problemes.Probleme.SIMPLE);

                            if (copie.Score < repartition.Score)
                            {
                                repartition = copie;
                                amelioration = true;
                                sortieAnticipee = true;
                                break;
                            }
                        }
                        if (sortieAnticipee) break;
                    }
                    if (sortieAnticipee) break;
                }
            }

            stopwatch.Stop();
            this.TempsExecution = stopwatch.ElapsedMilliseconds;
            return repartition;
        }

        private List<(List<Personnage>, List<Personnage>)> GenererToutesRepartitions(List<Personnage> persos)
        {
            var resultats = new List<(List<Personnage>, List<Personnage>)>();
            Combiner(persos, new List<Personnage>(), 0, resultats);
            return resultats;
        }

        private void Combiner(List<Personnage> persos, List<Personnage> courant, int index, List<(List<Personnage>, List<Personnage>)> resultats)
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
                Combiner(persos, courant, i + 1, resultats);
                courant.RemoveAt(courant.Count - 1);
            }
        }

        private void RetirerMembres(Equipe equipe, IEnumerable<Personnage> membres)
        {
            foreach (var p in membres)
            {
                var membre = equipe.Membres.First(mp => mp == p);
                equipe.RetirerMembre(membre);
            }
        }

        private void AjouterMembres(Equipe equipe, IEnumerable<Personnage> membres)
        {
            foreach (var p in membres)
            {
                equipe.AjouterMembre(p);
            }
        }
    }
}
