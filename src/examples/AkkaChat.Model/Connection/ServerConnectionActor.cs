using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaChat.Model.Connection.Messages;
using JetBrains.Annotations;

namespace AkkaChat.Model.Connection
{
    public sealed class ServerConnectionActor : ReceiveActor
    {
        [NotNull]
        private readonly IConnectionService _connectionService;
        [NotNull]
        private readonly IDisposable _connectionChangedSub;
        [NotNull]
        private readonly HashSet<IActorRef> _subscribers;

        public ServerConnectionActor([NotNull] IConnectionService connectionService)
        {
            _connectionService = connectionService;
            _subscribers = new HashSet<IActorRef>();
            _connectionChangedSub = _connectionService.IsConnected.Subscribe(OnIsConnectedChanged);
            Receive<SubscribeToConnectionChanged>(msg => AddSubscriber(msg.Subscriber));
            Become(NotConnected);
        }

        protected override void PostStop()
        {
            base.PostStop();
            _connectionChangedSub.Dispose();
        }

        private void AddSubscriber(IActorRef subscriber)
        {
            if (_subscribers.Contains(subscriber)) return;
            _subscribers.Add(subscriber);
        }

        private void OnIsConnectedChanged(bool isConnected)
        {
            if (isConnected)
            {
                Become(Connected);
            }
            else
            {
                Become(NotConnected);
            }
        }

        private void NotConnected()
        {
            Receive<ConnectMessage>(msg => DoConnect(msg.UserName));
            var message = new ConnectionChangedMessage(
                isConnected: false,
                connectedUser: null);
            foreach (var subscriber in _subscribers)
            {
                subscriber.Tell(message, Self);
            }
        }

        private void Connected()
        {
            var message = new ConnectionChangedMessage(
                isConnected: true,
                connectedUser: _connectionService.ConnectedUserName);
            foreach (var subscriber in _subscribers)
            {
                subscriber.Tell(message);
            }
        }

        private void DoConnect(string userName)
        {
            _connectionService.Connect(userName).ConfigureAwait(false);
        }
    }
}