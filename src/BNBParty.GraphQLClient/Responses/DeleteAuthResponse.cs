namespace BNBParty.GraphQLClient.Responses;

public class DeleteAuthResponse
{
    public Data data { get; set; }

    public class Data
    {
        public bool deleteAuth { get; set; }
    }
}