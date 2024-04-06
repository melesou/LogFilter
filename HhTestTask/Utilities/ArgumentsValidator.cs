namespace HhTestTask.Utilities;

public static class ArgumentsValidator
{
    public static bool Validate(string[] args)
    {
        if (!CheckPairsAmount(args))
            return false;

        var parameters = ArgumentsParser.Parse(args);

        if (!CheckRequiredParameters(parameters, ["--file-log", "--file-output", "--time-start", "--time-end"]))
            return false;

        if (!CheckOptionalParameters(parameters))
            return false;

        if (!CheckFilesPath(parameters))
            return false;

        if (!CheckDates(parameters, ["--time-start", "--time-end"]))
            return false;

        if (!CheckIpAdresses(parameters, ["--address-start", "--address-mask"]))
            return false;

        return true;
    }

    private static bool CheckPairsAmount(string[] args)
    {
        if (args.Length < 8 || args.Length % 2 != 0)
        {
            Print.ErrorMessage("Usage: LogFilter.exe --file-log <path> --file-output <path> --time-start <dd.MM.yyyy> --time-end <dd.MM.yyyy> [--address-start <ip>] [--address-mask <mask>]");
            return false;
        }
        return true;
    }

    private static bool CheckRequiredParameters(Dictionary<string, string> parameters, string[] requiredParams)
    {
        foreach (var param in requiredParams)
        {
            if (!parameters.ContainsKey(param))
            {
                Print.ErrorMessage("Usage: LogFilter.exe --file-log <path> --file-output <path> --time-start <dd.MM.yyyy> --time-end <dd.MM.yyyy> [--address-start <ip>] [--address-mask <mask>]");
                return false;
            }
        }
        return true;
    }

    private static bool CheckOptionalParameters(Dictionary<string, string> parameters)
    {
        if (parameters.ContainsKey("--address-mask") && !parameters.ContainsKey("--address-start"))
        {
            Print.ErrorMessage("При использовании --address-mask необходим атрибут --address-start");
            return false;
        }
        return true;
    }

    private static bool CheckFilesPath(Dictionary<string, string> parameters)
    {
        if (!File.Exists(parameters["--file-log"]))
        {
            Print.ErrorMessage("--file-log Неверно указан путь");
            return false;
        }
        
        if (!Directory.Exists(Path.GetDirectoryName(parameters["--file-output"])))
        {
            Print.ErrorMessage("--file-output Неверно указан путь");
            return false;
        }
        return true;
    }

    private static bool CheckDates(Dictionary<string, string> parameters, string[] requiredParams)
    {
        foreach (var param in requiredParams)
        {
            if (!DateTime.TryParse(parameters[param], out _))
            {
                Print.ErrorMessage($"{param} Не валидна");
                return false;
            }
        }
        return true;
    }

    private static bool CheckIpAdresses(Dictionary<string, string> parameters, string[] requiredParams)
    {
        foreach (var param in requiredParams)
        {
            if (parameters.ContainsKey(param) && !LogValidator.IsIpv4AddressValid(parameters[param]))
            {
                Print.ErrorMessage($"{param} Не валиден");
                return false;
            }
        }
        return true;
    }
}