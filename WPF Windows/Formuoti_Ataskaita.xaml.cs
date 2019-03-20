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
    /// Interaction logic for Formuoti_Ataskaita.xaml
    /// </summary>
    public partial class Formuoti_Ataskaita : Window
    {
        public Formuoti_Ataskaita()
        {
            InitializeComponent();
            Kriterijus_combo.Items.Add(".pdf");
            Kriterijus_combo.Items.Add(".docx");
        }
        private void Formuoti_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
