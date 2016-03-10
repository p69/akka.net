using Akka.Actor;

namespace AkkaChat.ActorModel.UI.Routing.Messages
{
    public class SubscribeToRouteChanges
    {
        public SubscribeToRouteChanges(IActorRef subscriber)
        {
            Subscriber = subscriber;
        }

        public IActorRef Subscriber { get; }
    }
}