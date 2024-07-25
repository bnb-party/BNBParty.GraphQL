namespace BNBParty.GraphQLClient.Responses;

public class GenerateAuthResponse
{
    public DataType? Data { get; set; }

    public class DataType
    {
        public string GenerateAuth { get; set; } = null!;
    }
}