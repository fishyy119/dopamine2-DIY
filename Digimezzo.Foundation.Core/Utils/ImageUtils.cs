using Digimezzo.Foundation.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using ColorM = System.Windows.Media.Color;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Digimezzo.Foundation.Core.Utils
{
    public static class ImageUtils
    {
        private static readonly ImageCodecInfo JpegCodec;

        static ImageUtils()
        {
            JpegCodec = ImageCodecInfo.GetImageEncoders().First(encoder => encoder.MimeType == "image/jpeg");
        }

        /// <summary>
        /// Converts an image file to a gray scale Byte array
        /// </summary>
        /// <param name="filename">The image file</param>
        /// <returns>A gray scale Byte array</returns>
        public static byte[] Image2GrayScaleByteArray(string filename)
        {
            byte[] byteArray = null;

            try
            {
                if (string.IsNullOrEmpty(filename)) return null;

                Bitmap bmp = default(Bitmap);

                using (Bitmap tempBmp = new Bitmap(filename))
                {

                    bmp = MakeGrayscale(new Bitmap(tempBmp));
                }

                ImageConverter converter = new ImageConverter();

                byteArray = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
            }
            catch (Exception)
            {
                throw;
            }

            return byteArray;
        }

        /// <summary>
        /// Converts a System.Drawing.Bitmap to a gray scale System.Drawing.Bitmap
        /// </summary>
        /// <param name="original">The original System.Drawing.Bitmap</param>
        /// <returns>A gray scale System.Drawing.Bitmap</returns>
        public static Bitmap MakeGrayscale(Bitmap original)
        {
            // Create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            // Get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            // Create the gray scale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] {new float[] {0.3f,0.3f,0.3f,0,0},
                                                                     new float[] {0.59f,0.59f,0.59f,0,0},
                                                                     new float[] {0.11f,0.11f,0.11f,0,0},
                                                                     new float[] {0,0,0,1,0},
                                                                     new float[] {0,0,0,0,1}
                                                                    });

            // Create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            // Set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            // Draw the original image on the new image using the gray scale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            // Dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        /// <summary>
        /// Converts an image file to a Byte array
        /// </summary>
        /// <param name="filename">The image file</param>
        /// /// <param name="width">The width to which the image should be rescaled (no rescale when 0)</param>
        /// <param name="height">The height to which the image should be rescaled (no rescale when 0)</param>
        /// <returns>A Byte array</returns>
        public static byte[] Image2ByteArray(string filename, int width, int height)
        {
            byte[] byteArray = null;

            try
            {
                if (string.IsNullOrEmpty(filename))
                {
                    return null;
                }

                Bitmap bmp = default(Bitmap);

                using (Bitmap tempBmp = new Bitmap(filename))
                {
                    bmp = new Bitmap(tempBmp);

                    if (width > 0 && height > 0)
                    {
                        Bitmap scaledBmp = new Bitmap(bmp, new Size(width, height));
                        bmp = scaledBmp;
                    }
                }

                ImageConverter converter = new ImageConverter();

                byteArray = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
            }
            catch (Exception)
            {
                throw;
            }

            return byteArray;
        }

        /// <summary>
        /// Converts a System.Drawing.Image to a file on disk
        /// </summary>
        /// <param name="img">The System.Drawing.Image</param>
        /// <param name="codec">The codec</param>
        /// <param name="filename">The file name</param>
        /// <param name="width">The width to which the image should be rescaled (no rescale when 0)</param>
        /// <param name="height">The height to which the image should be rescaled (no rescale when 0)</param>
        /// <param name="qualityPercent">The compression quality</param>
        public static void Image2File(Image img, ImageCodecInfo codec, string filename,int width, int height, long qualityPercent)
        {
            var encoderParams = new EncoderParameters
            {
                Param = { [0] = new EncoderParameter(Encoder.Quality, qualityPercent) }
            };

            Image scaledImg = null;
            try
            {
                if (width > 0 && height > 0)
                {
                    scaledImg = img.GetThumbnailImage(width, height, null, IntPtr.Zero);
                    img = scaledImg;
                }

                if (File.Exists(filename))
                    File.Delete(filename);
                img.Save(filename, codec, encoderParams);
            }
            finally
            {
                scaledImg?.Dispose();
            }
        }

        /// <summary>
        /// Converts a Byte array to a image file on disk
        /// </summary>
        /// <param name="imageData">The Byte array containing the image data</param>
        /// <param name="codec">The codec</param>
        /// <param name="filename">The file name</param>
        /// <param name="width">The width to which the image should be rescaled (no rescale when 0)</param>
        /// <param name="height">The height to which the image should be rescaled (no rescale when 0)</param>
        /// <param name="qualityPercent">The compression quality</param>
        public static void Byte2ImageFile(byte[] imageData, ImageCodecInfo codec, string filename, int width, int height,
            long qualityPercent)
        {
            using (var ms = new MemoryStream(imageData))
            {
                using (var img = Image.FromStream(ms))
                {
                    Image2File(img, codec, filename, width, height, qualityPercent);
                }
            }
        }

        /// <summary>
        /// Converts a Byte array to a JPG file on disk
        /// </summary>
        /// <param name="imageData">The Byte array containing the image data</param>
        /// <param name="filename">The file name</param>
        /// <param name="width">The width to which the image should be rescaled (no rescale when 0)</param>
        /// <param name="height">The height to which the image should be rescaled (no rescale when 0)</param>
        /// <param name="qualityPercent">The compression quality</param>
        public static void Byte2Jpg(byte[] imageData, string filename, int width, int height, long qualityPercent)
        {
            Byte2ImageFile(imageData, JpegCodec, filename, width, height, qualityPercent);
        }

        /// <summary>
        /// Gets the size (width and height) of an image contained in a Byte array
        /// </summary>
        /// <param name="imageData">The Byte array containing the image data</param>
        /// <returns>The size (width and height) of the image</returns>
        public static Size GetImageDataSize(byte[] imageData)
        {
            Size size = new Size(0,0);

            try
            {
                using (Image img = Image.FromStream(new MemoryStream(imageData)))
                {
                    size = new Size(img.Width, img.Height);
                }

            }
            catch (Exception)
            {
            }

            return size;
        }

        /// <summary>
        /// Converts a file to a System.Windows.Media.Imaging.BitmapImage
        /// </summary>
        /// <param name="filename">The file name</param>
        /// <param name="width">The width to which the image should be rescaled (no rescale when 0)</param>
        /// <param name="height">The height to which the image should be rescaled (no rescale when 0)</param>
        /// <returns></returns>
        public static BitmapImage PathToBitmapImage(string filename, int width, int height)
        {
            if (File.Exists(filename))
            {
                BitmapImage bi = new BitmapImage();

                bi.BeginInit();

                if (width > 0 && height > 0)
                {
                    bi.DecodePixelWidth = width;
                    bi.DecodePixelHeight = height;
                }

                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.UriSource = new Uri(filename);
                bi.EndInit();
                bi.Freeze();

                return bi;
            }

            return null;
        }

        /// <summary>
        /// Converts a file to a System.Windows.Media.Imaging.BitmapImage
        /// </summary>
        /// <param name="byteData">The Byte array containing the image data</param>
        /// <param name="width">The width to which the image should be rescaled (no rescale when 0)</param>
        /// <param name="height">The height to which the image should be rescaled (no rescale when 0)</param>
        /// <param name="maxLength">The maximum allowed length for width and height</param>
        /// <returns></returns>
        public static BitmapImage ByteToBitmapImage(byte[] byteData, int width, int height, int maxLength)
        {
            if (byteData != null && byteData.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(byteData))
                {
                    BitmapImage bi = new BitmapImage();

                    bi.BeginInit();

                    if (width > 0 && height > 0)
                    {
                        var size = new Size(width, height);
                        if (maxLength > 0) size = GetScaledSize(new Size(width, height), maxLength);

                        bi.DecodePixelWidth = size.Width;
                        bi.DecodePixelHeight = size.Height;
                    }

                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.StreamSource = ms;
                    bi.EndInit();
                    bi.Freeze();

                    return bi;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the scaled size of an image, when rescaled using a maximum length for width and height
        /// </summary>
        /// <param name="originalSize">The original size of the image</param>
        /// <param name="maxLength">The maximum allowed length for width and height</param>
        /// <returns></returns>
        public static Size GetScaledSize(Size originalSize, int maxLength)
        {
            var scaledSize = new Size();

            if (originalSize.Height > originalSize.Width)
            {
                scaledSize.Height = maxLength;
                scaledSize.Width = Convert.ToInt32(((double)originalSize.Width / maxLength) * 100);
            }
            else
            {
                scaledSize.Width = maxLength;
                scaledSize.Height = Convert.ToInt32(((double)originalSize.Height / maxLength) * 100);
            }

            return scaledSize;
        }

        /// <summary>
        /// Gets the dominant color in an image file
        /// </summary>
        /// <param name="filename">The image file</param>
        /// <returns>The dominant System.Windows.Media.Color</returns>
        public static System.Windows.Media.Color GetDominantColor(string filename)
        {
            Bitmap bitmap = (Bitmap)Bitmap.FromFile(filename);

            return GetDominantColor(bitmap);
        }

        /// <summary>
        /// Gets the dominant color in an image Byte array
        /// </summary>
        /// <param name="imageData">The Byte array containing the image data</param>
        /// <returns>The dominant System.Windows.Media.Color</returns>
        public static System.Windows.Media.Color GetDominantColor(byte[] imageData)
        {
            Bitmap bitmap;

            using (var ms = new MemoryStream(imageData))
            {
                bitmap = new Bitmap(ms);
            }

            return GetDominantColor(bitmap);
        }

        /// <summary>
        /// 使用HSL色彩空间筛选，去除黑色、白色、灰色像素
        /// </summary>
        /// <param name="hslColors">HSLColor的列表</param>
        /// <returns>保留点的对应索引</returns>
        private static List<int> GetValidColorIndices(List<HSLColor> hslColors)
        {
            List<int> validIndices = new List<int>();

            for (int i = 0; i < hslColors.Count; i++)
            {
                var hsl = hslColors[i];
                if (hsl.Luminosity >= 25 && hsl.Luminosity <= 75 && hsl.Saturation >= 30)
                {
                    validIndices.Add(i); // 记录索引
                }
            }

            return validIndices;
        }

        /// <summary>
        /// 计算平均色彩（Lab空间）
        /// </summary>
        /// <param name="pixels">一系列颜色的列表</param>
        /// <returns>色彩平均值</returns>
        private static LABColor ComputeMeanColor(List<LABColor> pixels)
        {
            double meanL = pixels.Average(p => p.L);
            double meanA = pixels.Average(p => p.A);
            double meanB = pixels.Average(p => p.B);
            return new LABColor(meanL, meanA, meanB);
        }

        /// <summary>
        /// 计算方差
        /// </summary>
        /// <param name="values">值</param>
        /// <returns>方差</returns>
        private static double ComputeVariance(IEnumerable<double> values)
        {
            int count = values.Count();
            if (count == 0) return 0;

            double mean = values.Average();
            double var = values.Sum(v => Math.Pow(v - mean, 2)) / count;
            return var;
        }

        /// <summary>
        /// LAB三通道的方差加权，其中L通道方差权重为0.1
        /// </summary>
        /// <param name="pixels">一系列点的列表</param>
        /// <returns>加权后方差</returns>
        private static double ComputeWeightedVariance(List<LABColor> pixels)
        {
            double varL = ComputeVariance(pixels.Select(p => p.L));
            double varA = ComputeVariance(pixels.Select(p => p.A));
            double varB = ComputeVariance(pixels.Select(p => p.B));
            return (varL / 10.0 + varA + varB) / 2.9; // 加权计算
        }

        /// <summary>
        /// 通过递归方式进行中值切分，内部变量numColors控制递归深度
        /// </summary>
        /// <param name="pixels">点的列表</param>
        /// <param name="depth">递归深度</param>
        /// <returns > 列表，每一项分别记录平均色彩与加权方差 </returns>
        public static List<Tuple<LABColor, double>> SplitColors(List<LABColor> pixels, int depth = 0)
        {
            int numColors = 7;
            if (pixels.Count == 0)
                return new List<Tuple<LABColor, double>>();

            if (depth >= Math.Log(numColors, 2))
            {
                var meanColor = ComputeMeanColor(pixels);
                double variance = ComputeWeightedVariance(pixels);
                return new List<Tuple<LABColor, double>> { Tuple.Create(meanColor, variance) };
            }

            // 计算每个通道的方差
            double varL = ComputeVariance(pixels.Select(p => p.L)) / 10.0;
            double varA = ComputeVariance(pixels.Select(p => p.A));
            double varB = ComputeVariance(pixels.Select(p => p.B));

            // 计算最大方差的通道
            int maxVarIndex = (varL > varA && varL > varB) ? 0 : (varA > varB ? 1 : 2);

            // 按该通道的中位数切分
            pixels.Sort((p1, p2) => maxVarIndex == 0 ? p1.L.CompareTo(p2.L) :
                                     maxVarIndex == 1 ? p1.A.CompareTo(p2.A) :
                                                        p1.B.CompareTo(p2.B));

            int medianIndex = pixels.Count / 2;
            return SplitColors(pixels.GetRange(0, medianIndex), depth + 1)
                .Concat(SplitColors(pixels.GetRange(medianIndex, pixels.Count - medianIndex), depth + 1))
                .ToList();
        }

        /// <summary>
        /// Gets the dominant color in a System.Drawing.Bitmap
        /// </summary>
        /// <param name="bitmap">The Byte array containing the image data</param>
        /// <returns>The dominant System.Windows.Media.Color</returns>
        private static ColorM GetDominantColor(Bitmap bitmap)
        {
            var labColors = new List<LABColor>();
            var rgbColors = new List<ColorM>();
            var hslColors = new List<HSLColor>();

            // 控制计算规模，进行缩放
            int maxSize = 500;
            if (bitmap.Width > maxSize || bitmap.Height > maxSize)
            {
                double scale = Math.Min((double)maxSize / bitmap.Width, (double)maxSize / bitmap.Height);
                int newWidth = (int)(bitmap.Width * scale);
                int newHeight = (int)(bitmap.Height * scale);

                Bitmap resized = new Bitmap(newWidth, newHeight);
                using (Graphics g = Graphics.FromImage(resized))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(bitmap, 0, 0, newWidth, newHeight);
                }
                bitmap = resized;
            }

            // 读取各像素，生成rgb列表与hsl列表，一一对应
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    rgbColors.Add(ColorM.FromRgb(pixelColor.R, pixelColor.G, pixelColor.B));
                    var hslColor = HSLColor.GetFromRgb(pixelColor.R, pixelColor.G, pixelColor.B);
                    hslColors.Add(hslColor);
                }
            }

            var validIndices = GetValidColorIndices(hslColors);
            if (validIndices.Count <= 20)
            {
                // 原先的方法，如果过滤完不剩几个像素了就用它
                var newBitmap = new Bitmap(1, 1);

                using (Graphics g = Graphics.FromImage(newBitmap))
                {
                    // Interpolation mode needs to be HighQualityBilinear or HighQualityBicubic
                    // or this method doesn't work. With either setting, the averaging result is
                    // slightly different.
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(bitmap, new Rectangle(0, 0, 1, 1));
                }

                Color color = newBitmap.GetPixel(0, 0);

                return System.Windows.Media.Color.FromRgb(color.R, color.G, color.B);
            }
            else
            {
                for (int i = 0; i < validIndices.Count; i++)
                {
                    var labColor = LABColor.GetFromRgb(rgbColors[validIndices[i]]);
                    labColors.Add(labColor);
                }

                var dominantLabColors = SplitColors(labColors).OrderBy(x => x.Item2).ToList();
                double minVariance = dominantLabColors.First().Item2;

                // 筛选方差小于 `min(minVariance * 2, 50)` 的颜色
                var closeColors = dominantLabColors
                    .Where(x => x.Item2 < Math.Min(minVariance * 2, 25))  // 这里的25与图像尺寸有关，这里是500x500下的经验值
                    .Select(x => x.Item1)
                    .ToList();

                LABColor result_lab;
                if (closeColors.Count > 1)
                {
                    // 选择最鲜艳的颜色
                    int max_s = 0;
                    int max_i = 0;
                    for (int i = 0; i < closeColors.Count; i++)
                    {
                        HSVColor hsv = HSVColor.GetFromRgb(closeColors[i].ToRgb());
                        if (max_s <= hsv.Saturation)
                        {
                            max_s = hsv.Saturation;
                            max_i = i;
                        }
                    }
                    result_lab = closeColors[max_i];
                }
                else
                {
                    // 选择方差最小的颜色
                    result_lab = dominantLabColors.First().Item1;
                }

                var result_rgb = result_lab.ToRgb();
                var result_hsv = HSVColor.GetFromRgb(result_rgb);
                // 限制S、V到80
                var S = result_hsv.Saturation;
                var V = result_hsv.Value;
                if (S >= 80 || V >= 80)
                {
                    result_hsv.Saturation = Math.Min(80, S);
                    result_hsv.Value = Math.Min(80, V);
                    return result_hsv.ToRgb();
                }
                else
                {
                    return result_rgb;
                }
                
            }
        }

        /// <summary>
        /// Rescales an image in a byte[] to another byte[]
        /// </summary>
        /// <param name="inputImage">The image to rescale</param>
        /// <param name="width">The width to which the image should to be rescaled</param>
        /// <param name="height">The height to which the image should to be rescaled</param>
        /// <returns>The rescaled image</returns>
        public static byte[] ResizeImageInByteArray(byte[] inputImage, int width, int height)
        {
            byte[] outputImage = null;

            if(inputImage != null)
            {
                MemoryStream inputMemoryStream = new MemoryStream(inputImage);
                Image fullsizeImage = Image.FromStream(inputMemoryStream);

                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size(width, height));
                MemoryStream resultStream = new MemoryStream();

                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                outputImage = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();
            }

            return outputImage;
        }
    }
}
