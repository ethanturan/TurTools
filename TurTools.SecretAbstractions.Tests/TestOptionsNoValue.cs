namespace TurTools.SecretAbstractions.Tests;

public class TestOptionsNoValue : OptionsWithSecrets
{
    public TestOptionsNoValue(string key7)
    {
        Key7 = key7;
    }

    [SecretKey("ValueThatDoesNotExist")]
    public string Key7 { get; set; }
}