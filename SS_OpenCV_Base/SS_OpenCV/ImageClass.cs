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
                            
                            if(blue_updated > 255)
                            {
                                blue_updated = 255;
                            }
                            else if(blue_updated < 0)
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

                if(nChannels == 3) // RGB   
                {
                    for(y = 0; y < height; y++)
                    {
                        for(x = 0; x < width; x++)
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

                if(nChannels == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            x_orig = (int)Math.Round(x / scaleFactor);
                            y_orig = (int)Math.Round(y / scaleFactor);

                            if(x_orig < 0 || y_orig < 0 || x_orig >= width || y_orig >= height)
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
                
                if(nC == 3)
                {
                    // without borders
                    dataPtr += widthstep + nC;
                    dataPtr_dest += widthstep + nC;
                    
                    for (y=1; y<height-1; y++)
                    {
                        for (x=1; x<width-1; x++)
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
                    blue = (dataPtr_dest[0] * (matrix[0,0] + matrix[0,1] + matrix[1,0] + matrix[1,1]) + (dataPtr_dest+nC)[0] * (matrix[0,2] + matrix[1,2]) + (dataPtr_dest+widthstep-nC)[0] * matrix[2,0] + (dataPtr_dest+widthstep)[0] * matrix[2,1] + (dataPtr_dest+widthstep+nC)[0] * matrix[2,2]) / matrixWeight;
                    green = (dataPtr_dest[1] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) + (dataPtr_dest + nC)[1] * (matrix[0, 2] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[1] * matrix[2, 0] + (dataPtr_dest + widthstep)[1] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[1] * matrix[2, 2]) / matrixWeight;
                    red = (dataPtr_dest[2] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) + (dataPtr_dest + nC)[2] * (matrix[0, 2] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[2] * matrix[2, 0] + (dataPtr_dest + widthstep)[2] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[2] * matrix[2, 2]) / matrixWeight;

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    dataPtr += nC;
                    dataPtr_dest += nC;

                    // 1ª linha (sem cantos)
                    for(x=1; x<width-1; x++)
                    {
                        blue = ((dataPtr_dest-nC)[0] * (matrix[0,0] + matrix[1,0]) + dataPtr_dest[0] * (matrix[0,1] + matrix[1,1]) + (dataPtr_dest+nC)[0] * (matrix[1,2] + matrix[0,2]) + (dataPtr_dest+widthstep-nC)[0] * matrix[2,0] + (dataPtr_dest+widthstep)[0] * matrix[2,1] + (dataPtr_dest+widthstep+nC)[0] * matrix[2,2]) / matrixWeight;
                        green = ((dataPtr_dest - nC)[1] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_dest[1] * (matrix[0, 1] + matrix[1, 1]) + (dataPtr_dest + nC)[1] * (matrix[1, 2] + matrix[0, 2]) + (dataPtr_dest + widthstep - nC)[1] * matrix[2, 0] + (dataPtr_dest + widthstep)[1] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[1] * matrix[2, 2]) / matrixWeight;
                        red = ((dataPtr_dest - nC)[2] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_dest[2] * (matrix[0, 1] + matrix[1, 1]) + (dataPtr_dest + nC)[2] * (matrix[1, 2] + matrix[0, 2]) + (dataPtr_dest + widthstep - nC)[2] * matrix[2, 0] + (dataPtr_dest + widthstep)[2] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[2] * matrix[2, 2]) / matrixWeight;

                        dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                        dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                        dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                        dataPtr += nC;
                        dataPtr_dest += nC;
                    }

                    // canto superior direito
                    blue = ((dataPtr_dest-nC)[0] * (matrix[0,0] + matrix[1,0]) + dataPtr_dest[0] * (matrix[0,1] + matrix[0,2] + matrix[1,1] + matrix[1,2]) + (dataPtr_dest+widthstep-nC)[0] * matrix[2,0] + (dataPtr_dest+widthstep)[0] * matrix[2,1] + (dataPtr_dest+widthstep+nC)[0] * matrix[2,2]) / matrixWeight;
                    green = ((dataPtr_dest - nC)[1] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_dest[1] * (matrix[0, 1] + matrix[0, 2] + matrix[1, 1] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[1] * matrix[2, 0] + (dataPtr_dest + widthstep)[1] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[1] * matrix[2, 2]) / matrixWeight;
                    red = ((dataPtr_dest - nC)[2] * (matrix[0, 0] + matrix[1, 0]) + dataPtr_dest[2] * (matrix[0, 1] + matrix[0, 2] + matrix[1, 1] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[2] * matrix[2, 0] + (dataPtr_dest + widthstep)[2] * matrix[2, 1] + (dataPtr_dest + widthstep + nC)[2] * matrix[2, 2]) / matrixWeight;

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                }

                dataPtr += widthstep;
                dataPtr_dest += widthstep;

                // coluna lateral direita (sem cantos)
                for(y=1; y<height-1;y++)
                {
                    blue = ((dataPtr_dest-widthstep-nC)[0] * matrix[0,0] + (dataPtr_dest-widthstep)[0] * (matrix[0,1] + matrix[0,2]) + (dataPtr_dest-nC)[0] * matrix[1,0] + dataPtr_dest[0] * (matrix[1,1] + matrix[1,2]) + (dataPtr_dest+widthstep-nC)[0] * matrix[2,0] + (dataPtr_dest+widthstep)[0] * (matrix[2,1] + matrix[2,2])) / matrixWeight;
                    green = ((dataPtr_dest - widthstep - nC)[1] * matrix[0, 0] + (dataPtr_dest - widthstep)[1] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_dest - nC)[1] * matrix[1, 0] + dataPtr_dest[1] * (matrix[1, 1] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[1] * matrix[2, 0] + (dataPtr_dest + widthstep)[1] * (matrix[2, 1] + matrix[2, 2])) / matrixWeight;
                    red = ((dataPtr_dest - widthstep - nC)[2] * matrix[0, 0] + (dataPtr_dest - widthstep)[2] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_dest - nC)[2] * matrix[1, 0] + dataPtr_dest[2] * (matrix[1, 1] + matrix[1, 2]) + (dataPtr_dest + widthstep - nC)[2] * matrix[2, 0] + (dataPtr_dest + widthstep)[2] * (matrix[2, 1] + matrix[2, 2])) / matrixWeight;

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    dataPtr += widthstep;
                    dataPtr_dest += widthstep;
                }

                // canto inferior direito
                blue = ((dataPtr_dest-widthstep-nC)[0] * matrix[0,0] + (dataPtr_dest-widthstep)[0] * (matrix[0,1] + matrix[0,2]) + (dataPtr_dest-nC)[0] * (matrix[1,0] + matrix[2,0]) + dataPtr_dest[0] * (matrix[1,1] + matrix[1,2] + matrix[2,1] + matrix[2,2])) / matrixWeight;
                green = ((dataPtr_dest - widthstep - nC)[1] * matrix[0, 0] + (dataPtr_dest - widthstep)[1] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_dest - nC)[1] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_dest[1] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2])) / matrixWeight;
                red = ((dataPtr_dest - widthstep - nC)[2] * matrix[0, 0] + (dataPtr_dest - widthstep)[2] * (matrix[0, 1] + matrix[0, 2]) + (dataPtr_dest - nC)[2] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_dest[2] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2])) / matrixWeight;

                dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                dataPtr -= nC;
                dataPtr_dest -= nC;

                // última linha (sem cantos)
                for(x=1; x<width-1; x++)
                {
                    blue = ((dataPtr_dest-widthstep-nC)[0] * matrix[0,0] + (dataPtr_dest-widthstep)[0] * matrix[0,1] + (dataPtr_dest-widthstep+nC)[0] * matrix[0,2] + (dataPtr_dest-nC)[0] * (matrix[1,0] + matrix[2,0]) + dataPtr_dest[0]* (matrix[1,1] + matrix[2,1]) + (dataPtr_dest+nC)[0] * (matrix[1,2] + matrix[2,2])) / matrixWeight;
                    green = ((dataPtr_dest - widthstep - nC)[1] * matrix[0, 0] + (dataPtr_dest - widthstep)[1] * matrix[0, 1] + (dataPtr_dest - widthstep + nC)[1] * matrix[0, 2] + (dataPtr_dest - nC)[1] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_dest[1] * (matrix[1, 1] + matrix[2, 1]) + (dataPtr_dest + nC)[1] * (matrix[1, 2] + matrix[2, 2])) / matrixWeight;
                    red = ((dataPtr_dest - widthstep - nC)[2] * matrix[0, 0] + (dataPtr_dest - widthstep)[2] * matrix[0, 1] + (dataPtr_dest - widthstep + nC)[2] * matrix[0, 2] + (dataPtr_dest - nC)[2] * (matrix[1, 0] + matrix[2, 0]) + dataPtr_dest[2] * (matrix[1, 1] + matrix[2, 1]) + (dataPtr_dest + nC)[2] * (matrix[1, 2] + matrix[2, 2])) / matrixWeight;

                    dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                    dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                    dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                    dataPtr -= nC;
                    dataPtr_dest -= nC;

                }

                // canto inferior esquerdo
                blue = ((dataPtr_dest-widthstep)[0] * (matrix[0,0] + matrix[0,1]) + (dataPtr_dest-widthstep+nC)[0] * matrix[0,2] + (dataPtr_dest+nC)[0] * (matrix[1,2] + matrix[2,2]) + dataPtr_dest[0] * (matrix[1,0] + matrix[1,1] + matrix[2,0] + matrix[2,1])) / matrixWeight;
                green = ((dataPtr_dest - widthstep)[1] * (matrix[0, 0] + matrix[0, 1]) + (dataPtr_dest - widthstep + nC)[1] * matrix[0, 2] + (dataPtr_dest + nC)[1] * (matrix[1, 2] + matrix[2, 2]) + dataPtr_dest[1] * (matrix[1, 0] + matrix[1, 1] + matrix[2, 0] + matrix[2, 1])) / matrixWeight;
                red = ((dataPtr_dest - widthstep)[2] * (matrix[0, 0] + matrix[0, 1]) + (dataPtr_dest - widthstep + nC)[2] * matrix[0, 2] + (dataPtr_dest + nC)[2] * (matrix[1, 2] + matrix[2, 2]) + dataPtr_dest[2] * (matrix[1, 0] + matrix[1, 1] + matrix[2, 0] + matrix[2, 1])) / matrixWeight;

                dataPtr[0] = (byte)Math.Round(blue < 0 ? 0 : blue > 255 ? 255 : blue);
                dataPtr[1] = (byte)Math.Round(green < 0 ? 0 : green > 255 ? 255 : green);
                dataPtr[2] = (byte)Math.Round(red < 0 ? 0 : red > 255 ? 255 : red);

                dataPtr -= widthstep;
                dataPtr_dest -= widthstep;

                // coluna lateral esquerda (sem cantos)
                for(y=1; y<height-1;y++)
                {

                    blue = ((dataPtr_dest-widthstep)[0] * (matrix[0,0] + matrix[0,1]) + (dataPtr_dest-widthstep+nC)[0] * matrix[0,2] + dataPtr_dest[0] * (matrix[1,0] + matrix[1,1]) + (dataPtr_dest + nC)[0] * matrix[1,2] + (dataPtr_dest+widthstep)[0] * (matrix[2,0] + matrix[2,1]) + (dataPtr_dest+widthstep+nC)[0] * matrix[2,2]) / matrixWeight;
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
                        blue_y = (dataPtr_dest - nC)[0] + dataPtr_dest[0] * 2 + (dataPtr_dest + nC)[0] - ((dataPtr_dest - widthstep - nC)[0] + (dataPtr_dest-widthstep)[0]*2 + (dataPtr_dest - widthstep + nC)[0]);

                        green_x = (dataPtr_dest - widthstep - nC)[1] + (dataPtr_dest - nC)[1] * 3 - ((dataPtr_dest - widthstep + nC)[1] + (dataPtr_dest + nC)[1] * 3);
                        green_y = (dataPtr_dest - nC)[1] + dataPtr_dest[1] * 2 + (dataPtr_dest + nC)[1] - ((dataPtr_dest - widthstep - nC)[1] + (dataPtr_dest - widthstep)[1]*2 + (dataPtr_dest - widthstep + nC)[1]);

                        red_x = (dataPtr_dest - widthstep - nC)[2] + (dataPtr_dest - nC)[2] * 3 - ((dataPtr_dest - widthstep + nC)[2] + (dataPtr_dest + nC)[2] * 3);
                        red_y = (dataPtr_dest - nC)[2] + dataPtr_dest[2] * 2 + (dataPtr_dest + nC)[2] - ((dataPtr_dest - widthstep - nC)[2] + (dataPtr_dest - widthstep)[2]*2 + (dataPtr_dest - widthstep + nC)[2]);

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

                        blue_x = (dataPtr_dest - widthstep)[0] + dataPtr_dest[0] * 2 + (dataPtr_dest + widthstep)[0] - ((dataPtr_dest-widthstep+nC)[0] + (dataPtr_dest+nC)[0]*2 + (dataPtr_dest+widthstep+nC)[0]);
                        blue_y = (dataPtr_dest + widthstep)[0] * 3 + (dataPtr_dest + widthstep + nC)[0] - ((dataPtr_dest-widthstep)[0]*3 + (dataPtr_dest-widthstep+nC)[0]);

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

                    for(x = 0; x < (width-1); x++)
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

                    for (y=1; y < (height-1); y++)
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

                    for(x=1; x < (width-1); x++)
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

                    for(y=1; y< (height-1); y++)
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
                
                if(nC == 3)
                {
                    for(y=0; y<height; y++)
                    {
                        for(x=0; x<width; x++)
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
                int[,] matrix = new int[3,256];

                if (nC == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            
                            matrix[0,blue]++;
                            matrix[1,green]++;
                            matrix[2,red]++;
            
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

                            gray = (byte)Math.Round(((int)blue+green+red) / 3.0);

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

                if(nC == 3)
                {
                    for(y=0; y<height;y++)
                    {
                        for(x=0; x<width; x++)
                        {
                            Y = dataPtr[0];

                            vector[Y]++;

                            dataPtr += nC;
                        }
                        dataPtr += padding;
                    }

                    for(i=0; i<=255; i++)
                    {
                        if (acumHistMin == 0)
                            acumHistMin = vector[i];

                        acumHist += vector[i];

                        newH[i] = Math.Round((((double)acumHist-acumHistMin)/(wXh - acumHistMin))*255);
                    }

                    dataPtr = (byte*)m.imageData.ToPointer();

                    for(y=0; y<height; y++)
                    {
                        for(x=0; x<width; x++)
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

                for(y=0; y<height;y++)
                {
                    for(x=0; x<width; x++)
                    {

                        blue = dataPtr[0];
                        green = dataPtr[1];
                        red = dataPtr[2];

                        gray = (byte)Math.Round(((int)blue + green + red)/ 3.0);

                        gray = (byte)(gray<=threshold ? 0 : 255);

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
                int threshold = OTSU(hist,nPixels);
                ImageClass.ConvertToBW(img, threshold);
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
        /// <param name="bc_size2">output the size of the second barcode</param>
        /// <param name="bc_image2">output a string containing the second barcode read from the bars. It returns null, if it does not exist.</param>
        /// <param name="bc_number2">output a string containing the second barcode read from the numbers in the bottom. It returns null, if it does not exist.</param>
        /// <returns>image with barcodes detected</returns>
        public static Image<Bgr, byte> BarCodeReader(Image<Bgr, byte> img, int type, out Point bc_centroid1, out Size bc_size1, out string bc_image1, out string bc_number1, out Point bc_centroid2, out Size bc_size2, out string bc_image2, out string bc_number2)
        {
            int[][] projections;
            int[] v_projections, h_projections;

            projections = Segmentation(img);
            v_projections = projections[0];
            h_projections = projections[1];

            float[] centroid = CalculateCentroid(img, v_projections, h_projections);

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
            
            return img;
            
        }

        // returns int[][] projections
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

                for (y=0; y<height; y++)
                {
                    for(x=0; x<width; x++)
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
        
        public static float[] CalculateCentroid(Image<Bgr,byte> img, int[] vertical_projections, int[] horizontal_projections)
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
                int x, y, i, num_x=0, num_y=0, den_x=0, den_y=0;
                float c_x, c_y;
                float[] centroid = new float[2];


                for(i=0; i<vertical_projections.Length; i++)
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


    }
}

        