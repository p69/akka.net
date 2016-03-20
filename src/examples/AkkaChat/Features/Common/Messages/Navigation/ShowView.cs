namespace AkkaChat.Features.Common.Messages.Navigation
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