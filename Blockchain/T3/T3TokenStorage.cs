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
        protected static StorageMap TokenStorageMap() => new StorageMap(Storage.CurrentContext, "T3TOKEN");

        protected static void AddToStorage(ByteString tokenId, ByteString value) => TokenStorageMap().Put(tokenId, value);
        protected static ByteString GetFromStorage(ByteString tokenId) => TokenStorageMap().Get(tokenId);
        protected static void DeleteFromStorage(ByteString tokenId) => TokenStorageMap().Delete(tokenId);

        protected static TokenState ValueOf(ByteString tokenId) => GetTokenFromStorage(tokenId);
        public static Iterator TokenKeys() => TokenStorageMap().Find(FindOptions.KeysOnly | FindOptions.RemovePrefix);
        public static Iterator Tokens() => TokenStorageMap().Find(FindOptions.RemovePrefix);
        private static UInt160 OwnerOf(ByteString tokenId) => ValueOf(tokenId).Owner;


        protected static void AddTokenToStorage(ByteString tokenId, TokenState token, BigInteger increment)
        {
            token.Id = tokenId;
            var tokenSerialized = StdLib.Serialize(token);

            if(token.Value.TokenData.Category == Categories.ART)
            {
                AddArt(token.Id, tokenSerialized, increment);
            }
            else if(token.Value.TokenData.Category == Categories.COLLECTIBLES)
            {
                AddCollectible(token.Id, tokenSerialized, increment);
            }
            else if(token.Value.TokenData.Category == Categories.DOMAINNAMES)
            {
                AddDomainName(token.Id, tokenSerialized, increment);
            }
            else if(token.Value.TokenData.Category == Categories.MUSIC)
            {
                AddMusic(token.Id, tokenSerialized, increment);
            }
            else if(token.Value.TokenData.Category == Categories.SPORTS)
            {
                AddSport(token.Id, tokenSerialized, increment);
            }
            else if(token.Value.TokenData.Category == Categories.TRADINGCARDS)
            {
                AddTradingCard(token.Id, tokenSerialized, increment);
            }
            else if(token.Value.TokenData.Category == Categories.UTILITIES)
            {
                AddUtility(token.Id, tokenSerialized, increment);
            }
            else if(token.Value.TokenData.Category == Categories.VIRTUALWORLDS)
            {
                AddVirtualWorld(token.Id, tokenSerialized, increment);
            }

            AddToStorage(token.Id, token.Value.TokenData.Category);
            UpdateTotalNFTSupply(increment);
        }

        protected static TokenState GetTokenFromStorage(ByteString tokenId)
        {
            var tokenCategory = GetFromStorage(tokenId);
            if(tokenCategory == null)
            {
                throw new Exception("Token does not exist");
            }

            if(tokenCategory == Categories.ART)
            {
                return GetArt(tokenId);
            }
            else if(tokenCategory == Categories.COLLECTIBLES)
            {
                return GetCollectible(tokenId);
            }
            else if(tokenCategory == Categories.DOMAINNAMES)
            {
                return GetDomainName(tokenId);
            }
            else if(tokenCategory == Categories.MUSIC)
            {
                return GetMusic(tokenId);
            }
            else if(tokenCategory == Categories.SPORTS)
            {
                return GetSport(tokenId);
            }
            else if(tokenCategory == Categories.TRADINGCARDS)
            {
                return GetTradingCard(tokenId);
            }
            else if(tokenCategory == Categories.UTILITIES)
            {
                return GetUtility(tokenId);
            }
            else if(tokenCategory == Categories.VIRTUALWORLDS)
            {
                return GetVirtualWorld(tokenId);
            }

            throw new Exception("Category does not exist");
        }

        protected static void DeleteTokenFromStorage(ByteString tokenId)
        {
            var tokenCategory = (string)GetFromStorage(tokenId);
            if(tokenCategory == null)
            {
                throw new Exception("Token does not exist");
            }

            if(tokenCategory == Categories.ART)
            {
                DeleteArt(tokenId);
            }
            else if(tokenCategory == Categories.COLLECTIBLES)
            {
                DeleteCollectible(tokenId);
            }
            else if(tokenCategory == Categories.DOMAINNAMES)
            {
                DeleteDomainName(tokenId);
            }
            else if(tokenCategory == Categories.MUSIC)
            {
                DeleteMusic(tokenId);
            }
            else if(tokenCategory == Categories.SPORTS)
            {
                DeleteSport(tokenId);
            }
            else if(tokenCategory == Categories.TRADINGCARDS)
            {
                DeleteTradingCard(tokenId);
            }
            else if(tokenCategory == Categories.UTILITIES)
            {
                DeleteUtility(tokenId);
            }
            else if(tokenCategory == Categories.VIRTUALWORLDS)
            {
                DeleteVirtualWorld(tokenId);
            }

            DeleteFromStorage(tokenId);
            UpdateTotalNFTSupply(-1);
        }

        protected static bool DoesTokenExist(ByteString tokenId)
        {
            var tokenCategory = GetFromStorage(tokenId);
            return tokenCategory != null;
        }
    }
}
