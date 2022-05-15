using LaborExchangeApplication.Model;
using LaborExchangeApplication.View.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LaborExchangeApplication.Core
{
    public class ImageConverter
    {
        /// <summary>
        /// Метод принимает массив байтов и конвертирует его в растовое изображение.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>Растовое изображение (BitmapImage).</returns>
        public static BitmapImage BitmapImageFromBytes(byte[]? bytes)
        {
            if (bytes is null) return new BitmapImage();

            var image = new BitmapImage();
            var stream = new MemoryStream(bytes);
            try
            {
                stream.Seek(0, SeekOrigin.Begin);

                var img = Image.FromStream(stream);
                image.BeginInit();

                var ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                image.StreamSource = ms;
                image.StreamSource.Seek(0, SeekOrigin.Begin);
                image.EndInit();
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
            finally
            {
                stream?.Close();
                stream?.Dispose();
            }

            return image;
        }

        /// <summary>
        /// Метод принимает растовое изображение и конвертирует его в массив байтов
        /// </summary>
        /// <param name="imageSource"></param>
        /// <returns>Массив байтов (byte[])</returns>
        public static byte[] BytesFromBitmapImage(BitmapImage? imageSource)
        {
            byte[]? buffer = null;
            try
            {
                if (imageSource is null) return Array.Empty<byte>();

                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageSource));

                using var ms = new MemoryStream();
                encoder.Save(ms);
                buffer = ms.ToArray();
            }
            catch (Exception ex)
            {
                new InformationBoxWindow("Ошибка", ex.Message, InformationBoxImage.Error).ShowDialog();
            }
            return buffer ?? Array.Empty<byte>();
        }
    }
}