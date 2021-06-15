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
        private static StorageMap SportTokenStorageMap() => new StorageMap(Storage.CurrentContext, "T3SPT");

        private static string SPORT_SUPPLY_KEY = "T3SPTKEY";
        private static StorageMap SportSupplyMap() => new StorageMap(Storage.CurrentContext, "T3SPTS");


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
