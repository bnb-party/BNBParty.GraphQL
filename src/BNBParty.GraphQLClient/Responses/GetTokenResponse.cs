using BNBParty.GraphQLClient.Generated;

namespace BNBParty.GraphQLClient.Responses
{
    public class GetTokenResponse
    {
        public DataType? Data { get; set; }

        public class DataType
        {
            public Types.Token GetToken { get; set; } = null!;
        }
    }
}