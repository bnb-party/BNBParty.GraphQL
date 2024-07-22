using EnvironmentManager;

namespace BNBParty.CodeGen;

public static class LambdaSettings
{
    private static readonly EnvManager EnvManager = new();

    public static readonly string ApiKey = EnvManager.GetEnvironmentValue<string>("GRAPHQL_API_KEY", true);

    public static readonly string Template = EnvManager.GetEnvironmentValue<string>("GRAPHQL_CODEGEN_TEMPLATE", true);
}