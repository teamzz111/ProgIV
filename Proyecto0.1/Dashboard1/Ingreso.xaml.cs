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
    /// Lógica de interacción para Ingreso.xaml
    /// </summary>
    public partial class Ingreso : Window
    {
        public Ingreso()
        {
            InitializeComponent();
        }
        Funciones f = new Funciones();
        private void button_Click(object sender, RoutedEventArgs e)
        {
            string estado="a",tipo="a";
            try
            {
                SqlCommand comandodb;
                if (f.conexion.State == ConnectionState.Closed)
                    f.conexion.Open();
                comandodb = new SqlCommand("ConsultarEmpleado", f.conexion);
                comandodb.CommandType = CommandType.StoredProcedure;
                comandodb.Parameters.AddWithValue("@Id", textBox.Text);
                SqlDataReader reader = comandodb.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(6)=="fuera")
                    { label.Content = "Bienvenido" + reader.GetString(1); }
                    else if (reader.GetString(6)=="inactivo")
                    { label.Content = "Lo sentimos, no tienes acceso"; }
                    else
                    { label.Content = "Hasta pronto" + reader.GetString(1); }
                    estado = reader.GetString(6);
                
                }
                reader.Close();
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


            try
            {
                SqlCommand comandodb;
                if (f.conexion.State == ConnectionState.Closed)
                    f.conexion.Open();
                comandodb = new SqlCommand("ModificarEmpleadoEstado", f.conexion);
                comandodb.CommandType = CommandType.StoredProcedure;
                comandodb.Parameters.AddWithValue("@Id", textBox.Text);
                if(estado=="fuera")
                    comandodb.Parameters.AddWithValue("@estado", "dentro");
                else if(tipo=="Empleado")
                    comandodb.Parameters.AddWithValue("@estado", "fuera");
                else
                    comandodb.Parameters.AddWithValue("@estado", "inactivo");
                comandodb.Parameters.AddWithValue("@msj", "Éxito");
                comandodb.ExecuteNonQuery();
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
    }
}
