using System.Linq;
using Akka.Actor;
using Akka.Util.Internal;
using AkkaChat.ActorModel.UI.Routing.Messages;
using AkkaChat.Bootstrapping;
using AkkaChat.Features.Menu.Item;
using AkkaChat.Features.Menu.Messages;

namespace AkkaChat.Features.Menu
{
    public sealed class MenuController : ReceiveActor
    {
        private IMenuVm _menuVm;

        public MenuController()
        {
            Receive<RouterReady>(msg => Init(msg));
            Receive<RouteChangedMessage>(msg => OnCurrentRouteChanged(msg));
        }

        private void OnCurrentRouteChanged(RouteChangedMessage routeChangedMessage)
        {
            var selectedItem = _menuVm?.Items.FirstOrDefault(x => x.Path == routeChangedMessage.CurrentRoutePath);
            if (selectedItem != null)
            {
                _menuVm.Items.ForEach(x=>x.IsSelected = false);
                selectedItem.IsSelected = true;
            }
        }

        private void Init(RouterReady message)
        {
            AppRoot.Router.Tell(new SubscribeToRouteChanges(Self));
            _menuVm = new MenuVm(message.Items.Select(x => new MenuItemVm(x.Name, x.Path)).ToList());
            Context.Parent.Tell(new MenuReady(_menuVm));
        }
    }
}