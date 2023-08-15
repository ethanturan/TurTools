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
        await options.PopulateSecrets<TestOptionsWithSecrets>(x =>
        {
            Assert.Equal(secretKey, x);
            return Task.FromResult(secretValue)!;
        });
        
        Assert.Equal(secretValue, options.SecretValue1);
        Assert.Equal(secretKey, options.SecretKey1);
    }
    
    // TODO test delegate returns null throws exception
    
    [Fact]
    public async Task PopulateSecrets_SetsNullValue()
    {
        const string secretKey = "secret key 23";
        const string? secretValue = null;
        
        var options = new TestOptionsWithSecrets(secretKey);

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        await options.PopulateSecrets<TestOptionsWithSecrets>(x =>
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
    public async Task? PopulateSecret_ThrowsExceptionWhen_TargetDoesNotExist()
    {
        var options = new TestOptionsNoValue("anything");
        await Assert.ThrowsAsync<PropertyDoesNotExistException>(async () =>
        {
            await options.PopulateSecrets<TestOptionsNoValue>(_ => Task.FromResult("value"));
        });
    }
    
}