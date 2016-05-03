using System.Threading.Tasks;
using Akka.Actor;
using AkkaChat.Model.Chat.User.Messages;
using AkkaChat.Model.Connection.Messages;
using AkkaChat.Model.Connection.Messages.ServerActions;
using AkkaChat.Model.Dto;
using JetBrains.Annotations;

namespace AkkaChat.Model.Chat.User
{
    public class UserActor : ReceiveActor
    {
        [NotNull]
        private readonly string _userName;

        public UserActor([NotNull] string userName)
        {
            _userName = userName;
            _connection = connection;
            _connection.Tell(new SubscribeToConnectionChanged(Self));
            Become(NotAuthorized);
        }

        private void NotAuthorized()
        {
            Receive<JoinChatMessage>(msg => JoinChatRoom());
        }

        private void Authorized()
        {
            Receive<ConnectionChangedMessage>(msg => OnConnectionStateChanged(msg));
        }

        private void Authorizing()
        {
            Receive<JoinResult>(msg=>)
        }

        private void OnConnectionStateChanged(ConnectionChangedMessage msg)
        {
            throw new System.NotImplementedException();
        }

        private void JoinChatRoom()
        {
            Become(Authorizing);
            _connection.Tell(new JoinChatActionMessage());
        }
    }
}