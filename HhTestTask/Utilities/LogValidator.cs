using System.Text.RegularExpressions;

namespace HhTestTask.Utilities
{
    internal class LogValidator
    {
        public static bool IsIpv4AddressValid(string address)
        {
            Regex regex = new("^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)");
            return regex.IsMatch(address);
        }
    }
}
