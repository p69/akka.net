using JetBrains.Annotations;

namespace AkkaChat.Model.Connection.Messages.ServerActions
{
    public class SendTextActionMessage : IServerAction
    {
        public string UserName { get; }
        public string Text { get; }
        public ActionType Type => ActionType.SendMessage;

        public SendTextActionMessage([NotNull] string userName, [NotNull] string text)
        {
            UserName = userName;
            Text = text;
        }
    }
}