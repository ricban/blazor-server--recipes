using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Extensions
{
    public static class ByteExtensions
    {
        #region GetString
        public static string GetString(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        public static string GetString(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        #endregion

        #region ToBase64String
        public static string ToBase64String(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static string ToBase64String(this byte[] bytes, Base64FormattingOptions option)
        {
            return Convert.ToBase64String(bytes, option);
        }
        #endregion

        public static async Task WriteAllBytesAsync(this byte[] bytes, string path)
        {
            await File.WriteAllBytesAsync(path, bytes).ConfigureAwait(false);
        }
    }
}