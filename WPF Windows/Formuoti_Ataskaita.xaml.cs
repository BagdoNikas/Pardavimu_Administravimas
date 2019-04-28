using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Xceed.Words.NET;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

//------------------------------------------------------------------
//---- prideti reikia nuget packages: ------------------------------
//---- Microsoft.Office.Interop.Word  ------------------------------
//---- DocX                           ------------------------------
//------------------------------------------------------------------

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

        string docxName = "";
        string pdfDocName = "";

        /// <summary>
        /// Iškviečia ataskaitos formatavimo metodus pagal combobox pasirinkimą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Formuoti_Click(object sender, RoutedEventArgs e)
        {
            string formatas = Kriterijus_combo.SelectedItem.ToString();
            string path = Directorija();

            pdfDocName += path + '\\' + Name_textbox.Text + ".pdf";
            docxName += path + '\\' + Name_textbox.Text + ".docx";

            switch (formatas)
            {
                case ".pdf":
                    Pdf(); Process.Start(pdfDocName); break;
                case ".docx":
                    Docx(docxName); Process.Start(docxName); break;
            }

            this.Close();
        }

        /// <summary>
        /// Uždaro šį langą
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Sukuria Pdf faila pries tai iskvieciant metoda sukurti docx failui tada istrina docx faila
        /// </summary>
        public void Pdf()
        {
            Docx(docxName);

            var appWord = new Word.Application();
            if (appWord.Documents != null)
            {
                var wordDocument = appWord.Documents.Open(docxName);
                //-----------Cia pakeist (PREFERABLY SAVE THE NAME THE USER WRITES)------------
                if (wordDocument != null)
                {
                    wordDocument.ExportAsFixedFormat(pdfDocName, Word.WdExportFormat.wdExportFormatPDF);
                    wordDocument.Close();
                }
                appWord.Quit();
            }
            File.Delete(docxName);
        }

        /// <summary>
        /// Sukuria docx faila ir iraso duomenis
        /// </summary>
        /// <param name="fileName"></param>
        public void Docx(string fileName)
        {
            //------Cia sukuria nauja docx failą-----//
            var doc = DocX.Create(fileName);

            //-------------------Link kur parodo kaip formatuoti viska --------------------//
            // https://www.c-sharpcorner.com/article/generate-word-document-using-c-sharp/ //
            //-----------------------------------------------------------------------------//

            #region Title nustatymai
            string title = DateTime.Now.Year + " metų ataskaita";

            Formatting titleFormat = new Formatting();
            titleFormat.Size = 18;

            Paragraph Title = doc.InsertParagraph(title, false, titleFormat);
            Title.Alignment = Alignment.center;
            #endregion

            //----------Cia galima deti visa kita likusi teksta----------------------------//
            #region texto nustatymai
            Formatting textFormat = new Formatting();
            textFormat.Size = 11;

            #endregion

            #region Lenteles nustatymai
            Table t = doc.AddTable(1 + Metodai.prekiuskaicius, 6);
            t.Alignment = Alignment.center;
            t.Design = TableDesign.TableGrid;

            for (int i = 0; i < 6; i++)
                t.Rows[0].Cells[i].FillColor = Color.LightGray;
            t.SetWidthsPercentage(new[] { 7f, 30f, 20f, 15f, 15f, 13f }, 600);
            t.Rows[0].Cells[0].Paragraphs.First().Append("Nr.");
            t.Rows[0].Cells[1].Paragraphs.First().Append("Pirkėjas");
            t.Rows[0].Cells[2].Paragraphs.First().Append("Prekė");
            t.Rows[0].Cells[3].Paragraphs.First().Append("Kiekis");
            t.Rows[0].Cells[4].Paragraphs.First().Append("Data");
            t.Rows[0].Cells[5].Paragraphs.First().Append("Suma");
            #endregion

            #region sukuria elementus lentelems / sugeneruoja lentelę
            // k - papyldomas kintamasis pereiti i kita eilute nes keletas prekiu vienam uzsakymui
            // l - kad graziai kiekviena preke susidetu i atskiras eilutes, nenaudojant kas kart pasikeitus uzsakymui atsirastu tuscia eilute
            int k = 1, l = 1;
            double kaina = 0;
            double prekeskaina;
            for (int i = 0; i < Metodai.Užsakymai.Count; i++)
            {
                l = 1;
                for (int j = 0; j < Metodai.Detalės[i].PrekėsID.Length; j++)
                {
                    t.Rows[i + k].Cells[0].Paragraphs.First().Append((i + k).ToString() + '.');
                    t.Rows[i + k].Cells[1].Paragraphs.First().Append(Metodai.Užsakymai[i].Pirkėjas);
                    t.Rows[i + k].Cells[2].Paragraphs.First().Append(Metodai.Detalės[i].PrekėsID[j]);
                    t.Rows[i + k].Cells[3].Paragraphs.First().Append(Metodai.Detalės[i].Kiekis[j].ToString());
                    t.Rows[i + k].Cells[4].Paragraphs.First().Append(Metodai.Užsakymai[i].Data.ToString());
                    prekeskaina = Metodai.Prekės.FirstOrDefault(stringToCheck => stringToCheck.ID.Contains(Metodai.Detalės[i].PrekėsID[j])).Kaina;
                    kaina = Metodai.Detalės[i].Kiekis[j] * prekeskaina;
                    t.Rows[i + k].Cells[5].Paragraphs.First().Append(kaina.ToString());
                    if (l != Metodai.Detalės[i].PrekėsID.Length)
                    {
                        l++;
                        k++;
                    }
                }
            }
            #endregion

            doc.InsertTable(t);
            doc.Save();
        }

        /// <summary>
        /// Leidzia vartotojui pasirinkti direktorija i kuria isaugoti ataskaita
        /// </summary>
        /// <returns></returns>
        public string Directorija()
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string name = dialog.SelectedPath;
            return name;
        }

        /// <summary>
        /// Ivedus ataskaitos pavadinima igalinamas formuoti mygtukas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Name_textbox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Name_textbox.IsLoaded)
            {
                if (Name_textbox.Text.Length > 0)
                    formuoti.IsEnabled = true;
                else
                    formuoti.IsEnabled = false;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("1. Pasirinkite ataskaitos formatą (pdf ar docx(Word dokumentas))\n" +
                                           "2. Įveskite ataskaitos pavadinimą. \n" +
                                           "3. Pasirinkite vietą į kurią norite išaugoti ataskaitos dokumentą", "Pagalba");
        }
    }
}
