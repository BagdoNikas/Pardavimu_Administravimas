using System;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Administravimas
{
    /// <summary>
    /// Metodų aprašymo klasė
    /// </summary>
    public class Metodai
    {
        //-----------------Duomenų saugojimo sąrašai------------------------
        public static List<Užsakymas> Užsakymai = new List<Užsakymas>();
        public static List<Detalizacija> Detalės = new List<Detalizacija>();
        public static List<Prekė> Prekės = new List<Prekė>();
        public static List<Klientas> Klientai = new List<Klientas>();
        public static List<Pardavėjas> Pardavėjai = new List<Pardavėjas>();

        //-----------------Duomenų failų konstantos-------------------------
        const string UžDataFile = "..\\..\\Failai\\Užsakymai.txt";
        const string DeDataFile = "..\\..\\Failai\\Detalizacija.txt";
        const string PDataFile = "..\\..\\Failai\\Prekės.txt";
        const string DaDataFile = "..\\..\\Failai\\Darbuotojai.txt";
        const string KlDataFile = "..\\..\\Failai\\Klientai.txt";

        //-----------------Generuojamų ID kintamieji------------------------
        public static string id_klientoJ;
        public static string id_klientoF;
        public static string id_darbuotojo;
        public static string id_uzsakymo;

        /// <summary>
        /// Duomenų nuskaitymo iš failų metodas
        /// </summary>
        public static void Skaitymas()
        {
            //-------------------------Užsakymai----------------------------
            using (StreamReader reader = new StreamReader(UžDataFile, Encoding.GetEncoding(1257)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] part = line.Split(';');
                    id_uzsakymo = part[0];
                    string klientas = part[1];
                    double suma = double.Parse(part[2]);
                    string pardavėjas = part[3];
                    DateTime data = DateTime.Parse(part[4]);
                    Užsakymas dummy = new Užsakymas(id_uzsakymo, klientas, suma, pardavėjas, data);
                    Užsakymai.Add(dummy);
                }
            }

            //-------------------------Detalizacija-------------------------
            using (StreamReader reader = new StreamReader(DeDataFile, Encoding.GetEncoding(1257)))
            {
                string line;
                List<string> pid = new List<string>();
                List<short> kiekiai = new List<short>();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] part = line.Split(';');
                    string uid = part[0];
                    int i = 1;
                    while (part[i] != "")
                    {
                        string pidnkiekis = part[i];
                        string[] pikd = pidnkiekis.Split(' ');
                        string id = pikd[0];
                        short kiekis = short.Parse(pikd[1]);
                        pid.Add(id);
                        kiekiai.Add(kiekis);
                        i++;
                    }
                    Detalizacija dummy = new Detalizacija(uid, pid.ToArray(), kiekiai.ToArray());
                    Detalės.Add(dummy);
                }
            }

            //-------------------------Prekės-------------------------------
            using (StreamReader reader = new StreamReader(PDataFile, Encoding.GetEncoding(1257)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] part = line.Split(';');
                    string id = part[0];
                    string pav = part[1];
                    double kain = double.Parse(part[2]);
                    Prekė dummy = new Prekė(id, pav, kain);
                    Prekės.Add(dummy);
                }
            }

            //-------------------------Klientai-----------------------------
            using (StreamReader reader = new StreamReader(KlDataFile, Encoding.GetEncoding(1257)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] part = line.Split(';');
                    string tipas = part[1];
                    string pav = part[2];
                    long kodas = long.Parse(part[3]);
                    long tel = long.Parse(part[4]);

                    Klientas dummy = new Klientas();
                    switch (part[0][0])
                    {
                        case 'F': id_klientoF = part[0]; dummy = new Klientas(id_klientoF, tipas, pav, kodas, tel); break;
                        case 'J': id_klientoJ = part[0]; dummy = new Klientas(id_klientoJ, tipas, pav, kodas, tel); break;
                    }
                    Klientai.Add(dummy);
                }
            }

            //-------------------------Darbuotojai--------------------------
            using (StreamReader reader = new StreamReader(DaDataFile, Encoding.GetEncoding(1257)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] part = line.Split(';');
                    id_darbuotojo = part[0];
                    string varpav = part[1];
                    Pardavėjas dummy = new Pardavėjas(id_darbuotojo, varpav);
                    Pardavėjai.Add(dummy);
                }
            }
        }

        /// <summary>
        /// Naujo užsakymo formavimo metodas
        /// </summary>
        /// <param name="id">užsakymo identifikatorius</param>
        /// <param name="klientas">kliento vardas ir pavardė arba pavadinimas</param>
        /// <param name="suma">užsakytų prekių suma</param>
        /// <param name="darbuotojas">pardavėjo vardas ir pavardė</param>
        /// <param name="data">užsakymo pateikimo data</param>
        /// <param name="pidkiekis">užsakytų prekių id ir kiekiai</param>
        public static void Naujas_Užsakymas(string id, string klientas, double suma, string darbuotojas, DateTime data, string pidkiekis)
        {
            //List'ų papildymas
            Užsakymas užs = new Užsakymas(id, klientas, suma, darbuotojas, data);
            string[] eil = pidkiekis.Split('\n');
            string[] pid = new string[eil.Length];
            short[] kiekis = new short[eil.Length];
            for (int i = 0; i < eil.Length; i += 2)
            {
                string[] parts = eil[i].Split(' ');
                pid[i] = parts[0];
                kiekis[i] = short.Parse(parts[1].Trim('\r'));
            }
            Detalizacija det = new Detalizacija(id, pid, kiekis);
            Užsakymai.Add(užs);
            Detalės.Add(det);
            // ^ ---> Nepyk, kad pakeičiau, taip man kiek logiškiau atrodo, aišku, jei netinka, visada galim pakeist


            //UŽKOMENTINAU NES TESTAVAU IVEDIMAS IS LANGO CIA MODIFIKUOT REIK
            //Text failo papildymas
            using (var fr = File.AppendText(UžDataFile))
            {
                fr.WriteLine(užs.ToString());
            }

            using (var fr = File.AppendText(DeDataFile))
            {
                fr.WriteLine(det.ToString());
            }
        }

        public static void Naujas_Klientas(string id, string tipas, string pavadinimas, long kodas, long telNr)
        {
            //List'o papildymas
            Klientas klientas = new Klientas(id, tipas, pavadinimas, kodas, telNr);
            Klientai.Add(klientas);

            //Text failo papildymas
            using (var fr = File.AppendText(KlDataFile))
            {
                fr.WriteLine(klientas.ToString());
            }
        }

        public static void Naujas_Darbuotojas(string tabelis, string varpav)
        {
            //List'o papildymas
            Pardavėjas pardavėjas = new Pardavėjas(tabelis, varpav);
            Pardavėjai.Add(pardavėjas);

            //Text failo papildymas
            using (var fr = File.AppendText(DaDataFile))
            {
                fr.WriteLine(pardavėjas.ToString());
            }
        }
    }
}
