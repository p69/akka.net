namespace AkkaChat.Model.Connection.Messages
{
    public sealed class ConnectionChangedMessage
    {
        public ConnectionChangedMessage(bool isConnected, string connectedUser)
        {
            IsConnected = isConnected;
            ConnectedUser = connectedUser;
        }

        public bool IsConnected { get; }
        public string ConnectedUser { get; }
    }
}