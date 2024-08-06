using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BNBParty.CodeGen.Generated {
  public class Types {
    
    #region Comment
    public class Comment {
      #region members
      [JsonProperty("commentId")]
      public int commentId { get; set; }
    
      [JsonProperty("content")]
      public string content { get; set; }
    
      [JsonProperty("createdAt")]
      public string createdAt { get; set; }
    
      [JsonProperty("icon")]
      public string icon { get; set; }
    
      [JsonProperty("likeCounter")]
      public int likeCounter { get; set; }
    
      [JsonProperty("tokenId")]
      public int tokenId { get; set; }
    
      [JsonProperty("userAddress")]
      public string userAddress { get; set; }
      #endregion
    }
    #endregion
    
    #region Comments
    public class Comments {
      #region members
      [JsonProperty("commentsCount")]
      public int commentsCount { get; set; }
    
      [JsonProperty("items")]
      public List<Comment> items { get; set; }
      #endregion
    }
    #endregion
    
    public interface IUser {
      [JsonProperty("Discord")]
      string Discord { get; set; }
    
      [JsonProperty("Telegram")]
      string Telegram { get; set; }
    
      [JsonProperty("X")]
      string X { get; set; }
    
      [JsonProperty("aboutMe")]
      string aboutMe { get; set; }
    
      [JsonProperty("address")]
      string address { get; set; }
    
      [JsonProperty("avatarUrl")]
      string avatarUrl { get; set; }
    
      [JsonProperty("userName")]
      string userName { get; set; }
    }
    
    #region Mutation
    public class Mutation {
      #region members
      [JsonProperty("addComment")]
      public Comment addComment { get; set; }
    
      [JsonProperty("deleteAuth")]
      public bool deleteAuth { get; set; }
    
      [JsonProperty("generateAuth")]
      public string generateAuth { get; set; }
    
      [JsonProperty("insertToken")]
      public NewToken insertToken { get; set; }
    
      [JsonProperty("likeComment")]
      public Comment likeComment { get; set; }
    
      [JsonProperty("likeToken")]
      public Token likeToken { get; set; }
    
      [JsonProperty("myFollowOn")]
      public UserWithFollowersAndFollowings myFollowOn { get; set; }
    
      [JsonProperty("updateMyData")]
      public UserWithFollowersAndFollowings updateMyData { get; set; }
    
      [JsonProperty("updateTokenContent")]
      public Token updateTokenContent { get; set; }
      #endregion
    }
    #endregion
    
    #region NewToken
    public class NewToken {
      #region members
      [JsonProperty("isNew")]
      public bool isNew { get; set; }
    
      [JsonProperty("tokenId")]
      public int tokenId { get; set; }
      #endregion
    }
    #endregion
    
    #region OffChainData
    public class OffChainData {
      #region members
      [JsonProperty("Discord")]
      public string Discord { get; set; }
    
      [JsonProperty("Telegram")]
      public string Telegram { get; set; }
    
      [JsonProperty("Website")]
      public string Website { get; set; }
    
      [JsonProperty("X")]
      public string X { get; set; }
    
      [JsonProperty("content")]
      public string content { get; set; }
    
      [JsonProperty("icon")]
      public string icon { get; set; }
    
      [JsonProperty("likeCounter")]
      public int likeCounter { get; set; }
      #endregion
    }
    #endregion
    
    #region OffChainDataInput
    public class OffChainDataInput {
      #region members
      public string Discord { get; set; }
    
      public string Telegram { get; set; }
    
      public string Website { get; set; }
    
      public string X { get; set; }
    
      public string content { get; set; }
    
      public string icon { get; set; }
      #endregion
    
      #region methods
      public dynamic GetInputObject()
      {
        IDictionary<string, object> d = new System.Dynamic.ExpandoObject();
    
        var properties = GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        foreach (var propertyInfo in properties)
        {
          var value = propertyInfo.GetValue(this);
          var defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;
    
          var requiredProp = propertyInfo.GetCustomAttributes(typeof(JsonRequiredAttribute), false).Length > 0;
    
          if (requiredProp || value != defaultValue)
          {
            d[propertyInfo.Name] = value;
          }
        }
        return d;
      }
      #endregion
    }
    #endregion
    
    #region Query
    public class Query {
      #region members
      [JsonProperty("countTokens")]
      public int countTokens { get; set; }
    
      [JsonProperty("getCoinsCreated")]
      public List<Token> getCoinsCreated { get; set; }
    
      [JsonProperty("getComment")]
      public Comments getComment { get; set; }
    
      [JsonProperty("getReplies")]
      public List<Comment> getReplies { get; set; }
    
      [JsonProperty("getToken")]
      public Token getToken { get; set; }
    
      [JsonProperty("getTokenByTokenAddress")]
      public Token getTokenByTokenAddress { get; set; }
    
      [JsonProperty("getTokenHolders")]
      public List<UserBalance> getTokenHolders { get; set; }
    
      [JsonProperty("getUserData")]
      public UserWithFollowersAndFollowings getUserData { get; set; }
    
      [JsonProperty("listMyTokens")]
      public List<Token> listMyTokens { get; set; }
    
      [JsonProperty("listTokensByDate")]
      public List<Token> listTokensByDate { get; set; }
    
      [JsonProperty("listTokensByLike")]
      public List<Token> listTokensByLike { get; set; }
    
      [JsonProperty("myAddress")]
      public string myAddress { get; set; }
      #endregion
    }
    #endregion
    
    #region Subscription
    public class Subscription {
      #region members
      /// <summary>
      /// both for createToken and insertToken
      /// </summary>
      [JsonProperty("likedComment")]
      public Comment likedComment { get; set; }
    
      [JsonProperty("likedToken")]
      public Token likedToken { get; set; }
    
      /// <summary>
      /// used only on updateTokenContent
      /// </summary>
      [JsonProperty("newComment")]
      public Comment newComment { get; set; }
    
      /// <summary>
      /// for each Token
      /// </summary>
      [JsonProperty("newToken")]
      public NewToken newToken { get; set; }
    
      [JsonProperty("tokenUpdated")]
      public Token tokenUpdated { get; set; }
      #endregion
    }
    #endregion
    
    #region Token
    public class Token {
      #region members
      [JsonProperty("Block")]
      public int Block { get; set; }
    
      [JsonProperty("chainId")]
      public long chainId { get; set; }
    
      [JsonProperty("createdAt")]
      public string createdAt { get; set; }
    
      [JsonProperty("creationTransaction")]
      public string creationTransaction { get; set; }
    
      [JsonProperty("flpAddress")]
      public string flpAddress { get; set; }
    
      [JsonProperty("makerAddress")]
      public string makerAddress { get; set; }
    
      [JsonProperty("offChainData")]
      public OffChainData offChainData { get; set; }
    
      [JsonProperty("partyFactoryAddress")]
      public string partyFactoryAddress { get; set; }
    
      [JsonProperty("tokenAddress")]
      public string tokenAddress { get; set; }
    
      [JsonProperty("tokenId")]
      public int tokenId { get; set; }
      #endregion
    }
    #endregion
    
    #region User
    public class User : IUser {
      #region members
      [JsonProperty("Discord")]
      public string Discord { get; set; }
    
      [JsonProperty("Telegram")]
      public string Telegram { get; set; }
    
      [JsonProperty("X")]
      public string X { get; set; }
    
      [JsonProperty("aboutMe")]
      public string aboutMe { get; set; }
    
      [JsonProperty("address")]
      public string address { get; set; }
    
      [JsonProperty("avatarUrl")]
      public string avatarUrl { get; set; }
    
      [JsonProperty("userName")]
      public string userName { get; set; }
      #endregion
    }
    #endregion
    
    #region UserBalance
    public class UserBalance {
      #region members
      [JsonProperty("address")]
      public string address { get; set; }
    
      [JsonProperty("bigIntAmount")]
      public string bigIntAmount { get; set; }
      #endregion
    }
    #endregion
    
    #region UserDataInput
    public class UserDataInput {
      #region members
      public string Discord { get; set; }
    
      public string Telegram { get; set; }
    
      public string X { get; set; }
    
      public string aboutMe { get; set; }
    
      public string avatarUrl { get; set; }
    
      [Required]
      [JsonRequired]
      public string userName { get; set; }
      #endregion
    
      #region methods
      public dynamic GetInputObject()
      {
        IDictionary<string, object> d = new System.Dynamic.ExpandoObject();
    
        var properties = GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        foreach (var propertyInfo in properties)
        {
          var value = propertyInfo.GetValue(this);
          var defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;
    
          var requiredProp = propertyInfo.GetCustomAttributes(typeof(JsonRequiredAttribute), false).Length > 0;
    
          if (requiredProp || value != defaultValue)
          {
            d[propertyInfo.Name] = value;
          }
        }
        return d;
      }
      #endregion
    }
    #endregion
    
    #region UserWithFollowersAndFollowings
    public class UserWithFollowersAndFollowings : IUser {
      #region members
      [JsonProperty("Discord")]
      public string Discord { get; set; }
    
      [JsonProperty("Telegram")]
      public string Telegram { get; set; }
    
      [JsonProperty("X")]
      public string X { get; set; }
    
      [JsonProperty("aboutMe")]
      public string aboutMe { get; set; }
    
      [JsonProperty("address")]
      public string address { get; set; }
    
      [JsonProperty("avatarUrl")]
      public string avatarUrl { get; set; }
    
      [JsonProperty("followers")]
      public List<User> followers { get; set; }
    
      [JsonProperty("followings")]
      public List<User> followings { get; set; }
    
      [JsonProperty("userName")]
      public string userName { get; set; }
      #endregion
    }
    #endregion
  }
  
}
