namespace AkkaChat.Messages
{
    public sealed class StartAppMessage
    {
        public string Text { get; }

        public StartAppMessage(string text)
        {
            Text = text;
        }
    }
}