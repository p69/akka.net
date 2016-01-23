namespace AkkaChat.ActorModel.Messages
{
    public sealed class UserJointMessage
    {
        public string UserName { get; }

        public UserJointMessage(string userName)
        {
            UserName = userName;
        }
    }
}