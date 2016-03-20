namespace AkkaChat.Features.Layout.Messages
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