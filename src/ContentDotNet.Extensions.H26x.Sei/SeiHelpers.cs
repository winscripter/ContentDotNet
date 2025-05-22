namespace ContentDotNet.Extensions.H26x.Sei;

internal static class SeiHelpers
{
    public static void RequireParameterList(SeiModelParameterList? parameterList, string paramName)
    {
        if (parameterList == null)
        {
            throw new ArgumentNullException(paramName, "Parameter list is required.");
        }
    }
}
