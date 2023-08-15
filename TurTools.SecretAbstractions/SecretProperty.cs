namespace TurTools.SecretAbstractions;

internal class SecretProperty
{
    /// <summary>
    /// Creates a new SecretProperty
    /// </summary>
    /// <param name="name">The nameof a property which is to be set with a secret value</param>
    /// <param name="secretStoreKey">The key to a secret in an external secret store</param>
    public SecretProperty(string name, string secretStoreKey)
    {
        Name = name;
        SecretStoreKey = secretStoreKey;
    }

    /// <summary>
    /// The nameof a property which is to be set with a secret value
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The key to a secret in an external secret store
    /// </summary>
    public string SecretStoreKey { get; set; }
}