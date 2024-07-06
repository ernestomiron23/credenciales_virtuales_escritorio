using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_Console
{
    public partial class FormRecorte : Form
    {

        private Image imagenOriginal;
        public Image ImagenRecortada { get; private set; }
        private Point inicioSeleccion;
        private Rectangle seleccionRectangulo = new Rectangle();

        public FormRecorte(Image imagen)
        {
            InitializeComponent();
            imagenOriginal = imagen;
            pictureBoxRecorte.Image = imagen;
            //pictureBoxRecorte.SizeMode = PictureBoxSizeMode.AutoSize;

            // Conecta los eventos del ratón
            pictureBoxRecorte.MouseDown += PictureBoxRecorte_MouseDown;
            pictureBoxRecorte.MouseMove += PictureBoxRecorte_MouseMove;
            pictureBoxRecorte.MouseUp += PictureBoxRecorte_MouseUp;
            pictureBoxRecorte.Paint += pictureBoxRecorte_Paint;

        }

        private void FormRecorte_Load(object sender, EventArgs e)
        {

        }

        private void PictureBoxRecorte_MouseDown(object sender, MouseEventArgs e)
        {
            // Al hacer clic, registra la posición de inicio de la selección
            inicioSeleccion = e.Location;
        }

        private void PictureBoxRecorte_MouseMove(object sender, MouseEventArgs e)
        {
            // Actualiza el rectángulo de selección mientras el ratón se mueve
            if (e.Button == MouseButtons.Left)
            {
                int ancho = e.X - inicioSeleccion.X;
                int alto = e.Y - inicioSeleccion.Y;

                seleccionRectangulo = new Rectangle(inicioSeleccion.X, inicioSeleccion.Y, ancho, alto);
                pictureBoxRecorte.Invalidate();
            }
        }


        private void PictureBoxRecorte_MouseUp(object sender, MouseEventArgs e)
        {
            // Aquí puedes realizar acciones adicionales si es necesario cuando se suelta el botón del ratón
        }


        private void btnAplicarRecorte_Click(object sender, EventArgs e)
        {

            // Obtener la región de recorte
            RectangleF regionRecorte = ObtenerRegiónRecorte();

            // Aplicar el recorte a la imagen original
            ImagenRecortada = RecortarImagen(imagenOriginal, regionRecorte);

            // Redimensionar la imagen recortada al tamaño del PictureBox en el FormRecorte
            ImagenRecortada = RedimensionarImagen(ImagenRecortada, pictureBoxRecorte.Width, pictureBoxRecorte.Height);

            DialogResult = DialogResult.OK;
            Close();
        }

        private Image RedimensionarImagen(Image imagen, int nuevoAncho, int nuevoAlto)
        {
            return imagen;
        }

        private RectangleF ObtenerRegiónRecorte()
        {
            // Asegúrate de que el usuario haya realizado una selección válida
            if (seleccionRectangulo.IsEmpty || seleccionRectangulo.Width <= 0 || seleccionRectangulo.Height <= 0)
            {
                // No se ha realizado una selección válida, puedes manejar esto de acuerdo a tus necesidades
                MessageBox.Show("Realice una selección válida antes de aplicar el recorte.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return RectangleF.Empty;
            }

            // Ajusta el rectángulo de selección para que tenga el tamaño original del PictureBox en FormRecorte
            int nuevoAncho = pictureBoxRecorte.Width;
            int nuevoAlto = pictureBoxRecorte.Height;

            float factorEscaladoX = (float)imagenOriginal.Width / pictureBoxRecorte.Width;
            float factorEscaladoY = (float)imagenOriginal.Height / pictureBoxRecorte.Height;

            float x = seleccionRectangulo.X * factorEscaladoX;
            float y = seleccionRectangulo.Y * factorEscaladoY;
            float width = seleccionRectangulo.Width * factorEscaladoX;
            float height = seleccionRectangulo.Height * factorEscaladoY;

            // Devuelve un RectangleF que representa la región de recorte
            return new RectangleF(x, y, width, height);
        }


        private Image RecortarImagen(Image imagen, RectangleF regionRecorte)
        {
            Bitmap bmp = new Bitmap((int)regionRecorte.Width, (int)regionRecorte.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(imagen, new Rectangle(0, 0, bmp.Width, bmp.Height), regionRecorte, GraphicsUnit.Pixel);
            }

            return bmp;
        }

        private void pictureBoxRecorte_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawRectangle(pen, seleccionRectangulo);
            }
        }
    }
}
