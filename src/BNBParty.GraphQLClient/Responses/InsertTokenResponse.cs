using BNBParty.GraphQLClient.Generated;

namespace BNBParty.GraphQLClient.Responses;

public class InsertTokenResponse
{
    public DataType? Data { get; set; }

    public class DataType
    {
        public Types.NewToken InsertToken { get; set; } = null!;
    }
}