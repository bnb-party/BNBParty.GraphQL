using static BNBParty.CodeGen.Generated.Types;

namespace BNBParty.GraphQLClient.Responses
{
    public class GetTokenResponse
    {
        public Data data { get; set; }

        public class Data
        {
            public Token getToken { get; set; }
        }
    }
}