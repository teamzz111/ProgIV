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
using System.Windows;
using System.Windows.Controls;

using Microsoft.Win32;

using WebEye.Controls.Wpf;
namespace Dashboard1
{
    /// <summary>
    /// Lógica de interacción para Foto.xaml
    /// </summary>
    public partial class Foto : Window
    {
        String id;
        public Foto(String id)
        {
            this.id = id;
            InitializeComponent();
            InitializeComboBox();
        }
        private void InitializeComboBox()
        {
            comboBox.ItemsSource = webCameraControl.GetVideoCaptureDevices();

            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedItem = comboBox.Items[0];
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startButton.IsEnabled = e.AddedItems.Count > 0;
        }

        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            var cameraId = (WebCameraId)comboBox.SelectedItem;
            webCameraControl.StartCapture(cameraId);
        }

        private void OnStopButtonClick(object sender, RoutedEventArgs e)
        {
            webCameraControl.StopCapture();
        }

        private void OnImageButtonClick(object sender, RoutedEventArgs e)
        {
            /*  var dialog = new SaveFileDialog { Filter = "Bitmap Image|*.bmp" };
              if (dialog.ShowDialog() == true)
              {*/
            // MessageBox.Show(dialog.FileName);
            try
            {
                String cadena = "C:\\Users\\AndresLargo\\Documents\\GitHub\\ProgIV\\Proyecto0.1\\Dashboard1\\users\\" + id + ".bmp";
                webCameraControl.GetCurrentImage().Save(cadena);
                MessageBox.Show("Guardado exitoso");

            } catch (Exception)
            {
                MessageBox.Show("Operación fallida, intente más tarde");
            }
            finally
            {
                this.Hide(); 
            }
          //  }
        }
    }
}
