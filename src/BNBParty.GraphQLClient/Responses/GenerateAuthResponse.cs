namespace BNBParty.GraphQLClient.Responses;

public class GenerateAuthResponse
{
    public Data data { get; set; }

    public class Data
    {
        public string generateAuth { get; set; }
    }
}