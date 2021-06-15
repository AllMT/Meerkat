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

        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object[] data)
        {
            // if((string)data[0] == "MintToken")
            // {

            // }
            // else if((string)data[0] == "ListToken")
            // {
            //     if (Runtime.CallingScriptHash == GAS.Hash)
            //     {
            //         var currentFee = GetFee();

            //         if(amount < currentFee)
            //         {
            //             throw new Exception("Fee is not enough");
            //         }

            //         GAS.Transfer(Runtime.ExecutingScriptHash, GetContractOwner(), amount, null);
            //     }
            //     else
            //     {
            //         throw new Exception("Wrong calling script hash");
            //     }

            //     ListToken((ByteString)data[1], (string)data[2]);
            // }
            // else 
            if((string)data[0] == "PurchaseToken")
            {
                var tokenId = (ByteString)data[1];

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

                if (Runtime.CallingScriptHash == GAS.Hash && token.Value.MarketData.PurchaseType == PurchaseType.GAS)
                {
                    GAS.Transfer(Runtime.ExecutingScriptHash, token.Owner, amount, null);
                }
                else
                {
                    throw new Exception("Wrong calling script hash");
                }

                var oldOwner = token.Owner;
                token.Owner = from;
                AddTokenToStorage(tokenId, token, 0);
                DeleteTokenFromMarket(tokenId);
                UpdateBalance(oldOwner, tokenId, -1);
                UpdateBalance(token.Owner, tokenId, +1);
                PostTransfer(oldOwner, token.Owner, tokenId, null);
                RemoveAllWhitelist(tokenId);
                SetWhitelist(from, tokenId);
            }
            else
            {
                throw new Exception("Not a valid operation");
            }
        }
    }
}