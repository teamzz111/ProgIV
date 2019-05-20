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
using System.Windows.Shapes;

namespace Dashboard1
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class LoginAdmin : Window
    {
        public LoginAdmin()
        {
            InitializeComponent();
        }
        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost;
Initial Catalog=dbo; Integrated Security=True;");
        private void button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String consulta = "SELECT COUNT(1) FROM Login WHERE User=@Username AND pass = @Password";
                 SqlCommand sqlCmd = new SqlCommand(consulta, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", user.Text);
                sqlCmd.Parameters.AddWithValue("@Password", pass.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                MessageBox.Show(count.ToString());
                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o password incorrecto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }


    
}
