using System;
using Akka.Actor;
using AkkaChat.Model.Common;
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

        public ServerConnectionActor()
        {
            _connectionService = new ChatServerClient(Self);
            Become(Disconnected);
        }

        protected override void PostStop()
        {
            base.PostStop();
            _connectionService.Dispose();
        }
        private void OnIsConnectedChanged(ConnectionChangedMessage msg)
        {
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

            ModelRoot.System.EventStream.Publish(msg);
        }

        private void Disconnected()
        {
            Receive<ConnectMessage>(msg => DoConnect());
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

        private void DoConnect()
        {
            Become(Connecting);
            var self = Self;
            _connectionService.Connect().PipeTo(self);
        }
    }
}