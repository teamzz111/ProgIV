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
using Microsoft.Win32;
using System.IO;

namespace Cuadros_de_diálogo_WPF
{
    /// <summary>
    /// Lógica de interacción para openfiledialog.xaml
    /// </summary>
    public partial class openfiledialog : Page
    {
        public openfiledialog()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //Los filtros para los tipos de archivos
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            //El directorio inicial
            openFileDialog.InitialDirectory = @"c:\";
            if (openFileDialog.ShowDialog() == true)
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }

        private void button_Click2(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //Los filtros para los tipos de archivos
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            //El directorio inicial
            saveFileDialog.InitialDirectory = @"c:\";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
        }
    }
}
