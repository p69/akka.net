using System.ComponentModel;

namespace AkkaChat.Features.Menu.Item
{
    public interface IMenuItemVm : INotifyPropertyChanged
    {
        string Name { get; }
        void OnTap();
        bool IsSelected { get; set; }
        string Path { get; }
    }
}