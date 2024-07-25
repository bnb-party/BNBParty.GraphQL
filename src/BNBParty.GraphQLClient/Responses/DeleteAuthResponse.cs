namespace BNBParty.GraphQLClient.Responses;

public class DeleteAuthResponse
{
    public DataType? Data { get; set; }

    public class DataType
    {
        public bool DeleteAuth { get; set; }
    }
}