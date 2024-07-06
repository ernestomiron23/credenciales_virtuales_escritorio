using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
//using System.Web.UI.WebControls;
using Google.Protobuf.WellKnownTypes;
using System.Net;
using System.Drawing.Drawing2D;

namespace App_Console
{
    public partial class Form5 : Form
    {
        

        MySqlConnection conectado;
        private bool Haydispositivos;
        private FilterInfoCollection misdispositivos;
        private VideoCaptureDevice miwebcam;
        public int seximatil = 0;
        private int usuario1;
        private Image imagenAnterior;
        public Form5(String nombre , String apellido,String nivel, int usuario1)
        {
            InitializeComponent();
            this.usuario1 = usuario1;

            label1.Text = nombre + " "+ apellido;
            panel1.Visible = false;
            carga();
            panel13.Visible = false;
            if (nivel == "4") { 
               button14.Visible = false;
               button15.Visible = false;
               button10.Visible = false;
               button11.Visible = false;
               button6.Visible = false;
               button7.Visible = false;
               button2.Visible = false;
               button3.Visible = false;
               button36.Visible = false;
               button37.Visible = false;
            }
        }

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


        private void button1_Click(object sender, EventArgs e)
        {
            Boolean valorar = false;

            if (textBox1.Text == "" || textBox2.Text == "") {

                MessageBox.Show("NO PUEDE QUEDAR NADA VACIO");
            
            }else
            {

                try
                {
                    conectar();
                    string sql = "SELECT nombre,apellidoP,apellidoM,estado,vigencia,password,curp,correo,foto FROM policias WHERE id_oficial = @id and nombre = @nombre";
                    using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                    {
                        comando.Parameters.AddWithValue("@id", textBox1.Text);
                        comando.Parameters.AddWithValue("@nombre", textBox2.Text);
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
                                        ;
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
                                else {
                                    
                                    BPinactivo.Checked = true; 
                                }

                                panel1.Visible = true;
                                panel2.Visible = false;
                                panel3.Visible = false;
                                panel4.Visible = false;
                                panel5.Visible = false;
                                BPnpolicia.Text = textBox1.Text;
                                valorar = true;

                            }
                            else
                            {
                                valorar = false;
                            }

     

                        }
                    }
                } catch (Exception ex){

                    MessageBox.Show("Error al buscar: " + ex.Message);

                }


