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
        private const string COLLECTIBLE_TOKEN_MAP = "T3COLLECTIBLE";
        private static StorageMap CollectibleTokenStorageMap() => new StorageMap(Storage.CurrentContext, COLLECTIBLE_TOKEN_MAP);
        private static string COLLECTIBLE_SUPPLY_KEY = "T3COLLECTIBLEK";
        private static string COLLECTIBLE_SUPPLY_PREFIX = "T3COLLECTIBLES";
        private static StorageMap CollectibleSupplyMap() => new StorageMap(Storage.CurrentContext, COLLECTIBLE_SUPPLY_PREFIX);

        protected static void AddCollectible(ByteString tokenId, ByteString value) => CollectibleTokenStorageMap().Put(tokenId, value);
        protected static ByteString GetCollectible(ByteString tokenId) => CollectibleTokenStorageMap().Get(tokenId);
        protected static void DeleteCollectible(ByteString tokenId) => CollectibleTokenStorageMap().Delete(tokenId);

        protected static void UpdateTotalCollectibleSupply(BigInteger increment)
        {
            var totalSupply = TotalT3CollectableSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                CollectibleSupplyMap().Put(COLLECTIBLE_SUPPLY_KEY, totalSupply);
            }
        }

        public static BigInteger TotalT3CollectableSupply() => (BigInteger)CollectibleSupplyMap().Get(COLLECTIBLE_SUPPLY_KEY);
    }
}
