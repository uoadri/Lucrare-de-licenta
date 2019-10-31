using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asigurare_viata.Clase
{
    class Rate
    {
        private Asigurare asigurare;
       

        public Rate(Asigurare asigurare)
        {
            this.asigurare = asigurare;
        }

        internal Asigurare Asigurare { get => asigurare; set => asigurare = value; }

        public int AnCurent()
        {
            
            DateTime now = DateTime.Today;
            int an = now.Year - asigurare.DataAsigurare.Year;
            if (asigurare.DataAsigurare > now.AddYears(-an))
                an--;
            if (an == 0)
                an = 1;
            return an;
        }

        public string numeFisierdbPUA()
        {
            switch (asigurare.Client.Sex)
            {
                case "Femeie":
                    {
                        if (asigurare.Client.Status == "NFM")
                            return "dbPUAfns.txt";
                        else
                            return "dbPUAfsm.txt";
                        break;
                    }
                case "Barbat":
                    {
                        if (asigurare.Client.Status == "NFM")
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
            dbPUA[0] = Math.Round(ratePUA[0, asigurare.Client.Varsta] * gcsv[0] * 0.055 * 0.01 / 10);
            for (int i = 1; i <= 100 - asigurare.Client.Varsta; i++)
            {
                dbPUA[i] = dbPUA[i - 1] + Math.Round(ratePUA[i, asigurare.Client.Varsta] * gcsv[i] * 0.055 * 0.01 / 10);
            }
            return dbPUA;
        }

        public double[] beneficiuDecesTotal()
        {
            double[] beneficiuDecesTotal = new double[101];
            double[] dbPUA = beneficiuDecesExtra();
            int i = AnCurent() - 1;
            {
                beneficiuDecesTotal[i] = asigurare.SumaAsigurata + dbPUA[i];
            }
            return beneficiuDecesTotal;
        }

        public string numeFisierGCSV()
        {
            switch (asigurare.PerioadaPlata)
            {

                case "10 Ani":
                    {
                        if (asigurare.Client.Sex == "Femeie")
                            return "gcsv10payf.txt";
                        else
                            return "gcsv10paym.txt";
                    }
                case "20 Ani":
                    {
                        if (asigurare.Client.Sex == "Femeie")
                            return "gcsv20payf.txt";
                        else
                            return "gcsv20paym.txt";
                    }
                case "Toata Viata":
                    {
                        if (asigurare.Client.Sex == "Femeie")
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
            
            for (int i = 0; i <= 100 - asigurare.Client.Varsta; i++)
            {
                gcsv[i] = Math.Round(rate[i, asigurare.Client.Varsta] / 1000 * asigurare.SumaAsigurata);
            }
            return gcsv;
        }

        public string numeFisierCsvPUA()
        {
            switch (asigurare.Client.Sex)
            {
                case "Femeie":
                    {
                        if (asigurare.Client.Status == "NFM")
                            return "csvPUAfns.txt";
                        else
                            return "csvPUAfsm.txt";
                        break;
                    }
                case "Barbat":
                    {
                        if (asigurare.Client.Status == "NFM")
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

        public double[] valoareRascumparareExtra()
        {
            double[] valoareRascumparareExtra = new double[101];
            double[,] rateCsvPUA = citireRataCsvPUA();
            double[] dbPUA = beneficiuDecesExtra();
            for (int i = 0; i <= 100 - asigurare.Client.Varsta; i++)
            {
                valoareRascumparareExtra[i] = Math.Round(rateCsvPUA[i, asigurare.Client.Varsta] / 1000 * dbPUA[i]);
            }
            return valoareRascumparareExtra;
        }

        public double[] valoareRascumparareTotala()
        {
            double[] valoareRascumparareTotala = new double[101];
            double[] csvPUA = valoareRascumparareExtra();
            double[] gcsv = valoareRascumparareGarantata();
            int i = AnCurent() - 1;
            
                valoareRascumparareTotala[i] = csvPUA[i] + gcsv[i];
            
            return valoareRascumparareTotala;
        }

        public override string ToString()
        {
            return asigurare.ToString();
        }
    }
}
