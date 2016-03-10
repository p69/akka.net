namespace AkkaChat.ActorModel.UI.Routing.Messages
{
    public sealed class RouteChangedMessage
    {
        public RouteChangedMessage(string currentRoutePath)
        {
            CurrentRoutePath = currentRoutePath;
        }

        public string CurrentRoutePath { get; }
    }
}