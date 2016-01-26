using System.Collections.Generic;
using Akka.Actor;
using AkkaChat.ActorModel.Messages;
using AkkaChat.Bootstrapping;
using AkkaChat.ViewModels;

namespace AkkaChat.ActorModel.Model
{
    public class Chat : ReceiveActor
    {
        private readonly UsersViewModel _usersVm;
        private readonly Dictionary<string, IActorRef> _users = new Dictionary<string, IActorRef>();
        private readonly List<SendTextMessage> _messagesHistory = new List<SendTextMessage>();

        public Chat(UsersViewModel usersVm)
        {
            _usersVm = usersVm;
            Receive<AddUserMessage>(msg => AddUser(msg));
            Receive<SendTextMessage>(msg => OnNewMessage(msg));
        }

        private void OnNewMessage(SendTextMessage sendTextMessage)
        {
            _messagesHistory.Add(sendTextMessage);
            //_usersVm.Chat.OnTextMessageReceived(new TextReceivedMessage(sendTextMessage.UserName, sendTextMessage.Text));
        }

        private void AddUser(AddUserMessage message)
        {
            if (!_users.ContainsKey(message.Name))
            {
                var userActor = AppRoot.System.ActorOf(Props.Create(() => new User()), $"user-{_users.Count+1}");
                _users.Add(message.Name, userActor);
                _usersVm.OnUserJoint(new UserJointMessage(message.Name));
            }
        }
    }
}