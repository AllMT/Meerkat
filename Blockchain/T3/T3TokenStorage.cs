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
        protected const string TOKEN_MAP = "T3TOKENMAP";
        protected static StorageMap TokenStorageMap() => new StorageMap(Storage.CurrentContext, TOKEN_MAP);

        protected static void AddToStorage(ByteString tokenId, ByteString value) => TokenStorageMap().Put(tokenId, value);
        protected static ByteString GetFromStorage(ByteString tokenId) => TokenStorageMap().Get(tokenId);
        protected static void DeleteFromStorage(ByteString tokenId) => TokenStorageMap().Delete(tokenId);

        protected static TokenState ValueOf(ByteString tokenId) => (TokenState)StdLib.Deserialize(GetTokenFromStorage(tokenId));
        public static Iterator Tokens() => TokenStorageMap().Find(FindOptions.KeysOnly | FindOptions.RemovePrefix);
        public static UInt160 OwnerOf(ByteString tokenId) => ValueOf(tokenId).Owner;


        protected static void AddTokenToStorage(ByteString tokenId, ByteString value, string category, BigInteger increment)
        {
            if(category == Categories.ART)
            {
                AddArt(tokenId, value);
                UpdateTotalArtSupply(increment);
            }
            else if(category == Categories.COLLECTIBLES)
            {
                AddCollectible(tokenId, value);
                UpdateTotalCollectibleSupply(increment);
            }
            else if(category == Categories.DOMAINNAMES)
            {
                AddDomainName(tokenId, value);
                UpdateTotalDomainNameSupply(increment);
            }
            else if(category == Categories.MUSIC)
            {
                AddMusic(tokenId, value);
                UpdateTotalMusicSupply(increment);
            }
            else if(category == Categories.SPORTS)
            {
                AddSport(tokenId, value);
                UpdateTotalSportSupply(increment);
            }
            else if(category == Categories.TRADINGCARDS)
            {
                AddTradingCard(tokenId, value);
                UpdateTotalTradingCardSupply(increment);
            }
            else if(category == Categories.UTILITIES)
            {
                AddUtility(tokenId, value);
                UpdateTotalUtilitySupply(increment);
            }
            else if(category == Categories.VIRTUALWORLDS)
            {
                AddVirtualWorld(tokenId, value);
                UpdateTotalVirtualWorldSupply(increment);
            }

            AddToStorage(tokenId, category);
            UpdateTotalNFTSupply(increment);
        }

        protected static ByteString GetTokenFromStorage(ByteString tokenId)
        {
            var tokenCategory = (string)GetFromStorage(tokenId);
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
                UpdateTotalArtSupply(-1);
            }
            else if(tokenCategory == Categories.COLLECTIBLES)
            {
                DeleteCollectible(tokenId);
                UpdateTotalCollectibleSupply(-1);
            }
            else if(tokenCategory == Categories.DOMAINNAMES)
            {
                DeleteDomainName(tokenId);
                UpdateTotalDomainNameSupply(-1);
            }
            else if(tokenCategory == Categories.MUSIC)
            {
                DeleteMusic(tokenId);
                UpdateTotalMusicSupply(-1);
            }
            else if(tokenCategory == Categories.SPORTS)
            {
                DeleteSport(tokenId);
                UpdateTotalSportSupply(-1);
            }
            else if(tokenCategory == Categories.TRADINGCARDS)
            {
                DeleteTradingCard(tokenId);
                UpdateTotalTradingCardSupply(-1);
            }
            else if(tokenCategory == Categories.UTILITIES)
            {
                DeleteUtility(tokenId);
                UpdateTotalUtilitySupply(-1);
            }
            else if(tokenCategory == Categories.VIRTUALWORLDS)
            {
                DeleteVirtualWorld(tokenId);
                UpdateTotalVirtualWorldSupply(-1);
            }

            DeleteFromStorage(tokenId);
            UpdateTotalNFTSupply(-1);
        }
    }
}
