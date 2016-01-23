using System;

namespace AkkaChat.ViewModels
{
    public class ChatItemViewModel
    {
        public ChatItemViewModel(string userName, string text)
        {
            UserName = userName;
            Text = text;
        }

        public string Time { get; } = DateTime.Now.TimeOfDay.ToString();
        public string UserName { get; }
        public string Text { get; }
    }
}