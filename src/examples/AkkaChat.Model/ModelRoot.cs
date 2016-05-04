using Akka.Actor;
using AkkaChat.Model.Connection;
using JetBrains.Annotations;

namespace AkkaChat.Model
{
    public static class ModelRoot
    {
        public static void Init([NotNull] ActorSystem systemRoot)
        {
            System = systemRoot;
            Connection = systemRoot.ActorOf(Props.Create(() => new ServerConnectionActor()), "connection");
        }

        public static IActorRef Connection { get; private set; }
        public static ActorSystem System { get; private set; }
    }
}