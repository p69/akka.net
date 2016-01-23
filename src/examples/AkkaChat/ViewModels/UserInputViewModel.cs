using Akka.Actor;
using AkkaChat.ActorModel.Messages;
using AkkaChat.Bootstrapping;

namespace AkkaChat.ViewModels
{
    public class UserInputViewModel : BindableBase
    {
        private string _text;
        private string _userName;

        public UserInputViewModel(string userName)
        {
            _userName = userName;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Say()
        {
            AppRoot.System.ActorSelection(ActorPath.FormatPathElements(new[] {"user", "chat"}))
                .Tell(new SendTextMessage(UserName, Text));
        }
    }
}