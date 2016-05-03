using Akka.Actor;
using AkkaChat.ActorModel;
using AkkaChat.ActorModel.UI.Routing;
using AkkaChat.ActorModel.UI.Routing.Messages;
using AkkaChat.Features.Chats;
using AkkaChat.Features.Layout;
using AkkaChat.Features.Settings;
using AkkaChat.Model.Connection;
using AkkaChat.Model.SignalR;

namespace AkkaChat.Bootstrapping
{
    public static class AppRoot
    {
        public static void Start(Layout layoutView)
        {
            System = ActorSystem.Create("akka-chat-system");

            var layout =
               System.ActorOf(
                   Props.Create(() => new LayoutController(layoutView)).WithDispatcher(AkkaDIspatchers.UiDispatcher),
                   "layout");

            Router =
                System.ActorOf(
                    Props.Create(() => new Router(layout)).WithDispatcher(AkkaDIspatchers.UiDispatcher),
                    "router");

            InitRouting();
        }

        private static void InitRouting()
        {
            var connectionProps = Props.Create(() => new ServerConnectionActor());
            var routes = new[]
            {
                new RouteEntry(
                    path: "settings",
                    props: Props.Create(() => new SettingsContoller(connectionProps)).WithDispatcher(AkkaDIspatchers.UiDispatcher),
                    name: "Settings"),
                new RouteEntry(
                    path: "chats",
                    props: Props.Create(() => new ChatsController()).WithDispatcher(AkkaDIspatchers.UiDispatcher),
                    name: "Chats")
            };
            Router.Tell(new InitRouting(routes));
        }

        public static ActorSystem System { get; private set; }
        public static IActorRef Router { get; private set; }
    }
}