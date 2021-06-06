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
            var owner = Storage.Get(Storage.CurrentContext, nameof(T3Contract));

            if (tx.Sender == owner)
            {
                ContractManagement.Destroy();
                return true;
            }
            return false;
        }

        public bool Update(ByteString nefFile, string manifest)
        {
            var tx = (Transaction)Runtime.ScriptContainer;
            var owner = Storage.Get(Storage.CurrentContext, nameof(T3Contract));

            if (tx.Sender == owner)
            {
                ContractManagement.Update(nefFile, manifest, null);
                return true;
            }
            return false;
        }
    }
}
