namespace AkkaChat.Model.Connection.Messages
{
    public sealed class ConnectMessage
    {
        public ConnectMessage(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}