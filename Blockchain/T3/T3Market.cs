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
        public delegate void OnMarketDelegate(ByteString tokenId);
        [DisplayName("Market")]
        public static event OnMarketDelegate OnMarket;

        private static readonly string MARKET_MAP = "T3MARKET";
        protected static StorageMap MarketStorageMap() => new StorageMap(Storage.CurrentContext, MARKET_MAP);

        protected static void AddTokenToMarket(ByteString tokenId) => MarketStorageMap().Put(tokenId, 0);
        protected static void DeleteTokenFromMarket(ByteString tokenId) => MarketStorageMap().Delete(tokenId);

        public static Iterator MarketTokens() => MarketStorageMap().Find(FindOptions.RemovePrefix);

        public static void UpdateListing(ByteString TokenId, string options)
        {
            VerifyTokenBelongsToSender(TokenId);
            VerifyTokenIsOnMarket(TokenId);
            UpdateTokenMarketData(TokenId, options);
        }

        public static void ListToken(ByteString TokenId, string options)
        {
            VerifyTokenBelongsToSender(TokenId);
            UpdateTokenMarketData(TokenId, options);
            
            AddTokenToMarket(TokenId);
            OnMarket(TokenId);

            UpdateTotalTokensOnMarket(+1);
        }

        private static void UpdateTokenMarketData(ByteString TokenId, string options)
        {
            var marketData = GetMarketData(options);
            
            var token = ValueOf(TokenId);
            token.Value.MarketData = marketData;
            AddTokenToStorage(TokenId, token, 0);
        }

        private static void VerifyTokenBelongsToSender(ByteString tokenId)
        {            
            if(IsTokenOwnerTheSender(tokenId))
            {
                throw new Exception("The token does not belong to you");
            }
        }

        private static void VerifyTokenIsOnMarket(ByteString tokenId)
        {
            var existingToken = MarketStorageMap().Get(tokenId);
            if(existingToken == null)
            {
                throw new Exception("Can not update a token which is not on the market");
            }
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

            if(options["listingType"] != ListingType.SALE  && options["listingType"] != ListingType.AUCTION)
            {
                throw new Exception("Not a valid listing type");
            }

            if(options["purchaseType"] != PurchaseType.GAS)
            {
                throw new Exception("Not a valid purchase type");
            }
        }

        private static MarketData GetMarketDataObject(Map<string,string> options)
        {
            return new MarketData()
            {
                Price = BigInteger.Parse(options["price"]),
                ListingType = options["listingType"],
                PurchaseType = options["purchaseType"]
            };
        }
    }
}
