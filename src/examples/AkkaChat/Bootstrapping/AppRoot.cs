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
            MainVm = new MainViewModel();
            ChatActorRef =
                System.ActorOf(Props.Create(() => new Chat(MainVm)).WithDispatcher(AkkaDIspatchers.UiDispatcher), "chat");
            var shellActor = System.ActorOf(Props.Create(() => new ShellActor(Shell.Current.AppFrame)), "ui-shell");
            shellActor.Tell(new GoToPageMessage(typeof (ChatPage), new ChatViewModel(), ApperanceTransition.Entrance));
        }

        public static ActorSystem System { get; private set; }
        public static IActorRef ChatActorRef { get; private set; }
        public static MainViewModel MainVm { get; private set; }
    }
}