using System;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace SS_OpenCV
{ 
    public partial class MainForm : Form
    {
        Image<Bgr, Byte> img = null; // working image
        Image<Bgr, Byte> imgUndo = null; // undo backup image - UNDO
        string title_bak = "";

        public MainForm()
        {
            InitializeComponent();
            title_bak = Text;
        }

        /// <summary>
        /// Opens a new image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(openFileDialog1.FileName);
                Text = title_bak + " [" +
                        openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\") + 1) +
                        "]";
                imgUndo = img.Copy();
                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh();
            }
        }

        /// <summary>
        /// Saves an image with a new name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageViewer.Image.Save(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// restore last undo copy of the working image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgUndo == null) // verify if the image is already opened
                return; 
            Cursor = Cursors.WaitCursor;
            img = imgUndo.Copy();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        /// <summary>
        /// Change visualization mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zoom
            if (autoZoomToolStripMenuItem.Checked)
            {
                ImageViewer.SizeMode = PictureBoxSizeMode.Zoom;
                ImageViewer.Dock = DockStyle.Fill;
            }
            else // with scroll bars
            {
                ImageViewer.Dock = DockStyle.None;
                ImageViewer.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        /// <summary>
        /// Show authors form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthorsForm form = new AuthorsForm();
            form.ShowDialog();
        }

        /// <summary>
        /// Calculate the image negative
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Negative(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        /// <summary>
        /// Call automated image processing check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void evalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvalForm eval = new EvalForm();
            eval.ShowDialog();
        }

        /// <summary>
        /// Call image convertion to gray scale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToGray(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void redChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.RedChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void brightContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form_bright = new InputBox("Brightness?");
            form_bright.ShowDialog();

            int bright = Convert.ToInt32(form_bright.ValueTextBox.Text);

            InputBox form_contrast = new InputBox("Contrast?");
            form_contrast.ShowDialog();

            double contrast = Convert.ToDouble(form_contrast.ValueTextBox.Text);

            ImageClass.BrightContrast(img, bright, contrast);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void translationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form_dx = new InputBox("Dx?");
            form_dx.ShowDialog();

            int dx = Convert.ToInt32(form_dx.ValueTextBox.Text);

            InputBox form_dy = new InputBox("Dy?");
            form_dy.ShowDialog();

            int dy = Convert.ToInt32(form_dx.ValueTextBox.Text);

            ImageClass.Translation(img, imgUndo, dx, dy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void rotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("Angle?");
            form.ShowDialog();

            float angle = (float)Convert.ToDouble(form.ValueTextBox.Text);

            ImageClass.Rotation(img, imgUndo, angle);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        int mouseX, mouseY;
        bool mouseFlag = false;

        private void ImageViewer_MouseClick(object sender, MouseEventArgs e)
        {
            if(mouseFlag)
            {
                mouseX = e.X;
                mouseY = e.Y;

                mouseFlag = false;
            }
        }

        private void nonUniformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            undoToolStripMenuItem_Click(null, null);

            float matrixWeight = 0;
            float matrix00;
            float matrix01;
            float matrix02;
            float matrix10;
            float matrix11;
            float matrix12;
            float matrix20;
            float matrix21;
            float matrix22;

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            Image<Bgr, byte> imgCpy = img.Copy();

            Prompt prompt = new Prompt("Non Uniform Filter");
            prompt.ShowDialog();

            do
            {
                matrix00 = (float)Convert.ToDouble(prompt.valueTextBox1.Text);
                matrix01 = (float)Convert.ToDouble(prompt.valueTextBox2.Text);
                matrix02 = (float)Convert.ToDouble(prompt.valueTextBox3.Text);
                matrix10 = (float)Convert.ToDouble(prompt.valueTextBox4.Text);
                matrix11 = (float)Convert.ToDouble(prompt.valueTextBox5.Text);
                matrix12 = (float)Convert.ToDouble(prompt.valueTextBox6.Text);
                matrix20 = (float)Convert.ToDouble(prompt.valueTextBox7.Text);
                matrix21 = (float)Convert.ToDouble(prompt.valueTextBox8.Text);
                matrix22 = (float)Convert.ToDouble(prompt.valueTextBox9.Text);

            } while (String.IsNullOrEmpty(prompt.valueTextBox1.Text) ||
                     String.IsNullOrEmpty(prompt.valueTextBox2.Text) ||
                     String.IsNullOrEmpty(prompt.valueTextBox3.Text) ||
                     String.IsNullOrEmpty(prompt.valueTextBox4.Text) ||
                     String.IsNullOrEmpty(prompt.valueTextBox5.Text) ||
                     String.IsNullOrEmpty(prompt.valueTextBox6.Text) ||
                     String.IsNullOrEmpty(prompt.valueTextBox7.Text) ||
                     String.IsNullOrEmpty(prompt.valueTextBox8.Text) ||
                     String.IsNullOrEmpty(prompt.valueTextBox9.Text));

            float[,] matrix = new float[,] { { matrix00, matrix01, matrix02 },
                                             { matrix10, matrix11, matrix12 },
                                             { matrix20, matrix21, matrix22 } };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrixWeight += matrix[i, j];
                }
            }
            if (matrixWeight == 0)
                matrixWeight = 1;

            Console.WriteLine(matrixWeight);
            ImageClass.NonUniform(img, imgCpy, matrix, matrixWeight);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
            
        }

        private void greenChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.GreenChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void blueChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.BlueChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> imgCopy = null; // copy Image

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            //copy Image
            imgCopy = img.Copy();

            ImageClass.Mean(img, imgCopy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> imgCopy = null; // copy Image

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            //copy Image
            imgCopy = img.Copy();

            ImageClass.Sobel(img, imgCopy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void diferentiationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> imgCopy = null; // copy Image

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            //copy Image
            imgCopy = img.Copy();

            ImageClass.Diferentiation(img, imgCopy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> imgCopy = null; // copy Image

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            //copy Image
            imgCopy = img.Copy();

            ImageClass.Median(img, imgCopy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void grayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            /*
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            Image<Bgr, byte> imgCpy = img.Copy();

            Histogram histogram = new Histogram(ImageClass.Histogram_Gray(img));
            histogram.ShowDialog();*/
        }

        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {/*
            
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            HistogramLines histogramLines = new HistogramLines(ImageClass.Histogram_RGB(img), 3);
            histogramLines.ShowDialog(); */
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {/*
           
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            HistogramLines histogramLines = new HistogramLines(ImageClass.Histogram_All(img), 4);
            histogramLines.ShowDialog();
            */
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> imgCopy = null; // copy Image

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            //copy Image
            imgCopy = img.Copy();

            int[][] projections;
            int[] vertical_projections, horizontal_projections;

            projections = ImageClass.Segmentation(img);
            vertical_projections = projections[0];
            horizontal_projections = projections[1];

            var angle = ImageClass.EixoMomento(img);

            if(angle != 0)
            {
                ImageClass.Rotation_Bilinear(img, imgCopy, (float)angle);
            }

            projections = ImageClass.Segmentation(img);
            vertical_projections = projections[0];
            horizontal_projections = projections[1];
            var barcode_dimensions = ImageClass.BarcodeDimensions(vertical_projections, horizontal_projections);

            ImageClass.LocateBarcode(imgCopy, vertical_projections, horizontal_projections, angle, barcode_dimensions[0], barcode_dimensions[1]);

            ImageViewer.Image = imgCopy.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 

        }

        private void dilatationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> imgCopy = null; // copy Image

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            //copy Image
            imgCopy = img.Copy();

            ImageClass.Dilatation(img, imgCopy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void convertBWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> imgCopy = null; // copy Image

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            //copy Image
            imgCopy = img.Copy();

            ImageClass.ConvertToBW_Otsu(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> imgCopy = null; // copy Image

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            //copy Image
            imgCopy = img.Copy();

            ImageClass.Erosion(img, imgCopy);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("Scale factor?");
            form.ShowDialog();

            float scaleFactor = (float)Convert.ToDouble(form.ValueTextBox.Text);

            mouseFlag = true;

            while (mouseFlag)
                Application.DoEvents();

            ImageClass.Scale_point_xy(img, imgUndo, scaleFactor, mouseX, mouseY);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }
    }




}