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
        private static readonly string TotalNFTOnMarketKey = "T3TOTALMARKETNFTKEY";
        private static readonly string TotalNFTSupplyKey = "T3TOTALNFTSUPPLYKEY";
        private static readonly string TokenIndexSupplyKey = "T3TOKENINDEXSUPPLYKEY";

        private static StorageMap T3SupplyMap() => new StorageMap(Storage.CurrentContext, "T3NFTS");
        public static BigInteger T3Supply() => (BigInteger)T3SupplyMap().Get(TotalNFTSupplyKey);
        public static BigInteger T3TokenIndexSupply() => (BigInteger)T3SupplyMap().Get(TokenIndexSupplyKey);
        public static BigInteger T3MarketSupply() => (BigInteger)T3SupplyMap().Get(TotalNFTOnMarketKey);
        
        private static string ART_SUPPLY_KEY = "T3ASKEY";
        private static string ART_INDEX_KEY = "T3ASINDKEY";
        public static BigInteger T3ArtSupply() => (BigInteger)T3SupplyMap().Get(ART_SUPPLY_KEY);
        private static BigInteger T3ArtIndexSupply() => (BigInteger)T3SupplyMap().Get(ART_INDEX_KEY);
        protected static StorageMap ArtMap() => new StorageMap(Storage.CurrentContext, "T3AT");
        protected static StorageMap ArtIndexMap() => new StorageMap(Storage.CurrentContext, "T3AIND");


        private static string COLLECTIBLE_SUPPLY_KEY = "T3CSKEY";
        private static string COLLECTIBLE_INDEX_KEY = "T3CINDKEY";
        public static BigInteger T3CollectableSupply() => (BigInteger)T3SupplyMap().Get(COLLECTIBLE_SUPPLY_KEY);
        private static BigInteger T3CollectableIndexSupply() => (BigInteger)T3SupplyMap().Get(COLLECTIBLE_INDEX_KEY);
        private static StorageMap CollectibleMap() => new StorageMap(Storage.CurrentContext, "T3CT");
        private static StorageMap CollectibleIndexMap() => new StorageMap(Storage.CurrentContext, "T3CIND");


        private static string DOMAIN_NAME_SUPPLY_KEY = "T3DSKEY";
        private static string DOMAIN_NAME_INDEX_KEY = "T3DINDKEY";
        public static BigInteger T3DomainNameSupply() => (BigInteger)T3SupplyMap().Get(DOMAIN_NAME_SUPPLY_KEY);
        private static BigInteger T3DomainNameIndexSupply() => (BigInteger)T3SupplyMap().Get(DOMAIN_NAME_INDEX_KEY);
        private static StorageMap DomainNameMap() => new StorageMap(Storage.CurrentContext, "T3DN");
        private static StorageMap DomainNameIndexMap() => new StorageMap(Storage.CurrentContext, "T3DIND");


        private static string SPORT_SUPPLY_KEY = "T3STKEY";
        private static string SPORT_INDEX_KEY = "T3SINDKEY";
        public static BigInteger T3SportSupply() => (BigInteger)T3SupplyMap().Get(SPORT_SUPPLY_KEY);
        protected static BigInteger T3SportIndexSupply() => (BigInteger)T3SupplyMap().Get(SPORT_INDEX_KEY);
        private static StorageMap SportMap() => new StorageMap(Storage.CurrentContext, "T3ST");
        private static StorageMap SportIndexMap() => new StorageMap(Storage.CurrentContext, "T3SIND");


        private static string MUSIC_SUPPLY_KEY = "T3MCKEY";
        private static string MUSIC_INDEX_KEY = "T3MCINDKEY";
        public static BigInteger T3MusicSupply() => (BigInteger)T3SupplyMap().Get(MUSIC_SUPPLY_KEY);
        protected static BigInteger T3MusicIndexSupply() => (BigInteger)T3SupplyMap().Get(MUSIC_INDEX_KEY);
        private static StorageMap MusicMap() => new StorageMap(Storage.CurrentContext, "T3MC");
        private static StorageMap MusicIndexMap() => new StorageMap(Storage.CurrentContext, "T3MIND");


        private static string TRADING_CARD_SUPPLY_KEY = "T3TSKEY";
        private static string TRADING_CARD_INDEX_KEY = "T3TSINDKEY";
        public static BigInteger T3TradingCardSupply() => (BigInteger)T3SupplyMap().Get(TRADING_CARD_SUPPLY_KEY);
        protected static BigInteger T3TradingCardIndexSupply() => (BigInteger)T3SupplyMap().Get(TRADING_CARD_INDEX_KEY);
        private static StorageMap TradingCardMap() => new StorageMap(Storage.CurrentContext, "T3TS");
        private static StorageMap TradingCardIndexMap() => new StorageMap(Storage.CurrentContext, "T3TIND");


        private static string UTILITY_SUPPLY_KEY = "T3USKEY";
        private static string UTILITY_INDEX_KEY = "T3UINDKEY";
        public static BigInteger T3UtilitySupply() => (BigInteger)T3SupplyMap().Get(UTILITY_SUPPLY_KEY);
        protected static BigInteger T3UtilityIndexSupply() => (BigInteger)T3SupplyMap().Get(UTILITY_INDEX_KEY);
        private static StorageMap UtilityMap() => new StorageMap(Storage.CurrentContext, "T3UY");
        private static StorageMap UtilityIndexMap() => new StorageMap(Storage.CurrentContext, "T3UIND");


        private static string VIRTUAL_WORLD_SUPPLY_KEY = "T3VWSKEY";
        private static string VIRTUAL_WORLD_INDEX_KEY = "T3VINDKEY";
        public static BigInteger T3VirtualWorldSupply() => (BigInteger)T3SupplyMap().Get(VIRTUAL_WORLD_SUPPLY_KEY);
        protected static BigInteger T3VirtualWorldIndexSupply() => (BigInteger)T3SupplyMap().Get(VIRTUAL_WORLD_INDEX_KEY);
        private static StorageMap VirtualWorldMap() => new StorageMap(Storage.CurrentContext, "T3VW");
        private static StorageMap VirtualWorldIndexMap() => new StorageMap(Storage.CurrentContext, "T3VIND");

    }
}
