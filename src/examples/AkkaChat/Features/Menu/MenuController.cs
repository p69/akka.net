using System.Linq;
using Akka.Actor;
using Akka.Util.Internal;
using AkkaChat.ActorModel.UI.Routing.Messages;
using AkkaChat.Bootstrapping;
using AkkaChat.Features.Layout.Messages;
using AkkaChat.Features.Menu.Item;
using AkkaChat.Features.Menu.Messages;

namespace AkkaChat.Features.Menu
{
    public sealed class MenuController : ReceiveActor
    {
        private IMenuVm _menuVm;

        public MenuController()
        {
            Receive<LayoutReady>(msg => OnLayoutReady(msg));
            Receive<AllRoutesMessageReply>(msg => InitVm(msg));
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

        private void InitVm(AllRoutesMessageReply message)
        {
            _menuVm = new MenuVm(message.Items.Select(x => new MenuItemVm(x.Name, x.Path)).ToList());
            AppRoot.LayoutController.Tell(new MenuReady(_menuVm));
        }

        private void OnLayoutReady(LayoutReady message)
        {
            AppRoot.Router.Tell(new SubscribeToRouteChanges(Self));
        }
    }
}