using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Administravimas.WPF_Windows
{
    /// <summary>
    /// Interaction logic for Pasalinti.xaml
    /// </summary>
    public partial class Prideti_preke : Window
    {
        string oldID;
        public Prideti_preke()
        {
            InitializeComponent();
            GenerateID();
        }

        /// <summary>
        /// Naujo identifikatoriaus generavimas pagal jau egzistuojančius ID.
        /// </summary>
        private void GenerateID()
        {
            oldID = Metodai.id_prekes;
            string id = "P" + (int.Parse(oldID.Substring(2)) + 1).ToString("D4");
            Metodai.id_prekes = id;
            pid.Text = id;
        }

        /// <summary>
        /// mygtuko "Pridėti" įgalinimas priklausomai nuo formos užpildymo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableButton(object sender, TextChangedEventArgs e)
        {
            if (pid.IsLoaded)
            {
                if (pid.Text.Length > 0 && pavadinimas.Text.Length > 0 && kaina.Text.Length > 0)
                    Add.IsEnabled = true;
                else
                    Add.IsEnabled = false;
            }
        }

        /// <summary>
        /// Mygtuko "Pridėti" atliekami veiksmai.
        /// Naujos prekės įtraukimas į sarašą.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            Metodai.Nauja_Prek(pid.Text, pavadinimas.Text, double.Parse(kaina.Text));
            this.Close();
            }
            catch
            {
                MessageBox.Show("Blogai įvesta kaina");
            }
        }

        /// <summary>
        /// Išjungia langą "Nauja prekė" mygtuko "Baigti" paspaudimu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Metodai.id_prekes = oldID;
            this.Close();
        }

        /// <summary>
        /// Mygtuko "Pagalba" atliekami veiksmai.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1. Įveskite prekės pavadinimą.\n" +
                            "2. Įveskite prekės kainą.\n" +
                            "3. Paspauskite mygtuką 'Įtraukti'.", "Pagalba");
        }
    }
}
