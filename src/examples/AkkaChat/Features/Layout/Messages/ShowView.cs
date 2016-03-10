using AkkaChat.Features.Common;

namespace AkkaChat.Features.Layout.Messages
{
    public sealed class ShowView
    {
        public ShowView(ViewBase view)
        {
            View = view;
        }

        public ViewBase View { get; }
    }
}