using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Algorithmes.Outils;
using TeamsMaker_METIER.JeuxTest;
using TeamsMaker_METIER.Personnages;
using System.Diagnostics;

namespace TeamsMaker_METIER.Algorithmes.Realisations
{
    public class AlgorithmeGloutonCroissant:Algorithme
    {
        #region Méthode
        //Méthode pour répartir les joueurs dans les différentes équipes selon un algorithme glouton croissant.
        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stopwatch = new Stopwatch();          //initialisation du chrono
            stopwatch.Start();                              //démarage du chrono
            Repartition repartition = new Repartition(jeuTest);
            Personnage[] arrJeuTest = jeuTest.Personnages;
            Array.Sort(arrJeuTest, new ComparateurPersonnageParNiveauPrincipal());
            for (int i = 0; i<arrJeuTest.Length  / 4; i ++)
            {
                Equipe equipe = new Equipe();
                for (int j = 4*i; j < 4*(i+1); j++)
                {
                    equipe.AjouterMembre(arrJeuTest[j]);
                }
                repartition.AjouterEquipe(equipe);
            }
            stopwatch.Stop();                               //arrêt du chrono
            this.TempsExecution = stopwatch.ElapsedMilliseconds;    //affichage du chrono.
            return repartition;
        }
        #endregion
    }
}
