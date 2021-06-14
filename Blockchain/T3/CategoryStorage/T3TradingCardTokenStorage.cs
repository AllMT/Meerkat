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
        private const string TRADING_CARD_TOKEN_MAP = "T3TRADINGCARDTOKENMAP";
        private static StorageMap TradingCardTokenStorageMap() => new StorageMap(Storage.CurrentContext, TRADING_CARD_TOKEN_MAP);

        private static string TRADING_CARD_SUPPLY_KEY = "T3TRADINGCARDSupplyKey";
        private static string TRADING_CARD_SUPPLY_PREFIX = "T3TRADINGCARDSupply";
        private static StorageMap TradingCardSupplyMap() => new StorageMap(Storage.CurrentContext, TRADING_CARD_SUPPLY_PREFIX);

        protected static void AddTradingCard(ByteString tokenId, ByteString value) => TradingCardTokenStorageMap().Put(tokenId, value);
        protected static ByteString GetTradingCard(ByteString tokenId) => TradingCardTokenStorageMap().Get(tokenId);
        protected static void DeleteTradingCard(ByteString tokenId) => TradingCardTokenStorageMap().Delete(tokenId);

        
        protected static void UpdateTotalTradingCardSupply(BigInteger increment)
        {
            var totalSupply = TotalT3TradingCardSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                TradingCardSupplyMap().Put(TRADING_CARD_SUPPLY_KEY, totalSupply += increment);
            }
        }

        public static BigInteger TotalT3TradingCardSupply() => (BigInteger)TradingCardSupplyMap().Get(TRADING_CARD_SUPPLY_KEY);
    }
}
