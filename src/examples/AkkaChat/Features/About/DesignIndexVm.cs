using AkkaChat.Features.Common;

namespace AkkaChat.Features.About
{
    public class DesignIndexVm : BindableBase, IIndexVm
    {
        public string Title { get; } = "Design title";
        public string About { get; } = "Design long about description. Wow, magic.";
    }
}