using System;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TfsBuildMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var tfsServer = "tfs.tribalgroup.net";
            var tfsPort = 8080;
            var tfsCollection = "DefaultCollection";

            var teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri($"http://{tfsServer}:{tfsPort}/tfs/{tfsCollection}"));

            //Authenticate with current logged on user
            teamProjectCollection.Authenticate();

            var buildServer = teamProjectCollection.GetService<IBuildServer>();

            var qspec = buildServer.CreateBuildQueueSpec("*", "*");
            var res = buildServer.QueryQueuedBuilds(qspec);

            foreach (var qb in res.QueuedBuilds)
            {
                var buildName = qb.BuildDefinition?.Name ?? "No Name";
                Console.WriteLine(qb.TeamProject + ":" + buildName);
            }
        }
    }
}
