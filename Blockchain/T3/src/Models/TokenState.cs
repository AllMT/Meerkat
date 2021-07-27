namespace T3.Models
{
    public class TokenState
    {
        public Neo.SmartContract.Framework.ByteString Id;
        public System.Numerics.BigInteger Index;
        public Neo.UInt160 Owner;
        public TokenProperties Value;
    }
}
