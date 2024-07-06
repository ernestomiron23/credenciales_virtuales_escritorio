using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
//using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;
using MySqlConnector;
using System.Drawing.Drawing2D;

namespace App_Console
{
    public partial class recuperar : Form
    {
        MySqlConnection conectado;
        const string Usuario = "tesoemcredenciales@gmail.com";
        const string cPassword = " uegs knlx nerk okud";

        public recuperar()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
   

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        public void conectar()
        {

            try
            {
                //String conexion = "Server=knowmanbd1.mysql.database.azure.com;UserID =tuzos1;Password=Tesoem1+;Database=tuzos;SslMode=Required;";
                string conexion = "Server=82.197.95.106;Port=3306;Database=virtualteso;UserID=virtualteso;Password=tesoem23+;";
                conectado = new MySqlConnection(conexion);
                conectado.Open();
                //MessageBox.Show("conectado");
                //  conectado.Close();


            }
            catch (Exception x)
            {

                MessageBox.Show("error al conectarse a la BD" + x);




            }


        }



        public static void EnviarCorreo(StringBuilder Mensaje , DateTime Fechaenvio ,string De, string Para , string Asunto , out string Error  ) 
        { 
            
            Error = "";
            try
            {

                Mensaje.Append(Environment.NewLine);
                Mensaje.Append(string.Format("Este correo ha sido enviado el dia {0:dd/MM/yyyy} a las {0:HH:mm:ss} Hrs: \n \n", Fechaenvio));   
                Mensaje.Append(Environment.NewLine);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(De);
                mail.To.Add(Para);
                mail.Subject = Asunto;  
                mail.Body = Mensaje.ToString();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(Usuario,cPassword);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                Error = "EXITO";
                MessageBox.Show(Error);
               // MessageBox.Show("CORREO ENVIADO");


    
               //this.Hide();

                //Form1 INICIO = new Form1();

                //INICIO.Show();

                
                

            }
            catch (Exception x) {

                Error = "Error: " + x.Message;
                MessageBox.Show(Error);
                return;      
            
            }
        } 




        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();


            Form1 INICIO = new Form1();

            INICIO.Show();




        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {





        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();


            Form1 INICIO = new Form1();

            INICIO.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void correo_TextChanged(object sender, EventArgs e)
        {

        }

        private void recuperar_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void recuperar_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, this.Height),
                Color.White,  // Color del inicio (arriba)
                Color.Green   // Color del final (abajo)
            );
            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
        }

        public void button1_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;

            // Crear un degradado lineal
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(btn.Width, 0),
                Color.Green,        // Color del inicio (izquierda)
                Color.GreenYellow   // Color del final (derecha)
            );

            // Crear un rectángulo redondo para el botón
            GraphicsPath path = new GraphicsPath();
            int radioBorde = 20; // Puedes ajustar el radio según tus preferencias
            path.AddArc(0, 0, radioBorde, radioBorde, 180, 90);
            path.AddArc(btn.Width - radioBorde, 0, radioBorde, radioBorde, 270, 90);
            path.AddArc(btn.Width - radioBorde, btn.Height - radioBorde, radioBorde, radioBorde, 0, 90);
            path.AddArc(0, btn.Height - radioBorde, radioBorde, radioBorde, 90, 90);

            // Rellenar el botón con el degradado
            e.Graphics.FillPath(gradientBrush, path);

            // Dibujar el borde del botón
            using (Pen pen = new Pen(Color.GreenYellow, 2))
            {
                e.Graphics.DrawPath(pen, path);
            }

            // Asegurarse de que el texto sea visible y centrado
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            // Puedes cambiar la fuente y el color según tus preferencias
            using (Font font = new Font("Arial", 12, FontStyle.Bold))
            {
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.DrawString("Acceder", font, brush, btn.ClientRectangle, sf);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
      
               Boolean mary = false;
             

                conectar();
                String jqquery = " select password,correo from administrativos where id_empleado =" + txtusuario.Text + ";";
                MySqlCommand qcomandoDB = new MySqlCommand(jqquery, conectado);
                qcomandoDB.CommandTimeout = 60;
                MySqlDataReader qreader;

                try
                {

                    qreader = qcomandoDB.ExecuteReader();
                    if (qreader.HasRows)
                    {

                        while (qreader.Read())
                        {
                           
                            string password = qreader.GetString(0);
                            String ctr = qreader.GetString(1);
                                                    
                            int usuario1 = Convert.ToInt32(txtusuario.Text);                                                      

                            if (ctr == correo.Text)
                            {

                                 string error = "";
                                 StringBuilder mensajebuilder = new StringBuilder();
                                 mensajebuilder.AppendLine(" tu password es : " + password);

                                EnviarCorreo(mensajebuilder, DateTime.Now, Usuario.Trim(), correo.Text.Trim(), correo.Text.Trim(), out error);

                            //    MessageBox.Show("EXITO");

                                this.Hide();

                                Form1 sexi = new Form1();

                                sexi.Show();

                                mary = true;
                                



                            }
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            if (mary == false) {

                MessageBox.Show("ERROR EN LOS DATOS");

            }
       
        }
    }
}
