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
        protected static StorageMap VirtualWorldTokenStorageMap() => new StorageMap(Storage.CurrentContext, "T3VWM");

        protected static void AddVirtualWorld(ByteString tokenId, ByteString value) => VirtualWorldTokenStorageMap().Put(tokenId, value);
        protected static ByteString GetVirtualWorld(ByteString tokenId) => VirtualWorldTokenStorageMap().Get(tokenId);
        protected static void DeleteVirtualWorld(ByteString tokenId) => VirtualWorldTokenStorageMap().Delete(tokenId);

        private static string VIRTUAL_WORLD_SUPPLY_KEY = "T3VWSKEY";
        private static StorageMap VirtualWorldSupplyMap() => new StorageMap(Storage.CurrentContext, "T3VWS");
        
        protected static void UpdateTotalVirtualWorldSupply(BigInteger increment)
        {
            var totalSupply = TotalT3VirtualWorldSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                VirtualWorldSupplyMap().Put(VIRTUAL_WORLD_SUPPLY_KEY, totalSupply);
            }
        }

        public static BigInteger TotalT3VirtualWorldSupply() => (BigInteger)VirtualWorldSupplyMap().Get(VIRTUAL_WORLD_SUPPLY_KEY);
    }
}
