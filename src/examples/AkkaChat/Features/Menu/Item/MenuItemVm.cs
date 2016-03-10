using Akka.Actor;
using AkkaChat.ActorModel.UI.Routing.Messages;
using AkkaChat.Bootstrapping;
using AkkaChat.Features.Common;

namespace AkkaChat.Features.Menu.Item
{
    public class MenuItemVm : BindableBase, IMenuItemVm
    {
        private bool _isSelected;

        public MenuItemVm(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; }
        public void OnTap()
        {
            AppRoot.Router.Tell(new RouteMessage(Path), ActorRefs.Nobody);
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Path { get; }
    }
}