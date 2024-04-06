using System.Net;

namespace HhTestTask.Entities;

public class Log
{
    public IPAddress IpAddress { get; set; }

    public DateTime RequestTime { get; set; }

    public Log(string line)
    {
        var splitLine = line.Trim().Split(":", 2);
        try
        {
            IpAddress = IPAddress.Parse(splitLine[0]);
            RequestTime = DateTime.Parse(splitLine[1]);
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Ошибка при обработке строки лога: {line}", ex);
        }
    }

    public bool IsInRange(byte[] lowerAddressByteRange, byte[] upperAddressByteRange)
    {
        var bytes = IpAddress.GetAddressBytes();
        for (var i = 0; i < 4; i++)
        {
            if (bytes[i] < lowerAddressByteRange[i] || bytes[i] > upperAddressByteRange[i])
                return false;
        }
        return true;
    }
}
