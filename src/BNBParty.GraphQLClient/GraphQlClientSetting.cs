using EnvironmentManager;

namespace BNBParty.GraphQLClient;

public static class GraphQlClientSetting
{
    private static readonly EnvManager EnvManager = new();

    public static readonly string Domain = EnvManager.GetEnvironmentValue<string>("DOMAIN", true);

    public static readonly string Terms = EnvManager.GetEnvironmentValue<string>("TERMS", true);

    public static readonly string Uri = EnvManager.GetEnvironmentValue<string>("URI", true);

    public static readonly string Version = EnvManager.GetEnvironmentValue<string>("VERSION", true);
}