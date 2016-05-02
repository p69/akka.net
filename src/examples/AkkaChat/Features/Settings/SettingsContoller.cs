using System;
using Akka.Actor;
using AkkaChat.Features.Common.Messages.Navigation;
using AkkaChat.Model.Connection.Messages;
using JetBrains.Annotations;

namespace AkkaChat.Features.Settings
{
    public class SettingsContoller : ReceiveActor
    {
        private readonly Props _connectionActorProps;
        private IActorRef _connectionActor;
        private SettingsView _view;
        private ISettingsVm _vm;
        private IDisposable _viewActionsSub;

        public SettingsContoller([NotNull] Props connectionActorProps)
        {
            _connectionActorProps = connectionActorProps;
            Receive<OnNavigatedTo>(_ => OnNavigated());
            Receive<ConnectionChangedMessage>(msg => OnConnectionChanged(msg));
        }

        private void OnNavigated()
        {
            if (_view != null)
            {
                Context.Parent.Tell(new ShowView(_view));
            }
            else
            {
                _connectionActor = Context.ActorOf(_connectionActorProps, "connection");
                _connectionActor.Tell(new SubscribeToConnectionChanged(Context.Self));

                _view = new SettingsView();
                _viewActionsSub = _view.OnUserConnect.Subscribe(ConnectUser);
                _vm = new SettingsVm();
                _view.Vm = _vm;
                Context.Parent.Tell(new ShowView(_view));
            }

            if (_view == null)
            {
                _view = new SettingsView();
            }
            _vm = new SettingsVm();
            _view.Vm = _vm;
            Context.Parent.Tell(new ShowView(_view));
        }

        private void ConnectUser([NotNull] string userName)
        {
            var message = new ConnectMessage(userName);
            _connectionActor.Tell(message);
        }

        private void OnConnectionChanged(ConnectionChangedMessage msg)
        {
            var isConnected = msg.IsConnected;
        }
    }
}