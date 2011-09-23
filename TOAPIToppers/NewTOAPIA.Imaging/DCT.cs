using System;

using NewTOAPIA;

namespace NewTOAPIA.Imaging
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Drawing;

    public class DCT
    {
        #region Fields
        int fBlockSize;
        double[,] fBasis;
        #endregion

        #region Constructors
        public DCT()
            : this(8)
        {
        }

        public DCT(int blockSize)
        {
            fBlockSize = blockSize;
            fBasis = CalculateBasis(fBlockSize);
        }
        #endregion

        #region Properties
        public int BlockSize
        {
            get { return fBlockSize; }
        }
        #endregion

        #region Static Methods
        public double[,] CalculateBasis(int N)
        {
            double [,] cosBuffer = new double[N, N];

            //
            // compute the table of basis functions and
            // store the result in the cosBuffer
            //
            for (int m = 0; m < N; ++m)
            {
                for (int n = 0; n < N; ++n)
                {
                    double result;
                    if (n == 0)
                    {
                        result =
                           Math.Sqrt(1.0 / N) *
                           Math.Cos(((2 * m + 1) * n * Math.PI) / (2.0 * N));
                    }
                    else
                    {
                        result =
                           Math.Sqrt(2.0 / N) *
                           Math.Cos(((2 * m + 1) * n * Math.PI) / (2.0 * N));
                    }
                    cosBuffer[m, n] = result;
                }
            }

            return cosBuffer;
        }
        #endregion

        #region Forward DCT
        /// <summary>
        /// Compute the Forward DCT
        /// </summary>
        /// <param name="imgBuffer">f(x, y); source image</param>
        /// <param name="dctBuffer">F(u, v); DCT coefficients</param>
        /// <param name="fBasis">DCT basis functions</param>
        public PixelArray ForwardDCT(PixelArray imgBuffer)
        {
            int N = fBlockSize;

            // extract the dimensions of the image
            int width = imgBuffer.Width;
            int height = imgBuffer.Height;

            // Create the array of DCT coefficients 
            PixelArray<RGBd> dctBuffer = new PixelArray<RGBd>(width, height);

            // intermediate result (an NxN block)
            PixelArray<RGBd> dctBlock = new PixelArray<RGBd>(N, N);

            //
            // for every NxN block of the image
            //
            for (int y = 0; y < height; y += N)
            {
                for (int x = 0; x < width; x += N)
                {
                    //
                    // do the first matrix multiplication
                    // ([fBasis]' [imgBuffer])
                    // and store the results in dctBlock
                    //
                    for (int col = 0; col < N; ++col)
                    {
                        for (int row = 0; row < N; ++row)
                        {
                            RGBd result = new RGBd(0.0, 0.0, 0.0);
                            for (int index = 0; index < N; ++index)
                            {
                                BGRb pixel = new BGRb(imgBuffer.GetPixelBytes(x + index, y + row));
                                double cosVal_transpose = fBasis[index, col];

                                result.red += cosVal_transpose * pixel.Red;
                                result.green += cosVal_transpose * pixel.Green;
                                result.blue += cosVal_transpose * pixel.Blue;
                            }
                            dctBlock.SetPixel(col, row, result);
                        }
                    }

                    //
                    // do the second matrix multiplication
                    // ([dctBlock] [fBasis])
                    // and store the results in dctBuffer
                    //
                    for (int col2 = 0; col2 < N; ++col2)
                    {
                        for (int row2 = 0; row2 < N; ++row2)
                        {
                            RGBd result = new RGBd(0.0, 0.0, 0.0);
                            for (int index = 0; index < N; ++index)
                            {
                                RGBd coeff = new RGBd(dctBlock.GetPixelBytes(col2, index));
                                double cosVal = fBasis[index, row2];

                                result.red += coeff.Red * cosVal;
                                result.green += coeff.Green * cosVal;
                                result.blue += coeff.Blue * cosVal;
                            }
                            dctBuffer.SetPixel(x + col2, y + row2, result);
                        }
                    }
                }
            }

            return dctBuffer;
        }
        #endregion

        #region Inverse DCT
        /// <summary>
        /// Compute the Inverse DCT
        /// </summary>
        /// <param name="dctBuffer">F(u, v); DCT coefficients</param>
        /// <param name="fBasis">DCT basis functions</param>
        /// <param name="imgBuffer">f(x, y); destination image</param>
        public PixelArray InverseDCT(PixelArray dctBuffer)
        {
            //int N = fBlockSize;

            // extract the dimensions of the DCT buffer
            int width = dctBuffer.Width;
            int height = dctBuffer.Height;

            PixelArray imgBuffer = new PixelArray(width, height, PixelType.BGRb, PixmapOrientation.TopToBottom, 1);

            // intermediate result (an NxN block)
            PixelArray dctBlock = new PixelArray(fBlockSize, fBlockSize, PixelType.RGBd, PixmapOrientation.TopToBottom, 1);

            //
            // for every NxN block of the coefficients
            //
            for (int y = 0; y < height; y += fBlockSize)
            {
                for (int x = 0; x < width; x += fBlockSize)
                {
                    //
                    // do the first matrix multiplication
                    // ([fBasis][dctBuffer])
                    // and store the results in dctBlock
                    //
                    for (int col = 0; col < fBlockSize; ++col)
                    {
                        for (int row = 0; row < fBlockSize; ++row)
                        {
                            RGBd result = new RGBd(0.0, 0.0, 0.0);
                            for (int index = 0; index < fBlockSize; ++index)
                            {
                                RGBd coeff = new RGBd();
                                coeff.SetBytes(dctBuffer.GetPixelBytes(x + index, y + row));
                                double cosVal = fBasis[col, index];

                                result.red += cosVal * coeff.Red;
                                result.green += cosVal * coeff.Green;
                                result.blue += cosVal * coeff.Blue;
                            }
                            dctBlock.SetPixel(col, row, result);
                        }
                    }

                    //
                    // do the second matrix multiplication
                    // ([dctBlock][fBasis]')
                    // and store the results in imgBuffer
                    //
                    for (int col2 = 0; col2 < fBlockSize; ++col2)
                    {
                        for (int row2 = 0; row2 < fBlockSize; ++row2)
                        {
                            RGBd result = new RGBd(0.0, 0.0, 0.0);
                            for (int index = 0; index < fBlockSize; ++index)
                            {
                                RGBd coeff = new RGBd();
                                coeff.SetBytes(dctBlock.GetPixelBytes(col2, index));
                                double cosVal_transpose = fBasis[row2, index];

                                result.red += coeff.Red * cosVal_transpose;
                                result.green += coeff.Green * cosVal_transpose;
                                result.blue += coeff.Blue * cosVal_transpose;
                            }

                            //
                            // assign the computed value back to the image
                            //
                            byte red = (byte)Math.Floor(result.Red + 0.5);
                            byte green = (byte)Math.Floor(result.Green + 0.5);
                            byte blue = (byte)Math.Floor(result.Blue + 0.5);

                            BGRb new_pixel = new BGRb(red, green, blue);
                            imgBuffer.SetPixel(x + col2, y + row2, new_pixel);
                        }
                    }
                }
            }

            return imgBuffer;
        }
        #endregion

    }
}
