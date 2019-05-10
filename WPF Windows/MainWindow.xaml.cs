using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Administravimas.WPF_Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow main = null;

        static string failai = "..\\..\\Failai";
        static string backup = "..\\..\\Backup";

        public MainWindow()
        {
            main = this;
            InitializeComponent();
            Metodai.Skaitymas();
            Update_Data_Grid();
            BackupFiles();
        }

        /// <summary>
        /// Išaugo failus į atsarginių kopojų direktoriją
        /// </summary>
        public void BackupFiles()
        {
            DateTime sth = File.GetLastWriteTime(backup).Date;
            if (File.GetLastWriteTime(backup).Date != DateTime.Today)
            {
                if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
                {
                    foreach (string file in Directory.GetFiles(backup))
                    {
                        File.Delete(file);
                    }

                    DirectoryInfo dir = new DirectoryInfo(failai);

                    DirectoryInfo[] dirs = dir.GetDirectories();
                    // If the destination directory doesn't exist, create it.
                    if (!Directory.Exists(backup))
                    {
                        Directory.CreateDirectory(backup);
                    }

                    // Get the files in the directory and copy them to the new location.
                    FileInfo[] files = dir.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        string temppath = Path.Combine(backup, file.Name);
                        file.CopyTo(temppath, false);
                    }
                }
            }
        }

        /// <summary>
        /// Užkrauna failus iš atsarginių kopijų
        /// </summary>
        public void ReBackupFiles()
        {
            if (System.Windows.MessageBox.Show("Ar tikrai norite užkrauti visus duomenis?", "Patvirtinti", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (string file in Directory.GetFiles(backup))
                {
                    File.Delete(file);
                }

                DirectoryInfo dir = new DirectoryInfo(failai);

                DirectoryInfo[] dirs = dir.GetDirectories();
                // If the destination directory doesn't exist, create it.
                if (!Directory.Exists(backup))
                {
                    Directory.CreateDirectory(backup);
                }

                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(backup, file.Name);
                    file.CopyTo(temppath, false);
                }
            }
            else
            {
                FileInfo file = new FileInfo(Direktorija());
                string temppath = Path.Combine(backup, file.Name);
                file.CopyTo(temppath, false);
            }
        }

        /// <summary>
        /// Interaktyvaus direktorijos parinkimo metodas.
        /// </summary>
        /// <returns></returns>
        public string Direktorija()
        {
            string path = Path.GetFullPath(failai+'\\');
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = path;
            openFileDialog1.ShowDialog();

            string name = openFileDialog1.FileName;
            return name;
        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            failai = "..\\..\\Backup";
            backup = "..\\..\\Failai";
            ReBackupFiles();
        }

        /// <summary>
        /// Duomenų lentelės atnaujinimo metodas.
        /// </summary>
        public void Update_Data_Grid()
        {
            for (int i = 0; i < Metodai.Užsakymai.Count; i++)
            {
                DuomenuGrid.Items.Clear();
            }
            for (int i = 0; i < Metodai.Užsakymai.Count; i++)
            {
                var užsakymas = Metodai.Užsakymai[i];
                DuomenuGrid.Items.Add(užsakymas);
            }
        }

        /// <summary>
        /// Aktyvuoja langą "Užsakymo kūrimas" mygtuko "Naujas užsakymas" paspaudimu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kurti_Užsakymą_Click(object sender, RoutedEventArgs e)
        {
            var KurtiUzakymaWindow = new Kurti_Uzsakyma();
            KurtiUzakymaWindow.Show();
        }

        /// <summary>
        /// Aktyvuoja langą "Ataskaitos formavimas" mygtuko "Formuoti ataskaitą" paspaudimu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Formuoti_Click(object sender, RoutedEventArgs e)
        {
            var FormuotiAtaskaitawindow = new Formuoti_Ataskaita();
            FormuotiAtaskaitawindow.Show();
        }

        /// <summary>
        /// Iškungia visus aktyvius langus mygtuko "Baigti" paspaudimu.
        /// </summary>
        private void Baigti_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Aktyvuoja langą "Naujas darbuotojas" mygtuko "Pridėti darbuotoją" paspaudimu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PridetiPardaveja_Click(object sender, RoutedEventArgs e)
        {
            var PridetidarbuotojaWindow = new Prideti_Darbuotoja();
            PridetidarbuotojaWindow.Show();
        }

        /// <summary>
        /// Aktyvuoja langą "Naujas klientas" mygtuko "Pridėti klientą" paspaudimu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PridetiKlienta_Click(object sender, RoutedEventArgs e)
        {
            var PridetiKlientaWindow = new Prideti_Klienta();
            PridetiKlientaWindow.Show();
        }

        /// <summary>
        /// Aktyvuoja langą "Šalinimas" mygtuko "Pašalinti" paspaudimu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pasalinti_Click(object sender, RoutedEventArgs e)
        {
            var PasalintiWindow = new Salinimas();
            PasalintiWindow.Show();
        }

        /// <summary>
        /// Aktyvuoja langą "Nauja prekė" mygtuko "Pridėti prekę" paspaudimu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PridetiPreke_Click(object sender, RoutedEventArgs e)
        {
            var PridetiprekeWindow = new Prideti_preke();
            PridetiprekeWindow.Show();
        }

        /// <summary>
        /// Mygtuko "Pagalba" atliekami veiksmai.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pagalba_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("1. Paspaudę mygtuką 'Naujas užsakymas' matysite naują langą,\n" +
                            "   kuriame galėsite užpildyti duomenis apie naują užsakymą.\n" +
                            "2. Paspaudę mygtuką 'Pridėti darbuotoją' matysite naują langą,\n" +
                            "   kuriame galėsite užpildyti duomenis apie naują pardavėją.\n" +
                            "3. Paspaudę mygtuką 'Pridėti klientą' matysite naują langą,\n" +
                            "   kuriame galėsite užpildyti duomenis apie naują prekę.\n" +
                            "4. Paspaudę mygtuką 'Formuoti ataskaitą' matysite naują langą,\n" +
                            "   kuriame galėsite pasirinkti norimą ataskaitos formatą,\n" +
                            "   kurią programa sugeneruos.\n" +
                            "5. Paspaudę mygtuką 'Pašalinti' matysite naują langą,\n" +
                            "   kuriame galėsite pašalinti pasirinktus objektus.\n" +
                            "6. Paspaudę mygtuką 'Baigti' uždarysite programą.\n\n" +
                            "   Perspėjimas!!! Paspaudus mygtuką 'Baigti' bus išjungti\n" +
                            "   visi aktyvūs programos langai.", "Pagalba");
        }

    }
}
