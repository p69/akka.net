using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using AkkaChat.Model.Connection;
using JetBrains.Annotations;
using Microsoft.AspNet.SignalR.Client;

namespace AkkaChat.Model.SignalR
{
    public class ChatServerClient : IConnectionService
    {
        [NotNull]
        private static string _serverUri = @"http://localhost:8069/akka/";
        [NotNull]
        private readonly Subject<bool> _connectedSubject = new Subject<bool>(); 
        private IHubProxy _proxy;
        private HubConnection _connection;

        public async Task Connect(string userName)
        {
            if (!string.IsNullOrWhiteSpace(ConnectedUserName))
            {
                throw new InvalidOperationException($"{ConnectedUserName} already connected. Please disconnect first!");
            }

            var proxy = GetProxy();

            proxy.On(
                "joinOk",
                () =>
                {
                    ConnectedUserName = userName;
                    _connectedSubject.OnNext(true);
                });
            await _connection.Start();
            await proxy.Invoke("Login", userName);
        }

        public IObservable<bool> IsConnected => _connectedSubject;
        public string ConnectedUserName { get; private set; }

        [NotNull]
        private IHubProxy GetProxy()
        {
            if (_proxy != null) return _proxy;

            _connection = new HubConnection(_serverUri);
            _connection.Error += ConnectionOnError;
            _proxy = _connection.CreateHubProxy("ChatHub");
            return _proxy;
        }

        private void ConnectionOnError(Exception exception)
        {
        }
    }
}