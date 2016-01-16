using Akka.Actor;
using AkkaChat.Messages;

namespace AkkaChat.Bootstrapping
{
    public static class AppRoot
    {
        public static void Start()
        {
            var system = ActorSystem.Create("akka-chat-system");
            var akkaChat = system.ActorOf<Actors.AkkaChat>("akka-chat");
            akkaChat.Tell(new StartAppMessage("App is started!"));
        }
    }
}