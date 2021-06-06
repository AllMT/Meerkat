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
        public static UInt160 TestMethod()
        {
            return Runtime.ExecutingScriptHash;
        }

        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
        {
            var tokenId = (ByteString)data;

            var mToken = MarketStorageMap().Get(tokenId);
            if(mToken == null)
            {
                throw new Exception("Token is not for sale");
            }

            var token = ValueOf(tokenId);
            if(amount < token.Value.MarketData.Price)
            {
                throw new Exception("Not enough payment");
            }
            
            if (Runtime.CallingScriptHash == NEO.Hash && token.Value.MarketData.PurchaseType == PurchaseType.NEO)
            {
                NEO.Transfer(Runtime.ExecutingScriptHash, token.Owner, amount, null);
            }
            else if (Runtime.CallingScriptHash == GAS.Hash && token.Value.MarketData.PurchaseType == PurchaseType.GAS)
            {
                GAS.Transfer(Runtime.ExecutingScriptHash, token.Owner, amount, null);
            }
            else
            {
                throw new Exception("Wrong calling script hash");
            }


            var oldOwner = token.Owner;
            token.Owner = from;
            AddTokenToStorage(tokenId, StdLib.Serialize(token));
            DeleteTokenFromMarket(tokenId);
            UpdateBalance(oldOwner, tokenId, -1);
            UpdateBalance(token.Owner, tokenId, +1);
            PostTransfer(oldOwner, token.Owner, tokenId, null);
            RemoveAllWhitelist(tokenId);
            SetWhitelist(from, tokenId);
        }
    }
}