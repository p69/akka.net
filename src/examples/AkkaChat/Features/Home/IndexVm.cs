using AkkaChat.Features.Common;

namespace AkkaChat.Features.Home
{
    public class IndexVm : BindableBase, IIndexVm
    {
        public string Title { get; } = "Home page";
    }
}