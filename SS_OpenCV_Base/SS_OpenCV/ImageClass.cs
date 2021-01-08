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
            int[][] projections;
            int[] vertical_projections, horizontal_projections;

            projections = Segmentation(img);
            vertical_projections = projections[0];
            horizontal_projections = projections[1];

            // first barcode
            bc_image1 = "5601212323434";
            bc_number1 = "9780201379624";
            bc_centroid1 = new Point(130, 60);
            //bc_centroid1 = new Point((int)centroid[0], (int)centroid[1]);
            bc_size1 = new Size(200, 80);

            //second barcode
            bc_image2 = null;
            bc_number2 = null;
            bc_centroid2 = Point.Empty;
            bc_size2 = Size.Empty;
            // draw the rectangle over the destination image
            img.Draw(new Rectangle(bc_centroid1.X - bc_size1.Width / 2,
            bc_centroid1.Y - bc_size1.Height / 2,
            bc_size1.Width,
            bc_size1.Height),
            new Bgr(0, 255, 0), 3);

            ProjectionsToBits(vertical_projections);

            return img;

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

        public static void ProjectionsToBits(int[] vertical_projections)
        {

            int initial_position_1, final_position_1, initial_position_2, final_position_2;
            int pixels_per_bit = 0;
            int digits = 6;
            int bits = 7;
            int[] first_6_digits_projections;
            int[] second_6_digits_projections;
            int i, j;

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

            DecodeDigits(first_6_digits_bits, second_6_digits_bits);

        }

        class DigitType
        {
            public string Digit { get; set; }
            public string Type { get; set; }
        }

        public static void DecodeDigits(int[] first_6_digits_bits, int[] second_6_digits_bits)
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

            Console.WriteLine(bar_code_number);

        }

        // receives the projections and angle and locates the barcode
        public static void LocateBarcode(Image<Bgr, byte> img, int[] vertical_projections, int[] horizontal_projections, double angle)
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
                int x, y, i;
                double cx=0, cy=0, area=0;

                for(i=0; i<vertical_projections.Length; i++)
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

                Console.WriteLine(angle);
                
                MCvBox2D mybox = new MCvBox2D(new System.Drawing.PointF((float)cx,(float)cy), new System.Drawing.Size(200, 90),(float)(angle*(-180/Math.PI)));
                img.Draw(mybox, new Bgr(0, 0, 255), 2);
            }
        }

        // to delete if not used
        public static double FindAngle(Image<Bgr, byte> img, List<Point> listCorners)
        {
            unsafe
            {
                
                Point corner1, corner2, corner3, corner4, point1, point2, auxPoint, centroid;
                double angleRad, angleDeg;

                corner1 = listCorners[0];
                corner2 = listCorners[1];
                corner3 = listCorners[2];
                corner4 = listCorners[3];
                point1 = listCorners[4];
                point2 = listCorners[5];
                centroid = listCorners[6];

                auxPoint = new Point(point2.X, centroid.Y);

                angleRad = Math.Atan2(auxPoint.Y - centroid.Y, auxPoint.X - centroid.X) - Math.Atan2(point2.Y - centroid.Y, point2.X - centroid.X);
                angleDeg = angleRad* (180 / Math.PI);

                Console.WriteLine("Corner 1: " + corner1);
                Console.WriteLine("Corner 2: " + corner2);
                Console.WriteLine("Corner 3: " + corner3);
                Console.WriteLine("Corner 4: " + corner4);
                Console.WriteLine("Point  1: " + point1);
                Console.WriteLine("Point  2: " + point2);
                Console.WriteLine("Centroid: " + centroid);
                Console.WriteLine("Angle   : " + angleDeg);

                return angleRad;
            }
        }

        // returns angle in radians
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

                angle *= Math.PI / 180;
                angle *= -1;

                return angle;

            }

        }

        // returns corners and some points from the rectangle correspondent to the barcode
        // listCorners[0..3] - corner1, corner2, corner3, corner4
        // listCorners[4] - point1
        // listCorners[5] - point2
        // listCorners[6] - centroid

        public static List<Point> FindCorners(Image<Bgr, byte> img)
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
                Point corner1 = Point.Empty, corner2 = Point.Empty, corner3 = Point.Empty, corner4 = Point.Empty, point1 = Point.Empty, point2 = Point.Empty, auxPoint = Point.Empty, centroid = Point.Empty;
                bool breakFlag = false;
                int aux_x, aux_y;
                var listCorners = new List<Point>();

                // Primeiro obtemos as coordenadas dos 4 cantos do retangulo correspondente ao codigo de barras
                // canto superior - corner1
                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {

                        if (dataPtr[0] == 255) // basta verificar uma componente visto que já temos uma imagem binarizada
                        {
                            corner1 = new Point(x, y);
                            breakFlag = true; // flag used for breaking the outer for loop
                            break;
                        }
                        dataPtr += nC;
                    }

                    if (breakFlag)
                    {
                        breakFlag = false;
                        break;

                    }
                    dataPtr += padding;
                }

                dataPtr = (byte*)m.imageData.ToPointer(); // reset the pointer 

                // canto à esquerda - corner2
                for (x = 0; x < width; x++)
                {
                    for (y = 0; y < height; y++)
                    {
                        if (dataPtr[0] == 255)
                        {
                            corner2 = new Point(x, y);
                            breakFlag = true;
                            break;
                        }

                        dataPtr += widthstep;
                    }

                    if (breakFlag)
                    {
                        breakFlag = false;
                        break;
                    }

                    dataPtr -= heightstep;
                    dataPtr += nC;

                }

                dataPtr = (byte*)m.imageData.ToPointer(); // reset the pointer
                dataPtr += (widthstep - padding);

                // canto à direita - corner3
                for (x = width; x > 0; x--)
                {
                    for (y = 0; y < height; y++)
                    {
                        if (dataPtr[0] == 255)
                        {
                            corner3 = new Point(x, y);
                            breakFlag = true;
                            break;
                        }

                        dataPtr += widthstep;
                    }

                    if (breakFlag)
                    {
                        breakFlag = false;
                        break;
                    }

                    dataPtr -= heightstep;
                    dataPtr -= nC;
                }

                dataPtr = (byte*)m.imageData.ToPointer(); // reset the pointer
                dataPtr += heightstep;
                dataPtr += (widthstep - padding);

                // canto inferior - corner4 
                for (y = height; y > 0; y--)
                {
                    for (x = width; x > 0; x--)
                    {
                        if (dataPtr[0] == 255)
                        {
                            corner4 = new Point(x, y);
                            breakFlag = true;
                            break;
                        }

                        dataPtr -= nC;
                    }

                    if (breakFlag)
                    {
                        breakFlag = false;
                        break;
                    }

                    dataPtr -= padding;
                }

                if (corner1.X > corner4.X)
                {
                    aux_x = (corner4.X - corner2.X) / 2;
                    aux_y = (corner4.Y - corner2.Y) / 2;
                    point1 = new Point(corner2.X + aux_x, corner2.Y + aux_y);

                    aux_x = (corner3.X - corner1.X) / 2;
                    aux_y = (corner3.Y - corner1.Y) / 2;
                    point2 = new Point(corner1.X + aux_x, corner1.Y + aux_y);

                    centroid.X = point1.X + (point2.X - point1.X) / 2;
                    centroid.Y = point2.Y + (point1.Y - point2.Y) / 2;

                }
                else
                {
                    aux_x = (corner1.X - corner2.X) / 2;
                    aux_y = (corner2.Y - corner1.Y) / 2;
                    point1 = new Point(corner2.X + aux_x, corner1.Y + aux_y);

                    aux_x = (corner3.X - corner4.X) / 2;
                    aux_y = (corner4.Y - corner3.Y) / 2;
                    point2 = new Point(corner4.X + aux_x, corner3.Y + aux_y);

                    centroid.X = point1.X + (point2.X - point1.X) / 2;
                    centroid.Y = point1.Y + (point2.Y - point1.Y) / 2;

                }

                listCorners.Add(corner1);
                listCorners.Add(corner2);
                listCorners.Add(corner3);
                listCorners.Add(corner4);
                listCorners.Add(point1);
                listCorners.Add(point2);
                listCorners.Add(centroid);

                return listCorners;
            }
        }

        // TO DO
        public static void LerDigitos(int[] vertical_projections, int[] horizontal_projections)
        {
            unsafe {

            } 
        } 



    } 

}

        