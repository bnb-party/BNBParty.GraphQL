using System.Diagnostics;
using HandlebarsDotNet;

namespace BNBParty.CodeGen.Services
{
    public class GraphQlCodeGenerator
    {
        public string? Template { get; } = LambdaSettings.Template;
        public string? ApiKey { get; } = LambdaSettings.ApiKey;

        public void GenerateGraphQlCode(string projectRoot, string queriesPath, string generatedPath)
        {
            var handlebars = Handlebars.Create();
            var templateFunction = handlebars.Compile(Template);

            var data = new
            {
                ApiKey,
                queriesPath,
                generatedPath
            };

            var codegenConfig = templateFunction(data);

            var tempConfigPath = Path.Combine(projectRoot, "codegen.temp.yml").Replace("\\", "/");
            File.WriteAllText(tempConfigPath, codegenConfig);

            var npxPath = @"C:\Program Files\nodejs\npx.cmd";

            var processInfo = new ProcessStartInfo(npxPath, $"graphql-codegen --config {tempConfigPath}")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process();
            process.StartInfo = processInfo;
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine("Codegen output:");
            Console.WriteLine(output);
            Console.WriteLine("Codegen errors:");
            Console.WriteLine(error);

            File.Delete(tempConfigPath);
        }
    }
}