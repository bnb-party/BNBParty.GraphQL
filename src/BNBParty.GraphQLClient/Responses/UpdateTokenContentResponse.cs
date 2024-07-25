using BNBParty.GraphQLClient.Generated;

namespace BNBParty.GraphQLClient.Responses;

public class UpdateTokenContentResponse
{
    public DataType? Data { get; set; }

    public class DataType
    {
        public Types.Token UpdateTokenContent { get; set; } = null!;
    }
}