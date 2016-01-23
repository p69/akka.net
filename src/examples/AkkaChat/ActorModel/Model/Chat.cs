using System.Collections.Generic;
using Akka.Actor;
using AkkaChat.ActorModel.Messages;
using AkkaChat.Bootstrapping;
using AkkaChat.ViewModels;

namespace AkkaChat.ActorModel.Model
{
    public class Chat : ReceiveActor
    {
        private readonly MainViewModel _mainVm;
        private readonly Dictionary<string, IActorRef> _users = new Dictionary<string, IActorRef>();
        private readonly List<SendTextMessage> _messagesHistory = new List<SendTextMessage>();

        public Chat(MainViewModel mainVm)
        {
            _mainVm = mainVm;
            Receive<AddUserMessage>(msg => AddUser(msg));
            Receive<SendTextMessage>(msg => OnNewMessage(msg));
        }

        private void OnNewMessage(SendTextMessage sendTextMessage)
        {
            _messagesHistory.Add(sendTextMessage);
            _mainVm.Chat.OnTextMessageReceived(new TextReceivedMessage(sendTextMessage.UserName, sendTextMessage.Text));
        }

        private void AddUser(AddUserMessage message)
        {
            if (!_users.ContainsKey(message.Name))
            {
                var userActor = AppRoot.System.ActorOf(Props.Create(() => new User()), $"user-{_users.Count+1}");
                _users.Add(message.Name, userActor);
                _mainVm.OnUserJoint(new UserJointMessage(message.Name));
            }
        }
    }
}