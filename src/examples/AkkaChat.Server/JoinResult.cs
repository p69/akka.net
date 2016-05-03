namespace AkkaChat.Server
{
    public class JoinResult
    {
        public string Error { get; }

        public bool IsOk { get; }
        public JoinResult()
        {
            IsOk = true;
        }

        public JoinResult(string error)
        {
            Error = error;
        }
    }
}