using JetBrains.Annotations;

namespace AkkaChat.Model.Chat.Messages
{
    public class SendTextMessage
    {
        public string Text { get; }

        public SendTextMessage([NotNull] string text)
        {
            Text = text;
        }
    }
}