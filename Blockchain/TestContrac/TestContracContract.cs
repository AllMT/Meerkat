using System;
using System.ComponentModel;
using System.Numerics;

using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;

namespace TestContrac
{
    [DisplayName("YourName.TestContracContract")]
    [ManifestExtra("Author", "Your name")]
    [ManifestExtra("Email", "your@address.invalid")]
    [ManifestExtra("Description", "Describe your contract...")]
    public class TestContracContract : SmartContract
    {
        private static StorageMap ContractStorage => new StorageMap(Storage.CurrentContext, "TestContracContract");
        private static StorageMap ContractMetadata => new StorageMap(Storage.CurrentContext, "Metadata");

        private static readonly string TotalNFTSupplyKey = "T3TOTALNFTSUPPLYKEY";
        private static StorageMap T3SupplyMap => new StorageMap(Storage.CurrentContext, "T3NFTS");
        public static BigInteger TotalT3TokenSupply() => (BigInteger)T3SupplyMap.Get(TotalNFTSupplyKey);


        public static bool Create()
        {
            CC();
            CC();
            CC();
            CC();

            var ss = FindByIndex(2);
            var tt = TokenKeys(2);

            while(tt.Next())
            {
                var val = tt.Value;
            }
            
            return true;
        }

        private static void CC()
        {
            var key = NewTokenId();
            BigInteger index = TotalT3TokenSupply() + 1;
            ByteString newKey = (ByteString)intToByteArray(index) + key;
            var Tx = (Transaction) Runtime.ScriptContainer;
            ContractStorage.Put(newKey, Tx.Sender);
            T3SupplyMap.Put(TotalNFTSupplyKey,  index);
        }

        protected static Iterator TokenKeys(BigInteger val)
        {
            var key = intToByteArray(val);


            return ContractStorage.Find(key, FindOptions.KeysOnly | FindOptions.RemovePrefix);
        }


        public static UInt160 FindByIndex(BigInteger val)
        {
            var key = intToByteArray(val);

            return (UInt160)ContractStorage.Get(key);
        }


        protected static byte[] intToByteArray(BigInteger value) 
        {
            return new byte[] {
                (byte)((value >> 24) & 0xff),
                (byte)((value >> 16) & 0xff),
                (byte)((value >> 8) & 0xff),
                (byte)((value >> 0) & 0xff),
            };
        }

        protected static ByteString NewTokenId()
        {
            StorageContext context = Storage.CurrentContext;
            byte[] key = new byte[] { 0x02 };
            ByteString id = Storage.Get(context, key);
            Storage.Put(context, key, (BigInteger)id + 1);
            ByteString data = Runtime.ExecutingScriptHash;
            if (id is not null) data += id;
            return CryptoLib.Sha256(data);
        }

        [DisplayName("_deploy")]
        public static void Deploy(object data, bool update)
        {
            if (!update)
            {
                var Tx = (Transaction) Runtime.ScriptContainer;
                ContractMetadata.Put("Owner", (ByteString) Tx.Sender);
            }
        }
    }
}
