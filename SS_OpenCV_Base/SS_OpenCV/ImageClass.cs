using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using ZedGraph;
using System.Xml;
using System.Windows.Forms;

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

                            // demora mais tempo mas menos codigo ?
                            /*
                            aux[0] = blue * contrast + bright;
                            aux[1] = green * contrast + bright;
                            aux[2] = red * contrast + bright;

                            for (i = 0; i < 3;i++)
                            {
                                if (aux[i] < 0)
                                {
                                    aux[i] = 0;
                                }
                                else if (aux[i] > 255)
                                {
                                    aux[i] = 255;
                                }
                            }

                            dataPtr[0] = (byte)Math.Round(aux[0]);
                            dataPtr[1] = (byte)Math.Round(aux[1]);
                            dataPtr[2] = (byte)Math.Round(aux[2]);
                            */

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
        { // imgCopy original
            unsafe
            {
                // obter apontador do inicio da imagem
                MIplImage m = imgCopy.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                
                MIplImage m_dest = img.MIplImage;
                byte* dataPtr_dest = (byte*)m_dest.imageData.ToPointer();

                int w = imgCopy.Width;
                int h = imgCopy.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                byte blue, green, red;
                int y_orig, x_orig;
                int padding = m_dest.widthStep - m_dest.nChannels * m_dest.width;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        x_orig = x - dx;
                        y_orig = y - dy;

                        // calcula endereço do pixel no ponto (x,y)
                        blue = (byte)(dataPtr + y_orig * widthstep + x_orig * nC)[0];
                        green = (byte)(dataPtr + y_orig * widthstep + x_orig * nC)[1];
                        red = (byte)(dataPtr + y_orig * widthstep + x_orig * nC)[2];

                        // verifica os limites
                        if(x_orig < 0 || y_orig < 0 || x_orig > w || y_orig > h)
                        {
                            blue = 0;
                            green = 0;
                            red = 0;
                        }

                        dataPtr_dest[0] = blue;
                        dataPtr_dest[1] = green;
                        dataPtr_dest[2] = red;

                        dataPtr_dest += nC;

                    }
                    dataPtr_dest += padding;
                }
            }
        }
    }
}
