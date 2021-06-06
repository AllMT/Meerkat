using System;
using System.ComponentModel;
using System.Numerics;
using T3.Models;
using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services;
using Neo.SmartContract.Framework.Native;

namespace T3
{
    
    public partial class T3Contract : SmartContract
    {
        private static readonly string MARKET_MAP = "T3MARKETMAP";
        protected static StorageMap MarketStorageMap() => new StorageMap(Storage.CurrentContext, MARKET_MAP);

        protected static void AddTokenToMarket(ByteString tokenId) => MarketStorageMap().Put(tokenId, 0);
        protected static void DeleteTokenFromMarket(ByteString tokenId) => MarketStorageMap().Delete(tokenId);

        public static Iterator MarketTokens()
        {
            return MarketStorageMap().Find(FindOptions.KeysOnly | FindOptions.RemovePrefix);
        }

        public static void Market(ByteString TokenId, string options)
        {
            var tx = (Transaction)Runtime.ScriptContainer;

            if(IsTokenOwnerTheSender(TokenId))
            {
                throw new Exception("The token does not belong to you");
            }

            var marketData = GetMarketData(options);
            
            var token = ValueOf(TokenId);
            token.Value.MarketData = marketData;

            AddTokenToStorage(TokenId, StdLib.Serialize(token));
            AddTokenToMarket(TokenId);

            UpdateTotalTokensOnMarket(+1);
        }

        private static MarketData GetMarketData(string options)
        {
            var map = (Map<string,string>)StdLib.JsonDeserialize(options);
            ValidateOptionsParameters(map);
            return GetMarketDataObject(map);
        }

        private static void ValidateOptionsParameters(Map<string,string> options)
        {
            var price = BigInteger.Parse(options["price"]);

            if(price <= 0)
            {
                throw new Exception("Not a valid price");
            }

            if(options["marketType"] != MarketType.SALE  && options["marketType"] != MarketType.AUCTION)
            {
                throw new Exception("Not a valid market type");
            }

            if(options["purchaseType"] != PurchaseType.NEO && options["purchaseType"] != PurchaseType.GAS)
            {
                throw new Exception("Not a valid purchase type");
            }
        }

        private static MarketData GetMarketDataObject(Map<string,string> options)
        {
            return new MarketData()
            {
                Price = BigInteger.Parse(options["price"]),
                MarketType = options["marketType"],
                PurchaseType = options["purchaseType"]
            };
        }
    }
}
