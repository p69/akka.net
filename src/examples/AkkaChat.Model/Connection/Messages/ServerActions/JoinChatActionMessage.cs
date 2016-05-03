namespace AkkaChat.Model.Connection.Messages.ServerActions
{
    public class JoinChatActionMessage : IServerAction
    {
        public JoinChatActionMessage(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }

        public ActionType Type => ActionType.Join;
    }
}