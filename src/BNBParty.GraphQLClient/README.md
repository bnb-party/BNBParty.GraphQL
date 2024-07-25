# BNBParty.GraphQL

BNBParty.GraphQL is a library for interacting with BNBParty.GraphQL project's GraphQL endpoint. It allows you to perform queries to the GraphQL server and retrieve data.

# Installation

Clone the repository:

```
dotnet add package BNBParty.GraphQLClient
```

# Usage

1. Add necessary packages:
   ```
   dotnet add package Flurl.Http
   dotnet add package Newtonsoft.Json
   ```
2. Create a new instance of the GraphQlClient class with the endpoint.
# Example Usage

Here is a basic example of how to use the library to perform queries to the GraphQL server:

```
using BNBParty.GraphQLClient;
using BNBParty.CodeGen.Generated.Types;
using System;
using System.Threading.Tasks;

namespace GraphQLClient.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var endpoint = "https://origin/graphql";
            var apiKey = "origin";
            var client = new GraphQlClient(endpoint, apiKey);

            try
            {
                // Example of GetToken query
                var getTokenResponse = await client.GetTokenAsync(3);
                if (getTokenResponse?.data?.getToken != null)
                {
                    var token = getTokenResponse.data.getToken;
                    Console.WriteLine($"Token ID: {token.tokenId}");
                    Console.WriteLine($"Token Address: {token.tokenAddress}");
                }

                // Example of InsertToken mutation
                var chainId = 97;
                var txHash = "0x212d6de7f588b193a492ff89faa387806cdca26bb5a07ff1ce860d9d0630d285";
                var insertTokenResponse = await client.InsertTokenAsync(chainId, txHash);
                Console.WriteLine($"Inserted Token ID: {insertTokenResponse.data.insertToken.tokenId}");
                Console.WriteLine($"Is New: {insertTokenResponse.data.insertToken.isNew}");

                // Example of GenerateAuth mutation
                var generateAuthResponse = await client.GenerateAuthAsync("someSign", "someMessage");
                Console.WriteLine($"Generated Auth: {generateAuthResponse.data.generateAuth}");

                // Example of UpdateTokenContent mutation
                var tokenToUpdate = new Token
                {
                    tokenId = 123,
                    offChainData = new OffChainData
                    {
                        content = "new content",
                        icon = "new icon",
                        likeCounter = 10,
                        Discord = "discord_link",
                        Telegram = "telegram_link",
                        Website = "website_link",
                        X = "x_link"
                    }
                };
                var updateTokenContentResponse = await client.UpdateTokenContentAsync(tokenToUpdate);
                Console.WriteLine($"Updated Token ID: {updateTokenContentResponse.data.updateTokenContent.tokenId}");
                Console.WriteLine($"Updated Content: {updateTokenContentResponse.data.updateTokenContent.offChainData.content}");
             // Example of Login mutation
                var loginResponse = await client.LoginAsync("someSign", "someMessage");
                if (loginResponse)
                {
                     Console.WriteLine("Login successful");
                }
                else
                {
                     Console.WriteLine("Login failed");
                }

                // Example of Logout mutation
                var logoutResponse = await client.LogoutAsync("someSign");
                if (logoutResponse)
                {
                     Console.WriteLine("Logout successful");
                }
                else
                {
                     Console.WriteLine("Logout failed");
                }
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
```

# Methods

GetTokenAsync
Query to retrieve information about a token by its ID.

```
public async Task<GetTokenResponse> GetTokenAsync(int tokenId);
```

InsertTokenAsync
Mutation to insert a new token.

```
public async Task<InsertTokenResponse> InsertTokenAsync(long chainId, string txHash);
```

GenerateAuthAsync
Mutation to generate an authorization token.

```
public async Task<GenerateAuthResponse> GenerateAuthAsync(string sign, string message);
```

UpdateTokenContentAsync
Mutation to update the content of a token.

```
public async Task<UpdateTokenContentResponse> UpdateTokenContentAsync(Token token);
```

LoginAsync
Mutation to login and generate an authorization token.


```
public async Task<bool> LoginAsync(string sign, string message);
```

LogoutAsync
Mutation to logout and delete the authorization token.


```
public async Task<bool> LogoutAsync(string sign);
```