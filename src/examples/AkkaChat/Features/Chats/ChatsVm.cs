using AkkaChat.Features.Common;

namespace AkkaChat.Features.Chats
{
    public class ChatsVm : BindableBase, IChatsVm
    {
        private string _title;

        public ChatsVm()
        {
            _title = "Chats page";
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}