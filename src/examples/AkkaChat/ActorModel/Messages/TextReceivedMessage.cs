namespace AkkaChat.ActorModel.Messages
{
    public sealed class TextReceivedMessage
    {
        public string UserName { get; }
        public string Text { get; }

        public TextReceivedMessage(string userName, string text)
        {
            UserName = userName;
            Text = text;
        }
    }
}