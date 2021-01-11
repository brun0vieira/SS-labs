using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using ZedGraph;
using System.Xml;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Emgu.CV.CvEnum;
using System.Diagnostics;
using System.Drawing;

namespace SS_OpenCV
{
    class ImageClass
    {

        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        public static void NegativeEmguCV(Image<Bgr, byte> img)
        {
            int x, y;

            Bgr aux;
            for (y = 0; y < img.Height; y++)
            {
                for (x = 0; x < img.Width; x++)
                {
                    // acesso directo : mais lento 
                    aux = img[y, x];
                    img[y, x] = new Bgr(255 - aux.Blue, 255 - aux.Green, 255 - aux.Red);
                }
            }
        }

        /// <summary>
        /// Image Negative 
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">Image</param>
        public static void Negative(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhaments bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {

                            //retrive 3 colour components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            //invert colors and store in the image
                            dataPtr[0] = (byte)(255 - blue);
                            dataPtr[1] = (byte)(255 - green);
                            dataPtr[2] = (byte)(255 - red);

                            //advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        /// <summary>
        /// Convert to gray
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //retrive 3 colour components
                            //although its rgb, in memory its saved as blue, green and red
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        /// <summary>
        /// Red Channel
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // retrieve the red color component
                            red = dataPtr[2];

                            // change the blue and green components to red
                            dataPtr[0] = red;
                            dataPtr[1] = red;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        /// <summary>
        /// Green Channel
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void GreenChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte green;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // retrieve the green color component
                            green = dataPtr[1];

                            // change the blue and red components to green
                            dataPtr[0] = green;
                            dataPtr[2] = green;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        /// <summary>
        /// Blue Channel
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void BlueChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // retrieve the blue color component
                            blue = dataPtr[0];

                            // change the red and green components to blue
                            dataPtr[1] = blue;
                            dataPtr[2] = blue;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        /// <summary>
        /// Changes the brightness and contrast
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;
                double blue_updated, green_updated, red_updated;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                double[] aux = new double[3];

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // retrieve the 3 components
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            blue_updated = blue * contrast + bright;
                            green_updated = green * contrast + bright;
                            red_updated = red * contrast + bright;

                            if (blue_updated > 255)
                            {
                                blue_updated = 255;
                            }
                            else if (blue_updated < 0)
                            {
                                blue_updated = 0;
                            }

                            if (green_updated > 255)
                            {
                                green_updated = 255;
                            }
                            else if (green_updated < 0)
                            {
                                green_updated = 0;
                            }

                            if (red_updated > 255)
                            {
                                red_updated = 255;
                            }
                            else if (red_updated < 0)
                            {
                                red_updated = 0;
                            }

                            dataPtr[0] = (byte)Math.Round(blue_updated);
                            dataPtr[1] = (byte)Math.Round(green_updated);
                            dataPtr[2] = (byte)Math.Round(red_updated);

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
        {
            unsafe
            {
                // obter apontador do inicio da imagem
                MIplImage m = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                MIplImage m_dest = img.MIplImage;
                byte* dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                int width = imgCopy.Width;
                int height = imgCopy.Height;
                int nChannels = m.nChannels;
                int widthstep = m.widthStep;
                byte blue, green, red;
                int y_orig, x_orig;
                int padding = m.widthStep - m.nChannels * m.width;

                if (nChannels == 3) // RGB
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            x_orig = x - dx;
                            y_orig = y - dy;

                            // calcula endereço do pixel no ponto (x,y)
                            blue = (byte)(dataPtr + y_orig * widthstep + x_orig * nChannels)[0];
                            green = (byte)(dataPtr + y_orig * widthstep + x_orig * nChannels)[1];
                            red = (byte)(dataPtr + y_orig * widthstep + x_orig * nChannels)[2];

                            // verifica os limites
                            if (x_orig < 0 || y_orig < 0 || x_orig >= width || y_orig >= height)
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }

                            dataPtr_dest[0] = blue;
                            dataPtr_dest[1] = green;
                            dataPtr_dest[2] = red;

                            dataPtr_dest += nChannels;

                        }
                        dataPtr_dest += padding;
                    }
                }
            }
        }


        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {
                // obter apontador do inicio da imagem
                MIplImage m = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                MIplImage m_dest = img.MIplImage;
                byte* dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                int width = imgCopy.Width;
                int height = imgCopy.Height;
                int nChannels = m.nChannels;
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int x_orig, y_orig;
                double sin = Math.Sin(angle), cos = Math.Cos(angle);
                double half_width = width / 2.0;
                double half_height = height / 2.0;

                if (nChannels == 3) // RGB   
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            x_orig = (int)Math.Round((x - half_width) * cos - (half_height - y) * sin + half_width);
                            y_orig = (int)Math.Round(half_height - (x - half_width) * sin - (half_height - y) * cos);

                            if (x_orig < 0 || y_orig < 0 || x_orig >= width || y_orig >= height)
                            {
                                dataPtr_dest[0] = 0;
                                dataPtr_dest[1] = 0;
                                dataPtr_dest[2] = 0;
                            }
                            else
                            {
                                dataPtr_dest[0] = (byte)(dataPtr + y_orig * widthstep + x_orig * nChannels)[0];
                                dataPtr_dest[1] = (byte)(dataPtr + y_orig * widthstep + x_orig * nChannels)[1];
                                dataPtr_dest[2] = (byte)(dataPtr + y_orig * widthstep + x_orig * nChannels)[2];
                            }

                            dataPtr_dest += nChannels;
                        }
                        dataPtr_dest += padding;
                    }
                }

            }
        }

        public static void Scale(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {
            unsafe
            {

                MIplImage m = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                MIplImage m_dest = img.MIplImage;
                byte* dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                int width = imgCopy.Width;
                int height = imgCopy.Height;
                int nChannels = m.nChannels;
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int x_orig, y_orig;

                if (nChannels == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            x_orig = (int)Math.Round(x / scaleFactor);
                            y_orig = (int)Math.Round(y / scaleFactor);

                            if (x_orig < 0 || y_orig < 0 || x_orig >= width || y_orig >= height)
                            {
                                dataPtr_dest[0] = 0;
                                dataPtr_dest[1] = 0;
                                dataPtr_dest[2] = 0;
                            }
                            else
                            {
                                dataPtr_dest[0] = (byte)(dataPtr + y_orig * widthstep + x_orig * nChannels)[0];
                                dataPtr_dest[1] = (byte)(dataPtr + y_orig * widthstep + x_orig * nChannels)[1];
                                dataPtr_dest[2] = (byte)(dataPtr + y_orig * widthstep + x_orig * nChannels)[2];
                            }
                            dataPtr_dest += nChannels;
                        }
                        dataPtr_dest += padding;
                    }
                }

            }
        }

        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                MIplImage m_dest = imgCopy.MIplImage;
                byte* dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                int width = img.Width; // value in pixels without padding
                int height = img.Height; // value in pixels without padding
                int nC = m.nChannels; // number of channels - 3 -> channels are rgb (red,green,blue)
                int widthstep = m.widthStep;
                int padding = widthstep - nC * width;

                int y_dest, x_dest, x1, y1; //x1 e y1 são as coordenadas da imagem original
                x1 = y1 = 0;


                if (nC == 3)
                {
                    for (y_dest = 0; y_dest < height; y_dest++)
                    {
                        y1 = (int)Math.Round((y_dest / scaleFactor) + centerY - ((height / 2) / scaleFactor));

                        for (x_dest = 0; x_dest < width; x_dest++)
                        {

                            x1 = (int)Math.Round((x_dest / scaleFactor) + centerX - ((width / 2) / scaleFactor));

                            //verifying image limits
                            if (x1 < 0 || y1 < 0 || x1 >= width || y1 >= height)
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;
                            }

                            else
                            {
                                dataPtr[0] = (byte)(dataPtr_dest + y1 * widthstep + x1 * nC)[0];
                                dataPtr[1] = (byte)(dataPtr_dest + y1 * widthstep + x1 * nC)[1];
                                dataPtr[2] = (byte)(dataPtr_dest + y1 * widthstep + x1 * nC)[2];
                            }

                            dataPtr += nC;

                        }
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                MIplImage m_dest = imgCopy.MIplImage;
                byte* dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                int width = img.Width; // value in pixels without padding
                int height = img.Height; // value in pixels without padding
                int nC = m.nChannels; // number of channels - 3 -> channels are rgb (red,green,blue)
                int widthstep = m.widthStep; //complete line
                int padding = widthstep - nC * width;

                int x, y;

                double blue, green, red;

                dataPtr += +widthstep + nC;
                dataPtr_dest += +widthstep + nC;

                if (nC == 3)
                {
                    for (y = 1; y < (height - 1); y++) // all but margins
                    {

                        for (x = 1; x < (width - 1); x++)
                        {

                            blue = ((dataPtr_dest - nC)[0] + (dataPtr_dest + nC)[0] + (dataPtr_dest - widthstep)[0] + (dataPtr_dest + widthstep)[0] + (dataPtr_dest - widthstep - nC)[0] + (dataPtr_dest - widthstep + nC)[0] + (dataPtr_dest + widthstep - nC)[0] + (dataPtr_dest + widthstep + nC)[0] + dataPtr_dest[0]) / 9.0;
                            green = ((dataPtr_dest - nC)[1] + (dataPtr_dest + nC)[1] + (dataPtr_dest - widthstep)[1] + (dataPtr_dest + widthstep)[1] + (dataPtr_dest - widthstep - nC)[1] + (dataPtr_dest - widthstep + nC)[1] + (dataPtr_dest + widthstep - nC)[1] + (dataPtr_dest + widthstep + nC)[1] + dataPtr_dest[1]) / 9.0;
                            red = ((dataPtr_dest - nC)[2] + (dataPtr_dest + nC)[2] + (dataPtr_dest - widthstep)[2] + (dataPtr_dest + widthstep)[2] + (dataPtr_dest - widthstep - nC)[2] + (dataPtr_dest - widthstep + nC)[2] + (dataPtr_dest + widthstep - nC)[2] + (dataPtr_dest + widthstep + nC)[2] + dataPtr_dest[2]) / 9.0;


                            dataPtr[0] = (byte)Math.Round(blue);
                            dataPtr[1] = (byte)Math.Round(green);
                            dataPtr[2] = (byte)Math.Round(red);


                            dataPtr_dest += nC;
                            dataPtr += nC;

                        }
                        dataPtr_dest += padding + 2 * nC;
                        dataPtr += padding + 2 * nC;
                    }

                    // x=0 , y=0 primeiro pixel
                    dataPtr = (byte*)m.imageData.ToPointer();
                    dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                    blue = (dataPtr_dest[0] * 4 + (dataPtr_dest + nC)[0] * 2 + (dataPtr_dest + widthstep)[0] * 2 + (dataPtr_dest + widthstep + nC)[0]) / 9.0;
                    green = (dataPtr_dest[1] * 4 + (dataPtr_dest + nC)[1] * 2 + (dataPtr_dest + widthstep)[1] * 2 + (dataPtr_dest + widthstep + nC)[1]) / 9.0;
                    red = (dataPtr_dest[2] * 4 + (dataPtr_dest + nC)[2] * 2 + (dataPtr_dest + widthstep)[2] * 2 + (dataPtr_dest + widthstep + nC)[2]) / 9.0;

                    dataPtr[0] = (byte)Math.Round(blue);
                    dataPtr[1] = (byte)Math.Round(green);
                    dataPtr[2] = (byte)Math.Round(red);


                    // x=1,2,3... , y=0 linha de cima
                    dataPtr += nC;
                    dataPtr_dest += nC;

                    for (x = 1; x < (width - 1); x++)
                    {

                        blue = (dataPtr_dest[0] * 2 + (dataPtr_dest + nC)[0] * 2 + (dataPtr_dest - nC)[0] * 2 + (dataPtr_dest + widthstep)[0] + (dataPtr_dest + widthstep + nC)[0] + (dataPtr_dest + widthstep - nC)[0]) / 9.0;
                        green = (dataPtr_dest[1] * 2 + (dataPtr_dest + nC)[1] * 2 + (dataPtr_dest - nC)[1] * 2 + (dataPtr_dest + widthstep)[1] + (dataPtr_dest + widthstep + nC)[1] + (dataPtr_dest + widthstep - nC)[1]) / 9.0;
                        red = (dataPtr_dest[2] * 2 + (dataPtr_dest + nC)[2] * 2 + (dataPtr_dest - nC)[2] * 2 + (dataPtr_dest + widthstep)[2] + (dataPtr_dest + widthstep + nC)[2] + (dataPtr_dest + widthstep - nC)[2]) / 9.0;

                        dataPtr[0] = (byte)Math.Round(blue);
                        dataPtr[1] = (byte)Math.Round(green);
                        dataPtr[2] = (byte)Math.Round(red);

                        dataPtr += nC;
                        dataPtr_dest += nC;

                    }


                    ////// x=n , y=0 ultimo pixel da 1a linha
                    blue = (dataPtr_dest[0] * 4 + (dataPtr_dest - nC)[0] * 2 + (dataPtr_dest + widthstep)[0] * 2 + (dataPtr_dest + widthstep - nC)[0]) / 9.0;
                    green = (dataPtr_dest[1] * 4 + (dataPtr_dest - nC)[1] * 2 + (dataPtr_dest + widthstep)[1] * 2 + (dataPtr_dest + widthstep - nC)[1]) / 9.0;
                    red = (dataPtr_dest[2] * 4 + (dataPtr_dest - nC)[2] * 2 + (dataPtr_dest + widthstep)[2] * 2 + (dataPtr_dest + widthstep - nC)[2]) / 9.0;

                    dataPtr[0] = (byte)Math.Round(blue);
                    dataPtr[1] = (byte)Math.Round(green);
                    dataPtr[2] = (byte)Math.Round(red);



                    //// x=n , y=1,2,3... margem da direita
                    dataPtr += widthstep;
                    dataPtr_dest += widthstep;

                    for (y = 1; y < (height - 1); y++)
                    {
                        blue = (dataPtr_dest[0] * 2 + (dataPtr_dest - widthstep)[0] * 2 + (dataPtr_dest + widthstep)[0] * 2 + (dataPtr_dest - nC)[0] + (dataPtr_dest - widthstep - nC)[0] + (dataPtr_dest + widthstep - nC)[0]) / 9.0;
                        green = (dataPtr_dest[1] * 2 + (dataPtr_dest - widthstep)[1] * 2 + (dataPtr_dest + widthstep)[1] * 2 + (dataPtr_dest - nC)[1] + (dataPtr_dest - widthstep - nC)[1] + (dataPtr_dest + widthstep - nC)[1]) / 9.0;
                        red = (dataPtr_dest[2] * 2 + (dataPtr_dest - widthstep)[2] * 2 + (dataPtr_dest + widthstep)[2] * 2 + (dataPtr_dest - nC)[2] + (dataPtr_dest - widthstep - nC)[2] + (dataPtr_dest + widthstep - nC)[2]) / 9.0;

                        dataPtr[0] = (byte)Math.Round(blue);
                        dataPtr[1] = (byte)Math.Round(green);
                        dataPtr[2] = (byte)Math.Round(red);


                        dataPtr += widthstep;
                        dataPtr_dest += widthstep;

                    }


                    //// x=n , y=n ultimo pixel direita em baixo
                    blue = (dataPtr_dest[0] * 4 + (dataPtr_dest - widthstep)[0] * 2 + (dataPtr_dest - nC)[0] * 2 + (dataPtr_dest - widthstep - nC)[0]) / 9.0;
                    green = (dataPtr_dest[1] * 4 + (dataPtr_dest - widthstep)[1] * 2 + (dataPtr_dest - nC)[1] * 2 + (dataPtr_dest - widthstep - nC)[1]) / 9.0;
                    red = (dataPtr_dest[2] * 4 + (dataPtr_dest - widthstep)[2] * 2 + (dataPtr_dest - nC)[2] * 2 + (dataPtr_dest - widthstep - nC)[2]) / 9.0;

                    dataPtr[0] = (byte)Math.Round(blue);
                    dataPtr[1] = (byte)Math.Round(green);
                    dataPtr[2] = (byte)Math.Round(red);
                    //

                    //// x= 1,2,3... , y=n margem de baixo
                    dataPtr -= nC;
                    dataPtr_dest -= nC;

                    for (x = 1; x < (width - 1); x++)
                    {
                        blue = (dataPtr_dest[0] * 2 + (dataPtr_dest + nC)[0] * 2 + (dataPtr_dest - nC)[0] * 2 + (dataPtr_dest - widthstep)[0] + (dataPtr_dest - widthstep + nC)[0] + (dataPtr_dest - widthstep - nC)[0]) / 9.0;
                        green = (dataPtr_dest[1] * 2 + (dataPtr_dest + nC)[1] * 2 + (dataPtr_dest - nC)[1] * 2 + (dataPtr_dest - widthstep)[1] + (dataPtr_dest - widthstep + nC)[1] + (dataPtr_dest - widthstep - nC)[1]) / 9.0;
                        red = (dataPtr_dest[2] * 2 + (dataPtr_dest + nC)[2] * 2 + (dataPtr_dest - nC)[2] * 2 + (dataPtr_dest - widthstep)[2] + (dataPtr_dest - widthstep + nC)[2] + (dataPtr_dest - widthstep - nC)[2]) / 9.0;

                        dataPtr[0] = (byte)Math.Round(blue);
                        dataPtr[1] = (byte)Math.Round(green);
                        dataPtr[2] = (byte)Math.Round(red);

                        dataPtr -= nC;
                        dataPtr_dest -= nC;

                    }
                    //

                    // x=0 , y=n ultimo pixel em baixo esquerda
                    blue = (dataPtr_dest[0] * 4 + (dataPtr_dest + nC)[0] * 2 + (dataPtr_dest - widthstep)[0] * 2 + (dataPtr_dest - widthstep + nC)[0]) / 9.0;
                    green = (dataPtr_dest[1] * 4 + (dataPtr_dest + nC)[1] * 2 + (dataPtr_dest - widthstep)[1] * 2 + (dataPtr_dest - widthstep + nC)[1]) / 9.0;
                    red = (dataPtr_dest[2] * 4 + (dataPtr_dest + nC)[2] * 2 + (dataPtr_dest - widthstep)[2] * 2 + (dataPtr_dest - widthstep + nC)[2]) / 9.0;

                    dataPtr[0] = (byte)Math.Round(blue);
                    dataPtr[1] = (byte)Math.Round(green);
                    dataPtr[2] = (byte)Math.Round(red);
                    //

                    //x=0 , y=1,2,3... coluna lado esq
                    dataPtr -= widthstep;
                    dataPtr_dest -= widthstep;

                    for (y = 1; y < (height - 1); y++)
                    {
                        blue = (dataPtr_dest[0] * 2 + (dataPtr_dest + widthstep)[0] * 2 + (dataPtr_dest - widthstep)[0] * 2 + (dataPtr_dest + nC)[0] + (dataPtr_dest + widthstep + nC)[0] + (dataPtr_dest - widthstep + nC)[0]) / 9.0;
                        green = (dataPtr_dest[1] * 2 + (dataPtr_dest + widthstep)[1] * 2 + (dataPtr_dest - widthstep)[1] * 2 + (dataPtr_dest + nC)[1] + (dataPtr_dest + widthstep + nC)[1] + (dataPtr_dest - widthstep + nC)[1]) / 9.0;
                        red = (dataPtr_dest[2] * 2 + (dataPtr_dest + widthstep)[2] * 2 + (dataPtr_dest - widthstep)[2] * 2 + (dataPtr_dest + nC)[2] + (dataPtr_dest + widthstep + nC)[2] + (dataPtr_dest - widthstep + nC)[2]) / 9.0;


                        dataPtr[0] = (byte)Math.Round(blue);
                        dataPtr[1] = (byte)Math.Round(green);
                        dataPtr[2] = (byte)Math.Round(red);

                        dataPtr -= widthstep;
                        dataPtr_dest -= widthstep;

                    }
                }
            }
        }

        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                MIplImage m_dest = imgCopy.MIplImage;
                byte* dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nC * width;

                double blue, red, green;
                int x, y;

                if (nC == 3)
                {
                    // without borders
                    dataPtr += widthstep + nC;
                    dataPtr_dest += widthstep + nC;

                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 1; x < width - 1; x++)
                        {
                            blue = ((dataPtr_dest - widthstep - nC)[0] * matrix[0, 0] + (dataPtr_dest - widthstep)[0] * matrix[0, 1] + (dataPtr_dest - widthstep + nC)[0] * matrix[0, 2] + (dataPtr_dest - nC)[0] * matrix[1, 0] + dataPtr_dest[0] * matrix[1, 1] + (dataPtr + nC)[0] * matrix[1, 2] + (dataPtr_dest + widthstep - nC)[0] * matrix[2, 0] + (dataPtr_dest + widthstep)[0] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[0] * matrix[2, 2]) / matrixWeight;
                            green = ((dataPtr_dest - widthstep - nC)[1] * matrix[0, 0] + (dataPtr_dest - widthstep)[1] * matrix[0, 1] + (dataPtr_dest - widthstep + nC)[1] * matrix[0, 2] + (dataPtr_dest - nC)[1] * matrix[1, 0] + dataPtr_dest[1] * matrix[1, 1] + (dataPtr + nC)[1] * matrix[1, 2] + (dataPtr_dest + widthstep - nC)[1] * matrix[2, 0] + (dataPtr_dest + widthstep)[1] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[1] * matrix[2, 2]) / matrixWeight;
                            red = ((dataPtr_dest - widthstep - nC)[2] * matrix[0, 0] + (dataPtr_dest - widthstep)[2] * matrix[0, 1] + (dataPtr_dest - widthstep + nC)[2] * matrix[0, 2] + (dataPtr_dest - nC)[2] * matrix[1, 0] + dataPtr_dest[2] * matrix[1, 1] + (dataPtr + nC)[2] * matrix[1, 2] + (dataPtr_dest + widthstep - nC)[2] * matrix[2, 0] + (dataPtr_dest + widthstep)[2] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[2] * matrix[2, 2]) / matrixWeight;

                            dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                            dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                            dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                            dataPtr += nC;
                            dataPtr_dest += nC;
                        }

                        dataPtr += padding + 2 * nC;
                        dataPtr_dest += padding + 2 * nC;

                    }

                    // inicio da imagem
                    dataPtr = (byte*)m.imageData.ToPointer();
                    dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                    // canto superior esquerdo
                    blue = (dataPtr_dest[0] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) + (dataPtr_dest + nC)[0] * (matrix[0, 2] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[0] * matrix[2, 0] + (dataPtr_dest + widthstep)[0] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[0] * matrix[2, 2]) / matrixWeight;
                    green = (dataPtr_dest[1] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) + (dataPtr_dest + nC)[1] * (matrix[0, 2] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[1] * matrix[2, 0] + (dataPtr_dest + widthstep)[1] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[1] * matrix[2, 2]) / matrixWeight;
                    red = (dataPtr_dest[2] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) + (dataPtr_dest + nC)[2] * (matrix[0, 2] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[2] * matrix[2, 0] + (dataPtr_dest + widthstep)[2] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[2] * matrix[2, 2]) / matrixWeight;

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    dataPtr += nC;
                    dataPtr_dest += nC;

                    // 1ª linha (sem cantos)
                    for (x = 1; x < width - 1; x++)
                    {
                        blue = ((dataPtr_dest - nC)[0] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_dest[0] * (matrix[0, 1] + matrix[1, 1]) + (dataPtr_dest + nC)[0] * (matrix[1, 2] + matrix[0, 2]) + (dataPtr_dest + widthstep - nC)[0] * matrix[2, 0] + (dataPtr_dest + widthstep)[0] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[0] * matrix[2, 2]) / matrixWeight;
                        green = ((dataPtr_dest - nC)[1] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_dest[1] * (matrix[0, 1] + matrix[1, 1]) + (dataPtr_dest + nC)[1] * (matrix[1, 2] + matrix[0, 2]) + (dataPtr_dest + widthstep - nC)[1] * matrix[2, 0] + (dataPtr_dest + widthstep)[1] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[1] * matrix[2, 2]) / matrixWeight;
                        red = ((dataPtr_dest - nC)[2] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_dest[2] * (matrix[0, 1] + matrix[1, 1]) + (dataPtr_dest + nC)[2] * (matrix[1, 2] + matrix[0, 2]) + (dataPtr_dest + widthstep - nC)[2] * matrix[2, 0] + (dataPtr_dest + widthstep)[2] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[2] * matrix[2, 2]) / matrixWeight;

                        dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                        dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                        dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                        dataPtr += nC;
                        dataPtr_dest += nC;
                    }

                    // canto superior direito
                    blue = ((dataPtr_dest - nC)[0] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_dest[0] * (matrix[0, 1] + matrix[0, 2] + matrix[1, 1] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[0] * matrix[2, 0] + (dataPtr_dest + widthstep)[0] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[0] * matrix[2, 2]) / matrixWeight;
                    green = ((dataPtr_dest - nC)[1] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_dest[1] * (matrix[0, 1] + matrix[0, 2] + matrix[1, 1] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[1] * matrix[2, 0] + (dataPtr_dest + widthstep)[1] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[1] * matrix[2, 2]) / matrixWeight;
                    red = ((dataPtr_dest - nC)[2] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_dest[2] * (matrix[0, 1] + matrix[0, 2] + matrix[1, 1] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[2] * matrix[2, 0] + (dataPtr_dest + widthstep)[2] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[2] * matrix[2, 2]) / matrixWeight;

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                }

                dataPtr += widthstep;
                dataPtr_dest += widthstep;

                // coluna lateral direita (sem cantos)
                for (y = 1; y < height - 1; y++)
                {
                    blue = ((dataPtr_dest - widthstep - nC)[0] * matrix[0, 0] + (dataPtr_dest - widthstep)[0] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_dest - nC)[0] * matrix[1, 0] + dataPtr_dest[0] * (matrix[1, 1] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[0] * matrix[2, 0] + (dataPtr_dest + widthstep)[0] * (matrix[2, 1] + matrix[2, 2])) / matrixWeight;
                    green = ((dataPtr_dest - widthstep - nC)[1] * matrix[0, 0] + (dataPtr_dest - widthstep)[1] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_dest - nC)[1] * matrix[1, 0] + dataPtr_dest[1] * (matrix[1, 1] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[1] * matrix[2, 0] + (dataPtr_dest + widthstep)[1] * (matrix[2, 1] + matrix[2, 2])) / matrixWeight;
                    red = ((dataPtr_dest - widthstep - nC)[2] * matrix[0, 0] + (dataPtr_dest - widthstep)[2] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_dest - nC)[2] * matrix[1, 0] + dataPtr_dest[2] * (matrix[1, 1] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[2] * matrix[2, 0] + (dataPtr_dest + widthstep)[2] * (matrix[2, 1] + matrix[2, 2])) / matrixWeight;

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    dataPtr += widthstep;
                    dataPtr_dest += widthstep;
                }

                // canto inferior direito
                blue = ((dataPtr_dest - widthstep - nC)[0] * matrix[0, 0] + (dataPtr_dest - widthstep)[0] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_dest - nC)[0] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_dest[0] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2])) / matrixWeight;
                green = ((dataPtr_dest - widthstep - nC)[1] * matrix[0, 0] + (dataPtr_dest - widthstep)[1] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_dest - nC)[1] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_dest[1] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2])) / matrixWeight;
                red = ((dataPtr_dest - widthstep - nC)[2] * matrix[0, 0] + (dataPtr_dest - widthstep)[2] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_dest - nC)[2] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_dest[2] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2])) / matrixWeight;

                dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                dataPtr -= nC;
                dataPtr_dest -= nC;

                // última linha (sem cantos)
                for (x = 1; x < width - 1; x++)
                {
                    blue = ((dataPtr_dest - widthstep - nC)[0] * matrix[0, 0] + (dataPtr_dest - widthstep)[0] * matrix[0, 1] + (dataPtr_dest - widthstep + nC)[0] * matrix[0, 2] + (dataPtr_dest - nC)[0] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_dest[0] * (matrix[1, 1] + matrix[2, 1]) + (dataPtr_dest + nC)[0] * (matrix[1, 2] + matrix[2, 2])) / matrixWeight;
                    green = ((dataPtr_dest - widthstep - nC)[1] * matrix[0, 0] + (dataPtr_dest - widthstep)[1] * matrix[0, 1] + (dataPtr_dest - widthstep + nC)[1] * matrix[0, 2] + (dataPtr_dest - nC)[1] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_dest[1] * (matrix[1, 1] + matrix[2, 1]) + (dataPtr_dest + nC)[1] * (matrix[1, 2] + matrix[2, 2])) / matrixWeight;
                    red = ((dataPtr_dest - widthstep - nC)[2] * matrix[0, 0] + (dataPtr_dest - widthstep)[2] * matrix[0, 1] + (dataPtr_dest - widthstep + nC)[2] * matrix[0, 2] + (dataPtr_dest - nC)[2] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_dest[2] * (matrix[1, 1] + matrix[2, 1]) + (dataPtr_dest + nC)[2] * (matrix[1, 2] + matrix[2, 2])) / matrixWeight;

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    dataPtr -= nC;
                    dataPtr_dest -= nC;

                }

                // canto inferior esquerdo
                blue = ((dataPtr_dest - widthstep)[0] * (matrix[0, 0] + matrix[0, 1]) + (dataPtr_dest - widthstep + nC)[0] * matrix[0, 2] + (dataPtr_dest + nC)[0] * (matrix[1, 2] + matrix[2, 2]) + dataPtr_dest[0] * (matrix[1, 0] + matrix[1, 1] + matrix[2, 0] + matrix[2, 1])) / matrixWeight;
                green = ((dataPtr_dest - widthstep)[1] * (matrix[0, 0] + matrix[0, 1]) + (dataPtr_dest - widthstep + nC)[1] * matrix[0, 2] + (dataPtr_dest + nC)[1] * (matrix[1, 2] + matrix[2, 2]) + dataPtr_dest[1] * (matrix[1, 0] + matrix[1, 1] + matrix[2, 0] + matrix[2, 1])) / matrixWeight;
                red = ((dataPtr_dest - widthstep)[2] * (matrix[0, 0] + matrix[0, 1]) + (dataPtr_dest - widthstep + nC)[2] * matrix[0, 2] + (dataPtr_dest + nC)[2] * (matrix[1, 2] + matrix[2, 2]) + dataPtr_dest[2] * (matrix[1, 0] + matrix[1, 1] + matrix[2, 0] + matrix[2, 1])) / matrixWeight;

                dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                dataPtr -= widthstep;
                dataPtr_dest -= widthstep;

                // coluna lateral esquerda (sem cantos)
                for (y = 1; y < height - 1; y++)
                {

                    blue = ((dataPtr_dest - widthstep)[0] * (matrix[0, 0] + matrix[0, 1]) + (dataPtr_dest - widthstep + nC)[0] * matrix[0, 2] + dataPtr_dest[0] * (matrix[1, 0] + matrix[1, 1]) + (dataPtr_dest + nC)[0] * matrix[1, 2] + (dataPtr_dest + widthstep)[0] * (matrix[2, 0] + matrix[2, 1]) + (dataPtr_dest + widthstep + nC)[0] * matrix[2, 2]) / matrixWeight;
                    green = ((dataPtr_dest - widthstep)[1] * (matrix[0, 0] + matrix[0, 1]) + (dataPtr_dest - widthstep + nC)[1] * matrix[0, 2] + dataPtr_dest[1] * (matrix[1, 0] + matrix[1, 1]) + (dataPtr_dest + nC)[1] * matrix[1, 2] + (dataPtr_dest + widthstep)[1] * (matrix[2, 0] + matrix[2, 1]) + (dataPtr_dest + widthstep + nC)[1] * matrix[2, 2]) / matrixWeight;
                    red = ((dataPtr_dest - widthstep)[2] * (matrix[0, 0] + matrix[0, 1]) + (dataPtr_dest - widthstep + nC)[2] * matrix[0, 2] + dataPtr_dest[2] * (matrix[1, 0] + matrix[1, 1]) + (dataPtr_dest + nC)[2] * matrix[1, 2] + (dataPtr_dest + widthstep)[2] * (matrix[2, 0] + matrix[2, 1]) + (dataPtr_dest + widthstep + nC)[2] * matrix[2, 2]) / matrixWeight;

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    dataPtr -= widthstep;
                    dataPtr_dest -= widthstep;
                }

            }
        }

        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                MIplImage m_dest = imgCopy.MIplImage;
                byte* dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nC * width;

                double blue, red, green;
                double blue_x, blue_y, red_x, red_y, green_x, green_y;
                int x, y;


                dataPtr += widthstep + nC;
                dataPtr_dest += widthstep + nC;

                if (nC == 3)
                {

                    for (y = 1; y < (height - 1); y++) // all but margins
                    {

                        for (x = 1; x < (width - 1); x++)
                        {

                            blue_x = (dataPtr_dest - nC - widthstep)[0] + (dataPtr_dest - nC)[0] * 2 + (dataPtr_dest - nC + widthstep)[0] - ((dataPtr_dest + nC + widthstep)[0] + (dataPtr_dest + nC)[0] * 2 + (dataPtr_dest + nC - widthstep)[0]);
                            blue_y = (dataPtr_dest - nC + widthstep)[0] + (dataPtr_dest + widthstep)[0] * 2 + (dataPtr_dest + nC + widthstep)[0] - ((dataPtr_dest - nC - widthstep)[0] + (dataPtr_dest - widthstep)[0] * 2 + (dataPtr_dest + nC - widthstep)[0]);

                            green_x = (dataPtr_dest - nC - widthstep)[1] + (dataPtr_dest - nC)[1] * 2 + (dataPtr_dest - nC + widthstep)[1] - ((dataPtr_dest + nC + widthstep)[1] + (dataPtr_dest + nC)[1] * 2 + (dataPtr_dest + nC - widthstep)[1]);
                            green_y = (dataPtr_dest - nC + widthstep)[1] + (dataPtr_dest + widthstep)[1] * 2 + (dataPtr_dest + nC + widthstep)[1] - ((dataPtr_dest - nC - widthstep)[1] + (dataPtr_dest - widthstep)[1] * 2 + (dataPtr_dest + nC - widthstep)[1]);

                            red_x = (dataPtr_dest - nC - widthstep)[2] + (dataPtr_dest - nC)[2] * 2 + (dataPtr_dest - nC + widthstep)[2] - ((dataPtr_dest + nC + widthstep)[2] + (dataPtr_dest + nC)[2] * 2 + (dataPtr_dest + nC - widthstep)[2]);
                            red_y = (dataPtr_dest - nC + widthstep)[2] + (dataPtr_dest + widthstep)[2] * 2 + (dataPtr_dest + nC + widthstep)[2] - ((dataPtr_dest - nC - widthstep)[2] + (dataPtr_dest - widthstep)[2] * 2 + (dataPtr_dest + nC - widthstep)[2]);

                            blue = Math.Abs(blue_x) + Math.Abs(blue_y);
                            green = Math.Abs(green_x) + Math.Abs(green_y);
                            red = Math.Abs(red_x) + Math.Abs(red_y);

                            dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                            dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                            dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                            dataPtr_dest += nC;
                            dataPtr += nC;

                        }
                        dataPtr_dest += padding + 2 * nC; // nC to gett out of the margin
                        dataPtr += padding + 2 * nC;
                    }

                    // x=0 , y=0 primeiro pixel
                    dataPtr = (byte*)m.imageData.ToPointer();
                    dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                    blue_x = dataPtr_dest[0] * 3 + (dataPtr_dest + widthstep)[0] - ((dataPtr_dest + nC)[0] * 3 + (dataPtr_dest + widthstep + nC)[0]);
                    blue_y = (dataPtr_dest + widthstep)[0] * 3 + (dataPtr_dest + widthstep + nC)[0] - (dataPtr_dest[0] * 3 + (dataPtr + nC)[0]);

                    green_x = dataPtr_dest[1] * 3 + (dataPtr_dest + widthstep)[1] - ((dataPtr_dest + nC)[1] * 3 + (dataPtr_dest + widthstep + nC)[1]);
                    green_y = (dataPtr_dest + widthstep)[1] * 3 + (dataPtr_dest + widthstep + nC)[1] - (dataPtr_dest[1] * 3 + (dataPtr + nC)[1]);

                    red_x = dataPtr_dest[2] * 3 + (dataPtr_dest + widthstep)[2] - ((dataPtr_dest + nC)[2] * 3 + (dataPtr_dest + widthstep + nC)[2]);
                    red_y = (dataPtr_dest + widthstep)[2] * 3 + (dataPtr_dest + widthstep + nC)[2] - (dataPtr_dest[2] * 3 + (dataPtr + nC)[2]);

                    blue = Math.Abs(blue_x) + Math.Abs(blue_y);
                    green = Math.Abs(green_x) + Math.Abs(green_y);
                    red = Math.Abs(red_x) + Math.Abs(red_y);

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    // x=1,2,3... , y=0 linha de cima
                    dataPtr += nC;
                    dataPtr_dest += nC;

                    for (x = 1; x < (width - 1); x++)
                    {


                        blue_x = (dataPtr_dest - nC)[0] * 3 + (dataPtr_dest - nC + widthstep)[0] - ((dataPtr_dest + nC)[0] * 3 + (dataPtr_dest + nC + widthstep)[0]);
                        blue_y = (dataPtr_dest - nC + widthstep)[0] + (dataPtr_dest + widthstep)[0] * 2 + (dataPtr_dest + nC + widthstep)[0] - ((dataPtr_dest - nC)[0] + dataPtr_dest[0] * 2 + (dataPtr_dest + nC)[0]);

                        green_x = (dataPtr_dest - nC)[1] * 3 + (dataPtr_dest - nC + widthstep)[1] - ((dataPtr_dest + nC)[1] * 3 + (dataPtr_dest + nC + widthstep)[1]);
                        green_y = (dataPtr_dest - nC + widthstep)[1] + (dataPtr_dest + widthstep)[1] * 2 + (dataPtr_dest + nC + widthstep)[1] - ((dataPtr_dest - nC)[1] + dataPtr_dest[1] * 2 + (dataPtr_dest + nC)[1]);

                        red_x = (dataPtr_dest - nC)[2] * 3 + (dataPtr_dest - nC + widthstep)[2] - ((dataPtr_dest + nC)[2] * 3 + (dataPtr_dest + nC + widthstep)[2]);
                        red_y = (dataPtr_dest - nC + widthstep)[2] + (dataPtr_dest + widthstep)[2] * 2 + (dataPtr_dest + nC + widthstep)[2] - ((dataPtr_dest - nC)[2] + dataPtr_dest[2] * 2 + (dataPtr_dest + nC)[2]);

                        blue = Math.Abs(blue_x) + Math.Abs(blue_y);
                        green = Math.Abs(green_x) + Math.Abs(green_y);
                        red = Math.Abs(red_x) + Math.Abs(red_y);

                        dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                        dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                        dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                        dataPtr += nC;
                        dataPtr_dest += nC;

                    }


                    ////// x=n , y=0 ultimo pixel da 1a linha
                    blue_x = (dataPtr_dest - nC)[0] * 3 + (dataPtr_dest - nC + widthstep)[0] - (dataPtr_dest[0] * 3 + (dataPtr_dest + widthstep)[0]);
                    blue_y = (dataPtr_dest - nC + widthstep)[0] + (dataPtr_dest + widthstep)[0] * 3 - ((dataPtr_dest - nC)[0] + dataPtr_dest[0] * 3);

                    green_x = (dataPtr_dest - nC)[1] * 3 + (dataPtr_dest - nC + widthstep)[1] - (dataPtr_dest[1] * 3 + (dataPtr_dest + widthstep)[1]);
                    green_y = (dataPtr_dest - nC + widthstep)[1] + (dataPtr_dest + widthstep)[1] * 3 - ((dataPtr_dest - nC)[1] + dataPtr_dest[1] * 3);

                    red_x = (dataPtr_dest - nC)[2] * 3 + (dataPtr_dest - nC + widthstep)[2] - (dataPtr_dest[2] * 3 + (dataPtr_dest + widthstep)[2]);
                    red_y = (dataPtr_dest - nC + widthstep)[2] + (dataPtr_dest + widthstep)[2] * 3 - ((dataPtr_dest - nC)[2] + dataPtr_dest[2] * 3);

                    blue = Math.Abs(blue_x) + Math.Abs(blue_y);
                    green = Math.Abs(green_x) + Math.Abs(green_y);
                    red = Math.Abs(red_x) + Math.Abs(red_y);

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    //// x=n , y=1,2,3... margem da direita
                    dataPtr += widthstep;
                    dataPtr_dest += widthstep;

                    for (y = 1; y < (height - 1); y++)
                    {

                        blue_x = (dataPtr_dest - nC - widthstep)[0] + (dataPtr_dest - nC)[0] * 2 + (dataPtr_dest - nC + widthstep)[0] - ((dataPtr_dest - widthstep)[0] + dataPtr_dest[0] * 2 + (dataPtr_dest + widthstep)[0]);
                        blue_y = (dataPtr_dest - nC + widthstep)[0] + (dataPtr_dest + widthstep)[0] * 3 - ((dataPtr_dest - nC - widthstep)[0] + (dataPtr_dest - widthstep)[0] * 3);

                        green_x = (dataPtr_dest - nC - widthstep)[1] + (dataPtr_dest - nC)[1] * 2 + (dataPtr_dest - nC + widthstep)[1] - ((dataPtr_dest - widthstep)[1] + dataPtr_dest[1] * 2 + (dataPtr_dest + widthstep)[1]);
                        green_y = (dataPtr_dest - nC + widthstep)[1] + (dataPtr_dest + widthstep)[1] * 3 - ((dataPtr_dest - nC - widthstep)[1] + (dataPtr_dest - widthstep)[1] * 3);

                        red_x = (dataPtr_dest - nC - widthstep)[2] + (dataPtr_dest - nC)[2] * 2 + (dataPtr_dest - nC + widthstep)[2] - ((dataPtr_dest - widthstep)[2] + dataPtr_dest[2] * 2 + (dataPtr_dest + widthstep)[2]);
                        red_y = (dataPtr_dest - nC + widthstep)[2] + (dataPtr_dest + widthstep)[2] * 3 - ((dataPtr_dest - nC - widthstep)[2] + (dataPtr_dest - widthstep)[2] * 3);

                        blue = Math.Abs(blue_x) + Math.Abs(blue_y);
                        green = Math.Abs(green_x) + Math.Abs(green_y);
                        red = Math.Abs(red_x) + Math.Abs(red_y);

                        dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                        dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                        dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                        dataPtr += widthstep;
                        dataPtr_dest += widthstep;

                    }


                    //// x=n , y=n ultimo pixel direita em baixo

                    blue_x = (dataPtr_dest - widthstep - nC)[0] + (dataPtr_dest - nC)[0] * 3 - ((dataPtr_dest - widthstep)[0] + dataPtr_dest[0] * 3);
                    blue_y = (dataPtr_dest - nC)[0] + dataPtr_dest[0] * 3 - ((dataPtr_dest - widthstep - nC)[0] + (dataPtr_dest - widthstep)[0] * 3);

                    green_x = (dataPtr_dest - widthstep - nC)[1] + (dataPtr_dest - nC)[1] * 3 - ((dataPtr_dest - widthstep)[1] + dataPtr_dest[1] * 3);
                    green_y = (dataPtr_dest - nC)[1] + dataPtr_dest[1] * 3 - ((dataPtr_dest - widthstep - nC)[1] + (dataPtr_dest - widthstep)[1] * 3);

                    red_x = (dataPtr_dest - widthstep - nC)[2] + (dataPtr_dest - nC)[2] * 3 - ((dataPtr_dest - widthstep)[2] + dataPtr_dest[2] * 3);
                    red_y = (dataPtr_dest - nC)[2] + dataPtr_dest[2] * 3 - ((dataPtr_dest - widthstep - nC)[2] + (dataPtr_dest - widthstep)[2] * 3);

                    blue = Math.Abs(blue_x) + Math.Abs(blue_y);
                    green = Math.Abs(green_x) + Math.Abs(green_y);
                    red = Math.Abs(red_x) + Math.Abs(red_y);

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    //// x= 1,2,3... , y=n margem de baixo
                    dataPtr -= nC;
                    dataPtr_dest -= nC;

                    for (x = 1; x < (width - 1); x++)
                    {

                        blue_x = (dataPtr_dest - widthstep - nC)[0] + (dataPtr_dest - nC)[0] * 3 - ((dataPtr_dest - widthstep + nC)[0] + (dataPtr_dest + nC)[0] * 3);
                        blue_y = (dataPtr_dest - nC)[0] + dataPtr_dest[0] * 2 + (dataPtr_dest + nC)[0] - ((dataPtr_dest - widthstep - nC)[0] + (dataPtr_dest - widthstep)[0] * 2 + (dataPtr_dest - widthstep + nC)[0]);

                        green_x = (dataPtr_dest - widthstep - nC)[1] + (dataPtr_dest - nC)[1] * 3 - ((dataPtr_dest - widthstep + nC)[1] + (dataPtr_dest + nC)[1] * 3);
                        green_y = (dataPtr_dest - nC)[1] + dataPtr_dest[1] * 2 + (dataPtr_dest + nC)[1] - ((dataPtr_dest - widthstep - nC)[1] + (dataPtr_dest - widthstep)[1] * 2 + (dataPtr_dest - widthstep + nC)[1]);

                        red_x = (dataPtr_dest - widthstep - nC)[2] + (dataPtr_dest - nC)[2] * 3 - ((dataPtr_dest - widthstep + nC)[2] + (dataPtr_dest + nC)[2] * 3);
                        red_y = (dataPtr_dest - nC)[2] + dataPtr_dest[2] * 2 + (dataPtr_dest + nC)[2] - ((dataPtr_dest - widthstep - nC)[2] + (dataPtr_dest - widthstep)[2] * 2 + (dataPtr_dest - widthstep + nC)[2]);

                        blue = Math.Abs(blue_x) + Math.Abs(blue_y);
                        green = Math.Abs(green_x) + Math.Abs(green_y);
                        red = Math.Abs(red_x) + Math.Abs(red_y);

                        dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                        dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                        dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                        dataPtr -= nC;
                        dataPtr_dest -= nC;

                    }

                    // x=0 , y=n ultimo pixel em baixo esquerda

                    blue_x = (dataPtr_dest - widthstep)[0] + dataPtr_dest[0] * 3 - ((dataPtr_dest - widthstep + nC)[0] + (dataPtr_dest + nC)[0] * 3);
                    blue_y = dataPtr_dest[0] * 3 + (dataPtr_dest + nC)[0] - ((dataPtr_dest - widthstep)[0] * 3 + (dataPtr_dest - widthstep + nC)[0]);

                    green_x = (dataPtr_dest - widthstep)[1] + dataPtr_dest[1] * 3 - ((dataPtr_dest - widthstep + nC)[1] + (dataPtr_dest + nC)[1] * 3);
                    green_y = dataPtr_dest[1] * 3 + (dataPtr_dest + nC)[1] - ((dataPtr_dest - widthstep)[1] * 3 + (dataPtr_dest - widthstep + nC)[1]);

                    red_x = (dataPtr_dest - widthstep)[2] + dataPtr_dest[2] * 3 - ((dataPtr_dest - widthstep + nC)[2] + (dataPtr_dest + nC)[2] * 3);
                    red_y = dataPtr_dest[2] * 3 + (dataPtr_dest + nC)[2] - ((dataPtr_dest - widthstep)[2] * 3 + (dataPtr_dest - widthstep + nC)[2]);

                    blue = Math.Abs(blue_x) + Math.Abs(blue_y);
                    green = Math.Abs(green_x) + Math.Abs(green_y);
                    red = Math.Abs(red_x) + Math.Abs(red_y);

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    //x=0 , y=1,2,3... coluna lado esq
                    dataPtr -= widthstep;
                    dataPtr_dest -= widthstep;

                    for (y = 1; y < (height - 1); y++)
                    {

                        blue_x = (dataPtr_dest - widthstep)[0] + dataPtr_dest[0] * 2 + (dataPtr_dest + widthstep)[0] - ((dataPtr_dest - widthstep + nC)[0] + (dataPtr_dest + nC)[0] * 2 + (dataPtr_dest + widthstep + nC)[0]);
                        blue_y = (dataPtr_dest + widthstep)[0] * 3 + (dataPtr_dest + widthstep + nC)[0] - ((dataPtr_dest - widthstep)[0] * 3 + (dataPtr_dest - widthstep + nC)[0]);

                        green_x = (dataPtr_dest - widthstep)[1] + dataPtr_dest[1] * 2 + (dataPtr_dest + widthstep)[1] - ((dataPtr_dest - widthstep + nC)[1] + (dataPtr_dest + nC)[1] * 2 + (dataPtr_dest + widthstep + nC)[1]);
                        green_y = (dataPtr_dest + widthstep)[1] * 3 + (dataPtr_dest + widthstep + nC)[1] - ((dataPtr_dest - widthstep)[1] * 3 + (dataPtr_dest - widthstep + nC)[1]);

                        red_x = (dataPtr_dest - widthstep)[2] + dataPtr_dest[2] * 2 + (dataPtr_dest + widthstep)[2] - ((dataPtr_dest - widthstep + nC)[2] + (dataPtr_dest + nC)[2] * 2 + (dataPtr_dest + widthstep + nC)[2]);
                        red_y = (dataPtr_dest + widthstep)[2] * 3 + (dataPtr_dest + widthstep + nC)[2] - ((dataPtr_dest - widthstep)[2] * 3 + (dataPtr_dest - widthstep + nC)[2]);

                        blue = Math.Abs(blue_x) + Math.Abs(blue_y);
                        green = Math.Abs(green_x) + Math.Abs(green_y);
                        red = Math.Abs(red_x) + Math.Abs(red_y);

                        dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                        dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                        dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                        dataPtr -= widthstep;
                        dataPtr_dest -= widthstep;

                    }
                }
            }
        }

        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {

            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                MIplImage m_dest = imgCopy.MIplImage;
                byte* dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nC * width;

                double blue, red, green;
                int x, y;

                dataPtr += widthstep + nC;
                dataPtr_dest += widthstep + nC;

                if (nC == 3)
                {
                    for (y = 1; y < (height - 1); y++) // all but margins
                    {

                        for (x = 1; x < (width - 1); x++)
                        {

                            blue = Math.Abs(dataPtr_dest[0] - (dataPtr_dest + nC)[0]) + Math.Abs(dataPtr_dest[0] - (dataPtr_dest + widthstep)[0]);
                            green = Math.Abs(dataPtr_dest[1] - (dataPtr_dest + nC)[1]) + Math.Abs(dataPtr_dest[1] - (dataPtr_dest + widthstep)[1]);
                            red = Math.Abs(dataPtr_dest[2] - (dataPtr_dest + nC)[2]) + Math.Abs(dataPtr_dest[2] - (dataPtr_dest + widthstep)[2]);

                            dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                            dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                            dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                            dataPtr_dest += nC;
                            dataPtr += nC;

                        }
                        dataPtr_dest += padding + 2 * nC; // nC to gett out of the margin
                        dataPtr += padding + 2 * nC;
                    }

                    // x=0 , y=0 primeiro pixel
                    dataPtr = (byte*)m.imageData.ToPointer();
                    dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                    for (x = 0; x < (width - 1); x++)
                    {
                        blue = Math.Abs(dataPtr_dest[0] - (dataPtr_dest + nC)[0]) + Math.Abs(dataPtr_dest[0] - (dataPtr_dest + widthstep)[0]);
                        green = Math.Abs(dataPtr_dest[1] - (dataPtr_dest + nC)[1]) + Math.Abs(dataPtr_dest[1] - (dataPtr_dest + widthstep)[1]);
                        red = Math.Abs(dataPtr_dest[2] - (dataPtr_dest + nC)[2]) + Math.Abs(dataPtr_dest[2] - (dataPtr_dest + widthstep)[2]);

                        dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                        dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                        dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                        dataPtr_dest += nC;
                        dataPtr += nC;
                    }

                    blue = Math.Abs(dataPtr_dest[0] - (dataPtr_dest + widthstep)[0]);
                    green = Math.Abs(dataPtr_dest[1] - (dataPtr_dest + widthstep)[1]);
                    red = Math.Abs(dataPtr_dest[2] - (dataPtr_dest + widthstep)[2]);

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    dataPtr += widthstep;
                    dataPtr_dest += widthstep;

                    for (y = 1; y < (height - 1); y++)
                    {
                        blue = Math.Abs(dataPtr_dest[0] - (dataPtr_dest + widthstep)[0]);
                        green = Math.Abs(dataPtr_dest[1] - (dataPtr_dest + widthstep)[1]);
                        red = Math.Abs(dataPtr_dest[2] - (dataPtr_dest + widthstep)[2]);

                        dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                        dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                        dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                        dataPtr_dest += widthstep;
                        dataPtr += widthstep;
                    }

                    dataPtr[0] = (byte)0;
                    dataPtr[1] = (byte)0;
                    dataPtr[2] = (byte)0;

                    dataPtr -= nC;
                    dataPtr_dest -= nC;

                    for (x = 1; x < (width - 1); x++)
                    {
                        blue = Math.Abs(dataPtr_dest[0] - (dataPtr_dest + nC)[0]);
                        green = Math.Abs(dataPtr_dest[1] - (dataPtr_dest + nC)[1]);
                        red = Math.Abs(dataPtr_dest[2] - (dataPtr_dest + nC)[2]);

                        dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                        dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                        dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                        dataPtr -= nC;
                        dataPtr_dest -= nC;
                    }

                    blue = Math.Abs(dataPtr_dest[0] - (dataPtr_dest + nC)[0]);
                    green = Math.Abs(dataPtr_dest[1] - (dataPtr_dest + nC)[1]);
                    red = Math.Abs(dataPtr_dest[2] - (dataPtr_dest + nC)[2]);

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    dataPtr -= widthstep;
                    dataPtr_dest -= widthstep;

                    for (y = 1; y < (height - 1); y++)
                    {
                        blue = Math.Abs(dataPtr_dest[0] - (dataPtr_dest + nC)[0]) + Math.Abs(dataPtr_dest[0] - (dataPtr_dest + widthstep)[0]);
                        green = Math.Abs(dataPtr_dest[1] - (dataPtr_dest + nC)[1]) + Math.Abs(dataPtr_dest[1] - (dataPtr_dest + widthstep)[1]);
                        red = Math.Abs(dataPtr_dest[2] - (dataPtr_dest + nC)[2]) + Math.Abs(dataPtr_dest[2] - (dataPtr_dest + widthstep)[2]);

                        dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                        dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                        dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                        dataPtr -= widthstep;
                        dataPtr_dest -= widthstep;
                    }


                }

            }
        }

        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            imgCopy.SmoothMedian(3).CopyTo(img);
        }

        public static int[] Histogram_Gray(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int[] vector = new int[256];

                if (nC == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            vector[gray]++;

                            dataPtr += nC;
                        }
                        dataPtr += padding;
                    }
                }
                return vector;

            }
        }

        public static int[,] Histogram_RGB(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int[,] matrix = new int[3, 256];

                if (nC == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];


                            matrix[0, blue]++;
                            matrix[1, green]++;
                            matrix[2, red]++;

                            dataPtr += nC;
                        }
                        dataPtr += padding;
                    }
                }
                return matrix;

            }
        }

        public static int[,] Histogram_All(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int[,] matrix = new int[4, 256];

                if (nC == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            matrix[0, gray]++;
                            matrix[1, blue]++;
                            matrix[2, green]++;
                            matrix[3, red]++;

                            dataPtr += nC;
                        }
                        dataPtr += padding;
                    }
                }
                return matrix;

            }
        }

        public static void Equalization(Image<Bgr, byte> img)
        {
            unsafe
            {
                Image<Ycc, byte> imgYCC = img.Convert<Ycc, byte>();
                MIplImage m = imgYCC.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                byte Y;
                int acumHist = 0, acumHistMin = 0;

                int width = img.Width;
                int height = img.Height;
                int wXh = width * height;
                int nC = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y, i;
                int[] vector = new int[256];
                double[] newH = new double[256];

                if (nC == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            Y = dataPtr[0];

                            vector[Y]++;

                            dataPtr += nC;
                        }
                        dataPtr += padding;
                    }

                    for (i = 0; i <= 255; i++)
                    {
                        if (acumHistMin == 0)
                            acumHistMin = vector[i];

                        acumHist += vector[i];

                        newH[i] = Math.Round((((double)acumHist - acumHistMin) / (wXh - acumHistMin)) * 255);
                    }

                    dataPtr = (byte*)m.imageData.ToPointer();

                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            dataPtr[0] = (byte)newH[dataPtr[0]];

                            dataPtr += nC;
                        }
                        dataPtr += padding;
                    }
                }

                img.ConvertFrom<Ycc, byte>(imgYCC);

            }
        }

        public static void ConvertToBW(Emgu.CV.Image<Bgr, byte> img, int threshold)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int[] vector = new int[256];

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {

                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                        gray = (byte)(gray <= threshold ? 0 : 255);

                        dataPtr[0] = gray;
                        dataPtr[1] = gray;
                        dataPtr[2] = gray;

                        dataPtr += nC;

                    }
                    dataPtr += padding;
                }
            }
        }

        private static int OTSU(int[] hist, int nPixels)
        {

            double u1, u2, o2, prev_o2, pi, q22, q2, q1, q11;
            int t, threshold;
            t = threshold = 0;
            u1 = u2 = o2 = prev_o2 = pi = q22 = q2 = q1 = q11 = 0;

            while (t < 255)
            {
                u1 = u2 = o2 = pi = q22 = q2 = q1 = q11 = 0;

                for (int i = 0; i <= t; i++)
                {
                    pi = (double)hist[i] / nPixels;
                    q1 += pi;
                    q11 += (double)i * pi;

                }
                for (int i = t + 1; i <= 255; i++)
                {
                    pi = (double)hist[i] / nPixels;
                    q2 += pi;
                    q22 += (double)i * pi;
                }

                u1 = q11 / q1;
                u2 = q22 / q2;
                o2 = q1 * q2 * Math.Pow((u1 - u2), 2);

                if (o2 > prev_o2)
                {
                    threshold = t;
                    prev_o2 = o2;
                }
                t++;
            }

            return threshold;
        }

        public static void ConvertToBW_Otsu(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;

                int nPixels = width * height;

                int[] hist = Histogram_Gray(img);
                int threshold = OTSU(hist, nPixels);
                ImageClass.ConvertToBW(img, threshold);
            }
        }

        public static void Rotation_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrAux = (byte*)mCopy.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, i;
                float xaux1, xaux2, final;
                float j, k; //fatores(offsets) para a interpolação ==> tem que se dar valores?
                double xf, yf;
                int xfaux, yfaux;

                if (nC == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xf = (((x - (width / 2.0)) * Math.Cos(angle)) - (((height / 2.0) - y) * Math.Sin(angle)) + width / 2.0);
                            yf = ((height / 2.0) - ((x - (width / 2.0)) * Math.Sin(angle)) - (((height / 2.0) - y) * Math.Cos(angle)));

                            j = (float)xf % 1;
                            k = (float)yf % 1;

                            xfaux = (int)(xf / 1);
                            yfaux = (int)(yf / 1);

                            if (xf < 0 || yf < 0 || xf >= width -1 || yf >= height -1)
                            {
                                (dataPtr + y * m.widthStep + x * nC)[0] = 255;
                                (dataPtr + y * m.widthStep + x * nC)[1] = 255;
                                (dataPtr + y * m.widthStep + x * nC)[2] = 255;
                            }
                            else
                            {
                                for (i = 0; i < 3; i++)
                                {
                                    xaux1 = ((1 - j) * (dataPtrAux + (yfaux) * m.widthStep + xfaux * nC)[i] + j * (dataPtrAux + yfaux * m.widthStep + (xfaux + 1) * nC)[i]);
                                    xaux2 = ((1 - j) * (dataPtrAux + (yfaux + 1) * m.widthStep + xfaux * nC)[i] + j * (dataPtrAux + (yfaux + 1) * m.widthStep + (xfaux + 1) * nC)[i]);
                                    final = (int)Math.Round((1 - k) * xaux1 + k * xaux2);
                                    (dataPtr + y * m.widthStep + x * nC)[i] = (byte)final;
                                }
                            }




                        }
                    }
                }

            }
        }

        // Morphological operation: Dilatation
        // Structure object (5x5 kernel) :
        // 11111
        // 11111
        // 11111
        // 11111
        public static void Dilatation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                MIplImage m_dest = imgCopy.MIplImage;
                byte* dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nC * width;

                int x, y;

                dataPtr += 2 * widthstep + 2 * nC;
                dataPtr_dest += 2 * widthstep + 2 * nC;

                if (nC == 3)
                {
                    for (y = 2; y < (height - 2); y++) // all but margins
                    {

                        for (x = 2; x < (width - 2); x++)
                        {
                            // mascara 5x5
                            if ((dataPtr_dest - nC - 2 * widthstep)[0] != 0 || (dataPtr_dest - 2 * widthstep)[0] != 0 || (dataPtr_dest + nC - 2 * widthstep)[0] != 0 || (dataPtr_dest + 2 * nC - 2 * widthstep)[0] != 0 || (dataPtr_dest - 2 * nC - 2 * widthstep)[0] != 0 ||
                                (dataPtr_dest - nC - widthstep)[0] != 0 || (dataPtr_dest - widthstep)[0] != 0 || (dataPtr_dest + nC - widthstep)[0] != 0 || (dataPtr_dest + 2 * nC - widthstep)[0] != 0 || (dataPtr_dest - 2 * nC - widthstep)[0] != 0 ||
                                (dataPtr_dest - nC)[0] != 0 || dataPtr_dest[0] != 0 || (dataPtr_dest + nC)[0] != 0 || (dataPtr_dest + 2 * nC)[0] != 0 || (dataPtr_dest - 2 * nC)[0] != 0 || (dataPtr_dest + 2 * nC + widthstep)[0] != 0 || (dataPtr_dest - 2 * nC + widthstep)[0] != 0 ||
                                (dataPtr_dest - nC + widthstep)[0] != 0 || (dataPtr_dest + widthstep)[0] != 0 || (dataPtr_dest + nC + widthstep)[0] != 0 || (dataPtr_dest + 2 * nC + widthstep)[0] != 0 || (dataPtr_dest - 2 * nC + widthstep)[0] != 0 ||
                                (dataPtr_dest - nC + 2 * widthstep)[0] != 0 || (dataPtr_dest + 2 * widthstep)[0] != 0 || (dataPtr_dest + nC + 2 * widthstep)[0] != 0 || (dataPtr_dest + 2 * nC + 2 * widthstep)[0] != 0 || (dataPtr_dest - 2 * nC + 2 * widthstep)[0] != 0
                                )
                            /* mascara em cruz 5x5
                            if ( (dataPtr_dest - 2 * widthstep)[0] != 0 ||
                                (dataPtr_dest - nC - widthstep)[0] != 0 || (dataPtr_dest - widthstep)[0] != 0 || (dataPtr_dest + nC - widthstep)[0] != 0 ||
                                (dataPtr_dest - nC)[0] != 0 || dataPtr_dest[0] != 0 || (dataPtr_dest + nC)[0] != 0 || (dataPtr_dest + 2 * nC)[0] != 0 || (dataPtr_dest - 2 * nC)[0] != 0 || (dataPtr_dest + 2 * nC + widthstep)[0] != 0 || (dataPtr_dest - 2 * nC + widthstep)[0] != 0 ||
                                (dataPtr_dest - nC + widthstep)[0] != 0 || (dataPtr_dest + widthstep)[0] != 0 || (dataPtr_dest + nC + widthstep)[0] != 0 ||
                                (dataPtr_dest + 2 * widthstep)[0] != 0 
                                )*/
                            {
                                dataPtr[0] = (byte)(255);
                                dataPtr[1] = (byte)(255);
                                dataPtr[2] = (byte)(255);
                            }

                            dataPtr_dest += nC;
                            dataPtr += nC;

                        }
                        dataPtr_dest += padding + 4 * nC; // nC to gett out of the margin
                        dataPtr += padding + 4 * nC;
                    }
                }
            }
        }

        public static void Erosion(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                MIplImage m_dest = imgCopy.MIplImage;
                byte* dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nC * width;

                int x, y;

                dataPtr += 2 * widthstep + 2 * nC;
                dataPtr_dest += 2 * widthstep + 2 * nC;

                if (nC == 3)
                {
                    for (y = 2; y < (height - 2); y++) // all but margins
                    {

                        for (x = 2; x < (width - 2); x++)
                        {
                            // mascara 5x5
                            if ((dataPtr_dest - nC - 2 * widthstep)[0] == 0 || (dataPtr_dest - 2 * widthstep)[0] == 0 || (dataPtr_dest + nC - 2 * widthstep)[0] == 0 || (dataPtr_dest + 2 * nC - 2 * widthstep)[0] == 0 || (dataPtr_dest - 2 * nC - 2 * widthstep)[0] == 0 ||
                                (dataPtr_dest - nC - widthstep)[0] == 0 || (dataPtr_dest - widthstep)[0] == 0 || (dataPtr_dest + nC - widthstep)[0] == 0 || (dataPtr_dest + 2 * nC - widthstep)[0] == 0 || (dataPtr_dest - 2 * nC - widthstep)[0] == 0 ||
                                (dataPtr_dest - nC)[0] == 0 || dataPtr_dest[0] == 0 || (dataPtr_dest + nC)[0] == 0 || (dataPtr_dest + 2 * nC)[0] == 0 || (dataPtr_dest - 2 * nC)[0] == 0 || (dataPtr_dest + 2 * nC + widthstep)[0] == 0 || (dataPtr_dest - 2 * nC + widthstep)[0] == 0 ||
                                (dataPtr_dest - nC + widthstep)[0] == 0 || (dataPtr_dest + widthstep)[0] == 0 || (dataPtr_dest + nC + widthstep)[0] == 0 || (dataPtr_dest + 2 * nC + widthstep)[0] == 0 || (dataPtr_dest - 2 * nC + widthstep)[0] == 0 ||
                                (dataPtr_dest - nC + 2 * widthstep)[0] == 0 || (dataPtr_dest + 2 * widthstep)[0] == 0 || (dataPtr_dest + nC + 2 * widthstep)[0] == 0 || (dataPtr_dest + 2 * nC + 2 * widthstep)[0] == 0 || (dataPtr_dest - 2 * nC + 2 * widthstep)[0] == 0
                                )
                            /* mascara 5x5 (em cruz)
                            if ((dataPtr_dest - 2 * widthstep)[0] == 0 ||
                               (dataPtr_dest - nC - widthstep)[0] == 0 || (dataPtr_dest - widthstep)[0] == 0 || (dataPtr_dest + nC - widthstep)[0] == 0 ||
                               (dataPtr_dest - nC)[0] == 0 || dataPtr_dest[0] == 0 || (dataPtr_dest + nC)[0] == 0 || (dataPtr_dest + 2 * nC)[0] == 0 || (dataPtr_dest - 2 * nC)[0] == 0 || (dataPtr_dest + 2 * nC + widthstep)[0] == 0 || (dataPtr_dest - 2 * nC + widthstep)[0] == 0 ||
                               (dataPtr_dest - nC + widthstep)[0] == 0 || (dataPtr_dest + widthstep)[0] == 0 || (dataPtr_dest + nC + widthstep)[0] == 0 ||
                               (dataPtr_dest + 2 * widthstep)[0] == 0
                               )*/
                            {
                                dataPtr[0] = (byte)(0);
                                dataPtr[1] = (byte)(0);
                                dataPtr[2] = (byte)(0);
                            }


                            dataPtr_dest += nC;
                            dataPtr += nC;

                        }
                        dataPtr_dest += padding + 4 * nC; // nC to gett out of the margin
                        dataPtr += padding + 4 * nC;
                    }
                }
            }
        }


        /// <summary>
        /// Barcode reader - SS final project
        /// </summary>
        /// <param name="img">Original image</param>
        /// <param name="type">image type</param>
        /// <param name="bc_centroid1">output the centroid of the first barcode </param>
        /// <param name="bc_size1">output the size of the first barcode </param>
        /// <param name="bc_image1">output a string containing the first barcode read from the bars</param>
        /// <param name="bc_number1">output a string containing the first barcode read from the numbers in the bottom</param>
        /// <param name="bc_centroid2">output the centroid of the second barcode </param>
        ///// <param name="bc_size2">output the size of the second barcode</param>
        ///// <param name="bc_image2">output a string containing the second barcode read from the bars. It returns null, if it does not exist.</param>
        ///// <param name="bc_number2">output a string containing the second barcode read from the numbers in the bottom. It returns null, if it does not exist.</param>
        /// <returns>image with barcodes detected</returns>
        public static Image<Bgr, byte> BarCodeReader(Image<Bgr, byte> img, int type, out Point bc_centroid1, out Size bc_size1, out string bc_image1, out string bc_number1, out Point bc_centroid2, out Size bc_size2, out string bc_image2, out string bc_number2)
        {
            // first barcode
            bc_image1 = null;
            bc_number1 = null;
            bc_centroid1 = Point.Empty;
            bc_size1 = Size.Empty;

            //second barcode
            bc_image2 = null;
            bc_number2 = null;
            bc_centroid2 = Point.Empty;
            bc_size2 = Size.Empty;

            int[][] projections;
            int[] vertical_projections, horizontal_projections;
            bool rotation_done = false;
            Point centroid;
            string barcode_barras = "", barcode_digitos = "";
            Image<Bgr, Byte> imgCopy = null;

            imgCopy = img.Copy();

            try
            {
                projections = ImageClass.Segmentation(img);
                vertical_projections = projections[0];
                horizontal_projections = projections[1];

                var angle = ImageClass.EixoMomento(img);

                if (angle != 0)
                {
                    ImageClass.Rotation_Bilinear(img, imgCopy, (float)angle);
                    rotation_done = true;
                }

                projections = ImageClass.Segmentation(img);
                vertical_projections = projections[0];
                horizontal_projections = projections[1];
                var barcode_dimensions = ImageClass.BarcodeDimensions(vertical_projections, horizontal_projections);
                
                // barcode_dimensions[0] - barcode width
                // barcode_dimensions[1] - barcode height
                bc_size1 = new Size(barcode_dimensions[1], barcode_dimensions[0]);

                centroid = ImageClass.LocateBarcode(imgCopy, vertical_projections, horizontal_projections, angle, barcode_dimensions);
                bc_centroid1 = centroid;

                if (rotation_done == false)
                {
                    projections = ImageClass.Segmentation(img);
                    vertical_projections = projections[0];
                    horizontal_projections = projections[1];

                    var returnList = ImageClass.ProjectionsToBits(vertical_projections, horizontal_projections);
                    barcode_barras = ImageClass.DecodeDigits(returnList[0], returnList[1]);

                }
                else
                {
                    var returnList = ImageClass.ConvertToBits(img, centroid);
                    barcode_barras = ImageClass.DecodeDigits(returnList[0], returnList[1]);
                }

                bc_image1 = barcode_barras;

                try
                {
                    barcode_digitos = ImageClass.ReadDigits(img, centroid, barcode_dimensions, horizontal_projections, angle);
                    bc_number1 = barcode_digitos;

                    if (barcode_digitos.Equals(barcode_barras))
                        Console.WriteLine("Os CB coincidem.");
                    else
                        Console.WriteLine("Os CB não coincidem.");
                }
                catch
                {
                    Console.WriteLine("Erro ao ler os digitos.");
                }
            }
            catch
            {
                Console.WriteLine("Erro ao processar a imagem.");
            }

            return imgCopy;

        }

        // returns int[][] 
        // projections[0] - array containing vertical projections
        // projections[1] - array containing horizontal projections
        public static int[][] Segmentation(Image<Bgr, byte> img)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                // default values of numeric array elements are set to zero
                int[] vertical_projections = new int[width];
                int[] horizontal_projections = new int[height];

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        // binary image -> r,g,b components holds the same value - 0 (black) or 255 (white)
                        // if it's a black pixel we increment the projection
                        if (dataPtr[0] == 0 && dataPtr[1] == 0 && dataPtr[2] == 0)
                        {
                            vertical_projections[x]++;
                            horizontal_projections[y]++;
                        }

                        dataPtr += nC;
                    }
                    dataPtr += padding;
                }

                int[][] projections = new int[][] { vertical_projections, horizontal_projections };

                return projections;
            }
        }

        // verificar
        public static float[] CalculateCentroid(Image<Bgr, byte> img, int[] vertical_projections, int[] horizontal_projections)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y, i, num_x = 0, num_y = 0, den_x = 0, den_y = 0;
                float c_x, c_y;
                float[] centroid = new float[2];

                for (i = 0; i < vertical_projections.Length; i++)
                {
                    num_x += vertical_projections[i] * i;
                    den_x += vertical_projections[i];
                }

                for (i = 0; i < horizontal_projections.Length; i++)
                {
                    num_y += horizontal_projections[i] * i;
                    den_y += horizontal_projections[i];
                }

                c_x = num_x / den_x;
                c_y = num_y / den_y;
                centroid[0] = c_x;
                centroid[1] = c_y;

                return centroid;

            }

        }

        // returns list
        // returnList[0] - first_6_digits_projections used to decode barcode
        // returnList[1] - second_6_digits_projections used to decode barcode
        public static List<int[]> ProjectionsToBits(int[] vertical_projections, int[] horizontal_projections)
        {

            int initial_position_1, final_position_1, initial_position_2, final_position_2;
            int pixels_per_bit = 0;
            int digits = 6;
            int bits = 7;
            int[] first_6_digits_projections;
            int[] second_6_digits_projections;
            int i, j;
            var returnList = new List<int[]>();

            // ir até à primeira barra (percorrer o vertical segmentation e ir ignorando até encontrar um valor alto - nao basta ser zero pois temos um digito antes da 1ª barra) 
            // como definir este nr? *** por enquanto 50 ***

            // estamos na primeira barra. verificar quantos pixeis tem a primeira barra que vale 1 bit.
            i = 0;
            while (vertical_projections[i] < 50)
            {
                i++;
            }
            

            while (vertical_projections[i] > 50)
            {
                pixels_per_bit++;
                i++;
            }

            // para irmos para a primeira posicao dos 6 primeiros digitos temos que passar a 2ª barra inicial
            i += 2 * pixels_per_bit;

            first_6_digits_projections = new int[digits * bits * pixels_per_bit];
            second_6_digits_projections = new int[digits * bits * pixels_per_bit];

            initial_position_1 = i;

            // estamos na primeira posicao dos 6 primeiros digitos
            // a última posição dos 6 primeiros digitos é: posicao_atual + 6(digitos)*7(bits)*2(pixeis_por_bit) - 1
            final_position_1 = initial_position_1 + digits * bits * pixels_per_bit - 1;

            // colocamos na variavel first_6_digits_projections as projecoes correspondentes aos 6 primeiros digitos para depois descodificar
            for (j = 0; i <= final_position_1; i++, j++)
            {
                first_6_digits_projections[j] = vertical_projections[i];
            }

            // para irmos para a primeira posicao dos 6 secundos digitos temos que passar as barras intermedias
            i += 5 * pixels_per_bit;
            initial_position_2 = i;

            // estamos na primeira posicao dos 6 segundos digitos
            // a última posição dos 6 segundos digitos é: posicao_atual + 6(digitos)*7(bits)*2(pixeis_por_bit) - 1
            final_position_2 = initial_position_2 + digits * bits * pixels_per_bit - 1;

            // colocamos na variavel second_6_digits_projections as projecoes correspondentes aos 6 segundos digitos para depois descodificar
            for (j = 0; i <= final_position_2; i++, j++)
            {
                second_6_digits_projections[j] = vertical_projections[i];
            }

            // temos que converter as projeções em bits
            int[] first_6_digits_bits = new int[digits * bits];
            for (i = 0, j = 0; i < first_6_digits_projections.Length; i += pixels_per_bit, j++)
            {

                if (first_6_digits_projections[i] < 50)
                {
                    first_6_digits_bits[j] = 0;
                }
                else
                {
                    first_6_digits_bits[j] = 1;
                }
            }

            int[] second_6_digits_bits = new int[digits * bits];
            for (i = 0, j = 0; i < second_6_digits_projections.Length; i += pixels_per_bit, j++)
            {
                if (second_6_digits_projections[i] < 50)
                {
                    second_6_digits_bits[j] = 0;
                }
                else
                {
                    second_6_digits_bits[j] = 1;
                }
            }

            returnList.Add(first_6_digits_bits);
            returnList.Add(second_6_digits_bits);

            return returnList;

        }

        public static List<int[]> ConvertToBits(Image<Bgr, byte> img,Point centroid)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y, pixels_per_bit = 0, i=0, j;
                int digits = 6;
                int bits = 7;
                int initial_position_1, final_position_1, initial_position_2, final_position_2;
                var returnList = new List<int[]>();

                dataPtr += widthstep * centroid.Y;

                // dataPtr está a apontar para o pixel (0,centroid.Y)
                // percorremos agora até ao fim para depois descodificarmos o codigo de barras
                ConvertToBW_Otsu(img);

                while (dataPtr[0]!=0)
                {
                    i++;
                    dataPtr += nC;
                }
                while (dataPtr[0]==0)
                {
                    i++;
                    pixels_per_bit++;
                    dataPtr += nC;
                }

                pixels_per_bit = 2;

                dataPtr += nC * 2 * pixels_per_bit;
                i += 2 * pixels_per_bit;
                
                int[] first_6_digits_projections = new int[digits * bits * pixels_per_bit];
                int[] second_6_digits_projections = new int[digits * bits * pixels_per_bit];

                initial_position_1 = i;

                final_position_1 = initial_position_1 + digits * bits * pixels_per_bit - 1;

                for(j=0; i<= final_position_1; i++, j++)
                {
                    first_6_digits_projections[j] = dataPtr[0];
                    dataPtr += nC;
                }

                i += 5 * pixels_per_bit;
                dataPtr += nC * 5 * pixels_per_bit;
                initial_position_2 = i;

                final_position_2 = initial_position_2 + digits * bits * pixels_per_bit - 1;

                for(j=0; i<= final_position_2; i++,j++)
                {
                    second_6_digits_projections[j] = dataPtr[0];
                    dataPtr += nC;
                }

                int[] first_6_digits_bits = new int[digits * bits];

                for (i = 0, j = 0; i < first_6_digits_projections.Length; i += pixels_per_bit, j++)
                {

                    if (first_6_digits_projections[i]==255)
                    {
                        first_6_digits_bits[j] = 0;
                    }
                    else
                    {
                        first_6_digits_bits[j] = 1;
                    }
                }

                int[] second_6_digits_bits = new int[digits * bits];
                for (i = 0, j = 0; i < second_6_digits_projections.Length; i += pixels_per_bit, j++)
                {
                    if (second_6_digits_projections[i]==255)
                    {
                        second_6_digits_bits[j] = 0;
                    }
                    else
                    {
                        second_6_digits_bits[j] = 1;
                    }
                }

                returnList.Add(first_6_digits_bits);
                returnList.Add(second_6_digits_bits);

                return returnList;

            }
        }

        // returns barcode_dimensions (int[])
        // barcode_dimensions[0] - barcode_width
        // barcode_dimensions[1] - barcode_height
        public static int[] BarcodeDimensions(int[] vertical_projections, int[] horizontal_projections)
        {
            unsafe
            {
                int barcode_width = 0, barcode_height = 0;
                int[] barcode_dimensions;
                int i;
                
                for(i=0; i<vertical_projections.Length; i++)
                {
                    if(vertical_projections[i]!=0)
                    {
                        barcode_width -= i; // i - initial position of the barcode
                        break; 
                    }
                }

                for(i=vertical_projections.Length-1; i>=0; i--)
                {
                    if(vertical_projections[i]!=0)
                    {
                        barcode_width += i; // i - final position of the barcode
                        break;
                    }
                }

                for(i=0; i<horizontal_projections.Length; i++)
                {
                    if(horizontal_projections[i]!=0)
                    {
                        barcode_height -= i;
                        break;
                    }
                }

                while(horizontal_projections[i]!=0)
                {
                    i++;
                }

                barcode_height += i;
                barcode_dimensions = new int[] { barcode_width, barcode_height };
                return barcode_dimensions;
            }
        }

        class DigitType
        {
            public string Digit { get; set; }
            public string Type { get; set; }
        }

        public static string DecodeDigits(int[] first_6_digits_bits, int[] second_6_digits_bits)
        {
            // agora que temos todos os bits basta descodificar
            // crie-se um dicionario com toda a codificação de CB
            // por enquanto temos apenas a codificação RRRRRRR e façamos apenas a descodificação dos últimos 6 digitos

            int i;
            string bits, aux, digit, first_6_digits, second_6_digits, bar_code_number, aux_2, first_6_digits_type, digit_type, first_digit;

            // consoante os bits de cada digito dos 6 primeiros digitos conseguimos descobrir o 1º digito
            var first_digit_codification = new Dictionary<string, string>()
            {
                {"LLLLLL","0"},
                {"LLGLGG","1"},
                {"LLGGLG","2"},
                {"LLGGGL","3"},
                {"LGLLGG","4"},
                {"LGGLLG","5"},
                {"LGGGLL","6"},
                {"LGLGLG","7"},
                {"LGLGGL","8"},
                {"LGGLGL","9"}
            };

            var digits_codification = new Dictionary<string, DigitType>(){
                // L-code
                {"0001101", new DigitType {Digit="0",Type="L"} },
                {"0011001", new DigitType {Digit="1",Type="L"} },
                {"0010011", new DigitType {Digit="2",Type="L"} },
                {"0111101", new DigitType {Digit="3",Type="L"} },
                {"0100011", new DigitType {Digit="4",Type="L"} },
                {"0110001", new DigitType {Digit="5",Type="L"} },
                {"0101111", new DigitType {Digit="6",Type="L"} },
                {"0111011", new DigitType {Digit="7",Type="L"} },
                {"0110111", new DigitType {Digit="8",Type="L"} },
                {"0001011", new DigitType {Digit="9",Type="L"} },
                // G-code
                {"0100111", new DigitType {Digit="0",Type="G"} },
                {"0110011", new DigitType {Digit="1",Type="G"} },
                {"0011011", new DigitType {Digit="2",Type="G"} },
                {"0100001", new DigitType {Digit="3",Type="G"} },
                {"0011101", new DigitType {Digit="4",Type="G"} },
                {"0111001", new DigitType {Digit="5",Type="G"} },
                {"0000101", new DigitType {Digit="6",Type="G"} },
                {"0010001", new DigitType {Digit="7",Type="G"} },
                {"0001001", new DigitType {Digit="8",Type="G"} },
                {"0010111", new DigitType {Digit="9",Type="G"} },
                // R-code
                {"1110010", new DigitType {Digit="0",Type="R"} },
                {"1100110", new DigitType {Digit="1",Type="R"} },
                {"1101100", new DigitType {Digit="2",Type="R"} },
                {"1000010", new DigitType {Digit="3",Type="R"} },
                {"1011100", new DigitType {Digit="4",Type="R"} },
                {"1001110", new DigitType {Digit="5",Type="R"} },
                {"1010000", new DigitType {Digit="6",Type="R"} },
                {"1000100", new DigitType {Digit="7",Type="R"} },
                {"1001000", new DigitType {Digit="8",Type="R"} },
                {"1110100", new DigitType {Digit="9",Type="R"} }
            };

            // string que contem todos os bits correspondentes aos primeiros 6 digitos
            bits = string.Join("", first_6_digits_bits);

            // vai buscar ao dicionario os digitos correspondentes aos bits
            first_6_digits = digits_codification[bits.Substring(0, 7)].Digit;
            first_6_digits_type = digits_codification[bits.Substring(0, 7)].Type;

            

            for (i = 7; i != 42; i += 7)
            {
                digit = digits_codification[bits.Substring(i, 7)].Digit;
                digit_type = digits_codification[bits.Substring(i, 7)].Type;
                aux = first_6_digits;
                aux_2 = first_6_digits_type;
                first_6_digits = aux + digit;
                first_6_digits_type = aux_2 + digit_type;
            }

            // string que contem todos os bits correspondentes aos segundos 6 digitos
            bits = string.Join("", second_6_digits_bits);

            // vai buscar ao dicionario os digitos correspondentes aos bits
            second_6_digits = digits_codification[bits.Substring(0, 7)].Digit;

            for (i = 7; i != 42; i += 7)
            {
                digit = digits_codification[bits.Substring(i, 7)].Digit;
                aux = second_6_digits;
                second_6_digits = aux + digit;
            }

            // o primeiro digito é obtido através dos primeiros 6 digitos
            // verificar o tipo de codificacao para cada digito (L ou G) e concatenar todos os tipos de forma a obtermos algo do tipo: LLRLRL (feito no for-loop da linha 1914)
            // depois basta ir ao dicionario first_digit_codification obter o digito
            first_digit = first_digit_codification[first_6_digits_type];

            bar_code_number = first_digit + first_6_digits + second_6_digits;

            Console.WriteLine("Descodificação pelas barras:  " + bar_code_number);
            return bar_code_number;

        }

        // receives the projections and angle and locates the barcode
        // returns the centroid of the barcode
        public static Point LocateBarcode(Image<Bgr, byte> img, int[] vertical_projections, int[] horizontal_projections, double angle, int[] barcode_dimensions)
        {
            unsafe
            {

                int i;
                double cx=0, cy=0, area=0;
                Point centroid;
                int barcode_width = barcode_dimensions[0], barcode_height = barcode_dimensions[1];

                for (i=0; i<vertical_projections.Length; i++)
                {
                    cx += (i+1) * vertical_projections[i];
                    area += vertical_projections[i];
                }
                for(i=0; i<horizontal_projections.Length; i++)
                {
                    cy += (i+1) * horizontal_projections[i];
                }

                cx = cx / area;
                cy = cy / area;

                centroid = new Point((int)cx,(int)cy);

                // se a imagem rodou, é necessário aumentar o barcode_width 1.2x e barcode_height 1.1x (tentativa e erro foram os valores que melhor se ajustaram)
                if(angle!=0)
                {
                    cx += cx * 0.05;
                    barcode_width = (int)(barcode_width * 1.2);
                    barcode_height = (int)(barcode_height * 1.1);
                }
                else
                {
                    cy -= cy * 0.05;
                    barcode_height = (int)(barcode_height / 1.2);
                }
                
                MCvBox2D box_barcode = new MCvBox2D(new System.Drawing.PointF((float)cx,(float)cy), new System.Drawing.Size((int)(barcode_width), (int)(barcode_height)),(float)(angle*(-180/Math.PI)));
                img.Draw(box_barcode, new Bgr(0, 0, 255), 2);

                return centroid;
            }
        }

        public static string ReadDigits(Image<Bgr,byte> img, Point centroid, int[] barcode_dimensions, int[] horizontal_projections, double angle)
        {
            unsafe
            {
                int i, y_inicial, y_final, x_inicial, x_final, altura_digitos, x, y;
                bool break_flag = false;
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nC * width;
                int largura_area_digitos, altura_area_digitos;
                string barcode;

                for (i = horizontal_projections.Length - 1; i >= 0; i--)
                {
                    if (horizontal_projections[i] != 0)
                    {
                        break_flag = true;
                        break;
                    }
                    if (break_flag)
                        break;
                }

                y_final = i;
                altura_digitos = (y_final - centroid.Y - barcode_dimensions[1] / 2) * 2;
                y_inicial = y_final - altura_digitos;

                y_inicial -= altura_digitos / 3; // limite superior
                y_final += altura_digitos / 3; // limite inferior
                altura_area_digitos = y_final - y_inicial + 1;

                x_inicial = centroid.X - barcode_dimensions[0]/2 - altura_digitos; // limite esquerda
                x_final = centroid.X + barcode_dimensions[0]/2 - altura_digitos; // limite direita
                largura_area_digitos = x_final - x_inicial + 1;

                // Temos os limites da zona dos digitos
                // Agora temos que segmentar verticalmente esta zona para dividirmos em digitos
                int[] proj_vertical_digitos = new int[x_final-x_inicial+1];

                dataPtr += widthstep * y_inicial;
                dataPtr += nC * x_inicial;

                for(y=0; y<altura_area_digitos; y++)
                {
                    for(x=0; x<largura_area_digitos; x++)
                    {

                        if(dataPtr[0] == 0)
                        {
                            proj_vertical_digitos[x]++;
                        }
                        dataPtr += nC;
                    }
                    dataPtr += widthstep;
                    dataPtr -= largura_area_digitos*nC;
                }

                int[] posicao_digitos = new int[34]; // a mudar
                int[] limites_digitos = new int[18];
                int[] limites = new int[14];
                int j = 0;

                for (i = 0; j<34; i++) {

                    while (proj_vertical_digitos[i] == 0)
                    {
                        i++;
                        //Console.WriteLine(i);
                    }
                    // encontra digito
                    // i posicao inicial
                    posicao_digitos[j] = i - 1;
                    j++;

                    while (proj_vertical_digitos[i] != 0)
                    {
                        i++;
                    } // encontra espaço em branco

                    //posicao_digitos[j] = i;
                    j++;
                }

                posicao_digitos[j-1] = i;

                i = 0;
                foreach(int p in posicao_digitos)
                {
                    if(p!=0) // quanto i toma valores 1, 2, 9 e 10 temos que eliminar pois corresponde a uma proj != 0 mas que nao corresponde a um digito
                    {
                        limites_digitos[i] = p;
                        i++;
                    }
                }

                // quando i toma valores: 1, 2, 9 e 10 temos que elimir pois corresponde a uma proj != 0 mas que nao corresponde a um digito
                i = 0;
                j = 0;
                foreach(int p in limites_digitos)
                {
                    if (i != 1 && i != 2 && i != 9 && i != 10)
                    {
                        limites[j] = p + x_inicial; // não esquecer de adicionar o x_inicial (assim ficamos logo com a posicao nas imagens)
                        j++;
                    }
                    i++;
                }

                List<Image<Bgr, byte>> digitos_segmentados = new List<Image<Bgr, byte>>();
                
                for (i=0; i<limites.Length-1; i++)
                {
                    Image<Bgr, byte> segmento = img.Copy(new System.Drawing.Rectangle(limites[i], y_inicial, limites[i+1] - limites[i] + 1, y_final - y_inicial + 1));
                    digitos_segmentados.Add(segmento);
                }
                barcode = CompareDigits(digitos_segmentados);
                return barcode;
            }
        }

        public static string CompareDigits(List<Image<Bgr, byte>> digitos_segmentados)
        {
            unsafe
            {
                int i,x,y,resto;
                string digitos_lidos = "";

                foreach(Image<Bgr,byte> segmento in digitos_segmentados)
                {
                    MIplImage d_segmentado = segmento.MIplImage;
                    int width = d_segmentado.width, height = d_segmentado.height;
                    int max = 0;
                    string melhor_digito = "";

                    for (i=0; i<50; i++)
                    {

                        string path = @"C:\\Users\\bruno\\Desktop\\SS-labs\\SS_OpenCV_Base\\Imagens\\digitos\\" + i.ToString() + ".png";
                        //string path = @"C:\\Users\\hjoaquim\\Desktop\\SS\\SS_OpenCV_Base\\Imagens\\digitos" + i.ToString() + ".png";
                        Image<Bgr, byte> digito_guardado = new Image<Bgr, byte>(path);
                        Image<Bgr, byte> digito_guardado_rs = digito_guardado.Resize(width, height, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                        ConvertToBW_Otsu(digito_guardado_rs);

                        MIplImage d_guardado = digito_guardado_rs.MIplImage;
                        byte* dataPtrDigitoGuardado = (byte*)d_guardado.imageData.ToPointer();
                        byte* dataPtrDigitoSegmentado = (byte*)d_segmentado.imageData.ToPointer();
                        int padding = d_guardado.widthStep - d_guardado.nChannels * d_guardado.width;
                        int nC = d_guardado.nChannels;
                        int pixeis_iguais = 0;

                        for (y=0; y<height; y++)
                        {
                            for(x=0; x<width; x++)
                            {
                                if(dataPtrDigitoGuardado[0]==dataPtrDigitoSegmentado[0] && dataPtrDigitoGuardado[1]==dataPtrDigitoSegmentado[1] && dataPtrDigitoGuardado[2] == dataPtrDigitoSegmentado[2])
                                {
                                    pixeis_iguais++;
                                }

                                dataPtrDigitoGuardado += nC;
                                dataPtrDigitoSegmentado += nC;
                            }
                            dataPtrDigitoGuardado += padding;
                            dataPtrDigitoSegmentado += padding;
                        }

                        if(pixeis_iguais > max)
                        {
                            max = pixeis_iguais;
                            resto = i % 10;
                            melhor_digito = resto.ToString();
                        }

                    }
                    digitos_lidos += melhor_digito;
                }

                Console.WriteLine("Descodificação pelos numeros: " + digitos_lidos);
                return digitos_lidos;
            }
        }

        // returns angle in radians
        // to-do: need to handle angles greater than 45 degrees
        public static double EixoMomento(Image<Bgr, byte> img)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                int heightstep = widthstep * height;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                double sx=0, sy=0, sxx=0, syy=0, sxy=0, mxx=0, myy=0, mxy=0, angle;
                int area=0;
                int[] proj = new int[width];

                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        if (dataPtr[0] == 0)
                        {
                            proj[x]++;
                            sxy += (x+1) * (y+1);
                            sx += x+1;
                            sxx += Math.Pow(x+1, 2);
                            sy += y+1;
                            syy += Math.Pow(y+1, 2);

                        }
                        dataPtr += nC;
                    }
                    dataPtr += padding;
                }

                for (x = 0; x < proj.Length; x++)
                    area += proj[x];

                mxx = sxx - (Math.Pow(sx, 2) / area);
                myy = syy - (Math.Pow(sy, 2) / area);
                mxy = sxy - ((sx * sy) / area);

                y = (int)(mxx - myy + Math.Sqrt(Math.Pow((mxx - myy), 2)) + 4 * Math.Pow(mxy, 2) / (2 * mxy));
                angle = Math.Atan((mxx - myy + Math.Sqrt((mxx - myy) * (mxx - myy) + 4 * mxy * mxy)) / (2 * mxy));

                angle = (angle * 180.0) / Math.PI;

                if (angle < 0)
                    angle = -(90 + (angle));
                else
                    angle = 90 - (angle);

                // um angulo perto de zero arredonda-se para zero
                if (angle > -1 && angle < 1)
                    angle = 0;

                angle *= Math.PI / 180;
                angle *= -1;

                return angle;

            }

        }


    } 

}

        