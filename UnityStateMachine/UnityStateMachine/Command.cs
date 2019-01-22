using UnityStateMachine;

namespace StateMachineLibrary

{
    public class Command : ICommand

    {

        public Command(Transform transform, CommandTypes command)

        {

            this.CommandDirective = command;

        }

        public CommandTypes CommandDirective { get; private set; }
        public Transform transform { get; set; }
    }

}
