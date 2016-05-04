namespace AkkaChat.Server
{
    public class TextMessage
    {
        public string UserName { get; }
        public string Text { get; }

        public TextMessage(string userName, string text)
        {
            UserName = userName;
            Text = text;
        }
    }
}