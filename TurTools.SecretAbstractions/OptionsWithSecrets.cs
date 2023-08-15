using System.Reflection;
using TurTools.SecretAbstractions.Exceptions;
using NullReferenceException = System.NullReferenceException;

namespace TurTools.SecretAbstractions;

/// <summary>
/// Options that contain properties which are identifiers for secrets and are decorated with <see cref="SecretKeyAttribute"/>
/// </summary>
public abstract class OptionsWithSecrets
{
    public async Task PopulateSecrets<T>(Func<string, Task<string?>> getSecret, SecretPopulationOptions? options = null)
    {
        options ??= new SecretPopulationOptions();
        
        var optionsProps = typeof(T).GetProperties();
        
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
                secretValue = await getSecret(secretProperty.SecretStoreKey);
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