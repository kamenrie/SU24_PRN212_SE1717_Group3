using Microsoft.AspNetCore.Http;
using System.IO.Compression;

namespace Utilites
{
    public class ImgUtil
    {

        public static byte[]? Compress(IFormFile data)
        {
            if (data == null)
            {
                return null;
            }
            byte[] bytes;
            using (var stream = data.OpenReadStream())
            {
                using (var reader = new BinaryReader(stream))
                {
                    bytes = reader.ReadBytes((int)stream.Length);
                }
            }
            using (MemoryStream compressedStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                {
                    gzipStream.Write(bytes, 0, bytes.Length);
                }
                return compressedStream.ToArray();
            }

        }

        public static byte[]? Decompress(byte[]? data)
        {
            if (data == null)
            {
                return null;
            }

            using (MemoryStream compressedStream = new MemoryStream(data))
            {
                using (GZipStream gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                {
                    using (MemoryStream decompressedStream = new MemoryStream())
                    {
                        gzipStream.CopyTo(decompressedStream);
                        return decompressedStream.ToArray();
                    }
                }
            }
        }

    }
}

