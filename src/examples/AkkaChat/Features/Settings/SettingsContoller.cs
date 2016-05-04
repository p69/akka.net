using Akka.Actor;
using AkkaChat.Features.Common.Messages.Navigation;
using AkkaChat.Features.Settings.Messages;
using AkkaChat.Model;
using AkkaChat.Model.Connection.Messages;
using AkkaChat.Model.Connection.Messages.ServerActions;
using AkkaChat.Model.Dto;

namespace AkkaChat.Features.Settings
{
    public class SettingsContoller : ReceiveActor
    {
        private SettingsView _view;
        private SettingsVm _vm;

        public SettingsContoller()
        {
            ModelRoot.System.EventStream.Subscribe(Self, typeof(ConnectionChangedMessage));
            Become(Offline);
        }

        private void Offline()
        {
            Receive<OnNavigatedTo>(_ => OnNavigated());
            Receive<ConnectionChangedMessage>(msg => OnConnectionChanged(msg));
        }

        private void Online()
        {
            Receive<OnNavigatedTo>(_ => OnNavigated());
            Receive<ConnectionChangedMessage>(msg => OnConnectionChanged(msg));
            Receive<JoinChatMessage>(msg => JoinChat(msg));
            Receive<JoinResult>(
                msg =>
                {
                    var error = msg.Error;
                    var isOk = msg.IsOk;
                });
        }
        private void OnNavigated()
        {
            if (_view != null)
            {
                Context.Parent.Tell(new ShowView(_view));
            }
            else
            {
                _view = new SettingsView();
                _vm = new SettingsVm(Self);
                _view.Vm = _vm;
                Context.Parent.Tell(new ShowView(_view));
            }

            if (_view == null)
            {
                _view = new SettingsView();
            }
            _vm = new SettingsVm(Self);
            _view.Vm = _vm;
            Context.Parent.Tell(new ShowView(_view));
            ModelRoot.Connection.Tell(new ConnectMessage());
        }

        private void JoinChat(JoinChatMessage msg)
        {
            ModelRoot.Connection.Tell(new JoinChatActionMessage(msg.UserName));
        }

        private void OnConnectionChanged(ConnectionChangedMessage msg)
        {
            var isConnected = msg.Status == ConectionStatus.Connected;
            _vm.IsOnline = isConnected;
            if (isConnected)
            {
                Become(Online);
            }
        }
    }
}