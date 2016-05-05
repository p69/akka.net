using System;
using System.Threading.Tasks;
using AkkaChat.Model.Connection.Messages;
using JetBrains.Annotations;

namespace AkkaChat.Model.Connection
{
    public interface IConnectionService : IDisposable
    {
        [NotNull]
        Task<ConnectionChangedMessage> Connect();

        [NotNull]
        Task<TResult> Invoke<TResult, TArg>(string actionName, params TArg[] args);

        [NotNull]
        Task Invoke<TArg>(string actionName, params TArg[] args);
    }
}