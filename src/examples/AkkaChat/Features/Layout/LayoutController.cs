using System.Collections.Generic;
using Akka.Actor;
using AkkaChat.ActorModel;
using AkkaChat.ActorModel.UI.Routing.Messages;
using AkkaChat.Features.Layout.Messages;
using AkkaChat.Features.Menu;
using AkkaChat.Features.Menu.Messages;

namespace AkkaChat.Features.Layout
{
    public class LayoutController : ReceiveActor
    {
        private readonly Layout _view;
        private readonly IActorRef _menuController;
        private IDictionary<string, IActorRef> _controllersCache;

        public LayoutController(Layout view)
        {
            _view = view;
            _menuController =
                Context.ActorOf(
                    Props.Create(() => new MenuController()).WithDispatcher(AkkaDIspatchers.UiDispatcher));
            Become(NotInited);
        }

        private void NotInited()
        {
            Receive<RouterReady>(
                msg =>
                {
                    _controllersCache = new Dictionary<string, IActorRef>(msg.Items.Count);
                    _menuController.Tell(msg);
                });
            Receive<MenuReady>(
                msg =>
                {
                    _view.SetMenuVm(msg.MenuVm);
                    Become(Inited);
                });
        }

        private void Inited()
        {
            Receive<OnNavigatedMessage>(msg => OnNavigated(msg));
            Receive<ShowView>(msg => ShowView(msg));
        }

        private void OnNavigated(OnNavigatedMessage message)
        {
            IActorRef controller;
            if (!_controllersCache.TryGetValue(message.Route.Path, out controller))
            {
                controller = Context.ActorOf(message.Route.Props, message.Route.Path);
                _controllersCache.Add(message.Route.Path, controller);
            }
            controller.Tell(new OnNavigatedTo(message.Args));
        }

        private void ShowView(ShowView message)
        {
            _view.ChangeMainView(message.View);
        }
    }
}