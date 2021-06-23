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
        protected static void AddTradingCard(ByteString tokenId, ByteString value, BigInteger increment)
        {
            if(increment > 0)
            {
                UpdateTotalTradingCardSupply(increment);
                
                var index = T3TradingCardIndexSupply() + increment;
                TradingCardIndexMap().Put((ByteString)index, tokenId);
                T3SupplyMap().Put(TRADING_CARD_INDEX_KEY, index);
            }
            TradingCardMap().Put(tokenId, value);
        } 
        protected static void DeleteTradingCard(ByteString tokenId) 
        {
            TradingCardMap().Delete(tokenId);
            UpdateTotalTradingCardSupply(-1);
        }

        protected static TokenState GetTradingCard(ByteString tokenId) => (TokenState)TradingCardMap().GetObject(tokenId);
        protected static TokenState GetTradingCardByIndex(BigInteger index)
        {
            var tokenId = TradingCardIndexMap().Get((ByteString)index);
            return (TokenState)TradingCardMap().GetObject(tokenId);
        }
        
        protected static void UpdateTotalTradingCardSupply(BigInteger increment)
        {
            var totalSupply = T3TradingCardSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                T3SupplyMap().Put(TRADING_CARD_SUPPLY_KEY, totalSupply);
            }
        }
    }
}
