namespace StateMachineLibrary

{
    public class Command : ICommand

    {

        public Command(CommandTypes command)

        {

            this.command = command;

        }

        public CommandTypes command { get; private set; }

    }

}
