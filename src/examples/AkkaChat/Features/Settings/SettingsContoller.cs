using Akka.Actor;
using AkkaChat.Features.Common.Messages.Navigation;

namespace AkkaChat.Features.Settings
{
    public class SettingsContoller : ReceiveActor
    {
        private SettingsView _view;
        private ISettingsVm _vm;

        public SettingsContoller()
        {
            Receive<OnNavigatedTo>(_ => OnNavigated());
        }

        private void OnNavigated()
        {
            if (_view == null)
            {
                _view = new SettingsView();
            }
            _vm = new SettingsVm();
            _view.Vm = _vm;
            Context.Parent.Tell(new ShowView(_view));
        }
    }
}