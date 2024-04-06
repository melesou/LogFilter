using System.Net;
using HhTestTask.Entities;

namespace HhTestTask.Utilities
{
    internal class LogFilter
    {
        public static List<Log> Filter(List<Log> logLines, string addressStart, string addressMask)
        {
            var ipAddressStartBytes = IPAddress.Parse(addressStart).GetAddressBytes();
            var ipAddressMaskBytes = IPAddress.Parse(addressMask).GetAddressBytes();

            return logLines.Where(log => log.IsInRange(ipAddressStartBytes, ipAddressMaskBytes)).ToList();
        }
    }
}