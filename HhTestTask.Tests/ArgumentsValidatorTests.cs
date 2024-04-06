using HhTestTask.Utilities;

namespace HhTestTask.Tests;

public class ArgumentsValidatorTests
{
    private string _inputFilePath = "";
    private string _outputFilePath = "";

    [SetUp]
    public void Setup()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        _inputFilePath = Path.Combine(baseDirectory, "..", "..", "..", "LogInput.txt");
        _inputFilePath = Path.GetFullPath(_inputFilePath);

        _outputFilePath = Path.Combine(baseDirectory, "..", "..", "..", "LogOutput.txt");
        _outputFilePath = Path.GetFullPath(_outputFilePath);
    }

    [Test]
    public void Validate_Test_InvalidArguments()
    {
        var args = new[] { "invalid", "args" };

        Assert.IsFalse(ArgumentsValidator.Validate(args));
    }

    [Test]
    public void Validate_Test_LackOfArguments()
    {
        string[] args = Array.Empty<string>();

        Assert.IsFalse(ArgumentsValidator.Validate(args));
    }

    [Test]
    public void Validate_Test_LackOfArgumentsPairs()
    {
        var txtPath = Directory.GetCurrentDirectory();
        var args = new[] { "--file-log", "--file-output", _inputFilePath, "--time-start", "2023-01-01", "--time-end ", "2024-04-08", "--address-start", "0.0.0.0", "--address-mask", "255.255.255.255" };

        Assert.IsFalse(ArgumentsValidator.Validate(args));
    }

    [Test]
    public void Validate_Test_LackOfRequiredParameters()
    {
        var args = new[] { "--time-start", "2023-01-01", "--time-end", "2024-04-08", "--address-start", "100.0.0.0", "--address-mask", "255.255.255.255" };

        Assert.IsFalse(ArgumentsValidator.Validate(args));
    }

    [Test]
    public void Validate_Test_WithoutOptionalParameters()
    {
        var txtPath = Directory.GetCurrentDirectory();
        var args = new[] { "--file-log", _inputFilePath, "--file-output", _outputFilePath, "--time-start", "2023-01-01", "--time-end", "2024-04-08" };

        Assert.True(ArgumentsValidator.Validate(args));
    }

    [Test]
    public void Validate_Test_AllGood()
    {
        var txtPath = Directory.GetCurrentDirectory();
        var args = new[] { "--file-log", _inputFilePath, "--file-output", _outputFilePath, "--time-start", "2023-01-01", "--time-end", "2024-04-08", "--address-start", "0.0.0.0", "--address-mask", "255.255.255.255" };

        Assert.True(ArgumentsValidator.Validate(args));
    }
}