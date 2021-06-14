namespace T3.Models
{
    public class TokenProperties
    {
        public TokenProperties(string properties)
        {
            TokenData = new TokenData(properties);
        }
        
        public TokenData TokenData;
        public MarketData MarketData;
    }
}
