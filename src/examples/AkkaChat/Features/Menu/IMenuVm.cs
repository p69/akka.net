using System.Collections.ObjectModel;
using System.ComponentModel;
using AkkaChat.Features.Menu.Item;

namespace AkkaChat.Features.Menu
{
    public interface IMenuVm : INotifyPropertyChanged
    {
        ObservableCollection<IMenuItemVm> Items { get; }
    }
}