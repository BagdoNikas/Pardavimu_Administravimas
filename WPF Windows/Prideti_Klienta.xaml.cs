using System.Windows;
using System.Windows.Controls;

namespace Administravimas.WPF_Windows
{
    /// <summary>
    /// Interaction logic for Prideti_Klienta.xaml
    /// </summary>
    public partial class Prideti_Klienta : Window
    {
        string oldIDfiz;
        string oldIdjur;

        public Prideti_Klienta()
        {
            InitializeComponent();

            //ComboBox'o "Tipas" užpildymas reikšmėmis.
            Tipas_combo.Items.Add("Fizinis");
            Tipas_combo.Items.Add("Juridinis");

            Generate_ID();

        }

        /// <summary>
        /// Išjungia "Naujas klientas" langą mygtuko "Baigti" paspaudimu.
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Metodai.id_klientoF = oldIDfiz;
            Metodai.id_klientoJ = oldIdjur;
            this.Close();
        }

        /// <summary>
        /// Įtraukia naują klientą į sąrašus mygtuko "Pridėti" paspaudimu.
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
        /// Naujo identifikatoriaus generavimas pagal pasirinktą tipą ir jau egzistuojančius ID.
        /// </summary>
        public void Generate_ID()
        {
            string tipas = Tipas_combo.SelectedItem.ToString();
            string id = "";
            oldIDfiz = Metodai.id_klientoF;
            oldIdjur = Metodai.id_klientoJ;
            switch (tipas[0])
            {
                case 'F': id = "F_" + (int.Parse(oldIDfiz.Substring(2)) + 1).ToString("D4"); Metodai.id_klientoF = id; break;
                case 'J': id = "J_" + (int.Parse(oldIdjur.Substring(2)) + 1).ToString("D4"); Metodai.id_klientoJ = id; break;
            }
            Id_text.Text = id;

        }

        /// <summary>
        /// Naujo identifikatoriaus generavimas pakeitus kliento tipą.
        /// </summary>
        private void Tipas_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                Metodai.id_klientoF = oldIDfiz;
                Metodai.id_klientoJ = oldIdjur;
                Generate_ID();
            }
        }

        /// <summary>
        /// Mygtuko "Pridėti" įgalinimas priklausomai nuo formos užpildymo.
        /// </summary>
        private void EnableButton(object sender, TextChangedEventArgs e)
        {
            if (Pavadinimas_text.Text.Length > 0 && Kodas_text.Text.Length > 0 && TelNr_text.Text.Length > 0 && Id_text.Text.Length > 0)
                Itraukti.IsEnabled = true;
            else
                Itraukti.IsEnabled = false;
        }

        /// <summary>
        /// Mygtuko "Pagalba" atliekami veiksmai.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1. Įveskite įmonės pavadinimą arba asmens vardą ir pavardę.\n" +
                            "2. Įveskite įmonės ar asmens kodą (sveikas skaičius).\n" +
                            "3. Įveskite kliento telefono numerį (sveikas skaičius).\n" +
                            "   Formatas: \"3706xxxxxxx\"." +
                            "4. Paspauskite mygtuką 'Įtraukti'.", "Pagalba");
        }
    }
}
