using System;
using System.Text;

namespace Library.Core.Extensions
{
    public static class ExceptionExtension
    {
        public static string ToText(this Exception ex)
        {
            //if (ex is AggregateException && ex.InnerException != null)
            //{
            //    ex = ex.InnerException; // It's more useful
            //}

            //if (exception is AggregateException aggregateException)
            //{
            //    foreach (var innerException in aggregateException.Flatten().InnerExceptions)
            //    {
            //        Console.Error.WriteLine(innerException);
            //    }
            //}
            //else
            //{
            //    Console.Error.WriteLine(exception);
            //}

            var sb = new StringBuilder(ex.ToString());

            sb.AppendLine();

            if (ex.InnerException?.Data?.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Inner Exception Data:");
                sb.AppendLine();
                sb.Append("Data = ").AppendLine(ex.InnerException.Data.Serialize(true));
                sb.AppendLine();
            }

            //if (ex.InnerException != null)
            //{
            //    sb.AppendLine();
            //    sb.AppendLine("Inner Exception:");
            //    sb.AppendLine();
            //    sb.AppendLine(ex.InnerException.ToString());

            //    if (ex.InnerException.Data.Count > 0)
            //    {
            //        sb.AppendLine();
            //        sb.Append("Data = ").AppendLine(ex.InnerException.Data.Serialize(true));
            //    }

            //    sb.AppendLine();
            //}

            if (ex.Data.Count > 0)
            {
                sb.AppendLine();
                sb.Append("Data = ").AppendLine(ex.Data.Serialize(true));
            }

            return sb.ToString();
        }
    }
}