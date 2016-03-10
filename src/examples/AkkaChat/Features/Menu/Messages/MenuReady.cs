namespace AkkaChat.Features.Menu.Messages
{
    public sealed class MenuReady
    {
        public MenuReady(IMenuVm menuVm)
        {
            MenuVm = menuVm;
        }

        public IMenuVm MenuVm { get; }
    }
}