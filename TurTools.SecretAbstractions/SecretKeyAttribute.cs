namespace TurTools.SecretAbstractions;

/// <summary>
/// Indicates that the property is an identifier for a secret which needs to be retrieved from an external store
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SecretKeyAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SecretKeyAttribute"/> class
    /// Indicates that the property is an identifier for a secret which needs to be retrieved from an external store
    /// </summary>
    /// <param name="secretPropertyName">The nameof a property which is to be populated with the secret's value</param>
    public SecretKeyAttribute(string secretPropertyName)
    {
        SecretPropertyName = secretPropertyName;
    }
    
    internal string SecretPropertyName { get; set; }
}