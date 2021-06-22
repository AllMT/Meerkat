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
        private static StorageMap ArtTokenStorageMap() => new StorageMap(Storage.CurrentContext, "T3AT");
        private static StorageMap ArtIndexTokenStorageMap() => new StorageMap(Storage.CurrentContext, "T3ATIND");

        private static string ART_SUPPLY_KEY = "T3ASKEY";
        private static string ART_INDEX_KEY = "T3ASINDKEY";
        private static StorageMap ArtSupplyMap() => new StorageMap(Storage.CurrentContext, "T3AS");

        protected static void AddArt(ByteString tokenId, ByteString value, BigInteger increment)
        {
            if(increment > 0)
            {
                UpdateTotalArtSupply(increment);
                
                var index = T3ArtIndexSupply() + increment;
                ArtIndexTokenStorageMap().Put((ByteString)index, tokenId);
                ArtSupplyMap().Put(ART_INDEX_KEY, index);
            }
            
            ArtTokenStorageMap().Put(tokenId, value);
        }

        protected static void DeleteArt(ByteString tokenId) 
        {
            ArtTokenStorageMap().Delete(tokenId);
            UpdateTotalArtSupply(-1);
        }
        
        protected static ByteString GetArt(ByteString tokenId) => ArtTokenStorageMap().Get(tokenId);
        protected static TokenState GetArtByIndex(BigInteger index)
        {
            var tokenId = ArtIndexTokenStorageMap().Get((ByteString)index);
            return (TokenState)ArtTokenStorageMap().GetObject(tokenId);
        }

        private static void UpdateTotalArtSupply(BigInteger increment)
        {
            var totalSupply = T3ArtSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                ArtSupplyMap().Put(ART_SUPPLY_KEY, totalSupply);
            }
        }

        public static Iterator ArtTokens() => ArtTokenStorageMap().Find(FindOptions.KeysOnly | FindOptions.RemovePrefix);

        public static BigInteger T3ArtSupply() => (BigInteger)ArtSupplyMap().Get(ART_SUPPLY_KEY);
        private static BigInteger T3ArtIndexSupply() => (BigInteger)ArtSupplyMap().Get(ART_INDEX_KEY);
    }
}
