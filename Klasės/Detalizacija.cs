using System;


namespace Administravimas
{
    /// <summary>
    /// Prekių aprašymų saugojimo klasė
    /// </summary>
    public class Detalizacija : IComparable<Detalizacija>
    {
        public string ID { get; set; } // Užsakymo id
        public string[] PrekėsID { get; set; } // Prekės id
        public short[] Kiekis { get; set; } // Prekių kiekis

        /// <summary>
        /// Tuščias konstruktorius
        /// </summary>
        public Detalizacija() { }

        /// <summary>
        /// Konstruktorius užsakymo detalizacijai užpildyti
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        /// <param name="kiekis"></param>
        public Detalizacija(string uid, string[] pid, short[] kiekis)
        {
            this.ID = uid;
            this.PrekėsID = pid;
            this.Kiekis = kiekis;
        }

        /// <summary>
        /// Užklotas palyginimo operatorius
        /// </summary>
        /// <param name="other">elementas su kuriuo lyginama</param>
        /// <returns></returns>
        public int CompareTo(Detalizacija other)
        {
            int poz = string.Compare(this.ID, other.ID, StringComparison.CurrentCulture);

            if (poz < 0)
                return 1;
            else
                return -1;
        }

        //Reikia tikrint ar gerai spausidina
        public override string ToString()
        {
            string pidnkiekis = PrekėsID[0] + " " + Kiekis[0] + ";";
            for(int i = 1; i < PrekėsID.Length; i++)
            {
                pidnkiekis = pidnkiekis + PrekėsID[i] + " " + Kiekis[i] + ";";
            }
            return ID + ";" + pidnkiekis;
        }
    }
}
