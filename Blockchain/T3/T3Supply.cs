using System;
using System.ComponentModel;
using System.Numerics;

using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services;

using Neo.SmartContract.Framework.Native;


namespace T3
{
    public partial class T3Contract : SmartContract
    {
        private static readonly string TotalNFTOnMarketKey = "TotalNFTOnMarketKey";
        private static readonly string TotalNFTSupplyKey = "TotalNFTSupplyKey";
        private static string NFT_SUPPLY_MAP = "T3NFTSupply";

        private static StorageMap T3SupplyMap() => new StorageMap(Storage.CurrentContext, NFT_SUPPLY_MAP);

        public static BigInteger BalanceOf(UInt160 owner)
        {
            if (owner is null || !owner.IsValid)
            {
                throw new Exception("The argument \"owner\" is invalid.");
            }
            return (BigInteger)T3SupplyMap().Get(owner);
        }

        protected static void UpdateTotalNFTSupply(BigInteger increment)
        {
            var totalSupply = (BigInteger)T3SupplyMap().Get(TotalNFTSupplyKey);
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                T3SupplyMap().Put(TotalNFTSupplyKey, totalSupply += increment);
            }
        }

        protected static void UpdateTotalTokensOnMarket(BigInteger increment)
        {
            var totalTokensOnMarket = (BigInteger)T3SupplyMap().Get(TotalNFTOnMarketKey);
            totalTokensOnMarket += increment;

            if(totalTokensOnMarket >= 0)
            {
                T3SupplyMap().Put(TotalNFTOnMarketKey, totalTokensOnMarket);
            }
        }

        protected static bool UpdateOwnersBalance(UInt160 owner, BigInteger increment)
        {
            var balance = (BigInteger)T3SupplyMap().Get(owner);

            balance += increment;
            
            if (balance < 0) 
            {
                return false;
            }

            if (balance.IsZero)
            {
                T3SupplyMap().Delete(owner);
            }
            else
            {
                T3SupplyMap().Put(owner, balance);    
            }
            return true;
        }
    }
}
