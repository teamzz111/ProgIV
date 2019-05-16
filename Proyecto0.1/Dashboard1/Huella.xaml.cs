using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Data;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms.Integration;

namespace Dashboard1
{
    public delegate void Functionz();
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Huella : Window, DPFP.Capture.EventHandler
    {
        public int PROBABILITY_ONE = 0x7FFFFFFF;
        bool registrationInProgress = false;
        int fingerCount = 0;
        public byte[] huella = null;
        System.Drawing.Graphics graphics;
        System.Drawing.Font font;
        DPFP.Capture.ReadersCollection readers;
        DPFP.Capture.ReaderDescription readerDescription;
        DPFP.Capture.Capture capturer;
        DPFP.Template template;
        DPFP.FeatureSet[] regFeatures;
        DPFP.FeatureSet verFeatures;
        DPFP.Processing.Enrollment createRegTemplate;
        DPFP.Verification.Verification verify;
        DPFP.Capture.SampleConversion converter;
        String huellabits = String.Empty;
        Funciones f = new Funciones();
            
        public Huella()
        {
            InitializeComponent();
            graphics = this.pbImage.CreateGraphics();
            DPFP.Capture.ReadersCollection coll = new DPFP.Capture.ReadersCollection();
            regFeatures = new DPFP.FeatureSet[4];
            for (int i = 0; i < 4; i++)
                regFeatures[i] = new DPFP.FeatureSet();
            verFeatures = new DPFP.FeatureSet();
            createRegTemplate = new DPFP.Processing.Enrollment();
            readers = new DPFP.Capture.ReadersCollection();
            for (int i = 0; i < readers.Count; i++)
            {
                readerDescription = readers[i];
                if ((readerDescription.Vendor == "Digital Persona, Inc.") || (readerDescription.Vendor
               == "DigitalPersona, Inc."))
                {
                    try
                    {
                        capturer = new DPFP.Capture.Capture(readerDescription.SerialNumber,
                       DPFP.Capture.Priority.Normal);//CREAMOS UNA OPERACION DE CAPTURAS.
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                    capturer.EventHandler = this;
                    //AQUI CAPTURAMOS LOS EVENTOS.
                    converter = new DPFP.Capture.SampleConversion();
                    try
                    {
                        verify = new DPFP.Verification.Verification();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Ex: " + ex.ToString());
                    }
                    break;
                }
            }
        }

        private void User_GotFocus(object sender, RoutedEventArgs e)
        {
            tbInfo.Text = "";

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand comandodb;
                if (f.conexion.State == ConnectionState.Closed)
                    f.conexion.Open();
                comandodb = new SqlCommand("insertarHuella", f.conexion);
                comandodb.CommandType = CommandType.StoredProcedure;
                comandodb.Parameters.AddWithValue("@huella", huella);
                comandodb.Parameters.AddWithValue("@nombres", textBox1.Text);
                comandodb.Parameters.Add("@msj", SqlDbType.VarChar, 60).Direction = ParameterDirection.Output;
                comandodb.ExecuteNonQuery();
               System.Windows.Forms.MessageBox.Show(comandodb.Parameters["@msj"].Value + "");
                if (f.conexion.State == ConnectionState.Open)
                {
                    f.conexion.Close();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Faltan datos", "Información",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message, "Información", MessageBoxButtons.OK,
               MessageBoxIcon.Exclamation);
            }
        }

        public void OnComplete(object obj, string info, DPFP.Sample sample)
        {
            this.pbImage.Invoke(new Functionz(delegate ()
            {
                tbInfo.Text = "Captura Completa";
            }));
            this.pbImage.Invoke(new Functionz(delegate ()
            {
                Bitmap tempRef = null;
                converter.ConvertToPicture(sample, ref tempRef);
                System.Drawing.Image img = tempRef;

                Bitmap bmp = new Bitmap(converter.ConvertToPicture(sample, ref tempRef), pbImage.Size);
                String pxFormat = bmp.PixelFormat.ToString();
                System.Drawing.Point txtLoc = new System.Drawing.Point(pbImage.Width / 2 - 20, 0);
                graphics = Graphics.FromImage(bmp);

                if (registrationInProgress)
                {
                    try
                    {

                        regFeatures[fingerCount] = ExtractFeatures(sample, DPFP.Processing.DataPurpose.Enrollment);
                        if (regFeatures[fingerCount] != null)
                        {
                            string b64 =
                           Convert.ToBase64String(regFeatures[fingerCount].Bytes);

                            regFeatures[fingerCount].DeSerialize(Convert.FromBase64String(b64));
                            if (regFeatures[fingerCount] == null)
                            {
                                txtLoc.X = pbImage.Width / 2 - 26;
                                graphics.DrawString("Bad Press", font,
                               System.Drawing.Brushes.Cyan, txtLoc);
                                return;
                            }
                            ++fingerCount;
                            createRegTemplate.AddFeatures(regFeatures[fingerCount
                            - 1]);
                            graphics = Graphics.FromImage(bmp);
                            if (fingerCount < 4)
                                graphics.DrawString("" + fingerCount + " De 4",
                               font, System.Drawing.Brushes.Black, txtLoc);
                            if (createRegTemplate.TemplateStatus ==
                           DPFP.Processing.Enrollment.Status.Failed)
                            {
                                capturer.StopCapture();
                                fingerCount = 0;
                                System.Windows.Forms.MessageBox.Show("Registration Failed, \nMake sure you use the same finger for all 4 presses.");
                            }
                            else
                            if (createRegTemplate.TemplateStatus ==
                           DPFP.Processing.Enrollment.Status.Ready)
                            {
                                string mensaje = "";
                                MemoryStream x = new MemoryStream();
                                MemoryStream mem = new MemoryStream();
                                template = createRegTemplate.Template;
                                template.Serialize(mem);
                                verFeatures = ExtractFeatures(sample,
                                DPFP.Processing.DataPurpose.Verification);
                                mensaje = "";
                                //comparar(verFeatures);
                                if (mensaje == "Ya Existe un Empleado Con LaHuella Capturada")
                                {
                                    System.Windows.Forms.MessageBox.Show(mensaje, "Seguridad NuevaEra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                    capturer.StopCapture();
                                    this.Close();
                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show("Captura Completa",
                                   "Seguridad Nueva Era", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    huella = mem.GetBuffer();
                                    capturer.StopCapture();
                                }
                            }
                        }
                    }
                    catch (DPFP.Error.SDKException ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    DPFP.Verification.Verification.Result rslt = new
                   DPFP.Verification.Verification.Result();
                    verFeatures = ExtractFeatures(sample,
                   DPFP.Processing.DataPurpose.Verification);
                    verify.Verify(verFeatures, template, ref rslt);
                    txtLoc.X = pbImage.Width / 2 - 38;
                    if (rslt.Verified == true)
                        graphics.DrawString("Match!!!!", font,
                       System.Drawing.Brushes.LightGreen, txtLoc);
                    else graphics.DrawString("No Match!!!", font, System.Drawing.Brushes.Red,
                   txtLoc);
                }
                pbImage.Image = bmp;
            }));
        }
        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            try
            {
                this.pbImage.Invoke(new Functionz(delegate ()
                {
                    tbInfo.Text = "Esperando...";
                }));
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message, "Seguridad Nueva Era",
               MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            try
            {
                this.pbImage.Invoke(new Functionz(delegate ()
                {
                    tbInfo.Text = "Leyendo huella";
                }));
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message, "Seguridad Nueva Era",
               MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            try
            {
                this.pbImage.Invoke(new Functionz(delegate ()
                {
                    tbInfo.Text = "Lector Conectado";
                }));
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message, "Seguridad Nueva Era",
               MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            try
            {
                this.pbImage.Invoke(new Functionz(delegate ()
                {
                    tbInfo.Text = "Lector Desconectado"; System.Windows.Forms.MessageBox.Show("readercount: " + readers.Count);
                }));
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message, "Seguridad Nueva Era",
               MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void OnSampleQuality(object Capture, string ReaderSerialNumber,
       DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            System.Windows.Forms.MessageBox.Show("Sample quality!!!! " + CaptureFeedback.ToString());
        }
        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample,
       DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new
           DPFP.Processing.FeatureExtraction(); // Create a feature extractor
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            try
            {
                Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref
               features);
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            MaskedTextBox mtbDate = new MaskedTextBox("00/00/0000");

            // Assign the MaskedTextBox control as the host control's child.
            host.Child = mtbDate;

            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.grid1.Children.Add(host);
            registrationInProgress = true;
            fingerCount = 0;
            createRegTemplate.Clear();
            if (capturer != null)
            {
                try
                {
                    capturer.StartCapture();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
