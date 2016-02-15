using AkkaChat.Features.Common;

namespace AkkaChat.Features.Home
{
    public class DesignIndexVm : BindableBase, IIndexVm
    {
        public string Title { get; } = "Design Title";
    }
}