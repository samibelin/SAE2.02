using System.Diagnostics;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;
using System.Linq;

namespace TeamsMaker_METIER.Algorithmes.Realisations
{
    public class Algorithme2Swap : Algorithme
    {
        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Génère une répartition initiale basée sur l'algorithme moyenne
            Repartition repartition = RepartitionInitiale(jeuTest);
            bool amelioration = true;

            while (amelioration)
            {
                amelioration = false;
                repartition.LancerEvaluation(Problemes.Probleme.SIMPLE);

                // Parcours toutes les paires d'équipes
                for (int i = 0; i < repartition.Equipes.Count(); i++)
                {
                    for (int j = i + 1; j < repartition.Equipes.Count(); j++)
                    {
                        Equipe equipe1 = repartition.Equipes[i];
                        Equipe equipe2 = repartition.Equipes[j];

                        // Parcours toutes les paires de personnages entre les deux équipes
                        foreach (Personnage p1 in equipe1.Membres.ToList()) // éviter modification durant l’itération
                        {
                            foreach (Personnage p2 in equipe2.Membres.ToList())
                            {
                                // Crée une copie de la répartition pour tester l’échange
                                Repartition copie = repartition.Clone();

                                Equipe copieEquipe1 = copie.Equipes[i];
                                Equipe copieEquipe2 = copie.Equipes[j];

                                copieEquipe1.RetirerMembre(p1);
                                copieEquipe2.RetirerMembre(p2);

                                copieEquipe1.AjouterMembre(p2);
                                copieEquipe2.AjouterMembre(p1);

                                copie.LancerEvaluation(Problemes.Probleme.SIMPLE);

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
            repartition.Equipes[1].RetirerMembre(repartition.Equipes[1].Membres[3]);
            repartition.Equipes[1].AjouterMembre(repartition.Equipes[0].Membres[4]);
            repartition.Equipes[0].RetirerMembre(repartition.Equipes[0].Membres[4]);

            stopwatch.Stop();
            this.TempsExecution = stopwatch.ElapsedMilliseconds;
            return repartition;
        }

        private Repartition RepartitionInitiale(JeuTest jeuTest)
        {
            // Utilise l’algorithme de moyenne comme base
            Algorithme algo = new AlgorithmeGloutonCroissant();
            return algo.Repartir(jeuTest);
        }
    }
}
