using System.Threading.Tasks;
using Akka.Actor;
using AkkaChat.Messages;

namespace AkkaChat.Actors
{
    public class AkkaChat : ReceiveActor
    {
        public AkkaChat()
        {
            Receive<StartAppMessage>(message => ChatAppStarted(message));
        }

        private static void ChatAppStarted(StartAppMessage message)
        {
            var text = message.Text;
        }
    }
}