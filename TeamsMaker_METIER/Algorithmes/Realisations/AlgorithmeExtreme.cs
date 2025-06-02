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
    internal class AlgorithmeExtreme:Algorithme
    {
        #region Méthode

        //Méthode pour répartir les joueurs dans les différentes équipes selon un algorithme glouton "extremes en premier".

        public override Repartition Repartir(JeuTest jeuTest)
        {
            Stopwatch stopwatch = new Stopwatch();          //initialisation du chrono
            stopwatch.Start();                              //démarage du chrono

            Repartition repartition = new Repartition(jeuTest);
            Personnage[] arrJeuTest = jeuTest.Personnages;
            Array.Sort(arrJeuTest, new ComparateurPersonnageParNiveauPrincipal());

            //initialisation de deux pointeur , respectivement au debut et a la fin de la liste;

            int debut = 0;                      
            int fin = arrJeuTest.Length - 1;   

            while (fin - debut + 1 >= 4)
            {
                Equipe equipe = new Equipe();

                //ajout des deux plus bas en niveau

                for (int i = 0; i<2; i++)                
                {
                    equipe.AjouterMembre(arrJeuTest[debut]);
                    debut ++;
                }

                //ajout des deux plus haut en niveau

                for (int i = 0; i < 2; i++)
                {
                    equipe.AjouterMembre(arrJeuTest[fin]);
                    fin --;
                }
                repartition.AjouterEquipe(equipe);
            }



            stopwatch.Stop();                                       //arrêt du chrono
            this.TempsExecution = stopwatch.ElapsedMilliseconds;    //affichage du chrono.
            return repartition;
        }
        #endregion
    }
}
