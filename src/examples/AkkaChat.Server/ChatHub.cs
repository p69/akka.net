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

        public JoinResult Login(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) ||
                _users.Contains(userName))
            {
                return new JoinResult(error: $"{userName} already joined");
            }
            _users.Add(userName);
            Clients.Others.userJoined(userName);
            return new JoinResult();
        }

        public void SendMessage(string userName, string text)
        {
            if (string.IsNullOrWhiteSpace(userName) || !_users.Contains(userName)) return;
            Clients.Others.newMessage(userName, text);
        }
    }
}