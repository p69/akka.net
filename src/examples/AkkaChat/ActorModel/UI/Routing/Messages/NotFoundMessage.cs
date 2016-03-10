namespace AkkaChat.ActorModel.UI.Routing.Messages
{
    public sealed class NotFoundMessage
    {
        public string Path { get; }

        public NotFoundMessage(string path)
        {
            Path = path;
        }
    }
}