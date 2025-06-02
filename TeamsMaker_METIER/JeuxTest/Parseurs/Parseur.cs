using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsMaker_METIER.Personnages;
using TeamsMaker_METIER.Personnages.Classes;

namespace TeamsMaker_METIER.JeuxTest.Parseurs
{
    public class Parseur
    {
        #region Méthode

        //Méthode ParserLigne qui extrait les lignes du fichier une par une pour en refaire des personnages valides.
        private Personnage ParserLigne(string ligne)
        {
            string[] lperso = ligne.Split();
            Personnage perso = new Personnage((Classe)Enum.Parse(typeof(Classe), lperso[0]), Int32.Parse(lperso[1]), Int32.Parse(lperso[2]));
            return perso ;
        }

        //Méthode Parser qui transforme un fichier de texte contenant les joueurs en personnage utilisable. 
        public JeuTest Parser(string nomFichier)
        {
            JeuTest jeuTest = new JeuTest();
            string cheminFichier = Path.Combine(Directory.GetCurrentDirectory(),
            "JeuxTest/Fichiers/" + nomFichier);
            using (StreamReader stream = new StreamReader(cheminFichier))
            {
                string ligne;
                while ((ligne = stream.ReadLine()) != null)
                {
                    jeuTest.AjouterPersonnage(ParserLigne(ligne));
                }
            }
            return jeuTest;
        }
        #endregion

    }
}
