using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySqlConnector;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Web.UI.WebControls.WebParts;
using System.CodeDom.Compiler;
using AForge.Video.DirectShow;
using AForge.Video;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Net;
using Emgu.CV.CvEnum;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Drawing2D;

namespace App_Console
{
    public partial class administración : Form
    {
        private Image imagenAnterior;

        int black = 0;
        const string Usuario = "tesoemcredenciales@gmail.com";
        const string cPassword = " uegs knlx nerk okud";
        MemoryStream temporal = new MemoryStream();
        private bool Haydispositivos;
        private FilterInfoCollection misdispositivos;
        private VideoCaptureDevice miwebcam;
        public int seximatil= 0;
        private int usuario1;

        MySqlConnection conectado;
        public administración(String administrador, string admin, int piso, int usuario1)
        {
            InitializeComponent();
            panel8.BringToFront();
            this.StartPosition = FormStartPosition.CenterScreen;

            marvetes.Visible = false;
            this.usuario1 = usuario1;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            if (piso == 1)
            {

                nombreA.Text = administrador + " " + admin;
                panel1.Visible = false;
                usuarios.Visible = true;
                alumnos.Visible = false;
                docentes.Visible = false;
                administrativos.Visible = false;
                policias.Visible = false;
                vicitantes.Visible = false;
                panel8.Visible = false;
                busqueda.Visible = false;
                //radioregistro.Checked = true;
                temporal = null;
                avisos.Visible = false;

            }
            if (piso == 2)
            {
                nombreA.Text = administrador + " " + admin;
                panel1.Visible = false;
                usuarios.Visible = true;
                alumnos.Visible = false;
                docentes.Visible = false;
                administrativos.Visible = false;
                policias.Visible = false;
                vicitantes.Visible = false;
                panel8.Visible = false;
                busqueda.Visible = false;
                //radioregistro.Checked = true;
                temporal = null;
                avisos.Visible = false;
                AVISOP.Visible = false;


            }
            carga();
        }


        public void carga()
        {


            misdispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (misdispositivos.Count > 0)
            {

                Haydispositivos = true;
                for (int i = 0; i < misdispositivos.Count; i++)
                {

                    comboBox3.Items.Add(misdispositivos[i].Name.ToString());
                    comboBox3.Text = misdispositivos[0].Name.ToString();
                }
            }
            else
            {

                Haydispositivos = false;

            }



        }
        private void cerrar()
        {

            if (miwebcam != null && miwebcam.IsRunning)
            {

                miwebcam.SignalToStop();
                miwebcam.WaitForStop();
                miwebcam = null;

            }
            

        }


        private void Capturando(object sender, NewFrameEventArgs eventArgs)
        {

            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            pictureBox2.Image = Imagen;

        }

        [DllImport("User32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("User32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);



        public void conectar()
        {

            try
            {
                string conexion = "Server=82.197.95.106;Port=3306;Database=virtualteso;UserID=virtualteso;Password=tesoem23+;";      
                conectado = new MySqlConnection(conexion);
                conectado.Open();
            }
            catch (Exception x)
            {

                MessageBox.Show("error al conectarse a la BD" + x);




            }


        }



        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 1;
                    panel8.Visible = false;
                    panel58.Visible = true;
                    avisos.Visible = true;
                    marvetes.Visible = true;

                }
                catch (Exception morra)
                {
                    MessageBox.Show("" + morra);

                }
            }
            else
            {


                try
                {

                    OpenFileDialog ofdseleccionar = new OpenFileDialog();
                    ofdseleccionar.Filter = "Imagenes|*.jpg;*.png;*.jpeg";
                    ofdseleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofdseleccionar.Title = "Seleccionar la imagen ";
                    if (ofdseleccionar.ShowDialog() == DialogResult.OK)
                    {

                        foto.Image = Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


            this.Hide();


            Form1 ven = new Form1();

            ven.Show();

        }





        private void button1_Click(object sender, EventArgs e)
        {

            conectar();

            String estado = "";
            String mensajeError = "";

            if (mmatricula.Text == "" || nnombre.Text == "" ||
                apellidop.Text == "" || apellidom.Text == "" ||
                ggrupo.Text == "" || txfecha.Text == "" ||
                ppassword.Text == "" || comboBox1.Text == "" ||
                foto == null || CCORREO.Text == null || ACTIVOB.Checked == false && INACTIVOB.Checked == false)
            {
                MessageBox.Show("no puede quedar nada vacio");
                mensajeError = "1";
            }

            int n;
            bool isNumeric = int.TryParse(mmatricula.Text, out n);
            if (mensajeError == "" && isNumeric == false) {
                MessageBox.Show("MATRICULA SOLO ACEPTA NUMEROS");
                mensajeError = "1";
            }

            if (ACTIVOB.Checked == true)
            {

                estado = "ACTIVO";

            }
            if (INACTIVOB.Checked == true)
            {
                estado = "INACTIVO";

            }

            if (mensajeError == "")
            {
                Boolean yes = false;

                try
                {
                    try {

                        StringBuilder mensajebuilder = new StringBuilder();
                        mensajebuilder.AppendLine("BIENVENIDO AL TESOEM ");
                        mensajebuilder.AppendLine("tu matricula es : " + mmatricula.Text);
                        mensajebuilder.AppendLine("Y tu password es : " + ppassword.Text);
                        string sixtin = mensajebuilder.ToString();
                        MailMessage email = new MailMessage();
                        email.From = new MailAddress(Usuario);
                        email.To.Add(CCORREO.Text.Trim());
                        email.Body = sixtin;
                        email.Subject = CCORREO.Text.Trim();
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(Usuario, cPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(email);
                        yes = true;

                    } catch {

                        MessageBox.Show("EL CORREO NO EXISTE");
                        yes = false;

                    }

                    if (yes == true) {
                        MemoryStream ms = new MemoryStream();
                        foto.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();


                        String sujeto = mmatricula.Text;
                        string nsujeto = nnombre.Text;

                        string sql = "INSERT INTO alumno (matricula, nombre, apellidoP,apellidoM , grupo, estado,vigencia,password,carrera,correo,imagen) VALUES (@matricula, @nombre, @apellidoP,@apellidoM , @grupo, @estado,@vigencia,@password,@carrera,@correo,@imagen)";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@matricula", mmatricula.Text);
                            comando.Parameters.AddWithValue("@nombre", nnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", apellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", apellidom.Text);
                            comando.Parameters.AddWithValue("@grupo", ggrupo.Text);
                            comando.Parameters.AddWithValue("@estado", estado);
                            comando.Parameters.AddWithValue("@vigencia", txfecha.Text);
                            comando.Parameters.AddWithValue("@password", ppassword.Text);
                            comando.Parameters.AddWithValue("@carrera", comboBox1.Text);
                            comando.Parameters.AddWithValue("@correo", CCORREO.Text);
                            comando.Parameters.AddWithValue("@imagen", data);

                            comando.ExecuteNonQuery();


                        }

                        alum1();

                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "registro");
                            comando.Parameters.AddWithValue("@tipo", "alumno");

                            comando.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception exx)
                {
                    Console.WriteLine(exx);
                }
            }
        }

        private void alum1()
        {
            MessageBox.Show("REGISTRO CORRECTAMENTE");
            mmatricula.Clear();
            nnombre.Clear();
            apellidom.Clear();
            apellidop.Clear();
            ggrupo.Clear();
            txfecha.Text = "";
            CCORREO.Clear();
            ppassword.Clear();
            ACTIVOB.Checked = false;
            INACTIVOB.Checked = false;
            comboBox1.Text = " ";
            foto.Image = null;
            foto.Visible = true;
        }

        private string ObtenerDireccionIP()
        {
            string localIP = "?";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        public void llenado() {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void foto_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {

        }

        private void mmatricula_TextChanged(object sender, EventArgs e)
        {

        }

        private void apellidom_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);
            ppassword.Text = strCadena;
        }

        private void ggrupo_TextChanged(object sender, EventArgs e)
        {

        }

        private void CCORREO_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox4_Click(sender, e);
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            
            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
            panel58.Visible = false;
            
            usuarios.Visible = false;
            alumnos.Visible = true;
            docentes.Visible = false;
            administrativos.Visible = false;
            policias.Visible = false;
            vicitantes.Visible = false;
            busqueda.Visible = false;
            panel8.Visible = false;
            label1.Visible = true;
            avisos.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox4_Click(sender, e);
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;

            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
            panel58.Visible = false;

            usuarios.Visible = false;
            alumnos.Visible = false;
            docentes.Visible = true;
            administrativos.Visible = false;
            policias.Visible = false;
            vicitantes.Visible = false;
            busqueda.Visible = false;
            panel8.Visible = false;
            label1.Visible = true;
            avisos.Visible = false;
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox4_Click(sender, e);
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = false;
            panel5.Visible = false;

            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
            panel58.Visible = false;

            usuarios.Visible = false;
            alumnos.Visible = false;
            docentes.Visible = false;
            administrativos.Visible = true;
            policias.Visible = false;
            vicitantes.Visible = false;
            busqueda.Visible = false;
            panel8.Visible = false;
            label1.Visible = true;
            avisos.Visible = false;
        }

        private void teestado_Enter(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    seximatil = 2;
                    panel8.Visible = false;
                    panel58.Visible = true;
                    avisos.Visible = true;
                    marvetes.Visible = true;
                }
                catch (Exception morra)
                {
                    MessageBox.Show("" + morra);

                }
            }
            else
            {

                try
                {

                    OpenFileDialog ofdseleccionar = new OpenFileDialog();
                    ofdseleccionar.Filter = "Imagenes|*.jpg;*.png;*.jpeg";
                    ofdseleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofdseleccionar.Title = "Seleccionar la imagen ";
                    if (ofdseleccionar.ShowDialog() == DialogResult.OK)
                    {

                       Dfoto.Image = Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void Dingresar_Click(object sender, EventArgs e)
        {

            conectar();

            String estado = "";
            String mensajeError = "";

            if (Dnempleado.Text == "" || Dnombre.Text == "" ||
                Dapellidop.Text == "" || Dapellidom.Text == "" ||
                Dvigencia.Text == "" || Dcurp.Text == "" ||
                Dcorreo.Text == "" || Dpassword.Text == "" ||
                Dfoto == null || Dactivo.Checked == false && Dinactivo.Checked == false)
            {
                MessageBox.Show("no puede quedar nada vacio");
                mensajeError = "1";
            }

            int n;
            bool isNumeric = int.TryParse(Dnempleado.Text, out n);
            if (mensajeError == "" && isNumeric == false)
            {
                MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                mensajeError = "1";
            }


            if (Dactivo.Checked == true)
            {

                estado = "ACTIVO";

            }
            if (Dinactivo.Checked == true)
            {
                estado = "INACTIVO";

            }


            if (mensajeError == "")
            {
                Boolean yes = false;

                try
                {
                    try
                    {

                        StringBuilder mensajebuilder = new StringBuilder();
                        mensajebuilder.AppendLine("BIENVENIDO AL TESOEM ");
                        mensajebuilder.AppendLine("tu usuario es : " + Dnempleado.Text);
                        mensajebuilder.AppendLine("Y tu password es : " + Dpassword.Text);
                        string sixtin = mensajebuilder.ToString();
                        MailMessage email = new MailMessage();
                        email.From = new MailAddress(Usuario);
                        email.To.Add(Dcorreo.Text.Trim());
                        email.Body = sixtin;
                        email.Subject = Dcorreo.Text.Trim();
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(Usuario, cPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(email);
                        yes = true;

                    }
                    catch
                    {

                        MessageBox.Show("EL CORREO NO EXISTE");
                        yes = false;

                    }


                    if (yes == true)
                    {
                        MemoryStream ms = new MemoryStream();
                        Dfoto.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();

                        String sujeto = Dnempleado.Text;
                        string nsujeto = Dnombre.Text;

                        string sql = "INSERT INTO docente (id_docente, nombre, apellidoP,apellidoM , vigencia, estado ,curp,correo,password,foto) VALUES (@id_docentes, @nombre, @apellidoPA,@apellidoMA , @vigencia, @estado,@curp,@correo,@password,@foto)";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id_docentes", Dnempleado.Text);
                            comando.Parameters.AddWithValue("@nombre", Dnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoPA", Dapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoMA", Dapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", Dvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", estado);
                            comando.Parameters.AddWithValue("@curp", Dcurp.Text);
                            comando.Parameters.AddWithValue("@correo", Dcorreo.Text);
                            comando.Parameters.AddWithValue("@password", Dpassword.Text);
                            comando.Parameters.AddWithValue("@foto", data);
                            comando.ExecuteNonQuery();

                        }
                        doce1();

                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "registro");
                            comando.Parameters.AddWithValue("@tipo", "docente");

                            comando.ExecuteNonQuery();
                        }

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);

                }


            }



        }

        private void doce1()
        {

            MessageBox.Show("REGISTRO CORRECTAMENTE");
            Dnempleado.Clear();
            Dnombre.Clear();
            Dapellidom.Clear();
            Dapellidop.Clear();
            Dvigencia.Text = "";
            Dpassword.Clear();
            Dactivo.Checked = false;
            Dinactivo.Checked = false;
            Dfoto.Image = null;
            Dcurp.Clear();
            Dcorreo.Clear();

        }


        private void Aagregar_Click(object sender, EventArgs e)
        {


            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA??? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 3;
                    panel8.Visible = false;
                    panel58.Visible = true;
                    avisos.Visible = true;
                    marvetes.Visible = true;

                }
                catch (Exception morra)
                {
                    MessageBox.Show("" + morra);

                }
            }
            else
            {


                try
                {

                    OpenFileDialog ofdseleccionar = new OpenFileDialog();
                    ofdseleccionar.Filter = "Imagenes|*.jpg;*.png;*.jpeg";
                    ofdseleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofdseleccionar.Title = "Seleccionar la imagen ";
                    if (ofdseleccionar.ShowDialog() == DialogResult.OK)
                    {

                        Afoto.Image = Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }


        }

        private void button12_Click(object sender, EventArgs e)
        {

            conectar();

            String estado = "";
            String mensajeError = "";

            if (Anempleado.Text == "" || Anombre.Text == "" ||
                AapellidoP.Text == "" || AapellidoM.Text == "" ||
                Avigencia.Text == "" || Acurp.Text == "" ||
                Acorreo.Text == "" || Apassword.Text == "" ||
                Afoto == null || Aactivo.Checked == false && Ainactivo.Checked == false)
            {
                MessageBox.Show("no puede quedar nada vacio");
                mensajeError = "1";
            }

            int n;
            bool isNumeric = int.TryParse(Anempleado.Text, out n);
            if (mensajeError == "" && isNumeric == false)
            {
                MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                mensajeError = "1";
            }

            if (Aactivo.Checked == true)
            {

                estado = "ACTIVO";

            }
            if (Ainactivo.Checked == true)
            {
                estado = "INACTIVO";

            }




            if (mensajeError == "")
            {
                Boolean yes = false;



                try
                {
                    try {

                        StringBuilder mensajebuilder = new StringBuilder();
                        mensajebuilder.AppendLine("BIENVENIDO AL TESOEM ");
                        mensajebuilder.AppendLine("tu usuario es : " + Anempleado.Text);
                        mensajebuilder.AppendLine("Y tu password es : " + Apassword.Text);
                        string sixtin = mensajebuilder.ToString();
                        MailMessage email = new MailMessage();
                        email.From = new MailAddress(Usuario);
                        email.To.Add(Acorreo.Text.Trim());
                        email.Body = sixtin;
                        email.Subject = Acorreo.Text.Trim();
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(Usuario, cPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(email);
                        yes = true;
                    } catch {

                        MessageBox.Show("EL CORREO NO EXISTE");
                        yes = false;

                    }



                    if (yes == true) {
                        MemoryStream ms = new MemoryStream();
                        Afoto.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();


                        String sujeto = Anempleado.Text;
                        string nsujeto = Anombre.Text;

                        string sql = "INSERT INTO administrativos (id_empleado, nombre, apellidoP,apellidoM , vigencia, estado ,curp,correo,password,foto,nivel) VALUES (@id_empleado, @nombre, @apellidoP,@apellidoM , @vigencia, @estado,@curp,@correo,@password,@foto,@nivel)";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id_empleado", Anempleado.Text);
                            comando.Parameters.AddWithValue("@nombre", Anombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", AapellidoP.Text);
                            comando.Parameters.AddWithValue("@apellidoM", AapellidoM.Text);
                            comando.Parameters.AddWithValue("@vigencia", Avigencia.Text);
                            comando.Parameters.AddWithValue("@estado", estado);
                            comando.Parameters.AddWithValue("@curp", Acurp.Text);
                            comando.Parameters.AddWithValue("@correo", Acorreo.Text);
                            comando.Parameters.AddWithValue("@password", Apassword.Text);
                            comando.Parameters.AddWithValue("@foto", data);
                            comando.Parameters.AddWithValue("@nivel", Aniveles.Text);
                            comando.ExecuteNonQuery();

                        }
                        admin12();
                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "registro");
                            comando.Parameters.AddWithValue("@tipo", "administrativo");

                            comando.ExecuteNonQuery();
                        }

                        

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);


                }


            }


        }

