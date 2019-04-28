using System;
using System.Collections.Generic;


namespace Administravimas
{
    /// <summary>
    /// Užsakymo informaciją aprašanti klasė
    /// </summary>
    public class Užsakymas : IComparable<Užsakymas>
    {
        public string ID { get; set; }  // Užsakymo ID kodas
        public string Pirkėjas { get; set; }  // Kliento duomenys
        public double Suma { get; set; }    // Galutinė užsakymo suma
        public string Darbuotojas { get; set; } // Aptarnavęs darbuotojas
        public DateTime Data { get; set; } // Užsakymo data

        /// <summary>
        /// Tuščias konstruktorius
        /// </summary>
        public Užsakymas() { }

        /// <summary>
        /// Konstruktorius prekės informacijai užpildyti
        /// </summary>
        /// <param name="id"></param>
        /// <param name="klientas"></param>
        /// <param name="prekė"></param>
        /// <param name="suma"></param>
        /// <param name="pardavėjas"></param>
        public Užsakymas(string id, string klientas, double suma, string pardavėjas, DateTime data)
        {
            ID = id;
            Pirkėjas = klientas;
            Suma = suma;
            Darbuotojas = pardavėjas;
            Data = data;
        }

        /// <summary>
        /// Užklotas palyginimo operatorius
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Užsakymas other)
        {
            int poz = string.Compare(this.ID, other.ID, StringComparison.CurrentCulture);

            if (poz < 0 || (poz == 0 && (this.Data > other.Data))) // Dėl daugiau/mažiau ženklo pasitikslinti
                return 1;
            else
                return -1;
        }

        /// <summary>
        /// Užklotas metodas sugeneruoti string tipo eilutę iš objekto duomenų
        /// </summary>
        /// <returns>suformuota string eilutę</returns>
        public override string ToString()
        {
            return ID + ";" + Pirkėjas + ";" + Suma.ToString() + ";" + Darbuotojas + ";" + Data.ToString();
        }
    }
}
