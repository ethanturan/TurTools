namespace TurTools.SecretAbstractions.Exceptions;

internal class SecretRetrievalException : Exception
{
    internal SecretRetrievalException(SecretProperty secretProperty, Exception? innerException) : base(
        FormatMessage(secretProperty), innerException)
    {
    }

    private static string FormatMessage(SecretProperty secretProperty)
    {
        return $"The secret with key {secretProperty.SecretStoreKey} could not be retrieved form the secret store";
    }
}