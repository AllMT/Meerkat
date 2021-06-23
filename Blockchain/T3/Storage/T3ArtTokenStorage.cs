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
        protected static void AddArt(ByteString tokenId, ByteString value, BigInteger increment)
        {
            if(increment > 0)
            {
                UpdateTotalArtSupply(increment);
                
                var index = T3ArtIndexSupply() + increment;
                ArtIndexMap().Put((ByteString)index, tokenId);
                T3SupplyMap().Put(ART_INDEX_KEY, index);
            }
            
            ArtMap().Put(tokenId, value);
        }

        protected static void DeleteArt(ByteString tokenId) 
        {
            ArtMap().Delete(tokenId);
            UpdateTotalArtSupply(-1);
        }
        
        protected static TokenState GetArt(ByteString tokenId) => (TokenState)ArtMap().GetObject(tokenId);
        protected static TokenState GetArtByIndex(BigInteger index)
        {
            var tokenId = ArtIndexMap().Get((ByteString)index);
            return (TokenState)ArtMap().GetObject(tokenId);
        }

        private static void UpdateTotalArtSupply(BigInteger increment)
        {
            var totalSupply = T3ArtSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                T3SupplyMap().Put(ART_SUPPLY_KEY, totalSupply);
            }
        }

        public static Iterator ArtTokens() => ArtMap().Find(FindOptions.KeysOnly | FindOptions.RemovePrefix);
    }
}
