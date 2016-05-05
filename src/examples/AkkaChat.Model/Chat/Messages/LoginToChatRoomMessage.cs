using JetBrains.Annotations;

namespace AkkaChat.Model.Chat.Messages
{
    public class LoginToChatRoomMessage
    {
        public string UserName { get; }

        public LoginToChatRoomMessage([NotNull] string userName)
        {
            UserName = userName;
        }
    }
}