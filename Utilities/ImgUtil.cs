using Microsoft.AspNetCore.Http;
using System.IO.Compression;

namespace Utilites
{
	public class ImgUtil
	{

		public static byte[] ConvertIFromFileToByte(IFormFile data) {
			byte[] bytes;
			using (var stream = data.OpenReadStream())
			{
				using (var reader = new BinaryReader(stream))
				{
					bytes = reader.ReadBytes((int)stream.Length);
				}
			}
			return bytes;
		}

		public static byte[]? Compress(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			
			using (MemoryStream compressedStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(compressedStream, CompressionMode.Compress))
				{
					gzipStream.Write(data, 0, data.Length);
				}
				return compressedStream.ToArray();
			}

		}

		public static byte[]? Decompress(byte[]? data)
		{
			if (data == null || data.Length < 2)
			{
				return null;
			}
			try
			{
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
			catch (Exception ex) { }
			return data;
		}

	}
}

