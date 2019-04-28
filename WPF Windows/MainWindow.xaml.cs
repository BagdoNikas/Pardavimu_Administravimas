using System;
using System.Windows;

namespace Administravimas.WPF_Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow main = null;

        public MainWindow()
        {
            main = this;
            InitializeComponent();
            Metodai.Skaitymas();
            Update_Data_Grid();
        }

        /// <summary>
        /// Atnaujina vartotojo matomą duomenų lentelę
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
        /// Įjungia langą Kurti užsakymą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kurti_Užsakymą_Click(object sender, RoutedEventArgs e)
        {
            var KurtiUzakymaWindow = new Kurti_Uzsakyma();
            KurtiUzakymaWindow.Show();
        }

        /// <summary>
        /// Atidaro Formuoti ataskaiktą langą
        /// </summary>
        private void Formuoti_Click(object sender, RoutedEventArgs e)
        {
            var FormuotiAtaskaitawindow = new Formuoti_Ataskaita();
            FormuotiAtaskaitawindow.Show();
        }

        /// <summary>
        /// Išjungia visus atidarytus langus
        /// </summary>
        private void Baigti_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Atidaro Pridėti pardaveją langą
        /// </summary>
        private void PridetiPardaveja_Click(object sender, RoutedEventArgs e)
        {
            var PridetidarbuotojaWindow = new Prideti_Darbuotoja();
            PridetidarbuotojaWindow.Show();
        }

        /// <summary>
        /// Atidaro pridėti klientą langą
        /// </summary>
        private void PridetiKlienta_Click(object sender, RoutedEventArgs e)
        {
            var PridetiKlientaWindow = new Prideti_Klienta();
            PridetiKlientaWindow.Show();
        }

        /// <summary>
        /// Įjungia elementų Šalinimo langą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pasalinti_Click(object sender, RoutedEventArgs e)
        {
            var PasalintiWindow = new Salinimas();
            PasalintiWindow.Show();
        }

        /// <summary>
        /// Įjungia naujos prekės sukūrimo langą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PridetiPreke_Click(object sender, RoutedEventArgs e)
        {
            var PridetiprekeWindow = new Prideti_preke();
            PridetiprekeWindow.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1. Paspaudus mygtuką 'Naujas užsakymas' matysite naują langą\n" +
                            "    kuriame galėsite užpildyti naujo užsakymo duomenis\n" +
                            "2. Paspaudus mygtuką 'Pridėti pardavėją' matysite naują langą\n" +
                            "    kuriame galėsite užpildyti naujo pardavėjo duomenis\n" +
                            "3. Paspaudus mygtuką 'pridėti klientą' matysite naują langą\n" +
                            "    kuriame galėsite užpildyti naujos prekės duomenis\n" +
                            "4. Paspaudus mygtuką 'Formuoti ataskaitą' matysite naują langą\n" +
                            "    kuriame galėsite pasirinkti ataskaitos formatą ir\n" +
                            "    programa sugeneruos ataskaitą\n" +
                            "5. Paspaudus mygtuką 'Pašalinti' matysite naują langą\n" +
                            "    kuriame galėsite pašalinti pasirinktus objektus\n" +
                            "6. Paspaudus mygtuką 'Baigti' bus uždaryta programa\n\n" +
                            "    Perspėjimas!!! Paspaudus mygtuką 'Baigti' bus išjungti\n" +
                            "    visi programos langai.", "Pagalba");
        }
    }
}
