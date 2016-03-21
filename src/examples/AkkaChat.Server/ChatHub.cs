using System.Collections.Generic;
using Microsoft.AspNet.SignalR;

namespace AkkaChat.Server
{
    public class ChatHub : Hub
    {
        private readonly HashSet<string> _users;

        public ChatHub()
        {
            _users = new HashSet<string>();
        }

        public void Login(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) ||
                _users.Contains(userName))
            {
                Clients.Caller.joinFailed();
                return;
            }
            _users.Add(userName);
            Clients.Others.userJoined(userName);
            Clients.Caller.joinOk();
        }

        public void SendMessage(string userName, string text)
        {
            if (string.IsNullOrWhiteSpace(userName) || !_users.Contains(userName)) return;
            Clients.Others.newMessage(userName, text);
        }
    }
}