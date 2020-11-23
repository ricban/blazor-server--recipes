using System.IO;
using System.Threading.Tasks;

namespace Library.Core.Extensions
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> ToBytesAsync(this Stream stream)
        {
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream).ConfigureAwait(false);
            return memoryStream.ToArray();
        }
    }
}