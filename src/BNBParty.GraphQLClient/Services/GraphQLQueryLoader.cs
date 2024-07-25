using System.IO;
using BNBParty.GraphQLClient.Generated;

namespace BNBParty.GraphQLClient.Services;

public static class GraphQlQueryLoader
{
    public static string LoadQuery(string queryFileName)
    {
        var assembly = typeof(Types.Query).Assembly;
        var resourceName = $"BNBParty.GraphQLClient.Queries.{queryFileName}";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            throw new FileNotFoundException($"The query file '{queryFileName}' was not found as an embedded resource.");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public static string LoadMutation(string mutationFileName)
    {
        var assembly = typeof(Types.Mutation).Assembly;
        var resourceName = $"BNBParty.GraphQLClient.Queries.{mutationFileName}";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            throw new FileNotFoundException($"The mutation file '{mutationFileName}' was not found as an embedded resource.");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}