namespace StateMachineLibrary

{
    public class StateProgressEventArgs
    {
        public CommandTypes Command { get; private set; }

        public StateProgressEventArgs(CommandTypes command)
        {
            this.Command = Command;
        }
    }

}
