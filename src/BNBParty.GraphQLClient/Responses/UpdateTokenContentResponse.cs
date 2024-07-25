using static BNBParty.CodeGen.Generated.Types;

namespace BNBParty.GraphQLClient.Responses;

public class UpdateTokenContentResponse
{
    public DataType? Data { get; set; }

    public class DataType
    {
        public Token UpdateTokenContent { get; set; } = null!;
    }
}