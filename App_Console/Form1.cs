using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;
using MySqlConnector;
using System.Drawing.Drawing2D;

namespace App_Console
{
    public partial class Form1 : Form
    {

        MySqlConnection conectado;

        public Form1()
        {
            InitializeComponent();
            ver.BringToFront();
            this.Text=string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

        }



        public void conectar()
        {
            try
            {
                //String conexion = "server=35.230.47.124;  uid=root; password = 'tesoem1' ;database = tuzos";
                //String conexion = "Server=knowmanbd1.mysql.database.azure.com;UserID =tuzos1;Password=Tesoem1+;Database=tuzos;SslMode=Required;";
                string conexion = "Server=82.197.95.106;Port=3306;Database=virtualteso;UserID=virtualteso;Password=tesoem23+;";
                conectado = new MySqlConnection(conexion);
                conectado.Open();
               // MessageBox.Show("conectado");
                 // conectado.Close();
            }
            catch(Exception x)
            {
                MessageBox.Show("error al conectarse a la BD" + x);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }




         private void button1_Click(object sender, EventArgs e)
        {
            Boolean mary = false;


            conectar();
            String jqquery = " select password,nombre,apellidoP,nivel from administrativos where id_empleado =" + matricula.Text + ";";
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
                        //MessageBox.Show(reader.GetString(0));

                        string password = qreader.GetString(0);
                        String nombre = qreader.GetString(1);
                        String apellido = qreader.GetString(2);
                        StringBuilder nivel = new StringBuilder();
                        //var coco =qreader.GetString(4);
                        int usuario1 = Convert.ToInt32(matricula.Text);

                        String wusuario = matricula.Text;
                        String wpassword = tpassword.Text;


                        if (wpassword == password && qreader.GetString(3) == "SUPER ADMINISTRADOR (NIVEL.1)")
                        {

                            this.Hide();

                            administración sexi = new administración(nombre, apellido, 1, usuario1);

                            sexi.Show();

                            mary = true;

                            matricula.Text = " ";
                            tpassword.Text = " ";

                        }
                        if (wpassword == password && qreader.GetString(3) == "COORDINADOR (NIVEL.2)")
                        {
                            this.Hide();

                            administración sexi = new administración(nombre, apellido, 2, usuario1);

                            sexi.Show();

                            mary = true;

                            matricula.Text = " ";
                            tpassword.Text = " ";

                        }
                        if (wpassword == password && qreader.GetString(3) == "EDITOR (NIVEL.3)")
                        {

                            //MessageBox.Show("ESTAMOS TRABAJANDO EN ELLO");

                            this.Hide();

                            Form5 sexi = new Form5(nombre, apellido, "3", usuario1);

                            sexi.Show();

                            mary = true;

                            matricula.Text = " ";
                            tpassword.Text = " ";



                            //matricula.Text = " ";
                            //tpassword.Text = " ";

                        }
                        if (wpassword == password && qreader.GetString(3) == "USUARIO ESTANDAR (NIVEL.4)")
                        {

                            // MessageBox.Show("ESTAMOS TRABAJANDO EN ELLO");

                            this.Hide();

                            Form5 sexi = new Form5(nombre, apellido, "4", usuario1);

                            sexi.Show();

                            mary = true;

                            mary = true;

                            matricula.Text = " ";
                            tpassword.Text = " ";

                            //matricula.Text = " ";
                            //tpassword.Text = " ";

                        }
                        if (wpassword == password && qreader.GetString(3) == "USUARIO PASIVO (NIVEL 5)" || wpassword == password && qreader.GetString(3) == "USUARIO PASIVO (NIVEL.5)")
                        {


                            this.Hide();


                            Form4 form4 = new Form4(nombre, apellido);

                            form4.Show();

                            mary = true;

                            matricula.Text = " ";
                            tpassword.Text = " ";



                            //matricula.Text = " ";
                            //tpassword.Text = " ";

                        }
                        else
                        {


                            //MessageBox.Show("ERROR EN LOS DATOS");

                        }



                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }



            if (mary == false)
            {
                conectar();
         

            String jquery = " select password from alumno where matricula =  " + matricula.Text + ";";
            MySqlCommand comandoDB = new MySqlCommand(jquery, conectado);
            comandoDB.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {

                reader = comandoDB.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        //MessageBox.Show(reader.GetString(0));

                        string password = reader.GetString(0);



                        String wusuario = matricula.Text;
                        String wpassword = tpassword.Text;


                        if (wpassword == password)
                        {

                            MessageBox.Show("NO TIENES EL PERMISO PARA ACCEDER");

                            mary = true;

                        }
                        else
                        {

                            //MessageBox.Show("ERROR EN LOS DATOS");

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }




            if (mary == false)
            {
                conectar();
                String jjquery = " select password from docente where id_docente =" + matricula.Text + ";";
                MySqlCommand ccomandoDB = new MySqlCommand(jjquery, conectado);
                ccomandoDB.CommandTimeout = 60;
                MySqlDataReader Dreader;

                try
                {

                    Dreader = ccomandoDB.ExecuteReader();
                    if (Dreader.HasRows)
                    {

                        while (Dreader.Read())
                        {
                            //MessageBox.Show(reader.GetString(0));

                            string password = Dreader.GetString(0);



                            String wusuario = matricula.Text;
                            String wpassword = tpassword.Text;


                            if (wpassword == password)
                            {


                                //this.Hide();


                                //Form2 sexi = new Form2(nombre, apellido);

                                //sexi.Show();

                                MessageBox.Show("NO TIENES EL PERMISO PARA ACCEDER");

                                mary = true;


                                //matricula.Text = " ";
                                //tpassword.Text = " ";

                            }
                            else
                            {


                                //MessageBox.Show("ERROR EN LOS DATOS");

                            }



                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }




            if (mary == false)
            {
                conectar();
                String Xjquery = " select password from policias where id_oficial ="+matricula.Text+";";
                MySqlCommand XcomandoDB = new MySqlCommand(Xjquery, conectado);
                XcomandoDB.CommandTimeout = 60;
                MySqlDataReader Xreader;

                try
                {

                    Xreader = XcomandoDB.ExecuteReader();
                    if (Xreader.HasRows)
                    {

                        while (Xreader.Read())
                        {
                            //MessageBox.Show(reader.GetString(0));
                          
                            string password = Xreader.GetString(0);

                        

                            String wusuario = matricula.Text;
                            String wpassword = tpassword.Text;


                            if (wpassword == password)
                            {

                                MessageBox.Show("NO TIENES EL PERMISO PARA ACCEDER");


                                mary = true;

                            }
                            else
                            {


                                //MessageBox.Show("ERROR EN LOS DATOS");

                            }



                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }

            if (mary == false)
            {
                conectar();
                String vjquery = " select password from visitantes where id_visitante ="+matricula.Text+";";
                MySqlCommand vcomandoDB = new MySqlCommand(vjquery, conectado);
                vcomandoDB.CommandTimeout = 60;
                MySqlDataReader vreader;

                try
                {

                    vreader = vcomandoDB.ExecuteReader();
                    if (vreader.HasRows)
                    {

                        while (vreader.Read())
                        {
                          
                            string password = vreader.GetString(0);

                  
                            String wusuario = matricula.Text;
                            String wpassword = tpassword.Text;


                            if ( wpassword == password)
                            {

                                MessageBox.Show("NO TIENES PERMISO PARA ACCEDER");
                                mary = true;


                            }
                            else
                            {


                                //MessageBox.Show("ERROR EN LOS DATOS");

                            }



                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }


            if (mary == false) {

                MessageBox.Show("USUARIO Y/O CONTRASEÑA INCORRECTA, VERIFIQUE SU INFORMACIÓN");
            
            }


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            this.Hide();

            recuperar re = new recuperar();

            re.Show();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ver.BringToFront();
            tpassword.PasswordChar = '*';

        }

        private void ver_Click(object sender, EventArgs e)
        {

            no.BringToFront();
            tpassword.PasswordChar = '\0';

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, this.Height),
                Color.White,  // Color del inicio (arriba)
                Color.Green   // Color del final (abajo)
            );
            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
        }

        private void button1_Paint(object sender, PaintEventArgs e)
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
    }
}
