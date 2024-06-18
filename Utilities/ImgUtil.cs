

using Microsoft.AspNetCore.Http;
using System.IO.Compression;

namespace Utilites
{
    public class ImgUtil
    {

        public static byte[]? Compress(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            using (var compressedStream = new MemoryStream())
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                zipStream.Write(data, 0, data.Length);
                return compressedStream.ToArray();
            }
        }

        public static byte[]? Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
        public static byte[]? ConvertIFormFileToByte(IFormFile data)
        {
            if (data == null) return null;

            using (var stream = data.OpenReadStream())
            {
                using (var reader = new BinaryReader(stream))
                {
                    return reader.ReadBytes((int)stream.Length);
                }
            }
        }


        //public static byte[]? CompressImage(IFormFile data)
        //{
        //    if (data == null)
        //    {
        //        return null;
        //    }
        //    byte[] bytes;
        //    using (var stream = data.OpenReadStream())
        //    {
        //        using (var reader = new BinaryReader(stream))
        //        {
        //            bytes = reader.ReadBytes((int)stream.Length);
        //        }
        //    }
        //    using (MemoryStream outputStream = new MemoryStream(bytes.Length))
        //    {
        //        using (DeflateStream deflateStream = new DeflateStream(outputStream, CompressionMode.Compress, true))
        //        {
        //            deflateStream.Write(bytes, 0, bytes.Length);
        //        }

        //        return outputStream.ToArray();
        //    }

        //}

        //public static byte[]? DecompressImage(byte[]? data)
        //{
        //    if (data == null)
        //    {
        //        return null;
        //    }

        //    using (MemoryStream outputStream = new MemoryStream(data.Length))
        //    {
        //        using (MemoryStream inputStream = new MemoryStream(data))
        //        using (DeflateStream deflateStream = new DeflateStream(inputStream, CompressionMode.Decompress))
        //        {
        //            deflateStream.CopyTo(outputStream);
        //        }

        //        return outputStream.ToArray();
        //    }
        //}

    }
}

