using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asigurare_viata.DB
{
    class Insert
    {
        public static void Salveaza()
        {
            int k = Select.id_asig() + 1;
            foreach (Clase.Asigurare asig in Form2.lAsigurare)
            {
                DateTime now = DateTime.Today;
                string Query = "INSERT INTO Client(id_client, id_agent, nume, prenume, sex, varsta, status, cnp, telefon, adresa, lat, lng, utilizator, parola) values("
                    + k + "," + Clase.Asigurare.ID_AGENT + ",'" + asig.Client.Nume + "','" + asig.Client.Prenume + "','" + asig.Client.Sex + "','" + asig.Client.Varsta + "','" + asig.Client.Status + "','" + asig.Client.Cnp + "','" + asig.Client.NrTelefon + "','" + asig.Client.Adresa + "','" + asig.Client.Lat + "','" + asig.Client.Lng + "','" + asig.Client.Nume.ToLower() + "_" + asig.Client.Prenume.ToLower() + "','" + asig.Client.Nume.ToLower() + "_" + asig.Client.Prenume.ToLower() + "'); ";

                SqlCommand cmd = new SqlCommand(Query, Connection.sqlcon());
                cmd.ExecuteNonQuery();

                string Query2 = "INSERT INTO Asigurare(id_asigurare,id_client,suma_asigurata,prima_totala,extra_prima,perioada_plata,data_incepere) values(" + k + "," + k + ",'" + asig.SumaAsigurata + "','" + Clase.Asigurare.primaTotala + "','" + Clase.Asigurare.extraPrima + "','" + asig.PerioadaPlata + "','" + now.Day + "." + now.Month + "." + now.Year + "');";

                SqlCommand cmd2 = new SqlCommand(Query2, Connection.sqlcon());
                cmd2.ExecuteNonQuery();
            }
            MessageBox.Show("Date salvate cu succes!");
        }
    }
}