        private void admin12()
        {
            MessageBox.Show("REGISTRO CORRECTAMENTE");
            Anempleado.Clear();
            Anombre.Clear();
            AapellidoM.Clear();
            AapellidoP.Clear();
            Avigencia.Text = "";
            Apassword.Clear();
            Aactivo.Checked = false;
            Ainactivo.Checked = false;
            Afoto.Image = null;
            Acurp.Clear();
            Acorreo.Clear();

        }
        private void button13_Click(object sender, EventArgs e)
        {
            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            Dpassword.Text = strCadena;
        }

        private void button14_Click(object sender, EventArgs e)
        {

            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            Apassword.Text = strCadena;

        }

        private void button15_Click(object sender, EventArgs e)
        {

            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            ppasword.Text = strCadena;

        }

        private void button17_Click(object sender, EventArgs e)
        {



            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 4;
                    panel8.Visible = false;
                    panel58.Visible = true;
                    avisos.Visible = true;
                    marvetes.Visible = true;

                }
                catch (Exception morra)
                {
                    MessageBox.Show("" + morra);

                }
            }
            else
            {


                try
                {

                    OpenFileDialog ofdseleccionar = new OpenFileDialog();
                    ofdseleccionar.Filter = "Imagenes|*.jpg;*.png;*.jpeg";
                    ofdseleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofdseleccionar.Title = "Seleccionar la imagen ";
                    if (ofdseleccionar.ShowDialog() == DialogResult.OK)
                    {

                        Pfoto.Image = Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox4_Click(sender, e);
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = false;

            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
            panel58.Visible = false;

            usuarios.Visible = false;
            alumnos.Visible = false;
            docentes.Visible = false;
            administrativos.Visible = false;
            policias.Visible = true;
            vicitantes.Visible = false;
            busqueda.Visible = false;
            panel8.Visible = false;
            label1.Visible = true;
            avisos.Visible = false;

        }

        private void button16_Click(object sender, EventArgs e)
        {

            conectar();

            String estado = "";
            String mensajeError = "";

            if (Pnempleado.Text == "" || Pnombre.Text == "" ||
                Papellidop.Text == "" || Papellidom.Text == "" ||
                Pvigencia.Text == "" || Pcurp.Text == "" ||
                Pcorreo.Text == "" || ppasword.Text == "" ||
                Pfoto == null || Pactivo.Checked == false && Pinactivo.Checked == false)
            {
                MessageBox.Show("no puede quedar nada vacio");
                mensajeError = "1";
            }

            int n;
            bool isNumeric = int.TryParse(Pnempleado.Text, out n);
            if (mensajeError == "" && isNumeric == false)
            {
                MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                mensajeError = "1";
            }

            if (Pactivo.Checked == true)
            {

                estado = "ACTIVO";

            }
            if (Pinactivo.Checked == true)
            {
                estado = "INACTIVO";

            }

            if (mensajeError == "")
            {
                Boolean yes = false;


                try
                {
                    try
                    {

                        StringBuilder mensajebuilder = new StringBuilder();
                        mensajebuilder.AppendLine("BIENVENIDO AL TESOEM ");
                        mensajebuilder.AppendLine("tu usuario es : " + Pnempleado.Text);
                        mensajebuilder.AppendLine("Y tu password es : " + ppasword.Text);
                        string sixtin = mensajebuilder.ToString();
                        MailMessage email = new MailMessage();
                        email.From = new MailAddress(Usuario);
                        email.To.Add(Pcorreo.Text.Trim());
                        email.Body = sixtin;
                        email.Subject = Pcorreo.Text.Trim();
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(Usuario, cPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(email);
                        yes = true;

                    }
                    catch
                    {

                        MessageBox.Show("EL CORREO NO EXISTE");
                        yes = false;

                    }


                    if (yes == true)
                    {
                        MemoryStream ms = new MemoryStream();
                        Pfoto.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();


                        String sujeto = Pnempleado.Text;
                        string nsujeto = Pnombre.Text;

                        string sql = "INSERT INTO policias (id_oficial, nombre, apellidoP,apellidoM , vigencia, estado ,curp,correo,password,foto) VALUES (@id_oficial, @nombre, @apellidoP,@apellidoM , @vigencia, @estado,@curp,@correo,@password,@foto)";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id_oficial", Pnempleado.Text);
                            comando.Parameters.AddWithValue("@nombre", Pnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", Papellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", Papellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", Pvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", estado);
                            comando.Parameters.AddWithValue("@curp", Pcurp.Text);
                            comando.Parameters.AddWithValue("@correo", Pcorreo.Text);
                            comando.Parameters.AddWithValue("@password", ppasword.Text);
                            comando.Parameters.AddWithValue("@foto", data);
                            comando.ExecuteNonQuery();

                        }
                        poli23();
                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "registro");
                            comando.Parameters.AddWithValue("@tipo", "policia");

                            comando.ExecuteNonQuery();
                        }

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);

                }

            }

        }

        private void poli23()
        {
            MessageBox.Show("REGISTRO CORRECTAMENTE");
            Pnempleado.Clear();
            Pnombre.Clear();
            Papellidom.Clear();
            Papellidop.Clear();
            Pvigencia.Text = "";
            ppasword.Clear();
            Pactivo.Checked = false;
            Pinactivo.Checked = false;
            Pfoto.Image = null;
            Pcurp.Clear();
            Pcorreo.Clear();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);
            Vpassword.Text = strCadena;
        }

        private void button20_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    seximatil = 5;
                    panel8.Visible = false;
                    panel58.Visible = true;
                    avisos.Visible = true;
                    marvetes.Visible = true;
                }
                catch (Exception morra)
                {
                    MessageBox.Show("" + morra);

                }
            }
            else
            {

                try
                {

                    OpenFileDialog ofdseleccionar = new OpenFileDialog();
                    ofdseleccionar.Filter = "Imagenes|*.jpg;*.png;*.jpeg";
                    ofdseleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofdseleccionar.Title = "Seleccionar la imagen ";
                    if (ofdseleccionar.ShowDialog() == DialogResult.OK)
                    {

                        Vfoto.Image = Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox4_Click(sender, e);
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;

            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
            panel58.Visible = false;

            usuarios.Visible = false;
            alumnos.Visible = false;
            docentes.Visible = false;
            administrativos.Visible = false;
            policias.Visible = false;
            vicitantes.Visible = true;

            busqueda.Visible = false;
            panel8.Visible = false;
            label1.Visible = true;
            avisos.Visible = false;
        }

        private void button19_Click(object sender, EventArgs e)
        {

            conectar();

            String estado = "";
            String mensajeError = "";

            if (Vnempleado.Text == "" || Vnombre.Text == "" ||
                 Vapellidop.Text == "" || Vapellidom.Text == "" ||
                 Vvigencia.Text == "" ||
                 Vcorreo.Text == "" || Vpassword.Text == "" ||
                 Vfoto == null || Vactivo.Checked == false && Vinactivo.Checked == false)
            {
                MessageBox.Show("no puede quedar nada vacio");
                mensajeError = "1";
            }

            int n;
            bool isNumeric = int.TryParse(Vnempleado.Text, out n);
            if (mensajeError == "" && isNumeric == false)
            {
                MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                mensajeError = "1";
            }

            if (Vactivo.Checked == true)
            {

                estado = "ACTIVO";

            }
            if (Vinactivo.Checked == true)
            {
                estado = "INACTIVO";

            }




            if (mensajeError == "")
            {
                Boolean yes = false;



                try
                {
                    try
                    {

                        StringBuilder mensajebuilder = new StringBuilder();
                        mensajebuilder.AppendLine("BIENVENIDO AL TESOEM ");
                        mensajebuilder.AppendLine("tu usuario es : " + Vnempleado.Text);
                        mensajebuilder.AppendLine("Y tu password es : " + Vpassword.Text);
                        string sixtin = mensajebuilder.ToString();
                        MailMessage email = new MailMessage();
                        email.From = new MailAddress(Usuario);
                        email.To.Add(Vcorreo.Text.Trim());
                        email.Body = sixtin;
                        email.Subject = Vcorreo.Text.Trim();
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(Usuario, cPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(email);
                        yes = true;

                    }
                    catch
                    {

                        MessageBox.Show("EL CORREO NO EXISTE");
                        yes = false;

                    }



                    if (yes == true)
                    {
                        MemoryStream ms = new MemoryStream();
                        Vfoto.Image.Save(ms, ImageFormat.Jpeg);
                        byte[] data = ms.ToArray();

                        String sujeto = Vnempleado.Text;
                        string nsujeto = Vnombre.Text;

                        string sql = "INSERT INTO visitantes (id_visitante , nombre, apellidoP ,apellidoM , vigencia, estado ,correo,password,asunto,foto) VALUES (@id_visitante, @nombre, @apellidoP,@apellidoM , @vigencia, @estado,@correo,@password,@asunto,@foto);";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id_visitante", Vnempleado.Text);
                            comando.Parameters.AddWithValue("@nombre", Vnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", Vapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", Vapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", Vvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", estado);
                            comando.Parameters.AddWithValue("@correo", Vcorreo.Text);
                            comando.Parameters.AddWithValue("@password", Vpassword.Text);
                            comando.Parameters.AddWithValue("@asunto", RA.Text);
                            comando.Parameters.AddWithValue("@foto", data);
                            comando.ExecuteNonQuery();

                        }
                        vist();
                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "registro");
                            comando.Parameters.AddWithValue("@tipo", "visitante");

                            comando.ExecuteNonQuery();
                        }

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);


                }


            }


        }

        private void vist()
        {
            MessageBox.Show("REGISTRO CORRECTAMENTE");
            Vnempleado.Clear();
            RA.Text = "";
            Vnombre.Clear();
            Vapellidom.Clear();
            Vapellidop.Clear();
            Vvigencia.Text = "";
            Vpassword.Clear();
            Vactivo.Checked = false;
            Vinactivo.Checked = false;
            Vfoto.Image = null;
            Vcorreo.Clear();
        }

        private void Vpassword_TextChanged(object sender, EventArgs e)
        {

        }




        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void radioregistro_CheckedChanged(object sender, EventArgs e)
        {
            if (black != 1) { 
                Busno.Clear();
                manu.Clear();
                label1.Visible = true;
                busqueda.Visible = false;
                panel1.Visible = false;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
                panel8.Visible = false;


                black = 1;
            }
 
     
        }

        private void radiobuscar_CheckedChanged(object sender, EventArgs e)
        {
            if (black != 2) {
                temporal = null;
                usuarios.Visible = true;
                alumnos.Visible = false;
                docentes.Visible = false;
                administrativos.Visible = false;
                policias.Visible = false;
                vicitantes.Visible = false;
                label1.Visible = false;
                busqueda.Visible = true;
                button5.Visible = false;
                button6.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                panel1.Visible = false;
                panel2.Visible = true;
                panel3.Visible = true;
                panel4.Visible = true;
                panel5.Visible = true;
                panel6.Visible = true;
                panel8.Visible = true;
                black = 2;

            }

        }

        private void radioeliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (black != 3)
            {
                MessageBox.Show("eliminar");
                
                black =3;

            }
             black = 4;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void BUSCAR_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel58.Visible = true;
            if (manu.Text != null && Busno.Text != null)
            {
                temporal = null;
                String miron = "0";
                conectar();

                try
                {
                    string sql = "SELECT nombre,apellidoP,apellidoM,grupo,estado,vigencia,password,carrera,correo,imagen FROM alumno WHERE matricula = @id and nombre = @nombre";
                    using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                    {
                        comando.Parameters.AddWithValue("@id", manu.Text);
                        comando.Parameters.AddWithValue("@nombre", Busno.Text);
                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                reader.Read();

                                byte[] imageData = (byte[])reader["imagen"];
                                if (imageData != null)
                                {
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        ms.Position = 0;
                                        Bitmap bm = new Bitmap(ms);
                                        BAfoto.Image = bm;
                                        
                                    }
                                }

                                BAnombre.Text = reader["nombre"].ToString();
                                BAapellidop.Text = reader["apellidoP"].ToString();
                                BAapellidom.Text = reader["apellidoM"].ToString();
                                BAgrupo.Text = reader["grupo"].ToString();
                                String estado = reader["estado"].ToString();
                                BAvigencia.Text = reader["vigencia"].ToString();
                                BApassword.Text = reader["password"].ToString();
                                BAcarrera.Text = reader["carrera"].ToString();
                                BAcorreo.Text = reader["correo"].ToString();
                                if (estado == "ACTIVO" || estado == "activo")
                                {

                                    BAactivo.Checked = true;

                                }
                                else { BAinactivo.Checked = true; }

                                BAmatricula.Text = manu.Text;

                                panel1.Visible = false;
                                panel58.Visible = true;
                                panel6.Visible = true;
                                panel7.Visible = true;
                                panel9.Visible = false;
                                panel10.Visible = false;
                                panel11.Visible = false;
                                miron = "1";

                            }
                            else
                            {
                                miron = "0";
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar: " + ex.Message);
                }

                if (miron == "0")
                {

                    conectar();

                    try
                    {
                        string sql = "SELECT nombre,apellidoP,apellidoM,estado,vigencia,password,correo,foto,asunto FROM visitantes WHERE id_visitante = @id and nombre = @nombre";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", manu.Text);
                            comando.Parameters.AddWithValue("@nombre", Busno.Text);
                            using (MySqlDataReader reader = comando.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {

                                    reader.Read();

                                    byte[] imageData = (byte[])reader["foto"];
                                    if (imageData != null)
                                    {
                                        using (MemoryStream ms = new MemoryStream(imageData))
                                        {
                                            ms.Position = 0;
                                            Bitmap bm = new Bitmap(ms);
                                            Bfoto.Image = bm;
                                        }
                                    }


                                    BVnombre.Text = reader["nombre"].ToString();
                                    BVapellidop.Text = reader["apellidoP"].ToString();
                                    BVapellidom.Text = reader["apellidoM"].ToString();
                                    String estado = reader["estado"].ToString();
                                    BVvigencia.Text = reader["vigencia"].ToString();
                                    BVpassword.Text = reader["password"].ToString();
                                    BVcorreo.Text = reader["correo"].ToString();
                                    comboBox2.Text = reader["asunto"].ToString();
                                    if (estado == "ACTIVO" || estado == "activo")
                                    {

                                        BVactivo.Checked = true;

                                    }
                                    else { BVinactivo.Checked = true; }

                                    BVnvisitante.Text = manu.Text;

                                    panel1.Visible = false;
                                    panel6.Visible = true;
                                    panel7.Visible = false;
                                    panel9.Visible = false;
                                    panel10.Visible = false;
                                    panel11.Visible = false;
                                    miron = "1";

                                }
                                else
                                {

                                    miron = "0";
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al buscar: " + ex.Message);
                    }

                }



                if (miron == "0")
                {

                    conectar();

                    try
                    {
                        string sql = "SELECT nombre,apellidoP,apellidoM,estado,vigencia,password,curp,correo,foto FROM docente WHERE id_docente = @id and nombre = @nombre ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", manu.Text);
                            comando.Parameters.AddWithValue("@nombre", Busno.Text);
                            using (MySqlDataReader reader = comando.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {

                                    reader.Read();

                                    byte[] imageData = (byte[])reader["foto"];
                                    if (imageData != null)
                                    {
                                        using (MemoryStream ms = new MemoryStream(imageData))
                                        {
                                            ms.Position = 0;
                                            Bitmap bm = new Bitmap(ms);
                                            BDfoto.Image = bm;
                                        }
                                    }


                                    BDnombre.Text = reader["nombre"].ToString();
                                    BDapellidop.Text = reader["apellidoP"].ToString();
                                    BDapellidom.Text = reader["apellidoM"].ToString();
                                    String estado = reader["estado"].ToString();
                                    BDcurp.Text = reader["curp"].ToString();
                                    BDvigencia.Text = reader["vigencia"].ToString();
                                    BDpassword.Text = reader["password"].ToString();
                                    BDcorreo.Text = reader["correo"].ToString();
                                    if (estado == "ACTIVO" || estado == "activo")
                                    {

                                        BDactivo.Checked = true;

                                    }
                                    else { BDinactivo.Checked = true; }

                                    BDndocente.Text = manu.Text;

                                    panel1.Visible = false;
                                    panel58.Visible = true;
                                    panel6.Visible = true;
                                    panel7.Visible = true;
                                    panel9.Visible = true;
                                    panel10.Visible = false;
                                    panel11.Visible = false;
                                    miron = "1";




                                }
                                else
                                {

                                    miron = "0";
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al buscar: " + ex.Message);
                    }



                }



                if (miron == "0")
                {


                    conectar();

                    try
                    {
                        string sql = "SELECT nombre,apellidoP,apellidoM,estado,vigencia,password,curp,correo,foto,nivel FROM administrativos WHERE id_empleado = @id and nombre = @nombre";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", manu.Text);
                            comando.Parameters.AddWithValue("@nombre", Busno.Text);
                            using (MySqlDataReader reader = comando.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {

                                    reader.Read();

                                    byte[] imageData = (byte[])reader["foto"];
                                    if (imageData != null)
                                    {
                                        using (MemoryStream ms = new MemoryStream(imageData))
                                        {
                                            ms.Position = 0;
                                            Bitmap bm = new Bitmap(ms);
                                            BADfoto.Image = bm;
                                        }
                                    }


                                    BADnombre.Text = reader["nombre"].ToString();
                                    BADapellidop.Text = reader["apellidoP"].ToString();
                                    BADapellidom.Text = reader["apellidoM"].ToString();
                                    String estado = reader["estado"].ToString();
                                    BADcurp.Text = reader["curp"].ToString();
                                    BADvigencia.Text = reader["vigencia"].ToString();
                                    BADpassword.Text = reader["password"].ToString();
                                    BADcorreo.Text = reader["correo"].ToString();
                                    BVniveles.Text = reader["nivel"].ToString();
                                    if (estado == "ACTIVO" || estado == "activo")
                                    {

                                        BADactivo.Checked = true;

                                    }
                                    else { BADinactivo.Checked = true; }

                                    BADnempleado.Text = manu.Text;

                                    panel1.Visible = false;
                                    panel58.Visible = true;
                                    panel6.Visible = true;
                                    panel7.Visible = true;
                                    panel9.Visible = true;
                                    panel10.Visible = true;
                                    panel11.Visible = false;
                                    miron = "1";


                                }
                                else
                                {

                                    miron = "0";

                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al buscar: " + ex.Message);
                    }



                }





                if (miron == "0")
                {


                    conectar();

                    try
                    {
                        string sql = "SELECT nombre,apellidoP,apellidoM,estado,vigencia,password,curp,correo,foto FROM policias WHERE id_oficial = @id and nombre = @nombre";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", manu.Text);
                            comando.Parameters.AddWithValue("@nombre", Busno.Text);
                            using (MySqlDataReader reader = comando.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {

                                    reader.Read();

                                    byte[] imageData = (byte[])reader["foto"];
                                    if (imageData != null)
                                    {
                                        using (MemoryStream ms = new MemoryStream(imageData))
                                        {
                                            ms.Position = 0;
                                            Bitmap bm = new Bitmap(ms);
                                            BPfoto.Image = bm;
                                        }
                                    }


                                    BPnombre.Text = reader["nombre"].ToString();
                                    BPapellidop.Text = reader["apellidoP"].ToString();
                                    BPapellidom.Text = reader["apellidoM"].ToString();
                                    String estado = reader["estado"].ToString();
                                    BPcurp.Text = reader["curp"].ToString();
                                    BPvigencia.Text = reader["vigencia"].ToString();
                                    BPpassword.Text = reader["password"].ToString();
                                    BPcorreo.Text = reader["correo"].ToString();
                                    if (estado == "ACTIVO" || estado == "activo")
                                    {

                                        BPactivo.Checked = true;

                                    }
                                    else { BPinactivo.Checked = true; }

                                    BPnpolicia.Text = manu.Text;

                                    panel1.Visible = false;
                                    panel58.Visible = true;
                                    panel6.Visible = true;
                                    panel7.Visible = true;
                                    panel9.Visible = true;
                                    panel10.Visible = true;
                                    panel11.Visible = true;
                                    miron = "1";




                                }
                                else
                                {

                                    miron = "0";
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al buscar: " + ex.Message);
                    }



                }

                if (miron == "0")
                {


                    MessageBox.Show("NO SE ENCONTRO NINGUN ARCHIVO");

                }

            }
            else {

                MessageBox.Show("NO PUEDE QUEDAR NADA VACIO");
            
            }

        }

        private void button22_Click(object sender, EventArgs e)
        {


            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 11;
                    panel8.Visible = false;
                    panel58.Visible = true;
                    avisos.Visible = true;
                    marvetes.Visible = true;

                }
                catch (Exception morra)
                {
                    MessageBox.Show("" + morra);

                }
            }
            else
            {


                try
                {

                    OpenFileDialog ofdseleccionar = new OpenFileDialog();
                    ofdseleccionar.Filter = "Imagenes|*.jpg;*.png;*.jpeg";
                    ofdseleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofdseleccionar.Title = "Seleccionar la imagen ";
                    if (ofdseleccionar.ShowDialog() == DialogResult.OK)
                    {

                        Bfoto.Image = Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            String sujeto = BVnvisitante.Text;
            string nsujeto = BVnombre.Text;
            conectar();
            string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                           "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

            using (MySqlCommand comando = new MySqlCommand(qy, conectado))
            {
                comando.Parameters.AddWithValue("@administrativo", usuario1);
                comando.Parameters.AddWithValue("@sujeto", sujeto);
                comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                comando.Parameters.AddWithValue("@realizo", "elimino");
                comando.Parameters.AddWithValue("@tipo", "visitante");
                comando.ExecuteNonQuery();
            }

            DialogResult dialogResult = MessageBox.Show("Seguro quieres eliminarlo", "CONFIRMACION", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    conectar();
                    String jquery = "DELETE FROM visitantes WHERE id_visitante = @id_visitante AND nombre = @nombre";
                    MySqlCommand comandoDB = new MySqlCommand(jquery, conectado);
                    comandoDB.Parameters.AddWithValue("id_visitante", BVnvisitante.Text);
                    comandoDB.Parameters.AddWithValue("@nombre", BVnombre.Text);

                    comandoDB.CommandTimeout = 60;
                    MySqlDataReader reader;

                    reader = comandoDB.ExecuteReader();
                    reader.Close();
                    visidel();
                }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
        }

        private void visidel()
        {
            MessageBox.Show("SE ELIMINO CORRECTAMENTE");
            BVnvisitante.Clear();
            BVnombre.Clear();
            BVapellidop.Clear();
            BVapellidom.Clear();
            BVvigencia.Text = "";
            BVpassword.Clear();
            BVactivo.Checked = false;
            BVinactivo.Checked = false;
            Bfoto.Image = null;
            BVcorreo.Clear();
            manu.Clear();
            Busno.Clear();
            panel1.Visible = false;
            panel58.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
        }

        private void button23_Click(object sender, EventArgs e)
        {

            try
            {
                MemoryStream ms = new MemoryStream();
                Bfoto.Image.Save(ms, ImageFormat.Jpeg);
                byte[] data = ms.ToArray();

                String ernesto = "0";
                String mensajeError = "0";

                if (BVnvisitante.Text == "" || BVnombre.Text == "" ||
                    BVapellidop.Text == "" || BVapellidom.Text == "" ||
                    BVvigencia.Text == "" ||
                    BVcorreo.Text == "" || BVpassword.Text == "" ||
                     BVactivo.Checked == false && BVinactivo.Checked == false)
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }

                int n;
                bool isNumeric = int.TryParse(BVnvisitante.Text, out n);
                if (mensajeError == "" && isNumeric == false)
                {
                    MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                    mensajeError = "1";
                }


                if (BVactivo.Checked == true)
                {

                    ernesto = "ACTIVO";

                }
                if (BVinactivo.Checked == true)
                {
                    ernesto = "INACTIVO";

                }

                if (mensajeError == "0")
                {

                    try
                    {

                        string sql = " UPDATE visitantes SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM,  estado = @estado, vigencia = @vigencia ,password = @password , correo = @correo,foto = @imagen, asunto = @asunto   where id_visitante = @matricula ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@matricula", BVnvisitante.Text);
                            comando.Parameters.AddWithValue("@nombre", BVnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BVapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BVapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BVvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@correo", BVcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BVpassword.Text);
                            comando.Parameters.AddWithValue("@imagen", data);
                            comando.Parameters.AddWithValue("@asunto", comboBox2.Text);
                            comando.ExecuteNonQuery();

                        }

                        String sujeto = BVnvisitante.Text;
                        string nsujeto = BVnombre.Text;
                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "visitante");

                            comando.ExecuteNonQuery();
                        }

                        viup();

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);

                    }
                }
            }
            catch {


                String ernesto = "0";
                String mensajeError = "0";

                if (BVnvisitante.Text == "" || BVnombre.Text == "" ||
                    BVapellidop.Text == "" || BVapellidom.Text == "" ||
                    BVvigencia.Text == "" ||
                    BVcorreo.Text == "" || BVpassword.Text == "" ||
                     BVactivo.Checked == false && BVinactivo.Checked == false)
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }

                int n;
                bool isNumeric = int.TryParse(BVnvisitante.Text, out n);
                if (mensajeError == "" && isNumeric == false)
                {
                    MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                    mensajeError = "1";
                }


                if (BVactivo.Checked == true)
                {

                    ernesto = "ACTIVO";

                }
                if (BVinactivo.Checked == true)
                {
                    ernesto = "INACTIVO";

                }


                if (mensajeError == "0")
                {

                    try
                    {

                        String sujeto = BVnvisitante.Text;
                        string nsujeto = BVnombre.Text;

                        string sql = " UPDATE visitantes SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM,  estado = @estado, vigencia = @vigencia ,password = @password , correo = @correo, asunto = @asunto   where id_visitante = @matricula ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@matricula", BVnvisitante.Text);
                            comando.Parameters.AddWithValue("@nombre", BVnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BVapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BVapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BVvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@correo", BVcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BVpassword.Text);
                            comando.Parameters.AddWithValue("@asunto", comboBox2.Text);
                            comando.ExecuteNonQuery();

                        }

                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "visitante");

                            comando.ExecuteNonQuery();
                        }
                        viup();

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }

            }
        }

        private void viup()
        {

            MessageBox.Show("SE ACTUALIZO CORRECTAMENTE");
            BAmatricula.Clear();
            BAnombre.Clear();
            BAapellidom.Clear();
            BAapellidop.Clear();
            BAgrupo.Clear();
            BAactivo.Checked = false;
            BAinactivo.Checked = false;
            BAvigencia.Text = "";
            BApassword.Clear();
            BAcarrera.Text = "";
            BAcorreo.Clear();
            manu.Clear();
            Busno.Clear();
            panel1.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
        }
        private void label62_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            BApassword.Text = strCadena;




        }

        private void button27_Click(object sender, EventArgs e)
        {


            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 12;
                    panel8.Visible = false;
                    panel58.Visible = true;
                    avisos.Visible = true;
                    marvetes.Visible = true;

                }
                catch (Exception morra)
                {
                    MessageBox.Show("" + morra);

                }
            }
            else
            {


                try
                {

                    OpenFileDialog ofdseleccionar = new OpenFileDialog();
                    ofdseleccionar.Filter = "Imagenes|*.jpg;*.png;*.jpeg";
                    ofdseleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofdseleccionar.Title = "Seleccionar la imagen ";
                    if (ofdseleccionar.ShowDialog() == DialogResult.OK)
                    {

                        BAfoto.Image = Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }

        }

        private void button10_Click(object sender, EventArgs e)
        {

            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            BVpassword.Text = strCadena;



            }
            
        private void button25_Click(object sender, EventArgs e)
        {
            String sujeto = BAmatricula.Text;
            string nsujeto = BAnombre.Text;
            conectar();
            string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                           "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

            using (MySqlCommand comando = new MySqlCommand(qy, conectado))
            {
                comando.Parameters.AddWithValue("@administrativo", usuario1);
                comando.Parameters.AddWithValue("@sujeto", sujeto);
                comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                comando.Parameters.AddWithValue("@realizo", "elimino");
                comando.Parameters.AddWithValue("@tipo", "alumno");

                comando.ExecuteNonQuery();
            }

            DialogResult dialogResult = MessageBox.Show("Seguro quieres eliminarlo", "CONFIRMACION", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                conectar();
                String jquery = "DELETE FROM alumno WHERE matricula = @matricula AND nombre = @nombre";
                MySqlCommand comandoDB = new MySqlCommand(jquery, conectado);
                comandoDB.Parameters.AddWithValue("@matricula", BAmatricula.Text);
                comandoDB.Parameters.AddWithValue("@nombre", BAnombre.Text);

                comandoDB.CommandTimeout = 60;
                MySqlDataReader reader;

                reader = comandoDB.ExecuteReader();
                reader.Close();
                    delalum1();
                }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }

        }

        private void delalum1()
        {
            MessageBox.Show("SE ELIMINO CORRECTAMENTE");
            BAmatricula.Clear();
            BAnombre.Clear();
            BAapellidop.Clear();
            BAapellidom.Clear();
            BAvigencia.Text = "";
            BApassword.Clear();
            BAactivo.Checked = false;
            BAinactivo.Checked = false;
            BAfoto.Image = null;
            BAcorreo.Clear();
            BAcarrera.Text = "";
            BAgrupo.Text = "";
            manu.Clear();
            Busno.Clear();
            panel1.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                BAfoto.Image.Save(ms, ImageFormat.Jpeg);
                byte[] data = ms.ToArray();
                String ernesto = "0";
                String mensajeError = "0";

                if (BAmatricula.Text == "" || BAnombre.Text == "" ||
                    BAapellidop.Text == "" || BAapellidom.Text == "" ||
                    BAvigencia.Text == "" ||
                    BAcorreo.Text == "" || BApassword.Text == "" ||
                     BAactivo.Checked == false && BAinactivo.Checked == false)
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }

                int n;
                bool isNumeric = int.TryParse(BAmatricula.Text, out n);
                if (mensajeError == "" && isNumeric == false)
                {
                    MessageBox.Show("MATRICULA SOLO ACEPTA NUMEROS");
                    mensajeError = "1";
                }


                if (BAactivo.Checked == true)
                {

                    ernesto = "ACTIVO";

                }
                if (BAinactivo.Checked == true)
                {
                    ernesto = "INACTIVO";

                }


                if (mensajeError == "0")
                {

                    String sujeto = BAmatricula.Text;
                    string nsujeto = BAnombre.Text;
                    conectar();
                    string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                   "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                    using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                    {
                        comando.Parameters.AddWithValue("@administrativo", usuario1);
                        comando.Parameters.AddWithValue("@sujeto", sujeto);
                        comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                        comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                        comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                        comando.Parameters.AddWithValue("@realizo", "modifico");
                        comando.Parameters.AddWithValue("@tipo", "alumno");

                        comando.ExecuteNonQuery();
                    }

                    try
                    {

                        conectar();

                        string sql = " UPDATE alumno SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, grupo= @grupo, estado = @estado, vigencia = @vigencia ,password = @password , carrera =@carrera,correo = @correo,imagen = @imagen   where matricula = @matricula ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@matricula", BAmatricula.Text);
                            comando.Parameters.AddWithValue("@nombre", BAnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BAapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BAapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BAvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@carrera", BAcarrera.Text);
                            comando.Parameters.AddWithValue("@grupo", BAgrupo.Text);
                            comando.Parameters.AddWithValue("@correo", BAcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BApassword.Text);
                            comando.Parameters.AddWithValue("@imagen", data);
                            comando.ExecuteNonQuery();

                        }
                        aluup();

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }

            }
            catch {

                String ernesto = "0";
                String mensajeError = "0";

                if (BAmatricula.Text == "" || BAnombre.Text == "" ||
                    BAapellidop.Text == "" || BAapellidom.Text == "" ||
                    BAvigencia.Text == "" ||
                    BAcorreo.Text == "" || BApassword.Text == "" ||
                     BAactivo.Checked == false && BAinactivo.Checked == false)
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }

                int n;
                bool isNumeric = int.TryParse(BAmatricula.Text, out n);
                if (mensajeError == "" && isNumeric == false)
                {
                    MessageBox.Show("MATRICULA SOLO ACEPTA NUMEROS");
                    mensajeError = "1";
                }


                if (BAactivo.Checked == true)
                {

                    ernesto = "ACTIVO";

                }
                if (BAinactivo.Checked == true)
                {
                    ernesto = "INACTIVO";

                }


                if (mensajeError == "0")
                {

                    String sujeto = BAmatricula.Text;
                    string nsujeto = BAnombre.Text;
                    conectar();
                    string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                   "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                    using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                    {
                        comando.Parameters.AddWithValue("@administrativo", usuario1);
                        comando.Parameters.AddWithValue("@sujeto", sujeto);
                        comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                        comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                        comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                        comando.Parameters.AddWithValue("@realizo", "modifico");
                        comando.Parameters.AddWithValue("@tipo", "alumno");

                        comando.ExecuteNonQuery();
                    }

                    try
                    {

                        conectar();

                        string sql = " UPDATE alumno SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, grupo= @grupo, estado = @estado, vigencia = @vigencia ,password = @password , carrera =@carrera,correo = @correo   where matricula = @matricula ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@matricula", BAmatricula.Text);
                            comando.Parameters.AddWithValue("@nombre", BAnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BAapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BAapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BAvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@carrera", BAcarrera.Text);
                            comando.Parameters.AddWithValue("@grupo", BAgrupo.Text);
                            comando.Parameters.AddWithValue("@correo", BAcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BApassword.Text);
                            comando.ExecuteNonQuery();

                        }
                        aluup();


                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }


            }

        }
        private void aluup()
        {


            MessageBox.Show("SE ACTUALIZO CORRECTAMENTE");
            BAmatricula.Clear();
            BAnombre.Clear();
            BAapellidom.Clear();
            BAapellidop.Clear();
            BAgrupo.Clear();
            BAactivo.Checked = false;
            BAinactivo.Checked = false;
            BAvigencia.Text = "";
            BApassword.Clear();
            BAcarrera.Text = "";
            BAcorreo.Clear();
            manu.Clear();
            Busno.Clear();
            panel1.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button30_Click(object sender, EventArgs e)
        {
            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            BDpassword.Text = strCadena;


        }

        private void button31_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 13;
                    panel8.Visible = false;
                    panel58.Visible = true;
                    avisos.Visible = true;
                    marvetes.Visible = true;

                }
                catch (Exception morra)
                {
                    MessageBox.Show("" + morra);

                }
            }
            else
            {


                try
                {

                    OpenFileDialog ofdseleccionar = new OpenFileDialog();
                    ofdseleccionar.Filter = "Imagenes|*.jpg;*.png;*.jpeg";
                    ofdseleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofdseleccionar.Title = "Seleccionar la imagen ";
                    if (ofdseleccionar.ShowDialog() == DialogResult.OK)
                    {

                       BDfoto.Image = Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }

        }

        private void button29_Click(object sender, EventArgs e)
        {
            String sujeto = BDndocente.Text;
            string nsujeto = BDnombre.Text;
            conectar();
            string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                           "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

            using (MySqlCommand comando = new MySqlCommand(qy, conectado))
            {
                comando.Parameters.AddWithValue("@administrativo", usuario1);
                comando.Parameters.AddWithValue("@sujeto", sujeto);
                comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                comando.Parameters.AddWithValue("@realizo", "elimino");
                comando.Parameters.AddWithValue("@tipo", "docente");

                comando.ExecuteNonQuery();
            }

            DialogResult dialogResult = MessageBox.Show("Seguro quieres eliminarlo", "CONFIRMACION", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                conectar();

                String jquery = "DELETE FROM docente WHERE id_docente = @id_docentes AND nombre = @nombre";
                    MySqlCommand comandoDB = new MySqlCommand(jquery, conectado);
                    comandoDB.Parameters.AddWithValue("@id_docentes", BDndocente.Text);
                    comandoDB.Parameters.AddWithValue("@nombre", BDnombre.Text);

                    comandoDB.CommandTimeout = 60;
                    MySqlDataReader reader;

                    reader = comandoDB.ExecuteReader();
                    reader.Close();
                    docedel();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);


                }

            }

        }

        private void docedel()
        {
            MessageBox.Show("SE ELIMINO CORRECTAMENTE");
            BDndocente.Clear();
            BDnombre.Clear();
            BDapellidop.Clear();
            BDapellidom.Clear();
            BDvigencia.Text = "";
            BDpassword.Clear();
            BDactivo.Checked = false;
            BDinactivo.Checked = false;
            BDfoto.Image = null;
            BDcurp.Clear();
            BDcorreo.Clear();
            manu.Clear();
            Busno.Clear();
            panel1.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;

        }
        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                BDfoto.Image.Save(ms, ImageFormat.Jpeg);
                byte[] data = ms.ToArray();

                String ernesto = "0";
                String mensajeError = "0";

                if (BDndocente.Text == "" || BDnombre.Text == "" ||
                    BDapellidop.Text == "" || BDapellidom.Text == "" ||
                    BDvigencia.Text == "" ||
                    BDcorreo.Text == "" || BDpassword.Text == "" ||
                     BDactivo.Checked == false && BDinactivo.Checked == false)
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }


                int n;
                bool isNumeric = int.TryParse(BDndocente.Text, out n);
                if (mensajeError == "" && isNumeric == false)
                {
                    MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                    mensajeError = "1";
                }


                if (BDactivo.Checked == true)
                {

                    ernesto = "ACTIVO";

                }
                if (BDinactivo.Checked == true)
                {
                    ernesto = "INACTIVO";

                }


                if (mensajeError == "0")
                {

                    try
                    {
                        String sujeto = manu.Text;
                        string nsujeto = BDnombre.Text;
                        conectar();
                        string sql = " UPDATE docente SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, foto = @foto, estado = @estado   where id_docente = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", manu.Text);
                            comando.Parameters.AddWithValue("@nombre", BDnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BDapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BDapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BDvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@curp", BDcurp.Text);
                            comando.Parameters.AddWithValue("@correo", BDcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BDpassword.Text);
                            comando.Parameters.AddWithValue("@foto", data);
                            comando.ExecuteNonQuery();

                        }
                        
                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "docente");

                            comando.ExecuteNonQuery();
                        }

                        docup();

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }

            }
            catch {



                String ernesto = "0";
                String mensajeError = "0";

                if (BDndocente.Text == "" || BDnombre.Text == "" ||
                    BDapellidop.Text == "" || BDapellidom.Text == "" ||
                    BDvigencia.Text == "" ||
                    BDcorreo.Text == "" || BDpassword.Text == "" ||
                     BDactivo.Checked == false && BDinactivo.Checked == false)
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }


                int n;
                bool isNumeric = int.TryParse(BDndocente.Text, out n);
                if (mensajeError == "" && isNumeric == false)
                {
                    MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                    mensajeError = "1";
                }


                if (BDactivo.Checked == true)
                {

                    ernesto = "ACTIVO";

                }
                if (BDinactivo.Checked == true)
                {
                    ernesto = "INACTIVO";

                }


                if (mensajeError == "0")
                {

                    try
                    {

                        String sujeto = manu.Text;
                        string nsujeto = BDnombre.Text;
                        conectar();
                        string sql = " UPDATE docente SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, estado = @estado   where id_docente = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", manu.Text);
                            comando.Parameters.AddWithValue("@nombre", BDnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BDapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BDapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BDvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@curp", BDcurp.Text);
                            comando.Parameters.AddWithValue("@correo", BDcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BDpassword.Text);
                            comando.ExecuteNonQuery();

                        }

                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "docente");

                            comando.ExecuteNonQuery();
                        }
                        docup();

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }

            }
        }

        private void docup()
        {

            MessageBox.Show("SE ACTUALIZO CORRECTAMENTE");
            BDndocente.Clear();
            BDnombre.Clear();
            BDapellidom.Clear();
            BDapellidop.Clear();
            BDcurp.Clear();
            BDactivo.Checked = false;
            BDinactivo.Checked = false;
            BDvigencia.Text = "";
            BDpassword.Clear();
            BDcorreo.Clear();
            manu.Clear();
            Busno.Clear();
            panel1.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;

        }
        private void button34_Click(object sender, EventArgs e)
        {

            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            BADpassword.Text = strCadena;



        }

        private void button35_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 14;
                    panel8.Visible = false;
                    panel58.Visible = true;
                    avisos.Visible = true;
                    marvetes.Visible = true;

                }
                catch (Exception morra)
                {
                    MessageBox.Show("" + morra);

                }
            }
            else
            {


                try
                {

                    OpenFileDialog ofdseleccionar = new OpenFileDialog();
                    ofdseleccionar.Filter = "Imagenes|*.jpg;*.png;*.jpeg";
                    ofdseleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofdseleccionar.Title = "Seleccionar la imagen ";
                    if (ofdseleccionar.ShowDialog() == DialogResult.OK)
                    {

                        BADfoto.Image = Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }

        }

        private void button33_Click(object sender, EventArgs e)
        {
            String sujeto = BADnempleado.Text;
            string nsujeto = BADnombre.Text;
            conectar();
            string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                           "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

            using (MySqlCommand comando = new MySqlCommand(qy, conectado))
            {
                comando.Parameters.AddWithValue("@administrativo", usuario1);
                comando.Parameters.AddWithValue("@sujeto", sujeto);
                comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                comando.Parameters.AddWithValue("@realizo", "elimino");
                comando.Parameters.AddWithValue("@tipo", "administrativo");

                comando.ExecuteNonQuery();
            }
            DialogResult dialogResult = MessageBox.Show("Seguro quieres eliminarlo", "CONFIRMACION", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    conectar();
                    String jquery = "DELETE FROM administrativos WHERE id_empleado = @id_empleado AND nombre = @nombre";
                    MySqlCommand comandoDB = new MySqlCommand(jquery, conectado);
                    comandoDB.Parameters.AddWithValue("@id_empleado", BADnempleado.Text);
                    comandoDB.Parameters.AddWithValue("@nombre", BADnombre.Text);

                    comandoDB.CommandTimeout = 60;
                    MySqlDataReader reader;

                    reader = comandoDB.ExecuteReader();
                    reader.Close();
                    addel();

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);


                }
            }
        }

        private void addel()
        {
            MessageBox.Show("SE ELIMINO CORRECTAMENTE");
            BADnempleado.Clear();
            BADnombre.Clear();
            BADapellidop.Clear();
            BADapellidom.Clear();
            BADvigencia.Text = "";
            BADpassword.Clear();
            BADactivo.Checked = false;
            BADinactivo.Checked = false;
            BADfoto.Image = null;
            BADcurp.Clear();
            BADcorreo.Clear();
            manu.Clear();
            Busno.Clear();
            panel1.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
        }
        private void button32_Click(object sender, EventArgs e)
        {

            try
            {

                MemoryStream ms = new MemoryStream();
                BADfoto.Image.Save(ms, ImageFormat.Jpeg);
                byte[] data = ms.ToArray();

                String ernesto = "0";
                String mensajeError = "0";

                if (BADnempleado.Text == "" || BADnombre.Text == "" ||
                    BADapellidop.Text == "" || BADapellidom.Text == "" ||
                    BADvigencia.Text == "" ||
                    BADcorreo.Text == "" || BADpassword.Text == "" ||
                     BADactivo.Checked == false && BADinactivo.Checked == false)
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }


                int n;
                bool isNumeric = int.TryParse(BADnempleado.Text, out n);
                if (mensajeError == "" && isNumeric == false)
                {
                    MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                    mensajeError = "1";
                }


                if (BADactivo.Checked == true)
                {

                    ernesto = "ACTIVO";

                }
                if (BADinactivo.Checked == true)
                {
                    ernesto = "INACTIVO";

                }


                if (mensajeError == "0")
                {

                    try
                    {

                        String sujeto = manu.Text;
                        string nsujeto = BADnombre.Text;

                        conectar();

                        string sql = " UPDATE administrativos SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, foto = @foto, estado = @estado, nivel = @nivel   where id_empleado = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", manu.Text);
                            comando.Parameters.AddWithValue("@nombre", BADnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BADapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BADapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BADvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@curp", BADcurp.Text);
                            comando.Parameters.AddWithValue("@correo", BADcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BADpassword.Text);
                            comando.Parameters.AddWithValue("@foto", data);
                            comando.Parameters.AddWithValue("@nivel", BVniveles.Text);
                            comando.ExecuteNonQuery();

                        }

                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "administrativo");

                            comando.ExecuteNonQuery();
                        }
                        udad();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

            }
            catch {


                String ernesto = "0";
                String mensajeError = "0";

                if (BADnempleado.Text == "" || BADnombre.Text == "" ||
                    BADapellidop.Text == "" || BADapellidom.Text == "" ||
                    BADvigencia.Text == "" ||
                    BADcorreo.Text == "" || BADpassword.Text == "" ||
                     BADactivo.Checked == false && BADinactivo.Checked == false)
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }


                int n;
                bool isNumeric = int.TryParse(BADnempleado.Text, out n);
                if (mensajeError == "" && isNumeric == false)
                {
                    MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                    mensajeError = "1";
                }


                if (BADactivo.Checked == true)
                {

                    ernesto = "ACTIVO";

                }
                if (BADinactivo.Checked == true)
                {
                    ernesto = "INACTIVO";

                }


                if (mensajeError == "0")
                {

                    try
                    {

                        String sujeto = manu.Text;
                        string nsujeto = BADnombre.Text;

                        conectar();

                        string sql = " UPDATE administrativos SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, estado = @estado, nivel = @nivel   where id_empleado = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", manu.Text);
                            comando.Parameters.AddWithValue("@nombre", BADnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BADapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BADapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BADvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@curp", BADcurp.Text);
                            comando.Parameters.AddWithValue("@correo", BADcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BADpassword.Text);
                            comando.Parameters.AddWithValue("@nivel", BVniveles.Text);
                            comando.ExecuteNonQuery();

                        }
                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "administrativo");

                            comando.ExecuteNonQuery();
                        }
                        udad();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

            }
        }

        private void udad()
        {
            MessageBox.Show("SE ACTUALIZO CORRECTAMENTE");
            BADnempleado.Clear();
            BADnombre.Clear();
            BADapellidom.Clear();
            BADapellidop.Clear();
            BADcurp.Clear();
            BADactivo.Checked = false;
            BADinactivo.Checked = false;
            BADvigencia.Text = "";
            BADpassword.Clear();
            BADcorreo.Clear();
            manu.Clear();
            Busno.Clear();
            panel1.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button38_Click(object sender, EventArgs e)
        {

            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            BPpassword.Text = strCadena;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    seximatil = 15;
                    panel58.Visible = true;
                    panel8.Visible = false;
                    panel6.Visible=true;
                    avisos.Visible = true;
                    marvetes.Visible = true;
                }
                catch (Exception morra)
                {
                    MessageBox.Show("" + morra);

                }
            }
            else
            {

                try
                {

                    OpenFileDialog ofdseleccionar = new OpenFileDialog();
                    ofdseleccionar.Filter = "Imagenes|*.jpg;*.png;*.jpeg";
                    ofdseleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ofdseleccionar.Title = "Seleccionar la imagen ";
                    if (ofdseleccionar.ShowDialog() == DialogResult.OK)
                    {
                      
                        panel11.Visible = true;
                        BPfoto.Image = Image.FromFile(ofdseleccionar.FileName);
                        marvetes.Visible= false;
                    }
                }
                catch
                {
                    MessageBox.Show("Error al abrir");
                }


            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            String sujeto = BPnpolicia.Text;
            string nsujeto = BPnombre.Text;
            conectar();
            string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                           "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

            using (MySqlCommand comando = new MySqlCommand(qy, conectado))
            {
                comando.Parameters.AddWithValue("@administrativo", usuario1);
                comando.Parameters.AddWithValue("@sujeto", sujeto);
                comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                comando.Parameters.AddWithValue("@realizo", "elimino");
                comando.Parameters.AddWithValue("@tipo", "policia");

                comando.ExecuteNonQuery();
            }
            DialogResult dialogResult = MessageBox.Show("Seguro quieres eliminarlo", "CONFIRMACION", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    conectar();
                    String jquery = "DELETE FROM policias WHERE id_oficial = @id_oficial AND nombre = @nombre";
                    MySqlCommand comandoDB = new MySqlCommand(jquery, conectado);
                    comandoDB.Parameters.AddWithValue("@id_oficial", BPnpolicia.Text);
                    comandoDB.Parameters.AddWithValue("@nombre", BPnombre.Text);

                    comandoDB.CommandTimeout = 60;
                    MySqlDataReader reader;

                    reader = comandoDB.ExecuteReader();
                    reader.Close();
                    poldel();

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);

                }
            }

        }

        private void poldel()
        {
            MessageBox.Show("SE ELIMINO CORRECTAMENTE");
            BPnpolicia.Clear();
            BPnombre.Clear();
            BPapellidop.Clear();
            BPapellidom.Clear();
            BPvigencia.Text = "";
            BPpassword.Clear();
            BPactivo.Checked = false;
            BPinactivo.Checked = false;
            BPfoto.Image = null;
            BPcurp.Clear();
            BPcorreo.Clear();
            manu.Clear();
            Busno.Clear();
            panel1.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                BPfoto.Image.Save(ms, ImageFormat.Jpeg);
                byte[] data = ms.ToArray();

                String ernesto = "0";
                String mensajeError = "0";

                if (BPnpolicia.Text == "" || BPnombre.Text == "" ||
                    BPapellidop.Text == "" || BPapellidom.Text == "" ||
                    BPvigencia.Text == "" ||
                    BPcorreo.Text == "" || BPpassword.Text == "" ||
                     BPactivo.Checked == false && BPinactivo.Checked == false)
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }

                int n;
                bool isNumeric = int.TryParse(BPnpolicia.Text, out n);
                if (mensajeError == "" && isNumeric == false)
                {
                    MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                    mensajeError = "1";
                }


                if (BPactivo.Checked == true)
                {
                    ernesto = "ACTIVO";
                }
                if (BPinactivo.Checked == true)
                {
                    ernesto = "INACTIVO";
                }

                if (mensajeError == "0")
                {
                    try
                    {
                        String sujeto = manu.Text;
                        string nsujeto = BPnombre.Text;
                        conectar();
                        string sql = " UPDATE policias SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, foto = @foto, estado = @estado where id_oficial = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", manu.Text);
                            comando.Parameters.AddWithValue("@nombre", BPnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BPapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BPapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BPvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@curp", BPcurp.Text);
                            comando.Parameters.AddWithValue("@correo", BPcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BPpassword.Text);
                            comando.Parameters.AddWithValue("@foto", data);
                            comando.ExecuteNonQuery();

                        }
                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "policia");

                            comando.ExecuteNonQuery();
                        }
                        udpoli();

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);

                    }
                }

            }
            catch {


                String ernesto = "0";
                String mensajeError = "0";

                if (BPnpolicia.Text == "" || BPnombre.Text == "" ||
                    BPapellidop.Text == "" || BPapellidom.Text == "" ||
                    BPvigencia.Text == "" ||
                    BPcorreo.Text == "" || BPpassword.Text == "" ||
                     BPactivo.Checked == false && BPinactivo.Checked == false)
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }


                int n;
                bool isNumeric = int.TryParse(BPnpolicia.Text, out n);
                if (mensajeError == "" && isNumeric == false)
                {
                    MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
                    mensajeError = "1";
                }


                if (BPactivo.Checked == true)
                {

                    ernesto = "ACTIVO";

                }
                if (BPinactivo.Checked == true)
                {
                    ernesto = "INACTIVO";

                }


                if (mensajeError == "0")
                {

                    try
                    {
                        String sujeto = manu.Text;
                        string nsujeto = BPnombre.Text;

                        conectar();
                        string sql = " UPDATE policias SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, estado = @estado   where id_oficial = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", manu.Text);
                            comando.Parameters.AddWithValue("@nombre", BPnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BPapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BPapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BPvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@curp", BPcurp.Text);
                            comando.Parameters.AddWithValue("@correo", BPcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BPpassword.Text);
                            comando.ExecuteNonQuery();

                        }
                        udpoli();
                        conectar();
                        string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                                       "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

                        using (MySqlCommand comando = new MySqlCommand(qy, conectado))
                        {
                            comando.Parameters.AddWithValue("@administrativo", usuario1);
                            comando.Parameters.AddWithValue("@sujeto", sujeto);
                            comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                            comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                            comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "policia");

                            comando.ExecuteNonQuery();
                        }

                        

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }



            }

        }

        private void udpoli()
        {
            MessageBox.Show("SE ACTUALIZO CORRECTAMENTE");
            BADnempleado.Clear();
            BADnombre.Clear();
            BADapellidom.Clear();
            BADapellidop.Clear();
            BADcurp.Clear();
            BADactivo.Checked = false;
            BADinactivo.Checked = false;
            BADvigencia.Text = "";
            BADpassword.Clear();
            BADcorreo.Clear();
            manu.Clear();
            Busno.Clear();
            panel1.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
        }
        
        private void administración_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AVISOP_Click(object sender, EventArgs e)
        {
            pictureBox4_Click(sender, e);
            usuarios.Visible = false;
                alumnos.Visible = false;
                docentes.Visible = false;
                administrativos.Visible = false;
                policias.Visible = false;
                vicitantes.Visible = false;
                label1.Visible = false;
                label12.Visible = false;
                busqueda.Visible = false;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = false;
                panel8.Visible = false;
                panel58.Visible = true;
                avisos.Visible = true;
        }

        private void avisos_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, this.Height),
                Color.White,  // Color del inicio (arriba)
                Color.Green   // Color del final (abajo)
            );
            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            avisos.Visible=false;
        }

        private void label115_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button41_Click(object sender, EventArgs e)
        {

            StringBuilder para = new StringBuilder();

            if (checkBox1.Checked==true) {


                para.Append("DOCENTES"); 
            }
            if (checkBox2.Checked == true)
            {


                para.Append(" ADMINISTRATIVOS");
            }
            if (checkBox3.Checked == true)
            {


                para.Append("ALUMNOS");
            }
            if (checkBox4.Checked == true)
            {


                para.Append("POLICIAS");
            }
            if (checkBox5.Checked == true)
            {


                para.Append("VISITANTES");
            }

            if (checkBox6.Checked == true)
            {

                para.Clear();
                para.Append("TODOS");

            }

            String chicharon = (String)para.ToString();



            conectar();

            string sql = "INSERT INTO aviso (id_aviso, texto, fecha, dirigido, titulo, vigencia) VALUES ( @id_aviso, @texto, @fecha, @dirigido, @titulo, @vigencia)";
            using (MySqlCommand comando = new MySqlCommand(sql, conectado))
            {   
                comando.Parameters.AddWithValue("@id_aviso", 0);
                comando.Parameters.AddWithValue("@texto", textBox2.Text);
                comando.Parameters.AddWithValue("@fecha", DateTime.Now );
                comando.Parameters.AddWithValue("@dirigido", chicharon);
                comando.Parameters.AddWithValue("@titulo", textBox1.Text);
                comando.Parameters.AddWithValue("@vigencia", avisovigencia.Text.ToString());
                comando.ExecuteNonQuery();


            }


            String sujeto = chicharon;
            string nsujeto = chicharon;

            conectar();

            string qy = "INSERT INTO movimientos (administrativo, sujeto, nsujeto, fecha, ip, realizo, tipo) " +
                           "VALUES (@administrativo, @sujeto, @nsujeto, @fecha, @ip, @realizo, @tipo)";

            using (MySqlCommand comando = new MySqlCommand(qy, conectado))
            {
                comando.Parameters.AddWithValue("@administrativo", usuario1);
                comando.Parameters.AddWithValue("@sujeto", sujeto);
                comando.Parameters.AddWithValue("@nsujeto", nsujeto);
                comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                comando.Parameters.AddWithValue("@ip", ObtenerDireccionIP());
                comando.Parameters.AddWithValue("@realizo", "aviso");
                comando.Parameters.AddWithValue("@tipo", "administrativo");

                comando.ExecuteNonQuery();
            }



            MessageBox.Show("AVISO ENVIADO");
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            textBox1.Clear();
            textBox2.Clear();




        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox6.Checked == true)
            {


                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
                checkBox5.Checked = true;

            }
            else {

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;


            }
            
        }

        private void button42_Click(object sender, EventArgs e)
        {
            pictureBox4_Click(sender, e);
            this.Hide();

            Form1 re = new Form1();

            re.Show();
        }

        private void tomarfoto_Click(object sender, EventArgs e)
        {
            if (miwebcam != null && miwebcam.IsRunning)
            {
                imagenAnterior = pictureBox2.Image;
                pictureBox3.Image = imagenAnterior;
            }

        }

        
        private void ABRIRCAMARA_Click(object sender, EventArgs e)
        {

                cerrar();
                int i = comboBox3.SelectedIndex;
                String NombreVideo = misdispositivos[i].MonikerString;
                miwebcam = new VideoCaptureDevice(NombreVideo);
                miwebcam.NewFrame += new NewFrameEventHandler(Capturando);
                miwebcam.Start();
            
        }

        private void lunas1()
        {
            Image imageToUpload = pictureBox3.Image;
            if (seximatil == 1)
            {
                foto.Image = pictureBox3.Image;
            }
            else if (seximatil == 2)
            {

                Dfoto.Image = pictureBox3.Image;

            }
            else if (seximatil == 3)
            {

                Afoto.Image = pictureBox3.Image;

            }
            else if (seximatil == 4)
            {

                Pfoto.Image = pictureBox3.Image;

            }
            else if (seximatil == 5)
            {

                Vfoto.Image = pictureBox3.Image;

            }

            cerrar();
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            panel8.Visible = false;
            marvetes.Visible = false;
            avisos.Visible = false;
            panel58.Visible = false;
        }
        private void sol1()
        {
            Image imageToUpload = pictureBox3.Image;
            if (seximatil == 11)
            {

                Bfoto.Image = pictureBox3.Image;

            }
            else if (seximatil == 12)
            {

                BAfoto.Image = pictureBox3.Image;

            }
            else if (seximatil == 13)
            {

                BDfoto.Image = pictureBox3.Image;

            }
            else if (seximatil == 14)
            {

                BADfoto.Image = pictureBox3.Image;

            }
            else if (seximatil == 15)
            {

                BPfoto.Image = pictureBox3.Image;

            }

            cerrar();
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            panel8.Visible = true;
            marvetes.Visible = false;
            avisos.Visible = false;
            panel58.Visible = true;
        }

        private void SUBIRFOTO_Click(object sender, EventArgs e)
        {
            if (seximatil >= 1 && seximatil <= 5)
            {
                lunas1();
            }
            else
            {
                sol1();
            }

        }

        bool sidebarExpand;

        private void button44_Click(object sender, EventArgs e)
        {
            pictureBox4_Click(sender, e);
            avisos.Visible = false;
            /*if (black != 2)
            {*/
                temporal = null;
                panel58.Visible = true;
                usuarios.Visible = true;
                alumnos.Visible = false;
                docentes.Visible = false;
                administrativos.Visible = false;
                policias.Visible = false;
                vicitantes.Visible = false;
                label12.Visible = true;
                label1.Visible = false;
                busqueda.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
                panel1.Visible = false;
                panel2.Visible = true;
                panel3.Visible = true;
                panel4.Visible = true;
                panel5.Visible = true;
                panel6.Visible = false;
                panel8.Visible = true;
                
                black = 2;

           // }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            siderbarTimer.Start();
        }

        private void siderbarTimer_Tick_1(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    siderbarTimer.Stop();

                    if (menuExpand)
                    {
                        menuTransition.Start();
                    }
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    siderbarTimer.Stop();
                }
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            
            if (sidebarExpand)
            {
                // Your existing code for button43_Click
                menuTransition.Start();
                if (black != 1)
                {
                    Busno.Clear();
                    manu.Clear();
                    label1.Visible = true;
                    busqueda.Visible = false;
                    label12.Visible = true;
                    panel1.Visible = false;
                    button5.Visible = true;
                    button6.Visible = true;
                    button7.Visible = true;
                    button8.Visible = true;
                    button9.Visible = true;
                    panel8.Visible = false;
                    panel58.Visible = false;
                    black = 1;
                }
                avisos.Visible = false;
            }
            else
            {
                return;
            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void panel14_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        bool menuExpand = false;
        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand==false)
            {
                menuContainer.Height += 10;
                if (menuContainer.Height >= 360)
                {
                    menuExpand = true;
                    menuTransition.Stop();
                }
            }
            else
            {
                menuContainer.Height -= 10;
                if (menuContainer.Height <= 71)
                {
                    menuExpand = false;
                    menuTransition.Stop();
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void administración_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, this.Height),
                Color.White,  // Color del inicio (arriba)
                Color.Green   // Color del final (abajo)
            );
            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
        }
        
        private void administración_BackColorChanged(object sender, EventArgs e)
        {

        }
        private int radioBorde = 70;
        private string nombre;
        private string apellido;
        private int v;

        private void mmatricula_Resize(object sender, EventArgs e)
        {
           
        }

        private void mmatricula_BorderStyleChanged(object sender, EventArgs e)
        {
            base.OnResize(e);
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, radioBorde, radioBorde, 180, 90);
                path.AddArc(Width - radioBorde, 0, radioBorde, radioBorde, 270, 90);
                path.AddArc(Width - radioBorde, Height - radioBorde, radioBorde, radioBorde, 0, 90);
                path.AddArc(0, Height - radioBorde, radioBorde, radioBorde, 90, 90);

                this.Region = new Region(path);
            }
        }

        private void foto_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button4_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        
    }

        private void button3_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        
        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button13_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button11_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void Dingresar_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button14_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button12_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void Aagregar_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button15_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button16_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button17_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button18_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button19_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button20_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void panel58_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, this.Height),
                Color.White,  // Color del inicio (arriba)
                Color.Green   // Color del final (abajo)
            );
            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
        }

        private void button10_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button21_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button23_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button22_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button25_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button24_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button27_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button30_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button28_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button29_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button31_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button32_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button33_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button35_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button34_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button38_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button37_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button36_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button39_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void BPnombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            mmatricula.Text= string.Empty;
            nnombre.Text= string.Empty;
            apellidop.Text= string.Empty;
            apellidom.Text= string.Empty;
            ggrupo.Text= string.Empty;
            CCORREO.Text= string.Empty;
            txfecha.Text= string.Empty;
            comboBox1.Items.Clear();
            ACTIVOB.Checked = false; ;
            INACTIVOB.Checked = false; ;
            ppassword.Text= string.Empty;
            foto.Image = null;
        }

        private void button40_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button40_Click_1(object sender, EventArgs e)
        {
            Dnempleado.Text = string.Empty;
            Dnombre.Text = string.Empty;
            Dapellidop.Text = string.Empty;
            Dapellidom.Text = string.Empty;
            Dcurp.Text = string.Empty;
            Dcorreo.Text = string.Empty;
            Dvigencia.Text = string.Empty;
            Dactivo.Checked = false ;
            Dinactivo.Checked = false; 
            Dpassword.Text = string.Empty;
            Dfoto.Image = null;
        }

        private void button45_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            Anempleado.Text = string.Empty;
            Anombre.Text = string.Empty;
            AapellidoP.Text = string.Empty;
            AapellidoM.Text = string.Empty;
            Acurp.Text = string.Empty;
            Acorreo.Text = string.Empty;
            Avigencia.Text = string.Empty;
            Aniveles.Items.Clear();
            Aactivo.Checked = false;
            Ainactivo.Checked = false;
            Apassword.Text = string.Empty;
            Afoto.Image = null;
        }

        private void button46_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            Pnempleado.Text = string.Empty;
            Pnombre.Text = string.Empty;
            Papellidop.Text = string.Empty;
            Papellidom.Text = string.Empty;
            Pcurp.Text = string.Empty;
            Pcorreo.Text = string.Empty;
            Pvigencia.Text = string.Empty;
            Pactivo.Checked = false; 
            Pinactivo.Checked = false;
            ppassword.Text = string.Empty;
            Pfoto.Image = null;
        }

        private void button47_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Button button = (System.Windows.Forms.Button)sender;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radio = 20; // Puedes ajustar el radio según tus preferencias

            // Agregar arcos para hacer los bordes redondos
            path.AddArc(0, 0, radio, radio, 180, 90); // Esquina superior izquierda
            path.AddArc(button.Width - radio, 0, radio, radio, 270, 90); // Esquina superior derecha
            path.AddArc(button.Width - radio, button.Height - radio, radio, radio, 0, 90); // Esquina inferior derecha
            path.AddArc(0, button.Height - radio, radio, radio, 90, 90); // Esquina inferior izquierda

            // Cerrar la figura
            path.CloseFigure();

            // Aplicar el borde redondo al botón
            button.Region = new System.Drawing.Region(path);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            Vnempleado.Text = string.Empty;
            Vnombre.Text = string.Empty;
            Vapellidop.Text = string.Empty;
            Vapellidom.Text = string.Empty;
            Vcorreo.Text = string.Empty;
            Vvigencia.Text = string.Empty;
            RA.Items.Clear();
            Vactivo.Checked = false;
            Vinactivo.Checked = false;
            Vpassword.Text = string.Empty;
            Vfoto.Image = null;
        }

        private void button48_Click(object sender, EventArgs e)
        {
        }
    }
}
