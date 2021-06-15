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
        private static readonly string TotalNFTOnMarketKey = "T3TOTALMARKETNFTKEY";
        private static readonly string TotalNFTSupplyKey = "T3TOTALNFTSUPPLYKEY";

        private static StorageMap T3SupplyMap() => new StorageMap(Storage.CurrentContext, "T3NFTS");

        public static BigInteger TotalT3TokenSupply() => (BigInteger)T3SupplyMap().Get(TotalNFTSupplyKey);
        public static BigInteger TotalT3MarketTokenSupply() => (BigInteger)T3SupplyMap().Get(TotalNFTOnMarketKey);

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
            var totalSupply = TotalT3TokenSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                T3SupplyMap().Put(TotalNFTSupplyKey, totalSupply);
            }
        }

        protected static void UpdateTotalTokensOnMarket(BigInteger increment)
        {
            var totalTokensOnMarket = TotalT3MarketTokenSupply();
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
