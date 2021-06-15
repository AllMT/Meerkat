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
        private static string LOCKED_CONTENT_MAP = "T3LOCKED";

        private static StorageMap LockedContentMap() => new StorageMap(Storage.CurrentContext, LOCKED_CONTENT_MAP);

        public static void SetWhitelistAddress(UInt160 address, ByteString tokenId)
        {
            if(IsTokenOwnerTheSender(tokenId))
            {
                LockedContentMap().Put(tokenId + address, 0);
            }
        }

        public static void RemoveWhitelistAddress(UInt160 address, ByteString tokenId)
        {
            if(IsTokenOwnerTheSender(tokenId))
            {
                LockedContentMap().Delete(tokenId + address);
            }
        }

        public static bool IsWhitelisted(UInt160 address, ByteString tokenId)
        {
            return LockedContentMap().Get(tokenId + address) != null;
        }

        public static bool RemoveAllFromWhiteList(ByteString tokenId)
        {
            if(!IsTokenOwnerTheSender(tokenId))
            {
                return false;
            }
            
            var tokens = LockedContentMap().Find(FindOptions.KeysOnly | FindOptions.RemovePrefix);

            while(tokens.Next())
            {
                LockedContentMap().Delete((ByteString)tokens.Value);
            }
            
            return true;
        }

        protected static void RemoveAllWhitelist(ByteString tokenId)
        {
            var tokens = LockedContentMap().Find(FindOptions.KeysOnly | FindOptions.RemovePrefix);

            while(tokens.Next())
            {
                LockedContentMap().Delete((ByteString)tokens.Value);
            }
        }
        protected static void SetWhitelist(UInt160 address, ByteString tokenId)
        {
            LockedContentMap().Put(tokenId + address, 0);
        }
    }
}       