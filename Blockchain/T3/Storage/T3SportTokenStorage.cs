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
        private const string SPORT_TOKEN_MAP = "T3SPORT";
        private static StorageMap SportTokenStorageMap() => new StorageMap(Storage.CurrentContext, SPORT_TOKEN_MAP);

        private static string SPORT_SUPPLY_KEY = "T3SPORTK";
        private static string SPORT_SUPPLY_PREFIX = "T3SPORTS";
        private static StorageMap SportSupplyMap() => new StorageMap(Storage.CurrentContext, SPORT_SUPPLY_PREFIX);


        protected static void AddSport(ByteString tokenId, ByteString value) => SportTokenStorageMap().Put(tokenId, value);
        protected static ByteString GetSport(ByteString tokenId) => SportTokenStorageMap().Get(tokenId);
        protected static void DeleteSport(ByteString tokenId) => SportTokenStorageMap().Delete(tokenId);

        protected static void UpdateTotalSportSupply(BigInteger increment)
        {
            var totalSupply = TotalT3SportSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                SportSupplyMap().Put(SPORT_SUPPLY_KEY, totalSupply);
            }
        }

        public static BigInteger TotalT3SportSupply() => (BigInteger)SportSupplyMap().Get(SPORT_SUPPLY_KEY);
    }
}
