using TurTools.SecretAbstractions.Exceptions;
using Xunit;

namespace TurTools.SecretAbstractions.Tests;

public class OptionsWithSecretsTests
{
    [Fact]
    public async Task PopulateSecrets_SetsSecretValue()
    {
        const string secretKey = "secret key 23";
        var secretValue = "secret value 56";
        
        var options = new TestOptionsWithSecrets(secretKey);

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        await options.PopulateSecrets(x =>
        {
            Assert.Equal(secretKey, x);
            return Task.FromResult(secretValue)!;
        });
        
        Assert.Equal(secretValue, options.SecretValue1);
        Assert.Equal(secretKey, options.SecretKey1);
    }
    
    [Fact]
    public async Task PopulateSecrets_SetsNullValue()
    {
        const string secretKey = "secret key 23";
        const string? secretValue = null;
        
        var options = new TestOptionsWithSecrets(secretKey);

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        await options.PopulateSecrets(x =>
        {
            Assert.Equal(secretKey, x);
            return Task.FromResult(secretValue);
        }, new SecretPopulationOptions()
        {
            AllowNullSecretValues = true
        });
        
        Assert.Null(options.SecretValue1);
        Assert.Equal(secretKey, options.SecretKey1);
    }

    
    [Fact]
    public async Task? PopulateSecret_ThrowsExceptionWhen_DelegateReturnsNullValue()
    {
        var options = new TestOptionsWithSecrets("anything");
        await Assert.ThrowsAsync<NullSecretException>(async () =>
        {
            await options.PopulateSecrets(_ => Task.FromResult(null as string));
        });
    }
    
    [Fact]
    public async Task? PopulateSecret_ThrowsExceptionWhen_TargetDoesNotExist()
    {
        var options = new TestOptionsNoValue("anything");
        await Assert.ThrowsAsync<PropertyDoesNotExistException>(async () =>
        {
            await options.PopulateSecrets(_ => Task.FromResult("value"));
        });
    }
    
}