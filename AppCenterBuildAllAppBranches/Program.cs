using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCenterBuildAllAppBranches
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<CommandLineOptions>(args)
                .MapResult(async (CommandLineOptions options) =>
                {
                    await BuildAllAppBranches(options.OwnerName, options.AppName, options.ApiKey, TimeSpan.FromSeconds(options.PollingDelayInSeconds));

                    return 0;
                },
                errors => Task.FromResult(-1));
        }

        static async Task BuildAllAppBranches(string ownerName, string appName, string apiKey, TimeSpan buildStatusPollingDelay)
        {
            using AppCenterRestApiClient client = new(ownerName, appName, apiKey);

            AppBranch[] appBranches = await client.GetAppBranches();

            // Consider only configured branches
            var configuredAppBranches = appBranches.Where(appBranch => appBranch.Configured);

            IEnumerable<Task> tasks = configuredAppBranches.Select(appBranch => appBranch.Branch.Name)
                .Select(async branchName => await BuildAppBranch(client, branchName, buildStatusPollingDelay));

            // Build branches simultaneously if possible
            await Task.WhenAll(tasks);
        }

        static async Task BuildAppBranch(AppCenterRestApiClient client, string branchName, TimeSpan buildStatusPollingDelay)
        {
            Build build = await client.CreateBuild(branchName, new BuildParams { });

            while (build.Status != BuildStatus.Completed)
            {
                await Task.Delay(buildStatusPollingDelay);

                build = await client.GetBuild(build.Id);
            }

            string buildLink = client.GetBuildLink(branchName, build.Id);

            string result = build.Result == BuildResult.Succeeded ? "completed" : "failed";

            // Round up the build duration seconds
            TimeSpan elapsed = build.FinishTime.Value - build.StartTime.Value;
            long elapsedSeconds = (long)Math.Ceiling(elapsed.TotalSeconds);

            Console.WriteLine($"{branchName} build {result} in {elapsedSeconds} seconds. Link to build logs: {buildLink}");
        }
    }
}
