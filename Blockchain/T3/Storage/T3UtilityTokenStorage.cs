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
        private static StorageMap UtilityTokenStorageMap() => new StorageMap(Storage.CurrentContext, "T3UTY");

        protected static void AddUtility(ByteString tokenId, ByteString value) => UtilityTokenStorageMap().Put(tokenId, value);
        protected static ByteString GetUtility(ByteString tokenId) => UtilityTokenStorageMap().Get(tokenId);
        protected static void DeleteUtility(ByteString tokenId) => UtilityTokenStorageMap().Delete(tokenId);

        private static string UTILITY_SUPPLY_KEY = "T3USKEY";
        private static StorageMap UtilitySupplyMap() => new StorageMap(Storage.CurrentContext, "T3US");
        
        protected static void UpdateTotalUtilitySupply(BigInteger increment)
        {
            var totalSupply = TotalT3SupplySupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                UtilitySupplyMap().Put(UTILITY_SUPPLY_KEY, totalSupply);
            }
        }

        public static BigInteger TotalT3SupplySupply() => (BigInteger)UtilitySupplyMap().Get(UTILITY_SUPPLY_KEY);
    }
}
