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
        private static StorageMap CollectibleTokenStorageMap() => new StorageMap(Storage.CurrentContext, "T3CT");
        private static StorageMap CollectibleIndexTokenStorageMap() => new StorageMap(Storage.CurrentContext, "T3CTIND");

        private static string COLLECTIBLE_SUPPLY_KEY = "T3CSKEY";
        private static string COLLECTIBLE_INDEX_KEY = "T3CSINDKEY";
        private static StorageMap CollectibleSupplyMap() => new StorageMap(Storage.CurrentContext, "T3CS");

        protected static void AddCollectible(ByteString tokenId, ByteString value, BigInteger increment) 
        {
            if(increment > 0)
            {
                UpdateTotalCollectibleSupply(increment);
                
                var index = T3CollectableIndexSupply() + increment;
                CollectibleIndexTokenStorageMap().Put((ByteString)index, tokenId);
                CollectibleSupplyMap().Put(ART_INDEX_KEY, index);
            }

            CollectibleTokenStorageMap().Put(tokenId, value);
        }

        protected static void DeleteCollectible(ByteString tokenId) => CollectibleTokenStorageMap().Delete(tokenId);

        protected static ByteString GetCollectible(ByteString tokenId) => CollectibleTokenStorageMap().Get(tokenId);
        protected static TokenState GetCollectibleByIndex(BigInteger index)
        {
            var tokenId = CollectibleIndexTokenStorageMap().Get((ByteString)index);
            return (TokenState)CollectibleSupplyMap().GetObject(tokenId);
        }

        protected static void UpdateTotalCollectibleSupply(BigInteger increment)
        {
            var totalSupply = T3CollectableSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                CollectibleSupplyMap().Put(COLLECTIBLE_SUPPLY_KEY, totalSupply);
            }
        }

        public static BigInteger T3CollectableSupply() => (BigInteger)CollectibleSupplyMap().Get(COLLECTIBLE_SUPPLY_KEY);
        private static BigInteger T3CollectableIndexSupply() => (BigInteger)CollectibleSupplyMap().Get(COLLECTIBLE_INDEX_KEY);
    }
}
