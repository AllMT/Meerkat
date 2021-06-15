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
        private const string DOMAIN_NAME_TOKEN_MAP = "T3DNAME";
        private static StorageMap DomainNameTokenStorageMap() => new StorageMap(Storage.CurrentContext, DOMAIN_NAME_TOKEN_MAP);
        private static string DOMAIN_NAME_SUPPLY_KEY = "T3DNAMEK";
        private static string DOMAIN_NAME_SUPPLY_PREFIX = "T3DNAMES";
        private static StorageMap DomainNameSupplyMap() => new StorageMap(Storage.CurrentContext, DOMAIN_NAME_SUPPLY_PREFIX);

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
