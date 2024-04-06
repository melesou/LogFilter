using HhTestTask.Utilities;

namespace HhTestTask.Tests;

public class ArgumentsValidatorTests
{
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
        var args = new[] { "--file-log", "--file-output", $"{txtPath}\\LogOutput.txt", "--time-start", "2023-01-01", "--time-end ", "2024-04-08", "--address-start", "0.0.0.0", "--address-mask", "255.255.255.255" };

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
        var args = new[] { "--file-log", $"{txtPath}\\LogInput.txt", "--file-output", $"{txtPath}\\LogOutput.txt", "--time-start", "2023-01-01", "--time-end", "2024-04-08" };

        Assert.True(ArgumentsValidator.Validate(args));
    }

    [Test]
    public void Validate_Test_AllGood()
    {
        var txtPath = Directory.GetCurrentDirectory();
        var args = new[] { "--file-log", $"{txtPath}\\LogInput.txt", "--file-output", $"{txtPath}\\LogOutput.txt", "--time-start", "2023-01-01", "--time-end", "2024-04-08", "--address-start", "0.0.0.0", "--address-mask", "255.255.255.255" };

        Assert.True(ArgumentsValidator.Validate(args));
    }
}
