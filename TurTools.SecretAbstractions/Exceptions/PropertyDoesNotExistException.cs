namespace TurTools.SecretAbstractions.Exceptions;

public class PropertyDoesNotExistException : Exception
{

    internal PropertyDoesNotExistException(string secretTargetValue) : base(FormatMessage(secretTargetValue))
    {
        
    }

    private static string FormatMessage(string secretTargetValue)
    {
        return $"A SecretKeyAttribute points to a property ({secretTargetValue}) " +
               "in the  which does not exist.";
    }
}