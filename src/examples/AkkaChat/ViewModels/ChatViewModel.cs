using System.Collections.ObjectModel;
using AkkaChat.ActorModel.Messages;

namespace AkkaChat.ViewModels
{
    public class ChatViewModel : BindableBase
    {
        public ChatViewModel()
        {
            Items = new ObservableCollection<ChatItemViewModel>();
        }

        public void OnTextMessageReceived(TextReceivedMessage message)
        {
            Items.Add(new ChatItemViewModel(message.UserName, message.Text));
        }

        public ObservableCollection<ChatItemViewModel> Items { get; }
    }
}