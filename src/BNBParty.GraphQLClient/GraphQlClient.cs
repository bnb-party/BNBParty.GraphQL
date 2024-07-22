using BNBParty.GraphQLClient.Responses;
using Flurl.Http;
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

        public async Task<TResponse> QueryAsync<TResponse>(string query, object variables)
        {
            var requestContent = new
            {
                query,
                variables
            };

            var response = await _endpoint
                .WithOAuthBearerToken(_apiKey)
                .PostJsonAsync(requestContent)
                .ReceiveJson<TResponse>();

            return response;
        }

        public async Task<GetTokenResponse> GetTokenAsync(int tokenId)
        {
            var query = @"
            query GetToken($tokenId: Int!) {
              getToken(tokenId: $tokenId) {
                tokenId
                tokenAddress
                makerAddress
                createdAt
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

            var variables = new { tokenId = tokenId };
            return await QueryAsync<GetTokenResponse>(query, variables);
        }

        public async Task<InsertTokenResponse> InsertTokenAsync(NewToken newToken)
        {
            var mutation = @"
            mutation InsertToken($tokenId: Int!, $isNew: Boolean!) {
              insertToken(tokenId: $tokenId, isNew: $isNew) {
                tokenId
                isNew
              }
            }";

            var variables = new { tokenId = newToken.tokenId, isNew = newToken.isNew };
            return await QueryAsync<InsertTokenResponse>(mutation, variables);
        }

        public async Task<GenerateAuthResponse> GenerateAuthAsync(string sign, string message)
        {
            var mutation = @"
            mutation GenerateAuth($sign: String!, $message: String!) {
              generateAuth(sign: $sign, message: $message)
            }";

            var variables = new { sign = sign, message = message };
            var response = await QueryAsync<GenerateAuthResponse>(mutation, variables);

            // Проверка myAddress
            var query = @"
            query {
              myAddress
            }";

            var addressResponse = await QueryAsync<MyAddressResponse>(query, null);
            if (addressResponse.myAddress == "0x0000000000000000000000000000000000000000")
            {
                throw new Exception("Login failed, address is zero.");
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