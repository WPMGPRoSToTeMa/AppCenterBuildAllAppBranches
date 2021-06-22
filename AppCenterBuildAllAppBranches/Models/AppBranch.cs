using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppCenterBuildAllAppBranches
{
    class AppBranch
    {
        public Branch Branch { get; set; }

        public bool Configured { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> Properties { get; set; }
    }
}
