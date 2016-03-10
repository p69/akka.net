using System.Collections.ObjectModel;
using AkkaChat.Features.Common;
using AkkaChat.Features.Menu.Item;

namespace AkkaChat.Features.Menu
{
    public class DesignMenuVm : BindableBase, IMenuVm
    {
        public ObservableCollection<IMenuItemVm> Items { get; } = new ObservableCollection<IMenuItemVm>()
        {
            new DesignMenuItemVm(),
            new DesignMenuItemVm(),
            new DesignMenuItemVm()
        };
    }
}