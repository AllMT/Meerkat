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
        protected const string TOKEN_MAP = "T3TOKENMAP";
        protected static StorageMap TokenStorageMap() => new StorageMap(Storage.CurrentContext, TOKEN_MAP);

        protected static void AddTokenToStorage(ByteString tokenId, ByteString value) => TokenStorageMap().Put(tokenId, value);
        protected static ByteString GetTokenFromStorage(ByteString tokenId) => TokenStorageMap().Get(tokenId);
        protected static void DeleteTokenFromStorage(ByteString tokenId) => TokenStorageMap().Delete(tokenId);


        public static Iterator Tokens()
        {
            return TokenStorageMap().Find(FindOptions.KeysOnly | FindOptions.RemovePrefix);
        }

        public static ByteString OwnerOf(ByteString tokenId)
        {
            var token = (TokenState)StdLib.Deserialize(GetTokenFromStorage(tokenId));

            return token.Owner;
        }

        protected static TokenState ValueOf(ByteString tokenId)
        {
            var token = (TokenState)StdLib.Deserialize(GetTokenFromStorage(tokenId));
            return token;
        }
    }
}
