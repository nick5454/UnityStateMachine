namespace StateMachineLibrary

{
    public class StateProgressEventArgs
    {
        public ICommand Command { get; private set; }

        public StateProgressEventArgs(ICommand command)
        {
            this.Command = Command;
        }
    }

}
