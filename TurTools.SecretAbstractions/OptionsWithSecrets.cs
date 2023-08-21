using System.Reflection;
using TurTools.SecretAbstractions.Exceptions;
using NullReferenceException = System.NullReferenceException;

namespace TurTools.SecretAbstractions;

/// <summary>
/// Options that contain properties which are identifiers for secrets and are decorated with <see cref="SecretKeyAttribute"/>
/// </summary>
public abstract class OptionsWithSecrets
{
    /// <summary>
    /// Populates any secrets in these options as by <see cref="SecretKeyAttribute"/>
    /// </summary>
    /// <param name="secretRetrievalDelegate">A delegate which returns the secret corresponding to a given key (For example from Azure KeyVault)</param>
    /// <param name="options">Secret retrieval options which modify the way secrets are populated</param>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="PropertyDoesNotExistException"></exception>
    /// <exception cref="SecretRetrievalException"></exception>
    /// <exception cref="NullSecretException"></exception>
    public async Task PopulateSecrets(Func<string, Task<string?>> secretRetrievalDelegate, SecretPopulationOptions? options = null)
    {
        options ??= new SecretPopulationOptions();
        
        var optionsProps = GetType().GetProperties();
        
        var secretProperties = optionsProps
            .Where(prop => Attribute.IsDefined(prop, typeof(SecretKeyAttribute)))
            .Select(prop =>
            {
                var secretPropName = prop.GetCustomAttribute<SecretKeyAttribute>()?.SecretPropertyName;
                var secretPropKey = (string?)prop.GetValue(this);

                if (secretPropName is null)
                {
                    // this property is not a secret
                    return null;
                }

                if (string.IsNullOrEmpty(secretPropKey))
                {
                    throw new NullReferenceException($"The value of the property with SecretKey attribute {secretPropName} is null or empty.");
                }

                return new SecretProperty(secretPropName, secretPropKey);
            })
            .Where(x => x is not null)
            .Select(x => x!)
            .ToList();

        foreach (var secretProperty in secretProperties)
        {
            var secretPropTarget = optionsProps.FirstOrDefault(prop => prop.Name.Equals(secretProperty.Name));

            if (secretPropTarget is null)
            {
                throw new PropertyDoesNotExistException(secretProperty.Name);
            }

            string? secretValue;

            try
            {
                secretValue = await secretRetrievalDelegate(secretProperty.SecretStoreKey);
            }
            catch (Exception e)
            {
                throw new SecretRetrievalException(secretProperty, e);
            }

            if (secretValue is null && !options.AllowNullSecretValues)
            {
                throw new NullSecretException(secretProperty);
            }
            
            secretPropTarget.SetValue(this, secretValue);
        }
    }
}