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
        //To be implemented later

        // private const string COLLECTION_EDIT_PERMISSION_PREFIX = "T3CEP";
        // private const string COLLECTION_NAME_PREFIX = "T3CLN";
        // private static StorageMap CollectionNameStorageMap() => new StorageMap(Storage.CurrentContext, COLLECTION_NAME_PREFIX);
        // private static StorageMap CollectionEditPermissionStorageMap() => new StorageMap(Storage.CurrentContext, COLLECTION_EDIT_PERMISSION_PREFIX);

        // public static Iterator GetCollectionNames() => CollectionNameStorageMap().Find(FindOptions.KeysOnly | FindOptions.RemovePrefix);
        // public static List<ByteString> GetCollectionTokens(ByteString name) => (List<ByteString>)StdLib.Deserialize(CollectionNameStorageMap().Get(name));
        // public static Iterator GetMyCollections() => CollectionEditPermissionStorageMap().Find(GetSenderAddress(), FindOptions.KeysOnly | FindOptions.RemovePrefix);

        // protected static bool AddOrUpdateCollection(string collectionName, ByteString TokenId)
        // {
        //     var collection = CollectionNameStorageMap().Get(collectionName);

        //     if(collection == null)
        //     {
        //         CollectionNameStorageMap().Put(collectionName, StdLib.Serialize(new List<ByteString>() { TokenId }) );
        //         CollectionEditPermissionStorageMap().Put(GetSenderAddress() + collectionName, GetSenderAddress());
        //         return true;
        //     }

        //     var owner = CollectionEditPermissionStorageMap().Get(GetSenderAddress() + collectionName);
        //     if(owner == null)
        //     {
        //         throw new Exception("Adding collection failed");
        //     }

        //     var collectionDeserialized = (List<ByteString>)StdLib.Deserialize(collection);
        //     collectionDeserialized.Add(TokenId);
        //     CollectionNameStorageMap().Put(collectionName, StdLib.Serialize(collection));
        //     return true;
        // }
    }
}
