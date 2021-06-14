using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Native;

namespace T3.Models
{
    public class TokenData
    {
        public TokenData(string properties)
        {
            var propertiesMap = (Map<string,string>)StdLib.JsonDeserialize(properties);

            if(!Categories.IsValidCategory(propertiesMap["category"]))
            {
                throw new System.Exception("Not a valid category");
            }
            
            Name = propertiesMap.HasKey("name") ?  propertiesMap["name"] : null;
            Description = propertiesMap.HasKey("description") ?  propertiesMap["description"] : null;
            Image = propertiesMap.HasKey("image") ?  propertiesMap["image"] : null;
            TokenURI = propertiesMap.HasKey("tokenURI") ?  propertiesMap["tokenURI"] : null;
            LockedContent = propertiesMap.HasKey("lockedContent") ?  propertiesMap["lockedContent"] : null;
            Collection = propertiesMap.HasKey("collection") ?  propertiesMap["collection"] : null;
            Category = propertiesMap.HasKey("category") ?  propertiesMap["category"] : null;
        }

        public string Name;
        public string Description;
        public string Category;
        public string Collection;
        public string Image;
        public string TokenURI;
        public string LockedContent;
    }
}
