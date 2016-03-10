using System.Collections.Generic;

namespace AkkaChat.ActorModel.UI.Routing.Messages
{
    public class AllRoutesMessageReply
    {
        public AllRoutesMessageReply(IReadOnlyList<RouteItem> items, string currentRoute)
        {
            Items = items;
            CurrentRoute = currentRoute;
        }

        public IReadOnlyList<RouteItem> Items { get; }
        public string CurrentRoute { get; }
    }

    public class RouteItem
    {
        public RouteItem(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; }
        public string Path { get; }
    }
}