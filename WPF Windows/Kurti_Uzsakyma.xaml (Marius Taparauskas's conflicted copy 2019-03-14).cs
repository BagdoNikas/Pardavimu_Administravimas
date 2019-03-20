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
    /// Interaction logic for Kurti_Uzsakyma.xaml
    /// </summary>
    public partial class Kurti_Uzsakyma : Window
    {
        Metodai M = new Metodai();

        public Kurti_Uzsakyma()
        {
            InitializeComponent();
         //   M.Skaitymas();
        }

        private void Prideti_Click(object sender, RoutedEventArgs e)
        {
            //   M.Skaitymas();
        }

        private void Kurti_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
