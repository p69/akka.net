using System.Collections.ObjectModel;
using Akka.Actor;
using AkkaChat.ActorModel.Messages;
using AkkaChat.Bootstrapping;

namespace AkkaChat.ViewModels
{
    public sealed class UsersViewModel : BindableBase
    {
        public UsersViewModel()
        {
        }

        public void OnUserJoint(UserJointMessage message)
        {
            var userInput = new UserInputViewModel(message.UserName);
            UserInputs.Add(userInput);
        }

        public ObservableCollection<UserInputViewModel> UserInputs { get; } =
            new ObservableCollection<UserInputViewModel>();

        public void AddUser()
        {
            var userName = $"User {UserInputs.Count + 1}";
            AppRoot.System.ActorSelection(ActorPath.FormatPathElements(new[] {"user","chat"}))
                .Tell(new AddUserMessage(userName));
        }
    }
}