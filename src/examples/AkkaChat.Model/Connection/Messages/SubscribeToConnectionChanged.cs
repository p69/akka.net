using Akka.Actor;

namespace AkkaChat.Model.Connection.Messages
{
    public sealed class SubscribeToConnectionChanged
    {
        public SubscribeToConnectionChanged(IActorRef subscriber)
        {
            Subscriber = subscriber;
        }

        public IActorRef Subscriber { get; }
    }
}