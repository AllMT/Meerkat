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
        public static bool MintToken(string properties)
        {
            // if(DoesTokenExist(tokenId))
            // {
            //     throw new Exception("Token already exists");
            // }
            var tokenId = NewTokenId();
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

        public static List<TokenState> GetMarketTokens()
        {
            var total = T3MarketSupply();
            var lowest = (total - 25) > 0 ? total - 25 : 0;

            var tokens = new List<TokenState>();

            var tokenIterator = MarketTokens();
            while(tokenIterator.Next() && (lowest != total))
            {
                tokens.Add(ValueOf((ByteString)tokenIterator.Value));
                lowest += 1;
            }

            return tokens;
        }

        public static List<TokenState> GetMainPageMarketTokens()
        {

            var total = T3MarketIndexSupply();
            BigInteger highest = total;
            BigInteger lowest = (total - 3) > 0 ? total - 3 : 0;

            var tokens = new List<TokenState>();
            while(lowest != highest)
            {
                var token = GetMarketTokenByIndex(highest);

                if (token == null)
                {
                    if(lowest > 0)
                    {
                        lowest -= 1;
                    }
                }
                else
                {
                    tokens.Add(token);
                }
                highest -= 1;
            }

            return tokens;
        }

        private static TokenState GetMarketTokenByIndex(BigInteger index)
        {
            var tokenId = MarketTokenIndexStorageMap().Get((ByteString)index);
            return (TokenState)GetTokenFromStorage(tokenId);
        }

        public static List<TokenState> GetLatestTokens()
        {
            var total = T3TokenIndexSupply();   
            BigInteger highest = total;
            BigInteger lowest = (total - 25) > 0 ? total - 25 : 0;

            var tokens = new List<TokenState>();
            while(lowest != highest)
            {
                var token = GetTokenByIndex(highest);

                if (token == null)
                {
                    if(lowest > 0)
                    {
                        lowest -= 1;
                    }
                }
                else
                {
                    tokens.Add(token);
                }
                highest -= 1;
            }

            return tokens;
        }

        public static List<TokenState> GetMainPageLatestTokens()
        {
            var total = T3TokenIndexSupply();
            BigInteger highest = total;
            BigInteger lowest = (total - 3) > 0 ? total - 3 : 0;

            var tokens = new List<TokenState>();
            while(lowest != highest)
            {
                var token = GetTokenByIndex(highest);

                if (token == null)
                {
                    if(lowest > 0)
                    {
                        lowest -= 1;
                    }
                }
                else
                {
                    tokens.Add(token);
                }
                highest -= 1;
            }

            return tokens;
        }


        public static List<TokenState> GetTokensFor(UInt160 owner)
        {
            var tokens = new List<TokenState>();

            var tokenIterator = TokensOf(owner);
            while(tokenIterator.Next())
            {
                tokens.Add(ValueOf((ByteString)tokenIterator.Value));
            }

            return tokens;
        }

        public static TokenState GetTokenByIndex(BigInteger index)
        {
            var tokenId = TokenIndexStorageMap().Get((ByteString)index);
            return (TokenState)GetTokenFromStorage(tokenId);
        }
    }
}