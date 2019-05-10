using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Administravimas.WPF_Windows
{
    /// <summary>
    /// Interaction logic for Kurti_Uzsakyma.xaml
    /// </summary>
    public partial class Kurti_Uzsakyma : Window
    {
        string senasID;

        public Kurti_Uzsakyma()
        {
            InitializeComponent();

            GenerateID();

            for (int i = 0; i < Metodai.Klientai.Count; i++)
            {
                Klientas_text.Items.Add(Metodai.Klientai[i].Pavadinimas);
            }
            for (int i = 0; i < Metodai.Pardavėjai.Count; i++)
            {
                Pardavejas_text.Items.Add(Metodai.Pardavėjai[i].VardasPavardė);
            }
        }

        /// <summary>
        /// Mygtuko "Kurti" atliekami veiksmai.
        /// Pagal pasirinkimus formoje sukuria naują užsakymą.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kurti_Click(object sender, RoutedEventArgs e)
        {
            string pidkiekis = new TextRange(Id_Kiekis_RichText.Document.ContentStart, Id_Kiekis_RichText.Document.ContentEnd).Text;
            pidkiekis = pidkiekis.Remove(pidkiekis.Length - 2);

            double suma = 0;
            try
            {
                string[] eil = pidkiekis.Split('\n');
                string[] pid = new string[eil.Length];
                short[] kiekis = new short[eil.Length];
                for (int i = 0; i < eil.Length; i++)
                {
                    string[] parts = eil[i].Split(' ');
                    pid[i] = "P" + int.Parse(parts[0]).ToString("D4");
                    kiekis[i] = short.Parse(parts[1].Trim('\r'));
                    suma += Metodai.Prekės[i].Kaina;
                }

                DateTime data = DateTime.Parse(Data_text.Text);
                Metodai.Naujas_Užsakymas(ID_text.Text, Klientas_text.Text, suma, Pardavejas_text.Text, data, pid, kiekis);

                MainWindow.main.Update_Data_Grid();

                this.Close();
            }
            catch
            {
                MessageBox.Show("Blogai įvesti prekių duomenys");
            }
        }

        /// <summary>
        /// Mygtuko "Atšaukti" atliekami veiksmai.
        /// Išjungia "Naujas užsakymas" langą.
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Metodai.id_uzsakymo = senasID;
            this.Close();
        }

        /// <summary>
        /// Mygtuko "Kurti" įgalinimas priklausomai nuo formos užpildymo.
        /// Suveikia kaskart pakeitus reikšmes TextBox'uose.
        /// </summary>
        private void EnableButton(object sender, TextChangedEventArgs e)
        {
            if (ID_text.IsLoaded)
            {
                string richtext = new TextRange(Id_Kiekis_RichText.Document.ContentStart, Id_Kiekis_RichText.Document.ContentEnd).Text;
                richtext = richtext.Remove(richtext.Length - 2);
                if (ID_text.Text.Length > 0 && Klientas_text.Text.Length > 0 && Pardavejas_text.Text.Length > 0 && Data_text.Text.Length > 0 && richtext.Length > 2)
                    Kurti.IsEnabled = true;
                else
                    Kurti.IsEnabled = false;
            }
        }

        /// <summary>
        /// Naujo identifikatoriaus kūrimas.
        /// </summary>
        private void GenerateID()
        {
            senasID = Metodai.id_uzsakymo;
            string id = "U_" + (int.Parse(senasID.Substring(2)) + 1).ToString("D4");
            Metodai.id_uzsakymo = id;
            ID_text.Text = id;
        }

        /// <summary>
        /// Mygtuko "Kurti" įgalinimas priklausomai nuo formos užpildymo.
        /// Suveikia kaskart pakeitus reikšmes ComboBox'uose.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void text_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string richtext = new TextRange(Id_Kiekis_RichText.Document.ContentStart, Id_Kiekis_RichText.Document.ContentEnd).Text;
            richtext = richtext.Remove(richtext.Length - 2);
            if (Klientas_text.SelectedIndex>0 && Pardavejas_text.SelectedIndex>0 && ID_text.Text.Length > 0 && Data_text.Text.Length > 0 && richtext.Length > 2)
                Kurti.IsEnabled = true;
            else
                Kurti.IsEnabled = false;

        }

        /// <summary>
        /// Mygtuko "Pagalba" atliekami veiksmai.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1. Pasirinkite klientą.\n" +
                            "2. Pasirinkite pardavėją.\n" +
                            "3. Įveskite prekės ID ir perkamą kiekį.\n" +
                            "    Įvedimo formatas:\n" +
                            "    3.1. Įveskite prekės identifikatorių (sveikas skaičius).\n" +
                            "    3.2. Padėkite tarpą ir įveskite prekių kiekį.\n" +
                            "    3.3. Perėję į kitą eilutę (paspaudus Enter mygtuką)\n" +
                            "         galite įvesti daugiau nei vieną prekę.\n" +
                            "         Kiekviena prekė turi būti atskiroje eilutėje.\n" +
                            "4. Programa įveda šiandienos datą automatiškai, tačiau galima ją pakeisti.\n" +
                            "5. Paspauskite mygtuką 'Kurti'.", "Pagalba");
        }
    }
}
