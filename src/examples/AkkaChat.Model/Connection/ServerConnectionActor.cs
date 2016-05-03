using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaChat.Model.Connection.Messages;
using AkkaChat.Model.Connection.Messages.ServerActions;
using AkkaChat.Model.Dto;
using AkkaChat.Model.SignalR;
using JetBrains.Annotations;

namespace AkkaChat.Model.Connection
{
    public sealed class ServerConnectionActor : ReceiveActor
    {
        [NotNull]
        private readonly IConnectionService _connectionService;
        [NotNull]
        private readonly HashSet<IActorRef> _subscribers;

        public ServerConnectionActor()
        {
            _connectionService = new ChatServerClient(Self);
            _subscribers = new HashSet<IActorRef>();
            Receive<SubscribeToConnectionChanged>(msg => AddSubscriber(msg.Subscriber));
            Become(Disconnected);
        }

        protected override void PostStop()
        {
            base.PostStop();
            _connectionService.Dispose();
        }

        private void AddSubscriber(IActorRef subscriber)
        {
            if (_subscribers.Contains(subscriber)) return;
            _subscribers.Add(subscriber);
        }

        private void OnIsConnectedChanged(ConnectionChangedMessage msg)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Tell(msg);
            }

            switch (msg.Status)
            {
                case ConectionStatus.Disconnected:
                    Become(Disconnected);
                    break;
                case ConectionStatus.Connecting:
                    Become(Connecting);
                    break;
                case ConectionStatus.Connected:
                    Become(Connected);
                    break;
                case ConectionStatus.Reconnecting:
                    Become(Connecting);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Disconnected()
        {
            Receive<ConnectMessage>(msg => DoConnect(msg.UserName));
        }

        private void Connecting()
        {
            Receive<ConnectionChangedMessage>(msg => OnIsConnectedChanged(msg));
        }

        private void Connected()
        {
            Receive<ConnectionChangedMessage>(msg => OnIsConnectedChanged(msg));
            Receive<JoinChatActionMessage>(msg => JoinChat(msg));
        }

        private void JoinChat(JoinChatActionMessage msg)
        {
            _connectionService.Invoke<JoinResult, string>("Login", msg.UserName).PipeTo(Sender);
        }

        private void DoConnect(string userName)
        {
            Become(Connecting);
            var self = Self;
            _connectionService.Connect().PipeTo(self);
        }
    }
}