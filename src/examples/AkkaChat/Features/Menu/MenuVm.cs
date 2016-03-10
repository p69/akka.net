using System.Collections.Generic;
using System.Collections.ObjectModel;
using AkkaChat.Features.Common;
using AkkaChat.Features.Menu.Item;

namespace AkkaChat.Features.Menu
{
    public sealed class MenuVm : BindableBase, IMenuVm
    {
        public MenuVm(IReadOnlyList<IMenuItemVm> items)
        {
            Items = new ObservableCollection<IMenuItemVm>(items);
        }
        public ObservableCollection<IMenuItemVm> Items { get; }
    }
}