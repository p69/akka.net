using JetBrains.Annotations;

namespace AkkaChat.Features.Settings.Messages
{
    public class JoinChatMessage
    {
        [NotNull]
        public string UserName { get; }

        public JoinChatMessage([NotNull] string userName)
        {
            UserName = userName;
        }
    }
}