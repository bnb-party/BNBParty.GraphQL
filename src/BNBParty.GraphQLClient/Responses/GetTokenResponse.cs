using static BNBParty.CodeGen.Generated.Types;

namespace BNBParty.GraphQLClient.Responses
{
    public class GetTokenResponse
    {
        public DataType? Data { get; set; }

        public class DataType
        {
            public Token GetToken { get; set; } = null!;
        }
    }
}