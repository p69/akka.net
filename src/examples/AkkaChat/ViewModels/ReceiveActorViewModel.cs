using System.ComponentModel;
using System.Runtime.CompilerServices;
using Akka.Actor;

namespace AkkaChat.ViewModels
{
    public class ReceiveActorViewModel : ReceiveActor, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}