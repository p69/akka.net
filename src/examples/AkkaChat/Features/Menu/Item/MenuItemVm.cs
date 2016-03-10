using Akka.Actor;
using AkkaChat.ActorModel.UI.Routing.Messages;
using AkkaChat.Bootstrapping;
using AkkaChat.Features.Common;

namespace AkkaChat.Features.Menu.Item
{
    public class MenuItemVm : BindableBase, IMenuItemVm
    {
        private readonly string _path;

        public MenuItemVm(string name, string path)
        {
            _path = path;
            Name = name;
        }

        public string Name { get; }
        public void OnTap()
        {
            AppRoot.Router.Tell(new RouteMessage(_path), ActorRefs.Nobody);
        }
    }
}