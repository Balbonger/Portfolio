using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Windows.Forms;
using ZXing;

namespace NibsInventoryManagementSystem
{
    public partial class Scanner : Form
    {

        public static string Scanned;
        public Scanner()
        {
            InitializeComponent();
            checkOutput();
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.Stop();
                }
            }

            this.Close();
        }

        public void checkOutput()
        {
            if(textBox1.Text == "")
            {
                scanner_addId.Enabled = false;
            }
            else
            {
                scanner_addId.Enabled = true;
            }
        }


        public Point mouseLocation;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);

                Location = mousePose;
            }
        }

        private void Scanner_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Form1.appC;
            scanner_addId.BackColor = Form1.appC;

            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
                comboBox_camera.Items.Add(device.Name);
            comboBox_camera.SelectedIndex = 0;

        }

        public void startScan()
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox_camera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();

        }

        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);

            pictureBox1.Image = bitmap;


            if (result != null)
            {

                textBox1.Invoke(new MethodInvoker(delegate ()
                {

                    Scanned = result.ToString();

                    textBox1.Text = Scanned;

                    scanner_addId.Enabled = true;
                    
                }));

                

            }

        }

        private void scanner_addId_Click(object sender, EventArgs e)
        {
            AdminAddProducts prod = new AdminAddProducts();

            prod.Show();
            prod.inputScan();
            this.Close();

        }

        private void Scanner_FormClosed(object sender, FormClosedEventArgs e)
        {
       
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.Stop();


                }
            }
            
        }
    }
}
