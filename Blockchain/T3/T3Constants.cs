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
        protected static byte[] ART_PREFIX = new byte[] { 0x41, 0x52, 0x54 };
        protected static ByteString COLLECTIBLE_PREFIX = "T3CEK";
        protected static ByteString DOMAIN_NAME_PREFIX = "T3DMK";
        protected static ByteString MUSIC_PREFIX = "T3MK";
        protected static ByteString SPORT_PREFIX = "T3SK";
        protected static ByteString TRADING_CARD_PREFIX = "T3TCK";
        protected static ByteString UTILITY_PREFIX = "T3UK";
        protected static ByteString VIRTUAL_WORLD_PREFIX = "T3VWK";
    }
}
