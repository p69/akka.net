using System.Collections.ObjectModel;
using AkkaChat.Features.Menu.Item;

namespace AkkaChat.Features.Menu
{
    public interface IIndexVm
    {
        ObservableCollection<IMenuItemVm> Items { get; }
    }
}