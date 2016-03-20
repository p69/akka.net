namespace AkkaChat.ActorModel.UI.Routing.Messages
{
    public sealed class OnNavigatedMessage
    {
        public OnNavigatedMessage(RouteEntry route, string args)
        {
            Route = route;
            Args = args;
        }

        public RouteEntry Route { get; }
        public string Args { get; }
    }
}