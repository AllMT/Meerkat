using System;
using System.ComponentModel;
using System.Numerics;
using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services;
using Neo.SmartContract.Framework.Native;
using T3.Models;

namespace T3
{
    [DisplayName("T3")]
    [ManifestExtra("Author", "AllMT")]
    [ContractPermission("*", "onNEP17Payment")]
    [ContractPermission("*", "onNEP11Payment")]
    public partial class T3Contract : SmartContract
    {
        public static bool MintToken(ByteString tokenId, string properties)
        {
            if(DoesTokenExist(tokenId))
            {
                throw new Exception("Token already exists");
            }

            Mint(tokenId, GetTokenState(properties));
            return true;
        }
        
        private static TokenState GetTokenState(string properties)
        {
            return new TokenState()
            {
                Owner = GetSenderAddress(),
                Value = new TokenProperties(properties)
            };
        }

        public static List<string> TestGetTokenIdsFor(UInt160 owner)
        {
            var list = new List<string>();
            var iterator = TokensOf(owner);

            while(iterator.Next())
            {
                list.Add((string)iterator.Value);
            }

            return list;
        }

        public static List<string> TestGetAllTokenIds()
        {
            var list = new List<string>();
            var iterator = TokenKeys();

            while(iterator.Next())
            {
                list.Add((string)iterator.Value);
            }

            return list;
        }

        public static List<string> TestGetAllTokens()
        {
            var list = new List<string>();
            var iterator = Tokens();

            while(iterator.Next())
            {
                list.Add((string)iterator.Value);
            }

            return list;
        }

        public static List<string> TestGetAllMarketTokenIds()
        {
            var list = new List<string>();
            var iterator = MarketTokens();

            while(iterator.Next())
            {
                list.Add((string)iterator.Value);
            }

            return list;
        }

        public static string GetTokenProperties(ByteString tokenId)
        {
            var token = ValueOf(tokenId);

            var map = new Map<string, object>();
            map["name"] = token.Value.TokenData.Name;
            map["description"] = token.Value.TokenData.Description;
            map["image"] = token.Value.TokenData.Image;
            map["tokenURI"] = token.Value.TokenData.TokenURI;
            map["category"] = token.Value.TokenData.Category;
            map["collection"] = token.Value.TokenData.Collection;

            // if(IsWhitelisted(GetSenderAddress(), tokenId))
            // {
            //     map["lockedContent"] = token.Value.TokenData.LockedContent;
            // }

            if(token.Value.MarketData != null)
            {
                map["price"] = token.Value.MarketData.Price;
                map["listingType"] = token.Value.MarketData.ListingType;
                map["purchaseType"] = token.Value.MarketData.PurchaseType;
            }

            return StdLib.JsonSerialize(map);
        }

        public static List<TokenState> GetLatestTokens()
        {
            var total = T3Supply();
            var lowest = (total - 10) > 0 ? 0 : total - 10;

            var tokens = new List<TokenState>();

            var tokenIterator = TokenKeys();
            while(tokenIterator.Next() || (lowest == total))
            {
                tokens.Add(ValueOf((ByteString)tokenIterator.Value));
                lowest += 1;
            }

            return tokens;
        }

        public static List<TokenState> TestGetLatestArtTokensByIndex()
        {
            var total = T3ArtIndexSupply() + 1;
            BigInteger lowest = (total - 10) < 0 ? 1 : total - 10;

            var tokens = new List<TokenState>();
            while(lowest != total)
            {
                tokens.Add(GetArtByIndex(lowest));
                lowest += 1;
            }

            return tokens;
        }

        public static void TestBurnAll()
        {
            var tokenIterator = TokenKeys();
            while(tokenIterator.Next())
            {
                Burn((ByteString)tokenIterator.Value);
            }
        }


        public static TokenState TestGetTokenProperties(ByteString tokenId) => ValueOf(tokenId);

        // public static TicketPaginate GetTickets(int page)
        // {
        //     BigInteger height = GetTicketHeight();
        //     BigInteger itemsPerPage = defaultItemsPerPage > height ? height : defaultItemsPerPage;
        //     BigInteger totalPages = height > itemsPerPage ? height % itemsPerPage == 0 ? (height / itemsPerPage) : (height / itemsPerPage) + 1 : 1;
        //     BigInteger startAt = height - (itemsPerPage * (page - 1));
        //     BigInteger endAt = startAt > itemsPerPage ? startAt - itemsPerPage : 0;

        //     List<TicketRecord> list = new List<TicketRecord>();
        //     StorageMap Tickets = new StorageMap(Storage.CurrentContext, TICKET_PREFIX);
        //     for (BigInteger i = startAt; i != endAt; i--)
        //     {
        //         ByteString item = Tickets.Get(Helper.ToByteString(Helper.ToByteArray(Helper.ToByte(i))));
        //         if (item != null)
        //         {
        //             list.Add((TicketRecord)StdLib.Deserialize(item));
        //         }
        //     }
        //     TicketPaginate paginate = new TicketPaginate
        //     {
        //         totalItems = height,
        //         totalPages = totalPages,
        //         currentpage = page,
        //         items = list
        //     };
        //     return paginate;
        // }
    }
}