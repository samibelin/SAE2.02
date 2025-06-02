using System.Diagnostics;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;

namespace TeamsMaker_METIER.Algorithmes.Realisations
{
    public class Algorithme2Swap : Algorithme
    {
        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Repartition repartition = RepartitionInitiale(jeuTest);
            bool amelioration = true;
            Personnage[] personnages = jeuTest.Personnages;
            int nbEquipes = personnages.Length / 4;

            while (amelioration)
            {
                amelioration = false;
                var equipes = repartition.Equipes;

                for (int i = 0; i < nbEquipes; i++)
                {
                    for (int j = i + 1; j < nbEquipes; j++)
                    {
                        var equipe1 = equipes[i];
                        var equipe2 = equipes[j];

                        foreach (Personnage p1 in equipe1.Membres)
                        {
                            foreach (Personnage p2 in equipe2.Membres)
                            {
                                var copie = CopierRepartition(repartition);
                                SwapPersonnages(copie, p1, p2);

                                if (copie.Score < repartition.Score)
                                {
                                    repartition = copie;
                                    amelioration = true;
                                }
                            }
                        }
                    }
                }
            }
            stopwatch.Stop();
            this.TempsExecution = stopwatch.ElapsedMilliseconds;
            return repartition;
        }

        private Repartition RepartitionInitiale(JeuTest jeuTest)
        {
            Algorithme algo = new AlgorithmeMoyenne();
            return algo.Repartir(jeuTest);
        }

        private void SwapPersonnages(Repartition repartition, Personnage p1, Personnage p2)
        {
            Equipe equipe1 = repartition.Equipes.First(e => e.Membres.Contains(p1));
            Equipe equipe2 = repartition.Equipes.First(e => e.Membres.Contains(p2));

            equipe1.RetirerMembre(p1);
            equipe2.RetirerMembre(p2);

            equipe1.AjouterMembre(p2);
            equipe2.AjouterMembre(p1);
        }

        private Repartition CopierRepartition(Repartition repartition)
        {
            return repartition.Clone();
        }
    }
}
