using System;
using System.Threading.Tasks;
using BNBParty.GraphQLClient.Responses;
using Flurl.Http;
using Newtonsoft.Json;
using static BNBParty.CodeGen.Generated.Types;

namespace BNBParty.GraphQLClient
{
    public class GraphQlClient
    {
        private readonly string _endpoint;
        private string _authKey;

        public GraphQlClient(string endpoint)
        {
            _endpoint = endpoint;
            _authKey = Net.Web3.EthereumWallet.EthereumAddress.ZeroAddress;
        }

        public async Task<TResponse> QueryAsync<TResponse>(string query, object? variables)
        {
            var requestContent = new
            {
                query,
                variables
            };

            try
            {
                var response = await _endpoint
                    .WithOAuthBearerToken(_authKey)
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

        public async Task<bool> LoginAsync(string sign, string message)
        {
            var mutation = Services.GraphQlQueryLoader.LoadMutation("GenerateAuthMutation.graphql");
            var variables = new { sign, message };

            var authResponse = await QueryAsync<GenerateAuthResponse>(mutation, variables);

            if (authResponse?.data == null || authResponse.data.generateAuth != sign)
            {
                Console.WriteLine("Login failed, address is zero.");
                return false;
            }

            _authKey = authResponse.data.generateAuth;
            return true;
        }

        public async Task<bool> LogoutAsync(string sign)
        {
            var mutation = Services.GraphQlQueryLoader.LoadMutation("DeleteAuthMutation.graphql");
            var variables = new { sign };

            var response = await QueryAsync<DeleteAuthResponse>(mutation, variables);

            if (response.data.deleteAuth)
            {
                _authKey = Net.Web3.EthereumWallet.EthereumAddress.ZeroAddress;
                Console.WriteLine("Logged out successfully.");
                return true;
            }

            Console.WriteLine("Logout failed.");
            return false;
        }

        public async Task<GetTokenResponse> GetTokenAsync(int tokenId)
        {
            var query = Services.GraphQlQueryLoader.LoadQuery("GetTokenQuery.graphql");
            var variables = new { tokenId };
            return await QueryAsync<GetTokenResponse>(query, variables);
        }

        public async Task<InsertTokenResponse> InsertTokenAsync(long chainId, string txHash)
        {
            var mutation = Services.GraphQlQueryLoader.LoadMutation("InsertTokenMutation.graphql");
            var variables = new { chainId, txHash };
            return await QueryAsync<InsertTokenResponse>(mutation, variables);
        }

        public async Task<GenerateAuthResponse> GenerateAuthAsync(string sign, string message)
        {
            var mutation = Services.GraphQlQueryLoader.LoadMutation("GenerateAuthMutation.graphql");
            var variables = new { sign, message };
            var response = await QueryAsync<GenerateAuthResponse>(mutation, variables);


            var query = Services.GraphQlQueryLoader.LoadQuery("MyAddressQuery.graphql");
            var addressResponse = await QueryAsync<MyAddressResponse>(query, null);
            if (addressResponse.data.myAddress == Net.Web3.EthereumWallet.EthereumAddress.ZeroAddress)
            {
                Console.WriteLine("Login failed, address is zero.");
            }

            return response;
        }

        public async Task<UpdateTokenContentResponse> UpdateTokenContentAsync(Token token)
        {
            var mutation = Services.GraphQlQueryLoader.LoadMutation("UpdateTokenContentMutation.graphql");
            var variables = new
            {
                token.tokenId,
                offChainData = new
                {
                    token.offChainData.content,
                    token.offChainData.icon,
                    token.offChainData.likeCounter,
                    token.offChainData.Discord,
                    token.offChainData.Telegram,
                    token.offChainData.Website,
                    token.offChainData.X
                }
            };

            return await QueryAsync<UpdateTokenContentResponse>(mutation, variables);
        }
    }
}