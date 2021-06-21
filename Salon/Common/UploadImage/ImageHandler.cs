using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
namespace Salon.Common.UploadImage
{
    public enum ImageFormat
    {
        jpeg,
        png,
        unknown
        //bmp,
        //,
        //gif,
        //tiff,
        //png,
    }
    public class ImageHandler
    {
        private string _path;
        public ImageHandler(string path)
        {
            _path = path;
        }
        public string UploadImage(IFormFile file)
        {
            if (CheckImageFormat(file))
                return WriteFile(file);
            return "Invalid image file";
        }
        private bool CheckImageFormat(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            return GetImageFormat(fileBytes) != ImageFormat.unknown;
        }
        private static ImageFormat GetImageFormat(byte[] bytes)
        {
            //var bmp = Encoding.ASCII.GetBytes("BM");        // BMP
           // var gif = Encoding.ASCII.GetBytes("GIF");       // GIF
            var png = new byte[] { 137, 80, 78, 71 };       // PNG
           // var tiff = new byte[] { 73, 73, 42 };           // TIFF
           // var tiff2 = new byte[] { 77, 77, 42 };          // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };   // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };  // jpeg canon
            //if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
            //    return ImageFormat.bmp;
            //if (gif.SequenceEqual(bytes.Take(gif.Length)))
            //    return ImageFormat.gif;
            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;
            //if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
            //    return ImageFormat.tiff;
            //if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
            //    return ImageFormat.tiff;
            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;
            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;
            return ImageFormat.unknown;
        }
        private string WriteFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            var fileName = Guid.NewGuid().ToString() + extension;   //Create a new Name for the file due to security reasons
            var path = Path.Combine(Directory.GetCurrentDirectory(), _path, fileName);
            using (var bits = new FileStream(path, FileMode.Create))
            {
                file.CopyToAsync(bits);
            }
            return _path + "\\" + fileName;
        }       
    }
}
