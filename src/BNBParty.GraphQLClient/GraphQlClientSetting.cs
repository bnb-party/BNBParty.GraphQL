using EnvironmentManager;

namespace BNBParty.GraphQLClient;

public static class GraphQlClientSetting
{
    private static readonly EnvManager EnvManager = new();

    public static readonly string Domain = EnvManager.GetEnvironmentValue<string>("GRAPH_QL_CLIENT_DOMAIN", true);

    public static readonly string Terms = EnvManager.GetEnvironmentValue<string>("GRAPH_QL_CLIENT_TERMS", true);

    public static readonly string Uri = EnvManager.GetEnvironmentValue<string>("GRAPH_QL_CLIENT_URI", true);

    public static readonly string Version = EnvManager.GetEnvironmentValue<string>("GRAPH_QL_CLIENT_VERSION", true);
}