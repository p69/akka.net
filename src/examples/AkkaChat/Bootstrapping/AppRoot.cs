using Akka.Actor;
using AkkaChat.ActorModel.Model;
using AkkaChat.ActorModel.UI.Shell;
using AkkaChat.ViewModels;
using AkkaChat.Views;

namespace AkkaChat.Bootstrapping
{
    public static class AppRoot
    {
        public static void Start()
        {
            System = ActorSystem.Create("akka-chat-system");
            UsersVm = new UsersViewModel();
            ChatActorRef =
                System.ActorOf(Props.Create(() => new Chat(UsersVm)), "chat");
            var shellActor =
                System.ActorOf(
                    Props.Create(() => new ShellActor())
                        .WithDispatcher(AkkaDIspatchers.UiDispatcher), "ui-shell");
            //shellActor.Tell(new GoToPageMessage(typeof (ChatPage), new ChatViewModel(), ApperanceTransition.Entrance));
        }

        public static ActorSystem System { get; private set; }
        public static IActorRef ChatActorRef { get; private set; }
        public static UsersViewModel UsersVm { get; private set; }
    }
}