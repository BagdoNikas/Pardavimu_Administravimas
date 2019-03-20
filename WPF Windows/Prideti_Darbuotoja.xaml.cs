using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Administravimas.WPF_Windows
{
    /// <summary>
    /// Interaction logic for Prideti_Darbuotoja.xaml
    /// </summary>
    public partial class Prideti_Darbuotoja : Window
    {
        string senasid;

        /// <summary>
        /// PAGRINDINIS METODAS ĮGLINANTIS PAGRINDINIAS LANGO SAVYBES
        /// NEIŠTRYNTI
        /// </summary>
        public Prideti_Darbuotoja()
        {
            InitializeComponent();

            Generate_ID();
        }

        /// <summary>
        /// Sukuria naują darbuotoją/Pardavėją
        /// </summary>
        private void Itraukti_Click(object sender, RoutedEventArgs e)
        {
            Metodai.Naujas_Darbuotojas(tabelis.Text, vardas.Text);
            this.Close();
        }

        /// <summary>
        /// Išjungia tik Pridėti_darbuotoja langą
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Metodai.id_darbuotojo = senasid;
            this.Close();
        }

        /// <summary>
        /// Generuoja naują id kodą pagal pasirinktą tipą ir jau egzistuojančius kodus
        /// </summary>
        public void Generate_ID()
        {
            senasid = Metodai.id_darbuotojo;
            string id = "TN" + (int.Parse(senasid.Substring(2)) + 1).ToString("D3");
            Metodai.id_darbuotojo = id;
            tabelis.Text = id;
        }

        /// <summary>
        /// Jeigu visi textbox yra užpildyti tai itraukti mygtukas įjungiamas, jei neužpildyti tai išjungtas
        /// </summary>
        private void EnableButton(Object sender, TextChangedEventArgs e)
        {
            if (tabelis.Text.Length > 0 && vardas.Text.Length > 0)
                Itraukti.IsEnabled = true;
            else
                Itraukti.IsEnabled = false;
        }
    }
}
