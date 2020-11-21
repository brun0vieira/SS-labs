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
                int x, y, i;
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
                double dx, dy;

                dx = centerX - (width / scaleFactor) / 2.0;
                dy = centerY - (height / scaleFactor) / 2.0;

                if (nChannels == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            x_orig = (int)Math.Round(x / scaleFactor + dx);
                            y_orig = (int)Math.Round(y / scaleFactor + dy);

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


                        blue_x = (dataPtr_dest - nC)[0] * 3 + (dataPtr - nC + widthstep)[0] - ((dataPtr_dest + nC)[0] * 3 + (dataPtr_dest + nC + widthstep)[0]);
                        blue_y = (dataPtr_dest - nC + widthstep)[0] + (dataPtr + widthstep)[0] * 2 + (dataPtr_dest + nC + widthstep)[0] - ((dataPtr_dest - nC)[0] + dataPtr_dest[0] * 2 + (dataPtr_dest + nC)[0]);

                        green_x = (dataPtr_dest - nC)[1] * 3 + (dataPtr - nC + widthstep)[1] - ((dataPtr_dest + nC)[1] * 3 + (dataPtr_dest + nC + widthstep)[1]);
                        green_y = (dataPtr_dest - nC + widthstep)[1] + (dataPtr + widthstep)[1] * 2 + (dataPtr_dest + nC + widthstep)[1] - ((dataPtr_dest - nC)[1] + dataPtr_dest[1] * 2 + (dataPtr_dest + nC)[1]);

                        red_x = (dataPtr_dest - nC)[2] * 3 + (dataPtr - nC + widthstep)[2] - ((dataPtr_dest + nC)[2] * 3 + (dataPtr_dest + nC + widthstep)[2]);
                        red_y = (dataPtr_dest - nC + widthstep)[2] + (dataPtr + widthstep)[2] * 2 + (dataPtr_dest + nC + widthstep)[2] - ((dataPtr_dest - nC)[2] + dataPtr_dest[2] * 2 + (dataPtr_dest + nC)[2]);

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

                        blue_x = (dataPtr_dest - nC - widthstep)[0] + (dataPtr_dest - nC)[0] * 2 + (dataPtr - nC + widthstep)[0] - ((dataPtr - widthstep)[0] + dataPtr_dest[0] * 2 + (dataPtr_dest + widthstep)[0]);
                        blue_y = (dataPtr_dest - nC + widthstep)[0] + (dataPtr_dest + widthstep)[0] * 3 - ((dataPtr_dest - nC - widthstep)[0] + (dataPtr_dest - widthstep)[0] * 3);

                        green_x = (dataPtr_dest - nC - widthstep)[1] + (dataPtr_dest - nC)[1] * 2 + (dataPtr - nC + widthstep)[1] - ((dataPtr - widthstep)[1] + dataPtr_dest[1] * 2 + (dataPtr_dest + widthstep)[1]);
                        green_y = (dataPtr_dest - nC + widthstep)[1] + (dataPtr_dest + widthstep)[1] * 3 - ((dataPtr_dest - nC - widthstep)[1] + (dataPtr_dest - widthstep)[1] * 3);

                        red_x = (dataPtr_dest - nC - widthstep)[2] + (dataPtr_dest - nC)[2] * 2 + (dataPtr - nC + widthstep)[2] - ((dataPtr - widthstep)[2] + dataPtr_dest[2] * 2 + (dataPtr_dest + widthstep)[2]);
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
                        blue_y = (dataPtr_dest - nC)[0] + dataPtr_dest[0] * 2 + (dataPtr_dest + nC)[0] - ((dataPtr_dest - widthstep - nC)[0] + (dataPtr_dest-widthstep)[0] + (dataPtr_dest - widthstep + nC)[0]);

                        green_x = (dataPtr_dest - widthstep - nC)[1] + (dataPtr_dest - nC)[1] * 3 - ((dataPtr_dest - widthstep + nC)[1] + (dataPtr_dest + nC)[1] * 3);
                        green_y = (dataPtr_dest - nC)[1] + dataPtr_dest[1] * 2 + (dataPtr_dest + nC)[1] - ((dataPtr_dest - widthstep - nC)[1] + (dataPtr_dest - widthstep)[1] + (dataPtr_dest - widthstep + nC)[1]);

                        red_x = (dataPtr_dest - widthstep - nC)[2] + (dataPtr_dest - nC)[2] * 3 - ((dataPtr_dest - widthstep + nC)[2] + (dataPtr_dest + nC)[2] * 3);
                        red_y = (dataPtr_dest - nC)[2] + dataPtr_dest[2] * 2 + (dataPtr_dest + nC)[2] - ((dataPtr_dest - widthstep - nC)[2] + (dataPtr_dest - widthstep)[2] + (dataPtr_dest - widthstep + nC)[2]);

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

    }
}
