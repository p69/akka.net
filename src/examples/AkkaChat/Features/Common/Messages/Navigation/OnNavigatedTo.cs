namespace AkkaChat.Features.Common.Messages.Navigation
{
    public sealed class OnNavigatedTo
    {
        public OnNavigatedTo(string args)
        {
            Args = args;
        }

        public string Args { get; }
    }
}