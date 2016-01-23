namespace AkkaChat.ActorModel.Messages
{
    public sealed class AddUserMessage
    {
        public string Name { get; }

        public AddUserMessage(string name)
        {
            Name = name;
        }
    }
}