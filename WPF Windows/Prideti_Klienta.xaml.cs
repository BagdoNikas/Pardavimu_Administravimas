using System.Windows;
using System.Windows.Controls;

namespace Administravimas.WPF_Windows
{
    /// <summary>
    /// Interaction logic for Prideti_Klienta.xaml
    /// </summary>
    public partial class Prideti_Klienta : Window
    {
        string senasidF;
        string senasidJ;
        /// <summary>
        /// PAGRINDINIS METODAS ĮGALINANTIS PAGRINDINIAS LANGO SAVYBES
        /// NEIŠTRYNTI
        /// </summary>
        public Prideti_Klienta()
        {
            InitializeComponent();

            Tipas_combo.Items.Add("Fizinis");
            Tipas_combo.Items.Add("Juridinis");
            Generate_ID();

        }

        /// <summary>
        /// Išjungia tik Pridėti_Klienta langą
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Metodai.id_klientoF = senasidF;
            Metodai.id_klientoJ = senasidJ;
            this.Close();
        }

        /// <summary>
        /// Sukuria naują klientą
        /// </summary>
        private void Itraukti_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Metodai.Naujas_Klientas(Id_text.Text, Tipas_combo.SelectedItem.ToString(),
                        Pavadinimas_text.Text, long.Parse(Kodas_text.Text), long.Parse(TelNr_text.Text));
                this.Close();
            }
            catch
            {
                MessageBox.Show("Blogai įvestas kodas arba telefono numeris");
            }
        }

        /// <summary>
        /// Generuoja naują id kodą pagal pasirinktą tipą ir jau egzistuojančius kodus
        /// </summary>
        public void Generate_ID()
        {
            string tipas = Tipas_combo.SelectedItem.ToString();
            string id = "";
            senasidF = Metodai.id_klientoF;
            senasidJ = Metodai.id_klientoJ;
            switch (tipas[0])
            {
                case 'F': id = "F_" + (int.Parse(senasidF.Substring(2)) + 1).ToString("D4"); Metodai.id_klientoF = id; break;
                case 'J': id = "J_" + (int.Parse(senasidJ.Substring(2)) + 1).ToString("D4"); Metodai.id_klientoJ = id; break;
            }
            Id_text.Text = id;

        }

        /// <summary>
        /// Generuoja naują id kodą pasikeitus kliento tipui
        /// </summary>
        private void Tipas_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                Metodai.id_klientoF = senasidF;
                Metodai.id_klientoJ = senasidJ;
                Generate_ID();
            }
        }

        /// <summary>
        /// Jeigu visi textbox yra upildyti tai itraukti mygtukas įjungiamas, jei neužpildyti tai išjungtas
        /// </summary>
        private void EnableButton(object sender, TextChangedEventArgs e)
        {
            if (Pavadinimas_text.Text.Length > 0 && Kodas_text.Text.Length > 0 && TelNr_text.Text.Length > 0 && Id_text.Text.Length > 0)
                Itraukti.IsEnabled = true;
            else
                Itraukti.IsEnabled = false;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1. Įveskite kliento pavadinimą (Gali būti vardas ir pavardė)\n" +
                            "2. Įveskite kliento kodą (sveikas skaičius)\n" +
                            "3. Įveskite kliento telefono numerį (sveikas skaičius)\n" +
                            "4. Paspauskite mygtuką 'Įtraukti'", "Pagalba");
        }
    }
}
