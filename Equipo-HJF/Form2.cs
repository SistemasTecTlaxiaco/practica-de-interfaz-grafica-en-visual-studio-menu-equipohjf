using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Equipo_HJF
{
    public partial class Form2 : Form
    {
        public string caracteresSalida;

        public Form2()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        int num = 0;
        private void Men_Click(object sender, EventArgs e)
        {
            if (num == 0) {
                PanelMenu.Visible = true;
                num = 1;
            }
            else
            {
                PanelMenu.Visible = false;
                num = 0;
            }

        }

        private void PanelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void estandar_Click(object sender, EventArgs e)
        {
            PanelMenu.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void seis_Click(object sender, EventArgs e)
        {
            Salida("6");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void nueve_Click(object sender, EventArgs e)
        {
            Salida("9");
        }

        string Salida(string caracteresS)
        {
            SalidaText.Text += caracteresS;
            return caracteresS;
        }

        private void tres_Click(object sender, EventArgs e)
        {
            Salida("3");
        }

        private void ocho_Click(object sender, EventArgs e)
        {
            Salida("8");
        }

        private void cinco_Click(object sender, EventArgs e)
        {
            Salida("5");
        }

        private void dos_Click(object sender, EventArgs e)
        {
            Salida("2");
        }

        private void cero_Click(object sender, EventArgs e)
        {
            Salida("0");
        }

        private void siete_Click(object sender, EventArgs e)
        {
            Salida("7");
        }

        private void cuatro_Click(object sender, EventArgs e)
        {
            Salida("4");
        }

        private void uno_Click(object sender, EventArgs e)
        {
            Salida("1");
        }

        private void igual_Click(object sender, EventArgs e)
        {
            Total();
        }

        float Total()
        {
            SalidaText.Text = "0";
            return 0;
        }

        private void Borrar1_Click(object sender, EventArgs e)
        {
            BorrarTodoCalculadora();
        }

        void BorrarTodoCalculadora()
        {
            SalidaText.Text = "";
            caracteresSalida = "";
        }

        private void Borrar2_Click(object sender, EventArgs e)
        {
            BorrarUno();
        }

        void BorrarUno()
        {
            SalidaText.Text = caracteresSalida = cadena(SalidaText.Text);
        }

        string cadena(string caracteresS)
        {
            string salida = "";
            if (caracteresS.Length > 0)
            {
                salida = caracteresS.Substring(0, caracteresS.Length - 1);
            }
            return salida;
        }

        private void punto_Click(object sender, EventArgs e)
        {
            if(!SalidaText.Text.Contains("."))
            { 
                Salida("."); 
            }
            
        }
    }
}
