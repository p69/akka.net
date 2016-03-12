using System.ComponentModel;

namespace AkkaChat.Features.Home
{
    public interface IHomeVm : INotifyPropertyChanged
    {
        string Title { get; set; } 
    }
}