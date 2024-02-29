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
        List urls2 = new List();
        public Form1()
        {
            InitializeComponent();
            this.Resize += new System.EventHandler(this.Form_Resize);
        }
        private void Form_Resize(object sender, EventArgs e)
        {
            webView21.Size = this.ClientSize - new System.Drawing.Size(webView21.Location);
            buttonIr.Left = this.ClientSize.Width - buttonIr.Width;
            comboBox1.Width = buttonIr.Left - comboBox1.Left;
        }
        private void Grabar(string Filename)
        {
            //utilizar a veces append o open.or.create
            FileStream stream = new FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            foreach (var url in urls2)
            {
                writer.WriteLine(url.Pagina);
                writer.WriteLine(url.Veces);
                writer.WriteLine(url.Fecha);
            }
            writer.Close();
        }
        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonIr_Click(object sender, EventArgs e)
        {
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

            // Guardar la URL en el archivo
            try
            {
                using (FileStream flujo = new FileStream(nombreArchivo, FileMode.Append, FileAccess.Write))
                using (StreamWriter guardar = new StreamWriter(flujo))
                {
                    guardar.WriteLine(url);
                }

                // Cargar lo guardado  del archivo en el ComboBox
                using (StreamReader lector = new StreamReader(nombreArchivo))
                {
                    comboBox1.Items.Clear(); // se limpia el combobox antes de volver a guardar 

                    while (!lector.EndOfStream)
                    {
                        string linea = lector.ReadLine();
                        comboBox1.Items.Add(linea);
                    }
                }

                MessageBox.Show("URL guardada en el historial y cargada en el ComboBox.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar o cargar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            URL urlExiste = urls2.Find(u => u.Pagina == url);
            if (urlExiste == null)
            {
                URL urlNueva = new URL();
                urlNueva.Pagina = url;
                urlNueva.Veces = 1;
                urlNueva.Fecha = DateTime.Now;
                urls2.Add(urlNueva);
                Grabar("historial.txt");
                webView21.CoreWebView2.Navigate(url);
            }
            else
            {
                urlExiste.Veces++;
                urlExiste.Fecha = DateTime.Now;
                Grabar("historial.txt");
                webView21.CoreWebView2.Navigate(urlExiste.Pagina);
            }*/
            string url = comboBox1.Text.ToString();
            if (url.Contains(".") || url.Contains("/") || url.Contains(":"))
            {
                if (url.Contains("https"))
                    webView21.CoreWebView2.Navigate(url);
                else
                {
                    url = "https://" + url;
                    webView21.CoreWebView2.Navigate(url);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(url))
                {
                    url = "https://www.google.com/search?q=" + url;
                    webView21.CoreWebView2.Navigate(url);
                }
            }

            URL urlExiste = urls2.Find(u => u.Pagina == url);
            if (urlExiste == null)
            {
                URL urlNueva = new URL();
                urlNueva.Pagina = url;
                urlNueva.Veces = 1;
                urlNueva.Fecha = DateTime.Now;
                urls2.Add(urlNueva);
                Grabar("historial.txt");
                webView21.CoreWebView2.Navigate(url);
            }
            else
            {
                urlExiste.Veces++;
                urlExiste.Fecha = DateTime.Now;
                Grabar("historial.txt");
                webView21.CoreWebView2.Navigate(urlExiste.Pagina);
            }
        }
        private void leer()
        {
            string fileName = "historial.txt";
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                URL url = new URL();
                url.Pagina = reader.ReadLine();
                url.Veces = Convert.ToInt32(reader.ReadLine());
                url.Fecha = Convert.ToDateTime(reader.ReadLine());
                urls2.Add(url);
            }

            reader.Close();
            comboBox1.DisplayMember = "pagina";
            comboBox1.DataSource = null;
            comboBox1.DataSource = urls2;
            comboBox1.Refresh();
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
