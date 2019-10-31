using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using asigurare_viata.Clase;
using asigurare_viata.enums;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace asigurare_viata
{

    public partial class Form2 : Form
    {
        public static int[,] saptamana = null;
        int TogMove;
        int MValX;
        int MvalY;
        List<PointLatLng> points = new List<PointLatLng>();
        public static ArrayList lAsigurare = new ArrayList();
        public static ArrayList listaBD = new ArrayList();
        bool harti = false;
        bool activitate = false;
        bool clienti = false;
        public Form2()
        {
            InitializeComponent();
            bunifuCards2.Hide();
            bunifuCards1.Show();
            InitializareAsigurare();
            DB.Select.clienti();
            DB.Select.Nr_Asig();
            lb_contracte.Text = Convert.ToString(Asigurare.Nr_Asig);
            Task task1 = Task.Run((Action)Activitate);
            Task task2 = Task.Run((Action)IncarcaClienti);
        }
        
        public static void Activitate()
        {
           saptamana = DB.Select.Activitate();
        }
        private void InitializareAsigurare()
        {
            tbClientNume.Text = "Client_Nume";
            tbClientPrenume.Text = "Client_Prenume";
            tbClientVarsta.Text = "35";
            cbClientSex.Text = cbClientSex.Items[0].ToString();
            tbClientCNP.Text = "1831111309587";
            tbClientAdresa.Text = "Bucuresti, Romania";
            tbClientNrTelefon.Text = "0750005328";


            cbPerioadaPlata.Text = cbPerioadaPlata.Items[0].ToString();
            tbSumaAsigurata.Text = "100000";
            cbRezolvaPentru.Text = cbRezolvaPentru.Items[0].ToString();
            object sender = new object();
            EventArgs e = new EventArgs();
            b_calculeaza_Click(sender, e);
        }

        private void graficActivitate()
        {
            if(saptamana == null)
                saptamana = DB.Select.Activitate();
            string tipGrafic = cbTipGrafic.selectedValue;
            Bunifu.DataViz.DataPoint point1 = null;
            Bunifu.DataViz.DataPoint point2 = null;
            Bunifu.DataViz.DataPoint point3 = null;
            Bunifu.DataViz.DataPoint point4 = null;
            if (tipGrafic == "Linie")
            {
                point1 = new Bunifu.DataViz.DataPoint(Bunifu.DataViz.BunifuDataViz._type.Bunifu_spline);
                point2 = new Bunifu.DataViz.DataPoint(Bunifu.DataViz.BunifuDataViz._type.Bunifu_spline);
                point3 = new Bunifu.DataViz.DataPoint(Bunifu.DataViz.BunifuDataViz._type.Bunifu_spline);
                point4 = new Bunifu.DataViz.DataPoint(Bunifu.DataViz.BunifuDataViz._type.Bunifu_spline);
            }
            else
            {
                point1 = new Bunifu.DataViz.DataPoint(Bunifu.DataViz.BunifuDataViz._type.Bunifu_column);
                point2 = new Bunifu.DataViz.DataPoint(Bunifu.DataViz.BunifuDataViz._type.Bunifu_column);
                point3 = new Bunifu.DataViz.DataPoint(Bunifu.DataViz.BunifuDataViz._type.Bunifu_column);
                point4 = new Bunifu.DataViz.DataPoint(Bunifu.DataViz.BunifuDataViz._type.Bunifu_column);
            }
            Bunifu.DataViz.Canvas data = new Bunifu.DataViz.Canvas();

            bunifuDataViz1.colorSet.Add(Color.FromArgb(149, 48, 93));
            bunifuDataViz1.colorSet.Add(Color.FromArgb(225, 155, 45));
            bunifuDataViz1.colorSet.Add(Color.FromArgb(75, 146, 108));
            bunifuDataViz1.colorSet.Add(Color.FromArgb(192, 192, 255));

            
            point1.addLabely("Luni", Convert.ToString(saptamana[1,1]));
            point1.addLabely("Marti", Convert.ToString(saptamana[1, 2]));
            point1.addLabely("Miercuri", Convert.ToString(saptamana[1, 3]));
            point1.addLabely("Joi", Convert.ToString(saptamana[1, 4]));
            point1.addLabely("Vineri", Convert.ToString(saptamana[1, 5]));
            point1.addLabely("Sambata", Convert.ToString(saptamana[1, 6]));
            
            data.addData(point1);

            point2.addLabely("Luni", Convert.ToString(saptamana[2, 1]));
            point2.addLabely("Marti", Convert.ToString(saptamana[2, 2]));
            point2.addLabely("Miercuri", Convert.ToString(saptamana[2, 3]));
            point2.addLabely("Joi", Convert.ToString(saptamana[2, 4]));
            point2.addLabely("Vineri", Convert.ToString(saptamana[2, 5]));
            point2.addLabely("Sambata", Convert.ToString(saptamana[2, 6]));
            
            data.addData(point2);

            point3.addLabely("Luni", Convert.ToString(saptamana[3, 1]));
            point3.addLabely("Marti", Convert.ToString(saptamana[3, 2]));
            point3.addLabely("Miercuri", Convert.ToString(saptamana[3, 3]));
            point3.addLabely("Joi", Convert.ToString(saptamana[3, 4]));
            point3.addLabely("Vineri", Convert.ToString(saptamana[3, 5]));
            point3.addLabely("Sambata", Convert.ToString(saptamana[3, 6]));
            
            data.addData(point3);

            point4.addLabely("Luni", Convert.ToString(saptamana[4, 1]));
            point4.addLabely("Marti", Convert.ToString(saptamana[4, 2]));
            point4.addLabely("Miercuri", Convert.ToString(saptamana[4, 3]));
            point4.addLabely("Joi", Convert.ToString(saptamana[4, 4]));
            point4.addLabely("Vineri", Convert.ToString(saptamana[4, 5]));
            point4.addLabely("Sambata", Convert.ToString(saptamana[4, 6]));
            
            data.addData(point4);

            bunifuDataViz1.Render(data);
        }
       
        private void RezumatIlustrare()
        {

            foreach (Asigurare asigurare in lAsigurare)
            {
                rtbIllustrationSummary.Clear();
                rtbIllustrationSummary.SelectionFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                rtbIllustrationSummary.SelectedText = "Rezumat Ilustrare: \n \nPrima Totala \n";
                rtbIllustrationSummary.SelectionFont = new System.Drawing.Font("Arial", 10, FontStyle.Regular);
                rtbIllustrationSummary.SelectedText = " Anual: ";
                rtbIllustrationSummary.SelectionColor = Color.Blue;
                rtbIllustrationSummary.SelectedText = Convert.ToString(Asigurare.primaTotala) + " Lei" + "\n \n";
                rtbIllustrationSummary.SelectionFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                rtbIllustrationSummary.SelectionColor = Color.Black;
                rtbIllustrationSummary.SelectedText = "AdLife \n";
                rtbIllustrationSummary.SelectionFont = new System.Drawing.Font("Arial", 10, FontStyle.Regular);
                rtbIllustrationSummary.SelectedText = "   Asigurare Viata Intreaga \n";
                rtbIllustrationSummary.SelectionColor = Color.Blue;
                rtbIllustrationSummary.SelectedText = "   " + Convert.ToString(asigurare.SumaAsigurata) + " Lei" + "\n \n";
                rtbIllustrationSummary.SelectionFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                rtbIllustrationSummary.SelectionColor = Color.Black;
                rtbIllustrationSummary.SelectedText = "Client: \n ";
                rtbIllustrationSummary.SelectionFont = new System.Drawing.Font("Arial", 10, FontStyle.Regular);
                rtbIllustrationSummary.SelectionColor = Color.Green;
                rtbIllustrationSummary.SelectedText = "   " + asigurare.Client.Nume + " "+ asigurare.Client.Prenume + "\n";
                rtbIllustrationSummary.SelectionColor = Color.Black;
                rtbIllustrationSummary.SelectedText = "        " + asigurare.Client.Sex + ", " + Convert.ToString(asigurare.Client.Varsta) + ", " + asigurare.Client.Status;
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
          
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            DB.Insert.Salveaza();
            DB.Select.clienti();
            Asigurare.Nr_Asig++;
            lb_contracte.Text = Convert.ToString(Asigurare.Nr_Asig);
            clienti = false;
            harti = false;
            activitate = false;
        }

        

        private void bunifuMaterialTextbox1_MouseClick(object sender, MouseEventArgs e)
        {
            tbSumaAsigurata.Text = "";
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            bunifuCards6.Hide();
            bunifuCards5.Hide();
            bunifuCards4.Hide();
            bunifuCards3.Hide();
            bunifuCards2.Hide();
            bunifuCards1.Show();
           


        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bunifuCards3.Hide();
            bunifuCards2.Show();
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            bunifuCards4.Hide();
            bunifuCards3.Show();
            foreach (Asigurare asigurare in lAsigurare)
            {
                int[] varsta = asigurare.dataVizualizareVarsta();
                double[] primaAnualaTotala = asigurare.primaAnualaTotala();
                double[] beneficiuMoarte = asigurare.beneficiuMoarte();
                double[] beneficiuDecesTotal = asigurare.beneficiuDecesTotal();
                double[] beneficiuDecesExtra = asigurare.beneficiuDecesExtra();
                double[] valoareRascumparareaGarantata = asigurare.valoareRascumparareGarantata();
                double[] valoareRascumparareaExtra = asigurare.valoareRascumparareExtra();
                double[] valoareRascumparareTotala = asigurare.valoareRascumparareTotala();
                int i = 0;
                bool stop = false;
                bunifuCustomDataGrid1.Rows.Clear();
                while (i <= 100 && stop == false)
                {
                    if (varsta[i] != 0)
                    {
                        string[] row0 = { Convert.ToString(i + 1) + " | " + Convert.ToString(varsta[i]), Convert.ToString(primaAnualaTotala[i]) + " lei", Convert.ToString(beneficiuMoarte[i]) + " lei", Convert.ToString(beneficiuDecesExtra[i]) + " lei", Convert.ToString(beneficiuDecesTotal[i]) + " lei", Convert.ToString(valoareRascumparareaGarantata[i]) + " lei", Convert.ToString(valoareRascumparareaExtra[i]) + " lei", Convert.ToString(valoareRascumparareTotala[i]) + " lei" };
                        bunifuCustomDataGrid1.Rows.Add(row0);
                        i++;
                    }
                    else
                        stop = true;
                }
            }
        }

        private void bunifuGradientPanel2_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MValX = e.X;
            MvalY = e.Y;
        }

        private void bunifuGradientPanel2_MouseMove(object sender, MouseEventArgs e)
        {
            if(TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX - 171, MousePosition.Y - MvalY);
            }
        }

        private void bunifuGradientPanel2_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void b_evalueaza_Click(object sender, EventArgs e)
        {
            double extraPrima = 0;
            StareSanatate.fumator = rbFumator.Checked;
            StareSanatate.consumatorAlcool = rbNuConsuma_Alcool.Checked;
            StareSanatate.consumatorDroguri = rbNuConsuma_Droguri.Checked;
            

            if (rbOcupatie_NivelMediu.Checked)
            {
                extraPrima += Convert.ToDouble((int)RiscOcupatie.NivelMediu) / 100.0;
            }
            else
            if (rbOcupatie_NivelRidicat.Checked)
            {
                extraPrima += Convert.ToDouble((int)RiscOcupatie.NivelRidicat) / 100.0;
            }
            

            if ((rbBoliEreditare_Prezent.Checked && rbConditieMeidcala_Buna.Checked == false)
                || rbConsuma_Droguri.Checked || rbConsuma_Alcool.Checked)
            {
                extraPrima = 2;
            }
            else
            if ((rbBoliEreditare_Prezent.Checked && rbConditieMeidcala_Buna.Checked))
            {
                extraPrima += 0.5;
            }

            foreach (RadioButton rb in gbConditieMedicala.Controls.OfType<RadioButton>())
            {
                if (rb.Checked)
                {
                    if (rb.Text == Convert.ToString(ConditieMedicala.Buna))
                    {
                        break;
                    }
                    else
                    if (rb.Text == Convert.ToString(ConditieMedicala.Acceptabila))
                    {
                        extraPrima += Convert.ToDouble((int)ConditieMedicala.Acceptabila) / 100.0;
                    }
                    else
                    if (rb.Text == Convert.ToString(ConditieMedicala.Grava))
                    {
                        if (rbFumator.Checked)
                        {
                            extraPrima = 2;
                        }
                        extraPrima += Convert.ToDouble((int)ConditieMedicala.Grava) / 100.0;
                    }
                    else
                    if (rb.Text == Convert.ToString(ConditieMedicala.Critica))
                    {
                        extraPrima = 2;
                    }
                }
            }
            Asigurare.extraPrima = extraPrima;
            if (extraPrima == 0.25 || extraPrima == 0.5)
            {

                lb_risc.Text = "mediu";
                MessageBox.Show(Convert.ToString("Extra Prima: " + Asigurare.extraPrima));
            }
            else
            if (extraPrima == 0.75 || extraPrima == 1)
            {
                lb_risc.Text = "ridicat";
                MessageBox.Show(Convert.ToString("Extra Prima: " + Asigurare.extraPrima));
            }
            else
            if(extraPrima > 1)
            {
                lb_risc.Text = "critic";
                MessageBox.Show("Clientul prezinta un risc mare de deces, nu poate fi asigurat!");
            }
            else
            {
                lb_risc.Text = "scazut";
                MessageBox.Show(Convert.ToString("Extra Prima: " + Asigurare.extraPrima));
            }   
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void b_calculeaza_Click(object sender, EventArgs e)
        {
            lAsigurare.Clear();
            string nume = tbClientNume.Text;
            string prenume = tbClientPrenume.Text;
            int varsta = Convert.ToInt32(tbClientVarsta.Text);
            string sex = cbClientSex.selectedValue;
            string cnp = tbClientCNP.Text;
            string adresa = tbClientAdresa.Text;
            string nrTelefon = tbClientNrTelefon.Text;
            string status;
            StareSanatate.fumator = rbFumator.Checked;
            if (StareSanatate.fumator == true)
            {
                status = "FM";
            }
            else
                status = "NFM";

            double sumaAsigurata = Convert.ToDouble(tbSumaAsigurata.Text);
            double prima = 0;
            string periadaPlata = cbPerioadaPlata.selectedValue;
            Random random = new Random();
            double lat = random.NextDouble() * (44.539139 - 44.336109) + 44.336109;
            double lng = random.NextDouble() * (26.236712 - 25.960738) + 25.960738;
            Asigurare asigurare = new Asigurare(sumaAsigurata, prima, periadaPlata,new DateTime(), nume, prenume, varsta, sex, cnp, adresa, nrTelefon, status);
            asigurare.Client.Lat = lat;
            asigurare.Client.Lng = lng;
            lAsigurare.Add(asigurare);
            if (cbRezolvaPentru.selectedValue == "Prima Totala")
            {
                Asigurare.primaTotala = Convert.ToDouble(tbPrimaTotala.Text);
                prima = Asigurare.primaTotala - (Asigurare.primaTotala * Asigurare.extraPrima * 100 / (Asigurare.extraPrima * 100 + 100));
                tbSumaAsigurata.Text = Convert.ToString(asigurare.calucleazaSumaAsigurare(prima, Constante.MIN_SUMA_ASIG, Constante.MAX_SUMA_ASIG, 0));
            }
            else
            {
                prima = asigurare.calculeazaPrima();
                Asigurare.primaTotala = prima + (prima * Asigurare.extraPrima);
                tbPrimaTotala.Text = Convert.ToString(Asigurare.primaTotala);
            }

            RezumatIlustrare();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            
            bunifuCards5.Hide();
            bunifuCards4.Show();
            if(activitate == false)
                graficActivitate();
            activitate = true;
        }
        private void IncarcaClienti()
        {
            if (clienti == false)
            {
                bunifuCustomDataGrid2.Rows.Clear();
                bunifuCustomDataGrid2.Refresh();
                foreach (Asigurare asig in listaBD)
                {
                    Rate rate = new Rate(asig);
                    int an = rate.AnCurent();
                    string[] row0 = { asig.Client.Nume, asig.Client.Prenume, Convert.ToString(asig.Client.Varsta + an) + " ani", asig.Client.NrTelefon, asig.DataAsigurare.ToShortDateString(), Convert.ToString(an), asig.PerioadaPlata, Convert.ToString(asig.Prima) + " lei", "bun platnic", Convert.ToString(asig.SumaAsigurata) + " lei", Convert.ToString(rate.beneficiuDecesTotal()[an - 1]) + " lei", Convert.ToString(rate.valoareRascumparareGarantata()[an - 1]) + " lei", Convert.ToString(rate.valoareRascumparareTotala()[an - 1]) + " lei" };
                    bunifuCustomDataGrid2.Rows.Add(row0);
                }
            }
            clienti = true;
        }
        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            bunifuCards6.Hide();
            bunifuCards5.Show();
            IncarcaClienti();        
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
             bunifuCards6.Show();
            if (harti == false)
            {
                points.Clear();
                gmap.MapProvider = GMapProviders.GoogleMap;
                gmap.DragButton = MouseButtons.Left;
                gmap.ShowCenter = false;
                //centru bucuresti;
                gmap.Position = new GMap.NET.PointLatLng(44.429865, 26.102081);
                foreach (Asigurare asig in listaBD)
                {
                    points.Add(new PointLatLng(asig.Client.Lat, asig.Client.Lng));
                }
                int i = 0;
                foreach (PointLatLng point in points)
                {
                    GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
                    GMapOverlay markers = new GMapOverlay("markers");
                    marker.ToolTipText = $"{listaBD[i].ToString()}";
                    i++;
                    GMapToolTip mapToolTip = new GMapToolTip(marker)
                    {
                        Fill = new SolidBrush(Color.FromArgb(33, 32, 77)),
                        Foreground = new SolidBrush(Color.White),
                        Stroke = new Pen(new SolidBrush(Color.Red))
                    };
                    marker.ToolTip = mapToolTip;
                    markers.Markers.Add(marker);
                    gmap.Overlays.Add(markers);
                }
                gmap.MinZoom = 5;
                gmap.MaxZoom = 100;
                gmap.Zoom = 13;
            }
            harti = true;
        }

        private void bunifuCards4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void rtbIllustrationSummary_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            graficActivitate();
        }

        private void bunifuGradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
