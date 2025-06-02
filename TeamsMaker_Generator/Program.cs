namespace TeamsMaker_Generator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string file = Directory.GetCurrentDirectory() + "\\..\\..\\..\\..\\TeamsMaker_METIER\\JeuxTest\\Fichiers";
            string choix, nomFile;
            int nbPerso;
            Outils outils = new Outils();

            Console.WriteLine("[1] : linéaire \n[2] : progressive");
            choix = Console.ReadLine();
            Console.WriteLine("nombre de perso");
            nbPerso = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("nom du fichier");
            nomFile = Console.ReadLine();
            file += "\\" + nomFile + ".jt";
            outils.FileSetup(file);

            switch (choix)
            {
                case "1":
                    outils.GeneratorLinear(nbPerso, file);
                    Console.WriteLine("Génération linéaire terminée");
                    break;

                case "2":
                    outils.GeneratorProgressive(nbPerso, file);
                    Console.WriteLine("Génération progressive terminée");
                    break;

                default:
                    Console.WriteLine("erreur");
                    break;
            }
        }
    }
}