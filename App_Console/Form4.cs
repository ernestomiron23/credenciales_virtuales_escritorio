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


namespace App_Console
{
    public partial class Form4 : Form
    {



        MySqlConnection conectado;
        public Form4(String nombre,String apellido)
        {
            InitializeComponent();
            label1.Text = nombre + apellido;


        }



        public void conectar()
        {

            try
            {

                string conexion = "Server=82.197.95.106;Port=3306;Database=virtualteso;UserID=virtualteso;Password=tesoem23+;";
                //String conexion = "Server=knowmanbd1.mysql.database.azure.com;UserID =tuzos1;Password=Tesoem1+;Database=tuzos;SslMode=Required;";
                conectado = new MySqlConnection(conexion);
                conectado.Open();


            }
            catch (Exception x)
            {

                MessageBox.Show("error al conectarse a la BD" + x);




            }


        }


        private void avisos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button41_Click(object sender, EventArgs e)
        {
            StringBuilder para = new StringBuilder();

            if (checkBox1.Checked == true)
            {


                para.AppendLine("docente");
            }
            if (checkBox2.Checked == true)
            {


                para.AppendLine("administativos");
            }
            if (checkBox3.Checked == true)
            {


                para.AppendLine("alumnos");
            }
            if (checkBox4.Checked == true)
            {


                para.AppendLine("policias");
            }
            if (checkBox5.Checked == true)
            {


                para.AppendLine("visitas");
            }


            String chicharon = (String)para.ToString();


            conectar();

            string sql = "INSERT INTO aviso (id_aviso, texto, fecha, dirigido, titulo) VALUES ( @id_aviso, @texto, @fecha, @dirigido, @titulo)";
            using (MySqlCommand comando = new MySqlCommand(sql, conectado))
            {
                comando.Parameters.AddWithValue("@id_aviso", 0);
                comando.Parameters.AddWithValue("@texto", textBox2.Text);
                comando.Parameters.AddWithValue("@fecha", DateTime.Now);
                comando.Parameters.AddWithValue("@dirigido", chicharon);
                comando.Parameters.AddWithValue("@titulo", textBox1.Text);

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
            else
            {

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 re = new Form1();

            re.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button42_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 re = new Form1();

            re.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


        bool vai = false;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            vai = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (vai == true)
            {
                this.Location = Cursor.Position;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            vai = false;
        }

        private void label116_Click(object sender, EventArgs e)
        {

        }
    }
}
