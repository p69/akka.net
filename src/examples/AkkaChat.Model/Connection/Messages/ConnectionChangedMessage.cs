namespace AkkaChat.Model.Connection.Messages
{
    public sealed class ConnectionChangedMessage
    {
        private ConnectionChangedMessage(ConectionStatus status, string connectionError)
        {
            Status = status;
            ConnectionError = connectionError;
        }

        public ConectionStatus Status { get; }
        public string ConnectionError { get; }

        public static ConnectionChangedMessage Disconnected()
        {
            return new ConnectionChangedMessage(
                status: ConectionStatus.Disconnected,
                connectionError: null);
        }

        public static ConnectionChangedMessage Connecting()
        {
            return new ConnectionChangedMessage(
                status: ConectionStatus.Connecting,
                connectionError: null);
        }

        public static ConnectionChangedMessage Connected()
        {
            return new ConnectionChangedMessage(
                status: ConectionStatus.Connected,
                connectionError: null);
        }

        public static ConnectionChangedMessage Reconnecting()
        {
            return new ConnectionChangedMessage(
                status: ConectionStatus.Reconnecting,
                connectionError: null);
        }

        public static ConnectionChangedMessage Error(string error)
        {
            return new ConnectionChangedMessage(
                status: ConectionStatus.Disconnected,
                connectionError: error);
        }
    }

    public enum ConectionStatus
    {
        Disconnected,
        Connecting,
        Connected,
        Reconnecting
    }
}