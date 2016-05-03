namespace AkkaChat.Model.Connection.Messages.ServerActions
{
    public interface IServerAction
    {
        ActionType Type { get; }
    }
}