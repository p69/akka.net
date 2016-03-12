using AkkaChat.Features.Common;

namespace AkkaChat.Features.Layout.Messages
{
    public sealed class ShowView
    {
        public ShowView(IView view)
        {
            View = view;
        }

        public IView View { get; }
    }
}