namespace HhTestTask.Entities;

public class IpAddressStatistic
{
    public string IpAddress { get; set; }

    public List<DateTime> RequestsTime { get; set; } = [];

    public int Count => RequestsTime.Count;

    public IpAddressStatistic(Log log)
    {
        IpAddress = log.IpAddress.ToString();
        RequestsTime.Add(log.RequestTime);
    }

    public void AddRequest(Log log)
    {
        RequestsTime.Add(log.RequestTime);
    }

    public override string ToString()
    {
        var value = $"{IpAddress,-18}";
        value += $"*{Count}* ".PadRight(7);
        if (Count == 1)
            value += $"{RequestsTime.First():dd.MM.yyyy HH:mm:ss}";
        else
            value += $"{RequestsTime.Min():dd.MM.yyyy HH:mm:ss} - {RequestsTime.Max():dd.MM.yyyy HH:mm:ss}";

        return value;
    }
}