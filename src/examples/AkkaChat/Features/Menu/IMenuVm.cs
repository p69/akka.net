using System.Collections.ObjectModel;
using AkkaChat.Features.Menu.Item;

namespace AkkaChat.Features.Menu
{
    public interface IMenuVm
    {
        ObservableCollection<IMenuItemVm> Items { get; }
    }
}