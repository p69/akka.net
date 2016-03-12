using AkkaChat.Features.Common;

namespace AkkaChat.Features.Home
{
    public class DesignHomeVm : BindableBase, IHomeVm
    {
        public string Title { get; set; } = "Design Title";
    }
}