using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Algorithmes.Outils;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;
using System.Diagnostics;
using TeamsMaker_METIER.JeuxTest.Parseurs;
using TeamsMaker_METIER.Algorithmes.Realisations;
using TeamsMaker_METIER.Problemes;



namespace TeamsMakerConsole
{
    public class Program
    {
        #region Main
        //Méthode Main qui fait les équipes sans les afficher pour un résultat plus léger.
        public static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();  
            stopwatch.Start();

            Parseur parseur = new Parseur();
            JeuTest jeutest = parseur.Parser("ressourceNSwap.jt");

            AlgorithmeMoyenne alg = new AlgorithmeMoyenne();
            Algorithme2Swap alg2 = new Algorithme2Swap();

            Repartition rep = alg.Repartir(jeutest);
            Repartition rep2 = alg2.Repartir(jeutest);

            rep.LancerEvaluation(Probleme.SIMPLE);
            rep2.LancerEvaluation(Probleme.SIMPLE);

            Console.WriteLine(rep.Score);
            Console.WriteLine(rep2.Score);

            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);
        }
        #endregion
    }
}
