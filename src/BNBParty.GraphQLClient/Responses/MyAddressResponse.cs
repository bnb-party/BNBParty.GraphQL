namespace BNBParty.GraphQLClient.Responses;

public class MyAddressResponse
{
    public Data data { get; set; }

    public class Data
    {
        public string myAddress { get; set; }
    }
}