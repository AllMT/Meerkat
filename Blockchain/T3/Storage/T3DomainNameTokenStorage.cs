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
        private static StorageMap DomainNameTokenStorageMap() => new StorageMap(Storage.CurrentContext, "T3DN");
        private static string DOMAIN_NAME_SUPPLY_KEY = "T3DNKEY";
        private static StorageMap DomainNameSupplyMap() => new StorageMap(Storage.CurrentContext, "T3DNS");

        protected static void AddDomainName(ByteString tokenId, ByteString value) => DomainNameTokenStorageMap().Put(tokenId, value);
        protected static ByteString GetDomainName(ByteString tokenId) => DomainNameTokenStorageMap().Get(tokenId);
        protected static void DeleteDomainName(ByteString tokenId) => DomainNameTokenStorageMap().Delete(tokenId);

        protected static void UpdateTotalDomainNameSupply(BigInteger increment)
        {
            var totalSupply = TotalT3DomainNameSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                DomainNameSupplyMap().Put(DOMAIN_NAME_SUPPLY_KEY, totalSupply);
            }
        }

        public static BigInteger TotalT3DomainNameSupply() => (BigInteger)DomainNameSupplyMap().Get(DOMAIN_NAME_SUPPLY_KEY);
    }
}
