namespace AkkaChat.ActorModel.UI.Routing.Messages
{
    public sealed class RouteMessage
    {
        public RouteMessage(string path)
        {
            Path = path;
        }

        public RouteMessage(string path, bool cacheActor)
        {
            Path = path;
            CacheActor = cacheActor;
        }

        public string Path { get; }
        public bool CacheActor { get; }
    }
}