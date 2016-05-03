using System;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaChat.Model.Connection;
using AkkaChat.Model.Connection.Messages;
using JetBrains.Annotations;
using Microsoft.AspNet.SignalR.Client;

namespace AkkaChat.Model.SignalR
{
    public class ChatServerClient : IConnectionService
    {
        [NotNull]
        private readonly IActorRef _connectionActor;
        [NotNull]
        private static string _serverUri = @"http://localhost:8069/akka/";
        
        private IHubProxy _proxy;
        private HubConnection _connection;
        private bool _isInited;

        public ChatServerClient([NotNull] IActorRef connectionActor)
        {
            _connectionActor = connectionActor;
        }

        public async Task<ConnectionChangedMessage> Connect()
        {
            if (_isInited)
            {
                return ConnectionChangedMessage.Error("Already connected.");
            }

            try
            {
                _connectionActor.Tell(ConnectionChangedMessage.Connecting());

                Init();
                await _connection.Start();
                return ConnectionChangedMessage.Connected();
            }
            catch (Exception ex)
            {
                return ConnectionChangedMessage.Error(ex.ToString());
            }
        }

        public async Task<TResult> Invoke<TResult, TArg>(string actionName, TArg arg)
        {
            var result = await _proxy.Invoke<TResult>(actionName, arg);
            return result;
        }

        private void Init()
        {
            if (_isInited) return;

            _connection = new HubConnection(_serverUri);
            _connection.Error += ConnectionOnError;
            _connection.StateChanged += ConnectionOnStateChanged;
            _proxy = _connection.CreateHubProxy("ChatHub");
            _isInited = true;
        }

        private void ConnectionOnStateChanged(StateChange stateChange)
        {
            switch (stateChange.NewState)
            {
                case ConnectionState.Connecting:
                    _connectionActor.Tell(ConnectionChangedMessage.Connecting());
                    break;
                case ConnectionState.Connected:
                    _connectionActor.Tell(ConnectionChangedMessage.Connected());
                    break;
                case ConnectionState.Reconnecting:
                    _connectionActor.Tell(ConnectionChangedMessage.Reconnecting());
                    break;
                case ConnectionState.Disconnected:
                    _connectionActor.Tell(ConnectionChangedMessage.Disconnected());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ConnectionOnError(Exception exception)
        {
            _connectionActor.Tell(ConnectionChangedMessage.Error(exception.ToString()));
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _proxy = null;
        }
    }
}