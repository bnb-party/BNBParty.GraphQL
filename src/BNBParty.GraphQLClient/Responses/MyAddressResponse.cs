namespace BNBParty.GraphQLClient.Responses;

public class MyAddressResponse
{
    public DataType? Data { get; set; }

    public class DataType
    {
        public string MyAddress { get; set; } = null!;
    }
}