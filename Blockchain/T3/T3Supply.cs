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
        private static string TOTALSUPPLY_MAP = "T3NFTSupply";
        private static string NFT_MAP = "T3NFT";

        public static string Symbol() => "SIM";

        public static byte Decimals() => 0;

        private static StorageMap TotalSupplyMap() => new StorageMap(Storage.CurrentContext, TOTALSUPPLY_MAP);

        private static StorageMap TotalNFTByOwnerStorage() => new StorageMap(Storage.CurrentContext, NFT_MAP);

        public static BigInteger BalanceOf(UInt160 owner)
        {
            if (owner is null || !owner.IsValid)
            {
                throw new Exception("The argument \"owner\" is invalid.");
            }
            return (BigInteger)TotalNFTByOwnerStorage().Get(owner);
        }

        protected static void UpdateTotalNFTSupply(BigInteger increment)
        {
            var totalSupply = (BigInteger)TotalSupplyMap().Get(TotalNFTSupplyKey);
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                TotalSupplyMap().Put(TotalNFTSupplyKey, totalSupply += increment);
            }
        }

        protected static void UpdateTotalTokensOnMarket(BigInteger increment)
        {
            var totalTokensOnMarket = (BigInteger)TotalSupplyMap().Get(TotalNFTOnMarketKey);
            totalTokensOnMarket += increment;

            if(totalTokensOnMarket >= 0)
            {
                TotalSupplyMap().Put(TotalNFTOnMarketKey, totalTokensOnMarket);
            }
        }

        protected static bool UpdateOwnersBalance(UInt160 owner, BigInteger increment)
        {
            var balance = (BigInteger)TotalNFTByOwnerStorage().Get(owner);

            balance += increment;
            
            if (balance < 0) 
            {
                return false;
            }

            if (balance.IsZero)
            {
                TotalNFTByOwnerStorage().Delete(owner);
            }
            else
            {
                TotalNFTByOwnerStorage().Put(owner, balance);    
            }
            return true;
        }
    }
}
