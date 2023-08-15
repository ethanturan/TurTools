namespace TurTools.SecretAbstractions.Tests;

public class TestOptionsWithSecrets : OptionsWithSecrets
{
    public TestOptionsWithSecrets(string secretKey1)
    {
        SecretKey1 = secretKey1;
    }

    [SecretKey(nameof(SecretValue1))]
    public string SecretKey1 { get; set; }
    public string? SecretValue1 { get; set; }
}