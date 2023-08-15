using TurTools.SecretAbstractions;
using Xunit;

namespace TurTools.SecretAbstractions.Tests;

public class OptionsWithSecretsTests
{
    [Fact]
    public async Task PopulateSecrets_PopulatesSecrets()
    {
        var options = new TestOptionsWithSecrets
        {
            SecretKey1 = "this is a secret key"
        };

        await options.PopulateSecrets<TestOptionsWithSecrets>(x =>
        {
            Assert.Equal("this is a secret key", x);
            return Task.FromResult("secret value");
        });
        
        Assert.Equal("secret value", options.SecretValue1);
        Assert.Equal("this is a secret key", options.SecretKey1);
    }
}

public class TestOptionsWithSecrets : OptionsWithSecrets
{
    [SecretKey(nameof(SecretValue1))]
    public string SecretKey1 { get; set; }
    public string SecretValue1 { get; set; }
}
