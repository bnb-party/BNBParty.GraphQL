using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BNBParty.CodeGen.Services;

public class GraphQlFileSplitter
{
    public void SplitGraphQlFile(string sourceFilePath, string outputDirectory)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(sourceFilePath));
        var root = syntaxTree.GetRoot();

        var typesClass = root.DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .FirstOrDefault(c => c.Identifier.Text == "Types");

        if (typesClass != null)
        {
            foreach (var innerClass in typesClass.Members.OfType<ClassDeclarationSyntax>())
            {
                var namespaceDeclaration = SyntaxFactory
                    .NamespaceDeclaration(SyntaxFactory.ParseName("GraphQLCodeGen.Generated"))
                    .AddMembers(innerClass);

                var compilationUnit = SyntaxFactory
                    .CompilationUnit()
                    .AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Newtonsoft.Json")))
                    .AddMembers(namespaceDeclaration)
                    .NormalizeWhitespace();

                var newFileContent = compilationUnit.ToFullString();

                var newFilePath = Path.Combine(outputDirectory, $"{innerClass.Identifier.ValueText}.cs");
                File.WriteAllText(newFilePath, newFileContent);

                Console.WriteLine($"Class {innerClass.Identifier.ValueText} has been written to {newFilePath}");
            }
        }
        else
        {
            Console.WriteLine("The 'Types' class was not found in the provided file.");
        }
    }
}