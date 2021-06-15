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
        protected const string T3_FEE_KEY = "T3FEE";
        protected const string T3_MAP = "T3MAP";
        protected static StorageMap T3Map() => new StorageMap(Storage.CurrentContext, T3_MAP);

        public static string Symbol() => "T3";

        public static byte Decimals() => 0;

        [DisplayName("_deploy")]
        public void Deploy(object data, bool update)
        {
            if (update) return;

            var tx = (Transaction)Runtime.ScriptContainer;
            Storage.Put(Storage.CurrentContext, nameof(T3Contract), tx.Sender);
        }

        public bool Destroy() 
        {
            var tx = (Transaction)Runtime.ScriptContainer;

            if (tx.Sender == GetContractOwner())
            {
                ContractManagement.Destroy();
                return true;
            }
            return false;
        }

        public bool Update(ByteString nefFile, string manifest)
        {
            var tx = (Transaction)Runtime.ScriptContainer;

            if (tx.Sender == GetContractOwner())
            {
                ContractManagement.Update(nefFile, manifest, null);
                return true;
            }
            return false;
        }

        public static bool SetFee(BigInteger amount)
        {
            if (GetSenderAddress() == GetContractOwner())
            {
                T3Map().Put(T3_FEE_KEY, amount);
                return true;
            }
            return false;
        }

        public static BigInteger GetFee() => (BigInteger)T3Map().Get(T3_FEE_KEY);

        protected static UInt160 GetContractOwner() => (UInt160)Storage.Get(Storage.CurrentContext, nameof(T3Contract));
    }
}
