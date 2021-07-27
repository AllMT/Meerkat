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
        protected static void AddDomainName(ByteString tokenId, ByteString value, BigInteger increment) 
        { 
            if(increment > 0)
            {
                UpdateTotalDomainNameSupply(increment);
                
                var index = T3DomainNameIndexSupply() + increment;
                DomainNameIndexMap().Put((ByteString)index, tokenId);
                T3SupplyMap().Put(DOMAIN_NAME_INDEX_KEY, index);
            }
            
            DomainNameMap().Put(tokenId, value);
        }

        protected static void DeleteDomainName(ByteString tokenId) 
        {
            DomainNameMap().Delete(tokenId);
            UpdateTotalDomainNameSupply(-1);
        } 


        protected static TokenState GetDomainName(ByteString tokenId) => (TokenState)DomainNameMap().GetObject(tokenId);
        protected static TokenState GetDomainNameByIndex(BigInteger index)
        {
            var tokenId = DomainNameIndexMap().Get((ByteString)index);
            return (TokenState)DomainNameMap().GetObject(tokenId);
        }

        protected static void UpdateTotalDomainNameSupply(BigInteger increment)
        {
            var totalSupply = T3DomainNameSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                T3SupplyMap().Put(DOMAIN_NAME_SUPPLY_KEY, totalSupply);
            }
        }

        public static List<TokenState> GetLatestDomainNamesTokensByIndex()
        {
            var total = T3DomainNameIndexSupply();
            BigInteger highest = total;
            BigInteger lowest = (total - 25) > 0 ? total - 25 : 0;

            var tokens = new List<TokenState>();
            while(lowest != highest)
            {
                var token = GetDomainNameByIndex(highest);

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
