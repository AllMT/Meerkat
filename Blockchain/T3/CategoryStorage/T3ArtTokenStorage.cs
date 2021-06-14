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
        private const string ART_TOKEN_PREFIX = "T3ARTTOKENMAP";
        private static StorageMap ArtTokenStorageMap() => new StorageMap(Storage.CurrentContext, ART_TOKEN_PREFIX);

        private static string ART_SUPPLY_KEY = "T3ARTSupplyKey";
        private static string ART_SUPPLY_PREFIX = "T3ARTSupply";
        private static StorageMap ArtSupplyMap() => new StorageMap(Storage.CurrentContext, ART_SUPPLY_PREFIX);

        protected static void AddArt(ByteString tokenId, ByteString value) => ArtTokenStorageMap().Put(tokenId, value);
        protected static ByteString GetArt(ByteString tokenId) => ArtTokenStorageMap().Get(tokenId);
        protected static void DeleteArt(ByteString tokenId) => ArtTokenStorageMap().Delete(tokenId);

        protected static void UpdateTotalArtSupply(BigInteger increment)
        {
            var totalSupply = TotalT3ArtSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                ArtSupplyMap().Put(ART_SUPPLY_KEY, totalSupply += increment);
            }
        }

        public static BigInteger TotalT3ArtSupply() => (BigInteger)ArtSupplyMap().Get(ART_SUPPLY_KEY);
    }
}
