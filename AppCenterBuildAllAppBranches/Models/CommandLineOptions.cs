using CommandLine;

namespace AppCenterBuildAllAppBranches
{
    class CommandLineOptions
    {
        [Option(shortName: 'o', longName: "ownername", Required = true, HelpText = "The name of the owner")]
        public string OwnerName { get; set; }

        [Option(shortName: 'a', longName: "appname", Required = true, HelpText = "The name of the application")]
        public string AppName { get; set; }

        [Option(shortName: 'k', longName: "apikey", Required = true, HelpText = "Application API token or user API token")]
        public string ApiKey { get; set; }

        [Option(longName: "rate", Required = false, HelpText = "A delay between build status queries (in seconds)", Default = 10)]
        public int PollingDelayInSeconds { get; set; }
    }
}
