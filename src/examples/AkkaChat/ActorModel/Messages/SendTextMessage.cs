namespace AkkaChat.ActorModel.Messages
{
    public class SendTextMessage
    {
        public string UserName { get; }
        public string Text { get; }

        public SendTextMessage(string userName, string text)
        {
            UserName = userName;
            Text = text;
        }
    }
}