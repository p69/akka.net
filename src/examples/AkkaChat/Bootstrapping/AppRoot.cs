using Akka.Actor;
using AkkaChat.ActorModel.Model;
using AkkaChat.ViewModels;

namespace AkkaChat.Bootstrapping
{
    public static class AppRoot
    {
        public static void Start()
        {
            System = ActorSystem.Create("akka-chat-system");
            MainVm = new MainViewModel();
            ChatActorRef =
                System.ActorOf(Props.Create(() => new Chat(MainVm)).WithDispatcher(AkkaDIspatchers.UiDispatcher), "chat");
        }

        public static ActorSystem System { get; private set; }
        public static IActorRef ChatActorRef { get; private set; }
        public static MainViewModel MainVm { get; private set; }
    }
}