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
    [ContractPermission("*", "onNEP11Payment")]
    public partial class T3Contract : SmartContract
    {
        public delegate void OnTransferDelegate(UInt160 from, UInt160 to, BigInteger amount, ByteString tokenId);
        [DisplayName("Transfer")]
        public static event OnTransferDelegate OnTransfer;


        protected const string ACCOUNT_TOKEN_MAP = "T3ACCOUNTMAP";
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
            AddTokenToStorage(tokenId, StdLib.Serialize(token));

            UpdateBalance(token.Owner, tokenId, +1);
            UpdateTotalNFTSupply(+1);
            PostTransfer(null, token.Owner, tokenId, null);
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
                AddTokenToStorage(tokenId, StdLib.Serialize(token));
                DeleteTokenFromMarket(tokenId);
                UpdateBalance(from, tokenId, -1);
                UpdateBalance(to, tokenId, +1);
            }
            PostTransfer(from, to, tokenId, data);
            return true;
        }

        protected static void Burn(ByteString tokenId)
        {
            var token = ValueOf(tokenId);
            DeleteTokenFromStorage(tokenId);
            DeleteTokenFromMarket(tokenId);
            UpdateBalance(token.Owner, tokenId, -1);
            UpdateTotalNFTSupply(-1);
            PostTransfer(token.Owner, null, tokenId, null);
        }
    }
}
