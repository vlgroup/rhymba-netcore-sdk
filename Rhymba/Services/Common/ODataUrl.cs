namespace Rhymba.Services.Common
{
    using Rhymba.Models.Streaming;
    using System.Web;

    internal class ODataUrl
    {
        internal static string AddQueryParam(string url, string name, object value)
        {
            if (!url.Contains('?'))
            {
                url += '?';
            }
            else
            {
                url += '&';
            }

            return url + $"{GetODataKey(name)}={GetODataValue(value)}";
        }

        private static string GetODataKey(string key)
        {
            return HttpUtility.UrlEncode(key);
        }

        private static string GetODataValue(object value)
        {
            var stringValue = value.ToString();

            if (stringValue == null)
            {
                return "''";
            }

            if (value is string)
            {
                stringValue = $"'{stringValue}'";
            }
            else if (value is string[])
            {
                stringValue = $"{string.Join(",", (value as string[] ?? new string[0]))}";
            }
            else if (value is int[])
            {
                stringValue = $"'{string.Join(",", ((int[])value).Select(x => x.ToString()))}'";
            }    
            else if (value is Guid)
            {
                stringValue = $"guid'{stringValue}'";
            }
            else if (value is bool)
            {
                stringValue = stringValue.ToLower();
            }
            else if (value is decimal)
            {
                stringValue = $"{stringValue}m";
            }
            else if (value is DateTime)
            {
                stringValue = $"'{((DateTime)value).ToString("MM-dd-yyyy HH:mm")}'";
            }
            else if (value is Enum)
            {
                if (value is GetStreamEncoding || value is GetStreamProtocol)
                {
                    stringValue = $"'{value.ToString()?.ToLower()}'";
                }
                else
                {
                    stringValue = $"{(int)value}";
                }
            }

            return HttpUtility.UrlEncode(stringValue);
        }
    }
}