                if (valorar == false) {

                    try
                    {
                        string sql = "SELECT nombre,apellidoP,apellidoM,grupo,estado,vigencia,password,carrera,correo, imagen FROM alumno WHERE matricula = @id and nombre = @nombre";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", textBox1.Text);
                            comando.Parameters.AddWithValue("@nombre", textBox2.Text);
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
                                            ;
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
                                    else
                                    {

                                        BAinactivo.Checked = true;
                                    }

                                    panel1.Visible = true;
                                    panel2.Visible = true;
                                    panel3.Visible = false;
                                    panel4.Visible = false;
                                    panel5.Visible = false;
                                    BAmatricula.Text = textBox1.Text;
                                    valorar = true;

                                }
                                else
                                {
                                    valorar = false;
                                }

                            


                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error al buscar: " + ex.Message);

                    }



                }
                if (valorar == false)
                {

                    try
                    {
                        string sql = "SELECT nombre,apellidoP,apellidoM,estado,vigencia,password,curp,correo,foto FROM docente WHERE id_docente = @id and nombre = @nombre";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", textBox1.Text);
                            comando.Parameters.AddWithValue("@nombre", textBox2.Text);
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
                                            ;
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

                                    BDnempleado.Text = textBox1.Text;
                                                                 
                                    panel1.Visible = true;
                                    panel2.Visible = true;
                                    panel3.Visible = true;
                                    panel4.Visible = false;
                                    panel5.Visible = false;
                                    valorar = true;

                                }
                                else
                                {
                                    valorar = false;
                                }




                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error al buscar: " + ex.Message);

                    }



                }

                if (valorar == false) { 

                conectar();

                try
                {
                    string sql = "SELECT nombre,apellidoP,apellidoM,estado,vigencia,password,curp,correo,foto,nivel FROM administrativos WHERE id_empleado = @id and nombre = @nombre";
                    using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                    {
                        comando.Parameters.AddWithValue("@id", textBox1.Text);
                        comando.Parameters.AddWithValue("@nombre", textBox2.Text);
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
                                BADniveles.Text = reader["nivel"].ToString();
                                if (estado == "ACTIVO" || estado == "activo")
                                {

                                    BADactivo.Checked = true;

                                }
                                else { BADinactivo.Checked = true; }

                                BADnempleado.Text = textBox1.Text;

                                    panel1.Visible = true;
                                    panel2.Visible = true;
                                    panel3.Visible = true;
                                    panel4.Visible = true;
                                    panel5.Visible = false;
                                    valorar = true;



                            }
                            else
                            {

                                    valorar = false;

                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar: " + ex.Message);
                }



            }

                if (valorar == false)
                {

                    conectar();

                    try
                    {
                        string sql = "SELECT nombre,apellidoP,apellidoM,estado,vigencia,password,correo,foto,asunto FROM visitantes WHERE id_visitante = @id and nombre = @nombre";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", textBox1.Text);
                            comando.Parameters.AddWithValue("@nombre", textBox2.Text);
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
                                            BVfoto.Image = bm;
                                        }
                                    }


                                    BVnombre.Text = reader["nombre"].ToString();
                                    BVapellidop.Text = reader["apellidoP"].ToString();
                                    BVapellidom.Text = reader["apellidoM"].ToString();
                                    String estado = reader["estado"].ToString();
                                    BVvigencia.Text = reader["vigencia"].ToString();
                                    BVpassword.Text = reader["password"].ToString();
                                    BVcorreo.Text = reader["correo"].ToString();
                                    BVasunto.Text = reader["asunto"].ToString();
                                    if (estado == "ACTIVO" || estado == "activo")
                                    {

                                        BVactivo.Checked = true;

                                    }
                                    else { BVinactivo.Checked = true; }

                                    BVnvisitante.Text = textBox1.Text;

                                    panel1.Visible = true;
                                    panel2.Visible = true;
                                    panel3.Visible = true;
                                    panel4.Visible = true;
                                    panel5.Visible = true;
                                    valorar = true;



                                }
                                else
                                {

                                    valorar = false;

                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al buscar: " + ex.Message);
                    }



                }

            }

            if (valorar == false)
            {

                MessageBox.Show("ERROR EN LOS DATOS");

            }


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
                miwebcam = null;

            }


        }


        private void Capturando(object sender, NewFrameEventArgs eventArgs)
        {

            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            pictureBox2.Image = Imagen;

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button39_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 1;
                    panel13.Visible = true;

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

                        BPfoto.Image = System.Drawing.Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }

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

        private void button38_Click(object sender, EventArgs e)
        {
            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            BPpassword.Text = strCadena;
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
                    textBox1.Clear();
                    textBox2.Clear();
                    panel1.Visible = false;

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);


                }

            }

        
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


                        conectar();
                        string sql = " UPDATE policias SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, foto = @foto, estado = @estado   where id_oficial = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", textBox1.Text);
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

                        String sujeto = textBox1.Text;
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
                            comando.Parameters.AddWithValue("@realizo", "registro");
                            comando.Parameters.AddWithValue("@tipo", "policia");

                            comando.ExecuteNonQuery();
                        }


                        MessageBox.Show("SE ACTUALIZO CORRECTAMENTE");
                        textBox1.Clear();
                        textBox2.Clear();
                        BPnpolicia.Clear();
                        BPnombre.Clear();
                        BPapellidop.Clear();
                        BPapellidom.Clear();
                        BPvigencia.Text = "";
                        BPcurp.Clear();
                        BPcorreo.Clear();
                        BPpassword.Clear();
                        BPfoto.Image = null;
                        BPinactivo.Checked = false;
                        BPactivo.Checked = false;
                        textBox2.Text = "";
                        textBox1.Text = "";
                        panel1.Visible = false;
                        panel1.Visible = false;

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }

            }
            catch
            {


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


                        conectar();
                        string sql = " UPDATE policias SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, estado = @estado   where id_oficial = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", textBox1.Text);
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
                        String sujeto = textBox1.Text;
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
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "policia");

                            comando.ExecuteNonQuery();
                        }

                        MessageBox.Show("SE ACTUALIZO CORRECTAMENTE");
                        BPnpolicia.Clear();
                        BPnombre.Clear();
                        BPapellidop.Clear();
                        BPapellidom.Clear();
                        BPvigencia.Text = "";
                        BPcurp.Clear();
                        BPcorreo.Clear();
                        BPpassword.Clear();
                        BPfoto.Image = null;
                        BPinactivo.Checked = false;
                        BPactivo.Checked = false;
                        textBox2.Text = "";
                        textBox1.Text = "";
                        panel1.Visible = false;

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }



            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {



        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            BApassword.Text = strCadena;

        }

        private void button5_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 2;
                    panel13.Visible = true;

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

                        BAfoto.Image = System.Drawing.Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }


        }

        private void button3_Click(object sender, EventArgs e)
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
                    BAcarrera.Text = "";
                    BAgrupo.Clear();
                    BPcorreo.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    panel1.Visible = false;

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);


                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                    BAvigencia.Text == "" || BAgrupo.Text == "" ||
                    BAcorreo.Text == "" || BApassword.Text == "" ||
                     BPactivo.Checked == false && BPinactivo.Checked == false || BAcarrera.Text == "")
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }


                int n;
                bool isNumeric = int.TryParse(BAmatricula.Text, out n);
                if (mensajeError == "" && isNumeric == false)
                {
                    MessageBox.Show("NUMERO DE EMPLEADO SOLO ACEPTA NUMEROS");
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

                        MessageBox.Show("SE ACTUALIZO CORRECTAMENTE");
                        textBox1.Clear();
                        textBox2.Clear();
                        BAmatricula.Clear();
                        BAnombre.Clear();
                        BAapellidop.Clear();
                        BAapellidom.Clear();
                        BAvigencia.Text = "";
                        BAcorreo.Clear();
                        BApassword.Clear();
                        BAcarrera.Text = "";
                        BAgrupo.Clear();
                        BAfoto.Image = null;
                        BAinactivo.Checked = false;
                        BAactivo.Checked = false;
                        panel1.Visible = false;
                        panel1.Visible = false;

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }

            }
            catch
            {


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

                        MessageBox.Show("SE ACTUALIZO CORRECTAMENTE");
                        textBox1.Clear();
                        textBox2.Clear();
                        BAmatricula.Clear();
                        BAnombre.Clear();
                        BAapellidop.Clear();
                        BAapellidom.Clear();
                        BAvigencia.Text = "";
                        BAcorreo.Clear();
                        BApassword.Clear();
                        BAcarrera.Text = "";
                        BAgrupo.Clear();
                        BAfoto.Image = null;
                        BAinactivo.Checked = false;
                        BAactivo.Checked = false;
                        panel1.Visible = false;
                        panel1.Visible = false;

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);

                    }
                }

            }

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 3;
                    panel13.Visible = true;

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

                        BDfoto.Image = System.Drawing.Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            String sujeto = BDnempleado.Text;
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
                    comandoDB.Parameters.AddWithValue("@id_docentes", BDnempleado.Text);
                    comandoDB.Parameters.AddWithValue("@nombre", BDnombre.Text);

                    comandoDB.CommandTimeout = 60;
                    MySqlDataReader reader;

                    reader = comandoDB.ExecuteReader();
                    reader.Close();
                    MessageBox.Show("SE ELIMINO CORRECTAMENTE");
                    BDnempleado.Clear();
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
                    textBox1.Clear();
                    textBox2.Clear();
                    panel1.Visible = false;

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);


                }

            }



        }

        private void button6_Click(object sender, EventArgs e)
        {

            try
            {


                MemoryStream ms = new MemoryStream();
                BDfoto.Image.Save(ms, ImageFormat.Jpeg);
                byte[] data = ms.ToArray();

                String ernesto = "0";
                String mensajeError = "0";

                if (BDnempleado.Text == "" || BDnombre.Text == "" ||
                    BDapellidop.Text == "" || BDapellidom.Text == "" ||
                    BDvigencia.Text == "" || BDcurp.Text == "" ||
                    BDcorreo.Text == "" || BDpassword.Text == "" ||
                     BDactivo.Checked == false && BDinactivo.Checked == false )
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }


                int n;
                bool isNumeric = int.TryParse(BDnempleado.Text, out n);
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

                        conectar();

                        string sql = " UPDATE docente SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, foto = @foto, estado = @estado   where id_docente = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", textBox1.Text);
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

                        String sujeto = textBox1.Text;
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
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "docente");

                            comando.ExecuteNonQuery();
                        }


                        MessageBox.Show("SE ACTUALIZO CORRECTAMENTE");
                        BDnempleado.Clear();
                        BDnombre.Clear();
                        BDapellidom.Clear();
                        BDapellidop.Clear();
                        BDcurp.Clear();
                        BDactivo.Checked = false;
                        BDinactivo.Checked = false;
                        BDvigencia.Text = "";
                        BDpassword.Clear();
                        BDcorreo.Clear();
                        textBox1.Clear();
                        textBox2.Clear();
                        panel1.Visible = false;

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }

            }
            catch
            {


                String ernesto = "0";
                String mensajeError = "0";

                if (BDnempleado.Text == "" || BDnombre.Text == "" ||
                    BDapellidop.Text == "" || BDapellidom.Text == "" ||
                    BDvigencia.Text == "" ||
                    BDcorreo.Text == "" || BDpassword.Text == "" ||
                     BDactivo.Checked == false && BDinactivo.Checked == false)
                {
                    MessageBox.Show("no puede quedar nada vacio");
                    mensajeError = "1";
                }


                int n;
                bool isNumeric = int.TryParse(BDnempleado.Text, out n);
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

                        conectar();
                        string sql = " UPDATE docente SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, estado = @estado   where id_docente = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", textBox1.Text);
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
                        String sujeto = textBox1.Text;
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
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "docente");

                            comando.ExecuteNonQuery();
                        }

                        MessageBox.Show("SE ACTUALIZO CORRECTAMENTE");
                        BDnempleado.Clear();
                        BDnombre.Clear();
                        BDapellidom.Clear();
                        BDapellidop.Clear();
                        BDcurp.Clear();
                        BDactivo.Checked = false;
                        BDinactivo.Checked = false;
                        BDvigencia.Text = "";
                        BDpassword.Clear();
                        BDcorreo.Clear();
                        textBox1.Clear();
                        textBox2.Clear();
                        panel1.Visible = false;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);

                    }
                }

            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            BDpassword.Text = strCadena;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            BADpassword.Text = strCadena;
        }

        private void button13_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 4;
                    panel13.Visible = true;

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

                        BADfoto.Image = System.Drawing.Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }
        }

        private void button11_Click(object sender, EventArgs e)
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

                    String jquery = " delete from administrativos where id_empleado = @id_empleado AND nombre = @nombre";
                    MySqlCommand comandoDB = new MySqlCommand(jquery, conectado);
                    comandoDB.Parameters.AddWithValue("@id_empleado", BADnempleado.Text);
                    comandoDB.Parameters.AddWithValue("@nombre", BDnombre.Text);

                    comandoDB.CommandTimeout = 60;
                    MySqlDataReader reader;

                    reader = comandoDB.ExecuteReader();
                    reader.Close();
                    MessageBox.Show("SE ELIMINO CORRECTAMENTE");
                    conectar();
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
                    BADniveles.Text = "";
                    BADcorreo.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    panel1.Visible = false;

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);


                }

            }
        }

        private void button10_Click(object sender, EventArgs e)
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
                    BADvigencia.Text == "" || BADcurp.Text == "" ||
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

                        conectar();


                        string sql = " UPDATE administrativos SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, foto = @foto, estado = @estado, nivel = @nivel   where id_empleado = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", textBox1.Text);
                            comando.Parameters.AddWithValue("@nombre", BADnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BADapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BADapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BADvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@curp", BADcurp.Text);
                            comando.Parameters.AddWithValue("@correo", BADcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BADpassword.Text);
                            comando.Parameters.AddWithValue("@foto", data);
                            comando.Parameters.AddWithValue("@nivel", BADniveles.Text);
                            comando.ExecuteNonQuery();

                        }

                        String sujeto = textBox1.Text;
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
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "administrativo");

                            comando.ExecuteNonQuery();
                        }


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
                        textBox1.Clear();
                        textBox2.Clear();
                        BADniveles.Text = "";
                        panel1.Visible = false;

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }

            }
            catch
            {


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

                        conectar();

                        string sql = " UPDATE administrativos SET  nombre = @nombre , apellidoP = @apellidoP , apellidoM = @apellidoM, curp = @curp, vigencia = @vigencia ,password = @password , correo = @correo, estado = @estado, nivel = @nivel   where id_empleado = @id ; ";
                        using (MySqlCommand comando = new MySqlCommand(sql, conectado))
                        {
                            comando.Parameters.AddWithValue("@id", textBox1.Text);
                            comando.Parameters.AddWithValue("@nombre", BADnombre.Text);
                            comando.Parameters.AddWithValue("@apellidoP", BADapellidop.Text);
                            comando.Parameters.AddWithValue("@apellidoM", BADapellidom.Text);
                            comando.Parameters.AddWithValue("@vigencia", BADvigencia.Text);
                            comando.Parameters.AddWithValue("@estado", ernesto);
                            comando.Parameters.AddWithValue("@curp", BADcurp.Text);
                            comando.Parameters.AddWithValue("@correo", BADcorreo.Text);
                            comando.Parameters.AddWithValue("@password", BADpassword.Text);
                            comando.Parameters.AddWithValue("@nivel", BADniveles.Text);
                            comando.ExecuteNonQuery();

                        }

                        String sujeto = textBox1.Text;
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
                            comando.Parameters.AddWithValue("@realizo", "modifico");
                            comando.Parameters.AddWithValue("@tipo", "administrativo");

                            comando.ExecuteNonQuery();
                        }

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
                        textBox1.Clear();
                        textBox2.Clear();
                        panel1.Visible = false;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);

                    }
                }

            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string strCadena = Guid.NewGuid().ToString().Substring(0, 10);

            BVpassword.Text = strCadena;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("TOMAR FOTO AHORITA?? ", "IMAGEN", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                try
                {
                    seximatil = 5;
                    panel13.Visible = true;

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

                        BPfoto.Image = System.Drawing.Image.FromFile(ofdseleccionar.FileName);

                    }
                }
                catch
                {

                    MessageBox.Show("Error al abrir");
                }


            }

        }

        private void button15_Click(object sender, EventArgs e)
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

                    String jquery = "delete from visitantes where id_visitante = @id_visitante AND nombre = @nombre";
                    MySqlCommand comandoDB = new MySqlCommand(jquery, conectado);
                    comandoDB.Parameters.AddWithValue("@id_docentes", BVnvisitante.Text);
                    comandoDB.Parameters.AddWithValue("@nombre", BVnombre.Text);

                    comandoDB.CommandTimeout = 60;
                    MySqlDataReader reader;

                    reader = comandoDB.ExecuteReader();
                    reader.Close();
                    MessageBox.Show("SE ELIMINO CORRECTAMENTE");

                    conectar();
                    BVnvisitante.Clear();
                    BVnombre.Clear();
                    BVapellidop.Clear();
                    BVapellidom.Clear();
                    BVvigencia.Text = "";
                    BVpassword.Clear();
                    BVactivo.Checked = false;
                    BVinactivo.Checked = false;
                    BVfoto.Image = null;
                    BVasunto.Text = "";
                    BVcorreo.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    panel1.Visible = false;

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);


                }

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {


            try
            {
                MemoryStream ms = new MemoryStream();
                BVfoto.Image.Save(ms, ImageFormat.Jpeg);
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
                            comando.Parameters.AddWithValue("@asunto", BVasunto.Text);
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
                        textBox2.Clear();
                        textBox1.Clear();
                        panel1.Visible = false;

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }
            }
            catch
            {


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
                            comando.Parameters.AddWithValue("@asunto", BVasunto.Text);
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
                        textBox1.Clear();
                        textBox2.Clear();
                        panel1.Visible = false;

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);


                    }
                }

            }

        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 re = new Form1();

            re.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           /* if(checkBox1.Checked = true){
                checkBox1.Checked = true;
            }*/
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

        private void tomarfoto_Click(object sender, EventArgs e)
        {
            if (miwebcam != null && miwebcam.IsRunning)
            {

                pictureBox3.Image = pictureBox2.Image;
                imagenAnterior = pictureBox2.Image;
            }
        }

        private void SUBIRFOTO_Click(object sender, EventArgs e)
        {
            if (seximatil == 1) {

                BPfoto.Image = pictureBox3.Image;

            }
            else if (seximatil == 2) { 
            
                BAfoto.Image = pictureBox3.Image;

            }
            else if (seximatil == 3)
            {

                BDfoto.Image = pictureBox3.Image;

            }
            else if(seximatil == 4)
            {

                BADfoto.Image= pictureBox3.Image;

            }
            else if (seximatil == 5)
            {

                BPfoto.Image = pictureBox3.Image;

            }
            cerrar();
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            panel13.Visible = false;

        }

        bool vai = false;
        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            vai = true;
        }

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        {
            if (vai == true)
            {
                this.Location = Cursor.Position;
            }
        }

        private void panel6_MouseUp(object sender, MouseEventArgs e)
        {
            vai = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button42_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 re = new Form1();

            re.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private Image AplicarEfectoCircular(Image imagenOriginal)
        {
            Bitmap bmp = new Bitmap(imagenOriginal.Width, imagenOriginal.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, bmp.Width, bmp.Height);
                    g.SetClip(path);
                    g.DrawImage(imagenOriginal, Point.Empty);
                }
            }

            return bmp;
        }

        private void editarFoto_Click(object sender, EventArgs e)
        {
            if (imagenAnterior != null)
            {
                // Redimensiona la imagen al tamaño deseado antes de pasarla a FormRecorte
                Image imagenRedimensionada = RedimensionarImagen(imagenAnterior, 467, 400);

                using (FormRecorte formRecorte = new FormRecorte(imagenRedimensionada))
                {
                    if (formRecorte.ShowDialog() == DialogResult.OK)
                    {
                        Image imagenRecortada = formRecorte.ImagenRecortada;
                        pictureBox3.Image = AplicarEfectoCircular(imagenRecortada);
                    }
                }
            }
        }

        private Image RedimensionarImagen(Image imagen, int nuevoAncho, int nuevoAlto)
        {
            Bitmap bmp = new Bitmap(nuevoAncho, nuevoAlto);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(imagen, new Rectangle(0, 0, nuevoAncho, nuevoAlto));
            }

            return bmp;
        }
    }
    
}
