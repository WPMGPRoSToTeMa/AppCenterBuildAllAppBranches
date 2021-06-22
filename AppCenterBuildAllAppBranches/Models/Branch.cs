using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppCenterBuildAllAppBranches
{
    class Branch
    {
        public string Name { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> Properties { get; set; }
    }
}
