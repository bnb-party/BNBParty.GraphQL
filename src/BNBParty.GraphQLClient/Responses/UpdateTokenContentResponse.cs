using static BNBParty.CodeGen.Generated.Types;

namespace BNBParty.GraphQLClient.Responses;

public class UpdateTokenContentResponse
{
    public Data data { get; set; }

    public class Data
    {
        public Token updateTokenContent { get; set; }
    }
}