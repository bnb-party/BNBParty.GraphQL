using System;
using System.Threading.Tasks;
using BNBParty.GraphQLClient.Responses;
using Flurl.Http;
using Newtonsoft.Json;
using static BNBParty.GraphQLClient.Generated.Types;

namespace BNBParty.GraphQLClient;

public class GraphQlClient
{
    private readonly string _endpoint;
    private string _authKey;

    public GraphQlClient(string endpoint)
    {
        _endpoint = endpoint;
        _authKey = Net.Web3.EthereumWallet.EthereumAddress.ZeroAddress;
    }

    public virtual async Task<TResponse> QueryAsync<TResponse>(string query, object? variables)
    {
        var requestContent = new
        {
            query,
            variables
        };

        try
        {
            var response = await _endpoint
                .WithHeader("Authorization", _authKey)
                .PostJsonAsync(requestContent)
                .ReceiveString();

            var deserializedResponse = JsonConvert.DeserializeObject<TResponse>(response);

            return deserializedResponse!;
        }
        catch (FlurlHttpException ex)
        {
            var error = await ex.GetResponseStringAsync();
            throw new Exception($"Request failed with status code {ex.StatusCode}: {error}");
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }

    public virtual async Task<bool> LoginAsync(string sign, string message)
    {
        var mutation = Services.GraphQlQueryLoader.LoadMutation("GenerateAuthMutation.graphql");
        var variables = new { sign, message };

        var authResponse = await QueryAsync<GenerateAuthResponse>(mutation, variables);

        if (authResponse.Data!.GenerateAuth != sign)
        {
            Console.WriteLine("Login failed, address is zero.");
            return false;
        }

        _authKey = authResponse.Data.GenerateAuth;
        return true;
    }

    public virtual async Task<bool> LogoutAsync(string sign)
    {
        var mutation = Services.GraphQlQueryLoader.LoadMutation("DeleteAuthMutation.graphql");
        var variables = new { sign };

        var response = await QueryAsync<DeleteAuthResponse>(mutation, variables);

        if (response.Data!.DeleteAuth)
        {
            _authKey = Net.Web3.EthereumWallet.EthereumAddress.ZeroAddress;
            Console.WriteLine("Logged out successfully.");
            return true;
        }

        Console.WriteLine("Logout failed.");
        return false;
    }

    public virtual async Task<MyAddressResponse> GetMyAddressAsync()
    {
        var query = Services.GraphQlQueryLoader.LoadQuery("MyAddressQuery.graphql");

        return await QueryAsync<MyAddressResponse>(query, null);
    }

    public virtual async Task<GetTokenResponse> GetTokenAsync(int tokenId)
    {
        var query = Services.GraphQlQueryLoader.LoadQuery("GetTokenQuery.graphql");
        var variables = new { tokenId };
        return await QueryAsync<GetTokenResponse>(query, variables);
    }

    public virtual async Task<InsertTokenResponse> InsertTokenAsync(long chainId, string txHash)
    {
        var mutation = Services.GraphQlQueryLoader.LoadMutation("InsertTokenMutation.graphql");
        var variables = new { chainId, txHash };
        return await QueryAsync<InsertTokenResponse>(mutation, variables);
    }

    public virtual async Task<GenerateAuthResponse> GenerateAuthAsync(string sign, string message)
    {
        var mutation = Services.GraphQlQueryLoader.LoadMutation("GenerateAuthMutation.graphql");
        var variables = new { sign, message };
        var response = await QueryAsync<GenerateAuthResponse>(mutation, variables);


        var query = Services.GraphQlQueryLoader.LoadQuery("MyAddressQuery.graphql");
        var addressResponse = await QueryAsync<MyAddressResponse>(query, null);
        if (addressResponse.Data!.MyAddress == Net.Web3.EthereumWallet.EthereumAddress.ZeroAddress)
        {
            Console.WriteLine("Login failed, address is zero.");
        }

        return response;
    }

    public virtual async Task<UpdateTokenContentResponse> UpdateTokenContentAsync(long tokenId, OffChainDataInput offChainDataInput)
    {
        var mutation = Services.GraphQlQueryLoader.LoadMutation("UpdateTokenContentMutation.graphql");
        var variables = new
        {
            tokenId,
            offChainData = new
            {
                offChainDataInput.content,
                offChainDataInput.icon,
                offChainDataInput.Discord,
                offChainDataInput.Telegram,
                offChainDataInput.Website,
                offChainDataInput.X
            }
        };

        return await QueryAsync<UpdateTokenContentResponse>(mutation, variables);
    }

    public virtual string GenerateMessage(string accountAddress, long chainId)
    {
        var issuedAtDateTime = DateTimeOffset.UtcNow;

        var domain = GraphQlClientSetting.Domain;
        var terms = GraphQlClientSetting.Terms;
        var uri = GraphQlClientSetting.Uri;
        var version = GraphQlClientSetting.Version;
        var nonce = $"Nonce: {issuedAtDateTime.ToUnixTimeMilliseconds()}";
        var issuedAt = $"Issued At: {issuedAtDateTime}";
        var expirationTime = $"Expiration Time: {issuedAtDateTime.AddDays(1)}";

        return $"{domain}\n{accountAddress}\n\n{terms}\n\n{uri}\n{version}\nChain ID: {chainId}\n{nonce}\n{issuedAt}\n{expirationTime}";
    }
}