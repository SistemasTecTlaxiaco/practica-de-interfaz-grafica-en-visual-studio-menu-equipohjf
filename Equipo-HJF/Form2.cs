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
        public Form2()
        {
            InitializeComponent();
        }

        private void agregarNumero(string numero)
        {
            if (SalidaText.Text == "0")
                SalidaText.Text = "";

            SalidaText.Text += numero;
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
            panel6.Visible = false;
            MenuVertical.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cero_Click(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            agregarNumero(boton.Text);
        }
        private void uno_Click(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            agregarNumero(boton.Text);
        }
        private void dos_Click(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            agregarNumero(boton.Text);
        }
        private void tres_Click(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            agregarNumero(boton.Text);
        }
        private void cuatro_Click(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            agregarNumero(boton.Text);
        }
        private void cinco_Click(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            agregarNumero(boton.Text);
        }
        private void seis_Click(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            agregarNumero(boton.Text);
        }
        private void siete_Click(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            agregarNumero(boton.Text);
        }
        private void ocho_Click(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            agregarNumero(boton.Text);
        }
        private void nueve_Click(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            agregarNumero(boton.Text);
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
            if (ValidarUltimo())//comprobar que no se repita
            {
                SalidaText.Text = Operacion(SalidaText.Text);
            }
        }

        String Operacion(string resultado)
        {
           //while (ComprobarNoSimbolos(resultado))
           //{
                for (int i = 0; i <= resultado.Length - 1; i++)//recorrer toda la cadena
                {
                    if (resultado.Substring(i, 1) == "/")
                    {
                        resultado = reducir(resultado, i, "division", false);
                    }
                }

                for (int i = 0; i <= resultado.Length - 1; i++)//recorrer toda la cadena
                {
                    if (resultado.Substring(i, 1) == "*")
                    {
                        resultado = reducir(resultado, i, "multiplicacion", false);
                    }
                }

                for (int i = 0; i <= resultado.Length - 1; i++)//recorrer toda la cadena
                {
                if (resultado.Substring(i, 1) == "+")
                    {
                        resultado = reducir(resultado, i, "suma", false);
                    }
                    else if (resultado.Substring(i, 1) == "-" && ExisteOtroNegativo(resultado) == true)
                    {
                        Console.WriteLine("i:  " + i);
                        if (resultado.Substring(0, 1) == "-")//si es el primero -8
                        {
                            resultado = reducir(resultado, i, "resta", true);
                        }
                        else {
                            resultado = reducir(resultado, i, "resta", false);
                        }
                    }
                
                }
             //}
             return resultado;
        }

        bool ExisteOtroNegativo(string resultado)
        {
            bool ex = false;
            int num = 0;
            for (int i = 0; i < resultado.Length; i++)//se recogen datos de derecha a izquiertda del simbolo
            {
                string simbolo = resultado.Substring(i, 1);
                if (resultado.Substring(i, 1) == "-" )
                {
                    num = num + 1;
                }
            }
            if(num > 1)
            {
                 ex = true;
            }
            return ex;
        }

        bool ComprobarNoSimbolos(string resultado)
        {
            bool existeSimbolo = false;
            for (int i = 0; i <= resultado.Length - 1; i++)//recorrer toda la cadena
            { 
                //posicion, cantidad
                string simbolo = resultado.Substring(i, 1);
                //(resultado.Substring(l, 1) == "-" && izquierda.Length >0 && (izquierda.Substring(0, 1) == "-")
                if (simbolo == "/" || simbolo == "*" || (simbolo == "-" && ExisteOtroNegativo(resultado)) || simbolo == "+")
                {
                    existeSimbolo = true;
                    break;
                }
            }
            return existeSimbolo;
        }

        string reducir(string resultado, int i, string OperacionArealizar, bool IsqNeg)
        {
            int j = 0;
            string izquierda = "", derecha = "", almacenar = "";

            if (IsqNeg == true){
                for (int l = 0; l < resultado.Length-1; l++)//se recogen datos de derecha a izquiertda del simbolo
                {
                    if (resultado.Substring(l, 1) == "+" || (resultado.Substring(l, 1) == "-" && izquierda.Length >0 && (izquierda.Substring(0, 1) == "-")) || resultado.Substring(l, 1) == "*" || resultado.Substring(l, 1) == "/")
                    {
                        break;//Salir si hay un simbolo
                    }
                    izquierda += resultado.Substring(l, 1);
                    resultado = resultado.Remove(l, 1);
                    l = l - 1;
                }
                for (int l = 0; l < resultado.Length; l++)//se recogen datos de derecha a izquiertda del simbolo
                {
                    if (resultado.Substring(l, 1) == "+" || (resultado.Substring(l, 1) == "-" && derecha.Length > 0 && (derecha.Substring(0, 1) == "-")) || resultado.Substring(l, 1) == "*" || resultado.Substring(l, 1) == "/")
                    {
                        break;//Salir si hay un simbolo
                    }
                    derecha += resultado.Substring(l, 1);
                    resultado = resultado.Remove(l, 1);
                    l = l - 1;
                }
            } else{
                resultado = resultado.Remove(i, 1);//se elimina el simbolo del operador

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
            }
            double subtotal = 0;
            if (OperacionArealizar == "division")
            {
                subtotal = (double.Parse(izquierda) / double.Parse(derecha));
            }else if (OperacionArealizar == "multiplicacion")
            {
                subtotal = (double.Parse(izquierda) * double.Parse(derecha));
            }
            else
            {
                if (OperacionArealizar == "suma")
                {
                    subtotal = (double.Parse(izquierda) + double.Parse(derecha));
                }
                if (OperacionArealizar == "resta" && IsqNeg == false)
                {
                    subtotal = (double.Parse(izquierda) - double.Parse(derecha));
                }
                else
                {
                    Console.WriteLine("izquierda ||: " + double.Parse(izquierda));
                    Console.WriteLine("derecha ||: " + double.Parse(derecha));
                    subtotal = (double.Parse(izquierda) + double.Parse(derecha));
                    Console.WriteLine("subtotal ||: " + subtotal.ToString());
                    return resultado.Insert(0, subtotal.ToString());
                }
            }

            return resultado.Insert(j + 1, subtotal.ToString());
        }

        private void Borrar1_Click(object sender, EventArgs e)
        {
            BorrarTodoCalculadora();
        }

        void BorrarTodoCalculadora()
        {
            SalidaText.Text = "";
        }

        private void Borrar2_Click(object sender, EventArgs e)
        {
            BorrarUno();
        }

        void BorrarUno()
        {
            SalidaText.Text = cadena(SalidaText.Text);
        }

        string cadena(string caracteresS)
        {
            string salida = "";
            if (caracteresS.Length > 0)
            {
                salida = caracteresS.Remove(caracteresS.Length - 1, 1);//borrar el ultimo caracter
            }
            return salida;
        }

        private void punto_Click(object sender, EventArgs e)
        {
            if(!SalidaText.Text.Contains("."))//mientras no contenga el punto
            {
                comprobarUltimo(".");
            }
        }

        void comprobarUltimo(string dig)
        {
            if (SalidaText.Text.Length != 0)
            {
                if (ValidarUltimo())//comprobar que no se repita
                {
                    Salida(dig);//se envia a la salida
                }
            }
        }
        bool ValidarUltimo()
        {
            bool correcto = false;
            string ultimo = SalidaText.Text.Substring(SalidaText.Text.Length - 1);//tomar el ultimo valor
            if (ultimo != "/" && ultimo != "+" && ultimo != "*" && ultimo != "-" && ultimo != ".")//comprobar que no se repita
            {
                correcto = true;
            }else
            {
                Console.WriteLine("Error en la escritura");
            }
            return correcto;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PanelMenu.Visible = false;
            panel6.Visible = true;
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
