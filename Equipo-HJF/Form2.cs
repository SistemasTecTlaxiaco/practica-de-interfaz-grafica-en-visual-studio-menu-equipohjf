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
            if (num == 0)
            {
                PanelMenu.Visible = true;
                num = 1;
            }
            else
            {
                PanelMenu.Visible = false;
                num = 0;
            }
        }

        private void estandar_Click(object sender, EventArgs e)
        {
            PanelMenu.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cero_Click(object sender, EventArgs e)
        {
            Salida("0");
        }
        private void uno_Click(object sender, EventArgs e)
        {
            Salida("1");
        }
        private void dos_Click(object sender, EventArgs e)
        {
            Salida("2");
        }
        private void tres_Click(object sender, EventArgs e)
        {
            Salida("3");
        }
        private void cuatro_Click(object sender, EventArgs e)
        {
            Salida("4");
        }
        private void cinco_Click(object sender, EventArgs e)
        {
            Salida("5");
        }
        private void seis_Click(object sender, EventArgs e)
        {
            Salida("6");
        }
        private void siete_Click(object sender, EventArgs e)
        {
            Salida("7");
        }
        private void ocho_Click(object sender, EventArgs e)
        {
            Salida("8");
        }
        private void nueve_Click(object sender, EventArgs e)
        {
            Salida("9");
        }

        private void resta_Click(object sender, EventArgs e)
        {
            comprobarUltimo("-");
        }

        private void suma_Click(object sender, EventArgs e)
        {
            comprobarUltimo("+");
        }

        private void multiplicar_Click(object sender, EventArgs e)
        {
            comprobarUltimo("*");
        }
        private void Modulo_Click(object sender, EventArgs e)
        {
            comprobarUltimo("%");
        }
        private void dividir_Click(object sender, EventArgs e)
        {
            comprobarUltimo("/");
        }

        string Salida(string caracteresS)
        {
            SalidaText.Text += caracteresS;//le gregamos la ultima salida a la cadena
            return caracteresS;
        }


        private void igual_Click(object sender, EventArgs e)
        {
            SalidaText.Text = Operacion(SalidaText.Text);
        }

        String Operacion(string resultado)
        {
            while (ComprobarNoSimbolos(resultado))
            {
                string simbolo = "";
                int i = 0;//almacenar la posicion donde se encontro el simbolo
                
                for (i = 0; i <= resultado.Length - 1; i++)//recorrer toda la cadena
                {
                    //posicion, cantidad
                    simbolo = resultado.Substring(i, 1);
                    if (simbolo == "/")
                    {
                        resultado = reducir(resultado, i, "division");
                        break;//evitar eliminar mas de un simbolo "/"
                    }
                    else if (simbolo == "*")
                    {
                        resultado = reducir(resultado, i, "multiplicacion");
                        break;//evitar eliminar mas de un simbolo "*"
                    }

                    if (simbolo == "+")
                    {
                        resultado = reducir(resultado, i, "suma");
                        break;//evitar eliminar mas de un simbolo "+"
                    }

                    if (simbolo == "-")
                    {
                        resultado = reducir(resultado, i, "resta");
                        break;//evitar eliminar mas de un simbolo "-"
                    }

                }
            }
            return resultado;
        }

        bool ComprobarNoSimbolos(string resultado)
        {
            bool existeSimbolo = false;
            string simbolo = "";
            for (int i = 0; i <= resultado.Length - 1; i++)//recorrer toda la cadena
            { 
                //posicion, cantidad
                simbolo = resultado.Substring(i, 1);
                if (simbolo == "/" || simbolo == "*" || simbolo == "-" || simbolo == "+")
                {
                    existeSimbolo = true;
                    break;
                }
            }
            return existeSimbolo;
        }

        string reducir(string resultado, int i, string OperacionArealizar)
        {
            int j = 0;
            string izquierda = "", derecha = "", almacenar = "", sub = "";
            
            resultado = resultado.Remove(i, 1);//se elimina el simbolo
            
            for (j = i - 1; j >= 0; j--)//se recogen datos de derecha a izquiertda del simbolo
            {
                if (resultado.Substring(j, 1) == "+" || resultado.Substring(j, 1) == "-" || resultado.Substring(j, 1) == "*" || resultado.Substring(j, 1) == "/")
                {
                    break;//Salir si hay un simbolo
                }
                almacenar += resultado.Substring(j, 1);
                resultado = resultado.Remove(j, 1);
            }

            for (int k = almacenar.Length - 1; k >= 0; k--)//pasarlo en orden
            {
                izquierda += almacenar.Substring(k, 1);
                almacenar = almacenar.Remove(k, 1);
            }

            //------------------------------------Izquierda a derecha--------------------------------------------------------------
            for (int l = j + 1; l < resultado.Length; l++)//se recogen datos de derecha a izquiertda del simbolo
            {
                if (resultado.Substring(l, 1) == "+" || resultado.Substring(l, 1) == "-" || resultado.Substring(l, 1) == "*" || resultado.Substring(l, 1) == "/")
                {
                    break;//Salir si hay un simbolo
                }
                derecha += resultado.Substring(l, 1);
                resultado = resultado.Remove(l, 1);
                l = l - 1;
            }
            //----------------------------------------------------------------------------------------------
            double subtotal = 0;
            if (OperacionArealizar == "division")
            {
                subtotal = (double.Parse(izquierda) / double.Parse(derecha));
            }else if (OperacionArealizar == "multiplicacion")
            {
                subtotal = (double.Parse(izquierda) * double.Parse(derecha));
            }

            if (OperacionArealizar == "suma")
            {
                subtotal = (double.Parse(izquierda) + double.Parse(derecha));
            }
            if (OperacionArealizar == "resta")
            {
                subtotal = (double.Parse(izquierda) - double.Parse(derecha));
            }

            sub = subtotal.ToString();
            return resultado.Insert(j + 1, sub);
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
                salida = caracteresS.Substring(0, caracteresS.Length - 1);//borrar el ultimo caracter
            }
            return salida;
        }

        private void punto_Click(object sender, EventArgs e)
        {
            if(!SalidaText.Text.Contains("."))
            {
                comprobarUltimo(".");
            }
        }

        

        void comprobarUltimo(string dig)
        {
            string ultimo = SalidaText.Text.Substring(SalidaText.Text.Length - 1);//tomar el ultimo valor
            if (ultimo != "/" && ultimo != "+" && ultimo != "*" && ultimo != "-" && ultimo != ".")
            {
                Salida(dig);//se envia a la salida
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
