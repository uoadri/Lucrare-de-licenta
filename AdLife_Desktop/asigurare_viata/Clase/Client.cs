using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asigurare_viata.Clase
{
    class Client
    {
        private string nume;
        private string prenume;
        private int varsta;
        private string sex;
        private string cnp;
        private string adresa;
        private string nrTelefon;
        private string status;
        private double lat;
        private double lng;

        public string Nume { get => nume; set => nume = value; }
        public string Prenume { get => prenume; set => prenume = value; }
        public int Varsta { get => varsta; set => varsta = value; }
        public string Sex { get => sex; set => sex = value; }
        public string Cnp { get => cnp; set => cnp = value; }
        public string Adresa { get => adresa; set => adresa = value; }
        public string NrTelefon { get => nrTelefon; set => nrTelefon = value; }
        public string Status { get => status; set => status = value; }
        public double Lat { get => lat; set => lat = value; }
        public double Lng { get => lng; set => lng = value; }

        public Client()
        {

        }

        public Client(string nume, string prenume, int varsta, string sex, string status)
        {
            this.Nume = nume;
            this.Prenume = prenume;
            this.Varsta = varsta;
            this.Sex = sex;
            this.Status = status;
        }

        public Client(string nume, string prenume, int varsta, string sex, string cnp, string adresa, string nrTelefon)
        {
            this.nume = nume;
            this.prenume = prenume;
            this.varsta = varsta;
            this.sex = sex;
            this.cnp = cnp;
            this.adresa = adresa;
            this.nrTelefon = nrTelefon;
        }

        public override string ToString()
        {
            return " " + nume + " " + prenume + "\n Telefon: " + nrTelefon + "\n";
        }
    }
}
