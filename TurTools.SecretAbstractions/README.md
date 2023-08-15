## Overview
`TuranTools.SecretAbstractions` makes it easy to register options which contain both plain text configuration and secrets which need to be programatically retrieved by a unique identifier.

### OptionsWithSecrets
Properties of objects which inherit from `OptionsWithSecrets` may use the `[SecretKey()]` attribute which specifies that the property contains the key to a value which is to be retrieved from a secret store.
```csharp
public class ExampleOptionsWithSecrets : OptionsWithSecrets
{
    [SecretKey(nameof(SecretValue1))]
    public string SecretKey1 { get; set; }
    public string SecretValue1 { get; set; }
}
```

Here, `SecretValue1` will be retrieved using the key held in the value of `SecretKey1`

### Registering OptionsWithSecrets in DI
A section of configuration whose corresponding options object inherits from `OptionsWithSecrets` may be configured in dependency injection

```csharp
builder.AddOptionsWithSecrets("ConfigurationSectionName", async secretKey => await getSecretDelegate(secretKey))
```

where `getSecretDelegate()` fetches the secret from the secret store by it's key at startup.