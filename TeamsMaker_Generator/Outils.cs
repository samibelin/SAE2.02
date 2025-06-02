namespace TeamsMaker_Generator
{
    public class Outils
    {
        private String[] strings = { "ARCHER", "BARBARE", "BARD", "CLERC", "DRUIDE", "MAGE", "MOINE", "PALADIN", "VOLEUR", "WARLOCK", "GUERRIER", "SORCIER" };
        private Random random = new Random();

        public void GeneratorLinear(int nb, string fichier)
        {
            using (StreamWriter sw = new StreamWriter(fichier, false))
            {
                for (int i = 0; i < nb; i++)
                {
                    string ligne = "";
                    ligne += strings[random.Next(0, 12)] + ' ';
                    ligne += random.Next(1, 101).ToString() + ' ';
                    ligne += random.Next(1, 101).ToString();
                    sw.WriteLine(ligne);
                }
            }
        }
        public void GeneratorProgressive(int nb, string fichier)
        {
            int equilibrage;
            int min;
            int max;

            using (StreamWriter sw = new StreamWriter(fichier, false))
            {
                for (int i = 0; i < nb; i++)
                {
                    equilibrage = random.Next(1, 101);

                    switch (equilibrage)
                    {
                        case int n when (n >= 1 && n <= 50):
                            min = 1;
                            max = 31;
                            break;

                        case int n when (n >= 41 && n <= 70):
                            min = 32;
                            max = 51;
                            break;

                        case int n when (n >= 71 && n <= 90):
                            min = 52;
                            max = 76;
                            break;

                        case int n when (n >= 91 && n <= 100):
                            min = 77;
                            max = 101;
                            break;

                        default:
                            min = 1;
                            max = 101;
                            break;
                    }

                    string ligne = "";
                    ligne += strings[random.Next(0, 12)] + ' ';
                    ligne += random.Next(min, max).ToString() + ' ';
                    ligne += random.Next(min, max).ToString();
                    sw.WriteLine(ligne);
                }
            }
        }




        public void FileCreator(string fichier)
        {
            using (FileStream fs = File.Create(fichier)) { }
        }

        public void FileDeleter(string fichier)
        {
            if (File.Exists(fichier))
            {
                File.Delete(fichier);
            }
        }

        public void FileSetup(string fichier)
        {
            FileDeleter(fichier);
            FileCreator(fichier);
        }



    }
}
