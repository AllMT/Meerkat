namespace T3.Models
{
    public static class Categories
    {
        public static string ART = "Art";
        public static string MUSIC = "Music";
        public static string DOMAINNAMES = "Domain Names";
        public static string VIRTUALWORLDS = "Virtual worlds";
        public static string TRADINGCARDS = "Trading Cards";
        public static string COLLECTIBLES = "Collectibles";
        public static string SPORTS = "Sports";
        public static string UTILITIES = "Utility";

        public static bool IsValidCategory(string category)
        {
            return category == ART || 
                    category == MUSIC || 
                    category == DOMAINNAMES ||
                    category == VIRTUALWORLDS ||
                    category == TRADINGCARDS ||
                    category == COLLECTIBLES ||
                    category == SPORTS ||
                    category == UTILITIES;
        }
    }
}
