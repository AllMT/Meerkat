using System;
using System.ComponentModel;
using System.Numerics;
using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services;

using Neo.SmartContract.Framework.Native;
using T3.Models;

namespace T3
{
    public partial class T3Contract : SmartContract
    {
        public delegate void OnTransferDelegate(UInt160 from, UInt160 to, BigInteger amount, ByteString tokenId);
        [DisplayName("Transfer")]
        public static event OnTransferDelegate OnTransfer;

        public delegate void OnMintDelegate(UInt160 from, ByteString tokenId);
        [DisplayName("Mint")]
        public static event OnMintDelegate OnMint;


        protected const string ACCOUNT_TOKEN_MAP = "T3ACC";
        protected static StorageMap AccountStorageMap() => new StorageMap(Storage.CurrentContext, ACCOUNT_TOKEN_MAP);

        public static Iterator TokensOf(UInt160 owner)
        {
            if (owner is null || !owner.IsValid)
            {
                throw new Exception("The argument \"owner\" is invalid");
            }
            return AccountStorageMap().Find(owner, FindOptions.KeysOnly | FindOptions.RemovePrefix);
        }

        private static void UpdateBalance(UInt160 owner, ByteString tokenId, int increment)
        {
            UpdateOwnersBalance(owner, increment);
            ByteString key = owner + tokenId;
            if (increment > 0)
            {
                AccountStorageMap().Put(key, 0);
            }
            else
            {
                AccountStorageMap().Delete(key);
            }
        }

        protected static void Mint(ByteString tokenId, TokenState token)
        {
            AddTokenToStorage(tokenId, token, +1);
            OnMint(token.Owner, tokenId);

            UpdateBalance(token.Owner, tokenId, +1);
            PostTransfer(null, token.Owner, tokenId, null);
            SetWhitelist(token.Owner, tokenId);
        }

        private static void PostTransfer(UInt160 from, UInt160 to, ByteString tokenId, object data)
        {
            OnTransfer(from, to, 1, tokenId);
            if (to is not null && ContractManagement.GetContract(to) is not null)
            {
                Contract.Call(to, "onNEP11Payment", CallFlags.All, from, 1, tokenId, data);
            }
        }

        public static bool Transfer(UInt160 to, ByteString tokenId, object data)
        {
            if (to is null || !to.IsValid)
            {
                throw new Exception("The argument \"to\" is invalid.");
            }
            var token = ValueOf(tokenId);
            var from = token.Owner;
            if (!Runtime.CheckWitness(from)) 
            {
                return false;
            }
            if (from != to)
            {
                token.Owner = to;
                AddTokenToStorage(tokenId, token, 0);
                DeleteTokenFromMarket(tokenId);
                UpdateBalance(from, tokenId, -1);
                UpdateBalance(to, tokenId, +1);
                RemoveAllWhitelist(tokenId);
                SetWhitelist(to, tokenId);
            }
            PostTransfer(from, to, tokenId, data);
            return true;
        }

        public static void Burn(ByteString tokenId)
        {
            var token = ValueOf(tokenId);
            DeleteTokenFromStorage(tokenId);
            DeleteTokenFromMarket(tokenId);
            UpdateBalance(token.Owner, tokenId, -1);
            
            PostTransfer(token.Owner, null, tokenId, null);
        }

        public static ByteString NewTokenId()
        {
            StorageContext context = Storage.CurrentContext;
            byte[] key = new byte[] { 0x02 };
            ByteString id = Storage.Get(context, key);
            Storage.Put(context, key, (BigInteger)id + 1);
            ByteString data = Runtime.ExecutingScriptHash;
            if (id is not null) data += id;
            return CryptoLib.Sha256(data);
        }
    }
}
