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
        private static StorageMap TradingCardTokenStorageMap() => new StorageMap(Storage.CurrentContext, "T3TC");

        private static string TRADING_CARD_SUPPLY_KEY = "T3TCKEY";
        private static StorageMap TradingCardSupplyMap() => new StorageMap(Storage.CurrentContext, "T3TCS");

        protected static void AddTradingCard(ByteString tokenId, ByteString value) => TradingCardTokenStorageMap().Put(tokenId, value);
        protected static ByteString GetTradingCard(ByteString tokenId) => TradingCardTokenStorageMap().Get(tokenId);
        protected static void DeleteTradingCard(ByteString tokenId) => TradingCardTokenStorageMap().Delete(tokenId);

        
        protected static void UpdateTotalTradingCardSupply(BigInteger increment)
        {
            var totalSupply = TotalT3TradingCardSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                TradingCardSupplyMap().Put(TRADING_CARD_SUPPLY_KEY, totalSupply);
            }
        }

        public static BigInteger TotalT3TradingCardSupply() => (BigInteger)TradingCardSupplyMap().Get(TRADING_CARD_SUPPLY_KEY);
    }
}
