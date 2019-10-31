using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asigurare_viata.Clase
{
    class Asigurare
    {
        private double sumaAsigurata;
        private double prima;
        private string perioadaPlata;
        private bool urmatoareaBanda;
        private Client client = new Client();
        public static double extraPrima;
        public static double primaTotala;
        private DateTime dataAsigurare;
        public static int ID_AGENT;
        public static int Nr_Asig = 0;

        public Asigurare()
        {

        }

        public Asigurare(double sumaAsigurata, double prima, string perioadaPlata, DateTime data, string nume, string prenume, int varsta, string sex, string cnp, string adresa, string nrTelefon, string status)
        {
            this.SumaAsigurata = sumaAsigurata;
            this.Prima = prima;
            this.PerioadaPlata = perioadaPlata;
            this.dataAsigurare = data;
            Client.Nume = nume;
            Client.Prenume = prenume;
            Client.Varsta = varsta;
            Client.Sex = sex;
            Client.Cnp = cnp;
            Client.Adresa = adresa;
            Client.NrTelefon = nrTelefon;
            Client.Status = status;
        }

        public Asigurare(double sumaAsigurata, double prima, string perioadaPlata, DateTime data, string nume, string prenume, int varsta, string sex, string cnp, string adresa, double lat, double lng, string nrTelefon, string status)
        {
            this.SumaAsigurata = sumaAsigurata;
            this.Prima = prima;
            this.PerioadaPlata = perioadaPlata;
            this.dataAsigurare = data;
            Client.Nume = nume;
            Client.Prenume = prenume;
            Client.Varsta = varsta;
            Client.Sex = sex;
            Client.Cnp = cnp;
            Client.Adresa = adresa;
            Client.Lat = lat;
            Client.Lng = lng;
            Client.NrTelefon = nrTelefon;
            Client.Status = status;
        }

        public double SumaAsigurata { get => sumaAsigurata; set => sumaAsigurata = value; }
        public double Prima { get => prima; set => prima = value; }
        public string PerioadaPlata { get => perioadaPlata; set => perioadaPlata = value; }
        public bool UrmatoareaBanda { get => urmatoareaBanda; set => urmatoareaBanda = value; }
        public DateTime DataAsigurare { get => dataAsigurare; set => dataAsigurare = value; }
        internal Client Client { get => client; set => client = value; }

        public string numeFisierRata()
        {
            switch (perioadaPlata)
            {
                case "10 Ani":
                    {
                        if (sumaAsigurata >= Constante.MIN_BAND_1 && sumaAsigurata <= Constante.MAX_BAND_1)
                        {
                            return urmatoareaBanda ? "wl10payb2.txt" : "wl10payb1.txt";
                        }
                        else
                        if (sumaAsigurata >= Constante.MIN_BAND_2 && sumaAsigurata <= Constante.MAX_BAND_2)
                        {
                            return urmatoareaBanda ? "wl10payb3.txt" : "wl10payb2.txt";
                        }
                        else
                        if (sumaAsigurata >= Constante.MIN_BAND_3 && sumaAsigurata <= Constante.MAX_BAND_3)
                        {
                            return urmatoareaBanda ? "wl10payb4.txt" : "wl10payb3.txt";
                        }
                        else
                        if (sumaAsigurata >= Constante.MIN_BAND_4 && sumaAsigurata <= Constante.MAX_BAND_4)
                        {
                            return urmatoareaBanda ? "wl10payb5.txt" : "wl10payb4.txt";
                        }
                        if (sumaAsigurata >= Constante.MIN_BAND_5 && sumaAsigurata <= Constante.MAX_BAND_5)
                        {
                            return "wl10payb5.txt";
                        }
                        break;
                    }
                case "20 Ani":
                    {
                        if (sumaAsigurata >= Constante.MIN_BAND_1 && sumaAsigurata <= Constante.MAX_BAND_1)
                        {
                            return "wl20payb1.txt";
                        }
                        else
                         if (sumaAsigurata >= Constante.MIN_BAND_2 && sumaAsigurata <= Constante.MAX_BAND_2)
                        {
                            return "wl20payb2.txt";
                        }
                        else
                         if (sumaAsigurata >= Constante.MIN_BAND_3 && sumaAsigurata <= Constante.MAX_BAND_3)
                        {
                            return "wl20payb3.txt";
                        }
                        else
                         if (sumaAsigurata >= Constante.MIN_BAND_4 && sumaAsigurata <= Constante.MAX_BAND_4)
                        {
                            return "wl20payb4.txt";
                        }
                        else
                        if (sumaAsigurata >= Constante.MIN_BAND_5 && sumaAsigurata <= Constante.MAX_BAND_5)
                        {
                            return "wl20payb5.txt";
                        }
                        break;
                    }
                case "Toata Viata":
                    {
                        if (sumaAsigurata >= Constante.MIN_BAND_1 && sumaAsigurata <= Constante.MAX_BAND_1)
                        {
                            return "wl100payb1.txt";
                        }
                        else
                        if (sumaAsigurata >= Constante.MIN_BAND_2 && sumaAsigurata <= Constante.MAX_BAND_2)
                        {
                            return "wl100payb2.txt";
                        }
                        else
                        if (sumaAsigurata >= Constante.MIN_BAND_3 && sumaAsigurata <= Constante.MAX_BAND_3)
                        {
                            return "wl100payb3.txt";
                        }
                        else
                        if (sumaAsigurata >= Constante.MIN_BAND_4 && sumaAsigurata <= Constante.MAX_BAND_4)
                        {
                            return "wl100payb4.txt";
                        }
                        if (sumaAsigurata >= Constante.MIN_BAND_5 && sumaAsigurata <= Constante.MAX_BAND_5)
                        {
                            return "wl100payb5.txt";
                        }
                        break;
                    }
            }
            return null;
        }

        public double[,] citesteRata()
        {
            double[,] rate = new double[86, 4];

            StreamReader sr = new StreamReader(numeFisierRata());
            string linie = null;
            int i = 0;
            while ((linie = sr.ReadLine()) != null)
            {
                try
                {
                    rate[i, 0] = Convert.ToDouble(linie.Split(',')[0]);
                    rate[i, 1] = Convert.ToDouble(linie.Split(',')[1]);
                    rate[i, 2] = Convert.ToDouble(linie.Split(',')[2]);
                    rate[i, 3] = Convert.ToDouble(linie.Split(',')[3]);
                    i++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            sr.Close();

            return rate;
        }

        public double calculeazaPrima()
        {
            double[,] rate = citesteRata();
            switch (Client.Sex)
            {
                case "Femeie":
                    {
                        if (Client.Status == "NFM")
                            prima = Math.Round(rate[Client.Varsta, Constante.F_NFM] * sumaAsigurata / 1000 + 50, 2);
                        else
                            prima = Math.Round(rate[Client.Varsta, Constante.F_FUM] * sumaAsigurata / 1000 + 50, 2);
                        break;
                    }
                case "Barbat":
                    {
                        if (Client.Status == "NFM")
                            prima = Math.Round(rate[Client.Varsta, Constante.M_NFM] * sumaAsigurata / 1000 + 50, 2);
                        else
                            prima = Math.Round(rate[Client.Varsta, Constante.M_FUM] * sumaAsigurata / 1000 + 50, 2);
                        break;
                    }
            };
            return prima;
        }

        public double calucleazaSumaAsigurare(double rezolvaPentru, double limInf, double limSup, double suma)
        {
            sumaAsigurata = limInf + (limSup - limInf) / 2;
            prima = calculeazaPrima();
            if (Math.Round(limSup - limInf, 2) == 0)
                return sumaAsigurata = Math.Floor(suma);
            else
            if (Math.Round(rezolvaPentru - prima, 2) == 0)
            {
                if (suma == 0)
                    incearcaRezolvareBandaUrmatoare();
                else
                if (sumaAsigurata < suma)
                    sumaAsigurata = suma;
                return sumaAsigurata = Math.Floor(sumaAsigurata);
            }
            else
            if (rezolvaPentru < prima)
                return calucleazaSumaAsigurare(rezolvaPentru, limInf, sumaAsigurata, suma);
            else
                return calucleazaSumaAsigurare(rezolvaPentru, sumaAsigurata, limSup, suma);
        }

        public void incearcaRezolvareBandaUrmatoare()
        {
            if (sumaAsigurata >= Constante.MIN_BAND_1 && sumaAsigurata <= Constante.MAX_BAND_1)
            {
                calucleazaSumaAsigurare(prima, Constante.MIN_BAND_2, Constante.MAX_BAND_2, sumaAsigurata);
            }
            else
                        if (sumaAsigurata >= Constante.MIN_BAND_2 && sumaAsigurata <= Constante.MAX_BAND_2)
            {
                calucleazaSumaAsigurare(prima, Constante.MIN_BAND_3, Constante.MAX_BAND_3, sumaAsigurata);
            }
            else
                        if (sumaAsigurata >= Constante.MIN_BAND_3 && sumaAsigurata <= Constante.MAX_BAND_3)
            {
                calucleazaSumaAsigurare(prima, Constante.MIN_BAND_4, Constante.MAX_BAND_4, sumaAsigurata);
            }
            else
                        if (sumaAsigurata >= Constante.MIN_BAND_4 && sumaAsigurata <= Constante.MAX_BAND_4)
            {
                calucleazaSumaAsigurare(prima, Constante.MIN_BAND_5, Constante.MAX_BAND_5, sumaAsigurata);
            }

        }

        public int[] dataVizualizareVarsta()
        {
            int[] vage = new int[101];
            int k = 0;
            for (int i = Client.Varsta + 1; i <= 100; i++)
            {
                vage[k++] = i;
            }
            return vage;
        }

        public double[] primaAnualaTotala()
        {
            double[] primaAnuala = new double[101];

            switch (perioadaPlata)
            {
                case "10 Ani":
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            primaAnuala[i] = Asigurare.primaTotala;
                        }
                        break;
                    }
                case "20 Ani":
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            primaAnuala[i] = Asigurare.primaTotala;
                        }
                        break;
                    }
                case "Toata Viata":
                    {
                        for (int i = 0; i <= 100 - Client.Varsta; i++)
                        {
                            primaAnuala[i] = Asigurare.primaTotala;
                        }
                        break;
                    }
            }
            return primaAnuala;
        }

        public double[] beneficiuMoarte()
        {
            double[] beneficiuMoarte = new double[101];
            for (int i = 0; i <= 100 - Client.Varsta; i++)
            {
                beneficiuMoarte[i] = sumaAsigurata;
            }
            return beneficiuMoarte;
        }

        public string numeFisierdbPUA()
        {
            switch (Client.Sex)
            {
                case "Femeie":
                    {
                        if (Client.Status == "NFM")
                            return "dbPUAfns.txt";
                        else
                            return "dbPUAfsm.txt";
                        break;
                    }
                case "Barbat":
                    {
                        if (Client.Status == "NFM")
                            return "dbPUAmns.txt";
                        else
                            return "dbPUAmsm.txt";
                        break;
                    }
            }
            return null;
        }

        public double[,] citireRatadbPUA()
        {
            double[,] rate = new double[101, 80];

            StreamReader sr = new StreamReader(numeFisierdbPUA());
            string linie = null;
            int i = 0;
            while ((linie = sr.ReadLine()) != null)
            {
                try
                {
                    for (int j = 0; j < 79; j++)
                    {
                        rate[i, j] = Convert.ToDouble(linie.Split(',')[j]);
                    }
                    i++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            sr.Close();
            return rate;
        }

        public double[] beneficiuDecesExtra()
        {
            double[,] ratePUA = citireRatadbPUA();
            double[] gcsv = valoareRascumparareGarantata();
            double[] dbPUA = new double[101];
            dbPUA[0] = Math.Round(ratePUA[0, Client.Varsta] * gcsv[0] * 0.055 * 0.01 / 10);
            for (int i = 1; i <= 100 - Client.Varsta; i++)
            {
                dbPUA[i] = dbPUA[i - 1] + Math.Round(ratePUA[i, Client.Varsta] * gcsv[i] * 0.055 * 0.01 / 10);
            }
            return dbPUA;
        }

        public double[] beneficiuDecesTotal()
        {
            double[] beneficiuDecesTotal = new double[101];
            double[] dbPUA = beneficiuDecesExtra();
            for (int i = 0; i <= 100 - Client.Varsta; i++)
            {
                beneficiuDecesTotal[i] = sumaAsigurata + dbPUA[i];
            }
            return beneficiuDecesTotal;
        }

        public string numeFisierGCSV()
        {
            switch (perioadaPlata)
            {
             
                case "10 Ani":
                    {
                        if (Client.Sex == "Femeie")
                            return "gcsv10payf.txt";
                        else
                            return "gcsv10paym.txt";
                    }
                case "20 Ani":
                    {
                        if (Client.Sex == "Femeie")
                            return "gcsv20payf.txt";
                        else
                            return "gcsv20paym.txt";
                    }
                case "Toata Viata":
                    {
                        if (Client.Sex == "Femeie")
                            return "gcsv100payf.txt";
                        else
                            return "gcsv100paym.txt";
                    }
            }
            return null;
        }

        public double[,] citireRataGCSV()
        {
            double[,] rate = new double[101, 80];
            StreamReader sr = new StreamReader(numeFisierGCSV());
            string linie = null;
            int i = 0;
            while ((linie = sr.ReadLine()) != null)
            {
                try
                {
                    for (int j = 0; j < 79; j++)
                    {
                        rate[i, j] = Convert.ToDouble(linie.Split(',')[j]);
                    }
                    i++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            sr.Close();
            return rate;
        }

        public double[] valoareRascumparareGarantata()
        {
            double[] gcsv = new double[101];
            double[,] rate = citireRataGCSV();
            for (int i = 0; i <= 100 - Client.Varsta; i++)
            {
                gcsv[i] =  Math.Round(rate[i, Client.Varsta] / 1000 * sumaAsigurata);
            }
            return gcsv;
        }

        public string numeFisierCsvPUA()
        {
            switch (Client.Sex)
            {
                case "Femeie":
                    {
                        if (Client.Status == "NFM")
                            return "csvPUAfns.txt";
                        else
                            return "csvPUAfsm.txt";
                        break;
                    }
                case "Barbat":
                    {
                        if (Client.Status == "NFM")
                            return "csvPUAmns.txt";
                        else
                            return "csvPUAmsm.txt";
                        break;
                    }
            }
            return null;
        }

        public double[,] citireRataCsvPUA()
        {
            double[,] rate = new double[101, 80];

            StreamReader sr = new StreamReader(numeFisierCsvPUA());
            string linie = null;
            int i = 0;
            while ((linie = sr.ReadLine()) != null)
            {
                try
                {
                    for (int j = 0; j < 79; j++)
                    {
                        rate[i, j] = Convert.ToDouble(linie.Split(',')[j]);
                    }
                    i++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            sr.Close();
            return rate;
        }

        public double [] valoareRascumparareExtra()
        {
            double[] valoareRascumparareExtra = new double[101];
            double[,] rateCsvPUA = citireRataCsvPUA();
            double[] dbPUA = beneficiuDecesExtra();
            for (int i = 0; i <= 100 - Client.Varsta; i++)
            {  
                valoareRascumparareExtra[i] = Math.Round(rateCsvPUA[i, Client.Varsta] / 1000 * dbPUA[i]);
            }
            return valoareRascumparareExtra;
        }

        public double[] valoareRascumparareTotala()
        {
            double[] valoareRascumparareTotala = new double[101];
            double[] csvPUA = valoareRascumparareExtra();
            double[] gcsv = valoareRascumparareGarantata();
            for (int i = 0; i <= 100 - Client.Varsta; i++)
            {
                valoareRascumparareTotala[i] = csvPUA[i] + gcsv[i];
            }
            return valoareRascumparareTotala;
        }

        public override string ToString()
        {
            return client.ToString() + " Suma Asigurata: " + sumaAsigurata + " lei\n Prima: " + prima + " lei";
        }
    }
}
