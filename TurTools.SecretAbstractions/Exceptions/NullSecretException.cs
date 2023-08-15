namespace TurTools.SecretAbstractions.Exceptions;

public class NullSecretException : Exception
{
    internal NullSecretException(SecretProperty secretProperty) : base(FormatMessage(secretProperty))
    {
    }

    private static string FormatMessage(SecretProperty secretProperty)
    {
        return $"The the secret retrieval delegate returned null for the secret with name {secretProperty.Name}. " +
               $"To allow null secret values set {nameof(SecretPopulationOptions.AllowNullSecretValues)} to true";
    }
}