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
    public interface IT3TokenStorage 
    {
        void Add(TokenState value);
        void UpdateMarketState(ByteString tokenId, MarketData market);
        void Delete(ByteString tokenId);
        TokenState Get(ByteString tokenId);
        TokenState GetByIndex(BigInteger index);
        Iterator Tokens();
        BigInteger Supply();
    }
}
