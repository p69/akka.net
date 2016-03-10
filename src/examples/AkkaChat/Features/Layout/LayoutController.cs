using Akka.Actor;
using AkkaChat.ActorModel;
using AkkaChat.Bootstrapping;
using AkkaChat.Features.Layout.Messages;
using AkkaChat.Features.Menu;
using AkkaChat.Features.Menu.Messages;

namespace AkkaChat.Features.Layout
{
    public class LayoutController : ReceiveActor
    {
        private readonly Layout _view;
        private readonly IActorRef _menuController;

        public LayoutController(Layout view)
        {
            _view = view;
            _menuController =
                Context.ActorOf(Props.Create(() => new MenuController()).WithDispatcher(AkkaDIspatchers.UiDispatcher));
            Receive<ShowView>(msg => ShowView(msg));
            Receive<MenuReady>(msg => _view.SetMenuVm(msg.MenuVm));
            Receive<AppReadyMessage>(_ => _menuController.Tell(new LayoutReady()));
        }

        private void ShowView(ShowView message)
        {
            _view.ChangeMainView(message.View);
        }
    }
}