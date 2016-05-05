using System.Diagnostics;
using Akka.Actor;
using AkkaChat.Model.Chat.Messages;
using AkkaChat.Model.Connection.Messages.ServerActions;
using AkkaChat.Model.Dto;

namespace AkkaChat.Model.Chat
{
    public class ChatActor : ReceiveActor
    {
        private string _userName;
        public ChatActor()
        {
            
        }

        private void LoggedOut()
        {
            Receive<LoginToChatRoomMessage>(
                msg =>
                {
                    _userName = msg.UserName;
                    ModelRoot.Connection.Tell(new JoinChatActionMessage(msg.UserName));
                });
            Receive<JoinResult>(
                joinResult =>
                {
                    if (joinResult.IsOk)
                    {
                        Become(LoggedIn);
                    }
                    else
                    {
                        Debug.WriteLine(joinResult.Error);
                    }
                });
        }

        private void LoggedIn()
        {
            Receive<SendTextMessage>(
                msg =>
                {
                    ModelRoot.Connection.Tell(new SendTextActionMessage(_userName, msg.Text));
                });
        }
    }
}