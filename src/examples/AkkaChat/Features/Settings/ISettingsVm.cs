using System.ComponentModel;

namespace AkkaChat.Features.Settings
{
    public interface ISettingsVm : INotifyPropertyChanged
    {
        string Title { get; }
        string UserName { get; set; }
        void Join();
        bool IsOnline { get; }
    }
}