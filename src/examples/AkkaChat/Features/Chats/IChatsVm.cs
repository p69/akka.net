using System.ComponentModel;

namespace AkkaChat.Features.Chats
{
    public interface IChatsVm : INotifyPropertyChanged
    {
        string Title { get; set; } 
    }
}