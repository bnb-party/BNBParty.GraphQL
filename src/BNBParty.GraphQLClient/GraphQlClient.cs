using BNBParty.GraphQLClient.Responses;
using Flurl.Http;
using Newtonsoft.Json;
using static BNBParty.CodeGen.Generated.Types;

namespace BNBParty.GraphQLClient
{
    public class GraphQlClient
    {
        private readonly string _endpoint;
        private readonly string _apiKey;

        public GraphQlClient(string endpoint, string apiKey)
        {
            _endpoint = endpoint;
            _apiKey = apiKey;
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
                    .WithOAuthBearerToken(_apiKey)
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

        public async Task<GetTokenResponse> GetTokenAsync(int tokenId)
        {
            var query = @"
            query GetToken($tokenId: Int!) {
              getToken(tokenId: $tokenId) {
                tokenId
                tokenAddress
                chainId
                makerAddress
                flpAddress
                creationTransaction
                createdAt
                Block
                offChainData {
                  content
                  icon
                  Website
                  X
                  Discord
                  Telegram
                  likeCounter
                }
              }
            }";

            var variables = new { tokenId };
            return await QueryAsync<GetTokenResponse>(query, variables);
        }

        public async Task<InsertTokenResponse> InsertTokenAsync(long chainId, string txHash)
        {
            var mutation = @"
                mutation InsertToken($chainId: Long!, $txHash: String!) {
                insertToken(chainID: $chainId, txHash: $txHash) {
                    tokenId
                    isNew
                }
            }";

            var variables = new { chainId, txHash };
            return await QueryAsync<InsertTokenResponse>(mutation, variables);
        }

        public async Task<GenerateAuthResponse> GenerateAuthAsync(string sign, string message)
        {
            var mutation = @"
            mutation GenerateAuth($sign: String!, $message: String!) {
              generateAuth(sign: $sign, message: $message)
            }";

            var variables = new { sign, message };
            var response = await QueryAsync<GenerateAuthResponse>(mutation, variables);

            var query = @"
            query {
              myAddress
            }";

            var addressResponse = await QueryAsync<MyAddressResponse>(query, null);
            if (addressResponse.data.myAddress == "0x0000000000000000000000000000000000000000")
            {
                Console.WriteLine("Login failed, address is zero.");
            }

            return response;
        }

        public async Task<UpdateTokenContentResponse> UpdateTokenContentAsync(Token token)
        {
            var mutation = @"
            mutation UpdateTokenContent($tokenId: Int!, $offChainData: OffChainDataInput!) {
            updateTokenContent(tokenId: $tokenId, offChainData: $offChainData) {
                tokenId
                offChainData {
                  content
                  icon
                  likeCounter
                Discord
                  Telegram
               Website
                X
                    }
                }
            }";

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