namespace HhTestTask.Utilities;

public static class ArgumentsParser
{
    public static Dictionary<string, string> Parse(string[] args)
    {
        var parameters = new Dictionary<string, string>();
        for (var i = 0; i < args.Length; i += 2)
            parameters.Add(args[i], args[i + 1].Trim());
        return parameters;
    }
}