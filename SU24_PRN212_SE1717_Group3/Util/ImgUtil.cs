using System.IO.Compression;

namespace SU24_PRN212_SE1717_Group3.Util
{
    public class ImgUtil
    {
        public static byte[]? CompressImage(IFormFile data)
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

            using (MemoryStream outputStream = new MemoryStream(bytes.Length))
            {
                using (DeflateStream deflateStream = new DeflateStream(outputStream, CompressionMode.Compress, true))
                {
                    deflateStream.Write(bytes, 0, bytes.Length);
                }

                return outputStream.ToArray();
            }

        }

        public static byte[]? DecompressImage(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            using (MemoryStream outputStream = new MemoryStream(data.Length))
            {
                using (MemoryStream inputStream = new MemoryStream(data))
                using (DeflateStream deflateStream = new DeflateStream(inputStream, CompressionMode.Decompress))
                {
                    deflateStream.CopyTo(outputStream);
                }

                return outputStream.ToArray();
            }
        }

    }
}

