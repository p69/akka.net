using Akka.Actor;
using AkkaChat.ActorModel;
using AkkaChat.ActorModel.UI.Routing;
using AkkaChat.ActorModel.UI.Routing.Messages;
using AkkaChat.Features.Layout;

namespace AkkaChat.Bootstrapping
{
    public static class AppRoot
    {
        public static void Start(Layout layoutView)
        {
            System = ActorSystem.Create("akka-chat-system");
            Router =
                System.ActorOf(Props.Create(() => new Router()).WithDispatcher(AkkaDIspatchers.UiDispatcher), "router");
            LayoutController =
                System.ActorOf(
                    Props.Create(() => new LayoutController(layoutView)).WithDispatcher(AkkaDIspatchers.UiDispatcher),
                    "layput");
            InitRouting();
            LayoutController.Tell(new AppReadyMessage());
        }

        private static void InitRouting()
        {
            var routes = new[]
            {
                new RouteEntry("about", Props.Empty, "about"),
                new RouteEntry("home", Props.Empty, "home")
            };
            Router.Tell(new InitRouting(routes));
        }

        public static ActorSystem System { get; private set; }
        public static IActorRef Router { get; private set; }
        public static IActorRef LayoutController { get; private set; }
    }
}