using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asigurare_viata
{
    public partial class Form1 : Form
    {
        int TogMove;
        int MValX;
        int MvalY;
        public Form1()
        {
            InitializeComponent();
            tb_parola.isPassword = true;
           

        }

        
        private void label1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

       

        private void b_logare_Click(object sender, EventArgs e)
        {
            /*if (DB.Select.login(tb_user.Text, tb_parola.Text))
            {
                
                MessageBox.Show("Utilizator " + tb_user.Text + " conectat cu succes!");
                Form2 frm = new Form2();
                frm.ShowDialog();
                
            }
            else
                MessageBox.Show("Utilizator inexistent sau parola gresita!");
                */
            Form2 frm = new Form2();
            frm.ShowDialog();

        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_Enter(object sender, EventArgs e)
        {
            tb_parola.Text = "";
        }

        private void bunifuMaterialTextbox1_Leave(object sender, EventArgs e)
        {
            if (tb_parola.Text == "")
                tb_parola.Text = "parola";
        }

        private void tb_parola_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MvalY);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            TogMove = 1;
            MValX = e.X;
            MvalY = e.Y;
        }
    }
}
