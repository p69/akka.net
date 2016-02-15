namespace AkkaChat.ActorModel
{
    public static class AkkaDIspatchers
    {
        public static string UiDispatcher = "akka.actor.synchronized-dispatcher";
        public static string ForkJoinDispatcher = "akka.actor.default-fork-join-dispatcher";
        public static string TaskDispatcher = "akka.actor.task-dispatcher";
    }
}