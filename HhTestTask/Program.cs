using HhTestTask.Entities;
using HhTestTask.Utilities;

namespace HhTestTask;

internal class Program
{
    private static void Main(string[] args)
    {
        #region Проверка параметров

        if(!ArgumentsValidator.Validate(args))
            return;

        #endregion

        #region Запись параметров
        
        var parameters = ArgumentsParser.Parse(args); 
        
        #endregion

        #region Чтение файла логов

        var logFilePath = parameters["--file-log"];
        List<string> lines;
        try
        {
            lines = File.ReadAllLines(logFilePath).ToList();
        }
        catch (IOException ex)
        {
            Print.ErrorMessage($"Ошибка чтения log файла: {ex.Message}");
            return;
        }

        #endregion

        #region Преобразование строк в обьекты

        var logLines = new List<Log>();
        try
        {
            logLines.AddRange(lines.Select(line => new Log(line)));
        }
        catch(Exception ex)
        {
            Print.ErrorMessage($"Произошла ошибка при обработке log файла: {ex.Message}");
            throw;
        }

        #endregion

        #region Фильтрация обьектов

        var addressStart = parameters.ContainsKey("--address-start") ? parameters["--address-start"] : "0.0.0.0";
        var addressMask = parameters.ContainsKey("--address-mask") ? parameters["--address-mask"] : "255.255.255.255";

        var filtredLogLines = LogFilter.Filter(logLines, addressStart, addressMask);

        #endregion

        #region Составление статистики

        var ipAddressStatistics = new List<IpAddressStatistic>();
        foreach (var log in filtredLogLines)
        {
            var statistic = ipAddressStatistics.FirstOrDefault(ip => ip.IpAddress == log.IpAddress.ToString());

            if (statistic == null)
                ipAddressStatistics.Add(new IpAddressStatistic(log));
            else
                statistic.AddRequest(log);
        }

        #endregion

        #region Сортировка статистики

        var ordered = ipAddressStatistics.OrderByDescending(ip => ip.Count).ToList(); 

        #endregion

        #region Запись результата в файл

        var outputFile = parameters["--file-output"];
        try
        {
            using var writer = new StreamWriter(outputFile);
            foreach (var statistic in ordered)
                writer.WriteLine(statistic);
        }
        catch (IOException ex)
        {
            Print.ErrorMessage($"Error writing to output file: {ex.Message}");
        }

        #endregion

        Print.GoodMessage("Анализ выполнен.");
    }
}