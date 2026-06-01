using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace actOrlando
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string rutaImagenSeleccionada = "";
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Por favor, llena los campos de Nombre y Precio.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(rutaImagenSeleccionada) || !File.Exists(rutaImagenSeleccionada))
            {
                MessageBox.Show("Es obligatorio seleccionar una imagen para el producto antes de guardar.", "Falta imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            try
            {
                string carpetaLocal = Path.Combine(Application.StartupPath, "DatosProductos");
                string carpetaImagenes = Path.Combine(carpetaLocal, "Imagenes");

                if (!Directory.Exists(carpetaLocal)) Directory.CreateDirectory(carpetaLocal);
                if (!Directory.Exists(carpetaImagenes)) Directory.CreateDirectory(carpetaImagenes);

               
                string nombreArchivoImagen = DateTime.Now.Ticks.ToString() + Path.GetExtension(rutaImagenSeleccionada);
                string rutaDestinoImagen = Path.Combine(carpetaImagenes, nombreArchivoImagen);

                
                File.Copy(rutaImagenSeleccionada, rutaDestinoImagen, true);

                
                string rutaArchivoTexto = Path.Combine(carpetaLocal, "productos.txt");

                // Formateamos la línea del registro plano
                string nuevaLinea = $"{txtNombre.Text};{txtPrecio.Text};{nombreArchivoImagen}";

               
                File.AppendAllLines(rutaArchivoTexto, new string[] { nuevaLinea });


                MessageBox.Show("¡Producto registrado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

               
                txtNombre.Clear();
                txtPrecio.Clear();
                pictureBox1.Image = null;
                rutaImagenSeleccionada = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico al guardar localmente: {ex.Message}", "Error de persistencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imágenes (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    rutaImagenSeleccionada = ofd.FileName;

                   
                    byte[] bytes = File.ReadAllBytes(rutaImagenSeleccionada);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled= false;
            }
        }
    }
    //fin de h01
}
