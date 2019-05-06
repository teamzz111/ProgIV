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
using System.Windows.Shapes;

namespace Cuadros_de_diálogo_WPF
{
    /// <summary>
    /// Lógica de interacción para cuadrod.xaml
    /// </summary>
    public partial class cuadrod : Window
    {
        public cuadrod(string question, string defaultAnswer = "")
        {
            InitializeComponent();
            lblPregunta.Content = question;
            txtRespuesta.Text = defaultAnswer;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void Window_ContentRendered(object sender, RoutedEventArgs e)
        {
            txtRespuesta.SelectAll();
            txtRespuesta.Focus();
        }
        public string Answer
        {
            get { return txtRespuesta.Text; }
        }
    }
}
