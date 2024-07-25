using System.IO;

namespace BNBParty.GraphQLClient.Services;

public static class GraphQlQueryLoader
{
    public static string LoadQuery(string queryFileName)
    {
        var assembly = typeof(CodeGen.Generated.Types.Query).Assembly;
        var resourceName = $"BNBParty.CodeGen.Queries.{queryFileName}";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            throw new FileNotFoundException($"The query file '{queryFileName}' was not found as an embedded resource.");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public static string LoadMutation(string mutationFileName)
    {
        var assembly = typeof(CodeGen.Generated.Types.Mutation).Assembly;
        var resourceName = $"BNBParty.CodeGen.Queries.{mutationFileName}";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            throw new FileNotFoundException($"The mutation file '{mutationFileName}' was not found as an embedded resource.");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}