using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asigurare_viata.DB
{
    class Select
    {
        public static bool login(string utilizator, string parola)
        {
            SqlCommand cmd = new SqlCommand("SELECT id_agent, utilizator, parola FROM agent WHERE utilizator = '" + utilizator + "' AND parola = '" + parola + "'", Connection.sqlcon());
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                Clase.Asigurare.ID_AGENT = Convert.ToInt32(dataReader.GetValue(0).ToString());
                return true;
            }
            return false;
        }

        public static void clienti()
        {
            /* Form2.listaBD.Clear();
             SqlCommand cmd = new SqlCommand("Select asig.suma_asigurata, asig.prima_totala, asig.extra_prima, asig.perioada_plata, asig.data_incepere, cl.nume, cl.prenume, cl.varsta, cl.sex, cl.cnp, cl.adresa, cl.lat, cl.lng, cl.telefon, cl.status" +
                 " FROM client cl, asigurare asig" +
                 " WHERE cl.id_agent = " + Clase.Asigurare.ID_AGENT + " AND asig.id_client = cl.id_client ", Connection.sqlcon());
             SqlDataReader dataReader = cmd.ExecuteReader();
             while (dataReader.Read())
             {
                 double sumaAsigurata = Convert.ToDouble(dataReader.GetValue(0));
                 Clase.Asigurare.extraPrima = Convert.ToDouble(dataReader.GetValue(2));
                 double prima = Convert.ToDouble(dataReader.GetValue(1)) - Clase.Asigurare.extraPrima;
                 string perioadaPlata = dataReader.GetValue(3).ToString();
                 DateTime data = DateTime.ParseExact(dataReader.GetValue(4).ToString(), "d.M.yyyy",
                                        System.Globalization.CultureInfo.InvariantCulture);

                 string nume = dataReader.GetValue(5).ToString();
                 string prenume = dataReader.GetValue(6).ToString();
                 int varsta = Convert.ToInt32(dataReader.GetValue(7).ToString());
                 string sex = dataReader.GetValue(8).ToString();
                 string cnp = dataReader.GetValue(9).ToString();
                 string adresa = dataReader.GetValue(10).ToString();
                 double lat = Convert.ToDouble(dataReader.GetValue(11));
                 double lng = Convert.ToDouble(dataReader.GetValue(12));
                 string nrTelefon = dataReader.GetValue(13).ToString();
                 string status = dataReader.GetValue(14).ToString();
                 if (status == "FM")
                 {
                     Clase.StareSanatate.fumator = true;
                 }
                 Clase.Asigurare asigurare = new Clase.Asigurare(sumaAsigurata, prima, perioadaPlata, data, nume, prenume, varsta, sex, cnp, adresa, lat, lng, nrTelefon, status);
                 Form2.listaBD.Add(asigurare);            
             }       */
            Clase.Asigurare asigurare = new Clase.Asigurare(100000, 4029, "10 Ani", new DateTime(2015,10,15), "Popescu", "Ionut", 35, "Barbat", "1831111309587", "Bucuresti, Romania", 44.429865, 26.102081, "0750005328", "NFM");
            Clase.Asigurare asigurare2 = new Clase.Asigurare(150000, 2550.5, "20 Ani", new DateTime(2012, 8, 25), "Banu", "Andrei", 20, "Barbat", "1831111309587", "Bucuresti, Romania", 44.435283, 26.084048, "0750005328", "NFM");
            Clase.Asigurare asigurare3 = new Clase.Asigurare(200000, 2985, "Toata Viata", new DateTime(2014, 5, 10), "Pop", "Bogdan", 25, "Barbat", "1831111309587", "Bucuresti, Romania", 44.442134, 26.101398, "0750005328", "FM");
            Form2.listaBD.Add(asigurare);
            Form2.listaBD.Add(asigurare2);
            Form2.listaBD.Add(asigurare3);
        }

        public static int id_asig()
        {
            /* SqlCommand cmd = new SqlCommand("SELECT COUNT(id_asigurare) FROM asigurare", Connection.sqlcon());
             SqlDataReader dataReader = cmd.ExecuteReader();
             int k = 0;
             while (dataReader.Read())
             {
                 k = Convert.ToInt32(dataReader.GetValue(0));
             }
             return k;
             */
            return 0;
        }

        public static void Nr_Asig()
        {
          /*  DateTime now = DateTime.Today;
            SqlCommand cmd = new SqlCommand("SELECT COUNT(asig.id_asigurare) FROM asigurare asig, client cl WHERE cl.id_agent = " + Clase.Asigurare.ID_AGENT + " AND asig.id_client = cl.id_client AND asig.data_incepere = '"+ now.Day + "." + now.Month + "." + now.Year + "'" , Connection.sqlcon());
            SqlDataReader dataReader = cmd.ExecuteReader();
          
            while (dataReader.Read())
            {
                Clase.Asigurare.Nr_Asig = Convert.ToInt32(dataReader.GetValue(0));
            }*/
        }

        public static int[,] Activitate()
        {
            DateTime now = DateTime.Today;
           
            int[,] saptamana = new int[5,8];
            int sfarsitulSapt = 0;
            switch (now.ToString("ddd", new CultureInfo("ro-RO")))
            {
                case "lun.":
                    {
                        sfarsitulSapt = 6;
                        break;
                    }
                case "mar.":
                    {
                        sfarsitulSapt = 5;
                        break;
                    }
                case "mie.":
                    {
                        sfarsitulSapt = 4;
                        break;
                    }
                case "joi":
                    {
                        sfarsitulSapt = 3;
                        break;
                    }
                case "vin.":
                    {
                        sfarsitulSapt = 2;
                        break;
                    }
                case "sâm.":
                    {
                        sfarsitulSapt = 1;
                        break;
                    }
                case "dum.":
                    {
                        sfarsitulSapt = 0;
                        break;
                    }
            }
            now = now.AddDays(sfarsitulSapt);
            int k = 5;
            int z = 7;
            Random rnd = new Random();
            for (int i = - 1; i > -28; i--)
            {
               
                switch (now.ToString("ddd", new CultureInfo("ro-RO")))
                {
                    case "lun.":
                        {
                            saptamana[k, z] = rnd.Next(1, 13);
                            z = 7;
                            break;
                        }
                    case "mar.":
                        {
                            saptamana[k, z] = rnd.Next(1, 13);
                            break;
                        }
                    case "mie.":
                        {
                            saptamana[k, z] = rnd.Next(1, 13);
                            break;
                        }
                    case "joi":
                        {
                            saptamana[k, z] = rnd.Next(1, 13);
                            break;
                        }
                    case "vin.":
                        {
                            saptamana[k, z] = rnd.Next(1, 13);
                            break;
                        }
                    case "sâm.":
                        {
                            saptamana[k, z] = rnd.Next(1, 13);
                            break;
                        }
                    case "dum.":
                        {
                            k--;
                            saptamana[k, z] = rnd.Next(1, 13);
                           
                            
                            break;
                        }

                }
                now = now.AddDays(-1);
                z--;
            }
          /*  SqlCommand cmd = null;
           
            for (int i = -1; i > -29; i--)
            {

                switch (now.ToString("ddd", new CultureInfo("ro-RO")))
                {
                    case "lun.":
                        {
                            cmd = new SqlCommand("SELECT COUNT(asig.id_asigurare) FROM asigurare asig, client cl WHERE cl.id_agent = " + Clase.Asigurare.ID_AGENT + " AND asig.id_client = cl.id_client AND asig.data_incepere = '" + now.Day + "." + now.Month + "." + now.Year + "'", Connection.sqlcon());
                            SqlDataReader dataReader = cmd.ExecuteReader();

                            if (dataReader.Read())
                            {
                                saptamana[k, z] = Convert.ToInt32(dataReader.GetValue(0));
                            }
                            z = 8;
                            break;
                        }
                    case "mar.":
                        {
                            cmd = new SqlCommand("SELECT COUNT(asig.id_asigurare) FROM asigurare asig, client cl WHERE cl.id_agent = " + Clase.Asigurare.ID_AGENT + " AND asig.id_client = cl.id_client AND asig.data_incepere = '" + now.Day + "." + now.Month + "." + now.Year + "'", Connection.sqlcon());
                            SqlDataReader dataReader = cmd.ExecuteReader();

                            if (dataReader.Read())
                            {
                                saptamana[k, z] = Convert.ToInt32(dataReader.GetValue(0));
                            }
                            break;
                        }
                    case "mie.":
                        {
                            cmd = new SqlCommand("SELECT COUNT(asig.id_asigurare) FROM asigurare asig, client cl WHERE cl.id_agent = " + Clase.Asigurare.ID_AGENT + " AND asig.id_client = cl.id_client AND asig.data_incepere = '" + now.Day + "." + now.Month + "." + now.Year + "'", Connection.sqlcon());
                            SqlDataReader dataReader = cmd.ExecuteReader();

                            if (dataReader.Read())
                            {
                                saptamana[k, z] = Convert.ToInt32(dataReader.GetValue(0));
                            }
                            break;
                        }
                    case "joi":
                        {
                            cmd = new SqlCommand("SELECT COUNT(asig.id_asigurare) FROM asigurare asig, client cl WHERE cl.id_agent = " + Clase.Asigurare.ID_AGENT + " AND asig.id_client = cl.id_client AND asig.data_incepere = '" + now.Day + "." + now.Month + "." + now.Year + "'", Connection.sqlcon());
                            SqlDataReader dataReader = cmd.ExecuteReader();

                            if (dataReader.Read())
                            {
                                saptamana[k, z] = Convert.ToInt32(dataReader.GetValue(0));
                            }
                            break;
                        }
                    case "vin.":
                        {
                            cmd = new SqlCommand("SELECT COUNT(asig.id_asigurare) FROM asigurare asig, client cl WHERE cl.id_agent = " + Clase.Asigurare.ID_AGENT + " AND asig.id_client = cl.id_client AND asig.data_incepere = '" + now.Day + "." + now.Month + "." + now.Year + "'", Connection.sqlcon());
                            SqlDataReader dataReader = cmd.ExecuteReader();

                            if (dataReader.Read())
                            {
                                saptamana[k, z] = Convert.ToInt32(dataReader.GetValue(0));
                            }
                            break;
                        }
                    case "sâm.":
                        {
                            cmd = new SqlCommand("SELECT COUNT(asig.id_asigurare) FROM asigurare asig, client cl WHERE cl.id_agent = " + Clase.Asigurare.ID_AGENT + " AND asig.id_client = cl.id_client AND asig.data_incepere = '" + now.Day + "." + now.Month + "." + now.Year + "'", Connection.sqlcon());
                            SqlDataReader dataReader = cmd.ExecuteReader();

                            if (dataReader.Read())
                            {
                                saptamana[k, z] = Convert.ToInt32(dataReader.GetValue(0));
                            }
                            break;
                        }
                    case "dum.":
                        {
                            k--;
                            cmd = new SqlCommand("SELECT COUNT(asig.id_asigurare) FROM asigurare asig, client cl WHERE cl.id_agent = " + Clase.Asigurare.ID_AGENT + " AND asig.id_client = cl.id_client AND asig.data_incepere = '" + now.Day + "." + now.Month + "." + now.Year + "'", Connection.sqlcon());
                            SqlDataReader dataReader = cmd.ExecuteReader();

                            if (dataReader.Read())
                            {
                                saptamana[k, z] = Convert.ToInt32(dataReader.GetValue(0));
                            }


                            break;
                        }

                }
                now = now.AddDays(-1);
                z--;
            }*/
            return saptamana;
        }
    }
}
