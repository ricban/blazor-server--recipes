using System;
using System.Text;

namespace Library.Core.Extensions
{
    public static class StringExtensions
    {
        public static byte[] FromBase64String(this string base64string)
        {
            return Convert.FromBase64String(base64string);
        }

        #region GetBytes
        public static byte[] GetBytes(this string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        public static byte[] GetBytes(this string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }
        #endregion

        public static bool Includes(this string str, string value)
        {
            return str.IndexOf(value) > -1;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static T ToEnum<T>(this string value, T defaultValue, bool ignoreCase = true) where T : struct, IComparable
        {
            if (value.IsNullOrEmpty())
            {
                return defaultValue;
            }

            return Enum.TryParse(value, ignoreCase, out T result) ? result : defaultValue;
        }

        public static string GetYoutubeThumbnailImageURL(this string youtubeVideoURL, string defaultImageURL = "", bool maxresdefault = true, string delimiter = "watch?v=")
        {
            var url = defaultImageURL;

            if (!youtubeVideoURL.IsNullOrWhiteSpace())
            {
                var index = youtubeVideoURL.IndexOf(delimiter);

                if (index > -1)
                {
                    url = "https://i.ytimg.com/vi/" + youtubeVideoURL.Substring(index + delimiter.Length) + "/" + maxresdefault.IIf("maxres", "hq") + "default.jpg";
                }
            }

            return url;
        }
    }
}