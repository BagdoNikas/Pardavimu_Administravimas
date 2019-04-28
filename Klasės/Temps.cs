using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Administravimas.Klasės
{
    class Temps
    {
        //public Excel.Application excel = new Excel.Application();

        //public void Skaitymas()
        //{
        //    Excel.Workbook wb1 = excel.Workbooks.Open(UDataFile);
        //    //-------------------------Užsakymai----------------------------
        //    Excel.Worksheet ws1 = wb1.Worksheets[1];
        //    Excel.Range range1 = ws1.UsedRange;
        //    int rowCount1 = range1.Rows.Count;
        //    int colCount1 = range1.Columns.Count;

        //    for (int i = 2; i <= rowCount1; i++)
        //    {
        //        if (range1.Cells[i, colCount1] != null && range1.Cells[i, colCount1].Value2 != null)
        //        {
        //            int j = 0;
        //            string id = (string)(range1.Cells[i, j++] as Excel.Range).Value2;
        //            string klientas = (string)(range1.Cells[i, j++] as Excel.Range).Value2;
        //            double suma = range1.Cells[i, j++].Value;
        //            string pardavėjas = range1.Cells[i, j++].Value;
        //            DateTime data = (DateTime)range1.Cells[i, j++].Value;
        //            Užsakymas dummy = new Užsakymas(id, klientas, suma, pardavėjas, data);
        //            Užsakymai.Add(dummy);
        //        }
        //    }

        //    //Detalizacija
        //    Excel.Worksheet ws2 = wb1.Worksheets[2];
        //    Excel.Range range2 = ws2.UsedRange;
        //    int rowCount2 = range2.Rows.Count;
        //    int colCount2 = range2.Columns.Count;

        //    for (int i = 2; i <= rowCount2; i++)
        //    {
        //        if (range2.Cells[i, colCount2] != null && range2.Cells[i, colCount2].Value2 != null)
        //        {
        //            int j = 0;
        //            string uid = range2.Cells[i, j++];
        //            string pid = range2.Cells[i, j++];
        //            short kiek = range2.Cells[i, j++];
        //            Detalizacija dummy = new Detalizacija(uid, pid, kiek);
        //            Detalės.Add(dummy);
        //        }
        //    }
        //    wb1.Close(0);
        //    //--------------------------------------------------------------

        //    Excel.Workbook wb2 = excel.Workbooks.Open(PDataFile);

        //    //-------------------------Prekės-------------------------------
        //    Excel.Worksheet ws3 = wb2.Worksheets[1];
        //    Excel.Range range3 = ws3.UsedRange;
        //    int rowCount3 = range3.Rows.Count;
        //    int colCount3 = range3.Columns.Count;

        //    for (int i = 2; i <= rowCount3; i++)
        //    {
        //        if (range3.Cells[i, colCount3] != null && range3.Cells[i, colCount3].Value2 != null)
        //        {
        //            int j = 0;
        //            string id = range3.Cells[i, j++];
        //            string pav = range3.Cells[i, j++];
        //            double kain = range3.Cells[i, j++];
        //            Prekė dummy = new Prekė(id, pav, kain);
        //            Prekės.Add(dummy);
        //        }
        //    }

        //    wb2.Close(0);
        //    //--------------------------------------------------------------

        //    Excel.Workbook wb3 = excel.Workbooks.Open(DKDataFile);

        //    //-------------------------Klientai-----------------------------
        //    Excel.Worksheet ws4 = wb3.Worksheets[1];
        //    Excel.Range range4 = ws4.UsedRange;
        //    int rowCount4 = range4.Rows.Count;
        //    int colCount4 = range4.Columns.Count;

        //    for (int i = 2; i <= rowCount4; i++)
        //    {
        //        if (range4.Cells[i, colCount4] != null && range4.Cells[i, colCount4].Value2 != null)
        //        {
        //            int j = 0;
        //            string id = range4.Cells[i, j++];
        //            string tipas = range4.Cells[i, j++];
        //            string pav = range4.Cells[i, j++];
        //            long kodas = range4.Cells[i, j++];
        //            long tel = range4.Cells[i, j++];
        //            Klientas dummy = new Klientas(id, tipas, pav, kodas, tel);
        //            Klientai.Add(dummy);
        //        }
        //    }

        //    //-------------------------Darbuotojai--------------------------
        //    Excel.Worksheet ws5 = wb3.Worksheets[2];
        //    Excel.Range range5 = ws5.UsedRange;
        //    int rowCount5 = range5.Rows.Count;
        //    int colCount5 = range5.Columns.Count;

        //    for (int i = 2; i <= rowCount5; i++)
        //    {
        //        if (range5.Cells[i, colCount5] != null && range5.Cells[i, colCount5].Value2 != null)
        //        {
        //            int j = 0;
        //            string id = range5.Cells[i, j++];
        //            string varpav = range5.Cells[i, j++];
        //            Pardavėjas dummy = new Pardavėjas(id, varpav);
        //            Pardavėjai.Add(dummy);
        //        }
        //    }

        //    wb3.Close(0);
        //    //--------------------------------------------------------------
        //}

        //public void Naujas_Užsakymas(string id, string klientas, double suma, string darbuotojas, DateTime data, string pid, short kiekis)
        //{
        //    //List'ų papildymas
        //    Užsakymas užs = new Užsakymas(id, klientas, suma, darbuotojas, data);
        //    Detalizacija det = new Detalizacija(id, pid, kiekis);
        //    Užsakymai.Add(užs);
        //    Detalės.Add(det);

        //    //Excel'io failo papildymas
        //    Excel.Workbook wb = excel.Workbooks.Open(UDataFile);
        //    Excel.Worksheet ws1 = wb.Worksheets[1];
        //    Excel.Range range1 = ws1.UsedRange;
        //    int rowCount1 = range1.Rows.Count;
        //    int colCount1 = range1.Columns.Count;

        //    int i = rowCount1++;
        //    int j = 0;
        //    range1.Cells[i, j++] = id;
        //    range1.Cells[i, j++] = klientas;
        //    range1.Cells[i, j++] = suma;
        //    range1.Cells[i, j++] = darbuotojas;
        //    range1.Cells[i, j++] = data;

        //    Excel.Worksheet ws2 = wb.Worksheets[2];
        //    Excel.Range range2 = ws2.UsedRange;
        //    int rowCount2 = range2.Rows.Count;
        //    int colCount2 = range2.Columns.Count;

        //    int k = rowCount2++;
        //    int l = 0;
        //    range2.Cells[k, l++] = id;
        //    range2.Cells[k, l++] = pid;
        //    range2.Cells[k, l++] = kiekis;

        //    wb.Close(0);
        //}

        //public void Naujas_Klientas(string id, string tipas, string pavadinimas, long kodas, long telNr)
        //{
        //    //List'o papildymas
        //    Klientas klie = new Klientas(id, tipas, pavadinimas, kodas, telNr);
        //    Klientai.Add(klie);

        //    //Excel'io failo papildymas
        //    Excel.Workbook wb = excel.Workbooks.Open(DKDataFile);
        //    Excel.Worksheet ws = wb.Worksheets[1];
        //    Excel.Range range = ws.UsedRange;
        //    int rowCount = range.Rows.Count;
        //    int colCount = range.Columns.Count;

        //    int i = rowCount++;
        //    int j = 0;
        //    range.Cells[i, j++] = id;
        //    range.Cells[i, j++] = tipas;
        //    range.Cells[i, j++] = pavadinimas;
        //    range.Cells[i, j++] = kodas;
        //    range.Cells[i, j++] = telNr;

        //    wb.Close(0);
        //}

        //public void Naujas_Darbuotojas(string tabelis, string varpav)
        //{
        //    //List'o papildymas
        //    Pardavėjas pard = new Pardavėjas(tabelis, varpav);
        //    Pardavėjai.Add(pard);

        //    //Excel'io failo papildymas
        //    Excel.Workbook wb = excel.Workbooks.Open(DKDataFile);
        //    Excel.Worksheet ws = wb.Worksheets[2];
        //    Excel.Range range = ws.UsedRange;
        //    int rowCount = range.Rows.Count;
        //    int colCount = range.Columns.Count;

        //    int i = rowCount++;
        //    int j = 0;
        //    range.Cells[i, j++] = tabelis;
        //    range.Cells[i, j++] = varpav;

        //    wb.Close(0);
        //}
    }
}
