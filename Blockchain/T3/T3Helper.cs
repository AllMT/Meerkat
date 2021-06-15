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
        protected static bool IsTokenOwnerTheSender(ByteString tokenId)
        {
            var tx = (Transaction)Runtime.ScriptContainer;
            return tx.Sender == OwnerOf(tokenId);
        }
        protected static UInt160 GetSenderAddress()
        {
            var tx = (Transaction) Runtime.ScriptContainer;
            return tx.Sender;
        }
    }
}
