using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppCenterBuildAllAppBranches
{
    class Build
    {
        public long Id { get; set; }

        public BuildStatus Status { get; set; }

        public BuildResult? Result { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? FinishTime { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> Parameters { get; set; }
    }
}
