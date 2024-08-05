namespace BNBParty.GraphQLClient.Responses;

internal class GetMyAddressResponse
{
    public DataType? Data { get; set; }

    public class DataType
    {
        public string GetMyAddress { get; set; } = null!;
    }
}