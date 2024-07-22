using BNBParty.CodeGen.Services;

namespace BNBParty.CodeGen;

public class Program
{
    private static void Main()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var projectRoot = Directory.GetParent(currentDirectory)?.Parent?.Parent?.Parent?.FullName;

        var sourceFilePath = Path.Combine(projectRoot!, "GraphQLCodeGen", "Generated", "GraphQL.cs").Replace("\\", "/");
        var outputDirectory = Path.Combine(projectRoot!, "GraphQLCodeGen", "Generated").Replace("\\", "/");

        var gen = new GraphQlCodeGenerator();
        gen.GenerateGraphQlCode(projectRoot!, sourceFilePath, outputDirectory);

        var graphQl = new GraphQlFileSplitter();
        graphQl.SplitGraphQlFile(sourceFilePath, outputDirectory);
    }
}