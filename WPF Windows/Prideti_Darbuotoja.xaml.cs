using System;
using System.Windows;
using System.Windows.Controls;

namespace Administravimas.WPF_Windows
{
    /// <summary>
    /// Interaction logic for Prideti_Darbuotoja.xaml
    /// </summary>
    public partial class Prideti_Darbuotoja : Window
    {
        string oldID;

        public Prideti_Darbuotoja()
        {
            InitializeComponent();

            Generate_ID();
        }

        /// <summary>
        /// Mygtuko "Pridėti" paspaudimu sukuriamas naujas darbuotojas.
        /// </summary>
        private void Itraukti_Click(object sender, RoutedEventArgs e)
        {
            Metodai.Naujas_Darbuotojas(tabelis.Text, vardas.Text);
            this.Close();
        }

        /// <summary>
        /// Išjungia "Naujas darbuotojas" langą.
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Metodai.id_darbuotojo = oldID;
            this.Close();
        }

        /// <summary>
        /// Naujo identifikatoriaus generavimas pagal jau egzistuojančius ID.
        /// </summary>
        public void Generate_ID()
        {
            oldID = Metodai.id_darbuotojo;
            string id = "TN" + (int.Parse(oldID.Substring(2)) + 1).ToString("D3");
            Metodai.id_darbuotojo = id;
            tabelis.Text = id;
        }

        /// <summary>
        /// Mygtuko "Pridėti" įgalinimas priklausomai nuo formos užpildymo.
        /// Suveikia kaskart pakeitus Textbox'ų reikšmes.
        /// </summary>
        private void EnableButton(Object sender, TextChangedEventArgs e)
        {
            if (tabelis.Text.Length > 0 && vardas.Text.Length > 0)
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
           MessageBox.Show("1. Įveskite darbuotojo vardą ir pavardę.\n" +
                           "2. Paspauskite mygtuką 'Įtraukti'.","Pagalba");
        }
    }
}
