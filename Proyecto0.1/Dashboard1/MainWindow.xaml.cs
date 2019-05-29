using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            setUp();
        }
        Funciones f = new Funciones();
        private void setUp()
        {
            try
            {
                
                SqlCommand comandodb;
                if (f.conexion.State == ConnectionState.Closed)
                    f.conexion.Open();
                comandodb = new SqlCommand("ConsultarCantidadVisitas", f.conexion);
                comandodb.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = comandodb.ExecuteReader();
                while (reader.Read())
                {
                    label3.Content = reader.GetInt32(0).ToString();
                    reader.Close();
                    comandodb.ExecuteNonQuery();
                    break;

                }

                comandodb = new SqlCommand("ConsultarCantidadUsuarios", f.conexion);
                comandodb.CommandType = CommandType.StoredProcedure;
                reader = comandodb.ExecuteReader();
                while (reader.Read())
                {
                    label1.Content = reader.GetInt32(0).ToString();
                    reader.Close();
                    comandodb.ExecuteNonQuery();
                    break;

                }
                comandodb = new SqlCommand("ConsultarCantidadVisitantes", f.conexion);
                comandodb.CommandType = CommandType.StoredProcedure;
                reader = comandodb.ExecuteReader();
                while (reader.Read())
                {
                    label2.Content = reader.GetInt32(0).ToString();
                    reader.Close();
                    comandodb.ExecuteNonQuery();
                    break;

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
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Switch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Usuarios u = new Usuarios();
            u.Show();
            this.Hide();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Visistantes u = new Visistantes();
            u.Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Ingreso p = new Ingreso();
            p.Show();
            this.Hide();
        }
    }


}
