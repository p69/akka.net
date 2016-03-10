using System.Collections.Generic;
using Akka.Actor;

namespace AkkaChat.ActorModel.UI.Routing.Messages
{
    public sealed class InitRouting
    {
        public InitRouting(IReadOnlyList<RouteEntry> routes)
        {
            Routes = routes;
        }

        public IReadOnlyList<RouteEntry> Routes { get; }
    }

    public sealed class RouteEntry
    {
        public RouteEntry(string path, Props props, string name)
        {
            Path = path;
            Props = props;
            Name = name;
        }

        public string Path { get; }
        public Props Props { get; }
        public string Name { get; }
    }
}