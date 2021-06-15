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
        private const string MUSIC_TOKEN_MAP = "T3MUSIC";
        private static StorageMap MusicTokenStorageMap() => new StorageMap(Storage.CurrentContext, MUSIC_TOKEN_MAP);

        private static string MUSIC_SUPPLY_KEY = "T3MUSICK";
        private static string MUSIC_SUPPLY_PREFIX = "T3MUSICS";
        private static StorageMap MusicSupplyMap() => new StorageMap(Storage.CurrentContext, MUSIC_SUPPLY_PREFIX);

        protected static void AddMusic(ByteString tokenId, ByteString value) => MusicTokenStorageMap().Put(tokenId, value);
        protected static ByteString GetMusic(ByteString tokenId) => MusicTokenStorageMap().Get(tokenId);
        protected static void DeleteMusic(ByteString tokenId) => MusicTokenStorageMap().Delete(tokenId);

        protected static void UpdateTotalMusicSupply(BigInteger increment)
        {
            var totalSupply = TotalT3MusicSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                MusicSupplyMap().Put(MUSIC_SUPPLY_KEY, totalSupply);
            }
        }

        public static BigInteger TotalT3MusicSupply() => (BigInteger)MusicSupplyMap().Get(MUSIC_SUPPLY_KEY);
    }
}