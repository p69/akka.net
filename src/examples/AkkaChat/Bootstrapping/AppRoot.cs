using Akka.Actor;
using AkkaChat.ActorModel;
using AkkaChat.ActorModel.UI.Routing;
using AkkaChat.ActorModel.UI.Routing.Messages;
using AkkaChat.Features.About;
using AkkaChat.Features.Home;
using AkkaChat.Features.Layout;

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
            var routes = new[]
            {
                new RouteEntry("about", Props.Create(()=>new AboutContoller()).WithDispatcher(AkkaDIspatchers.UiDispatcher), "about"),
                new RouteEntry("home", Props.Create(()=>new HomeController()).WithDispatcher(AkkaDIspatchers.UiDispatcher), "home")
            };
            Router.Tell(new InitRouting(routes));
        }

        public static ActorSystem System { get; private set; }
        public static IActorRef Router { get; private set; }
    }
}