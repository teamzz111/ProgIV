using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data.SqlClient;
using System.Windows.Shapes;
using System.Data;

namespace Dashboard1
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Window
    {
        public Usuarios()
        {
            InitializeComponent();
        }
        Funciones f = new Funciones();

        private void TextBox5_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox5.Text = "";
        }

        private void TextBox4_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox4.Text = "";
        }

        private void TextBox3_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox3.Text = "";
        }

       
        private void TextBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "")
            {
                textBox.Text = "Nombre";
            }
        }

        private void TextBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Apellido";
            }
        }

        private void TextBox3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Dirección";
            }
        }

        private void TextBox4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Teléfono";
            }
        }

        private void TextBox5_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "E-mail";
            }
        }

        private void TextBox6_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox6.Text = "";
        }

        private void TextBox6_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Cédula";
            }
        }

        private void TextBox3_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                SqlCommand comandodb;
                if (f.conexion.State == ConnectionState.Closed)
                    f.conexion.Open();
                comandodb = new SqlCommand("insertarEmpleado", f.conexion);
                comandodb.CommandType = CommandType.StoredProcedure;
                comandodb.Parameters.AddWithValue("@Id", textBox6.Text);
                comandodb.Parameters.AddWithValue("@nombre", textBox.Text);
                comandodb.Parameters.AddWithValue("@apellido", textBox1.Text);
                comandodb.Parameters.AddWithValue("@direccion", textBox3.Text);
                comandodb.Parameters.AddWithValue("@telefono", textBox4.Text);
                comandodb.Parameters.AddWithValue("@email", textBox5.Text);
                if (radioButton1.IsChecked == true)
                    comandodb.Parameters.AddWithValue("@tipo", "Visitante");
                else
                    comandodb.Parameters.AddWithValue("@tipo", "Empleado");
                comandodb.Parameters.Add("@msj", SqlDbType.VarChar, 60).Direction=ParameterDirection.Output;
                comandodb.ExecuteNonQuery();
                MessageBox.Show(comandodb.Parameters["@msj"].Value + "");
                if (f.conexion.State == ConnectionState.Open)
                {
                    f.conexion.Close();
                }
                else
                {
                    MessageBox.Show("Faltan datos", "Información");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Información");
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand comandodb;
                if (f.conexion.State == ConnectionState.Closed)
                    f.conexion.Open();
                comandodb = new SqlCommand("ModificarEmpleado", f.conexion);
                comandodb.CommandType = CommandType.StoredProcedure;
                comandodb.Parameters.AddWithValue("@Id", textBox6.Text);
                comandodb.Parameters.AddWithValue("@nombre", textBox.Text);
                comandodb.Parameters.AddWithValue("@apellido", textBox1.Text);
                comandodb.Parameters.AddWithValue("@direccion", textBox3.Text);
                comandodb.Parameters.AddWithValue("@telefono", textBox4.Text);
                comandodb.Parameters.AddWithValue("@email", textBox5.Text);
                comandodb.Parameters.Add("@msj", SqlDbType.VarChar, 60).Direction
               =
                ParameterDirection.Output;
                comandodb.ExecuteNonQuery();
                MessageBox.Show(comandodb.Parameters["@msj"].Value + "");
                if (f.conexion.State == ConnectionState.Open)
                {
                    f.conexion.Close();
                }
                else
                {
                    MessageBox.Show("Faltan datos", "Información");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Información");
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Huella h = new Huella();
            h.Show();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand comandodb;
                if (f.conexion.State == ConnectionState.Closed)
                    f.conexion.Open();
                comandodb = new SqlCommand("ConsultarEmpleado", f.conexion);
                comandodb.CommandType = CommandType.StoredProcedure;
                comandodb.Parameters.AddWithValue("@Id", textBox6.Text);
                SqlDataReader reader = comandodb.ExecuteReader();
                while (reader.Read())
                {
                    textBox6.Text = "as";
                    textBox.Text = reader.GetString(1);
                    textBox1.Text = reader.GetString(2);
                    textBox3.Text = reader.GetString(3);
                    textBox4.Text = reader.GetInt32(4).ToString();
                    textBox5.Text = reader.GetString(5);
                    reader.Close();
                    comandodb.ExecuteNonQuery();
                    
                }



                if (f.conexion.State == ConnectionState.Open)
                {
                    f.conexion.Close();
                }
                else
                {
                    MessageBox.Show("Faltan datos", "Información");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Información");
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Foto f = new Foto();
            f.Show();
        }
    }
}
