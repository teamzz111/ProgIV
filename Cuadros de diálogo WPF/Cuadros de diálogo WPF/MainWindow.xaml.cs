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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cuadros_de_diálogo_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Electiva Profesional IV");
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            //MessageBox con botones
            MessageBox.Show("Quiere mostrar.\n\nBotones adicionales?", "Sistema",
           MessageBoxButton.YesNoCancel);
            
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            //MessageBox con ícono
            MessageBox.Show("Electiva Profesional IV", "Sistema", MessageBoxButton.OK,
           MessageBoxImage.Information);
            
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            //MessageBox con título
            MessageBox.Show("Electiva Profesional IV", "Sistema");
        }
    }
}
