using static BNBParty.CodeGen.Generated.Types;

namespace BNBParty.GraphQLClient.Responses;

public class InsertTokenResponse
{
    public Data data { get; set; }

    public class Data
    {
        public NewToken insertToken { get; set; }
    }
}