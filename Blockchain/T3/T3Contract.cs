using System;
using System.ComponentModel;
using System.Numerics;
using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services;
using Neo.SmartContract.Framework.Native;
using T3.Models;

namespace T3
{
    [DisplayName("T3")]
    [ManifestExtra("Author", "AllMT")]
    [ManifestExtra("Email", "andrewxm27@gmail.com")]
    [ManifestExtra("Description", "NFT contract")]
    [ContractPermission("*", "onNEP17Payment")]
    [ContractPermission("*", "onNEP11Payment")]
    public partial class T3Contract : SmartContract
    {
        public static bool MintToken(ByteString tokenId, string properties)
        {
            Mint(tokenId, GetTokenState(properties));

            return true;
        }
        
        private static TokenState GetTokenState(string properties)
        {
            var tx = (Transaction) Runtime.ScriptContainer;
            var propertiesMap = (Map<string,string>)StdLib.JsonDeserialize(properties);

            return new TokenState()
            {
                Owner = tx.Sender,
                Value = new TokenProperties()
                {
                    TokenData = new TokenData()
                    {
                        Name = propertiesMap.HasKey("name") ?  propertiesMap["name"] : null,
                        Description = propertiesMap.HasKey("description") ?  propertiesMap["description"] : null,
                        Image = propertiesMap.HasKey("image") ?  propertiesMap["image"] : null,
                        TokenURI = propertiesMap.HasKey("tokenURI") ?  propertiesMap["tokenURI"] : null,
                        LockedContent = propertiesMap.HasKey("lockedContent") ?  propertiesMap["lockedContent"] : null,
                    }
                }
            };
        }

        public static List<string> GetTokenIdsFor(UInt160 owner)
        {
            var list = new List<string>();
            var iterator = TokensOf(owner);

            while(iterator.Next())
            {
                list.Add((string)iterator.Value);
            }

            return list;
        }

        public static List<string> GetAllTokenIds()
        {
            var list = new List<string>();
            var iterator = Tokens();

            while(iterator.Next())
            {
                list.Add((string)iterator.Value);
            }

            return list;
        }

        public static List<string> GetAllMarketTokenIds()
        {
            var list = new List<string>();
            var iterator = MarketTokens();

            while(iterator.Next())
            {
                list.Add((string)iterator.Value);
            }

            return list;
        }

        public static string GetTokenProperties(ByteString tokenId)
        {
            var tx = (Transaction)Runtime.ScriptContainer;
            
            var token = ValueOf(tokenId);

            var map = new Map<string, object>();
            map["name"] = token.Value.TokenData.Name;
            map["description"] = token.Value.TokenData.Description;
            map["image"] = token.Value.TokenData.Image;
            map["tokenURI"] = token.Value.TokenData.TokenURI;

            if(IsWhitelisted(tx.Sender, tokenId))
            {
                map["lockedContent"] = token.Value.TokenData.LockedContent;
            }

            if(token.Value.MarketData != null)
            {
                map["price"] = token.Value.MarketData.Price;
                map["marketType"] = token.Value.MarketData.MarketType;
                map["purchaseType"] = token.Value.MarketData.PurchaseType;
            }

            return StdLib.JsonSerialize(map);
        }
    }
}