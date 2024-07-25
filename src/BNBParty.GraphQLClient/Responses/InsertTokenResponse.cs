using static BNBParty.CodeGen.Generated.Types;

namespace BNBParty.GraphQLClient.Responses;

public class InsertTokenResponse
{
    public DataType? Data { get; set; }

    public class DataType
    {
        public NewToken InsertToken { get; set; } = null!;
    }
}