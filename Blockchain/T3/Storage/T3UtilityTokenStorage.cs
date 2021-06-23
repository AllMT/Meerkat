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
        protected static void AddUtility(ByteString tokenId, ByteString value, BigInteger increment) 
        {
            if(increment > 0)
            {
                UpdateTotalUtilitySupply(increment);
                
                var index = T3UtilityIndexSupply() + increment;
                UtilityIndexMap().Put((ByteString)index, tokenId);
                T3SupplyMap().Put(UTILITY_INDEX_KEY, index);
            }
            UtilityMap().Put(tokenId, value);
        } 
        
        protected static void DeleteUtility(ByteString tokenId) 
        {
            UtilityMap().Delete(tokenId);
            UpdateTotalUtilitySupply(-1);
        } 

        protected static TokenState GetUtility(ByteString tokenId) => (TokenState)UtilityMap().GetObject(tokenId);
        protected static TokenState GetUtilityByIndex(BigInteger index)
        {
            var tokenId = UtilityIndexMap().Get((ByteString)index);
            return (TokenState)UtilityMap().GetObject(tokenId);
        }

        protected static void UpdateTotalUtilitySupply(BigInteger increment)
        {
            var totalSupply = T3UtilitySupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                T3SupplyMap().Put(UTILITY_SUPPLY_KEY, totalSupply);
            }
        }
    }
}
