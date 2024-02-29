using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using static System.Net.WebRequestMethods;
using System.IO;
namespace NavegadorWeb1
{
    public partial class Form1 : Form
    {
        List<URL> urls= new List<URL>();
        // private readonly Dictionary<string, int> contadores = new Dictionary<string, int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonIr_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\Users\quiss\source\repos\NavegadorWeb1\Historial.txt";
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            String urlingresada = comboBox1.Text;
            URL urlexiste = urls.Find(u => u.Pagina == urlingresada);
           
            if (urlexiste == null)
            {
                URL urlnueva = new URL();
                urlnueva.Pagina = urlingresada;
                urlnueva.Veces = 1;
                urlnueva.Fecha = DateTime.Now;
                urls.Add(urlnueva);
                grabar("Historial.txt");

            }
            /*string nombreArchivo = @"C:\Users\quiss\source\repos\NavegadorWeb1\Historial.txt";
            string url = comboBox1.Text.ToString();
            string direccion = comboBox1.Text.ToString();

            if (comboBox1.SelectedItem == null)
            {
                if (!(url.Contains("http")))
                {
                    url = "http://" + url;
                }

                webView21.CoreWebView2.Navigate(url);

                if (!(direccion.Contains('.')))
                {
                    direccion = $"http://www.google.com/search?q={Uri.EscapeDataString(direccion)}";
                }

                webView21.CoreWebView2.Navigate(direccion);
            }
            else
            {
                webView21.CoreWebView2.Navigate(comboBox1.SelectedItem.ToString());
            }

            try
            {
                string urlIngresada = comboBox1.Text.ToString();
                // Verificar si la URL ya existe en el historial
                if (!comboBox1.Items.Contains(url))
                {
                    // Si la URL no está en el historial, guardarla
                    using (FileStream flujo = new FileStream(nombreArchivo, FileMode.Append, FileAccess.Write))
                    using (StreamWriter guardar = new StreamWriter(flujo))
                    {

                        guardar.WriteLine(url);
                        
                    }
               
                    MessageBox.Show("URL guardada en el historial y cargada en el ComboBox.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("La URL ya está en el historial.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar o cargar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            

        }
        private void grabar(string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
            StreamWriter writer = new StreamWriter(stream);

            foreach ( var url in urls) 
            {
                writer.WriteLine(url.Pagina);
                writer.WriteLine(url.Veces);
                writer.WriteLine(url.Fecha);
            }
            writer.Close();
        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string home = "https://www.google.com/webhp?hl=es-419&sa=X&ved=0ahUKEwjeiOnihJyEAxX0TDABHcSeCzgQPAgJ";
            webView21.CoreWebView2.Navigate(home);
        }

        private void hacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webView21.GoForward();
        }

        private void haciaDelanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webView21.GoBack();
        }
    }
}
