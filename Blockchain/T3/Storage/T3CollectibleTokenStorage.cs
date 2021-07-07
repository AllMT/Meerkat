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
        protected static void AddCollectible(ByteString tokenId, ByteString value, BigInteger increment) 
        {
            if(increment > 0)
            {
                UpdateTotalCollectibleSupply(increment);
                
                var index = T3CollectableIndexSupply() + increment;
                CollectibleIndexMap().Put((ByteString)index, tokenId);
                T3SupplyMap().Put(COLLECTIBLE_INDEX_KEY, index);
            }

            CollectibleMap().Put(tokenId, value);
        }

        protected static void DeleteCollectible(ByteString tokenId) 
        {
            CollectibleMap().Delete(tokenId);
            UpdateTotalCollectibleSupply(-1);
        }

        protected static TokenState GetCollectible(ByteString tokenId) => (TokenState)CollectibleMap().GetObject(tokenId);
        protected static TokenState GetCollectibleByIndex(BigInteger index)
        {
            var tokenId = CollectibleIndexMap().Get((ByteString)index);
            return (TokenState)CollectibleMap().GetObject(tokenId);
        }

        protected static void UpdateTotalCollectibleSupply(BigInteger increment)
        {
            var totalSupply = T3CollectableSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                T3SupplyMap().Put(COLLECTIBLE_SUPPLY_KEY, totalSupply);
            }
        }

        public static List<TokenState> GetLatestCollectiblesTokensByIndex()
        {
            var total = T3CollectableIndexSupply();
            BigInteger highest = total;
            BigInteger lowest = (total - 25) > 0 ? total - 25 : 0;

            var tokens = new List<TokenState>();
            while(lowest != highest)
            {
                var token = GetCollectibleByIndex(highest);

                if (token == null)
                {
                    if(lowest > 0)
                    {
                        lowest -= 1;
                    }
                }
                else
                {
                    tokens.Add(token);
                }
                highest -= 1;
            }

            return tokens;
        }
    }
}
