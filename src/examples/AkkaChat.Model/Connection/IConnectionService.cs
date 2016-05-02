using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AkkaChat.Model.Connection
{
    public interface IConnectionService
    {
        [NotNull]
        Task Connect(string userName);

        [NotNull]
        IObservable<bool> IsConnected { get; }

        [CanBeNull]
        string ConnectedUserName { get; }
    }
}