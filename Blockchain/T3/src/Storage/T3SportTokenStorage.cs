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
        protected static void AddSport(ByteString tokenId, ByteString value, BigInteger increment) 
        {
            if(increment > 0)
            {
                UpdateTotalSportSupply(increment);
                
                var index = T3SportIndexSupply() + increment;
                SportIndexMap().Put((ByteString)index, tokenId);
                T3SupplyMap().Put(SPORT_INDEX_KEY, index);
            }

            SportMap().Put(tokenId, value);
        }
        protected static void DeleteSport(ByteString tokenId) 
        {
            SportMap().Delete(tokenId);
            UpdateTotalSportSupply(-1);
        }

        protected static TokenState GetSport(ByteString tokenId) => (TokenState)SportMap().GetObject(tokenId);
        protected static TokenState GetSportByIndex(BigInteger index)
        {
            var tokenId = SportIndexMap().Get((ByteString)index);
            return (TokenState)SportMap().GetObject(tokenId);
        }

        protected static void UpdateTotalSportSupply(BigInteger increment)
        {
            var totalSupply = T3SportSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                T3SupplyMap().Put(SPORT_SUPPLY_KEY, totalSupply);
            }
        }

        public static List<TokenState> GetLatestSportsTokensByIndex()
        {
            var total = T3SportIndexSupply();
            BigInteger highest = total;
            BigInteger lowest = (total - 25) > 0 ? total - 25 : 0;

            var tokens = new List<TokenState>();
            while(lowest != highest)
            {
                var token = GetSportByIndex(highest);

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
