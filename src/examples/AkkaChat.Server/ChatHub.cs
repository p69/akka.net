using System.Collections.Generic;
using Microsoft.AspNet.SignalR;

namespace AkkaChat.Server
{
    public class ChatHub : Hub
    {
        private static readonly HashSet<string> Users = new HashSet<string>();

        public JoinResult Login(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) ||
                Users.Contains(userName))
            {
                return new JoinResult(error: $"{userName} already joined");
            }
            Users.Add(userName);
            Clients.Others.userJoined(userName);
            return new JoinResult();
        }

        public void SendMessage(string userName, string text)
        {
            if (string.IsNullOrWhiteSpace(userName) || !Users.Contains(userName)) return;
            Clients.Others.newMessage(new TextMessage(userName, text));
        }
    }
}