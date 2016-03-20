using AkkaChat.Features.Common;

namespace AkkaChat.Features.Chats
{
    public class DesignChatsVm : BindableBase, IChatsVm
    {
        public string Title { get; set; } = "Design Title";
    }
}