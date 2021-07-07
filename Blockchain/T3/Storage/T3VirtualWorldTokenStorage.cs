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
        protected static void AddVirtualWorld(ByteString tokenId, ByteString value, BigInteger increment) 
        {
            if(increment > 0)
            {
                UpdateTotalSportSupply(increment);
                
                var index = T3VirtualWorldIndexSupply() + increment;
                VirtualWorldIndexMap().Put((ByteString)index, tokenId);
                T3SupplyMap().Put(VIRTUAL_WORLD_INDEX_KEY, index);
            }

            VirtualWorldMap().Put(tokenId, value);
        } 
        
        protected static void DeleteVirtualWorld(ByteString tokenId) 
        {
            VirtualWorldMap().Delete(tokenId);
            UpdateTotalVirtualWorldSupply(-1);
        } 

        protected static TokenState GetVirtualWorld(ByteString tokenId) => (TokenState)VirtualWorldMap().GetObject(tokenId);
        protected static TokenState GetVirtualWorldByIndex(BigInteger index)
        {
            var tokenId = VirtualWorldIndexMap().Get((ByteString)index);
            return (TokenState)VirtualWorldMap().GetObject(tokenId);
        }
        
        protected static void UpdateTotalVirtualWorldSupply(BigInteger increment)
        {
            var totalSupply = T3VirtualWorldSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                T3SupplyMap().Put(VIRTUAL_WORLD_SUPPLY_KEY, totalSupply);
            }
        }

        public static List<TokenState> GetLatestVirtualWorldTokensByIndex()
        {
            var total = T3VirtualWorldIndexSupply();
            BigInteger highest = total;
            BigInteger lowest = (total - 25) > 0 ? total - 25 : 0;

            var tokens = new List<TokenState>();
            while(lowest != highest)
            {
                var token = GetVirtualWorldByIndex(highest);

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
