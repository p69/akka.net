namespace AkkaChat.ActorModel.UI.Routing.Messages
{
    public sealed class GoToMessage
    {
        public GoToMessage(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}