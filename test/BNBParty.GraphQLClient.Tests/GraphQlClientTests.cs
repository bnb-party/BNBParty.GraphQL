using FluentAssertions;
using Flurl.Http.Testing;
using Xunit;
using static BNBParty.CodeGen.Generated.Types;

namespace BNBParty.GraphQLClient.Tests
{
    public class GraphQlClientTests
    {
        private const string Endpoint = "https://test/graphql";
        private const string ApiKey = "test";

        private GraphQlClient CreateClient()
        {
            return new GraphQlClient(Endpoint);
        }

        [Fact]
        public async Task GetTokenAsync_ShouldReturnToken()
        {
            using (var httpTest = new HttpTest())
            {
                var client = CreateClient();
                var tokenId = 3;
                var response = new
                {
                    data = new
                    {
                        getToken = new Token
                        {
                            tokenId = 3,
                            tokenAddress = "0x9c853178CD5d73b1B50dfE19d6784858c7617bE7",
                            chainId = 97,
                            makerAddress = "0x57e436b0Aa38f56670d9fdc50530AC9546b11694",
                            flpAddress = "0xe2C9DdfaC782a7dffad5BbDCF0364B842CfE9199",
                            creationTransaction = "0x8fb56bd99eba01f5f329a781ae3e1a29876deb110caebf5179323d9df2cd91df",
                            createdAt = "2024-07-17T11:33:51",
                            Block = 42165346,
                            offChainData = new OffChainData
                            {
                                content = null!,
                                icon = null!,
                                Website = null!,
                                X = null!,
                                Discord = null!,
                                Telegram = null!,
                                likeCounter = 3
                            }
                        }
                    }
                };
                httpTest.RespondWithJson(response);

                var result = await client.GetTokenAsync(tokenId);

                result.data.getToken.Should().NotBeNull();
                result.data.getToken.tokenId.Should().Be(3);
                result.data.getToken.tokenAddress.Should().Be("0x9c853178CD5d73b1B50dfE19d6784858c7617bE7");
            }
        }

        [Fact]
        public async Task InsertTokenAsync_ShouldReturnInsertedToken()
        {
            using var httpTest = new HttpTest();
            var client = CreateClient();
            var chainId = 97;
            var txHash = "212d6de7f588b193a492ff89faa387806cdca26bb5a07ff1ce860d9d0630d285";
            var response = new
            {
                data = new
                {
                    insertToken = new
                    {
                        tokenId = 123,
                        isNew = true
                    }
                }
            };
            httpTest.RespondWithJson(response);

            var result = await client.InsertTokenAsync(chainId, txHash);

            result.data.insertToken.Should().NotBeNull();
            result.data.insertToken.tokenId.Should().Be(123);
            result.data.insertToken.isNew.Should().Be(true);
        }

        [Fact]
        public async Task GenerateAuthAsync_ShouldReturnAuth()
        {
            using var httpTest = new HttpTest();
            var client = CreateClient();
            var sign = "someSign";
            var message = "someMessage";
            var response = new
            {
                data = new
                {
                    generateAuth = "auth_token"
                }
            };
            httpTest.RespondWithJson(response);
            var addressResponse = new
            {
                data = new
                {
                    myAddress = "0x1234567890abcdef1234567890abcdef12345678"
                }
            };
            httpTest.RespondWithJson(addressResponse);

            var result = await client.GenerateAuthAsync(sign, message);

            result.data.generateAuth.Should().Be("auth_token");
        }

        [Fact]
        public async Task UpdateTokenContentAsync_ShouldReturnUpdatedToken()
        {
            using var httpTest = new HttpTest();
            var client = CreateClient();
            var token = new Token
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
            var response = new
            {
                data = new
                {
                    updateTokenContent = new
                    {
                        tokenId = 123,
                        offChainData = new
                        {
                            content = "new content",
                            icon = "new icon",
                            likeCounter = 10,
                            Discord = "discord_link",
                            Telegram = "telegram_link",
                            Website = "website_link",
                            X = "x_link"
                        }
                    }
                }
            };
            httpTest.RespondWithJson(response);

            var result = await client.UpdateTokenContentAsync(token);

            result.data.updateTokenContent.Should().NotBeNull();
            result.data.updateTokenContent.tokenId.Should().Be(123);
            result.data.updateTokenContent.offChainData.content.Should().Be("new content");
            result.data.updateTokenContent.offChainData.icon.Should().Be("new icon");
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnTrue_WhenLoginIsSuccessful()
        {
            using var httpTest = new HttpTest();
            var client = CreateClient();
            var sign = "someSign";
            var message = "someMessage";
            var authResponse = new
            {
                data = new
                {
                    generateAuth = "someSign"
                }
            };

            httpTest
                .ForCallsTo("https://test/graphql")
                .RespondWithJson(authResponse);

            var result = await client.LoginAsync(sign, message);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnFalse_WhenLoginFails()
        {
            using var httpTest = new HttpTest();
            var client = CreateClient();
            var sign = "someSign";
            var message = "someMessage";
            var authResponse = new
            {
                data = new
                {
                    generateAuth = "wrong_auth_token"
                }
            };
            var addressResponse = new
            {
                data = new
                {
                    myAddress = "0x0000000000000000000000000000000000000000"
                }
            };

            httpTest
                .ForCallsTo("https://test/graphql")
                .RespondWithJson(authResponse)
                .RespondWithJson(addressResponse);

            var result = await client.LoginAsync(sign, message);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task LogoutAsync_ShouldReturnTrue_WhenLogoutIsSuccessful()
        {
            using var httpTest = new HttpTest();
            var client = CreateClient();
            var sign = "someSign";
            var response = new
            {
                data = new
                {
                    deleteAuth = true
                }
            };

            httpTest.RespondWithJson(response);

            var result = await client.LogoutAsync(sign);

            result.Should().BeTrue();
        }

    }
}
