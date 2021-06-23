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
        protected static void AddMusic(ByteString tokenId, ByteString value, BigInteger increment)
        {
            if(increment > 0)
            {
                UpdateTotalMusicSupply(increment);
                
                var index = T3MusicIndexSupply() + increment;
                MusicIndexMap().Put((ByteString)index, tokenId);
                T3SupplyMap().Put(MUSIC_INDEX_KEY, index);
            }

            MusicMap().Put(tokenId, value);
        } 
        protected static void DeleteMusic(ByteString tokenId)
        {
            MusicMap().Delete(tokenId);
            UpdateTotalMusicSupply(-1);
        } 

        protected static TokenState GetMusic(ByteString tokenId) => (TokenState)MusicMap().GetObject(tokenId);
        protected static TokenState GetMusicByIndex(BigInteger index)
        {
            var tokenId = MusicIndexMap().Get((ByteString)index);
            return (TokenState)MusicMap().GetObject(tokenId);
        }

        protected static void UpdateTotalMusicSupply(BigInteger increment)
        {
            var totalSupply = T3MusicSupply();
            totalSupply += increment;

            if(totalSupply >= 0)
            {
                T3SupplyMap().Put(MUSIC_SUPPLY_KEY, totalSupply);
            }
        }
    }
}
