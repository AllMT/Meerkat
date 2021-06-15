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
        private static Transaction Tx => (Transaction)Runtime.ScriptContainer;

        protected static bool IsTokenOwnerTheSender(ByteString tokenId)
        {
            return Tx.Sender == OwnerOf(tokenId);
        }

        protected static UInt160 GetSenderAddress() => Tx.Sender;
    }
}
