using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppCenterBuildAllAppBranches
{
    class BuildParams
    {
        [JsonExtensionData]
        public Dictionary<string, JsonElement> Parameters { get; set; }
    }
}
